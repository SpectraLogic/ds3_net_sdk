/*
 * ******************************************************************************
 *   Copyright 2014 Spectra Logic Corporation. All Rights Reserved.
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

using System;

namespace Ds3.Calls
{
    public class InitiateMultipartUploadRequest : Ds3Request
    {
        //TODO: metadata
        public string BucketName { get; private set; }
        public string ObjectName { get; private set; }
        public Guid JobId { get; private set; }
        public long Offset { get; private set; }

        public InitiateMultipartUploadRequest(string bucketName, string objectName)
        {
            this.BucketName = bucketName;
            this.ObjectName = objectName;
            this.QueryParams.Add("uploads", "");
        }

        public InitiateMultipartUploadRequest(string bucketName, string objectName, Guid jobId, long offset)
            : this(bucketName, objectName)
        {
            this.JobId = jobId;
            this.Offset = offset;
            if (this.JobId != Guid.Empty)
            {
                this.QueryParams.Add("job", jobId.ToString());
                this.QueryParams.Add("offset", offset.ToString());
            }
        }

        internal override HttpVerb Verb
        {
            get { return HttpVerb.POST; }
        }

        internal override string Path
        {
            get { return "/" + BucketName + "/" + ObjectName; }
        }
    }
}
