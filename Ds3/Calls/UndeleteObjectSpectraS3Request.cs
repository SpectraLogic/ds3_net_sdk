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
    public class UndeleteObjectSpectraS3Request : Ds3Request
    {
        
        public string BucketId { get; private set; }

        public string Name { get; private set; }

        
        private string _versionId;
        public string VersionId
        {
            get { return _versionId; }
            set { WithVersionId(value); }
        }

        
        public UndeleteObjectSpectraS3Request WithVersionId(Guid? versionId)
        {
            this._versionId = versionId.ToString();
            if (versionId != null)
            {
                this.QueryParams.Add("version_id", versionId.ToString());
            }
            else
            {
                this.QueryParams.Remove("version_id");
            }
            return this;
        }

        
        public UndeleteObjectSpectraS3Request WithVersionId(string versionId)
        {
            this._versionId = versionId;
            if (versionId != null)
            {
                this.QueryParams.Add("version_id", versionId);
            }
            else
            {
                this.QueryParams.Remove("version_id");
            }
            return this;
        }


        
        
        public UndeleteObjectSpectraS3Request(Guid bucketId, string name)
        {
            this.BucketId = bucketId.ToString();
            this.Name = name;
            
            this.QueryParams.Add("bucket_id", bucketId.ToString());

            this.QueryParams.Add("name", name);

        }

        
        public UndeleteObjectSpectraS3Request(string bucketId, string name)
        {
            this.BucketId = bucketId;
            this.Name = name;
            
            this.QueryParams.Add("bucket_id", bucketId);

            this.QueryParams.Add("name", name);

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
                return "/_rest_/object";
            }
        }
    }
}