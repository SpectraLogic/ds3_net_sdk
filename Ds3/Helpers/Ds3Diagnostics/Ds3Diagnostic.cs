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

using Ds3.Models;

namespace Ds3.Helpers.Ds3Diagnostics
{
    public class Ds3Diagnostic
    {
        /// <summary>
        /// Gets the <see cref="CacheFilesystemInformation"/> for all cache that are near capacity.
        /// A cache is determined near capacity if the current utilization is at or exceeds <see cref="CacheNearCapacity.CacheUtilizationNearCapacityLevel"/>.
        /// <see cref="Ds3DiagnosticsCode.Ok"/> code will be return if no cache file system is near capacity limit
        /// <see cref="Ds3DiagnosticsCode.CacheNearCapacity"/> code will be return if found a cache file system that is near capacity limit
        /// and <see cref="Ds3DiagnosticResult{T}.ErrorInfo"/> will include all cache file systems that are near capacity limit.
        /// <see cref="Ds3DiagnosticsCode.NoCacheSystemFound"/> code will be return if no cache system is found in the system.
        /// </summary>
        public Ds3DiagnosticResult<CacheFilesystemInformation> CacheNearCapacityDiagnostic { get; set; }

        /// <summary>
        /// Gets the <see cref="Tape"/> for all tapes with status of OFFLINE.
        /// <see cref="Ds3DiagnosticsCode.Ok"/> code will be return if no tapes are offline
        /// <see cref="Ds3DiagnosticsCode.OfflineTapes"/> code will be return if offline tapes are found 
        /// and <see cref="Ds3DiagnosticResult{T}.ErrorInfo"/> will include all offline tapes
        /// </summary>
        public Ds3DiagnosticResult<Tape> OfflineTapesDiagnostic { get; set; }

        /// <summary>
        /// The no tapes diagnostic.
        /// <see cref="Ds3DiagnosticsCode.Ok"/> code will be return if found at least one tape in the system
        /// <see cref="Ds3DiagnosticsCode.NoTapesFound"/> code will be return if no tapes found in the system
        /// </summary>
        public Ds3DiagnosticResult<object> NoTapesDiagnostic { get; set; }


        /// <summary>
        /// Gets the <see cref="Pool"/> for all powered off pools.
        /// <see cref="Ds3DiagnosticsCode.Ok"/> code will be return if no powered off pools were found
        /// <see cref="Ds3DiagnosticsCode.PoweredOffPools"/> code will be return if powered off pools are found 
        /// and <see cref="Ds3DiagnosticResult{T}.ErrorInfo"/> will include all powered off pools
        /// </summary>
        public Ds3DiagnosticResult<Pool> PoweredOffPoolsDiagnostic { get; set; }
    }
}