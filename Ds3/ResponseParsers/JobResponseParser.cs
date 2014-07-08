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
    internal class JobResponseParser<TRequest> : IResponseParser<TRequest, JobResponse>
        where TRequest : Ds3Request
    {
        public JobResponse Parse(TRequest request, IWebResponse response)
        {
            using (response)
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.OK);
                using (Stream content = response.GetResponseStream())
                {
                    var masterObjectList = XmlExtensions
                        .ReadDocument(content)
                        .ElementOrThrow("MasterObjectList");
                    return new JobResponse(
                        bucketName: masterObjectList.AttributeText("BucketName"),
                        jobId: Guid.Parse(masterObjectList.AttributeText("JobId")),
                        nodes: (
                            from nodeElement in masterObjectList.Element("Nodes").Elements("Node")
                            select new Node(
                                Guid.Parse(nodeElement.AttributeText("Id")),
                                nodeElement.AttributeText("EndPoint"),
                                ParseIntOrNull(nodeElement.AttributeTextOrNull("HttpPort")),
                                ParseIntOrNull(nodeElement.AttributeTextOrNull("HttpsPort"))
                            )
                        ).ToList(),
                        objectLists: (
                            from objs in masterObjectList.Elements("Objects")
                            let objects =
                                from objectElement in objs.Elements("Object")
                                select new JobObject(
                                    objectElement.AttributeText("Name"),
                                    from blob in objectElement.Elements("Blob")
                                    select new Blob(
                                        Guid.Parse(blob.AttributeText("Id")),
                                        long.Parse(blob.AttributeText("Length")),
                                        long.Parse(blob.AttributeText("Offset"))
                                    )
                                )
                            select new Ds3ObjectList(
                                long.Parse(objs.AttributeTextOrNull("ChunkNumber")),
                                ParseGuidOrNull(objs.AttributeTextOrNull("NodeId")),
                                objects
                            )
                        ).ToList()
                    );
                }
            }
        }

        private static Guid? ParseGuidOrNull(string guidStringOrNull)
        {
            return guidStringOrNull == null ? null : (Guid?)Guid.Parse(guidStringOrNull);
        }

        private static int? ParseIntOrNull(string intStringOrNull)
        {
            return intStringOrNull == null ? null : (int?)int.Parse(intStringOrNull);
        }
    }
}
