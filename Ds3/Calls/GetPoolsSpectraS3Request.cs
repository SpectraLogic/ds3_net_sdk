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
using System;
using System.Net;

namespace Ds3.Calls
{
    public class GetPoolsSpectraS3Request : Ds3Request
    {
        
        
        private bool? _assignedToStorageDomain;
        public bool? AssignedToStorageDomain
        {
            get { return _assignedToStorageDomain; }
            set { WithAssignedToStorageDomain(value); }
        }

        private string _bucketId;
        public string BucketId
        {
            get { return _bucketId; }
            set { WithBucketId(value); }
        }

        private PoolHealth? _health;
        public PoolHealth? Health
        {
            get { return _health; }
            set { WithHealth(value); }
        }

        private bool? _lastPage;
        public bool? LastPage
        {
            get { return _lastPage; }
            set { WithLastPage(value); }
        }

        private DateTime? _lastVerified;
        public DateTime? LastVerified
        {
            get { return _lastVerified; }
            set { WithLastVerified(value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { WithName(value); }
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

        private string _partitionId;
        public string PartitionId
        {
            get { return _partitionId; }
            set { WithPartitionId(value); }
        }

        private bool? _poweredOn;
        public bool? PoweredOn
        {
            get { return _poweredOn; }
            set { WithPoweredOn(value); }
        }

        private PoolState? _state;
        public PoolState? State
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

        private PoolType? _type;
        public PoolType? Type
        {
            get { return _type; }
            set { WithType(value); }
        }

        
        public GetPoolsSpectraS3Request WithAssignedToStorageDomain(bool? assignedToStorageDomain)
        {
            this._assignedToStorageDomain = assignedToStorageDomain;
            if (assignedToStorageDomain != null)
            {
                this.QueryParams.Add("assigned_to_storage_domain", assignedToStorageDomain.ToString());
            }
            else
            {
                this.QueryParams.Remove("assigned_to_storage_domain");
            }
            return this;
        }

        
        public GetPoolsSpectraS3Request WithBucketId(Guid? bucketId)
        {
            this._bucketId = bucketId.ToString();
            if (bucketId != null)
            {
                this.QueryParams.Add("bucket_id", bucketId.ToString());
            }
            else
            {
                this.QueryParams.Remove("bucket_id");
            }
            return this;
        }

        
        public GetPoolsSpectraS3Request WithBucketId(string bucketId)
        {
            this._bucketId = bucketId;
            if (bucketId != null)
            {
                this.QueryParams.Add("bucket_id", bucketId);
            }
            else
            {
                this.QueryParams.Remove("bucket_id");
            }
            return this;
        }

        
        public GetPoolsSpectraS3Request WithHealth(PoolHealth? health)
        {
            this._health = health;
            if (health != null)
            {
                this.QueryParams.Add("health", health.ToString());
            }
            else
            {
                this.QueryParams.Remove("health");
            }
            return this;
        }

        
        public GetPoolsSpectraS3Request WithLastPage(bool? lastPage)
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

        
        public GetPoolsSpectraS3Request WithLastVerified(DateTime? lastVerified)
        {
            this._lastVerified = lastVerified;
            if (lastVerified != null)
            {
                this.QueryParams.Add("last_verified", lastVerified.ToString());
            }
            else
            {
                this.QueryParams.Remove("last_verified");
            }
            return this;
        }

        
        public GetPoolsSpectraS3Request WithName(string name)
        {
            this._name = name;
            if (name != null)
            {
                this.QueryParams.Add("name", name);
            }
            else
            {
                this.QueryParams.Remove("name");
            }
            return this;
        }

        
        public GetPoolsSpectraS3Request WithPageLength(int? pageLength)
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

        
        public GetPoolsSpectraS3Request WithPageOffset(int? pageOffset)
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

        
        public GetPoolsSpectraS3Request WithPageStartMarker(Guid? pageStartMarker)
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

        
        public GetPoolsSpectraS3Request WithPageStartMarker(string pageStartMarker)
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

        
        public GetPoolsSpectraS3Request WithPartitionId(Guid? partitionId)
        {
            this._partitionId = partitionId.ToString();
            if (partitionId != null)
            {
                this.QueryParams.Add("partition_id", partitionId.ToString());
            }
            else
            {
                this.QueryParams.Remove("partition_id");
            }
            return this;
        }

        
        public GetPoolsSpectraS3Request WithPartitionId(string partitionId)
        {
            this._partitionId = partitionId;
            if (partitionId != null)
            {
                this.QueryParams.Add("partition_id", partitionId);
            }
            else
            {
                this.QueryParams.Remove("partition_id");
            }
            return this;
        }

        
        public GetPoolsSpectraS3Request WithPoweredOn(bool? poweredOn)
        {
            this._poweredOn = poweredOn;
            if (poweredOn != null)
            {
                this.QueryParams.Add("powered_on", poweredOn.ToString());
            }
            else
            {
                this.QueryParams.Remove("powered_on");
            }
            return this;
        }

        
        public GetPoolsSpectraS3Request WithState(PoolState? state)
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

        
        public GetPoolsSpectraS3Request WithStorageDomainId(Guid? storageDomainId)
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

        
        public GetPoolsSpectraS3Request WithStorageDomainId(string storageDomainId)
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

        
        public GetPoolsSpectraS3Request WithType(PoolType? type)
        {
            this._type = type;
            if (type != null)
            {
                this.QueryParams.Add("type", type.ToString());
            }
            else
            {
                this.QueryParams.Remove("type");
            }
            return this;
        }


        
        
        public GetPoolsSpectraS3Request()
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
                return "/_rest_/pool";
            }
        }
    }
}