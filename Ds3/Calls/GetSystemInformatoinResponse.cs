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

/* <Data>
 *  <ApiVersion>91C76B3B5B01A306A0DFA94C9EE3549A.C3E40C732B27EFA6D25AB24D3E01E851</ApiVersion>
 *  <BuildInformation>
 *      <Branch/>
 *      <Revision/>
 *      <Version/>
 *  </BuildInformation>
 *  <SerialNumber>UNKNOWN</SerialNumber>
 * </Data>
 */

namespace Ds3.Calls
{
    public class GetSystemInformationResponse
    {
        public string ApiMC5Major { get; set; }
        public string ApiMC5Full { get; set; }
        public string BuildBranch { get; set; }
        public string BuildRev { get; set; }
        public string BuildVersion { get; set; }
        public string SerialNumber { get; set; }

        public GetSystemInformationResponse(string apiMC5Major, string apiMC5Full, string buildBranch, string buildRev, string buildVersion, string serialNumber)
        { 
            this.ApiMC5Major = apiMC5Major;
            this.ApiMC5Full = apiMC5Full;
            this.BuildBranch = buildBranch;
            this.BuildRev = buildRev;
            this.BuildVersion = buildVersion;
            this.SerialNumber = serialNumber;
        }
     }
}
