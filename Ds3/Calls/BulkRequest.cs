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
using System.Collections.Generic;

using Ds3.Runtime;
using Ds3.Models;
using System.Xml.Linq;

namespace Ds3.Calls
{
    public abstract class BulkRequest : Ds3Request
    {
        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.PUT;
            }
        }

        internal override string Path
        {
            get
            {
                return "/_rest_/buckets/" + BucketName;
            }
        }

        public string BucketName { get; private set; }
        public List<Ds3Object> Objects { get; private set; }

        /// <summary>
        /// </summary>
        /// <param name="bucketName"></param>
        /// <param name="objectList">Note that both the Name and Size fields must be set on each Ds3Object.</param>
        public BulkRequest(string bucketName, List<Ds3Object> objectList)
        {
            this.BucketName = bucketName;
            this.Objects = objectList;
        }

        internal override Stream GetContentStream()
        {
            return GenerateObjectStream(this.Objects);
        }

        protected Stream GenerateObjectStream(IEnumerable<Ds3Object> objects)
        {
            return new XDocument()
                .AddFluent(
                    new XElement("objects").AddAllFluent(
                        from obj in objects
                        select new XElement("object")
                            .SetAttributeValueFluent("name", obj.Name)
                            .SetAttributeValueFluent("size", obj.Size.Value.ToString("D"))
                    )
                )
                .WriteToMemoryStream();
        }
    }
}
