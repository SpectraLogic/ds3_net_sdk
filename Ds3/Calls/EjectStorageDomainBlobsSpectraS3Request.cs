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
using Ds3.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Ds3.Calls
{
    public class EjectStorageDomainBlobsSpectraS3Request : Ds3Request
    {
        
        public string BucketId { get; private set; }

        public string StorageDomainId { get; private set; }

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
            if (ejectLabel != null) {
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
            if (ejectLocation != null) {
                this.QueryParams.Add("eject_location", ejectLocation);
            }
            else
            {
                this.QueryParams.Remove("eject_location");
            }
            return this;
        }

        public EjectStorageDomainBlobsSpectraS3Request(Guid bucketId, IEnumerable<Ds3Object> objects, Guid storageDomainId) {
            this.BucketId = bucketId.ToString();
            this.StorageDomainId = storageDomainId.ToString();
            this.Objects = objects.ToList();
            this.QueryParams.Add("operation", "eject");
            
            this.QueryParams.Add("blobs", null);

            this.QueryParams.Add("bucket_id", bucketId.ToString());

            this.QueryParams.Add("storage_domain_id", storageDomainId.ToString());

            if (!objects.ToList().TrueForAll(obj => obj.Size.HasValue))
            {
                throw new Ds3RequestException(Resources.ObjectsMissingSizeException);
            }
        }
        public EjectStorageDomainBlobsSpectraS3Request(string bucketId, IEnumerable<Ds3Object> objects, string storageDomainId) {
            this.BucketId = bucketId;
            this.StorageDomainId = storageDomainId;
            this.Objects = objects.ToList();
            this.QueryParams.Add("operation", "eject");
            
            this.QueryParams.Add("blobs", null);

            this.QueryParams.Add("bucket_id", bucketId);

            this.QueryParams.Add("storage_domain_id", storageDomainId);

            if (!objects.ToList().TrueForAll(obj => obj.Size.HasValue))
            {
                throw new Ds3RequestException(Resources.ObjectsMissingSizeException);
            }
        }

        internal override Stream GetContentStream()
        {
            return new XDocument()
                .AddFluent(
                    new XElement("Objects").AddAllFluent(
                        from obj in this.Objects
                        select new XElement("Object")
                            .SetAttributeValueFluent("Name", obj.Name)
                            .SetAttributeValueFluent("Size", obj.Size.Value.ToString("D"))
                    )
                )
                .WriteToMemoryStream();
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
    }
}