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
    public class PutDataPersistenceRuleSpectraS3Request : Ds3Request
    {
        
        public string DataPolicyId { get; private set; }

        public DataIsolationLevel IsolationLevel { get; private set; }

        public string StorageDomainId { get; private set; }

        public DataPersistenceRuleType Type { get; private set; }

        
        private int? _minimumDaysToRetain;
        public int? MinimumDaysToRetain
        {
            get { return _minimumDaysToRetain; }
            set { WithMinimumDaysToRetain(value); }
        }

        public PutDataPersistenceRuleSpectraS3Request WithMinimumDaysToRetain(int? minimumDaysToRetain)
        {
            this._minimumDaysToRetain = minimumDaysToRetain;
            if (minimumDaysToRetain != null) {
                this.QueryParams.Add("minimum_days_to_retain", minimumDaysToRetain.ToString());
            }
            else
            {
                this.QueryParams.Remove("minimum_days_to_retain");
            }
            return this;
        }

        
        public PutDataPersistenceRuleSpectraS3Request(Guid dataPolicyId, DataIsolationLevel isolationLevel, Guid storageDomainId, DataPersistenceRuleType type) {
            this.DataPolicyId = dataPolicyId.ToString();
            this.IsolationLevel = isolationLevel;
            this.StorageDomainId = storageDomainId.ToString();
            this.Type = type;
            
            this.QueryParams.Add("data_policy_id", dataPolicyId.ToString());

            this.QueryParams.Add("isolation_level", isolationLevel.ToString());

            this.QueryParams.Add("storage_domain_id", storageDomainId.ToString());

            this.QueryParams.Add("type", type.ToString());

        }

        public PutDataPersistenceRuleSpectraS3Request(string dataPolicyId, DataIsolationLevel isolationLevel, string storageDomainId, DataPersistenceRuleType type) {
            this.DataPolicyId = dataPolicyId;
            this.IsolationLevel = isolationLevel;
            this.StorageDomainId = storageDomainId;
            this.Type = type;
            
            this.QueryParams.Add("data_policy_id", dataPolicyId);

            this.QueryParams.Add("isolation_level", isolationLevel.ToString());

            this.QueryParams.Add("storage_domain_id", storageDomainId);

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
                return "/_rest_/data_persistence_rule";
            }
        }
    }
}