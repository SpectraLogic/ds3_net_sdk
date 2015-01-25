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
using System.Linq;
using System.Management.Automation;
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

        [Alias(new string[] { "Directory" })]
        [Parameter(ParameterSetName = ToLocalFolderParamSet, Mandatory = true)]
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
            var file = Path.GetFullPath(Path.Combine(this.SessionState.Path.CurrentFileSystemLocation.Path, File));
            if (System.IO.File.Exists(file))
            {
                throw new ApiException(Resources.FileAlreadyExistsException, file);
            }

            new Ds3ClientHelpers(CreateClient())
                .StartReadJob(this.BucketName, new[] { new Ds3Object(this.Key, null) })
                .Transfer(key => System.IO.File.OpenWrite(file));
        }

        private void WriteToLocalFolder()
        {
            var folder = Path.GetFullPath(Path.Combine(this.SessionState.Path.CurrentFileSystemLocation.Path, Folder));
            if (Directory.Exists(folder))
            {
                throw new ApiException(Resources.DirectoryAlreadyExistsException, folder);
            }

            new Ds3ClientHelpers(CreateClient())
                .StartReadAllJob(BucketName)
                .Transfer(FileHelpers.BuildFileGetter(folder));
        }
    }
}
