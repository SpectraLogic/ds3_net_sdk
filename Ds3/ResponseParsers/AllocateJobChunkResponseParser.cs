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
using System.Net;

using Ds3.Calls;
using Ds3.Runtime;

namespace Ds3.ResponseParsers
{
    internal class AllocateJobChunkResponseParser : IResponseParser<AllocateJobChunkRequest, AllocateJobChunkResponse>
    {
        public AllocateJobChunkResponse Parse(AllocateJobChunkRequest request, IWebResponse response)
        {
            using (response)
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.OK, HttpStatusCode.ServiceUnavailable);
                using (var responseStream = response.GetResponseStream())
                {
                    switch (response.StatusCode)
                    {
                        case HttpStatusCode.OK:
                            return AllocateJobChunkResponse.Success(
                                JobResponseParser<AllocateJobChunkRequest>.ParseObjectList(
                                    XmlExtensions
                                        .ReadDocument(responseStream)
                                        .ElementOrThrow("Objects")
                                )
                            );

                        case HttpStatusCode.ServiceUnavailable:
                            string retryAfterInSeconds;
                            return response.Headers.TryGetValue("retry-after", out retryAfterInSeconds)
                                ? AllocateJobChunkResponse.RetryAfter(TimeSpan.FromSeconds(int.Parse(retryAfterInSeconds)))
                                : AllocateJobChunkResponse.Retry;

                        default:
                            throw new NotSupportedException("This line of code should be impossible to hit.");
                    }
                }
            }
        }
    }
}
