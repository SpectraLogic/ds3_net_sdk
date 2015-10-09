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
using Ds3.Helpers.Jobs;
using Ds3.Helpers.TransferItemSources;
using Ds3.Helpers.Transferrers;
using Ds3.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ds3.Helpers
{
    public class Ds3ClientHelpers : IDs3ClientHelpers
    {
        private const int _defaultMaxKeys = 1000;

        private readonly IDs3Client _client;
        private const string JobTypePut = "PUT";
        private const string JobTypeGet = "GET";
        private readonly int _retryAfter; //-1 represent infinite number

        public Ds3ClientHelpers(IDs3Client client, int retryAfter = -1)
        {
            this._client = client;
            this._retryAfter = retryAfter;
        }

        public IJob StartWriteJob(string bucket, IEnumerable<Ds3Object> objectsToWrite, long? maxBlobSize = null)
        {
            var request = new BulkPutRequest(
                bucket,
                VerifyObjectCount(objectsToWrite)
            );
            if (maxBlobSize.HasValue)
            {
                request.WithMaxBlobSize(maxBlobSize.Value);
            }
            var jobResponse = this._client.BulkPut(request);
            return FullObjectJob.Create(
                jobResponse,
                new WriteTransferItemSource(this._client, this._retryAfter, jobResponse),
                new WriteTransferrer()
            );
        }

        public IJob StartReadJob(string bucket, IEnumerable<Ds3Object> objectsToRead)
        {
            var jobResponse = this._client.BulkGet(
                new BulkGetRequest(bucket, VerifyObjectCount(objectsToRead))
                    .WithChunkOrdering(ChunkOrdering.None)
            );
            return FullObjectJob.Create(
                jobResponse,
                new ReadTransferItemSource(this._client, this._retryAfter, jobResponse),
                new ReadTransferrer()
            );
        }

        public IPartialReadJob StartPartialReadJob(
            string bucket,
            IEnumerable<string> fullObjects,
            IEnumerable<Ds3PartialObject> partialObjects)
        {
            var partialObjectList = new SortedSet<Ds3PartialObject>(partialObjects);
            var fullObjectList = fullObjects.ToList();
            if (partialObjectList.Count + fullObjectList.Count == 0)
            {
                throw new InvalidOperationException(Resources.NoObjectsToTransferException);
            }
            var jobResponse = this._client.BulkGet(
                new BulkGetRequest(bucket, fullObjectList, partialObjectList)
                    .WithChunkOrdering(ChunkOrdering.None)
            );
            return PartialReadJob.Create(
                jobResponse,
                fullObjectList,
                partialObjectList,
                new ReadTransferItemSource(this._client, this._retryAfter, jobResponse)
            );
        }

        private static List<T> VerifyObjectCount<T>(IEnumerable<T> objects)
        {
            var objectList = objects.ToList();
            if (objectList.Count == 0)
            {
                throw new InvalidOperationException(Resources.NoObjectsToTransferException);
            }
            return objectList;
        }

        public IJob StartReadAllJob(string bucket)
        {
            return this.StartReadJob(bucket, this.ListObjects(bucket));
        }

        public IEnumerable<Ds3Object> ListObjects(string bucketName)
        {
            return ListObjects(bucketName, null);
        }

        public IEnumerable<Ds3Object> ListObjects(string bucketName, string keyPrefix)
        {
            var isTruncated = false;
            string marker = null;
            do
            {
                var request = new Ds3.Calls.GetBucketRequest(bucketName)
                {
                    Marker = marker,
                    Prefix = keyPrefix
                };
                var response = _client.GetBucket(request);
                isTruncated = response.IsTruncated;
                marker = response.NextMarker;
                var responseObjects = response.Objects as IList<Ds3ObjectInfo> ?? response.Objects.ToList();
                foreach (var ds3Object in responseObjects)
                {
                    yield return ds3Object;
                }
            } while (isTruncated);
        }

        public void EnsureBucketExists(string bucketName)
        {
            var headResponse = _client.HeadBucket(new HeadBucketRequest(bucketName));
            if (headResponse.Status == HeadBucketResponse.StatusType.DoesntExist)
            {
                _client.PutBucket(new PutBucketRequest(bucketName));
            }
        }

        public IJob RecoverWriteJob(Guid jobId)
        {
            var jobResponse = this._client.ModifyJob(new ModifyJobRequest(jobId));
            if (jobResponse.RequestType != JobTypePut)
            {
                throw new InvalidOperationException(Resources.ExpectedPutJobButWasGetJobException);
            }
            return FullObjectJob.Create(
                jobResponse,
                new WriteTransferItemSource(this._client, this._retryAfter, jobResponse),
                new WriteTransferrer()
            );
        }
    }
}
