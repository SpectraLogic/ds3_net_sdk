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
    public class GetTapesSpectraS3Request : Ds3Request
    {
        
        
        private bool? _assignedToStorageDomain;
        public bool? AssignedToStorageDomain
        {
            get { return _assignedToStorageDomain; }
            set { WithAssignedToStorageDomain(value); }
        }

        private long? _availableRawCapacity;
        public long? AvailableRawCapacity
        {
            get { return _availableRawCapacity; }
            set { WithAvailableRawCapacity(value); }
        }

        private string _barCode;
        public string BarCode
        {
            get { return _barCode; }
            set { WithBarCode(value); }
        }

        private string _bucketId;
        public string BucketId
        {
            get { return _bucketId; }
            set { WithBucketId(value); }
        }

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

        private bool? _fullOfData;
        public bool? FullOfData
        {
            get { return _fullOfData; }
            set { WithFullOfData(value); }
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

        private DateTime? _partiallyVerifiedEndOfTape;
        public DateTime? PartiallyVerifiedEndOfTape
        {
            get { return _partiallyVerifiedEndOfTape; }
            set { WithPartiallyVerifiedEndOfTape(value); }
        }

        private string _partitionId;
        public string PartitionId
        {
            get { return _partitionId; }
            set { WithPartitionId(value); }
        }

        private TapeState? _previousState;
        public TapeState? PreviousState
        {
            get { return _previousState; }
            set { WithPreviousState(value); }
        }

        private string _serialNumber;
        public string SerialNumber
        {
            get { return _serialNumber; }
            set { WithSerialNumber(value); }
        }

        private string _sortBy;
        public string SortBy
        {
            get { return _sortBy; }
            set { WithSortBy(value); }
        }

        private TapeState? _state;
        public TapeState? State
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

        private string _type;
        public string Type
        {
            get { return _type; }
            set { WithType(value); }
        }

        private Priority? _verifyPending;
        public Priority? VerifyPending
        {
            get { return _verifyPending; }
            set { WithVerifyPending(value); }
        }

        private bool? _writeProtected;
        public bool? WriteProtected
        {
            get { return _writeProtected; }
            set { WithWriteProtected(value); }
        }

        
        public GetTapesSpectraS3Request WithAssignedToStorageDomain(bool? assignedToStorageDomain)
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

        
        public GetTapesSpectraS3Request WithAvailableRawCapacity(long? availableRawCapacity)
        {
            this._availableRawCapacity = availableRawCapacity;
            if (availableRawCapacity != null)
            {
                this.QueryParams.Add("available_raw_capacity", availableRawCapacity.ToString());
            }
            else
            {
                this.QueryParams.Remove("available_raw_capacity");
            }
            return this;
        }

        
        public GetTapesSpectraS3Request WithBarCode(string barCode)
        {
            this._barCode = barCode;
            if (barCode != null)
            {
                this.QueryParams.Add("bar_code", barCode);
            }
            else
            {
                this.QueryParams.Remove("bar_code");
            }
            return this;
        }

        
        public GetTapesSpectraS3Request WithBucketId(Guid? bucketId)
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

        
        public GetTapesSpectraS3Request WithBucketId(string bucketId)
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

        
        public GetTapesSpectraS3Request WithEjectLabel(string ejectLabel)
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

        
        public GetTapesSpectraS3Request WithEjectLocation(string ejectLocation)
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

        
        public GetTapesSpectraS3Request WithFullOfData(bool? fullOfData)
        {
            this._fullOfData = fullOfData;
            if (fullOfData != null)
            {
                this.QueryParams.Add("full_of_data", fullOfData.ToString());
            }
            else
            {
                this.QueryParams.Remove("full_of_data");
            }
            return this;
        }

        
        public GetTapesSpectraS3Request WithLastPage(bool? lastPage)
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

        
        public GetTapesSpectraS3Request WithLastVerified(DateTime? lastVerified)
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

        
        public GetTapesSpectraS3Request WithPageLength(int? pageLength)
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

        
        public GetTapesSpectraS3Request WithPageOffset(int? pageOffset)
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

        
        public GetTapesSpectraS3Request WithPageStartMarker(Guid? pageStartMarker)
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

        
        public GetTapesSpectraS3Request WithPageStartMarker(string pageStartMarker)
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

        
        public GetTapesSpectraS3Request WithPartiallyVerifiedEndOfTape(DateTime? partiallyVerifiedEndOfTape)
        {
            this._partiallyVerifiedEndOfTape = partiallyVerifiedEndOfTape;
            if (partiallyVerifiedEndOfTape != null)
            {
                this.QueryParams.Add("partially_verified_end_of_tape", partiallyVerifiedEndOfTape.ToString());
            }
            else
            {
                this.QueryParams.Remove("partially_verified_end_of_tape");
            }
            return this;
        }

        
        public GetTapesSpectraS3Request WithPartitionId(Guid? partitionId)
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

        
        public GetTapesSpectraS3Request WithPartitionId(string partitionId)
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

        
        public GetTapesSpectraS3Request WithPreviousState(TapeState? previousState)
        {
            this._previousState = previousState;
            if (previousState != null)
            {
                this.QueryParams.Add("previous_state", previousState.ToString());
            }
            else
            {
                this.QueryParams.Remove("previous_state");
            }
            return this;
        }

        
        public GetTapesSpectraS3Request WithSerialNumber(string serialNumber)
        {
            this._serialNumber = serialNumber;
            if (serialNumber != null)
            {
                this.QueryParams.Add("serial_number", serialNumber);
            }
            else
            {
                this.QueryParams.Remove("serial_number");
            }
            return this;
        }

        
        public GetTapesSpectraS3Request WithSortBy(string sortBy)
        {
            this._sortBy = sortBy;
            if (sortBy != null)
            {
                this.QueryParams.Add("sort_by", sortBy);
            }
            else
            {
                this.QueryParams.Remove("sort_by");
            }
            return this;
        }

        
        public GetTapesSpectraS3Request WithState(TapeState? state)
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

        
        public GetTapesSpectraS3Request WithStorageDomainId(Guid? storageDomainId)
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

        
        public GetTapesSpectraS3Request WithStorageDomainId(string storageDomainId)
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

        
        public GetTapesSpectraS3Request WithType(string type)
        {
            this._type = type;
            if (type != null)
            {
                this.QueryParams.Add("type", type);
            }
            else
            {
                this.QueryParams.Remove("type");
            }
            return this;
        }

        
        public GetTapesSpectraS3Request WithVerifyPending(Priority? verifyPending)
        {
            this._verifyPending = verifyPending;
            if (verifyPending != null)
            {
                this.QueryParams.Add("verify_pending", verifyPending.ToString());
            }
            else
            {
                this.QueryParams.Remove("verify_pending");
            }
            return this;
        }

        
        public GetTapesSpectraS3Request WithWriteProtected(bool? writeProtected)
        {
            this._writeProtected = writeProtected;
            if (writeProtected != null)
            {
                this.QueryParams.Add("write_protected", writeProtected.ToString());
            }
            else
            {
                this.QueryParams.Remove("write_protected");
            }
            return this;
        }


        
        
        public GetTapesSpectraS3Request()
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
                return "/_rest_/tape";
            }
        }
    }
}