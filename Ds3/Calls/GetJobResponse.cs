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

using System.Net;
using System.Collections.Generic;

using Ds3.Runtime;
using Ds3.Models;
using System.Linq;
using System;

namespace Ds3.Calls
{
    public class GetJobResponse : Ds3Response
    {
        public Bucket Bucket { get; private set; }
        public IEnumerable<Ds3ObjectList> ObjectLists { get; private set; }

        internal GetJobResponse(IWebResponse response)
            : base(response)
        {
        }

        protected override void ProcessResponse()
        {
            HandleStatusCode(HttpStatusCode.OK);
            using (var stream = response.GetResponseStream())
            {
                var root = XmlExtensions.ReadDocument(stream).ElementOrThrow("Data");

                // Populate Bucket.
                var bucketElement = root.ElementOrThrow("Job").ElementOrThrow("Bucket");
                this.Bucket = new Bucket(bucketElement.TextOf("Name"), bucketElement.TextOf("CreationDate"));

                // Populate ObjectLists.
                this.ObjectLists = (
                    from objList in root.Elements("Streams")
                    let objects =
                        from obj in objList.Elements("ObjectsNotInCache").Concat(objList.Elements("ObjectsInCache"))
                        orderby Convert.ToInt32(obj.TextOf("OrderIndex"))
                        select new Ds3Object(obj.TextOf("Key"), Convert.ToInt64(obj.TextOf("Size")))
                    select new Ds3ObjectList(objects, objList.AttributeText("ServerId"))
                ).ToList();
            }
        }
    }
}
