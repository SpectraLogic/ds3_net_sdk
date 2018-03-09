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
    public class EjectStorageDomainBlobsSpectraS3Request : Ds3Request
    {
        
        public string BucketId { get; private set; }

        public string StorageDomain { get; private set; }

        public IEnumerable<Ds3Object> Objects { get; private set; }

        
        private string _ejectLabel;
        public string EjectLabel
        {
            get { return _ejectLabel; }
            set { WithEjectLabel(value); }
        }

        private string _ejectLocation;
        public string EjectLocation
        {
            get { return _ejectLocation; }
            set { WithEjectLocation(value); }
        }

        
        public EjectStorageDomainBlobsSpectraS3Request WithEjectLabel(string ejectLabel)
        {
            this._ejectLabel = ejectLabel;
            if (ejectLabel != null)
            {
                this.QueryParams.Add("eject_label", ejectLabel);
            }
            else
            {
                this.QueryParams.Remove("eject_label");
            }
            return this;
        }

        
        public EjectStorageDomainBlobsSpectraS3Request WithEjectLocation(string ejectLocation)
        {
            this._ejectLocation = ejectLocation;
            if (ejectLocation != null)
            {
                this.QueryParams.Add("eject_location", ejectLocation);
            }
            else
            {
                this.QueryParams.Remove("eject_location");
            }
            return this;
        }


        
        
        public EjectStorageDomainBlobsSpectraS3Request(Guid bucketId, IEnumerable<Ds3Object> objects, string storageDomain)
        {
            this.BucketId = bucketId.ToString();
            this.StorageDomain = storageDomain;
            this.Objects = objects.ToList();
            this.QueryParams.Add("operation", "eject");
            
            this.QueryParams.Add("blobs", null);

            this.QueryParams.Add("bucket_id", bucketId.ToString());

            this.QueryParams.Add("storage_domain", storageDomain);

        }

        
        public EjectStorageDomainBlobsSpectraS3Request(string bucketId, IEnumerable<Ds3Object> objects, string storageDomain)
        {
            this.BucketId = bucketId;
            this.StorageDomain = storageDomain;
            this.Objects = objects.ToList();
            this.QueryParams.Add("operation", "eject");
            
            this.QueryParams.Add("blobs", null);

            this.QueryParams.Add("bucket_id", bucketId);

            this.QueryParams.Add("storage_domain", storageDomain);

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
                return "/_rest_/tape";
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