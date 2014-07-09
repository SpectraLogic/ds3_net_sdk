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
using System.Linq;

using Ds3.Calls;
using Ds3.Models;

namespace Ds3.Helpers
{
    public class Ds3ClientHelpers : IDs3ClientHelpers
    {
        private const int _defaultMaxKeys = 1000;

        private readonly IDs3Client _client;
        private const string JobTypePut = "PUT";
        private const string JobTypeGet = "GET";

        public Ds3ClientHelpers(IDs3Client client)
        {
            this._client = client;
        }

        public IWriteJob StartWriteJob(string bucket, IEnumerable<Ds3Object> objectsToWrite)
        {
            return new WriteJob(this._client, this._client.BulkPut(new BulkPutRequest(bucket, objectsToWrite.ToList())));
        }

        public IReadJob StartReadJob(string bucket, IEnumerable<Ds3Object> objectsToRead)
        {
            return new ReadJob(this._client, this._client.BulkGet(new BulkGetRequest(bucket, objectsToRead.ToList())));
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
                var response = _client.GetBucket(request);
                isTruncated = response.IsTruncated;
                marker = response.NextMarker;
                var responseObjects = response.Objects as IList<Ds3ObjectInfo> ?? response.Objects.ToList();
                remainingKeys -= responseObjects.Count;
                foreach (var ds3Object in responseObjects)
                {
                    yield return ds3Object;
                }
            } while (isTruncated && remainingKeys > 0);
        }

        public void EnsureBucketExists(string bucketName)
        {
            var headResponse = _client.HeadBucket(new HeadBucketRequest(bucketName));
            if (headResponse.Status == HeadBucketResponse.StatusType.DoesntExist)
            {
                _client.PutBucket(new PutBucketRequest(bucketName));
            }
        }

        public IWriteJob RecoverWriteJob(Guid jobId)
        {
            return new WriteJob(this._client, this._client.GetJob(new GetJobRequest(jobId)));
        }

        public IReadJob RecoverReadJob(Guid jobId)
        {
            return new ReadJob(this._client, this._client.GetJob(new GetJobRequest(jobId)));
        }

        private static void CheckJobType(string expectedJobType, string actualJobType)
        {
            if (actualJobType != expectedJobType)
            {
                throw new JobRecoveryException(expectedJobType, actualJobType);
            }
        }
    }
}
