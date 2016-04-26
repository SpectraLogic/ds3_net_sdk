/*
 * ******************************************************************************
 *   Copyright 2014-2015 Spectra Logic Corporation. All Rights Reserved.
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

// This code is auto-generated, do not modify
using System.Net;
using System.IO;
using System.Collections.Generic;
using Ds3.Models;

using Ds3.Runtime;
using System;

namespace Ds3.Calls
{
    public class PutGlobalDataPolicyAclForGroupSpectraS3Response
    {
        public DataPolicyAcl ResponsePayload { get; private set; }

        public PutGlobalDataPolicyAclForGroupSpectraS3Response(DataPolicyAcl responsePayload)
        {
            this.ResponsePayload = responsePayload;
        }
    }
}
