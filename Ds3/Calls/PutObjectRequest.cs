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
using System.Linq;

using Ds3.Models;
using System.Collections.Generic;
using Ds3.Runtime;

namespace Ds3.Calls
{
    public class PutObjectRequest : Ds3Request
    {
        private readonly Stream _content;
        private Checksum _checksum = Checksum.None;
        private IDictionary<string, string> _metadata = new Dictionary<string, string>();

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

        internal override Checksum Md5
        {
            get { return this._checksum; }
        }

        public Checksum Checksum
        {
            get { return this._checksum; }
            set { this.WithChecksum(value); }
        }

        public PutObjectRequest WithChecksum(Checksum checksum)
        {
            this._checksum = checksum;
            return this;
        }

        public IDictionary<string, string> Metadata
        {
            get { return this._metadata; }
            set { this.WithMetadata(value); }
        }

        public PutObjectRequest WithMetadata(IDictionary<string, string> metadata)
        {
            foreach (var key in this.Headers.Keys.Where(key => key.StartsWith(HttpHeaders.AwsMetadataPrefix)).ToList())
            {
                this.Headers.Remove(key);
            }
            foreach (var keyValuePair in metadata)
            {
                this.Headers.Add(HttpHeaders.AwsMetadataPrefix + keyValuePair.Key, keyValuePair.Value);
            }
            this._metadata = metadata;
            return this;
        }

        internal override Stream GetContentStream()
        {
            return _content;
        }

        public string BucketName { get; private set; }
        public string ObjectName { get; private set; }
        public Guid JobId { get; private set; }
        public Guid BlobId { get; private set; }

        [Obsolete]
        public PutObjectRequest(Bucket bucket, string objectName, Stream content)
            : this(bucket.Name, objectName, content)
        {
        }

        [Obsolete]
        public PutObjectRequest(string bucketName, string objectName, Stream content)
            : this(bucketName, objectName, Guid.Empty, Guid.Empty, content)
        {
        }

        [Obsolete]
        public PutObjectRequest(string bucketName, string objectName, Guid jobId, Stream content)
            : this(bucketName, objectName, jobId, Guid.Empty, content)
        {
        }

        public PutObjectRequest(string bucketName, string objectName, Guid jobId, Guid blobId, Stream content)
        {
            this.BucketName = bucketName;
            this.ObjectName = objectName;
            this._content = content;
            this.JobId = jobId;
            this.BlobId = blobId;
            if (jobId != Guid.Empty)
            {
                QueryParams.Add("job", jobId.ToString());
            }
            if (blobId != Guid.Empty)
            {
                QueryParams.Add("id", blobId.ToString());
            }
        }
    }
}
