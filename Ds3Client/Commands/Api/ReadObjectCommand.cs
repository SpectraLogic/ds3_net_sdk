using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Management.Automation;
using IOFile = System.IO.File;
using Ds3Client.Api;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Ds3Client.Commands.Api
{
    [Cmdlet(VerbsCommunications.Read, DS3Nouns.Object)]
    public class ReadObjectCommand : BaseApiCommand
    {
        private const string ToLocalFileParamSet = "ToLocalFileParamSet";
        private const string ToLocalFolderParamSet = "ToLocalFolderParamSet";

        [Parameter(Position = 0, Mandatory = true, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true)]
        public string BucketName { get; set; }

        [Alias(new string[] { "Name" })]
        [Parameter(Position = 1, ParameterSetName = ToLocalFileParamSet, ValueFromPipelineByPropertyName = true, Mandatory = true)]
        public string Key { get; set; }

        [Parameter(Position = 2, ParameterSetName = ToLocalFileParamSet, Mandatory = true)]
        public string File { get; set; }

        [Parameter(ParameterSetName = ToLocalFileParamSet)]
        public long? Start { get; set; }

        [Parameter(ParameterSetName = ToLocalFileParamSet)]
        public long? End { get; set; }

        [Alias(new string[] { "Prefix" })]
        [Parameter(Position = 1, ParameterSetName = ToLocalFolderParamSet, ValueFromPipelineByPropertyName = true)]
        public string KeyPrefix { get; set; }

        [Alias(new string[] { "Directory" })]
        [Parameter(Position = 2, ParameterSetName = ToLocalFolderParamSet, Mandatory = true)]
        public string Folder { get; set; }

        protected override void ProcessRecord()
        {
            switch (this.ParameterSetName)
            {
                case ToLocalFileParamSet: WriteToLocalFile(); break;
                case ToLocalFolderParamSet: WriteToLocalFolder(); break;
                default: throw new ApiException(Resources.InvalidParameterSetException);
            }
        }

        private void WriteToLocalFile()
        {
            if (IOFile.Exists(File))
            {
                throw new ApiException(Resources.FileAlreadyExistsException, File);
            }

            WriteObjectToFile(CreateClient(), Key, MakeValidPath(File));
        }

        private void WriteToLocalFolder()
        {
            if (Directory.Exists(Folder))
            {
                throw new ApiException(Resources.DirectoryAlreadyExistsException, Folder);
            }

            var client = CreateClient();
            var resultObjects = GetAllObjectsHelper.GetAllObjects(client, BucketName, KeyPrefix).ToList();
            using (var bulkGet = client.BulkGet(new Ds3.Calls.BulkGetRequest(BucketName, resultObjects)))
            {
                try
                {
                    Parallel.ForEach(bulkGet.ObjectLists, ds3ObjectList =>
                    {
                        foreach (var key in from ds3Object in ds3ObjectList select ds3Object.Name)
                        {
                            WriteObjectToFile(client, key, EnsureDirectoryForFileExists(Path.Combine(Folder, MakeValidPath(key))));
                        }
                    });
                }
                catch (AggregateException e)
                {
                    foreach (var innerException in e.InnerExceptions)
	                {
                        WriteError(new ErrorRecord(innerException, "GetFailed", ErrorCategory.ReadError, innerException));
	                }
                }
            }
        }

        private static object _ensureDirectoryLock = new object();

        private string EnsureDirectoryForFileExists(string filePath)
        {
            var destinationDirectory = Path.GetDirectoryName(filePath);
            lock(_ensureDirectoryLock)
            {
                if (!Directory.Exists(destinationDirectory))
                {
                    Directory.CreateDirectory(destinationDirectory);
                }
            }
            return filePath;
        }

        private void WriteObjectToFile(Ds3.Ds3Client client, string key, string file)
        {
            var request = new Ds3.Calls.GetObjectRequest(BucketName, key);
            if (Start.HasValue && End.HasValue)
            {
                request.WithByteRange(new Ds3.Calls.GetObjectRequest.Range(Start.Value, End.Value));
            }
            using (var response = client.GetObject(request))
            using (var outputStream = IOFile.OpenWrite(file))
            {
                response.Contents.CopyTo(outputStream);
            }
        }

        private static string MakeValidPath(string path)
        {
            return string.Join("\\", path.Split('/', '\\').Select(MakeValidFileName).ToArray());
        }

        private static string MakeValidFileName(string name)
        {
            return Regex.Replace(name, string.Format(@"([{0}]*\.+$)|([{0}]+)", Regex.Escape(new string(Path.GetInvalidFileNameChars()))), "_");
        }
    }
}
