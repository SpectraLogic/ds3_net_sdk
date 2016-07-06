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
    public class ListMultiPartUploadsRequest : Ds3Request
    {
        
        public string BucketName { get; private set; }

        
        private string _delimiter;
        public string Delimiter
        {
            get { return _delimiter; }
            set { WithDelimiter(value); }
        }

        private string _keyMarker;
        public string KeyMarker
        {
            get { return _keyMarker; }
            set { WithKeyMarker(value); }
        }

        private int? _maxUploads;
        public int? MaxUploads
        {
            get { return _maxUploads; }
            set { WithMaxUploads(value); }
        }

        private string _prefix;
        public string Prefix
        {
            get { return _prefix; }
            set { WithPrefix(value); }
        }

        private string _uploadIdMarker;
        public string UploadIdMarker
        {
            get { return _uploadIdMarker; }
            set { WithUploadIdMarker(value); }
        }

        public ListMultiPartUploadsRequest WithDelimiter(string delimiter)
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
        public ListMultiPartUploadsRequest WithKeyMarker(string keyMarker)
        {
            this._keyMarker = keyMarker;
            if (keyMarker != null) {
                this.QueryParams.Add("key_marker", keyMarker);
            }
            else
            {
                this.QueryParams.Remove("key_marker");
            }
            return this;
        }
        public ListMultiPartUploadsRequest WithMaxUploads(int? maxUploads)
        {
            this._maxUploads = maxUploads;
            if (maxUploads != null) {
                this.QueryParams.Add("max_uploads", maxUploads.ToString());
            }
            else
            {
                this.QueryParams.Remove("max_uploads");
            }
            return this;
        }
        public ListMultiPartUploadsRequest WithPrefix(string prefix)
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
        public ListMultiPartUploadsRequest WithUploadIdMarker(string uploadIdMarker)
        {
            this._uploadIdMarker = uploadIdMarker;
            if (uploadIdMarker != null) {
                this.QueryParams.Add("upload_id_marker", uploadIdMarker);
            }
            else
            {
                this.QueryParams.Remove("upload_id_marker");
            }
            return this;
        }

        
        public ListMultiPartUploadsRequest(string bucketName)
        {
            this.BucketName = bucketName;
            
            this.QueryParams.Add("uploads", null);

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