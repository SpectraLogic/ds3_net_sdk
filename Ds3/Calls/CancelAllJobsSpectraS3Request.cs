/*
 * ******************************************************************************
 *   Copyright 2014-2015 Spectra Logic Corporation. All Rights Reserved.
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
    public class CancelAllJobsSpectraS3Request : Ds3Request
    {
        
        
        private Guid _bucketId;
        public Guid BucketId
        {
            get { return _bucketId; }
            set { WithBucketId(value); }
        }

        public CancelAllJobsSpectraS3Request WithBucketId(Guid bucketId)
        {
            this._bucketId = bucketId;
            if (bucketId != null) {
                this.QueryParams.Add("bucket_id", BucketId.ToString());
            }
            else
            {
                this.QueryParams.Remove("bucket_id");
            }
            return this;
        }

        private bool _force;
        public bool Force
        {
            get { return _force; }
            set { WithForce(value); }
        }

        public CancelAllJobsSpectraS3Request WithForce(bool force)
        {
            this._force = force;
            if (force != null) {
                this.QueryParams.Add("force", Force.ToString());
            }
            else
            {
                this.QueryParams.Remove("force");
            }
            return this;
        }

        private JobRequestType _requestType;
        public JobRequestType RequestType
        {
            get { return _requestType; }
            set { WithRequestType(value); }
        }

        public CancelAllJobsSpectraS3Request WithRequestType(JobRequestType requestType)
        {
            this._requestType = requestType;
            if (requestType != null) {
                this.QueryParams.Add("request_type", RequestType.ToString());
            }
            else
            {
                this.QueryParams.Remove("request_type");
            }
            return this;
        }

        public CancelAllJobsSpectraS3Request() {
            
        }

        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.DELETE;
            }
        }

        internal override string Path
        {
            get
            {
                return "/_rest_/job/";
            }
        }
    }
}