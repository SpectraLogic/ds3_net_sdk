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
    public class PutBulkJobSpectraS3Request : Ds3Request
    {
        
        public string BucketName { get; private set; }

        public IEnumerable<Ds3Object> Objects { get; private set; }
        public long? MaxUploadSize { get; private set; }

        public PutBulkJobSpectraS3Request WithMaxUploadSize(long maxUploadSize)
        {
            this.MaxUploadSize = maxUploadSize;
            this.QueryParams["max_upload_size"] = maxUploadSize.ToString("D");
            return this;
        }

        
        private bool? _aggregating;
        public bool? Aggregating
        {
            get { return _aggregating; }
            set { WithAggregating(value); }
        }

        private bool? _ignoreNamingConflicts;
        public bool? IgnoreNamingConflicts
        {
            get { return _ignoreNamingConflicts; }
            set { WithIgnoreNamingConflicts(value); }
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

        public PutBulkJobSpectraS3Request WithAggregating(bool? aggregating)
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
        public PutBulkJobSpectraS3Request WithIgnoreNamingConflicts(bool? ignoreNamingConflicts)
        {
            this._ignoreNamingConflicts = ignoreNamingConflicts;
            if (ignoreNamingConflicts != null)
            {
                this.QueryParams.Add("ignore_naming_conflicts", ignoreNamingConflicts.ToString());
            }
            else
            {
                this.QueryParams.Remove("ignore_naming_conflicts");
            }
            return this;
        }
        public PutBulkJobSpectraS3Request WithName(string name)
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
        public PutBulkJobSpectraS3Request WithPriority(Priority? priority)
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

        
        public PutBulkJobSpectraS3Request(string bucketName, IEnumerable<Ds3Object> objects) {
            this.BucketName = bucketName;
            this.Objects = objects.ToList();
            this.QueryParams.Add("operation", "start_bulk_put");
            
        }

        internal override Stream GetContentStream()
        {
            return new XDocument()
                .AddFluent(
                    new XElement("Objects").AddAllFluent(
                        from obj in this.Objects
                        select new XElement("Object")
                            .SetAttributeValueFluent("Name", obj.Name)
                            .SetAttributeValueFluent("Size", toDs3ObjectSize(obj))
                    )
                )
                .WriteToMemoryStream();
        }

        internal string toDs3ObjectSize(Ds3Object ds3Object)
        {
            if (ds3Object.Size == null)
            {
                return null;
            }
            return ds3Object.Size.Value.ToString("D");
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
    }
}