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
    public class PutAzureDataReplicationRuleSpectraS3Request : Ds3Request
    {
        
        public string DataPolicyId { get; private set; }

        public string TargetId { get; private set; }

        public DataReplicationRuleType Type { get; private set; }

        
        private long? _maxBlobPartSizeInBytes;
        public long? MaxBlobPartSizeInBytes
        {
            get { return _maxBlobPartSizeInBytes; }
            set { WithMaxBlobPartSizeInBytes(value); }
        }

        private bool? _replicateDeletes;
        public bool? ReplicateDeletes
        {
            get { return _replicateDeletes; }
            set { WithReplicateDeletes(value); }
        }

        
        public PutAzureDataReplicationRuleSpectraS3Request WithMaxBlobPartSizeInBytes(long? maxBlobPartSizeInBytes)
        {
            this._maxBlobPartSizeInBytes = maxBlobPartSizeInBytes;
            if (maxBlobPartSizeInBytes != null)
            {
                this.QueryParams.Add("max_blob_part_size_in_bytes", maxBlobPartSizeInBytes.ToString());
            }
            else
            {
                this.QueryParams.Remove("max_blob_part_size_in_bytes");
            }
            return this;
        }

        
        public PutAzureDataReplicationRuleSpectraS3Request WithReplicateDeletes(bool? replicateDeletes)
        {
            this._replicateDeletes = replicateDeletes;
            if (replicateDeletes != null)
            {
                this.QueryParams.Add("replicate_deletes", replicateDeletes.ToString());
            }
            else
            {
                this.QueryParams.Remove("replicate_deletes");
            }
            return this;
        }


        
        
        public PutAzureDataReplicationRuleSpectraS3Request(Guid dataPolicyId, Guid targetId, DataReplicationRuleType type)
        {
            this.DataPolicyId = dataPolicyId.ToString();
            this.TargetId = targetId.ToString();
            this.Type = type;
            
            this.QueryParams.Add("data_policy_id", dataPolicyId.ToString());

            this.QueryParams.Add("target_id", targetId.ToString());

            this.QueryParams.Add("type", type.ToString());

        }

        
        public PutAzureDataReplicationRuleSpectraS3Request(string dataPolicyId, string targetId, DataReplicationRuleType type)
        {
            this.DataPolicyId = dataPolicyId;
            this.TargetId = targetId;
            this.Type = type;
            
            this.QueryParams.Add("data_policy_id", dataPolicyId);

            this.QueryParams.Add("target_id", targetId);

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
                return "/_rest_/azure_data_replication_rule";
            }
        }
    }
}