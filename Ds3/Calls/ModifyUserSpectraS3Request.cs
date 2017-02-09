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
    public class ModifyUserSpectraS3Request : Ds3Request
    {
        
        public string UserId { get; private set; }

        
        private string _defaultDataPolicyId;
        public string DefaultDataPolicyId
        {
            get { return _defaultDataPolicyId; }
            set { WithDefaultDataPolicyId(value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { WithName(value); }
        }

        private string _secretKey;
        public string SecretKey
        {
            get { return _secretKey; }
            set { WithSecretKey(value); }
        }

        
        public ModifyUserSpectraS3Request WithDefaultDataPolicyId(Guid? defaultDataPolicyId)
        {
            this._defaultDataPolicyId = defaultDataPolicyId.ToString();
            if (defaultDataPolicyId != null)
            {
                this.QueryParams.Add("default_data_policy_id", defaultDataPolicyId.ToString());
            }
            else
            {
                this.QueryParams.Remove("default_data_policy_id");
            }
            return this;
        }

        
        public ModifyUserSpectraS3Request WithDefaultDataPolicyId(string defaultDataPolicyId)
        {
            this._defaultDataPolicyId = defaultDataPolicyId;
            if (defaultDataPolicyId != null)
            {
                this.QueryParams.Add("default_data_policy_id", defaultDataPolicyId);
            }
            else
            {
                this.QueryParams.Remove("default_data_policy_id");
            }
            return this;
        }

        
        public ModifyUserSpectraS3Request WithName(string name)
        {
            this._name = name;
            if (name != null)
            {
                this.QueryParams.Add("name", name);
            }
            else
            {
                this.QueryParams.Remove("name");
            }
            return this;
        }

        
        public ModifyUserSpectraS3Request WithSecretKey(string secretKey)
        {
            this._secretKey = secretKey;
            if (secretKey != null)
            {
                this.QueryParams.Add("secret_key", secretKey);
            }
            else
            {
                this.QueryParams.Remove("secret_key");
            }
            return this;
        }


        
        
        public ModifyUserSpectraS3Request(Guid userId)
        {
            this.UserId = userId.ToString();
            
        }

        
        public ModifyUserSpectraS3Request(string userId)
        {
            this.UserId = userId;
            
        }

        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.PUT;
            }
        }

        internal override string Path
        {
            get
            {
                return "/_rest_/user/" + UserId.ToString();
            }
        }
    }
}