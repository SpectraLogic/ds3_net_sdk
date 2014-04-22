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

using System.Linq;
using System.Management.Automation;
using Ds3Client.Configuration;
using Ds3Client.Api;

namespace Ds3Client.Commands.Api
{
    [Cmdlet(VerbsCommon.Get, DS3Nouns.Bucket)]
    public class GetBucketCommand : BaseApiCommand
    {
        [Parameter(Position = 0, ValueFromPipelineByPropertyName = true, ValueFromPipeline = true)]
        public string BucketName { get; set; }

        protected override void ProcessRecord()
        {
            if (BucketName != null)
            {
                throw new ApiException(Resources.BucketNameNotImplementedException);
            }

            using (var response = CreateClient().GetService(new Ds3.Calls.GetServiceRequest()))
            {
                foreach (var bucket in response.Buckets)
                {
                    WriteObject(bucket);
                }
            }
        }
    }
}
