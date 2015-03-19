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

using Ds3.Models;

namespace Ds3.Helpers
{
    public interface IDs3ClientHelpers
    {
        /// <summary>
        /// Runs a DS3 bulk PUT request with a set of objects and returns an
        /// interface that can PUT individual objects efficiently to the server.
        /// </summary>
        /// <seealso cref="FileHelpers.ListObjectsForDirectory"/>
        /// <param name="bucket">The name of the bucket to put the objects to.</param>
        /// <param name="objectsToWrite">The object names and sizes to put.</param>
        /// <param name="maxBlobSize">The (optional) maximum size for the parts of the objects to transmit.</param>
        /// <returns>An IJob implementation that can put each object per the DS3 protocol.</returns>
        IJob StartWriteJob(string bucket, IEnumerable<Ds3Object> objectsToWrite, long? maxBlobSize = null);

        /// <summary>
        /// Runs a DS3 bulk GET request with a set of objects and returns an
        /// interface that can GET individual objects efficiently from the server.
        /// </summary>
        /// <param name="bucket">The name of the bucket to get the objects from.</param>
        /// <param name="objectsToRead">The object names to get.</param>
        /// <returns>An IJob implementation that can get each object per the DS3 protocol.</returns>
        IJob StartReadJob(string bucket, IEnumerable<Ds3Object> objectsToRead);

        /// <summary>
        /// Runs a DS3 bulk GET request for all of the objects in a bucket.
        /// </summary>
        /// <param name="bucket">The name of the bucket to get the objects from.</param>
        /// <returns>An IJob implementation that can get each object per the DS3 protocol.</returns>
        IJob StartReadAllJob(string bucket);

        /// <summary>
        /// Runs a DS3 bulk GET request with a set of partial object transfers and
        /// returns an interface that can GET individual object parts efficiently
        /// from the server.
        /// 
        /// Note that you can get multiple ranges within the same object at the same
        /// time, but those ranges must be non-overlapping.
        /// </summary>
        /// <param name="bucket">The name of the bucket to get the objects from.</param>
        /// <param name="fullObjects">The list of full objects to get.</param>
        /// <param name="partialObjects">The object parts to get.</param>
        /// <returns>The IPartialReadJob implementation that can get each partial object per the DS3 protocol.</returns>
        IPartialReadJob StartPartialReadJob(
            string bucket,
            IEnumerable<string> fullObjects,
            IEnumerable<Ds3PartialObject> partialObjects);

        /// <summary>
        /// Returns information about all of the objects in a bucket.
        ///
        /// Note that this method requests 1,000 objects at a time as they are consumed.
        /// Thus, if a bucket contains 2,500 objects and you call
        /// helpers.ListObjects("bucket").Take(1500).ToList()
        /// then the client will issue exactly two requests.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <returns></returns>
        IEnumerable<Ds3Object> ListObjects(string bucketName);

        /// <summary>
        /// Returns information about all of the objects in a bucket
        /// whose names start with a given prefix.
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="keyPrefix"></param>
        /// <returns></returns>
        IEnumerable<Ds3Object> ListObjects(string bucketName, string keyPrefix);

        /// <summary>
        /// Creates a bucket if it does not exist.
        /// </summary>
        /// <param name="bucketName"></param>
        void EnsureBucketExists(string bucketName);

        /// <summary>
        /// Determines the state of an existing bulk PUT job and returns
        /// an interface that can PUT the remaining objects efficiently.
        /// </summary>
        /// <param name="jobId"></param>
        /// <returns>An IJob implementation that can put each object per the DS3 protocol.</returns>
        IJob RecoverWriteJob(Guid jobId);
    }
}
