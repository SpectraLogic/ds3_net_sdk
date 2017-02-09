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

// This code is auto-generated, do not modify

using System;
using Ds3.Models;

namespace Ds3.Calls
{
    public abstract class AllocateJobChunkSpectraS3Response
    {
        private static readonly AllocateJobChunkSpectraS3Response _chunkGone = new ChunkGoneResponse();

        private AllocateJobChunkSpectraS3Response()
        {
            // Prevent non-internal implementations.
        }

        /// <summary>
        /// Creates a response object specifying the object list that was successfully (or already) allocated.
        /// </summary>
        /// <param name="jobObjectList">The job objects that were allocated.</param>
        /// <returns>The response instance.</returns>
        public static AllocateJobChunkSpectraS3Response Success(Objects jobObjectList)
        {
            return new SuccessChunkResponse(jobObjectList);
        }

        /// <summary>
        /// Creates a response object specifying that the client should retry the request.
        /// </summary>
        /// <param name="retryAfter">The amount of time that the client should wait before retrying.</param>
        /// <returns>The response instance.</returns>
        public static AllocateJobChunkSpectraS3Response RetryAfter(TimeSpan retryAfter)
        {
            return new RetryAfterResponse(retryAfter);
        }

        /// <summary>
        /// Creates a response object specifying that the chunk no longer exists.
        /// </summary>
        public static AllocateJobChunkSpectraS3Response ChunkGone
        {
            get { return _chunkGone; }
        }

        public void Match(Action<Objects> success, Action<TimeSpan> retryAfter)
        {
            Match(success, retryAfter, () => { throw new InvalidOperationException(Resources.JobGoneException); });
        }

        public T Match<T>(Func<Objects, T> success, Func<TimeSpan, T> retryAfter)
        {
            return Match(success, retryAfter, () => { throw new InvalidOperationException(Resources.JobGoneException); });
        }

        /// <summary>
        /// Calls success, retryAfter, or chunkGone depending on which type of response this actually is.
        /// </summary>
        /// <param name="success">The function to call if this is a "success" instance.</param>
        /// <param name="retryAfter">The function to call if this is a "retryAfter" instance.</param>
        /// <param name="chunkGone">The function to call if this is a "chunkGone" instance.</param>
        public abstract void Match(Action<Objects> success, Action<TimeSpan> retryAfter, Action chunkGone);

        /// <summary>
        /// Calls success, retryAfter, or chunkGone depending on which type of response this actually is.
        /// </summary>
        /// <param name="success">The function to call if this is a "success" instance.</param>
        /// <param name="retryAfter">The function to call if this is a "retryAfter" instance.</param>
        /// <param name="chunkGone">The function to call if this is a "chunkGone" instance.</param>
        /// <returns>What either success, retryAfter, or chunkGone return.</returns>
        public abstract T Match<T>(Func<Objects, T> success, Func<TimeSpan, T> retryAfter, Func<T> chunkGone);

        private class SuccessChunkResponse : AllocateJobChunkSpectraS3Response
        {
            private readonly Objects _jobObjectList;

            public SuccessChunkResponse(Objects jobObjectList)
            {
                this._jobObjectList = jobObjectList;
            }

            public override void Match(Action<Objects> success, Action<TimeSpan> retryAfter, Action chunkGone)
            {
                success(_jobObjectList);
            }

            public override T Match<T>(Func<Objects, T> success, Func<TimeSpan, T> retryAfter, Func<T> chunkGone)
            {
                return success(_jobObjectList);
            }
        }

        private class RetryAfterResponse : AllocateJobChunkSpectraS3Response
        {
            private readonly TimeSpan _retryAfter;

            public RetryAfterResponse(TimeSpan retryAfter)
            {
                this._retryAfter = retryAfter;
            }

            public override void Match(Action<Objects> success, Action<TimeSpan> retryAfter, Action chunkGone)
            {
                retryAfter(_retryAfter);
            }

            public override T Match<T>(Func<Objects, T> success, Func<TimeSpan, T> retryAfter, Func<T> chunkGone)
            {
                return retryAfter(_retryAfter);
            }
        }

        private class ChunkGoneResponse : AllocateJobChunkSpectraS3Response
        {
            public override void Match(Action<Objects> success, Action<TimeSpan> retryAfter, Action chunkGone)
            {
                chunkGone();
            }

            public override T Match<T>(Func<Objects, T> success, Func<TimeSpan, T> retryAfter, Func<T> chunkGone)
            {
                return chunkGone();
            }
        }
    }
}