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
    public class DataPathBackend
    {
        public bool Activated { get; set; }
        public bool AllowNewJobRequests { get; set; }
        public int? AutoActivateTimeoutInMins { get; set; }
        public AutoInspectMode AutoInspect { get; set; }
        public int CacheAvailableRetryAfterInSeconds { get; set; }
        public Priority? DefaultVerifyDataAfterImport { get; set; }
        public bool DefaultVerifyDataPriorToImport { get; set; }
        public Guid Id { get; set; }
        public Guid InstanceId { get; set; }
        public bool IomEnabled { get; set; }
        public DateTime LastHeartbeat { get; set; }
        public int? PartiallyVerifyLastPercentOfTapes { get; set; }
        public UnavailableMediaUsagePolicy UnavailableMediaPolicy { get; set; }
        public int UnavailablePoolMaxJobRetryInMins { get; set; }
        public int UnavailableTapePartitionMaxJobRetryInMins { get; set; }
    }
}
