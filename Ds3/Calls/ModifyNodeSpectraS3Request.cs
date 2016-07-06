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
    public class ModifyNodeSpectraS3Request : Ds3Request
    {
        
        public string Node { get; private set; }

        
        private string _dnsName;
        public string DnsName
        {
            get { return _dnsName; }
            set { WithDnsName(value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { WithName(value); }
        }

        public ModifyNodeSpectraS3Request WithDnsName(string dnsName)
        {
            this._dnsName = dnsName;
            if (dnsName != null)
            {
                this.QueryParams.Add("dns_name", dnsName);
            }
            else
            {
                this.QueryParams.Remove("dns_name");
            }
            return this;
        }
        public ModifyNodeSpectraS3Request WithName(string name)
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

        
        public ModifyNodeSpectraS3Request(string node)
        {
            this.Node = node;
            
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
                return "/_rest_/node/" + Node;
            }
        }
    }
}