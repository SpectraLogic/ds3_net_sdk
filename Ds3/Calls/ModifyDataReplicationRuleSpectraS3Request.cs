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
    public class ModifyDataReplicationRuleSpectraS3Request : Ds3Request
    {
        
        public string DataReplicationRule { get; private set; }

        
        private string _ds3TargetDataPolicy;
        public string Ds3TargetDataPolicy
        {
            get { return _ds3TargetDataPolicy; }
            set { WithDs3TargetDataPolicy(value); }
        }

        private DataReplicationRuleType? _type;
        public DataReplicationRuleType? Type
        {
            get { return _type; }
            set { WithType(value); }
        }

        public ModifyDataReplicationRuleSpectraS3Request WithDs3TargetDataPolicy(string ds3TargetDataPolicy)
        {
            this._ds3TargetDataPolicy = ds3TargetDataPolicy;
            if (ds3TargetDataPolicy != null)
            {
                this.QueryParams.Add("ds3_target_data_policy", ds3TargetDataPolicy);
            }
            else
            {
                this.QueryParams.Remove("ds3_target_data_policy");
            }
            return this;
        }
        public ModifyDataReplicationRuleSpectraS3Request WithType(DataReplicationRuleType? type)
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

        
        public ModifyDataReplicationRuleSpectraS3Request(string dataReplicationRule)
        {
            this.DataReplicationRule = dataReplicationRule;
            
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
                return "/_rest_/data_replication_rule/" + DataReplicationRule;
            }
        }
    }
}