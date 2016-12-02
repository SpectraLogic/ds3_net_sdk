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
    public class ModifyS3DataReplicationRuleSpectraS3Request : Ds3Request
    {
        
        public string S3DataReplicationRule { get; private set; }

        
        private S3InitialDataPlacementPolicy? _initialDataPlacement;
        public S3InitialDataPlacementPolicy? InitialDataPlacement
        {
            get { return _initialDataPlacement; }
            set { WithInitialDataPlacement(value); }
        }

        private long? _maxBlobPartSizeInBytes;
        public long? MaxBlobPartSizeInBytes
        {
            get { return _maxBlobPartSizeInBytes; }
            set { WithMaxBlobPartSizeInBytes(value); }
        }

        private DataReplicationRuleType? _type;
        public DataReplicationRuleType? Type
        {
            get { return _type; }
            set { WithType(value); }
        }

        
        public ModifyS3DataReplicationRuleSpectraS3Request WithInitialDataPlacement(S3InitialDataPlacementPolicy? initialDataPlacement)
        {
            this._initialDataPlacement = initialDataPlacement;
            if (initialDataPlacement != null)
            {
                this.QueryParams.Add("initial_data_placement", initialDataPlacement.ToString());
            }
            else
            {
                this.QueryParams.Remove("initial_data_placement");
            }
            return this;
        }

        
        public ModifyS3DataReplicationRuleSpectraS3Request WithMaxBlobPartSizeInBytes(long? maxBlobPartSizeInBytes)
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

        
        public ModifyS3DataReplicationRuleSpectraS3Request WithType(DataReplicationRuleType? type)
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


        
        
        public ModifyS3DataReplicationRuleSpectraS3Request(string s3DataReplicationRule)
        {
            this.S3DataReplicationRule = s3DataReplicationRule;
            
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
                return "/_rest_/s3_data_replication_rule/" + S3DataReplicationRule;
            }
        }
    }
}