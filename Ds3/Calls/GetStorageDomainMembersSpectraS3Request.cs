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
    public class GetStorageDomainMembersSpectraS3Request : Ds3Request
    {
        
        
        private bool? _lastPage;
        public bool? LastPage
        {
            get { return _lastPage; }
            set { WithLastPage(value); }
        }

        private int? _pageLength;
        public int? PageLength
        {
            get { return _pageLength; }
            set { WithPageLength(value); }
        }

        private int? _pageOffset;
        public int? PageOffset
        {
            get { return _pageOffset; }
            set { WithPageOffset(value); }
        }

        private string _pageStartMarker;
        public string PageStartMarker
        {
            get { return _pageStartMarker; }
            set { WithPageStartMarker(value); }
        }

        private string _poolPartitionId;
        public string PoolPartitionId
        {
            get { return _poolPartitionId; }
            set { WithPoolPartitionId(value); }
        }

        private StorageDomainMemberState? _state;
        public StorageDomainMemberState? State
        {
            get { return _state; }
            set { WithState(value); }
        }

        private string _storageDomainId;
        public string StorageDomainId
        {
            get { return _storageDomainId; }
            set { WithStorageDomainId(value); }
        }

        private string _tapePartitionId;
        public string TapePartitionId
        {
            get { return _tapePartitionId; }
            set { WithTapePartitionId(value); }
        }

        private string _tapeType;
        public string TapeType
        {
            get { return _tapeType; }
            set { WithTapeType(value); }
        }

        private WritePreferenceLevel? _writePreference;
        public WritePreferenceLevel? WritePreference
        {
            get { return _writePreference; }
            set { WithWritePreference(value); }
        }

        
        public GetStorageDomainMembersSpectraS3Request WithLastPage(bool? lastPage)
        {
            this._lastPage = lastPage;
            if (lastPage != null)
            {
                this.QueryParams.Add("last_page", lastPage.ToString());
            }
            else
            {
                this.QueryParams.Remove("last_page");
            }
            return this;
        }

        
        public GetStorageDomainMembersSpectraS3Request WithPageLength(int? pageLength)
        {
            this._pageLength = pageLength;
            if (pageLength != null)
            {
                this.QueryParams.Add("page_length", pageLength.ToString());
            }
            else
            {
                this.QueryParams.Remove("page_length");
            }
            return this;
        }

        
        public GetStorageDomainMembersSpectraS3Request WithPageOffset(int? pageOffset)
        {
            this._pageOffset = pageOffset;
            if (pageOffset != null)
            {
                this.QueryParams.Add("page_offset", pageOffset.ToString());
            }
            else
            {
                this.QueryParams.Remove("page_offset");
            }
            return this;
        }

        
        public GetStorageDomainMembersSpectraS3Request WithPageStartMarker(Guid? pageStartMarker)
        {
            this._pageStartMarker = pageStartMarker.ToString();
            if (pageStartMarker != null)
            {
                this.QueryParams.Add("page_start_marker", pageStartMarker.ToString());
            }
            else
            {
                this.QueryParams.Remove("page_start_marker");
            }
            return this;
        }

        
        public GetStorageDomainMembersSpectraS3Request WithPageStartMarker(string pageStartMarker)
        {
            this._pageStartMarker = pageStartMarker;
            if (pageStartMarker != null)
            {
                this.QueryParams.Add("page_start_marker", pageStartMarker);
            }
            else
            {
                this.QueryParams.Remove("page_start_marker");
            }
            return this;
        }

        
        public GetStorageDomainMembersSpectraS3Request WithPoolPartitionId(Guid? poolPartitionId)
        {
            this._poolPartitionId = poolPartitionId.ToString();
            if (poolPartitionId != null)
            {
                this.QueryParams.Add("pool_partition_id", poolPartitionId.ToString());
            }
            else
            {
                this.QueryParams.Remove("pool_partition_id");
            }
            return this;
        }

        
        public GetStorageDomainMembersSpectraS3Request WithPoolPartitionId(string poolPartitionId)
        {
            this._poolPartitionId = poolPartitionId;
            if (poolPartitionId != null)
            {
                this.QueryParams.Add("pool_partition_id", poolPartitionId);
            }
            else
            {
                this.QueryParams.Remove("pool_partition_id");
            }
            return this;
        }

        
        public GetStorageDomainMembersSpectraS3Request WithState(StorageDomainMemberState? state)
        {
            this._state = state;
            if (state != null)
            {
                this.QueryParams.Add("state", state.ToString());
            }
            else
            {
                this.QueryParams.Remove("state");
            }
            return this;
        }

        
        public GetStorageDomainMembersSpectraS3Request WithStorageDomainId(Guid? storageDomainId)
        {
            this._storageDomainId = storageDomainId.ToString();
            if (storageDomainId != null)
            {
                this.QueryParams.Add("storage_domain_id", storageDomainId.ToString());
            }
            else
            {
                this.QueryParams.Remove("storage_domain_id");
            }
            return this;
        }

        
        public GetStorageDomainMembersSpectraS3Request WithStorageDomainId(string storageDomainId)
        {
            this._storageDomainId = storageDomainId;
            if (storageDomainId != null)
            {
                this.QueryParams.Add("storage_domain_id", storageDomainId);
            }
            else
            {
                this.QueryParams.Remove("storage_domain_id");
            }
            return this;
        }

        
        public GetStorageDomainMembersSpectraS3Request WithTapePartitionId(Guid? tapePartitionId)
        {
            this._tapePartitionId = tapePartitionId.ToString();
            if (tapePartitionId != null)
            {
                this.QueryParams.Add("tape_partition_id", tapePartitionId.ToString());
            }
            else
            {
                this.QueryParams.Remove("tape_partition_id");
            }
            return this;
        }

        
        public GetStorageDomainMembersSpectraS3Request WithTapePartitionId(string tapePartitionId)
        {
            this._tapePartitionId = tapePartitionId;
            if (tapePartitionId != null)
            {
                this.QueryParams.Add("tape_partition_id", tapePartitionId);
            }
            else
            {
                this.QueryParams.Remove("tape_partition_id");
            }
            return this;
        }

        
        public GetStorageDomainMembersSpectraS3Request WithTapeType(string tapeType)
        {
            this._tapeType = tapeType;
            if (tapeType != null)
            {
                this.QueryParams.Add("tape_type", tapeType);
            }
            else
            {
                this.QueryParams.Remove("tape_type");
            }
            return this;
        }

        
        public GetStorageDomainMembersSpectraS3Request WithWritePreference(WritePreferenceLevel? writePreference)
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


        
        
        public GetStorageDomainMembersSpectraS3Request()
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
                return "/_rest_/storage_domain_member";
            }
        }
    }
}