/*
 * ******************************************************************************
 *   Copyright 2014-2017 Spectra Logic Corporation. All Rights Reserved.
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
using Ds3.Calls.Util;
using Ds3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ds3.Calls
{
    public class StageObjectsJobSpectraS3Request : Ds3Request
    {
        
        public string BucketName { get; private set; }

        public IEnumerable<string> FullObjects { get; private set; }

        public IEnumerable<Ds3PartialObject> PartialObjects { get; private set; }

        
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

        
        public StageObjectsJobSpectraS3Request WithName(string name)
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

        
        public StageObjectsJobSpectraS3Request WithPriority(Priority? priority)
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


        
        
        public StageObjectsJobSpectraS3Request(string bucketName, IEnumerable<string> fullObjects, IEnumerable<Ds3PartialObject> partialObjects)
        {
            this.BucketName = bucketName;
            this.FullObjects = fullObjects.ToList();
            this.PartialObjects = partialObjects.ToList();
            this.QueryParams.Add("operation", "start_bulk_stage");
            
        }

        public StageObjectsJobSpectraS3Request(string bucketName, List<Ds3Object> ds3Objects)
            : this(bucketName, ds3Objects.Select(o => o.Name), Enumerable.Empty<Ds3PartialObject>())
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
            return RequestPayloadUtil.MarshalFullAndPartialObjects(this.FullObjects, this.PartialObjects);
        }

        internal override long GetContentLength()
        {
            return GetContentStream().Length;
        }
    }
}