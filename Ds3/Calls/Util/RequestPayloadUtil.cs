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

using System.Collections.Generic;
using System.Linq;
using System.IO;
using Ds3.Models;
using Ds3.Runtime;
using System.Xml.Linq;

namespace Ds3.Calls.Util
{
    /// <summary>
    /// Contains utilities used to create properly formatted xml request payloads.
    /// </summary>
    public static class RequestPayloadUtil
    {
        /// <summary>
        /// Iterates over a group of Part objects and marshals them into an xml stream.
        /// This is used in CompleteMultiPartUploadRequest to marshal the request payload.
        /// </summary>
        /// <param name="parts">Parts to be marshaled to xml</param>
        /// <returns>Stream containing xml marshaling of parts</returns>
        public static Stream MarshalPartsToStream(IEnumerable<Part> parts)
        {
            return new XDocument()
                .AddFluent(
                    new XElement("CompleteMultipartUpload").AddAllFluent(
                        from part in parts
                        select new XElement("Part")
                        .AddFluent(new XElement("PartNumber").SetValueFluent(part.PartNumber.ToString()))
                        .AddFluent(new XElement("ETag").SetValueFluent(part.ETag))
                    )
                )
                .WriteToMemoryStream();
        }
    }
}
