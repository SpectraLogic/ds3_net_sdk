
using System;
using System.Net;

using Ds3.Calls;
using Ds3.Runtime;

namespace Ds3.ResponseParsers
{
    class GetAvailableJobChunksResponseParser : IResponseParser<GetAvailableJobChunksRequest, GetAvailableJobChunksResponse>
    {
        public GetAvailableJobChunksResponse Parse(GetAvailableJobChunksRequest request, IWebResponse response)
        {
            using (response)
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.OK, HttpStatusCode.ServiceUnavailable, HttpStatusCode.NotFound);
                using (var responseStream = response.GetResponseStream())
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return GetAvailableJobChunksResponse.Success(
                                JobResponseParser<GetAvailableJobChunksRequest>.ParseResponseContent(responseStream)
                            );

                        case HttpStatusCode.ServiceUnavailable:
                            string retryAfterInSeconds;
                            return response.Headers.TryGetValue("retry-after", out retryAfterInSeconds)
                                ? GetAvailableJobChunksResponse.RetryAfter(TimeSpan.FromSeconds(int.Parse(retryAfterInSeconds)))
                                : GetAvailableJobChunksResponse.Retry;

                        case HttpStatusCode.NotFound:
                            return GetAvailableJobChunksResponse.JobGone;

                        default:
                            throw new NotSupportedException("This line of code should be impossible to hit.");
                    }
                }
            }
        }
    }
}
