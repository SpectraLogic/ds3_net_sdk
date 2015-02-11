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

namespace Ds3.Calls
{
    public abstract class GetAvailableJobChunksResponse
    {
        private static readonly JobGoneResponse _jobGone = new JobGoneResponse();

        private GetAvailableJobChunksResponse()
        {
            // Prevent non-internal implementations.
        }

        public static GetAvailableJobChunksResponse Success(TimeSpan retryAfter, JobResponse jobResponse)
        {
            return new SuccessResponse(retryAfter, jobResponse);
        }

        public static GetAvailableJobChunksResponse JobGone
        {
            get { return _jobGone; }
        }

        public static GetAvailableJobChunksResponse RetryAfter(TimeSpan retryAfter)
        {
            return new RetryAfterResponse(retryAfter);
        }

        public void Match(Action<TimeSpan, JobResponse> success, Action<TimeSpan> retryAfter)
        {
            this.Match(success, () => { throw new InvalidOperationException(Resources.JobGoneException); }, retryAfter);
        }

        public T Match<T>(Func<TimeSpan, JobResponse, T> success, Func<TimeSpan, T> retryAfter)
        {
            return this.Match(success, () => { throw new InvalidOperationException(Resources.JobGoneException); }, retryAfter);
        }

        public abstract void Match(Action<TimeSpan, JobResponse> success, Action jobGone, Action<TimeSpan> retryAfter);

        public abstract T Match<T>(Func<TimeSpan, JobResponse, T> success, Func<T> jobGone, Func<TimeSpan, T> retryAfter);

        private class SuccessResponse : GetAvailableJobChunksResponse
        {
            private readonly TimeSpan _retryAfter;
            private readonly JobResponse _jobResponse;

            public SuccessResponse(TimeSpan retryAfter, JobResponse jobResponse)
            {
                this._retryAfter = retryAfter;
                this._jobResponse = jobResponse;
            }

            public override void Match(Action<TimeSpan, JobResponse> success, Action jobGone, Action<TimeSpan> retryAfter)
            {
                success(this._retryAfter, this._jobResponse);
            }

            public override T Match<T>(Func<TimeSpan, JobResponse, T> success, Func<T> jobGone, Func<TimeSpan, T> retryAfter)
            {
                return success(this._retryAfter, this._jobResponse);
            }
        }

        private class JobGoneResponse : GetAvailableJobChunksResponse
        {
            public override void Match(Action<TimeSpan, JobResponse> success, Action jobGone, Action<TimeSpan> retryAfter)
            {
                jobGone();
            }

            public override T Match<T>(Func<TimeSpan, JobResponse, T> success, Func<T> jobGone, Func<TimeSpan, T> retryAfter)
            {
                return jobGone();
            }
        }

        private class RetryAfterResponse : GetAvailableJobChunksResponse
        {
            private readonly TimeSpan _retryAfter;

            public RetryAfterResponse(TimeSpan retryAfter)
            {
                this._retryAfter = retryAfter;
            }

            public override void Match(Action<TimeSpan, JobResponse> success, Action jobGone, Action<TimeSpan> retryAfter)
            {
                retryAfter(_retryAfter);
            }

            public override T Match<T>(Func<TimeSpan, JobResponse, T> success, Func<T> jobGone, Func<TimeSpan, T> retryAfter)
            {
                return retryAfter(_retryAfter);
            }
        }
    }
}
