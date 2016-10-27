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

        private JobChunkClientProcessingOrderGuarantee? _chunkClientProcessingOrderGuarantee;
        public JobChunkClientProcessingOrderGuarantee? ChunkClientProcessingOrderGuarantee
        {
            get { return _chunkClientProcessingOrderGuarantee; }
            set { WithChunkClientProcessingOrderGuarantee(value); }
        }

        public GetBulkJobSpectraS3Request WithChunkClientProcessingOrderGuarantee(JobChunkClientProcessingOrderGuarantee? chunkClientProcessingOrderGuarantee)
        {
            this._chunkClientProcessingOrderGuarantee = chunkClientProcessingOrderGuarantee;
            if (chunkClientProcessingOrderGuarantee != null)
            {
                this.QueryParams.Add("chunkClientProcessingOrderGuarantee", BuildChunkOrderingEnumString(chunkClientProcessingOrderGuarantee.Value));
            }
            else
            {
                this.QueryParams.Remove("chunkClientProcessingOrderGuarantee");
            }
            return this;
        }

        
        private bool? _aggregating;
        public bool? Aggregating
        {
            get { return _aggregating; }
            set { WithAggregating(value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { WithName(value); }
        }

        private Priority? _priority;
        public Priority? Priority
        {
            get { return _priority; }
            set { WithPriority(value); }
        }

        
        public GetBulkJobSpectraS3Request WithAggregating(bool? aggregating)
        {
            this._aggregating = aggregating;
            if (aggregating != null)
            {
                this.QueryParams.Add("aggregating", aggregating.ToString());
            }
            else
            {
                this.QueryParams.Remove("aggregating");
            }
            return this;
        }

        
        public GetBulkJobSpectraS3Request WithName(string name)
        {
            this._name = name;
            if (name != null)
            {
                this.QueryParams.Add("name", name);
            }
            else
            {
                this.QueryParams.Remove("name");
            }
            return this;
        }

        
        public GetBulkJobSpectraS3Request WithPriority(Priority? priority)
        {
            this._priority = priority;
            if (priority != null)
            {
                this.QueryParams.Add("priority", priority.ToString());
            }
            else
            {
                this.QueryParams.Remove("priority");
            }
            return this;
        }


        
        
        public GetBulkJobSpectraS3Request(string bucketName, IEnumerable<string> fullObjects, IEnumerable<Ds3PartialObject> partialObjects)
        {
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
            return new XDocument().AddFluent(root).WriteToMemoryStream();
        }

        internal override long GetContentLength()
        {
            return GetContentStream().Length;
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