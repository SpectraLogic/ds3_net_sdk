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
    public class ModifyPoolSpectraS3Request : Ds3Request
    {
        
        public string Pool { get; private set; }

        
        private string _partitionId;
        public string PartitionId
        {
            get { return _partitionId; }
            set { WithPartitionId(value); }
        }

        private Quiesced? _quiesced;
        public Quiesced? Quiesced
        {
            get { return _quiesced; }
            set { WithQuiesced(value); }
        }

        
        public ModifyPoolSpectraS3Request WithPartitionId(Guid? partitionId)
        {
            this._partitionId = partitionId.ToString();
            if (partitionId != null)
            {
                this.QueryParams.Add("partition_id", partitionId.ToString());
            }
            else
            {
                this.QueryParams.Remove("partition_id");
            }
            return this;
        }

        
        public ModifyPoolSpectraS3Request WithPartitionId(string partitionId)
        {
            this._partitionId = partitionId;
            if (partitionId != null)
            {
                this.QueryParams.Add("partition_id", partitionId);
            }
            else
            {
                this.QueryParams.Remove("partition_id");
            }
            return this;
        }

        
        public ModifyPoolSpectraS3Request WithQuiesced(Quiesced? quiesced)
        {
            this._quiesced = quiesced;
            if (quiesced != null)
            {
                this.QueryParams.Add("quiesced", quiesced.ToString());
            }
            else
            {
                this.QueryParams.Remove("quiesced");
            }
            return this;
        }


        
        
        public ModifyPoolSpectraS3Request(string pool)
        {
            this.Pool = pool;
            
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
                return "/_rest_/pool/" + Pool;
            }
        }
    }
}