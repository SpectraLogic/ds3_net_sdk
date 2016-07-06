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
using System;
using System.Net;

namespace Ds3.Calls
{
    public class DeleteBucketSpectraS3Request : Ds3Request
    {
        
        public string BucketName { get; private set; }

        
        private bool? _force;
        public bool? Force
        {
            get { return _force; }
            set { WithForce(value); }
        }

        private bool? _replicate;
        public bool? Replicate
        {
            get { return _replicate; }
            set { WithReplicate(value); }
        }

        public DeleteBucketSpectraS3Request WithForce(bool? force)
        {
            this._force = force;
            if (force != null) {
                this.QueryParams.Add("force", force.ToString());
            }
            else
            {
                this.QueryParams.Remove("force");
            }
            return this;
        }
        public DeleteBucketSpectraS3Request WithReplicate(bool? replicate)
        {
            this._replicate = replicate;
            if (replicate != null) {
                this.QueryParams.Add("replicate", replicate.ToString());
            }
            else
            {
                this.QueryParams.Remove("replicate");
            }
            return this;
        }

        
        public DeleteBucketSpectraS3Request(string bucketName)
        {
            this.BucketName = bucketName;
            
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
                return "/_rest_/bucket/" + BucketName;
            }
        }
    }
}