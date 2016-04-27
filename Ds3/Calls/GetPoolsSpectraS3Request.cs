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
using System;
using System.Net;

namespace Ds3.Calls
{
    public class GetPoolsSpectraS3Request : Ds3Request
    {
        
        
        private bool _assignedToStorageDomain;
        public bool AssignedToStorageDomain
        {
            get { return _assignedToStorageDomain; }
            set { WithAssignedToStorageDomain(value); }
        }

        public GetPoolsSpectraS3Request WithAssignedToStorageDomain(bool assignedToStorageDomain)
        {
            this._assignedToStorageDomain = assignedToStorageDomain;
            if (assignedToStorageDomain != null) {
                this.QueryParams.Add("assigned_to_storage_domain", AssignedToStorageDomain.ToString());
            }
            else
            {
                this.QueryParams.Remove("assigned_to_storage_domain");
            }
            return this;
        }

        private Guid _bucketId;
        public Guid BucketId
        {
            get { return _bucketId; }
            set { WithBucketId(value); }
        }

        public GetPoolsSpectraS3Request WithBucketId(Guid bucketId)
        {
            this._bucketId = bucketId;
            if (bucketId != null) {
                this.QueryParams.Add("bucket_id", BucketId.ToString());
            }
            else
            {
                this.QueryParams.Remove("bucket_id");
            }
            return this;
        }

        private PoolHealth _health;
        public PoolHealth Health
        {
            get { return _health; }
            set { WithHealth(value); }
        }

        public GetPoolsSpectraS3Request WithHealth(PoolHealth health)
        {
            this._health = health;
            if (health != null) {
                this.QueryParams.Add("health", Health.ToString());
            }
            else
            {
                this.QueryParams.Remove("health");
            }
            return this;
        }

        private bool _lastPage;
        public bool LastPage
        {
            get { return _lastPage; }
            set { WithLastPage(value); }
        }

        public GetPoolsSpectraS3Request WithLastPage(bool lastPage)
        {
            this._lastPage = lastPage;
            if (lastPage != null) {
                this.QueryParams.Add("last_page", LastPage.ToString());
            }
            else
            {
                this.QueryParams.Remove("last_page");
            }
            return this;
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { WithName(value); }
        }

        public GetPoolsSpectraS3Request WithName(string name)
        {
            this._name = name;
            if (name != null) {
                this.QueryParams.Add("name", Name);
            }
            else
            {
                this.QueryParams.Remove("name");
            }
            return this;
        }

        private int _pageLength;
        public int PageLength
        {
            get { return _pageLength; }
            set { WithPageLength(value); }
        }

        public GetPoolsSpectraS3Request WithPageLength(int pageLength)
        {
            this._pageLength = pageLength;
            if (pageLength != null) {
                this.QueryParams.Add("page_length", PageLength.ToString());
            }
            else
            {
                this.QueryParams.Remove("page_length");
            }
            return this;
        }

        private int _pageOffset;
        public int PageOffset
        {
            get { return _pageOffset; }
            set { WithPageOffset(value); }
        }

        public GetPoolsSpectraS3Request WithPageOffset(int pageOffset)
        {
            this._pageOffset = pageOffset;
            if (pageOffset != null) {
                this.QueryParams.Add("page_offset", PageOffset.ToString());
            }
            else
            {
                this.QueryParams.Remove("page_offset");
            }
            return this;
        }

        private Guid _pageStartMarker;
        public Guid PageStartMarker
        {
            get { return _pageStartMarker; }
            set { WithPageStartMarker(value); }
        }

        public GetPoolsSpectraS3Request WithPageStartMarker(Guid pageStartMarker)
        {
            this._pageStartMarker = pageStartMarker;
            if (pageStartMarker != null) {
                this.QueryParams.Add("page_start_marker", PageStartMarker.ToString());
            }
            else
            {
                this.QueryParams.Remove("page_start_marker");
            }
            return this;
        }

        private Guid _partitionId;
        public Guid PartitionId
        {
            get { return _partitionId; }
            set { WithPartitionId(value); }
        }

        public GetPoolsSpectraS3Request WithPartitionId(Guid partitionId)
        {
            this._partitionId = partitionId;
            if (partitionId != null) {
                this.QueryParams.Add("partition_id", PartitionId.ToString());
            }
            else
            {
                this.QueryParams.Remove("partition_id");
            }
            return this;
        }

        private bool _poweredOn;
        public bool PoweredOn
        {
            get { return _poweredOn; }
            set { WithPoweredOn(value); }
        }

        public GetPoolsSpectraS3Request WithPoweredOn(bool poweredOn)
        {
            this._poweredOn = poweredOn;
            if (poweredOn != null) {
                this.QueryParams.Add("powered_on", PoweredOn.ToString());
            }
            else
            {
                this.QueryParams.Remove("powered_on");
            }
            return this;
        }

        private PoolState _state;
        public PoolState State
        {
            get { return _state; }
            set { WithState(value); }
        }

        public GetPoolsSpectraS3Request WithState(PoolState state)
        {
            this._state = state;
            if (state != null) {
                this.QueryParams.Add("state", State.ToString());
            }
            else
            {
                this.QueryParams.Remove("state");
            }
            return this;
        }

        private Guid _storageDomainId;
        public Guid StorageDomainId
        {
            get { return _storageDomainId; }
            set { WithStorageDomainId(value); }
        }

        public GetPoolsSpectraS3Request WithStorageDomainId(Guid storageDomainId)
        {
            this._storageDomainId = storageDomainId;
            if (storageDomainId != null) {
                this.QueryParams.Add("storage_domain_id", StorageDomainId.ToString());
            }
            else
            {
                this.QueryParams.Remove("storage_domain_id");
            }
            return this;
        }

        private PoolType _type;
        public PoolType Type
        {
            get { return _type; }
            set { WithType(value); }
        }

        public GetPoolsSpectraS3Request WithType(PoolType type)
        {
            this._type = type;
            if (type != null) {
                this.QueryParams.Add("type", Type.ToString());
            }
            else
            {
                this.QueryParams.Remove("type");
            }
            return this;
        }

        public GetPoolsSpectraS3Request() {
            
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
                return "/_rest_/pool/";
            }
        }
    }
}