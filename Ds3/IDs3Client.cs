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
        /// Submits the contents of a Stream to a specified object.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        PutObjectResponse PutObject(PutObjectRequest request);

        /// <summary>
        /// Deletes the specified object.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        DeleteObjectResponse DeleteObject(DeleteObjectRequest request);

        /// <summary>
        /// Deletes the specified bucket.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        DeleteBucketResponse DeleteBucket(DeleteBucketRequest request);

        /// <summary>
        /// Creates the specified bucket.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        PutBucketResponse PutBucket(PutBucketRequest request);

        /// <summary>
        /// Primes a DS3 bulk get for better performance.
        /// Note that this request requires that each Ds3Object have both the
        /// name and the size set. Subsequent GetObject operations should be
        /// performed in the order specified by the BulkGetResponse.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        BulkGetResponse BulkGet(BulkGetRequest request);

        /// <summary>
        /// Primes a DS3 bulk put for better performance.
        /// Note that this request requires that each Ds3Object have both the
        /// name and the size set. Subsequent PutObject operations should be
        /// performed in the order specified by the BulkPutResponse.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        BulkPutResponse BulkPut(BulkPutRequest request);
    }
}
