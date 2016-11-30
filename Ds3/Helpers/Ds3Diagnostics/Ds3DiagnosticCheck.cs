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

using System;

namespace Ds3.Helpers.Ds3Diagnostics
{
    internal abstract class Ds3DiagnosticCheck<T> : IDs3DiagnosticCheck<T>
    {
        public abstract Ds3DiagnosticResults<T> Get(Ds3DiagnosticClient ds3DiagnosticClient);

        protected Ds3DiagnosticResults<T> Get(Ds3DiagnosticClient ds3DiagnosticClient,
            Func<IDs3Client, Ds3DiagnosticResult<T>> func)
        {
            var results = new Ds3DiagnosticResults<T> {ClientResult = func(ds3DiagnosticClient.Client)};

            if (ds3DiagnosticClient.Targets == null)
            {
                results.TargetsResults = null;
            }
            else
            {
                foreach (var target in ds3DiagnosticClient.Targets)
                {
                    results.TargetsResults.Add(Get(target, func));
                }
            }

            return results;
        }
    }
}