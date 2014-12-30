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
using Ds3.Runtime;
using System.Linq;

namespace Ds3.ResponseParsers
{
    internal class HeadObjectResponseParser : IResponseParser<HeadObjectRequest, HeadObjectResponse>
    {
        public HeadObjectResponse Parse(HeadObjectRequest request, IWebResponse response)
        {
            using (response)
            {
                return new HeadObjectResponse(
                    long.Parse(response.Headers.Single(kvp => kvp.Key.ToLowerInvariant() == "content-length").Value),
                    response.Headers.Single(kvp => kvp.Key.ToLowerInvariant() == "etag").Value,
                    ResponseParseUtilities.ExtractCustomMetadata(response.Headers)
                );
            }
        }
    }
}
