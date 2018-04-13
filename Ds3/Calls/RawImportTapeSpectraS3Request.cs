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
using Ds3.Models;
using System;
using System.Net;

namespace Ds3.Calls
{
    public class RawImportTapeSpectraS3Request : Ds3Request
    {
        
        public string TapeId { get; private set; }

        public string BucketId { get; private set; }

        
        private string _storageDomainId;
        public string StorageDomainId
        {
            get { return _storageDomainId; }
            set { WithStorageDomainId(value); }
        }

        private Priority? _taskPriority;
        public Priority? TaskPriority
        {
            get { return _taskPriority; }
            set { WithTaskPriority(value); }
        }

        
        public RawImportTapeSpectraS3Request WithStorageDomainId(Guid? storageDomainId)
        {
            this._storageDomainId = storageDomainId.ToString();
            if (storageDomainId != null)
            {
                this.QueryParams.Add("storage_domain_id", storageDomainId.ToString());
            }
            else
            {
                this.QueryParams.Remove("storage_domain_id");
            }
            return this;
        }

        
        public RawImportTapeSpectraS3Request WithStorageDomainId(string storageDomainId)
        {
            this._storageDomainId = storageDomainId;
            if (storageDomainId != null)
            {
                this.QueryParams.Add("storage_domain_id", storageDomainId);
            }
            else
            {
                this.QueryParams.Remove("storage_domain_id");
            }
            return this;
        }

        
        public RawImportTapeSpectraS3Request WithTaskPriority(Priority? taskPriority)
        {
            this._taskPriority = taskPriority;
            if (taskPriority != null)
            {
                this.QueryParams.Add("task_priority", taskPriority.ToString());
            }
            else
            {
                this.QueryParams.Remove("task_priority");
            }
            return this;
        }


        
        
        public RawImportTapeSpectraS3Request(Guid bucketId, Guid tapeId)
        {
            this.TapeId = tapeId.ToString();
            this.BucketId = bucketId.ToString();
            this.QueryParams.Add("operation", "import");
            
            this.QueryParams.Add("bucket_id", bucketId.ToString());

        }

        
        public RawImportTapeSpectraS3Request(string bucketId, string tapeId)
        {
            this.TapeId = tapeId;
            this.BucketId = bucketId;
            this.QueryParams.Add("operation", "import");
            
            this.QueryParams.Add("bucket_id", bucketId);

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
                return "/_rest_/tape/" + TapeId;
            }
        }
    }
}