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
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Ds3.Models;
using Ds3.Runtime;
using Ds3.Helpers.Streams;

namespace Ds3.Calls
{
    public class PutObjectRequest : Ds3Request
    {
        private readonly Stream _content;
        private Checksum _checksum = Checksum.None;
        private Checksum.ChecksumType _checksumType;
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

        internal override Checksum ChecksumValue
        {
            get { return this._checksum; }
        }

        internal override Checksum.ChecksumType ChecksumType
        {
            get { return this._checksumType; }
        }

        public Checksum Checksum
        {
            get { return this._checksum; }
            set { this.WithChecksum(value); }
        }

        public PutObjectRequest WithChecksum(Checksum checksum, Checksum.ChecksumType checksumType = Checksum.ChecksumType.Md5)
        {
            this._checksum = checksum;
            this._checksumType = checksumType;
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
        public long Offset { get; private set; }

        private void SetParameters(string bucketName, string objectName, Guid jobId, long offset)
        {
            this.BucketName = bucketName;
            this.ObjectName = objectName;
            this.JobId = jobId;
            this.Offset = offset;

            if (jobId != Guid.Empty)
            {
                QueryParams.Add("job", jobId.ToString());
                QueryParams.Add("offset", offset.ToString());
            }
        }

        public PutObjectRequest(string bucketName, string objectName, Guid jobId, long offset, Stream content, long length)
        {
            SetParameters(bucketName, objectName, jobId, offset);

            this._content = new PutObjectRequestStream(content, Offset, length);
        }

        public PutObjectRequest(string bucketName, string objectName, Guid jobId, long offset, Stream content)
        {
            SetParameters(bucketName, objectName, jobId, offset);

            this._content = content;
        }
    }
}
