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
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace Ds3.Helpers
{
    internal class ReadJob : Job
    {
        public ReadJob(IDs3Client client, JobResponse bulkGetResponse)
            : base(client, bulkGetResponse)
        {
        }

        protected override void TransferChunk(IDs3Client clientForNode, Dictionary<string, Stream> objectStreams, IEnumerable<JobObject> jobObjects)
        {
            //jobObjects
            //    .AsParallel()
            //    .WithDegreeOfParallelism(_maxParallelRequests)
            //    .Select(jobObject => );
            throw new System.NotImplementedException();
            //clientForNode.GetObject(new GetObjectRequest(
            //    jobObjectRequest.BucketName,
            //    jobObjectRequest.ObjectName,
            //    jobObjectRequest.JobId,
            //    jobObjectRequest.Offset,
            //    jobObjectRequest.Stream
            //));
        }
    }
}
