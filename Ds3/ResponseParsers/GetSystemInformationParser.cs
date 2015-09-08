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

using System;
using System.IO;
using System.Net;
using Ds3.Calls;
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
namespace Ds3.ResponseParsers
{
    class GetSystemInformationParser : IResponseParser<GetSystemInformationRequest, GetSystemInformationResponse>
    {
        public GetSystemInformationResponse Parse(GetSystemInformationRequest request, IWebResponse response)
        {
            using (response)
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.OK);
                using (Stream content = response.GetResponseStream())
                {
                    var root = XmlExtensions.ReadDocument(content).ElementOrThrow("Data");

                    var buildParams = root.Element("BuildInformation");
                    string BuildBranch = buildParams.TextOf("Branch");
                    string BuildRev = buildParams.TextOf("Revision");
                    string BuildVersion = buildParams.TextOf("Version");
                    string ApiVersion = root.TextOf("ApiVersion");
                    string MC5Major = string.Empty;
                    string MC5Full = string.Empty;
                    if (!string.IsNullOrEmpty(ApiVersion))
                    {
                        string[] mc5s = ApiVersion.Split('.');
                        if ((mc5s != null) &&  (mc5s.Length == 2))
                        {
                            MC5Major = mc5s[0];
                            MC5Full = mc5s[1];
                        }
                    }
                    string SerialNumber = root.TextOf("SerialNumber");
                    return new GetSystemInformationResponse(MC5Major, MC5Full, BuildBranch, BuildRev, BuildVersion, SerialNumber);

                }
            }
        }
    }
}
