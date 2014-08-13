/*
 * ******************************************************************************
 *   Copyright 2014 Spectra Logic Corporation. All Rights Reserved.
 *   Licensed under the Apache License, Version 2.0 (the "License"). You may not use
 *   this file except in compliance with the License. A copy of the License is located at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 *   or in the "license" file accompanying this file.
 *   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
 *   CONDITIONS OF ANY KIND, either express or implied. See the License for the
 *   specific language governing permissions and limitations under the License.
 * ****************************************************************************
 */

using Ds3Client.Api;
using System;
using System.IO;
using System.Linq;
using System.Management.Automation;
using System.Threading.Tasks;
using IOFile = System.IO.File;

namespace Ds3Client.Commands.Api
{
    [Cmdlet(VerbsCommunications.Write, DS3Nouns.Object)]
    public class WriteObjectCommand : BaseApiCommand
    {
        private const string FromLocalFileParamSet = "FromLocalFileParamSet";
        private const string FromLocalFolderParamSet = "FromLocalFolderParamSet";

        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Mandatory = true)]
        public string BucketName { get; set; }

        [Alias(new string[] { "Name" })]
        [Parameter(ParameterSetName = FromLocalFileParamSet, Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string Key { get; set; }

        [Parameter(ParameterSetName = FromLocalFileParamSet, Mandatory = true)]
        public string File { get; set; }

        [Alias(new string[] { "Prefix" })]
        [Parameter(ParameterSetName = FromLocalFolderParamSet, Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string KeyPrefix { get; set; }

        [Alias(new string[] { "Directory" })]
        [Parameter(ParameterSetName = FromLocalFolderParamSet, Mandatory = true)]
        public string Folder { get; set; }

        [Parameter(ParameterSetName = FromLocalFolderParamSet)]
        public SwitchParameter Recurse { get; set; }

        [Alias(new string[] { "Pattern" })]
        [Parameter(ParameterSetName = FromLocalFolderParamSet)]
        public string SearchPattern { get; set; }


        protected override void ProcessRecord()
        {
            switch (this.ParameterSetName)
            {
                case FromLocalFileParamSet: WriteFromLocalFile(); break;
                case FromLocalFolderParamSet: WriteFromLocalFolder(); break;
                default: throw new ApiException(Resources.InvalidParameterSetException);
            }
        }

        private void WriteFromLocalFile()
        {
            using (var fileStream = IOFile.OpenRead(File))
            {
                CreateClient().PutObject(new Ds3.Calls.PutObjectRequest(BucketName, Key, fileStream));
            }
        }

        private void WriteFromLocalFolder()
        {
            var keyToFileMapping = Directory
                .GetFiles(Folder, SearchPattern ?? "*", Recurse.IsPresent ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly)
                .ToDictionary(file => (KeyPrefix ?? "") + file.Substring(Folder.Length).Replace('\\', '/'));
            var ds3ObjectsToQuery = keyToFileMapping.Select(item => new Ds3.Models.Ds3Object(item.Key, new FileInfo(item.Value).Length)).ToList();
            var client = CreateClient();
            var bulkPutResponse = client.BulkPut(new Ds3.Calls.BulkPutRequest(BucketName, ds3ObjectsToQuery));
            try
            {
                Parallel.ForEach(bulkPutResponse.ObjectLists, ds3ObjectList =>
                {
                    foreach (var key in from ds3Object in ds3ObjectList select ds3Object.Name)
                    {
                        using (var fileStream = IOFile.OpenRead(keyToFileMapping[key]))
                        {
                            client.PutObject(new Ds3.Calls.PutObjectRequest(BucketName, key, fileStream));
                        }
                    }
                });
            }
            catch (AggregateException e)
            {
                foreach (var innerException in e.InnerExceptions)
	            {
                    WriteError(new ErrorRecord(innerException, "PutFailed", ErrorCategory.WriteError, innerException));
	            }
            }
        }
    }
}
