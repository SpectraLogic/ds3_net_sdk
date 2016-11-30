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
        /// A cache is determined near capacity if the current utilization is at or exceeds <see cref="CacheNearCapacityDiagnostic.CacheUtilizationNearCapacityLevel"/>.
        /// <see cref="Ds3DiagnosticsCode.Ok"/> code will be return if no cache file system is near capacity limit
        /// <see cref="Ds3DiagnosticsCode.CacheNearCapacity"/> code will be return if found a cache file system that is near capacity limit
        /// and <see cref="Ds3DiagnosticResult{T}.ErrorInfo"/> will include all cache file systems that are near capacity limit.
        /// <see cref="Ds3DiagnosticsCode.NoCacheSystemFound"/> code will be return if no cache system is found in the system.
        /// </summary>
        public Ds3DiagnosticResults<CacheFilesystemInformation> CacheNearCapacityDiagnosticResult { get; set; }

        /// <summary>
        /// The tape diagnostic result
        /// <see cref="Ds3DiagnosticsCode.Ok"/> code will be return if tapes are found and none are offline
        /// <see cref="Ds3DiagnosticsCode.OfflineTapes"/> code will be return if offline tapes are found 
        /// <see cref="Ds3DiagnosticsCode.NoTapesFound"/> code will be return if no tapes found in the system
        /// and <see cref="Ds3DiagnosticResult{T}.ErrorInfo"/> will include all offline tapes
        /// </summary>
        public Ds3DiagnosticResults<Tape> TapesDiagnosticResult { get; set; }

        /// <summary>
        /// The pools diagnostic result.
        /// <see cref="Ds3DiagnosticsCode.Ok"/> code will be return if pools are found and none are powered off
        /// <see cref="Ds3DiagnosticsCode.NoPoolsFound"/> code will be return if no pools found in the system
        /// <see cref="Ds3DiagnosticsCode.PoweredOffPools"/> code will be return if powered off pools are found
        /// </summary>
        public Ds3DiagnosticResults<Pool> PoolsDiagnosticResult { get; set; }

        /// <summary>
        /// Gets the <see cref="BlobStoreTaskInformation"/> for all reading chunks from tape.
        /// <see cref="Ds3DiagnosticsCode.Ok"/> code will be return if no reading chunks from tape found
        /// <see cref="Ds3DiagnosticsCode.ReadingFromTape"/> code will be return if reading chunks from tape found
        /// </summary>
        public Ds3DiagnosticResults<BlobStoreTaskInformation> ReadingFromTapeTasksResult { get; set; }

        /// <summary>
        /// Gets the <see cref="BlobStoreTaskInformation"/> for all writing chunks to tape.
        /// <see cref="Ds3DiagnosticsCode.Ok"/> code will be return if no writing chunks to tape found
        /// <see cref="Ds3DiagnosticsCode.WritingToTape"/> code will be return if writing chunks to tape found
        /// </summary>
        public Ds3DiagnosticResults<BlobStoreTaskInformation> WritingFromTapeTasksResult { get; set; }
    }
}