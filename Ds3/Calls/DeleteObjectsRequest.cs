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
using Ds3.Calls.Util;
using Ds3.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ds3.Calls
{
    public class DeleteObjectsRequest : Ds3Request
    {
        
        public string BucketName { get; private set; }

        public IEnumerable<Ds3Object> Objects { get; private set; }

        

        
        
        public DeleteObjectsRequest(string bucketName, IEnumerable<Ds3Object> objects)
        {
            this.BucketName = bucketName;
            this.Objects = objects.ToList();
            
            this.QueryParams.Add("delete", null);

        }

        internal override Stream GetContentStream()
        {
            return RequestPayloadUtil.MarshalDeleteObjectNames(this.Objects);
        }

        internal override long GetContentLength()
        {
            return GetContentStream().Length;
        }

        internal override ChecksumType ChecksumValue
        {
            get { return ChecksumType.Compute; }
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
                return "/" + BucketName;
            }
        }
    }
}