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
using Ds3.Calls;

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
            : this(client, new Ds3TargetClientBuilder())
        {
        }

        public Ds3DiagnosticHelper(IDs3Client client, IDs3TargetClientBuilder ds3TargetClientBuilder)
        {
            Ds3TargetClientBuilder = ds3TargetClientBuilder;

            Ds3DiagnosticClient = new Ds3DiagnosticClient
            {
                Client = client,
                Targets = GetDs3Target(client)
            };
        }

        private Ds3DiagnosticClient Ds3DiagnosticClient { get; }
        private IDs3TargetClientBuilder Ds3TargetClientBuilder { get; }

        private IEnumerable<Ds3DiagnosticClient> GetDs3Target(IDs3Client client)
        {
            var response = client.GetDs3TargetsSpectraS3(new GetDs3TargetsSpectraS3Request());
            var targets = response.ResponsePayload.Ds3Targets;
            if (!targets.Any())
            {
                return null;
            }

            var clientTargets = new List<Ds3DiagnosticClient>();
            foreach (var target in targets)
            {
                var targetEndpoint = target.DataPathEndPoint;
                var targetAccessId = target.AdminAuthId;
                var targetSecretKey = target.AdminSecretKey;
                var targetClient = Ds3TargetClientBuilder.Build(targetEndpoint, targetAccessId, targetSecretKey);
                var ds3DiagnosticClient = new Ds3DiagnosticClient
                {
                    Client = targetClient,
                    Targets = GetDs3Target(targetClient)
                };
                clientTargets.Add(ds3DiagnosticClient);
            }

            return clientTargets;
        }

        /// <summary>
        /// Runs all diagnostics.
        /// <see cref="CacheNearCapacityDiagnostic"/>
        /// <see cref="TapesDiagnostic"/>
        /// <see cref="PoolsDiagnostic"/>
        /// <see cref="ReadFromTapeDiagnostic"/>
        /// <see cref="WriteToTapeDiagnostic"/>
        /// </summary>
        /// <returns>
        /// <see cref="Ds3Diagnostic"/>
        /// </returns>
        public Ds3Diagnostic RunAll()
        {
            var ds3Diagnostic = new Ds3Diagnostic
            {
                CacheNearCapacityDiagnosticResult = Get(new CacheNearCapacityDiagnostic()),
                TapesDiagnosticResult = Get(new TapesDiagnostic()),
                PoolsDiagnosticResult = Get(new PoolsDiagnostic()),
                ReadingFromTapeTasksResult = Get(new ReadFromTapeDiagnostic()),
                WritingFromTapeTasksResult = Get(new WriteToTapeDiagnostic())
            };

            return ds3Diagnostic;
        }

        /// <summary>
        /// Gets a specified DS3 diagnostic to preform.
        /// </summary>
        /// <typeparam name="T">
        /// <see cref="CacheNearCapacityDiagnostic"/>
        /// <see cref="TapesDiagnostic"/>
        /// <see cref="PoolsDiagnostic"/>
        /// <see cref="ReadFromTapeDiagnostic"/>
        /// <see cref="WriteToTapeDiagnostic"/>
        /// </typeparam>
        /// <param name="ds3Diagnostic">The DS3 diagnostic.</param>
        /// <returns>
        /// <see cref="Ds3DiagnosticResult{T}"/>
        /// </returns>
        private Ds3DiagnosticResults<T> Get<T>(IDs3DiagnosticCheck<T> ds3Diagnostic)
        {
            return ds3Diagnostic.Get(Ds3DiagnosticClient);
        }
    }
}