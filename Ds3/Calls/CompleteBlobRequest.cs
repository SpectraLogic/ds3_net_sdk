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
    public class CompleteBlobRequest : Ds3Request
    {
        
        public string BucketName { get; private set; }

        public string ObjectName { get; private set; }

        public string Blob { get; private set; }

        public string Job { get; private set; }

        
        private long? _size;
        public long? Size
        {
            get { return _size; }
            set { WithSize(value); }
        }

        
        public CompleteBlobRequest WithSize(long? size)
        {
            this._size = size;
            if (size != null)
            {
                this.QueryParams.Add("size", size.ToString());
            }
            else
            {
                this.QueryParams.Remove("size");
            }
            return this;
        }


        
        
        public CompleteBlobRequest(string bucketName, string objectName, Guid blob, Guid job)
        {
            this.BucketName = bucketName;
            this.ObjectName = objectName;
            this.Blob = blob.ToString();
            this.Job = job.ToString();
            
            this.QueryParams.Add("blob", blob.ToString());

            this.QueryParams.Add("job", job.ToString());

        }

        
        public CompleteBlobRequest(string bucketName, string objectName, string blob, string job)
        {
            this.BucketName = bucketName;
            this.ObjectName = objectName;
            this.Blob = blob;
            this.Job = job;
            
            this.QueryParams.Add("blob", blob);

            this.QueryParams.Add("job", job);

        }

        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.POST;
            }
        }

        internal override string Path
        {
            get
            {
                return "/" + BucketName + "/" + ObjectName;
            }
        }
    }
}