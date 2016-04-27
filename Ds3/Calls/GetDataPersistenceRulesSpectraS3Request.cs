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
using Ds3.Models;
using System;
using System.Net;

namespace Ds3.Calls
{
    public class GetDataPersistenceRulesSpectraS3Request : Ds3Request
    {
        
        
        private Guid _dataPolicyId;
        public Guid DataPolicyId
        {
            get { return _dataPolicyId; }
            set { WithDataPolicyId(value); }
        }

        public GetDataPersistenceRulesSpectraS3Request WithDataPolicyId(Guid dataPolicyId)
        {
            this._dataPolicyId = dataPolicyId;
            if (dataPolicyId != null) {
                this.QueryParams.Add("data_policy_id", DataPolicyId.ToString());
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

        public GetDataPersistenceRulesSpectraS3Request WithIsolationLevel(DataIsolationLevel isolationLevel)
        {
            this._isolationLevel = isolationLevel;
            if (isolationLevel != null) {
                this.QueryParams.Add("isolation_level", IsolationLevel.ToString());
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

        public GetDataPersistenceRulesSpectraS3Request WithLastPage(bool lastPage)
        {
            this._lastPage = lastPage;
            if (lastPage != null) {
                this.QueryParams.Add("last_page", LastPage.ToString());
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

        public GetDataPersistenceRulesSpectraS3Request WithPageLength(int pageLength)
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

        public GetDataPersistenceRulesSpectraS3Request WithPageOffset(int pageOffset)
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

        public GetDataPersistenceRulesSpectraS3Request WithPageStartMarker(Guid pageStartMarker)
        {
            this._pageStartMarker = pageStartMarker;
            if (pageStartMarker != null) {
                this.QueryParams.Add("page_start_marker", PageStartMarker.ToString());
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

        public GetDataPersistenceRulesSpectraS3Request WithState(DataPersistenceRuleState state)
        {
            this._state = state;
            if (state != null) {
                this.QueryParams.Add("state", State.ToString());
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

        public GetDataPersistenceRulesSpectraS3Request WithStorageDomainId(Guid storageDomainId)
        {
            this._storageDomainId = storageDomainId;
            if (storageDomainId != null) {
                this.QueryParams.Add("storage_domain_id", StorageDomainId.ToString());
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

        public GetDataPersistenceRulesSpectraS3Request WithType(DataPersistenceRuleType type)
        {
            this._type = type;
            if (type != null) {
                this.QueryParams.Add("type", Type.ToString());
            }
            else
            {
                this.QueryParams.Remove("type");
            }
            return this;
        }

        public GetDataPersistenceRulesSpectraS3Request() {
            
        }

        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.GET;
            }
        }

        internal override string Path
        {
            get
            {
                return "/_rest_/data_persistence_rule/";
            }
        }
    }
}