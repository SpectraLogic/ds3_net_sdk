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

using Ds3.Calls;
using Ds3.Models;

namespace Ds3.Helpers
{
    internal class WriteJob : Job<PutObjectRequest>, IWriteJob
    {
        public WriteJob(IDs3Client client, JobResponse jobResponse)
            : base(client, jobResponse)
        {
        }

        protected override void TransferJobObject(IDs3Client client, JobObjectRequest jobObjectRequest)
        {
            client.PutObject(new PutObjectRequest(
                jobObjectRequest.BucketName,
                jobObjectRequest.ObjectName,
                jobObjectRequest.JobId,
                jobObjectRequest.Offset,
                jobObjectRequest.Stream
            ));
        }

        protected override bool ShouldTransferJobObject(JobObject jobObject)
        {
            return !jobObject.InCache;
        }
    }
}
