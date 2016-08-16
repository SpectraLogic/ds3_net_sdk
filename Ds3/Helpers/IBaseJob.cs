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
using System.IO;
using System.Threading;

namespace Ds3.Helpers
{
    public interface IBaseJob<TSelf, TItem> where TSelf : IBaseJob<TSelf, TItem>
    {
        /// <summary>
        /// The id that allows the client to track job status and recover
        /// or delete jobs in the case of a failure during transfer.
        /// </summary>
        Guid JobId { get; }

        /// <summary>
        /// The name of the bucket that this job is transferring to.
        /// </summary>
        string BucketName { get; }

        /// <summary>
        /// Must always be called before the Transfer method.
        ///
        /// Specifies The maximum number of simultaneous transfers
        /// to or from the server for this particular job.
        /// </summary>
        /// <param name="maxParallelRequests"></param>
        /// <returns>This IJob instance.</returns>
        TSelf WithMaxParallelRequests(int maxParallelRequests);

        /// <summary>
        /// Must always be called before the Transfer method.
        ///
        /// Allows the client to stop transferring to a job using a
        /// CancellationTokenSource. Note that this does not cancel
        /// a job, and the job can be resumed later. If you'd like
        /// to cancel a job, use IDs3Client.DeleteJob().
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>This IJob instance.</returns>
        TSelf WithCancellationToken(CancellationToken cancellationToken);

        /// <summary>
        /// Must always be called before the Transfer method.
        /// 
        /// Allows the client to add meta-data to objects.
        /// </summary>
        /// <param name="metadataAccess"></param>
        /// <returns>This IJob instance.</returns>
        TSelf WithMetadata(IMetadataAccess metadataAccess);

        /// <summary>
        /// Performs all GETs or PUTs for the job (depending on the type
        /// of job).
        ///
        /// This method uses DS3 cache handling requests to efficiently
        /// transfer objects and handles multiplexing single object streams
        /// when the DS3 job response splits individual objects into multiple
        /// requests. It also performs requests in parallel for situations
        /// where doing so can improve performance.
        /// </summary>
        /// <seealso cref="FileHelpers.BuildFileGetter(string)"/>
        /// <seealso cref="FileHelpers.BuildFilePutter(string, string)"/>
        /// <param name="createStreamForTransferItem">Opens a stream for a given transfer item.</param>
        void Transfer(Func<TItem, Stream> createStreamForTransferItem);

        /// <summary>
        /// Fires handlers with the amount of additional data that's been transferred
        /// when a part of a job is transferred.
        /// </summary>
        event Action<long> DataTransferred;

        /// <summary>
        /// Fires handlers with the name of each transferred item as their transfers finish.
        /// </summary>
        event Action<TItem> ItemCompleted;

        /// <summary>
        /// Fires handlers with an object name and the meta-data associated with it.
        /// </summary>
        event Action<string, IDictionary<string, string>> MetadataListener;

        /// <summary>
        /// Fires handlers with an object name, offset and the exception thrown
        /// </summary>
        event Action<string, long, Exception> OnFailure;
    }
}
