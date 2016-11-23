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

namespace Ds3.Helpers.Ds3Diagnostics
{
    public interface IDs3DiagnosticCheck<T>
    {
        /// <summary>
        /// Gets the specified client.
        /// </summary>
        /// <param name="client">The client.</param>
        /// <returns></returns>
        Ds3DiagnosticResult<T> Get(IDs3Client client);
    }
}
