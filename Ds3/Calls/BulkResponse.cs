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
using System.Net;
using System.Linq;
using System.Collections.Generic;

using Ds3.Runtime;
using Ds3.Models;

namespace Ds3.Calls
{
    public class BulkResponse : Ds3Response
    {
        public Guid JobId { get; set; }

        /// <summary>
        /// The ordered lists of objects to put or get.
        /// Note that the inner lists may be processed concurrently.
        /// </summary>
        public virtual IEnumerable<Ds3ObjectList> ObjectLists { get; private set; }

        internal BulkResponse(IWebResponse response)
            : base(response)
        {
        }

        protected override void ProcessResponse()
        {
            HandleStatusCode(HttpStatusCode.OK);
            using (Stream content = response.GetResponseStream())
            {
                var masterObjectList = XmlExtensions
                    .ReadDocument(content)
                    .ElementOrThrow("MasterObjectList");
                JobId = Guid.Parse(masterObjectList.AttributeOrThrow("JobId").Value);
                ObjectLists = (
                    from objs in masterObjectList.Elements("Objects")
                    let objects =
                        from obj in objs.Elements("Object")
                        select new Ds3Object(
                            obj.AttributeOrThrow("Name").Value,
                            Convert.ToInt64(obj.AttributeOrThrow("Size").Value)
                        )
                    select new Ds3ObjectList(objects, objs.AttributeText("ServerId"))
                ).ToList();
            }
        }
    }
}
