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
using System.Net;

namespace Ds3.Calls
{
    public class AbortMultiPartUploadRequest : Ds3Request
    {
        
        public string BucketName { get; private set; }

        public string ObjectName { get; private set; }

        public Guid UploadId { get; private set; }

        
        public AbortMultiPartUploadRequest(string bucketName, string objectName, Guid uploadId) {
            this.BucketName = bucketName;
            this.ObjectName = objectName;
            this.UploadId = uploadId;
            
            this.QueryParams.Add("upload_id", uploadId.ToString());

        }

        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.DELETE
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