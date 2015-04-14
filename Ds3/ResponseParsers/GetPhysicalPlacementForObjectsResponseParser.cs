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
using Ds3.Models;
using Ds3.Runtime;
using System.Linq;
using System.Net;
using System.Xml.Linq;

namespace Ds3.ResponseParsers
{
    internal class GetPhysicalPlacementForObjectsResponseParser : IResponseParser<GetPhysicalPlacementForObjectsRequest, GetPhysicalPlacementForObjectsResponse>
    {
        public GetPhysicalPlacementForObjectsResponse Parse(
            GetPhysicalPlacementForObjectsRequest request,
            IWebResponse response)
        {
            using (response)
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.OK);
                using (var stream = response.GetResponseStream())
                {
                    return new GetPhysicalPlacementForObjectsResponse(
                        XmlExtensions
                            .ReadDocument(stream)
                            .ElementOrThrow("Data")
                            .Elements("Object")
                            .Select(ParseObjectPlacement)
                            .ToList()
                    );
                }
            }
        }

        private static Ds3ObjectPlacement ParseObjectPlacement(XElement objectEl)
        {
            return new Ds3ObjectPlacement
            {
                 Name = objectEl.AttributeText("Name"),
                 Offset = long.Parse(objectEl.AttributeText("Offset")),
                 Length = long.Parse(objectEl.AttributeText("Length")),
                 Tapes = objectEl
                     .ElementOrThrow("PhysicalPlacement")
                     .ElementOrThrow("Tapes")
                     .Elements("Tape")
                     .Select(GetAggregatePhysicalPlacementResponseParser.ParseTape)
            };
        }
    }
}
