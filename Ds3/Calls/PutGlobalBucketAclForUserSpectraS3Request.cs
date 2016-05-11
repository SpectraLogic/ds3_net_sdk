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
    public class PutGlobalBucketAclForUserSpectraS3Request : Ds3Request
    {
        
        public BucketAclPermission Permission { get; private set; }

        public string UserId { get; private set; }

        

        
        public PutGlobalBucketAclForUserSpectraS3Request(BucketAclPermission permission, Guid userId) {
            this.Permission = permission;
            this.UserId = userId.ToString();
            
            this.QueryParams.Add("permission", permission.ToString());

            this.QueryParams.Add("user_id", userId.ToString());

        }

        public PutGlobalBucketAclForUserSpectraS3Request(BucketAclPermission permission, string userId) {
            this.Permission = permission;
            this.UserId = userId;
            
            this.QueryParams.Add("permission", permission.ToString());

            this.QueryParams.Add("user_id", userId);

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
                return "/_rest_/bucket_acl";
            }
        }
    }
}