/*
 * ******************************************************************************
 *   Copyright 2014 Spectra Logic Corporation. All Rights Reserved.
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

namespace Ds3.Helpers
{
    /// <summary>
    /// Provides a simple API to efficiently transfer objects for a bulk job.
    /// </summary>
    /// <seealso cref="IDs3ClientHelpers.StartWriteJob"/>
    /// <seealso cref="IDs3ClientHelpers.StartReadJob"/>
    public interface IJob : IBaseJob<IJob, string>
    {
    }
}
