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

namespace Ds3.Helpers.Ds3Diagnostics
{
    public class Ds3DiagnosticResult<T>
    {
        public Ds3DiagnosticResult(Ds3DiagnosticsCode code, string errorMessage, IEnumerable<T> errorInfo)
        {
            Code = code;
            ErrorMessage = errorMessage;
            ErrorInfo = errorInfo;
        }

        /// <summary>
        /// Gets the return code of the diagnostic.
        /// <see cref="Ds3DiagnosticsCode"/>.
        /// </summary>
        public Ds3DiagnosticsCode Code { get; set; }

        /// <summary>
        /// Gets the error message.
        /// A null message will be return if no error was found.
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets the error information.
        /// A null collection will be return if no error was found.
        /// </summary>
        public IEnumerable<T> ErrorInfo { get; set; }
    }

    /// <summary>
    /// An enum representing all of the runnable diagnostics return code.
    /// </summary>
    public enum Ds3DiagnosticsCode
    {
        Ok,
        NoCacheSystemFound,
        CacheNearCapacity,
        NoTapesFound,
        OfflineTapes,
        PoweredOffPools,
        NoPoolsFound,
        ReadingFromTape,
        WritingToTape
    }
}