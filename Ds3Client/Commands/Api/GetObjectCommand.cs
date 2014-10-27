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
using System;
using System.Linq;
using System.Management.Automation;

namespace Ds3Client.Commands.Api
{
    [Cmdlet(VerbsCommon.Get, DS3Nouns.Object)]
    public class GetObjectCommand : BaseApiCommand
    {
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Mandatory = true)]
        public string BucketName { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public string KeyPrefix { get; set; }

        [Parameter(ValueFromPipelineByPropertyName = true)]
        public int? MaxKeys { get; set; }

        protected override void ProcessRecord()
        {
            var clientHelper = new Ds3ClientHelpers(CreateClient());
            var ds3Objects = clientHelper.ListObjects(BucketName, KeyPrefix);
            if (MaxKeys.HasValue)
            {
                ds3Objects = ds3Objects.Take(MaxKeys.Value);
            }
            foreach (var ds3Object in ds3Objects)
            {
                WriteObject(ds3Object);
            }
        }
    }
}
