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

using Ds3.Calls;
using Ds3.Models;
using Ds3.Runtime;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace Ds3.ResponseParsers
{
    internal class PutTapePartitionFailureNotificationRegistrationSpectraS3ResponseParser : IResponseParser<PutTapePartitionFailureNotificationRegistrationSpectraS3Request, PutTapePartitionFailureNotificationRegistrationSpectraS3Response>
    {
        public PutTapePartitionFailureNotificationRegistrationSpectraS3Response Parse(PutTapePartitionFailureNotificationRegistrationSpectraS3Request request, IWebResponse response)
        {
            using (response)
            {
                ResponseParseUtilities.HandleStatusCode(response, (HttpStatusCode)201);
                using (var stream = response.GetResponseStream())
                {
                    return new PutTapePartitionFailureNotificationRegistrationSpectraS3Response(
                        ModelParsers.ParseTapePartitionFailureNotificationRegistration(
                            XmlExtensions.ReadDocument(stream).ElementOrThrow("Data"))
                    );
                }
            }
        }
    }
}