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

using System.IO;
using System.Net;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

using Ds3.Runtime;
using Ds3.Models;

namespace Ds3.Calls
{
    public class GetServiceResponse : Ds3Response
    {
        public Owner Owner { get; private set; }
        public List<Bucket> Buckets { get; private set; }

        internal GetServiceResponse(IWebResponse responseStream)
            : base(responseStream)
        {
        }

        protected override void ProcessResponse()
        {
            HandleStatusCode(HttpStatusCode.OK);
            using (Stream content = response.GetResponseStream())
            {
                var root = XmlExtensions.ReadDocument(content).ElementOrThrow("ListAllMyBucketsResult");

                // Parse owner.
                var owner = root.ElementOrThrow("Owner");
                this.Owner = new Owner(owner.ElementOrThrow("ID").Value, owner.ElementOrThrow("DisplayName").Value);

                // Parse buckets.
                this.Buckets = (
                    from bucket in root.ElementOrThrow("Buckets").Elements("Bucket")
                    select new Bucket(bucket.ElementOrThrow("Name").Value, bucket.ElementOrThrow("CreationDate").Value)
                ).ToList();
            }
        }
    }
}
