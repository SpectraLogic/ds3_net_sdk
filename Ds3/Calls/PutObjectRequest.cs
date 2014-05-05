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
using System.IO;

using Ds3.Models;

namespace Ds3.Calls
{
    public class PutObjectRequest : Ds3Request
    {
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
                return "/" + BucketName + "/" + ObjectName;
            }
        }

        private Stream _content;

        internal override Stream GetContentStream()
        {
            return _content;
        }

        public string BucketName { get; private set; }
        public string ObjectName { get; private set; }
        public Guid JobId { get; private set; }

        [Obsolete]
        public PutObjectRequest(Bucket bucket, string objectName, Stream content)
            : this(bucket.Name, objectName, content)
        {
        }

        [Obsolete]
        public PutObjectRequest(string bucketName, string objectName, Stream content)
        {
            this.BucketName = bucketName;
            this.ObjectName = objectName;
            this._content = content;
        }

        public PutObjectRequest(string bucketName, string objectName, Guid jobId, Stream content)
        {
            this.BucketName = bucketName;
            this.ObjectName = objectName;
            this._content = content;
            this.JobId = jobId;
            QueryParams.Add("job", jobId.ToString());
        }
    }
}
