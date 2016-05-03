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
    public class GetTapesSpectraS3Request : Ds3Request
    {
        
        
        private bool _assignedToStorageDomain;
        public bool AssignedToStorageDomain
        {
            get { return _assignedToStorageDomain; }
            set { WithAssignedToStorageDomain(value); }
        }

        public GetTapesSpectraS3Request WithAssignedToStorageDomain(bool assignedToStorageDomain)
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

        private string _barCode;
        public string BarCode
        {
            get { return _barCode; }
            set { WithBarCode(value); }
        }

        public GetTapesSpectraS3Request WithBarCode(string barCode)
        {
            this._barCode = barCode;
            if (barCode != null) {
                this.QueryParams.Add("bar_code", BarCode);
            }
            else
            {
                this.QueryParams.Remove("bar_code");
            }
            return this;
        }

        private Guid _bucketId;
        public Guid BucketId
        {
            get { return _bucketId; }
            set { WithBucketId(value); }
        }

        public GetTapesSpectraS3Request WithBucketId(Guid bucketId)
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

        private string _ejectLabel;
        public string EjectLabel
        {
            get { return _ejectLabel; }
            set { WithEjectLabel(value); }
        }

        public GetTapesSpectraS3Request WithEjectLabel(string ejectLabel)
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

        public GetTapesSpectraS3Request WithEjectLocation(string ejectLocation)
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

        private bool _fullOfData;
        public bool FullOfData
        {
            get { return _fullOfData; }
            set { WithFullOfData(value); }
        }

        public GetTapesSpectraS3Request WithFullOfData(bool fullOfData)
        {
            this._fullOfData = fullOfData;
            if (fullOfData != null) {
                this.QueryParams.Add("full_of_data", FullOfData.ToString());
            }
            else
            {
                this.QueryParams.Remove("full_of_data");
            }
            return this;
        }

        private bool _lastPage;
        public bool LastPage
        {
            get { return _lastPage; }
            set { WithLastPage(value); }
        }

        public GetTapesSpectraS3Request WithLastPage(bool lastPage)
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

        private int _pageLength;
        public int PageLength
        {
            get { return _pageLength; }
            set { WithPageLength(value); }
        }

        public GetTapesSpectraS3Request WithPageLength(int pageLength)
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

        public GetTapesSpectraS3Request WithPageOffset(int pageOffset)
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

        public GetTapesSpectraS3Request WithPageStartMarker(Guid pageStartMarker)
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

        public GetTapesSpectraS3Request WithPartitionId(Guid partitionId)
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

        private TapeState _previousState;
        public TapeState PreviousState
        {
            get { return _previousState; }
            set { WithPreviousState(value); }
        }

        public GetTapesSpectraS3Request WithPreviousState(TapeState previousState)
        {
            this._previousState = previousState;
            if (previousState != null) {
                this.QueryParams.Add("previous_state", PreviousState.ToString());
            }
            else
            {
                this.QueryParams.Remove("previous_state");
            }
            return this;
        }

        private string _serialNumber;
        public string SerialNumber
        {
            get { return _serialNumber; }
            set { WithSerialNumber(value); }
        }

        public GetTapesSpectraS3Request WithSerialNumber(string serialNumber)
        {
            this._serialNumber = serialNumber;
            if (serialNumber != null) {
                this.QueryParams.Add("serial_number", SerialNumber);
            }
            else
            {
                this.QueryParams.Remove("serial_number");
            }
            return this;
        }

        private TapeState _state;
        public TapeState State
        {
            get { return _state; }
            set { WithState(value); }
        }

        public GetTapesSpectraS3Request WithState(TapeState state)
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

        public GetTapesSpectraS3Request WithStorageDomainId(Guid storageDomainId)
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

        private TapeType _type;
        public TapeType Type
        {
            get { return _type; }
            set { WithType(value); }
        }

        public GetTapesSpectraS3Request WithType(TapeType type)
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

        private bool _writeProtected;
        public bool WriteProtected
        {
            get { return _writeProtected; }
            set { WithWriteProtected(value); }
        }

        public GetTapesSpectraS3Request WithWriteProtected(bool writeProtected)
        {
            this._writeProtected = writeProtected;
            if (writeProtected != null) {
                this.QueryParams.Add("write_protected", WriteProtected.ToString());
            }
            else
            {
                this.QueryParams.Remove("write_protected");
            }
            return this;
        }

        public GetTapesSpectraS3Request() {
            
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
                return "/_rest_/tape/";
            }
        }
    }
}