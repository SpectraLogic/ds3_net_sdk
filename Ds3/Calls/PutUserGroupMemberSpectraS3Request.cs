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

// This code is auto-generated, do not modify
using Ds3.Models;
using System;
using System.Net;

namespace Ds3.Calls
{
    public class PutUserGroupMemberSpectraS3Request : Ds3Request
    {
        
        public string GroupId { get; private set; }

        public string MemberUserId { get; private set; }

        

        
        public PutUserGroupMemberSpectraS3Request(Guid groupId, Guid memberUserId) {
            this.GroupId = groupId.ToString();
            this.MemberUserId = memberUserId.ToString();
            
            this.QueryParams.Add("group_id", groupId.ToString());

            this.QueryParams.Add("member_user_id", memberUserId.ToString());

        }

        public PutUserGroupMemberSpectraS3Request(string groupId, string memberUserId) {
            this.GroupId = groupId;
            this.MemberUserId = memberUserId;
            
            this.QueryParams.Add("group_id", groupId);

            this.QueryParams.Add("member_user_id", memberUserId);

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
                return "/_rest_/group_member";
            }
        }
    }
}