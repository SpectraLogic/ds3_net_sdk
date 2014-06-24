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
    public class GetBucketResponse : Ds3Response
    {
        public virtual string Name { get; private set; }
        public virtual string Prefix { get; private set; }
        public virtual string Marker { get; private set; }
        public virtual int MaxKeys { get; private set; }
        public virtual bool IsTruncated { get; private set; }
        public virtual string Delimiter { get; private set; }
        public virtual string NextMarker { get; private set; }
        public virtual DateTime CreationDate { get; private set; }
        public virtual List<Ds3Object> Objects { get; private set; }
        public virtual IDictionary<string, string> Metadata { get; private set; }

        internal GetBucketResponse(IWebResponse responseStream)
            : base(responseStream)
        {
        }

        protected override void ProcessResponse()
        {
            HandleStatusCode(HttpStatusCode.OK);            
            using (Stream content = response.GetResponseStream())
            {
                var root = XmlExtensions.ReadDocument(content).ElementOrThrow("ListBucketResult");
                Name = root.TextOf("Name");
                Prefix = root.TextOf("Prefix");
                Marker = root.TextOf("Marker");
                NextMarker = root.TextOfOrNull("NextMarker");
                Delimiter = root.TextOfOrNull("Delimiter");
                MaxKeys = int.Parse(root.TextOf("MaxKeys"));
                IsTruncated = bool.Parse(root.TextOf("IsTruncated"));
                CreationDate = ParseDateTime(root.TextOfOrNull("CreationDate"));
                Objects = (
                    from obj in root.Elements("Contents")
                    let owner = obj.ElementOrThrow("Owner")
                    select new Ds3Object(
                        obj.TextOf("Key"),
                        int.Parse(obj.TextOf("Size")),
                        new Owner(owner.TextOf("ID"), owner.TextOf("DisplayName")),
                        obj.TextOf("ETag"),
                        obj.TextOf("StorageClass"),
                        ParseDateTime(obj.TextOf("LastModified"))
                    )
                ).ToList();
                Metadata = ParseUtilities.ExtractCustomMetadata(response.Headers);
            }
        }

        private static DateTime ParseDateTime(string dateTime)
        {
            DateTime result;
            return DateTime.TryParse(dateTime, out result) ? result : DateTime.MinValue;
        }
    }
}
