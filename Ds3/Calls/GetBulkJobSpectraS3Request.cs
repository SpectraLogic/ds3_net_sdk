/*
 * ******************************************************************************
 *   Copyright 2014-2016 Spectra Logic Corporation. All Rights Reserved.
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

// This code is auto-generated, do not modify
using Ds3.Models;
using Ds3.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Ds3.Calls
{
    public class GetBulkJobSpectraS3Request : Ds3Request
    {
        
        public string BucketName { get; private set; }

        public IEnumerable<string> FullObjects { get; private set; }

        public IEnumerable<Ds3PartialObject> PartialObjects { get; private set; }

        public JobChunkClientProcessingOrderGuarantee? ChunkClientProcessingOrderGuarantee { get; private set; }

        public GetBulkJobSpectraS3Request WithChunkClientProcessingOrderGuarantee(JobChunkClientProcessingOrderGuarantee chunkClientProcessingOrderGuarantee)
        {
            this.ChunkClientProcessingOrderGuarantee = chunkClientProcessingOrderGuarantee;
            return this;
        }

        
        private bool _aggregating;
        public bool Aggregating
        {
            get { return _aggregating; }
            set { WithAggregating(value); }
        }

        public GetBulkJobSpectraS3Request WithAggregating(bool aggregating)
        {
            this._aggregating = aggregating;
            if (aggregating != null) {
                this.QueryParams.Add("aggregating", Aggregating.ToString());
            }
            else
            {
                this.QueryParams.Remove("aggregating");
            }
            return this;
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { WithName(value); }
        }

        public GetBulkJobSpectraS3Request WithName(string name)
        {
            this._name = name;
            if (name != null) {
                this.QueryParams.Add("name", Name);
            }
            else
            {
                this.QueryParams.Remove("name");
            }
            return this;
        }

        private Priority _priority;
        public Priority Priority
        {
            get { return _priority; }
            set { WithPriority(value); }
        }

        public GetBulkJobSpectraS3Request WithPriority(Priority priority)
        {
            this._priority = priority;
            if (priority != null) {
                this.QueryParams.Add("priority", Priority.ToString());
            }
            else
            {
                this.QueryParams.Remove("priority");
            }
            return this;
        }

        public GetBulkJobSpectraS3Request(string bucketName, IEnumerable<string> fullObjects, IEnumerable<Ds3PartialObject> partialObjects) {
            this.BucketName = bucketName;
            this.FullObjects = fullObjects.ToList();
            this.PartialObjects = partialObjects.ToList();
            this.QueryParams.Add("operation", "start_bulk_get");
            
        }

        public GetBulkJobSpectraS3Request(string bucketName, List<Ds3Object> objects)
            : this(bucketName, objects.Select(o => o.Name), Enumerable.Empty<Ds3PartialObject>())
        {
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
                )
                .AddAllFluent(
                    from partial in this.PartialObjects
                    select new XElement("Object")
                        .SetAttributeValueFluent("Name", partial.Name)
                        .SetAttributeValueFluent("Offset", partial.Range.Start.ToString())
                        .SetAttributeValueFluent("Length", partial.Range.Length.ToString())
                );
            if (this.ChunkClientProcessingOrderGuarantee.HasValue)
            {
                root.SetAttributeValue(
                    "ChunkClientProcessingOrderGuarantee",
                    BuildChunkOrderingEnumString(this.ChunkClientProcessingOrderGuarantee.Value)
                );
            }
            return new XDocument().AddFluent(root).WriteToMemoryStream();
        }


        private static string BuildChunkOrderingEnumString(JobChunkClientProcessingOrderGuarantee chunkClientProcessingOrderGuarantee)
        {
            switch (chunkClientProcessingOrderGuarantee)
            {
                case JobChunkClientProcessingOrderGuarantee.NONE: return "NONE";
                case JobChunkClientProcessingOrderGuarantee.IN_ORDER: return "IN_ORDER";
                default: throw new NotSupportedException(Resources.InvalidEnumValueException);
            }
        }
    }
}