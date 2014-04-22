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
        /// <summary>
        /// The ordered lists of objects to put or get.
        /// Note that the inner lists may be processed concurrently.
        /// </summary>
        public List<List<Ds3Object>> ObjectLists { get; private set; }

        internal BulkResponse(IWebResponse response)
            : base(response)
        {
            HandleStatusCode(HttpStatusCode.OK);
            ProcessRequest();
        }

        private void ProcessRequest()
        {
            using (Stream content = response.GetResponseStream())
            {
                ObjectLists = (
                    from objs in XmlExtensions
                        .ReadDocument(content)
                        .ElementOrThrow("masterobjectlist")
                        .Elements("objects")
                    select (
                        from obj in objs.Elements("object")
                        select new Ds3Object(
                            obj.AttributeOrThrow("name").Value,
                            Convert.ToInt64(obj.AttributeOrThrow("size").Value)
                        )
                    ).ToList()
                ).ToList();
            }
        }
    }
}
