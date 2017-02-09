/*
 * ******************************************************************************
 *   Copyright 2014-2017 Spectra Logic Corporation. All Rights Reserved.
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
using Ds3.Models;
using System;
using System.Net;

namespace Ds3.Calls
{
    public class PutGlobalDataPolicyAclForGroupSpectraS3Request : Ds3Request
    {
        
        public string GroupId { get; private set; }

        

        
        
        public PutGlobalDataPolicyAclForGroupSpectraS3Request(Guid groupId)
        {
            this.GroupId = groupId.ToString();
            
            this.QueryParams.Add("group_id", groupId.ToString());

        }

        
        public PutGlobalDataPolicyAclForGroupSpectraS3Request(string groupId)
        {
            this.GroupId = groupId;
            
            this.QueryParams.Add("group_id", groupId);

        }

        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.POST;
            }
        }

        internal override string Path
        {
            get
            {
                return "/_rest_/data_policy_acl";
            }
        }
    }
}