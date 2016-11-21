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

using System.Collections.Generic;
using System.Linq;
using Ds3.Models;

namespace Ds3.Helpers.Ds3Diagnostics
{
    /// <summary>
    /// Diagnostic helper class
    /// </summary>
    public class Ds3Diagnostic
    {
        private IDs3Client Client { get; }

        /// <summary>
        /// Gets the <see cref="CacheFilesystemInformation"/> for all cache that are near capacity.
        /// A cache is determined near capacity if the current utilization is at or exceeds <see cref="CacheNearCapacity.CacheUtilizationNearCapacityLevel"/>.
        /// If no file systems are near capacity, than an empty collection is returned.
        /// </summary>
        public IEnumerable<CacheFilesystemInformation> CacheFilesystemInformations { get; private set; }

        /// <summary>
        /// Gets the <see cref="Tape"/> for all tapes with status of OFFLINE.
        /// If no tapes are offline, than an empty collection is returned.
        /// </summary>
        public IEnumerable<Tape> OfflineTapes { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ds3Diagnostic"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public Ds3Diagnostic(IDs3Client client)
        {
            Client = client;
        }

        /// <summary>
        /// Runs all diagnostics.
        /// <see cref="CacheNearCapacity"/>
        /// <see cref="OfflineTapes"/>
        /// </summary>
        /// <returns>
        /// A list of <see cref="Ds3DiagnosticsResult"/>
        /// </returns>
        public IEnumerable<Ds3DiagnosticsResult> RunAll()
        {
            var ds3DiagnosticsResults = new HashSet<Ds3DiagnosticsResult>();

            CacheFilesystemInformations = Get(new CacheNearCapacity());
            if (CacheFilesystemInformations.Any())
            {
                ds3DiagnosticsResults.Add(Ds3DiagnosticsResult.CacheNearCapacity);
            }

            OfflineTapes = Get(new OfflineTapes());
            if (OfflineTapes.Any())
            {
                ds3DiagnosticsResults.Add(Ds3DiagnosticsResult.OfflineTapes);
            }

            //TODO add more checks

            return ds3DiagnosticsResults;
        }

        /// <summary>
        /// Gets a specified DS3 diagnostic to preform.
        /// <see cref="CacheNearCapacity"/>
        /// <see cref="OfflineTapes"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="ds3Diagnostic">The DS3 diagnostic.</param>
        /// <returns></returns>
        public IEnumerable<T> Get<T>(IDs3DiagnosticCheck<T> ds3Diagnostic)
        {
            return ds3Diagnostic.Get(Client);
        }
    }

    /// <summary>
    /// An enum representing all of the runnable diagnostics. This is used
    /// in the <code>RunAll()</code> to denote which diagnostics returned a possible issue.
    /// </summary>
    public enum Ds3DiagnosticsResult
    {
        CacheNearCapacity,
        OfflineTapes
        //TODO add more results
    }
}
