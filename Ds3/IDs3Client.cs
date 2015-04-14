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

using System.Collections.Generic;

using Ds3.Calls;
using Ds3.Models;

namespace Ds3
{
    /// <summary>
    /// The main DS3 API interface. Use Ds3Builder to instantiate.
    /// </summary>
    public interface IDs3Client
    {
        /// <summary>
        /// Retrieves the list of buckets currently on the DS3 server given the client's credentials.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetServiceResponse GetService(GetServiceRequest request);

        /// <summary>
        /// Retrieves the list of objects in the specified bucket. Note that
        /// the server may choose to limit the number of objects specified in its
        /// reply, so you may have to call this multiple times using the
        /// request.WithMarker() method.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetBucketResponse GetBucket(GetBucketRequest request);

        /// <summary>
        /// Retrieves a Stream of the contents of the specified object.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetObjectResponse GetObject(GetObjectRequest request);

        /// <summary>
        /// Retrieves information about an object without obtaining its contents.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        HeadObjectResponse HeadObject(HeadObjectRequest request);

        /// <summary>
        /// Submits the contents of a Stream to a specified object.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        void PutObject(PutObjectRequest request);

        /// <summary>
        /// Deletes the specified object.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        void DeleteObject(DeleteObjectRequest request);

        /// <summary>
        /// Deletes the specified list of objects.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        DeleteObjectListResponse DeleteObjectList(DeleteObjectListRequest request);

        /// <summary>
        /// Deletes the specified bucket.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        void DeleteBucket(DeleteBucketRequest request);

        /// <summary>
        /// Creates the specified bucket.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        void PutBucket(PutBucketRequest request);

        /// <summary>
        /// Performs a HTTP HEAD for a bucket. The HEAD will return information about if
        /// the bucket exists, or if the user has access to that bucket.
        /// </summary>
        /// <param name="request"></param>
        /// <returns>The response containing the status of the bucket.</returns>
        HeadBucketResponse HeadBucket(HeadBucketRequest request);

        /// <summary>
        /// Primes a DS3 bulk get for better performance.
        /// Note that this request requires that each Ds3Object have both the
        /// name and the size set. Subsequent GetObject operations should be
        /// performed in the order specified by the BulkGetResponse.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        JobResponse BulkGet(BulkGetRequest request);

        /// <summary>
        /// Primes a DS3 bulk put for better performance.
        /// Note that this request requires that each Ds3Object have both the
        /// name and the size set. Subsequent PutObject operations should be
        /// performed in the order specified by the BulkPutResponse.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        JobResponse BulkPut(BulkPutRequest request);

        /// <summary>
        /// Retrieves the list of available jobs.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetJobListResponse GetJobList(GetJobListRequest request);

        /// <summary>
        /// Returns the same information as GetJob, but also updates the last
        /// access time to keep the job from expiring.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        JobResponse ModifyJob(ModifyJobRequest request);

        /// <summary>
        /// Deletes an in-progress job.
        /// </summary>
        /// <param name="request"></param>
        void DeleteJob(DeleteJobRequest request);

        /// <summary>
        /// Retrieves information about a job so it can be resumed.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        JobResponse GetJob(GetJobRequest request);

        /// <summary>
        /// Tries to ensure that the server can accept PUT reqeusts for
        /// a particular chunk of data within a job. If the server does
        /// not have space to accept the data it will return a retry-after
        /// response.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        AllocateJobChunkResponse AllocateJobChunk(AllocateJobChunkRequest request);

        /// <summary>
        /// Returns the chunks of data that the client can GET from the
        /// server. If the job is still active but the server doesn't
        /// have data ready yet, then this will return a retry-after response.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetAvailableJobChunksResponse GetAvailableJobChunks(GetAvailableJobChunksRequest request);

        /// <summary>
        /// Returns the set of physical media containing the provided set of objects.
        /// In full details mode, lists tapes used to store each offset and length of
        /// each of the provided objects.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        GetPhysicalPlacementResponse GetPhysicalPlacement(GetPhysicalPlacementRequest request);

        /// <summary>
        /// For multi-node support (planned), this provides a means of creating
        /// a client that connects to the specified node id.
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        IDs3ClientFactory BuildFactory(IEnumerable<Node> nodes);
    }
}
