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
    public class VerifyPhysicalPlacementForObjectsWithFullDetailsSpectraS3Request : Ds3Request
    {
        
        public string BucketName { get; private set; }

        public List<Ds3Object> Objects { get; private set; }

        
        private Guid _storageDomainId;
        public Guid StorageDomainId
        {
            get { return _storageDomainId; }
            set { WithStorageDomainId(value); }
        }

        public VerifyPhysicalPlacementForObjectsWithFullDetailsSpectraS3Request WithStorageDomainId(Guid storageDomainId)
        {
            this._storageDomainId = storageDomainId;
            if (storageDomainId != null) {
                this.QueryParams.Add("storage_domain_id", storageDomainId.ToString());
            }
            else
            {
                this.QueryParams.Remove("storage_domain_id");
            }
            return this;
        }

        public VerifyPhysicalPlacementForObjectsWithFullDetailsSpectraS3Request(string bucketName, List<Ds3Object> objects) {
            this.BucketName = bucketName;
            this.Objects = objects;
            this.QueryParams.Add("operation", "verify_physical_placement");
            
            this.QueryParams.Add("full_details", null);

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
                return HttpVerb.GET
            }
        }

        internal override string Path
        {
            get
            {
                return "/_rest_/bucket/" + BucketName;
            }
        }
    }
}