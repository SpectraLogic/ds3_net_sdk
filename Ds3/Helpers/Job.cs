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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ds3.Helpers
{
    internal abstract class Job : IJob
    {
        protected readonly IDs3Client _client;
        protected readonly JobResponse _bulkResponse;
        protected CancellationToken _cancellationToken = CancellationToken.None;
        private int _maxParallelRequests = 0;

        public event Action<long> DataTransferred;
        public event Action<string> ObjectCompleted;

        protected Job(IDs3Client client, JobResponse bulkResponse)
        {
            this._client = client;
            this._bulkResponse = bulkResponse;
        }

        public Guid JobId
        {
            get { return this._bulkResponse.JobId; }
        }

        public string BucketName
        {
            get { return this._bulkResponse.BucketName; }
        }

        public IJob WithMaxParallelRequests(int maxParallelRequests)
        {
            this._maxParallelRequests = maxParallelRequests;
            return this;
        }

        public IJob WithCancellationToken(CancellationToken cancellationToken)
        {
            this._cancellationToken = cancellationToken;
            return this;
        }

        public abstract void Transfer(Func<string, Stream> createStreamForObjectKey);

        protected void OnObjectCompleted(string objectName)
        {
            if (this.ObjectCompleted != null)
            {
                this.ObjectCompleted(objectName);
            }
        }

        protected void OnDataTransferred(long byteCount)
        {
            if (this.DataTransferred != null)
            {
                this.DataTransferred(byteCount);
            }
        }

        protected void InParallel<T>(IEnumerable<T> things, Action<T> action)
        {
            var parallelOptions = new ParallelOptions { CancellationToken = this._cancellationToken };
            if (this._maxParallelRequests > 0)
            {
                parallelOptions.MaxDegreeOfParallelism = this._maxParallelRequests;
            }
            Parallel.ForEach(things, parallelOptions, action);
        }
    }
}
