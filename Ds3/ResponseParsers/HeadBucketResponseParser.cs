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
using Ds3.Models;
using Ds3.Runtime;
using HeadBucketStatus = Ds3.Calls.HeadBucketResponse.StatusType;

namespace Ds3.ResponseParsers
{
    internal class HeadBucketResponseParser : IResponseParser<HeadBucketRequest, HeadBucketResponse>
    {
        public HeadBucketResponse Parse(HeadBucketRequest request, IWebResponse response)
        {
            using (response)
            {
                ResponseParserHelpers.HandleStatusCode(response, HttpStatusCode.OK, HttpStatusCode.Forbidden, HttpStatusCode.NotFound);
                HeadBucketStatus status;
                switch (response.StatusCode)
                {
                    case HttpStatusCode.OK: status = HeadBucketStatus.Exists; break;
                    case HttpStatusCode.Forbidden: status = HeadBucketStatus.NotAuthorized; break;
                    case HttpStatusCode.NotFound: status = HeadBucketStatus.DoesntExist; break;
                    default: throw new NotSupportedException(Resources.InvalidEnumValueException);
                }
                return new HeadBucketResponse(status: status);
            }
        }
    }
}