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
    public class GetBucketRequest : Ds3Request
    {
        
        public string BucketName { get; private set; }

        
        private string _delimiter;
        public string Delimiter
        {
            get { return _delimiter; }
            set { WithDelimiter(value); }
        }

        private string _marker;
        public string Marker
        {
            get { return _marker; }
            set { WithMarker(value); }
        }

        private int? _maxKeys;
        public int? MaxKeys
        {
            get { return _maxKeys; }
            set { WithMaxKeys(value); }
        }

        private string _prefix;
        public string Prefix
        {
            get { return _prefix; }
            set { WithPrefix(value); }
        }

        public GetBucketRequest WithDelimiter(string delimiter)
        {
            this._delimiter = delimiter;
            if (delimiter != null) {
                this.QueryParams.Add("delimiter", delimiter);
            }
            else
            {
                this.QueryParams.Remove("delimiter");
            }
            return this;
        }
        public GetBucketRequest WithMarker(string marker)
        {
            this._marker = marker;
            if (marker != null) {
                this.QueryParams.Add("marker", marker);
            }
            else
            {
                this.QueryParams.Remove("marker");
            }
            return this;
        }
        public GetBucketRequest WithMaxKeys(int? maxKeys)
        {
            this._maxKeys = maxKeys;
            if (maxKeys != null) {
                this.QueryParams.Add("max_keys", maxKeys.ToString());
            }
            else
            {
                this.QueryParams.Remove("max_keys");
            }
            return this;
        }
        public GetBucketRequest WithPrefix(string prefix)
        {
            this._prefix = prefix;
            if (prefix != null) {
                this.QueryParams.Add("prefix", prefix);
            }
            else
            {
                this.QueryParams.Remove("prefix");
            }
            return this;
        }

        
        public GetBucketRequest(string bucketName)
        {
            this.BucketName = bucketName;
            
        }

        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.GET;
            }
        }

        internal override string Path
        {
            get
            {
                return "/" + BucketName;
            }
        }
    }
}