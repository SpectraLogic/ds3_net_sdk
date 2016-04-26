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
using System.Net;

namespace Ds3.Calls
{
    public class PutPoolStorageDomainMemberSpectraS3Request : Ds3Request
    {
        
        public Guid PoolPartitionId { get; private set; }

        public Guid StorageDomainId { get; private set; }

        
        private WritePreferenceLevel _writePreference;
        public WritePreferenceLevel WritePreference
        {
            get { return _writePreference; }
            set { WithWritePreference(value); }
        }

        public PutPoolStorageDomainMemberSpectraS3Request WithWritePreference(WritePreferenceLevel writePreference)
        {
            this._writePreference = writePreference;
            if (writePreference != null) {
                this.QueryParams.Add("write_preference", writePreference.ToString());
            }
            else
            {
                this.QueryParams.Remove("write_preference");
            }
            return this;
        }

        public PutPoolStorageDomainMemberSpectraS3Request(Guid poolPartitionId, Guid storageDomainId) {
            this.PoolPartitionId = poolPartitionId;
            this.StorageDomainId = storageDomainId;
            
            this.QueryParams.Add("pool_partition_id", poolPartitionId.ToString());

            this.QueryParams.Add("storage_domain_id", storageDomainId.ToString());

        }

        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.POST
            }
        }

        internal override string Path
        {
            get
            {
                return "/_rest_/storage_domain_member/";
            }
        }
    }
}