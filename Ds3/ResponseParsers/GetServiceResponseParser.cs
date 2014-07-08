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

using System.IO;
using System.Linq;
using System.Net;

using Ds3.Calls;
using Ds3.Models;
using Ds3.Runtime;

namespace Ds3.ResponseParsers
{
    internal class GetServiceResponseParser : IResponseParser<GetServiceRequest, GetServiceResponse>
    {
        public GetServiceResponse Parse(GetServiceRequest request, IWebResponse response)
        {
            ResponseParserHelpers.HandleStatusCode(response, HttpStatusCode.OK);
            using (Stream content = response.GetResponseStream())
            {
                var root = XmlExtensions.ReadDocument(content).ElementOrThrow("ListAllMyBucketsResult");
                var owner = root.ElementOrThrow("Owner");
                return new GetServiceResponse(
                    owner: new Owner(owner.ElementOrThrow("ID").Value, owner.ElementOrThrow("DisplayName").Value),
                    buckets: (
                        from bucket in root.ElementOrThrow("Buckets").Elements("Bucket")
                        select new Bucket(bucket.ElementOrThrow("Name").Value, bucket.ElementOrThrow("CreationDate").Value)
                    ).ToList()
                );
            }
        }
    }
}