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
    public class VerifyPhysicalPlacementForObjectsWithFullDetailsSpectraS3Request : Ds3Request
    {
        
        public string BucketName { get; private set; }

        public IEnumerable<Ds3Object> FullObjects { get; private set; }

        public IEnumerable<Ds3PartialObject> PartialObjects { get; private set; }

        
        private string _storageDomain;
        public string StorageDomain
        {
            get { return _storageDomain; }
            set { WithStorageDomain(value); }
        }

        
        public VerifyPhysicalPlacementForObjectsWithFullDetailsSpectraS3Request WithStorageDomain(string storageDomain)
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


        
        
        public VerifyPhysicalPlacementForObjectsWithFullDetailsSpectraS3Request(string bucketName, IEnumerable<Ds3Object> fullObjects, IEnumerable<Ds3PartialObject> partialObjects)
        {
            this.BucketName = bucketName;
            this.FullObjects = fullObjects.ToList();
            this.PartialObjects = partialObjects.ToList();
            this.QueryParams.Add("operation", "verify_physical_placement");
            
            this.QueryParams.Add("full_details", null);

        }

        public VerifyPhysicalPlacementForObjectsWithFullDetailsSpectraS3Request(string bucketName, IEnumerable<Ds3Object> ds3Objects)
            : this(bucketName, ds3Objects, Enumerable.Empty<Ds3PartialObject>())
        {
        }

        public VerifyPhysicalPlacementForObjectsWithFullDetailsSpectraS3Request(string bucketName, IEnumerable<string> objectNames)
            : this(bucketName, objectNames.Select(name => new Ds3Object(name, null)), Enumerable.Empty<Ds3PartialObject>())
        {
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
                return "/_rest_/bucket/" + BucketName;
            }
        }

        internal override Stream GetContentStream()
        {
            return RequestPayloadUtil.MarshalFullAndPartialObjects(this.FullObjects, this.PartialObjects);
        }

        internal override long GetContentLength()
        {
            return GetContentStream().Length;
        }
    }
}