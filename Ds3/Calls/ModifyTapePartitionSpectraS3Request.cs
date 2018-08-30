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
    public class ModifyTapePartitionSpectraS3Request : Ds3Request
    {
        
        public string TapePartition { get; private set; }

        
        private bool? _autoCompactionEnabled;
        public bool? AutoCompactionEnabled
        {
            get { return _autoCompactionEnabled; }
            set { WithAutoCompactionEnabled(value); }
        }

        private int? _minimumReadReservedDrives;
        public int? MinimumReadReservedDrives
        {
            get { return _minimumReadReservedDrives; }
            set { WithMinimumReadReservedDrives(value); }
        }

        private int? _minimumWriteReservedDrives;
        public int? MinimumWriteReservedDrives
        {
            get { return _minimumWriteReservedDrives; }
            set { WithMinimumWriteReservedDrives(value); }
        }

        private Quiesced? _quiesced;
        public Quiesced? Quiesced
        {
            get { return _quiesced; }
            set { WithQuiesced(value); }
        }

        private string _serialNumber;
        public string SerialNumber
        {
            get { return _serialNumber; }
            set { WithSerialNumber(value); }
        }

        
        public ModifyTapePartitionSpectraS3Request WithAutoCompactionEnabled(bool? autoCompactionEnabled)
        {
            this._autoCompactionEnabled = autoCompactionEnabled;
            if (autoCompactionEnabled != null)
            {
                this.QueryParams.Add("auto_compaction_enabled", autoCompactionEnabled.ToString());
            }
            else
            {
                this.QueryParams.Remove("auto_compaction_enabled");
            }
            return this;
        }

        
        public ModifyTapePartitionSpectraS3Request WithMinimumReadReservedDrives(int? minimumReadReservedDrives)
        {
            this._minimumReadReservedDrives = minimumReadReservedDrives;
            if (minimumReadReservedDrives != null)
            {
                this.QueryParams.Add("minimum_read_reserved_drives", minimumReadReservedDrives.ToString());
            }
            else
            {
                this.QueryParams.Remove("minimum_read_reserved_drives");
            }
            return this;
        }

        
        public ModifyTapePartitionSpectraS3Request WithMinimumWriteReservedDrives(int? minimumWriteReservedDrives)
        {
            this._minimumWriteReservedDrives = minimumWriteReservedDrives;
            if (minimumWriteReservedDrives != null)
            {
                this.QueryParams.Add("minimum_write_reserved_drives", minimumWriteReservedDrives.ToString());
            }
            else
            {
                this.QueryParams.Remove("minimum_write_reserved_drives");
            }
            return this;
        }

        
        public ModifyTapePartitionSpectraS3Request WithQuiesced(Quiesced? quiesced)
        {
            this._quiesced = quiesced;
            if (quiesced != null)
            {
                this.QueryParams.Add("quiesced", quiesced.ToString());
            }
            else
            {
                this.QueryParams.Remove("quiesced");
            }
            return this;
        }

        
        public ModifyTapePartitionSpectraS3Request WithSerialNumber(string serialNumber)
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


        
        
        public ModifyTapePartitionSpectraS3Request(string tapePartition)
        {
            this.TapePartition = tapePartition;
            
        }

        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.PUT;
            }
        }

        internal override string Path
        {
            get
            {
                return "/_rest_/tape_partition/" + TapePartition;
            }
        }
    }
}