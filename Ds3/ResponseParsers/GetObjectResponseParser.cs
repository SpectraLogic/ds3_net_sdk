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

using System.Net;

using Ds3.Calls;
using Ds3.Runtime;

namespace Ds3.ResponseParsers
{
    internal class GetObjectResponseParser : IResponseParser<GetObjectRequest, GetObjectResponse>
    {
        private readonly int _copyBufferSize;

        public GetObjectResponseParser(int copyBufferSize)
        {
            this._copyBufferSize = copyBufferSize;
        }
        
        public GetObjectResponse Parse(GetObjectRequest request, IWebResponse response)
        {
            using (response)
            {
                ResponseParserHelpers.HandleStatusCode(response, HttpStatusCode.OK);
                using (var responseStream = response.GetResponseStream())
                {
                    responseStream.CopyTo(request.DestinationStream, this._copyBufferSize);
                }
                return new GetObjectResponse(
                    metadata: ParseUtilities.ExtractCustomMetadata(response.Headers)
                );
            }
        }
    }
}
