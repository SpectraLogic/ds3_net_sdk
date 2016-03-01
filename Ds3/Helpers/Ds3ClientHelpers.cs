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
using Ds3.Helpers.Transferrers;
using Ds3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Ds3.Helpers.Strategys;
using Ds3.Runtime;

namespace Ds3.Helpers
{
    public class Ds3ClientHelpers : IDs3ClientHelpers
    {
        private const int DefaultMaxKeys = 1000;

        private readonly IDs3Client _client;
        private const string JobTypePut = "PUT";
        private const string JobTypeGet = "GET";
        private readonly int _retryAfter; //-1 represent infinite number
        private readonly int _getObjectRetries; // -1 represents infinite number of retries
        private readonly int _jobRetries;
        private readonly int _jobWaitTime; //in minutes

        public Ds3ClientHelpers(IDs3Client client, int retryAfter = -1, int getObjectRetries = 5, int jobRetries = -1, int jobWaitTime = 5)
        {
            this._client = client;
            this._retryAfter = retryAfter;
            this._getObjectRetries = getObjectRetries;
            this._jobRetries = jobRetries;
            this._jobWaitTime = jobWaitTime;
        }

        public IJob StartWriteJob(string bucket, IEnumerable<Ds3Object> objectsToWrite, long? maxBlobSize = null, Type helperStrategyType = null)
        {
            var request = new BulkPutRequest(
                bucket,
                VerifyObjectCount(objectsToWrite)
            );
            if (maxBlobSize.HasValue)
            {
                request.WithMaxBlobSize(maxBlobSize.Value);
            }
            JobResponse jobResponse = null;
            var retriesLeft = this._jobRetries;
            do
            {
                try
                {
                    jobResponse = this._client.BulkPut(request);
                }
                catch (Ds3MaxJobsException)
                {
                    if (retriesLeft == 0)
                    {
                        throw;
                    }

                    retriesLeft--;
                    Thread.Sleep(this._jobWaitTime * 1000 * 60);
                }
            } while (jobResponse == null);

            var helperStrategyInstance = Activator.CreateInstance(helperStrategyType ?? typeof(WriteRandomAccessHelperStrategy));

            return FullObjectJob.Create(
                this._client,
                jobResponse,
                helperStrategyInstance,
                new WriteTransferrer()
            );
        }

        public IJob StartReadJob(string bucket, IEnumerable<Ds3Object> objectsToRead, Type helperStrategyType = null)
        {
            var jobResponse = this._client.BulkGet(
                new BulkGetRequest(bucket, VerifyObjectCount(objectsToRead))
                    .WithChunkOrdering(ChunkOrdering.None)
            );

            var helperStrategyInstance = Activator.CreateInstance(helperStrategyType ?? typeof(ReadRandomAccessHelperStrategy<string>), this._retryAfter);

            return FullObjectJob.Create(
                this._client,
                jobResponse,
                helperStrategyInstance,
                new PartialDataTransferrerDecorator(new ReadTransferrer(), _getObjectRetries)
            );
        }

        public IPartialReadJob StartPartialReadJob(
            string bucket,
            IEnumerable<string> fullObjects,
            IEnumerable<Ds3PartialObject> partialObjects,
            Type helperStrategyType = null
            )
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

            var helperStrategyInstance = Activator.CreateInstance(helperStrategyType ?? typeof(ReadRandomAccessHelperStrategy<Ds3PartialObject>), this._retryAfter);

            return PartialReadJob.Create(
                this._client,
                jobResponse,
                fullObjectList,
                partialObjectList,
                helperStrategyInstance,
                _getObjectRetries
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

        public IJob StartReadAllJob(string bucket, Type helperStrategyType = null)
        {
            return this.StartReadJob(bucket, this.ListObjects(bucket), helperStrategyType);
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

        public IJob RecoverWriteJob(Guid jobId, Type helperStrategyType = null)
        {
            var jobResponse = this._client.ModifyJob(new ModifyJobRequest(jobId));
            if (jobResponse.RequestType != JobTypePut)
            {
                throw new InvalidOperationException(Resources.ExpectedPutJobButWasGetJobException);
            }

            var helperStrategyInstance = Activator.CreateInstance(helperStrategyType ?? typeof(WriteRandomAccessHelperStrategy));

            return FullObjectJob.Create(
                this._client,
                jobResponse,
                helperStrategyInstance,
                new WriteTransferrer()
            );
        }
    }
}