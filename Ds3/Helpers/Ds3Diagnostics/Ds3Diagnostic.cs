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
        /// If no file systems are near capacity or no cache system was found in the system, than a null collection is returned.
        /// </summary>
        public Ds3DiagnosticResult<CacheFilesystemInformation> CacheNearCapacityDiagnostic { get; set; }

        /// <summary>
        /// Gets the <see cref="Tape"/> for all tapes with status of OFFLINE.
        /// If no tapes are offline, than a null collection is returned.
        /// </summary>
        public Ds3DiagnosticResult<Tape> OfflineTapeDiagnostic { get; set; }
    }
}