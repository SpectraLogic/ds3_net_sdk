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

using Ds3.Models;

namespace Ds3.Calls
{
    public abstract class AllocateJobChunkResponse
    {
        private static readonly AllocateJobChunkResponse _chunkGone = new ChunkGoneResponse();

        private AllocateJobChunkResponse()
        {
            // Prevent non-internal implementations.
        }

        public static AllocateJobChunkResponse Success(JobObjectList jobObjectList)
        {
            return new SuccessChunkResponse(jobObjectList);
        }

        public static AllocateJobChunkResponse RetryAfter(TimeSpan retryAfter)
        {
            return new RetryAfterResponse(retryAfter);
        }

        public static AllocateJobChunkResponse ChunkGone
        {
            get { return _chunkGone; }
        }

        public void Match(Action<JobObjectList> success, Action<TimeSpan> retryAfter)
        {
            Match(success, retryAfter, () => { throw new InvalidOperationException(Resources.JobGoneException); });
        }

        public T Match<T>(Func<JobObjectList, T> success, Func<TimeSpan, T> retryAfter)
        {
            return Match(success, retryAfter, () => { throw new InvalidOperationException(Resources.JobGoneException); });
        }

        public abstract void Match(Action<JobObjectList> success, Action<TimeSpan> retryAfter, Action chunkGone);

        public abstract T Match<T>(Func<JobObjectList, T> success, Func<TimeSpan, T> retryAfter, Func<T> chunkGone);

        private class SuccessChunkResponse : AllocateJobChunkResponse
        {
            private readonly JobObjectList _jobObjectList;

            public SuccessChunkResponse(JobObjectList jobObjectList)
            {
                this._jobObjectList = jobObjectList;
            }

            public override void Match(Action<JobObjectList> success, Action<TimeSpan> retryAfter, Action chunkGone)
            {
                success(_jobObjectList);
            }

            public override T Match<T>(Func<JobObjectList, T> success, Func<TimeSpan, T> retryAfter, Func<T> chunkGone)
            {
                return success(_jobObjectList);
            }
        }

        private class RetryAfterResponse : AllocateJobChunkResponse
        {
            private readonly TimeSpan _retryAfter;

            public RetryAfterResponse(TimeSpan retryAfter)
            {
                this._retryAfter = retryAfter;
            }

            public override void Match(Action<JobObjectList> success, Action<TimeSpan> retryAfter, Action chunkGone)
            {
                retryAfter(_retryAfter);
            }

            public override T Match<T>(Func<JobObjectList, T> success, Func<TimeSpan, T> retryAfter, Func<T> chunkGone)
            {
                return retryAfter(_retryAfter);
            }
        }

        private class ChunkGoneResponse : AllocateJobChunkResponse
        {
            public override void Match(Action<JobObjectList> success, Action<TimeSpan> retryAfter, Action chunkGone)
            {
                chunkGone();
            }

            public override T Match<T>(Func<JobObjectList, T> success, Func<TimeSpan, T> retryAfter, Func<T> chunkGone)
            {
                return chunkGone();
            }
        }
    }
}
