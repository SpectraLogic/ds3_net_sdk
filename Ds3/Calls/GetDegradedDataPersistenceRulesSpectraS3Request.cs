/*
 * ******************************************************************************
 *   Copyright 2014-2015 Spectra Logic Corporation. All Rights Reserved.
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
using System.Net;

namespace Ds3.Calls
{
    public class GetDegradedDataPersistenceRulesSpectraS3Request : Ds3Request
    {
        
        
        private Guid _dataPolicyId;
        public Guid DataPolicyId
        {
            get { return _dataPolicyId; }
            set { WithDataPolicyId(value); }
        }

        public GetDegradedDataPersistenceRulesSpectraS3Request WithDataPolicyId(Guid dataPolicyId)
        {
            this._dataPolicyId = dataPolicyId;
            if (dataPolicyId != null) {
                this.QueryParams.Add("data_policy_id", dataPolicyId.ToString());
            }
            else
            {
                this.QueryParams.Remove("data_policy_id");
            }
            return this;
        }

        private DataIsolationLevel _isolationLevel;
        public DataIsolationLevel IsolationLevel
        {
            get { return _isolationLevel; }
            set { WithIsolationLevel(value); }
        }

        public GetDegradedDataPersistenceRulesSpectraS3Request WithIsolationLevel(DataIsolationLevel isolationLevel)
        {
            this._isolationLevel = isolationLevel;
            if (isolationLevel != null) {
                this.QueryParams.Add("isolation_level", isolationLevel.ToString());
            }
            else
            {
                this.QueryParams.Remove("isolation_level");
            }
            return this;
        }

        private bool _lastPage;
        public bool LastPage
        {
            get { return _lastPage; }
            set { WithLastPage(value); }
        }

        public GetDegradedDataPersistenceRulesSpectraS3Request WithLastPage(bool lastPage)
        {
            this._lastPage = lastPage;
            if (lastPage != null) {
                this.QueryParams.Add("last_page", lastPage.ToString());
            }
            else
            {
                this.QueryParams.Remove("last_page");
            }
            return this;
        }

        private int _pageLength;
        public int PageLength
        {
            get { return _pageLength; }
            set { WithPageLength(value); }
        }

        public GetDegradedDataPersistenceRulesSpectraS3Request WithPageLength(int pageLength)
        {
            this._pageLength = pageLength;
            if (pageLength != null) {
                this.QueryParams.Add("page_length", PageLength.ToString());
            }
            else
            {
                this.QueryParams.Remove("page_length");
            }
            return this;
        }

        private int _pageOffset;
        public int PageOffset
        {
            get { return _pageOffset; }
            set { WithPageOffset(value); }
        }

        public GetDegradedDataPersistenceRulesSpectraS3Request WithPageOffset(int pageOffset)
        {
            this._pageOffset = pageOffset;
            if (pageOffset != null) {
                this.QueryParams.Add("page_offset", PageOffset.ToString());
            }
            else
            {
                this.QueryParams.Remove("page_offset");
            }
            return this;
        }

        private Guid _pageStartMarker;
        public Guid PageStartMarker
        {
            get { return _pageStartMarker; }
            set { WithPageStartMarker(value); }
        }

        public GetDegradedDataPersistenceRulesSpectraS3Request WithPageStartMarker(Guid pageStartMarker)
        {
            this._pageStartMarker = pageStartMarker;
            if (pageStartMarker != null) {
                this.QueryParams.Add("page_start_marker", pageStartMarker.ToString());
            }
            else
            {
                this.QueryParams.Remove("page_start_marker");
            }
            return this;
        }

        private DataPersistenceRuleState _state;
        public DataPersistenceRuleState State
        {
            get { return _state; }
            set { WithState(value); }
        }

        public GetDegradedDataPersistenceRulesSpectraS3Request WithState(DataPersistenceRuleState state)
        {
            this._state = state;
            if (state != null) {
                this.QueryParams.Add("state", state.ToString());
            }
            else
            {
                this.QueryParams.Remove("state");
            }
            return this;
        }

        private Guid _storageDomainId;
        public Guid StorageDomainId
        {
            get { return _storageDomainId; }
            set { WithStorageDomainId(value); }
        }

        public GetDegradedDataPersistenceRulesSpectraS3Request WithStorageDomainId(Guid storageDomainId)
        {
            this._storageDomainId = storageDomainId;
            if (storageDomainId != null) {
                this.QueryParams.Add("storage_domain_id", storageDomainId.ToString());
            }
            else
            {
                this.QueryParams.Remove("storage_domain_id");
            }
            return this;
        }

        private DataPersistenceRuleType _type;
        public DataPersistenceRuleType Type
        {
            get { return _type; }
            set { WithType(value); }
        }

        public GetDegradedDataPersistenceRulesSpectraS3Request WithType(DataPersistenceRuleType type)
        {
            this._type = type;
            if (type != null) {
                this.QueryParams.Add("type", type.ToString());
            }
            else
            {
                this.QueryParams.Remove("type");
            }
            return this;
        }

        public GetDegradedDataPersistenceRulesSpectraS3Request() {
            
        }

        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.GET
            }
        }

        internal override string Path
        {
            get
            {
                return "/_rest_/degraded_data_persistence_rule/";
            }
        }
    }
}