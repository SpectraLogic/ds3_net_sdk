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
    public class GetUsersSpectraS3Request : Ds3Request
    {
        
        
        private string _authId;
        public string AuthId
        {
            get { return _authId; }
            set { WithAuthId(value); }
        }

        private string _defaultDataPolicyId;
        public string DefaultDataPolicyId
        {
            get { return _defaultDataPolicyId; }
            set { WithDefaultDataPolicyId(value); }
        }

        private bool? _lastPage;
        public bool? LastPage
        {
            get { return _lastPage; }
            set { WithLastPage(value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { WithName(value); }
        }

        private int? _pageLength;
        public int? PageLength
        {
            get { return _pageLength; }
            set { WithPageLength(value); }
        }

        private int? _pageOffset;
        public int? PageOffset
        {
            get { return _pageOffset; }
            set { WithPageOffset(value); }
        }

        private string _pageStartMarker;
        public string PageStartMarker
        {
            get { return _pageStartMarker; }
            set { WithPageStartMarker(value); }
        }

        public GetUsersSpectraS3Request WithAuthId(string authId)
        {
            this._authId = authId;
            if (authId != null)
            {
                this.QueryParams.Add("auth_id", authId);
            }
            else
            {
                this.QueryParams.Remove("auth_id");
            }
            return this;
        }
        public GetUsersSpectraS3Request WithDefaultDataPolicyId(Guid? defaultDataPolicyId)
        {
            this._defaultDataPolicyId = defaultDataPolicyId.ToString();
            if (defaultDataPolicyId != null)
            {
                this.QueryParams.Add("default_data_policy_id", defaultDataPolicyId.ToString());
            }
            else
            {
                this.QueryParams.Remove("default_data_policy_id");
            }
            return this;
        }
        public GetUsersSpectraS3Request WithDefaultDataPolicyId(string defaultDataPolicyId)
        {
            this._defaultDataPolicyId = defaultDataPolicyId;
            if (defaultDataPolicyId != null)
            {
                this.QueryParams.Add("default_data_policy_id", defaultDataPolicyId);
            }
            else
            {
                this.QueryParams.Remove("default_data_policy_id");
            }
            return this;
        }
        public GetUsersSpectraS3Request WithLastPage(bool? lastPage)
        {
            this._lastPage = lastPage;
            if (lastPage != null)
            {
                this.QueryParams.Add("last_page", lastPage.ToString());
            }
            else
            {
                this.QueryParams.Remove("last_page");
            }
            return this;
        }
        public GetUsersSpectraS3Request WithName(string name)
        {
            this._name = name;
            if (name != null)
            {
                this.QueryParams.Add("name", name);
            }
            else
            {
                this.QueryParams.Remove("name");
            }
            return this;
        }
        public GetUsersSpectraS3Request WithPageLength(int? pageLength)
        {
            this._pageLength = pageLength;
            if (pageLength != null)
            {
                this.QueryParams.Add("page_length", pageLength.ToString());
            }
            else
            {
                this.QueryParams.Remove("page_length");
            }
            return this;
        }
        public GetUsersSpectraS3Request WithPageOffset(int? pageOffset)
        {
            this._pageOffset = pageOffset;
            if (pageOffset != null)
            {
                this.QueryParams.Add("page_offset", pageOffset.ToString());
            }
            else
            {
                this.QueryParams.Remove("page_offset");
            }
            return this;
        }
        public GetUsersSpectraS3Request WithPageStartMarker(Guid? pageStartMarker)
        {
            this._pageStartMarker = pageStartMarker.ToString();
            if (pageStartMarker != null)
            {
                this.QueryParams.Add("page_start_marker", pageStartMarker.ToString());
            }
            else
            {
                this.QueryParams.Remove("page_start_marker");
            }
            return this;
        }
        public GetUsersSpectraS3Request WithPageStartMarker(string pageStartMarker)
        {
            this._pageStartMarker = pageStartMarker;
            if (pageStartMarker != null)
            {
                this.QueryParams.Add("page_start_marker", pageStartMarker);
            }
            else
            {
                this.QueryParams.Remove("page_start_marker");
            }
            return this;
        }

        
        public GetUsersSpectraS3Request()
        {
            
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
                return "/_rest_/user";
            }
        }
    }
}