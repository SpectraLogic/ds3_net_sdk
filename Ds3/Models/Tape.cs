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

using System;
using System.Collections.Generic;

namespace Ds3.Models
{
    public class Tape
    {
        public bool AssignedToStorageDomain { get; set; }
        public long? AvailableRawCapacity { get; set; }
        public string BarCode { get; set; }
        public Guid? BucketId { get; set; }
        public string DescriptionForIdentification { get; set; }
        public DateTime? EjectDate { get; set; }
        public string EjectLabel { get; set; }
        public string EjectLocation { get; set; }
        public DateTime? EjectPending { get; set; }
        public bool FullOfData { get; set; }
        public Guid Id { get; set; }
        public DateTime? LastAccessed { get; set; }
        public string LastCheckpoint { get; set; }
        public DateTime? LastModified { get; set; }
        public DateTime? LastVerified { get; set; }
        public DateTime? PartiallyVerifiedEndOfTape { get; set; }
        public Guid? PartitionId { get; set; }
        public TapeState? PreviousState { get; set; }
        public string SerialNumber { get; set; }
        public TapeState State { get; set; }
        public Guid? StorageDomainId { get; set; }
        public bool TakeOwnershipPending { get; set; }
        public long? TotalRawCapacity { get; set; }
        public string Type { get; set; }
        public Priority? VerifyPending { get; set; }
        public bool WriteProtected { get; set; }
    }
}
