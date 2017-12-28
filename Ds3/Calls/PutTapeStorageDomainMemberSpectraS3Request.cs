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
using System.Net;

namespace Ds3.Calls
{
    public class PutTapeStorageDomainMemberSpectraS3Request : Ds3Request
    {
        
        public string StorageDomainId { get; private set; }

        public string TapePartitionId { get; private set; }

        public string TapeType { get; private set; }

        
        private WritePreferenceLevel? _writePreference;
        public WritePreferenceLevel? WritePreference
        {
            get { return _writePreference; }
            set { WithWritePreference(value); }
        }

        
        public PutTapeStorageDomainMemberSpectraS3Request WithWritePreference(WritePreferenceLevel? writePreference)
        {
            this._writePreference = writePreference;
            if (writePreference != null)
            {
                this.QueryParams.Add("write_preference", writePreference.ToString());
            }
            else
            {
                this.QueryParams.Remove("write_preference");
            }
            return this;
        }


        
        
        public PutTapeStorageDomainMemberSpectraS3Request(Guid storageDomainId, Guid tapePartitionId, string tapeType)
        {
            this.StorageDomainId = storageDomainId.ToString();
            this.TapePartitionId = tapePartitionId.ToString();
            this.TapeType = tapeType;
            
            this.QueryParams.Add("storage_domain_id", storageDomainId.ToString());

            this.QueryParams.Add("tape_partition_id", tapePartitionId.ToString());

            this.QueryParams.Add("tape_type", tapeType);

        }

        
        public PutTapeStorageDomainMemberSpectraS3Request(string storageDomainId, string tapePartitionId, string tapeType)
        {
            this.StorageDomainId = storageDomainId;
            this.TapePartitionId = tapePartitionId;
            this.TapeType = tapeType;
            
            this.QueryParams.Add("storage_domain_id", storageDomainId);

            this.QueryParams.Add("tape_partition_id", tapePartitionId);

            this.QueryParams.Add("tape_type", tapeType);

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
                return "/_rest_/storage_domain_member";
            }
        }
    }
}