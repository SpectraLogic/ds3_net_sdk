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

using System;
using System.Collections.Generic;

namespace Ds3.Models
{
    public class StorageDomain
    {
        public string AutoEjectUponCron { get; set; }
        public bool AutoEjectUponJobCancellation { get; set; }
        public bool AutoEjectUponJobCompletion { get; set; }
        public bool AutoEjectUponMediaFull { get; set; }
        public Guid Id { get; set; }
        public LtfsFileNamingMode LtfsFileNaming { get; set; }
        public int MaxTapeFragmentationPercent { get; set; }
        public int MaximumAutoVerificationFrequencyInDays { get; set; }
        public bool MediaEjectionAllowed { get; set; }
        public string Name { get; set; }
        public bool SecureMediaAllocation { get; set; }
        public Priority? VerifyPriorToAutoEject { get; set; }
        public WriteOptimization WriteOptimization { get; set; }
    }
}
