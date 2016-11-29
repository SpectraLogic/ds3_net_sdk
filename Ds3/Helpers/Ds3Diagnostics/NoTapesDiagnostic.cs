﻿/*
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

using System.Linq;
using Ds3.Calls;

namespace Ds3.Helpers.Ds3Diagnostics
{
    internal class NoTapesDiagnostic : IDs3DiagnosticCheck<object>
    {
        public Ds3DiagnosticResult<object> Get(IDs3Client client)
        {
            var getTapesRequest = new GetTapesSpectraS3Request();
            var getTapesResponse = client.GetTapesSpectraS3(getTapesRequest);
            var tapes = getTapesResponse.ResponsePayload.Tapes;

            return !tapes.Any()
                ? new Ds3DiagnosticResult<object>(Ds3DiagnosticsCode.NoTapesFound, DiagnosticsMessages.NoTapesFound, null)
                : new Ds3DiagnosticResult<object>(Ds3DiagnosticsCode.Ok, null, null);
        }
    }
}