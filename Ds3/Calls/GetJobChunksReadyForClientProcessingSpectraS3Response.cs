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
    public abstract class GetJobChunksReadyForClientProcessingSpectraS3Response
    {
        private static readonly JobGoneResponse _jobGone = new JobGoneResponse();

        private GetJobChunksReadyForClientProcessingSpectraS3Response()
        {
            // Prevent non-internal implementations.
        }

        /// <summary>
        /// Creates a response object stating that the job has chunks available.
        /// </summary>
        /// <param name="retryAfter">The number of seconds to wait before performing the REST call again if the client already knows about all of the chunks provided.</param>
        /// <param name="jobResponse">A job response with only the chunks that are available for the client.</param>
        /// <returns>The new response instance.</returns>
        public static GetJobChunksReadyForClientProcessingSpectraS3Response Success(TimeSpan retryAfter, MasterObjectList jobResponse)
        {
            return new SuccessResponse(retryAfter, jobResponse);
        }

        /// <summary>
        /// Creates a response object stating that the job does not exist.
        /// </summary>
        public static GetJobChunksReadyForClientProcessingSpectraS3Response JobGone
        {
            get { return _jobGone; }
        }

        /// <summary>
        /// Creates a response object stating that there are no chunks available to process.
        /// </summary>
        /// <param name="retryAfter">The amount of time to wait before asking again.</param>
        /// <returns>The new response instance.</returns>
        public static GetJobChunksReadyForClientProcessingSpectraS3Response RetryAfter(TimeSpan retryAfter)
        {
            return new RetryAfterResponse(retryAfter);
        }

        /// <summary>
        /// Convenience overload if the client wants to throw a job gone exception if the job is gone.
        /// </summary>
        /// <param name="success"></param>
        /// <param name="retryAfter"></param>
        /// <returns></returns>
        public void Match(Action<TimeSpan, MasterObjectList> success, Action<TimeSpan> retryAfter)
        {
            this.Match(success, () => { throw new InvalidOperationException(Resources.JobGoneException); }, retryAfter);
        }

        /// <summary>
        /// Convenience overload if the client wants to throw a job gone exception if the job is gone.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="success"></param>
        /// <param name="retryAfter"></param>
        /// <returns></returns>
        public T Match<T>(Func<TimeSpan, MasterObjectList, T> success, Func<TimeSpan, T> retryAfter)
        {
            return this.Match(success, () => { throw new InvalidOperationException(Resources.JobGoneException); }, retryAfter);
        }

        /// <summary>
        /// Calls success, jobGone, or retryAfter depending on which response type this actually is.
        /// </summary>
        /// <param name="success">The function to call if the response object is a "success".</param>
        /// <param name="jobGone">The function to call if the response object is "jobGone".</param>
        /// <param name="retryAfter">The function to call if the response object is "retryAfter".</param>
        public abstract void Match(Action<TimeSpan, MasterObjectList> success, Action jobGone, Action<TimeSpan> retryAfter);

        /// <summary>
        /// Calls success, jobGone, or retryAfter depending on which response type this actually is.
        /// </summary>
        /// <param name="success">The function to call if the response object is a "success".</param>
        /// <param name="jobGone">The function to call if the response object is "jobGone".</param>
        /// <param name="retryAfter">The function to call if the response object is "retryAfter".</param>
        /// <returns>What success, jobGone, or  retryAfter return.</returns>
        public abstract T Match<T>(Func<TimeSpan, MasterObjectList, T> success, Func<T> jobGone, Func<TimeSpan, T> retryAfter);

        private class SuccessResponse : GetJobChunksReadyForClientProcessingSpectraS3Response
        {
            private readonly TimeSpan _retryAfter;
            private readonly MasterObjectList _jobResponse;

            public SuccessResponse(TimeSpan retryAfter, MasterObjectList jobResponse)
            {
                this._retryAfter = retryAfter;
                this._jobResponse = jobResponse;
            }

            public override void Match(Action<TimeSpan, MasterObjectList> success, Action jobGone, Action<TimeSpan> retryAfter)
            {
                success(this._retryAfter, this._jobResponse);
            }

            public override T Match<T>(Func<TimeSpan, MasterObjectList, T> success, Func<T> jobGone, Func<TimeSpan, T> retryAfter)
            {
                return success(this._retryAfter, this._jobResponse);
            }
        }

        private class JobGoneResponse : GetJobChunksReadyForClientProcessingSpectraS3Response
        {
            public override void Match(Action<TimeSpan, MasterObjectList> success, Action jobGone, Action<TimeSpan> retryAfter)
            {
                jobGone();
            }

            public override T Match<T>(Func<TimeSpan, MasterObjectList, T> success, Func<T> jobGone, Func<TimeSpan, T> retryAfter)
            {
                return jobGone();
            }
        }

        private class RetryAfterResponse : GetJobChunksReadyForClientProcessingSpectraS3Response
        {
            private readonly TimeSpan _retryAfter;

            public RetryAfterResponse(TimeSpan retryAfter)
            {
                this._retryAfter = retryAfter;
            }

            public override void Match(Action<TimeSpan, MasterObjectList> success, Action jobGone, Action<TimeSpan> retryAfter)
            {
                retryAfter(_retryAfter);
            }

            public override T Match<T>(Func<TimeSpan, MasterObjectList, T> success, Func<T> jobGone, Func<TimeSpan, T> retryAfter)
            {
                return retryAfter(_retryAfter);
            }
        }
    }
}
