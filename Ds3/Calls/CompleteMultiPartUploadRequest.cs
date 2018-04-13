/*
 * ******************************************************************************
 *   Copyright 2014-2017 Spectra Logic Corporation. All Rights Reserved.
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
using System.Collections.Generic;
using System.IO;
using Ds3.Calls.Util;
using System.Linq;

namespace Ds3.Calls
{
    public class CompleteMultiPartUploadRequest : Ds3Request
    {
        public IEnumerable<Part> Parts { get; private set; }
        
        public string BucketName { get; private set; }

        public string ObjectName { get; private set; }

        public string UploadId { get; private set; }

        

        
        
        public CompleteMultiPartUploadRequest(string bucketName, string objectName, IEnumerable<Part> parts, Guid uploadId)
        {
            this.BucketName = bucketName;
            this.ObjectName = objectName;
            this.UploadId = uploadId.ToString();
            this.Parts = parts.ToList();
            
            this.QueryParams.Add("upload_id", uploadId.ToString());

        }

        
        public CompleteMultiPartUploadRequest(string bucketName, string objectName, IEnumerable<Part> parts, string uploadId)
        {
            this.BucketName = bucketName;
            this.ObjectName = objectName;
            this.UploadId = uploadId;
            this.Parts = parts.ToList();
            
            this.QueryParams.Add("upload_id", uploadId);

        }

        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.POST;
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
            return RequestPayloadUtil.MarshalPartsToStream(Parts);
        }

        internal override long GetContentLength()
        {
            return GetContentStream().Length;
        }
    }
}