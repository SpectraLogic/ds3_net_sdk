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

using System.Net;

using Ds3.Models;
using Ds3.Runtime;

namespace Ds3.Calls
{
    public class GetSystemInformationResponse
    {
        public string ApiMC5 { get; private set; }
        public string BuildBranch { get; private set; }
        public string BuildRev { get; private set; }
        public string BuildVersion { get; private set; }
        public string SerialNumber { get; private set; }

        public GetSystemInformationResponse(string apiMC5, string buildBranch, string buildRev, string buildVersion, string serialNumber)
        { 
            this.ApiMC5 = apiMC5;
            this.BuildBranch = buildBranch;
            this.BuildRev = buildRev;
            this.BuildVersion = buildVersion;
            this.SerialNumber = serialNumber;
        }
     }
}
