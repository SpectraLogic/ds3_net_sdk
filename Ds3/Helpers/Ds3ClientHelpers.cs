/*
 * ******************************************************************************
 *   Copyright 2014-2017 Spectra Logic Corporation. All Rights Reserved.
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
using Ds3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Ds3.Helpers.Strategies;
using Ds3.Helpers.TransferStrategies;
using Ds3.Runtime;

namespace Ds3.Helpers
{
    public class Ds3ClientHelpers : IDs3ClientHelpers
    {
        private readonly IDs3Client _client;
        private const JobRequestType JobTypePut = JobRequestType.PUT;
        private const JobRequestType JobTypeGet = JobRequestType.GET;
        private readonly int _retryAfter; //Negative represent infinite number
        private readonly int _objectTransferAttempts; //Negative number represents infinite number of retries
        private readonly int _jobRetries;
        private readonly int _jobWaitTime; //in minutes

        public Ds3ClientHelpers(IDs3Client client, int retryAfter = -1, int objectTransferAttempts = 5, int jobRetries = -1,
            int jobWaitTime = 5)
        {
            _client = client;
            _retryAfter = retryAfter;
            _objectTransferAttempts = objectTransferAttempts;
            _jobRetries = jobRetries;
            _jobWaitTime = jobWaitTime;
        }

        public IJob StartWriteJob(string bucket, IEnumerable<Ds3Object> objectsToWrite, Ds3WriteJobOptions ds3WriteJobOptions  = null, IHelperStrategy < string> helperStrategy = null)
        {
            ds3WriteJobOptions = ds3WriteJobOptions ?? new Ds3WriteJobOptions();

            if (helperStrategy != null && helperStrategy is WriteAggregateJobsHelperStrategy)
            {
                //make sure that aggregating is marked as true on the request
                ds3WriteJobOptions.Aggregating = true;
            }

            var request = new PutBulkJobSpectraS3Request(bucket, VerifyObjectCount(objectsToWrite));

            UpdateWriteJobRequest(ds3WriteJobOptions, request);

            PutBulkJobSpectraS3Response jobResponse = null;
            var retriesLeft = _jobRetries;
            do
            {
                try
                {
                    jobResponse = _client.PutBulkJobSpectraS3(request);
                }
                catch (Ds3MaxJobsException)
                {
                    if (retriesLeft == 0)
                    {
                        throw;
                    }

                    retriesLeft--;
                    Thread.Sleep(_jobWaitTime * 1000 * 60);
                }
            } while (jobResponse == null);

            if (helperStrategy == null)
            {
                helperStrategy = new WriteRandomAccessHelperStrategy(_retryAfter);
            }

            return FullObjectJob.Create(
                _client,
                jobResponse.ResponsePayload,
                helperStrategy,
                new WriteTransferStrategy(),
                _objectTransferAttempts
                );
        }

        private static void UpdateWriteJobRequest(Ds3WriteJobOptions ds3WriteJobOptions, PutBulkJobSpectraS3Request request)
        {
            request.Priority = ds3WriteJobOptions.Priority;
            request.Aggregating = ds3WriteJobOptions.Aggregating;
            request.Force = ds3WriteJobOptions.Force;
            request.IgnoreNamingConflicts = ds3WriteJobOptions.IgnoreNamingConflicts;
            request.Name = ds3WriteJobOptions.Name;
            request.MinimizeSpanningAcrossMedia = ds3WriteJobOptions.MinimizeSpanningAcrossMedia;
            if (ds3WriteJobOptions.MaxUploadSize.HasValue)
            {
                request.WithMaxUploadSize(ds3WriteJobOptions.MaxUploadSize.Value);
            }
        }

        private static long GetJobSize(IEnumerable<Ds3Object> objectsToWrite)
        {
            return
                objectsToWrite.Where(objectToWrite => objectToWrite.Size != null)
                    .Sum(objectToWrite => objectToWrite.Size.Value);
        }

        public IJob StartReadJob(string bucket, IEnumerable<Ds3Object> objectsToRead, Ds3ReadJobOptions ds3ReadJobOptions = null,
            IHelperStrategy<string> helperStrategy = null)
        {
            ds3ReadJobOptions = ds3ReadJobOptions ?? new Ds3ReadJobOptions();

            if (helperStrategy == null)
            {
                helperStrategy = new ReadRandomAccessHelperStrategy<string>(_retryAfter);
            }

            var request = new GetBulkJobSpectraS3Request(bucket, VerifyObjectCount(objectsToRead));

            UpdateReadJobRequest(helperStrategy.GetType(), ds3ReadJobOptions, request);

            var jobResponse = _client.GetBulkJobSpectraS3(request);

            return FullObjectJob.Create(
                _client,
                jobResponse.ResponsePayload,
                helperStrategy,
                new PartialDataTransferStrategyDecorator(new ReadTransferStrategy(), _objectTransferAttempts)
                );
        }

        public IPartialReadJob StartPartialReadJob(
            string bucket,
            IEnumerable<string> fullObjects,
            IEnumerable<Ds3PartialObject> partialObjects,
            Ds3ReadJobOptions ds3ReadJobOptions = null,
            IHelperStrategy<Ds3PartialObject> helperStrategy = null)
        {
            ds3ReadJobOptions = ds3ReadJobOptions ?? new Ds3ReadJobOptions();

            var partialObjectList = new SortedSet<Ds3PartialObject>(partialObjects);
            var fullObjectList = fullObjects.ToList();
            if (partialObjectList.Count + fullObjectList.Count == 0)
            {
                throw new InvalidOperationException(Resources.NoObjectsToTransferException);
            }

            if (helperStrategy == null)
            {
                helperStrategy = new ReadRandomAccessHelperStrategy<Ds3PartialObject>(_retryAfter);
            }

            var request = new GetBulkJobSpectraS3Request(bucket, fullObjectList, partialObjectList);

            UpdateReadJobRequest(helperStrategy.GetType(), ds3ReadJobOptions, request);

            var jobResponse = _client.GetBulkJobSpectraS3(request);

            return PartialReadJob.Create(
                _client,
                jobResponse.ResponsePayload,
                fullObjectList,
                partialObjectList,
                helperStrategy,
                _objectTransferAttempts
                );
        }

        private static void UpdateReadJobRequest(Type helperStrategyType, Ds3ReadJobOptions ds3ReadJobOptions, GetBulkJobSpectraS3Request request)
        {

            request.Aggregating = ds3ReadJobOptions.Aggregating;
            request.Name = ds3ReadJobOptions.Name;
            request.Priority = ds3ReadJobOptions.Priority;
            if (ds3ReadJobOptions.ChunkClientProcessingOrderGuarantee.HasValue)
            {
                request.WithChunkClientProcessingOrderGuarantee(
                    ds3ReadJobOptions.ChunkClientProcessingOrderGuarantee.Value);
            }

            //When using ReadStreamHelperStrategy the JobChunkClientProcessingOrderGuarantee must be IN_ORDER
            if (helperStrategyType == typeof(ReadStreamHelperStrategy) &&
                request.ChunkClientProcessingOrderGuarantee != JobChunkClientProcessingOrderGuarantee.IN_ORDER)
            {
                request.WithChunkClientProcessingOrderGuarantee(JobChunkClientProcessingOrderGuarantee.IN_ORDER);
            }
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

        public IJob StartReadAllJob(string bucket, Ds3ReadJobOptions ds3ReadJobOptions = null, IHelperStrategy < string> helperStrategy = null)
        {
            return StartReadJob(bucket, ListObjects(bucket), ds3ReadJobOptions, helperStrategy);
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
            var jobResponse = _client.ModifyJobSpectraS3(new ModifyJobSpectraS3Request(jobId)).ResponsePayload;

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
                helperStrategy = new WriteRandomAccessHelperStrategy(_retryAfter);
            }
            else if (helperStrategy is WriteAggregateJobsHelperStrategy)
            {
                throw new InvalidOperationException(Resources.UseRecoverAggregatedWriteJob);
            }

            return FullObjectJob.Create(
                _client,
                jobResponse,
                helperStrategy,
                new WriteTransferStrategy()
                );
        }

        public IJob RecoverAggregatedWriteJob(Guid jobId, IEnumerable<Ds3Object> objectsToRecover)
        {
            var jobResponse = _client.ModifyJobSpectraS3(new ModifyJobSpectraS3Request(jobId)).ResponsePayload;

            if (jobResponse.Status == JobStatus.COMPLETED)
            {
                throw new InvalidOperationException(Resources.JobCompletedException);
            }

            if (jobResponse.RequestType != JobTypePut)
            {
                throw new InvalidOperationException(Resources.ExpectedPutJobButWasGetJobException);
            }

            return FullObjectJob.Create(
                _client,
                jobResponse,
                new WriteAggregateJobsHelperStrategy(objectsToRecover, _retryAfter),
                new WriteTransferStrategy()
                );
        }

        public IJob RecoverReadJob(Guid jobId, IHelperStrategy<string> helperStrategy = null)
        {
            var jobResponse = _client.ModifyJobSpectraS3(new ModifyJobSpectraS3Request(jobId)).ResponsePayload;

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
                helperStrategy = new ReadRandomAccessHelperStrategy<string>(_retryAfter);
            }

            return FullObjectJob.Create(
                _client,
                jobResponse,
                helperStrategy,
                new ReadTransferStrategy()
                );
        }
    }
}