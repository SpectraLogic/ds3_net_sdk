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
    public class ListMultiPartUploadPartsRequest : Ds3Request
    {
        
        public string BucketName { get; private set; }

        public string ObjectName { get; private set; }

        public string UploadId { get; private set; }

        
        private int? _maxParts;
        public int? MaxParts
        {
            get { return _maxParts; }
            set { WithMaxParts(value); }
        }

        private int? _partNumberMarker;
        public int? PartNumberMarker
        {
            get { return _partNumberMarker; }
            set { WithPartNumberMarker(value); }
        }

        public ListMultiPartUploadPartsRequest WithMaxParts(int? maxParts)
        {
            this._maxParts = maxParts;
            if (maxParts != null) {
                this.QueryParams.Add("max_parts", maxParts.ToString());
            }
            else
            {
                this.QueryParams.Remove("max_parts");
            }
            return this;
        }
        public ListMultiPartUploadPartsRequest WithPartNumberMarker(int? partNumberMarker)
        {
            this._partNumberMarker = partNumberMarker;
            if (partNumberMarker != null) {
                this.QueryParams.Add("part_number_marker", partNumberMarker.ToString());
            }
            else
            {
                this.QueryParams.Remove("part_number_marker");
            }
            return this;
        }

        
        public ListMultiPartUploadPartsRequest(string bucketName, string objectName, Guid uploadId) {
            this.BucketName = bucketName;
            this.ObjectName = objectName;
            this.UploadId = uploadId.ToString();
            
            this.QueryParams.Add("upload_id", uploadId.ToString());

        }

        public ListMultiPartUploadPartsRequest(string bucketName, string objectName, string uploadId) {
            this.BucketName = bucketName;
            this.ObjectName = objectName;
            this.UploadId = uploadId;
            
            this.QueryParams.Add("upload_id", uploadId);

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
                return "/" + BucketName + "/" + ObjectName;
            }
        }
    }
}