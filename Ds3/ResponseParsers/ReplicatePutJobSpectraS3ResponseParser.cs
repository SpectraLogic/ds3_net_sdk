/*
 * ******************************************************************************
 *   Copyright 2014-2016 Spectra Logic Corporation. All Rights Reserved.
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

using Ds3.Calls;
using Ds3.Runtime;
using System.Net;

namespace Ds3.ResponseParsers
{
    internal class ReplicatePutJobSpectraS3ResponseParser : IResponseParser<ReplicatePutJobSpectraS3Request, ReplicatePutJobSpectraS3Response>
    {
        public ReplicatePutJobSpectraS3Response Parse(ReplicatePutJobSpectraS3Request request, IWebResponse response)
        {
            using (response)
            {
                ResponseParseUtilities.HandleStatusCode(response, (HttpStatusCode)200, HttpStatusCode.ServiceUnavailable);
                switch (response.StatusCode)
                {
                    case (HttpStatusCode)200:
                        using (var stream = response.GetResponseStream())
                        {
                            return new ReplicatePutJobSpectraS3Response(
                                ModelParsers.ParseMasterObjectList(
                                    XmlExtensions.ReadDocument(stream).ElementOrThrow("MasterObjectList"))
                            );
                        }

                    case HttpStatusCode.ServiceUnavailable:
                        throw new Ds3MaxJobsException();

                    default:
                        return null; // we should never hit that
                }
            }
        }
    }
}