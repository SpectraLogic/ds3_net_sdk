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

using Ds3.Helpers;
using Ds3.Models;
using Ds3Client.Api;
using System.IO;
using System.Management.Automation;

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
            var file = Path.GetFullPath(Path.Combine(this.SessionState.Path.CurrentFileSystemLocation.Path, File));
            if (!System.IO.File.Exists(file))
            {
                throw new ApiException(Resources.FileDoesNotExistException, file);
            }
            var ds3Obj = new Ds3Object(this.Key, new System.IO.FileInfo(file).Length);
            new Ds3ClientHelpers(CreateClient())
                .StartWriteJob(BucketName, new[] { ds3Obj })
                .Transfer(key => System.IO.File.OpenRead(file));
        }

        private void WriteFromLocalFolder()
        {
            var folder = Path.GetFullPath(Path.Combine(this.SessionState.Path.CurrentFileSystemLocation.Path, Folder));
            if (!Directory.Exists(folder))
            {
                throw new ApiException(Resources.DirectoryDoesNotExistException, folder);
            }
            new Ds3ClientHelpers(CreateClient())
                .StartWriteJob(BucketName, FileHelpers.ListObjectsForDirectory(folder))
                .Transfer(FileHelpers.BuildFilePutter(folder));
        }
    }
}
