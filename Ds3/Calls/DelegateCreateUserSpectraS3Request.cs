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
    public class DelegateCreateUserSpectraS3Request : Ds3Request
    {
        
        public string Name { get; private set; }

        
        private string _id;
        public string Id
        {
            get { return _id; }
            set { WithId(value); }
        }

        private string _secretKey;
        public string SecretKey
        {
            get { return _secretKey; }
            set { WithSecretKey(value); }
        }

        public DelegateCreateUserSpectraS3Request WithId(Guid? id)
        {
            this._id = id.ToString();
            if (id != null) {
                this.QueryParams.Add("id", id.ToString());
            }
            else
            {
                this.QueryParams.Remove("id");
            }
            return this;
        }
        public DelegateCreateUserSpectraS3Request WithId(string id)
        {
            this._id = id;
            if (id != null) {
                this.QueryParams.Add("id", id);
            }
            else
            {
                this.QueryParams.Remove("id");
            }
            return this;
        }
        public DelegateCreateUserSpectraS3Request WithSecretKey(string secretKey)
        {
            this._secretKey = secretKey;
            if (secretKey != null) {
                this.QueryParams.Add("secret_key", secretKey);
            }
            else
            {
                this.QueryParams.Remove("secret_key");
            }
            return this;
        }

        
        public DelegateCreateUserSpectraS3Request(string name)
        {
            this.Name = name;
            
            this.QueryParams.Add("name", name);

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
                return "/_rest_/user";
            }
        }
    }
}