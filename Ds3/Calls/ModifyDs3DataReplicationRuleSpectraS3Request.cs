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
    public class ModifyDs3DataReplicationRuleSpectraS3Request : Ds3Request
    {
        
        public string Ds3DataReplicationRule { get; private set; }

        
        private string _targetDataPolicy;
        public string TargetDataPolicy
        {
            get { return _targetDataPolicy; }
            set { WithTargetDataPolicy(value); }
        }

        private DataReplicationRuleType? _type;
        public DataReplicationRuleType? Type
        {
            get { return _type; }
            set { WithType(value); }
        }

        
        public ModifyDs3DataReplicationRuleSpectraS3Request WithTargetDataPolicy(string targetDataPolicy)
        {
            this._targetDataPolicy = targetDataPolicy;
            if (targetDataPolicy != null)
            {
                this.QueryParams.Add("target_data_policy", targetDataPolicy);
            }
            else
            {
                this.QueryParams.Remove("target_data_policy");
            }
            return this;
        }

        
        public ModifyDs3DataReplicationRuleSpectraS3Request WithType(DataReplicationRuleType? type)
        {
            this._type = type;
            if (type != null)
            {
                this.QueryParams.Add("type", type.ToString());
            }
            else
            {
                this.QueryParams.Remove("type");
            }
            return this;
        }


        
        
        public ModifyDs3DataReplicationRuleSpectraS3Request(string ds3DataReplicationRule)
        {
            this.Ds3DataReplicationRule = ds3DataReplicationRule;
            
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
                return "/_rest_/ds3_data_replication_rule/" + Ds3DataReplicationRule;
            }
        }
    }
}