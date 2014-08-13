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

using System.Linq;
using System.Collections.Generic;

using Ds3.Runtime;

namespace Ds3.Calls
{
    public class PutBucketRequest : Ds3Request
    {
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
                return "/" + BucketName;
            }
        }

        public string BucketName { get; private set; }

        public IDictionary<string, string> Metadata
        {
            get { return this._metadata; }
            set { this.WithMetadata(value); }
        }

        public PutBucketRequest WithMetadata(IDictionary<string, string> metadata)
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

        public PutBucketRequest(string bucketName)
        {
            this.BucketName = bucketName;
        }
    }
}
