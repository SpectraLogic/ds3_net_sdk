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

using Ds3.Models;
using Ds3.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Ds3.Calls
{
    public class BulkGetRequest : Ds3Request
    {
        public string BucketName { get; private set; }
        public IEnumerable<string> FullObjects { get; private set; }
        public ChunkOrdering? ChunkOrder { get; private set; }

        public BulkGetRequest(string bucketName, IEnumerable<string> fullObjects)
        {
            this.BucketName = bucketName;
            this.FullObjects = fullObjects.ToList();
            this.QueryParams.Add("operation", "start_bulk_get");
        }

        public BulkGetRequest(string bucketName, List<Ds3Object> objects) 
            : this(bucketName, objects.Select(o => o.Name))
        {
        }

        public BulkGetRequest WithChunkOrdering(ChunkOrdering chunkOrdering)
        {
            this.ChunkOrder = chunkOrdering;
            return this;
        }

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
                return "/_rest_/bucket/" + BucketName;
            }
        }

        internal override Stream GetContentStream()
        {
            var root = new XElement("Objects")
                .AddAllFluent(
                    from name in this.FullObjects
                    select new XElement("Object").SetAttributeValueFluent("Name", name)
                );
            if (this.ChunkOrder.HasValue)
            {
                root.SetAttributeValue(
                    "ChunkClientProcessingOrderGuarantee",
                    BuildChunkOrderingEnumString(this.ChunkOrder.Value)
                );
            }
            return new XDocument().AddFluent(root).WriteToMemoryStream();
        }


        private static string BuildChunkOrderingEnumString(ChunkOrdering chunkOrder)
        {
            switch (chunkOrder)
            {
                case ChunkOrdering.None: return "NONE";
                case ChunkOrdering.InOrder: return "IN_ORDER";
                default: throw new NotSupportedException(Resources.InvalidEnumValueException);
            }
        }
    }
}
