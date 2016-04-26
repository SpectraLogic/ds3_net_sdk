/*
 * ******************************************************************************
 *   Copyright 2014-2015 Spectra Logic Corporation. All Rights Reserved.
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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Ds3.Calls
{
    public class EjectStorageDomainSpectraS3Request : Ds3Request
    {
        
        public Guid StorageDomainId { get; private set; }

        public List<Ds3Object> Objects { get; private set; }

        
        private Guid _bucketId;
        public Guid BucketId
        {
            get { return _bucketId; }
            set { WithBucketId(value); }
        }

        public EjectStorageDomainSpectraS3Request WithBucketId(Guid bucketId)
        {
            this._bucketId = bucketId;
            if (bucketId != null) {
                this.QueryParams.Add("bucket_id", bucketId.ToString());
            }
            else
            {
                this.QueryParams.Remove("bucket_id");
            }
            return this;
        }

        private string _ejectLabel;
        public string EjectLabel
        {
            get { return _ejectLabel; }
            set { WithEjectLabel(value); }
        }

        public EjectStorageDomainSpectraS3Request WithEjectLabel(string ejectLabel)
        {
            this._ejectLabel = ejectLabel;
            if (ejectLabel != null) {
                this.QueryParams.Add("eject_label", EjectLabel);
            }
            else
            {
                this.QueryParams.Remove("eject_label");
            }
            return this;
        }

        private string _ejectLocation;
        public string EjectLocation
        {
            get { return _ejectLocation; }
            set { WithEjectLocation(value); }
        }

        public EjectStorageDomainSpectraS3Request WithEjectLocation(string ejectLocation)
        {
            this._ejectLocation = ejectLocation;
            if (ejectLocation != null) {
                this.QueryParams.Add("eject_location", EjectLocation);
            }
            else
            {
                this.QueryParams.Remove("eject_location");
            }
            return this;
        }

        public EjectStorageDomainSpectraS3Request(List<Ds3Object> objects, Guid storageDomainId) {
            this.StorageDomainId = storageDomainId;
            this.Objects = objects;
            this.QueryParams.Add("operation", "eject");
            
            this.QueryParams.Add("storage_domain_id", storageDomainId.ToString());

            if (!objects.TrueForAll(obj => obj.Size.HasValue))
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
                return HttpVerb.PUT
            }
        }

        internal override string Path
        {
            get
            {
                return "/_rest_/tape/";
            }
        }
    }
}