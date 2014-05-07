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

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Ds3.Models;
using IJob = Ds3.Helpers.Ds3ClientHelpers.IJob;

namespace Ds3.Helpers
{
    internal abstract class Job : IJob
    {
        private readonly IDs3ClientFactory _clientFactory;
        private readonly IEnumerable<Ds3ObjectList> _objectLists;

        public Guid JobId { get; private set; }
        public string BucketName { get; private set; }

        public Job(
            IDs3ClientFactory clientFactory,
            Guid jobId,
            string bucketName,
            IEnumerable<Ds3ObjectList> objectLists)
        {
            this._clientFactory = clientFactory;
            this.JobId = jobId;
            this.BucketName = bucketName;
            this._objectLists = objectLists;
        }

        protected delegate void Transfer(IDs3Client client, Guid jobId, string bucket, Ds3Object ds3Object);

        protected void TransferAll(Transfer transfer)
        {
            Parallel.ForEach(this._objectLists, objects =>
            {
                var client = this._clientFactory.GetClientForServerId(objects.ServerId);
                foreach (var obj in objects.Objects)
                {
                    transfer(client, JobId, BucketName, obj);
                }
            });
        }
    }
}
