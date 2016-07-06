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
    public class PutDataReplicationRuleSpectraS3Request : Ds3Request
    {
        
        public string DataPolicyId { get; private set; }

        public string Ds3TargetId { get; private set; }

        public DataReplicationRuleType Type { get; private set; }

        
        private string _ds3TargetDataPolicy;
        public string Ds3TargetDataPolicy
        {
            get { return _ds3TargetDataPolicy; }
            set { WithDs3TargetDataPolicy(value); }
        }

        public PutDataReplicationRuleSpectraS3Request WithDs3TargetDataPolicy(string ds3TargetDataPolicy)
        {
            this._ds3TargetDataPolicy = ds3TargetDataPolicy;
            if (ds3TargetDataPolicy != null) {
                this.QueryParams.Add("ds3_target_data_policy", ds3TargetDataPolicy);
            }
            else
            {
                this.QueryParams.Remove("ds3_target_data_policy");
            }
            return this;
        }

        
        public PutDataReplicationRuleSpectraS3Request(Guid dataPolicyId, Guid ds3TargetId, DataReplicationRuleType type)
        {
            this.DataPolicyId = dataPolicyId.ToString();
            this.Ds3TargetId = ds3TargetId.ToString();
            this.Type = type;
            
            this.QueryParams.Add("data_policy_id", dataPolicyId.ToString());

            this.QueryParams.Add("ds3_target_id", ds3TargetId.ToString());

            this.QueryParams.Add("type", type.ToString());

        }

        public PutDataReplicationRuleSpectraS3Request(string dataPolicyId, string ds3TargetId, DataReplicationRuleType type)
        {
            this.DataPolicyId = dataPolicyId;
            this.Ds3TargetId = ds3TargetId;
            this.Type = type;
            
            this.QueryParams.Add("data_policy_id", dataPolicyId);

            this.QueryParams.Add("ds3_target_id", ds3TargetId);

            this.QueryParams.Add("type", type.ToString());

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
                return "/_rest_/data_replication_rule";
            }
        }
    }
}