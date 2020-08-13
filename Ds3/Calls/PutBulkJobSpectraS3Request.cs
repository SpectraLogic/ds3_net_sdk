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
using System.Collections.Generic;
using System.IO;
using System.Linq;

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

        private bool? _force;
        public bool? Force
        {
            get { return _force; }
            set { WithForce(value); }
        }

        private bool? _ignoreNamingConflicts;
        public bool? IgnoreNamingConflicts
        {
            get { return _ignoreNamingConflicts; }
            set { WithIgnoreNamingConflicts(value); }
        }

        private bool? _implicitJobIdResolution;
        public bool? ImplicitJobIdResolution
        {
            get { return _implicitJobIdResolution; }
            set { WithImplicitJobIdResolution(value); }
        }

        private bool? _minimizeSpanningAcrossMedia;
        public bool? MinimizeSpanningAcrossMedia
        {
            get { return _minimizeSpanningAcrossMedia; }
            set { WithMinimizeSpanningAcrossMedia(value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { WithName(value); }
        }

        private bool? _preAllocateJobSpace;
        public bool? PreAllocateJobSpace
        {
            get { return _preAllocateJobSpace; }
            set { WithPreAllocateJobSpace(value); }
        }

        private Priority? _priority;
        public Priority? Priority
        {
            get { return _priority; }
            set { WithPriority(value); }
        }

        private bool? _verifyAfterWrite;
        public bool? VerifyAfterWrite
        {
            get { return _verifyAfterWrite; }
            set { WithVerifyAfterWrite(value); }
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

        
        public PutBulkJobSpectraS3Request WithForce(bool? force)
        {
            this._force = force;
            if (force != null)
            {
                this.QueryParams.Add("force", force.ToString());
            }
            else
            {
                this.QueryParams.Remove("force");
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

        
        public PutBulkJobSpectraS3Request WithImplicitJobIdResolution(bool? implicitJobIdResolution)
        {
            this._implicitJobIdResolution = implicitJobIdResolution;
            if (implicitJobIdResolution != null)
            {
                this.QueryParams.Add("implicit_job_id_resolution", implicitJobIdResolution.ToString());
            }
            else
            {
                this.QueryParams.Remove("implicit_job_id_resolution");
            }
            return this;
        }

        
        public PutBulkJobSpectraS3Request WithMinimizeSpanningAcrossMedia(bool? minimizeSpanningAcrossMedia)
        {
            this._minimizeSpanningAcrossMedia = minimizeSpanningAcrossMedia;
            if (minimizeSpanningAcrossMedia != null)
            {
                this.QueryParams.Add("minimize_spanning_across_media", minimizeSpanningAcrossMedia.ToString());
            }
            else
            {
                this.QueryParams.Remove("minimize_spanning_across_media");
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

        
        public PutBulkJobSpectraS3Request WithPreAllocateJobSpace(bool? preAllocateJobSpace)
        {
            this._preAllocateJobSpace = preAllocateJobSpace;
            if (preAllocateJobSpace != null)
            {
                this.QueryParams.Add("pre_allocate_job_space", preAllocateJobSpace.ToString());
            }
            else
            {
                this.QueryParams.Remove("pre_allocate_job_space");
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

        
        public PutBulkJobSpectraS3Request WithVerifyAfterWrite(bool? verifyAfterWrite)
        {
            this._verifyAfterWrite = verifyAfterWrite;
            if (verifyAfterWrite != null)
            {
                this.QueryParams.Add("verify_after_write", verifyAfterWrite.ToString());
            }
            else
            {
                this.QueryParams.Remove("verify_after_write");
            }
            return this;
        }


        
        
        public PutBulkJobSpectraS3Request(string bucketName, IEnumerable<Ds3Object> objects)
        {
            this.BucketName = bucketName;
            this.Objects = objects.ToList();
            this.QueryParams.Add("operation", "start_bulk_put");
            
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
            return RequestPayloadUtil.MarshalDs3ObjectNameAndSize(this.Objects);
        }

        internal override long GetContentLength()
        {
            return GetContentStream().Length;
        }
    }
}