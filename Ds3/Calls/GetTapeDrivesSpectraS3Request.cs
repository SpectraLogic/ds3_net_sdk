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
    public class GetTapeDrivesSpectraS3Request : Ds3Request
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

        private string _partitionId;
        public string PartitionId
        {
            get { return _partitionId; }
            set { WithPartitionId(value); }
        }

        private ReservedTaskType? _reservedTaskType;
        public ReservedTaskType? ReservedTaskType
        {
            get { return _reservedTaskType; }
            set { WithReservedTaskType(value); }
        }

        private string _serialNumber;
        public string SerialNumber
        {
            get { return _serialNumber; }
            set { WithSerialNumber(value); }
        }

        private TapeDriveState? _state;
        public TapeDriveState? State
        {
            get { return _state; }
            set { WithState(value); }
        }

        private TapeDriveType? _type;
        public TapeDriveType? Type
        {
            get { return _type; }
            set { WithType(value); }
        }

        
        public GetTapeDrivesSpectraS3Request WithLastPage(bool? lastPage)
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

        
        public GetTapeDrivesSpectraS3Request WithPageLength(int? pageLength)
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

        
        public GetTapeDrivesSpectraS3Request WithPageOffset(int? pageOffset)
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

        
        public GetTapeDrivesSpectraS3Request WithPageStartMarker(Guid? pageStartMarker)
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

        
        public GetTapeDrivesSpectraS3Request WithPageStartMarker(string pageStartMarker)
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

        
        public GetTapeDrivesSpectraS3Request WithPartitionId(Guid? partitionId)
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

        
        public GetTapeDrivesSpectraS3Request WithPartitionId(string partitionId)
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

        
        public GetTapeDrivesSpectraS3Request WithReservedTaskType(ReservedTaskType? reservedTaskType)
        {
            this._reservedTaskType = reservedTaskType;
            if (reservedTaskType != null)
            {
                this.QueryParams.Add("reserved_task_type", reservedTaskType.ToString());
            }
            else
            {
                this.QueryParams.Remove("reserved_task_type");
            }
            return this;
        }

        
        public GetTapeDrivesSpectraS3Request WithSerialNumber(string serialNumber)
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

        
        public GetTapeDrivesSpectraS3Request WithState(TapeDriveState? state)
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

        
        public GetTapeDrivesSpectraS3Request WithType(TapeDriveType? type)
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


        
        
        public GetTapeDrivesSpectraS3Request()
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
                return "/_rest_/tape_drive";
            }
        }
    }
}