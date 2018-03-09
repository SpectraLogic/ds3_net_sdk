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
    public class ImportPoolSpectraS3Request : Ds3Request
    {
        
        public string Pool { get; private set; }

        
        private string _dataPolicyId;
        public string DataPolicyId
        {
            get { return _dataPolicyId; }
            set { WithDataPolicyId(value); }
        }

        private Priority? _priority;
        public Priority? Priority
        {
            get { return _priority; }
            set { WithPriority(value); }
        }

        private string _storageDomainId;
        public string StorageDomainId
        {
            get { return _storageDomainId; }
            set { WithStorageDomainId(value); }
        }

        private string _userId;
        public string UserId
        {
            get { return _userId; }
            set { WithUserId(value); }
        }

        private Priority? _verifyDataAfterImport;
        public Priority? VerifyDataAfterImport
        {
            get { return _verifyDataAfterImport; }
            set { WithVerifyDataAfterImport(value); }
        }

        private bool? _verifyDataPriorToImport;
        public bool? VerifyDataPriorToImport
        {
            get { return _verifyDataPriorToImport; }
            set { WithVerifyDataPriorToImport(value); }
        }

        
        public ImportPoolSpectraS3Request WithDataPolicyId(Guid? dataPolicyId)
        {
            this._dataPolicyId = dataPolicyId.ToString();
            if (dataPolicyId != null)
            {
                this.QueryParams.Add("data_policy_id", dataPolicyId.ToString());
            }
            else
            {
                this.QueryParams.Remove("data_policy_id");
            }
            return this;
        }

        
        public ImportPoolSpectraS3Request WithDataPolicyId(string dataPolicyId)
        {
            this._dataPolicyId = dataPolicyId;
            if (dataPolicyId != null)
            {
                this.QueryParams.Add("data_policy_id", dataPolicyId);
            }
            else
            {
                this.QueryParams.Remove("data_policy_id");
            }
            return this;
        }

        
        public ImportPoolSpectraS3Request WithPriority(Priority? priority)
        {
            this._priority = priority;
            if (priority != null)
            {
                this.QueryParams.Add("priority", priority.ToString());
            }
            else
            {
                this.QueryParams.Remove("priority");
            }
            return this;
        }

        
        public ImportPoolSpectraS3Request WithStorageDomainId(Guid? storageDomainId)
        {
            this._storageDomainId = storageDomainId.ToString();
            if (storageDomainId != null)
            {
                this.QueryParams.Add("storage_domain_id", storageDomainId.ToString());
            }
            else
            {
                this.QueryParams.Remove("storage_domain_id");
            }
            return this;
        }

        
        public ImportPoolSpectraS3Request WithStorageDomainId(string storageDomainId)
        {
            this._storageDomainId = storageDomainId;
            if (storageDomainId != null)
            {
                this.QueryParams.Add("storage_domain_id", storageDomainId);
            }
            else
            {
                this.QueryParams.Remove("storage_domain_id");
            }
            return this;
        }

        
        public ImportPoolSpectraS3Request WithUserId(Guid? userId)
        {
            this._userId = userId.ToString();
            if (userId != null)
            {
                this.QueryParams.Add("user_id", userId.ToString());
            }
            else
            {
                this.QueryParams.Remove("user_id");
            }
            return this;
        }

        
        public ImportPoolSpectraS3Request WithUserId(string userId)
        {
            this._userId = userId;
            if (userId != null)
            {
                this.QueryParams.Add("user_id", userId);
            }
            else
            {
                this.QueryParams.Remove("user_id");
            }
            return this;
        }

        
        public ImportPoolSpectraS3Request WithVerifyDataAfterImport(Priority? verifyDataAfterImport)
        {
            this._verifyDataAfterImport = verifyDataAfterImport;
            if (verifyDataAfterImport != null)
            {
                this.QueryParams.Add("verify_data_after_import", verifyDataAfterImport.ToString());
            }
            else
            {
                this.QueryParams.Remove("verify_data_after_import");
            }
            return this;
        }

        
        public ImportPoolSpectraS3Request WithVerifyDataPriorToImport(bool? verifyDataPriorToImport)
        {
            this._verifyDataPriorToImport = verifyDataPriorToImport;
            if (verifyDataPriorToImport != null)
            {
                this.QueryParams.Add("verify_data_prior_to_import", verifyDataPriorToImport.ToString());
            }
            else
            {
                this.QueryParams.Remove("verify_data_prior_to_import");
            }
            return this;
        }


        
        
        public ImportPoolSpectraS3Request(string pool)
        {
            this.Pool = pool;
            this.QueryParams.Add("operation", "import");
            
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