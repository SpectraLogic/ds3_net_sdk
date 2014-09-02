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
    internal class GetBucketResponseParser : IResponseParser<GetBucketRequest, GetBucketResponse>
    {
        public GetBucketResponse Parse(GetBucketRequest request, IWebResponse response)
        {
            using (response)
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.OK);
                using (Stream content = response.GetResponseStream())
                {
                    var root = XmlExtensions.ReadDocument(content).ElementOrThrow("ListBucketResult");
                    return new GetBucketResponse(
                        name: root.TextOf("Name"),
                        prefix: root.TextOf("Prefix"),
                        marker: root.TextOf("Marker"),
                        nextMarker: root.TextOfOrNull("NextMarker"),
                        delimiter: root.TextOfOrNull("Delimiter"),
                        maxKeys: int.Parse(root.TextOf("MaxKeys")),
                        isTruncated: bool.Parse(root.TextOf("IsTruncated")),
                        creationDate: ParseDateTime(root.TextOfOrNull("CreationDate")),
                        objects: (
                            from obj in root.Elements("Contents")
                            let owner = obj.ElementOrThrow("Owner")
                            select new Ds3ObjectInfo(
                                obj.TextOf("Key"),
                                long.Parse(obj.TextOf("Size")),
                                new Owner(owner.TextOf("ID"), owner.TextOf("DisplayName")),
                                obj.TextOf("ETag"),
                                obj.TextOf("StorageClass"),
                                ParseDateTime(obj.TextOf("LastModified"))
                            )
                        ).ToList(),
                        metadata: ResponseParseUtilities.ExtractCustomMetadata(response.Headers),
                        commonPrefixes: root
                            .Elements("CommonPrefixes")
                            .Select(cp => cp.TextOf("Prefix"))
                            .ToList()
                    );
                }
            }
        }

        private static DateTime ParseDateTime(string dateTime)
        {
            DateTime result;
            return DateTime.TryParse(dateTime, out result) ? result : DateTime.MinValue;
        }
    }
}