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

namespace Ds3.Helpers.Ds3Diagnostics
{
    /// <summary>
    /// Diagnostic helper class
    /// </summary>
    public class Ds3DiagnosticHelper
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Ds3DiagnosticHelper"/> class.
        /// </summary>
        /// <param name="client">The client.</param>
        public Ds3DiagnosticHelper(IDs3Client client)
        {
            Client = client;
        }

        private IDs3Client Client { get; }

        /// <summary>
        /// Runs all diagnostics.
        /// <see cref="CacheNearCapacity"/>
        /// <see cref="OfflineTapes"/>
        /// <see cref="NoTapes"/>
        /// </summary>
        /// <returns>
        /// <see cref="Ds3Diagnostic"/>
        /// </returns>
        public Ds3Diagnostic RunAll()
        {
            var ds3Diagnostic = new Ds3Diagnostic
            {
                CacheNearCapacityDiagnostic = Get(new CacheNearCapacity()),
                OfflineTapesDiagnostic = Get(new OfflineTapes()),
                NoTapesDiagnostic = Get(new NoTapes())

                //TODO add more checks
            };

            return ds3Diagnostic;
        }

        /// <summary>
        /// Gets a specified DS3 diagnostic to preform.
        /// </summary>
        /// <typeparam name="T">
        /// <see cref="CacheNearCapacity"/>
        /// <see cref="OfflineTapes"/>
        /// <see cref="NoTapes"/>
        /// </typeparam>
        /// <param name="ds3Diagnostic">The DS3 diagnostic.</param>
        /// <returns>
        /// <see cref="Ds3DiagnosticResult{T}"/>
        /// </returns>
        public Ds3DiagnosticResult<T> Get<T>(IDs3DiagnosticCheck<T> ds3Diagnostic)
        {
            return ds3Diagnostic.Get(Client);
        }
    }
}