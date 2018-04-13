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
    public class ModifyTapeDriveSpectraS3Request : Ds3Request
    {
        
        public string TapeDriveId { get; private set; }

        
        private Quiesced? _quiesced;
        public Quiesced? Quiesced
        {
            get { return _quiesced; }
            set { WithQuiesced(value); }
        }

        private ReservedTaskType? _reservedTaskType;
        public ReservedTaskType? ReservedTaskType
        {
            get { return _reservedTaskType; }
            set { WithReservedTaskType(value); }
        }

        
        public ModifyTapeDriveSpectraS3Request WithQuiesced(Quiesced? quiesced)
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

        
        public ModifyTapeDriveSpectraS3Request WithReservedTaskType(ReservedTaskType? reservedTaskType)
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


        
        
        public ModifyTapeDriveSpectraS3Request(Guid tapeDriveId)
        {
            this.TapeDriveId = tapeDriveId.ToString();
            
        }

        
        public ModifyTapeDriveSpectraS3Request(string tapeDriveId)
        {
            this.TapeDriveId = tapeDriveId;
            
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
                return "/_rest_/tape_drive/" + TapeDriveId;
            }
        }
    }
}