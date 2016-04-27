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

using System;
using System.Collections.Generic;

namespace Ds3.Models
{
    public class Pool
    {
        public bool AssignedToStorageDomain { get; set; }
        public long AvailableCapacity { get; set; }
        public Guid? BucketId { get; set; }
        public string Guid { get; set; }
        public PoolHealth Health { get; set; }
        public Guid Id { get; set; }
        public DateTime? LastAccessed { get; set; }
        public DateTime? LastModified { get; set; }
        public DateTime? LastVerified { get; set; }
        public string Mountpoint { get; set; }
        public string Name { get; set; }
        public Guid? PartitionId { get; set; }
        public bool PoweredOn { get; set; }
        public Quiesced Quiesced { get; set; }
        public long ReservedCapacity { get; set; }
        public PoolState State { get; set; }
        public Guid? StorageDomainId { get; set; }
        public long TotalCapacity { get; set; }
        public PoolType Type { get; set; }
        public long UsedCapacity { get; set; }
    }
}
