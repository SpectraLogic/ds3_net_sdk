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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using Ds3.Models;
using Ds3.Runtime;

namespace Ds3.Calls
{
    public class PutObjectRequest : Ds3Request
    {
        private readonly Stream RequestPayload;

        
        public string BucketName { get; private set; }

        public string ObjectName { get; private set; }

        private Checksum _checksum = Checksum.None;
        private Checksum.ChecksumType _checksumType;

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
        private IDictionary<string, string> _metadata = new Dictionary<string, string>();

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
        
        private Guid _job;
        public Guid Job
        {
            get { return _job; }
            set { WithJob(value); }
        }

        public PutObjectRequest WithJob(Guid job)
        {
            this._job = job;
            if (job != null) {
                this.QueryParams.Add("job", Job.ToString());
            }
            else
            {
                this.QueryParams.Remove("job");
            }
            return this;
        }

        private long _offset;
        public long Offset
        {
            get { return _offset; }
            set { WithOffset(value); }
        }

        public PutObjectRequest WithOffset(long offset)
        {
            this._offset = offset;
            if (offset != null) {
                this.QueryParams.Add("offset", Offset.ToString());
            }
            else
            {
                this.QueryParams.Remove("offset");
            }
            return this;
        }

        public PutObjectRequest(string bucketName, string objectName, Stream requestPayload) {
            this.BucketName = bucketName;
            this.ObjectName = objectName;
            this.RequestPayload = requestPayload;
            
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
                return "/" + BucketName + "/" + ObjectName;
            }
        }

        internal override Stream GetContentStream()
        {
            return RequestPayload;
        }
    }
}