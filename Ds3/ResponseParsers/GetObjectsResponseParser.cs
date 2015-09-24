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
using System.IO;
using System.Linq;
using System.Net;

using Ds3.Calls;
using Ds3.Models;
using Ds3.Runtime;

namespace Ds3.ResponseParsers
{
    internal class GetObjectsResponseParser : IResponseParser<GetObjectsRequest, GetObjectsResponse>
    {
        public GetObjectsResponse Parse(GetObjectsRequest request, IWebResponse response)
        {
            using (response)
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.OK);
                using (Stream content = response.GetResponseStream())
                {
                    var root = XmlExtensions.ReadDocument(content).ElementOrThrow("Data");

                    var infoobjects =   from obj in root.Elements("S3Object")
                                    select new DS3GetObjectsInfo(
                                        obj.TextOf("Name"),
                                        obj.TextOf("Id"),
                                        obj.TextOf("BucketId"),
                                        ParseDateTime(obj.TextOf("CreationDate")),
                                        obj.TextOf("Type"),
                                        long.Parse(obj.TextOf("Version")));
                    return new GetObjectsResponse()
                        {
                            Objects = infoobjects
                        };
                    
                }
            }
        }

        private static DateTime ParseDateTime(string dateTime)
        {
            DateTime result;
            return DateTime.TryParse(dateTime, out result) ? result : DateTime.MinValue;
        }

        public GetBucketResponse Parse(GetBucketRequest request, IWebResponse response)
        {
            throw new NotImplementedException();
        }
    }
}