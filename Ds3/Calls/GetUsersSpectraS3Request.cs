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
    public class GetUsersSpectraS3Request : Ds3Request
    {
        
        
        private string _authId;
        public string AuthId
        {
            get { return _authId; }
            set { WithAuthId(value); }
        }

        public GetUsersSpectraS3Request WithAuthId(string authId)
        {
            this._authId = authId;
            if (authId != null) {
                this.QueryParams.Add("auth_id", AuthId);
            }
            else
            {
                this.QueryParams.Remove("auth_id");
            }
            return this;
        }

        private Guid _defaultDataPolicyId;
        public Guid DefaultDataPolicyId
        {
            get { return _defaultDataPolicyId; }
            set { WithDefaultDataPolicyId(value); }
        }

        public GetUsersSpectraS3Request WithDefaultDataPolicyId(Guid defaultDataPolicyId)
        {
            this._defaultDataPolicyId = defaultDataPolicyId;
            if (defaultDataPolicyId != null) {
                this.QueryParams.Add("default_data_policy_id", defaultDataPolicyId.ToString());
            }
            else
            {
                this.QueryParams.Remove("default_data_policy_id");
            }
            return this;
        }

        private bool _lastPage;
        public bool LastPage
        {
            get { return _lastPage; }
            set { WithLastPage(value); }
        }

        public GetUsersSpectraS3Request WithLastPage(bool lastPage)
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

        private string _name;
        public string Name
        {
            get { return _name; }
            set { WithName(value); }
        }

        public GetUsersSpectraS3Request WithName(string name)
        {
            this._name = name;
            if (name != null) {
                this.QueryParams.Add("name", Name);
            }
            else
            {
                this.QueryParams.Remove("name");
            }
            return this;
        }

        private int _pageLength;
        public int PageLength
        {
            get { return _pageLength; }
            set { WithPageLength(value); }
        }

        public GetUsersSpectraS3Request WithPageLength(int pageLength)
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

        public GetUsersSpectraS3Request WithPageOffset(int pageOffset)
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

        public GetUsersSpectraS3Request WithPageStartMarker(Guid pageStartMarker)
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

        public GetUsersSpectraS3Request() {
            
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
                return "/_rest_/user/";
            }
        }
    }
}