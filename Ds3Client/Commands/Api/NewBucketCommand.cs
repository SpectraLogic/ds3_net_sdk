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

using System.Management.Automation;

namespace Ds3Client.Commands.Api
{
    [Cmdlet(VerbsCommon.New, DS3Nouns.Bucket)]
    public class NewBucketCommand : BaseApiCommand
    {
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true, Mandatory = true)]
        public string BucketName { get; set; }

        protected override void ProcessRecord()
        {
            CreateClient().PutBucket(new Ds3.Calls.PutBucketRequest(BucketName));
        }
    }
}
