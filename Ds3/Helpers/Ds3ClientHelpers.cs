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
        private readonly IDs3Client _client;
        private const JobRequestType JobTypePut = JobRequestType.PUT;
        private const JobRequestType JobTypeGet = JobRequestType.GET;
        private readonly int _retryAfter; //-1 represent infinite number
        private readonly int _objectTransferAttemps; // -1 represents infinite number of retries
        private readonly int _jobRetries;
        private readonly int _jobWaitTime; //in minutes
        private readonly long? _maximumFileSizeForAggregating;

        public Ds3ClientHelpers(IDs3Client client, int retryAfter = -1, int objectTransferAttemps = 5, int jobRetries = -1,
            int jobWaitTime = 5, long? maximumFileSizeForAggregating = null)
        {
            this._client = client;
            this._retryAfter = retryAfter;
            this._objectTransferAttemps = objectTransferAttemps;
            this._jobRetries = jobRetries;
            this._jobWaitTime = jobWaitTime;
            this._maximumFileSizeForAggregating = maximumFileSizeForAggregating;
        }

        public IJob StartWriteJob(string bucket, IEnumerable<Ds3Object> objectsToWrite, long? maxBlobSize = null,
            IHelperStrategy<string> helperStrategy = null)
        {
            var withAggregation = false;
            var request = new PutBulkJobSpectraS3Request(
                bucket,
                VerifyObjectCount(objectsToWrite)
                );

            if (maxBlobSize.HasValue)
            {
                request.WithMaxUploadSize(maxBlobSize.Value);
            }

            if (_maximumFileSizeForAggregating.HasValue &&
                GetJobSize(objectsToWrite) <= _maximumFileSizeForAggregating.Value)
            {
                withAggregation = true;
                request.Aggregating = true;
            }

            PutBulkJobSpectraS3Response jobResponse = null;
            var retriesLeft = this._jobRetries;
            do
            {
                try
                {
                    jobResponse = this._client.PutBulkJobSpectraS3(request);
                }
                catch (Ds3MaxJobsException)
                {
                    if (retriesLeft == 0)
                    {
                        throw;
                    }

                    retriesLeft--;
                    Thread.Sleep(this._jobWaitTime*1000*60);
                }
            } while (jobResponse == null);

            if (helperStrategy == null)
            {
                helperStrategy = new WriteRandomAccessHelperStrategy(this._retryAfter, withAggregation);
            }

            return FullObjectJob.Create(
                this._client,
                jobResponse.ResponsePayload,
                helperStrategy,
                new WriteTransferrer(),
                _objectTransferAttemps
                );
        }

        private static long GetJobSize(IEnumerable<Ds3Object> objectsToWrite)
        {
            return
                objectsToWrite.Where(objectToWrite => objectToWrite.Size != null)
                    .Sum(objectToWrite => objectToWrite.Size.Value);
        }

        public IJob StartReadJob(string bucket, IEnumerable<Ds3Object> objectsToRead,
            IHelperStrategy<string> helperStrategy = null)
        {
            var jobResponse = this._client.GetBulkJobSpectraS3(
                new GetBulkJobSpectraS3Request(bucket, VerifyObjectCount(objectsToRead))
                    .WithChunkClientProcessingOrderGuarantee(JobChunkClientProcessingOrderGuarantee.NONE)
                );

            if (helperStrategy == null)
            {
                helperStrategy = new ReadRandomAccessHelperStrategy<string>(this._retryAfter);
            }

            return FullObjectJob.Create(
                this._client,
                jobResponse.ResponsePayload,
                helperStrategy,
                new PartialDataTransferrerDecorator(new ReadTransferrer(), _objectTransferAttemps)
                );
        }

        public IPartialReadJob StartPartialReadJob(
            string bucket,
            IEnumerable<string> fullObjects,
            IEnumerable<Ds3PartialObject> partialObjects,
            IHelperStrategy<Ds3PartialObject> helperStrategy = null
            )
        {
            var partialObjectList = new SortedSet<Ds3PartialObject>(partialObjects);
            var fullObjectList = fullObjects.ToList();
            if (partialObjectList.Count + fullObjectList.Count == 0)
            {
                throw new InvalidOperationException(Resources.NoObjectsToTransferException);
            }
            var jobResponse = this._client.GetBulkJobSpectraS3(
                new GetBulkJobSpectraS3Request(bucket, fullObjectList, partialObjectList)
                    .WithChunkClientProcessingOrderGuarantee(JobChunkClientProcessingOrderGuarantee.NONE)
                );

            if (helperStrategy == null)
            {
                helperStrategy = new ReadRandomAccessHelperStrategy<Ds3PartialObject>(this._retryAfter);
            }

            return PartialReadJob.Create(
                this._client,
                jobResponse.ResponsePayload,
                fullObjectList,
                partialObjectList,
                helperStrategy,
                _objectTransferAttemps
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

        public IJob StartReadAllJob(string bucket, IHelperStrategy<string> helperStrategy = null)
        {
            return this.StartReadJob(bucket, this.ListObjects(bucket), helperStrategy);
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
                var request = new GetBucketRequest(bucketName)
                {
                    Marker = marker,
                    Prefix = keyPrefix
                };
                var response = _client.GetBucket(request).ResponsePayload;
                isTruncated = response.Truncated;
                marker = response.NextMarker;
                var responseObjects = response.Objects.ToList() as IList<Contents> ?? response.Objects.ToList();
                foreach (var ds3Object in responseObjects)
                {
                    yield return new Ds3Object(ds3Object.Key, ds3Object.Size);
                }
            } while (isTruncated);
        }

        public static readonly Func<Ds3Object, bool> FolderFilterPredicate = obj => !obj.Name.EndsWith("/");
        public static readonly Func<Ds3Object, bool> ZeroLengthFilterPredicate = obj => obj.Size != 0;

        public static IEnumerable<Ds3Object> FilterDs3Objects(IEnumerable<Ds3Object> objects,
            params Func<Ds3Object, bool>[] predicates)
        {
            if (predicates == null) return objects;

            var result = objects;
            for (var i = 0; i < predicates.Length; i++)
            {
                var predicate = predicates[i];
                if (predicate == null) continue;
                result = result.Where(obj => predicate.Invoke(obj));
            }
            return result;
        }

        public void EnsureBucketExists(string bucketName)
        {
            var headResponse = _client.HeadBucket(new HeadBucketRequest(bucketName));
            if (headResponse.Status == HeadBucketResponse.StatusType.DoesntExist)
            {
                _client.PutBucket(new PutBucketRequest(bucketName));
            }
        }

        public IJob RecoverWriteJob(Guid jobId, IHelperStrategy<string> helperStrategy = null)
        {
            var jobResponse = this._client.ModifyJobSpectraS3(new ModifyJobSpectraS3Request(jobId)).ResponsePayload;

            if (jobResponse.Status == JobStatus.COMPLETED)
            {
                throw new InvalidOperationException(Resources.JobCompletedException);
            }

            if (jobResponse.RequestType != JobTypePut)
            {
                throw new InvalidOperationException(Resources.ExpectedPutJobButWasGetJobException);
            }

            if (helperStrategy == null)
            {
                helperStrategy = new WriteRandomAccessHelperStrategy(this._retryAfter, false);
            }

            return FullObjectJob.Create(
                this._client,
                jobResponse,
                helperStrategy,
                new WriteTransferrer()
                );
        }

        public IJob RecoverReadJob(Guid jobId, IHelperStrategy<string> helperStrategy = null)
        {
            var jobResponse = this._client.ModifyJobSpectraS3(new ModifyJobSpectraS3Request(jobId)).ResponsePayload;

            if (jobResponse.Status == JobStatus.COMPLETED)
            {
                throw new InvalidOperationException(Resources.JobCompletedException);
            }

            if (jobResponse.RequestType != JobTypeGet)
            {
                throw new InvalidOperationException(Resources.ExpectedGetJobButWasPutJobException);
            }

            if (helperStrategy == null)
            {
                helperStrategy = new ReadRandomAccessHelperStrategy<string>(this._retryAfter);
            }

            return FullObjectJob.Create(
                this._client,
                jobResponse,
                helperStrategy,
                new ReadTransferrer()
                );
        }
    }
}