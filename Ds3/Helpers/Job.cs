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

namespace Ds3.Helpers
{
    internal abstract class Job : IJob
    {
        private readonly IDs3ClientFactory _clientFactory;
        private readonly IEnumerable<Ds3ObjectList> _objectLists;

        private int _maxParallelRequests = 0;

        protected int MaxParallelRequests
        {
            set { this._maxParallelRequests = value; }
        }

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
            var options = _maxParallelRequests > 0
                ? new ParallelOptions() { MaxDegreeOfParallelism = _maxParallelRequests }
                : new ParallelOptions();
            foreach (var objects in this._objectLists)
            {
                var client = this._clientFactory.GetClientForServerId(objects.ServerId);
                Parallel.ForEach(objects.Objects, options, obj =>
                {
                    transfer(client, JobId, BucketName, obj);
                });
            }
        }
    }
}
