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
using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Ds3.Calls
{
    public class BulkGetRequest : BulkRequest
    {
        public ChunkOrdering? ChunkOrder { get; private set; }

        public BulkGetRequest(string bucketName, List<Ds3Object> objects) 
            : base(bucketName, objects, false)
        {
            QueryParams.Add("operation", "start_bulk_get");
        }

        public BulkGetRequest WithChunkOrdering(ChunkOrdering chunkOrdering)
        {
            this.ChunkOrder = chunkOrdering;
            return this;
        }

        protected internal override XDocument GenerateObjectsDocument(IEnumerable<Ds3Object> objects)
        {
            var doc = base.GenerateObjectsDocument(objects);
            if (this.ChunkOrder.HasValue)
            {
                doc.Root.SetAttributeValue(
                    "ChunkClientProcessingOrderGuarantee",
                    BuildChunkOrderingEnumString(this.ChunkOrder.Value)
                );
            }
            return doc;
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
