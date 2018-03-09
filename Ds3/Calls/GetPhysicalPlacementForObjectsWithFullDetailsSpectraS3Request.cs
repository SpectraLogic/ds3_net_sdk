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
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ds3.Calls
{
    public class GetPhysicalPlacementForObjectsWithFullDetailsSpectraS3Request : Ds3Request
    {
        
        public string BucketName { get; private set; }

        public IEnumerable<Ds3Object> Objects { get; private set; }

        
        private string _storageDomain;
        public string StorageDomain
        {
            get { return _storageDomain; }
            set { WithStorageDomain(value); }
        }

        
        public GetPhysicalPlacementForObjectsWithFullDetailsSpectraS3Request WithStorageDomain(string storageDomain)
        {
            this._storageDomain = storageDomain;
            if (storageDomain != null)
            {
                this.QueryParams.Add("storage_domain", storageDomain);
            }
            else
            {
                this.QueryParams.Remove("storage_domain");
            }
            return this;
        }


        
        
        public GetPhysicalPlacementForObjectsWithFullDetailsSpectraS3Request(string bucketName, IEnumerable<Ds3Object> objects)
        {
            this.BucketName = bucketName;
            this.Objects = objects.ToList();
            this.QueryParams.Add("operation", "get_physical_placement");
            
            this.QueryParams.Add("full_details", null);

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
                return "/_rest_/bucket/" + BucketName;
            }
        }

        internal override Stream GetContentStream()
        {
            return RequestPayloadUtil.MarshalDs3ObjectNames(this.Objects);
        }

        internal override long GetContentLength()
        {
            return GetContentStream().Length;
        }
    }
}