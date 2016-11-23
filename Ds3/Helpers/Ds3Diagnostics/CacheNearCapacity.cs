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

using System.Linq;
using Ds3.Calls;
using Ds3.Models;

namespace Ds3.Helpers.Ds3Diagnostics
{
    internal class CacheNearCapacity : IDs3DiagnosticCheck<CacheFilesystemInformation>
    {
        //TODO check if this value is right
        private const double CacheUtilizationNearCapacityLevel = 0.95;

        public Ds3DiagnosticResult<CacheFilesystemInformation> Get(IDs3Client client)
        {
            var response = client.GetCacheStateSpectraS3(new GetCacheStateSpectraS3Request());
            var fileSystemsInfo = response.ResponsePayload.Filesystems;

            if (fileSystemsInfo == null || !fileSystemsInfo.Any())
            {
                //TODO extract string to resource file
                return new Ds3DiagnosticResult<CacheFilesystemInformation>(Ds3DiagnosticsCode.NoCacheSystemFound,
                    "No cache file systems were found", null);
            }

            var fileSystems =
            (
                from filesystemInfo in fileSystemsInfo
                let percentUtilization =
                (double) filesystemInfo.UsedCapacityInBytes/filesystemInfo.AvailableCapacityInBytes
                where percentUtilization >= CacheUtilizationNearCapacityLevel
                select filesystemInfo
            ).ToList();

            return fileSystems.Any()
                //TODO extract string to resource file
                ? new Ds3DiagnosticResult<CacheFilesystemInformation>(Ds3DiagnosticsCode.CacheNearCapacity,
                    "Found cache file systems that near the capacity limit", fileSystems)
                : new Ds3DiagnosticResult<CacheFilesystemInformation>(Ds3DiagnosticsCode.Ok, null, null);
        }
    }
}