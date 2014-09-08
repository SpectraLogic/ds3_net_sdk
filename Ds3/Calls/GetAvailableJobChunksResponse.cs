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

        public static GetAvailableJobChunksResponse Success(JobResponse jobResponse)
        {
            return new SuccessResponse(jobResponse);
        }

        public static GetAvailableJobChunksResponse JobGone
        {
            get { return _jobGone; }
        }

        public static GetAvailableJobChunksResponse RetryAfter(TimeSpan retryAfter)
        {
            return new RetryAfterResponse(retryAfter);
        }

        public abstract void Match(Action<JobResponse> success, Action jobGone, Action<TimeSpan> retryAfter);

        public abstract T Match<T>(Func<JobResponse, T> success, Func<T> jobGone, Func<TimeSpan, T> retryAfter);

        private class SuccessResponse : GetAvailableJobChunksResponse
        {
            private readonly JobResponse _jobResponse;

            public SuccessResponse(JobResponse jobResponse)
            {
                this._jobResponse = jobResponse;
            }

            public override void Match(Action<JobResponse> success, Action jobGone, Action<TimeSpan> retryAfter)
            {
                success(this._jobResponse);
            }

            public override T Match<T>(Func<JobResponse, T> success, Func<T> jobGone, Func<TimeSpan, T> retryAfter)
            {
                return success(this._jobResponse);
            }
        }

        private class JobGoneResponse : GetAvailableJobChunksResponse
        {
            public override void Match(Action<JobResponse> success, Action jobGone, Action<TimeSpan> retryAfter)
            {
                jobGone();
            }

            public override T Match<T>(Func<JobResponse, T> success, Func<T> jobGone, Func<TimeSpan, T> retryAfter)
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

            public override void Match(Action<JobResponse> success, Action jobGone, Action<TimeSpan> retryAfter)
            {
                retryAfter(_retryAfter);
            }

            public override T Match<T>(Func<JobResponse, T> success, Func<T> jobGone, Func<TimeSpan, T> retryAfter)
            {
                return retryAfter(_retryAfter);
            }
        }
    }
}
