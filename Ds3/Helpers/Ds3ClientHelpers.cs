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
using System.Text;

namespace Ds3.Helpers
{
    public class Ds3ClientHelpers : IDs3ClientHelpers
    {
        private const int _defaultMaxKeys = 1000;

        private readonly IDs3Client _client;

        public Ds3ClientHelpers(IDs3Client client)
        {
            this._client = client;
        }

        public IWriteJob StartWriteJob(string bucket, IEnumerable<Ds3Object> objectsToWrite)
        {
            using (var prime = this._client.BulkPut(new BulkPutRequest(bucket, objectsToWrite.ToList())))
            {
                return new WriteJob(new Ds3ClientFactory(this._client), prime.JobId, bucket, prime.ObjectLists);
            }
        }

        public IReadJob StartReadJob(string bucket, IEnumerable<Ds3Object> objectsToRead)
        {
            using (var prime = this._client.BulkGet(new BulkGetRequest(bucket, objectsToRead.ToList())))
            {
                return new ReadJob(new Ds3ClientFactory(this._client), prime.JobId, bucket, prime.ObjectLists);
            }
        }

        public IReadJob StartReadAllJob(string bucket)
        {
            return this.StartReadJob(bucket, this.ListObjects(bucket));
        }

        public IEnumerable<Ds3Object> ListObjects(string bucketName)
        {
            return ListObjects(bucketName, null, int.MaxValue);
        }

        public IEnumerable<Ds3Object> ListObjects(string bucketName, string keyPrefix)
        {
            return ListObjects(bucketName, keyPrefix, int.MaxValue);
        }

        public IEnumerable<Ds3Object> ListObjects(string bucketName, string keyPrefix, int maxKeys)
        {
            var remainingKeys = maxKeys;
            var isTruncated = false;
            string marker = null;
            do
            {
                var request = new Ds3.Calls.GetBucketRequest(bucketName)
                {
                    Marker = marker,
                    MaxKeys = Math.Min(remainingKeys, _defaultMaxKeys),
                    Prefix = keyPrefix
                };
                using (var response = _client.GetBucket(request))
                {
                    isTruncated = response.IsTruncated;
                    marker = response.NextMarker;
                    remainingKeys -= response.Objects.Count;
                    foreach (var ds3Object in response.Objects)
                    {
                        yield return ds3Object;
                    }
                }
            } while (isTruncated && remainingKeys > 0);
        }


        public void EnsureBucketExists(string bucketName)
        {
            using (var headResponse = _client.HeadBucket(new HeadBucketRequest(bucketName)))
            {
                if (headResponse.Status == HeadBucketResponse.StatusType.DoesntExist)
                {
                    using (_client.PutBucket(new PutBucketRequest(bucketName)))
                    {
                    }
                }
            }
        }

        public IWriteJob RecoverWriteJob(Guid jobId)
        {
            using (var job = this._client.GetJob(new GetJobRequest(jobId)))
            {
                return new WriteJob(new Ds3ClientFactory(this._client), jobId, job.Bucket.Name, job.ObjectLists);
            }
        }

        public IReadJob RecoverReadJob(Guid jobId)
        {
            using (var job = this._client.GetJob(new GetJobRequest(jobId)))
            {
                return new ReadJob(new Ds3ClientFactory(this._client), jobId, job.Bucket.Name, job.ObjectLists);
            }
        }
    }
}
