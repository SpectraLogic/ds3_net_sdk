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

        /// <summary>
        /// Iterates over a group of Ds3Objects and marshals them into an xml stream.
        /// Only the name for the Ds3Objects are marshaled. This is used to marshal
        /// the request payloads for:
        ///   EjectStorageDomainBlobsSpectraS3Request
        ///   GetPhysicalPlacementForObjectsSpectraS3Request
        ///   GetPhysicalPlacementForObjectsWithFullDetailsSpectraS3Request
        ///   VerifyPhysicalPlacementForObjectsSpectraS3Request
        ///   VerifyPhysicalPlacementForObjectsWithFullDetailsSpectraS3Request
        /// </summary>
        /// <param name="ds3Objects">The Ds3Objects to be marshaled to xml</param>
        /// <returns>Stream containing xml marshaling of Ds3Object names</returns>
        public static Stream MarshalDs3ObjectNames(IEnumerable<Ds3Object> ds3Objects)
        {
            return new XDocument()
                .AddFluent(
                    new XElement("Objects").AddAllFluent(
                        from obj in ds3Objects
                        select new XElement("Object")
                            .SetAttributeValueFluent("Name", obj.Name)
                    )
                )
                .WriteToMemoryStream();
        }

        /// <summary>
        /// Marshals object names and Ds3PartialObjects into an xml formatted stream.
        /// This is used to marshal the request payloads for:
        ///   GetBulkJobSpectraS3Request
        ///   VerifyBulkJobSpectraS3Request
        /// </summary>
        /// <param name="fullObjectNames">List of object names representing full objects to be marshaled to xml</param>
        /// <param name="ds3PartialObjects">List of Ds3PartialObjects to be marshaled to xml</param>
        /// <returns>Stream containing xml marshaling of full objects followed by Ds3PartialObjects</returns>
        public static Stream MarshalFullAndPartialObjects(IEnumerable<Ds3Object> fullObjectNames, IEnumerable<Ds3PartialObject> ds3PartialObjects)
        {
            var root = new XElement("Objects")
                .AddAllFluent(
                    from obj in fullObjectNames
                    select new XElement("Object")
                        .SetAttributeValueFluent("Name", obj.Name)
                        .SetAttributeValueFluent("VersionId", obj.VersionId)
                )
                .AddAllFluent(
                    from partial in ds3PartialObjects
                    select new XElement("Object")
                        .SetAttributeValueFluent("Name", partial.Name)
                        .SetAttributeValueFluent("Offset", partial.Range.Start.ToString())
                        .SetAttributeValueFluent("Length", partial.Range.Length.ToString())
                        .SetAttributeValueFluent("VersionId", partial.VersionId)
                );
            return new XDocument().AddFluent(root).WriteToMemoryStream();
        }

        /// <summary>
        /// Iterates over a group of Ds3Objects and marshals them into an xml stream
        /// describing objects to be deleted. Only the name for the Ds3Objects are 
        /// marshaled. This is used to marshal the request payload for:
        ///   DeleteObjectsRequest
        /// </summary>
        /// <param name="ds3Objects">List of objects to be deleted</param>
        /// <returns>Stream containing xml marshaling of object names to be deleted</returns>
        public static Stream MarshalDeleteObjectNames(IEnumerable<Ds3Object> ds3Objects)
        {
            return new XDocument()
                .AddFluent(
                    new XElement("Delete").AddAllFluent(
                        from curObject in ds3Objects
                        select new XElement("Object").AddFluent(new XElement("Key").SetValueFluent(curObject.Name))
                    )
                )
                .WriteToMemoryStream();
        }

        /// <summary>
        /// Iterates over a group of Ds3Objects and marshals them into an xml stream
        /// describing their name and size. This is used to marshal the request payload
        /// for command:
        ///   PutBulkJobSpectraS3Request
        /// </summary>
        /// <param name="ds3Objects">List of objects being marshaled</param>
        /// <returns>Stream containing xml marshaling of objects' name and size</returns>
        public static Stream MarshalDs3ObjectNameAndSize(IEnumerable<Ds3Object> ds3Objects)
        {
            return new XDocument()
                .AddFluent(
                    new XElement("Objects").AddAllFluent(
                        from obj in ds3Objects
                        select new XElement("Object")
                            .SetAttributeValueFluent("Name", obj.Name)
                            .SetAttributeValueFluent("Size", ToDs3ObjectSize(obj))
                    )
                )
                .WriteToMemoryStream();
        }

        /// <summary>
        /// Retrieves the string representation of a Ds3Object's size, or null if
        /// it does not have one.
        /// </summary>
        /// <param name="ds3Object">The object whose size we are retrieving</param>
        /// <returns></returns>
        internal static string ToDs3ObjectSize(Ds3Object ds3Object)
        {
            if (ds3Object.Size == null)
            {
                return null;
            }
            return ds3Object.Size.Value.ToString("D");
        }
    }
}
