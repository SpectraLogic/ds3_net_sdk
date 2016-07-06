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
    public class GetDs3TargetsSpectraS3Request : Ds3Request
    {
        
        
        private string _adminAuthId;
        public string AdminAuthId
        {
            get { return _adminAuthId; }
            set { WithAdminAuthId(value); }
        }

        private string _dataPathEndPoint;
        public string DataPathEndPoint
        {
            get { return _dataPathEndPoint; }
            set { WithDataPathEndPoint(value); }
        }

        private bool? _dataPathHttps;
        public bool? DataPathHttps
        {
            get { return _dataPathHttps; }
            set { WithDataPathHttps(value); }
        }

        private int? _dataPathPort;
        public int? DataPathPort
        {
            get { return _dataPathPort; }
            set { WithDataPathPort(value); }
        }

        private string _dataPathProxy;
        public string DataPathProxy
        {
            get { return _dataPathProxy; }
            set { WithDataPathProxy(value); }
        }

        private bool? _dataPathVerifyCertificate;
        public bool? DataPathVerifyCertificate
        {
            get { return _dataPathVerifyCertificate; }
            set { WithDataPathVerifyCertificate(value); }
        }

        private TargetReadPreference? _defaultReadPreference;
        public TargetReadPreference? DefaultReadPreference
        {
            get { return _defaultReadPreference; }
            set { WithDefaultReadPreference(value); }
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

        private bool? _permitGoingOutOfSync;
        public bool? PermitGoingOutOfSync
        {
            get { return _permitGoingOutOfSync; }
            set { WithPermitGoingOutOfSync(value); }
        }

        private Quiesced? _quiesced;
        public Quiesced? Quiesced
        {
            get { return _quiesced; }
            set { WithQuiesced(value); }
        }

        private TargetState? _state;
        public TargetState? State
        {
            get { return _state; }
            set { WithState(value); }
        }

        public GetDs3TargetsSpectraS3Request WithAdminAuthId(string adminAuthId)
        {
            this._adminAuthId = adminAuthId;
            if (adminAuthId != null) {
                this.QueryParams.Add("admin_auth_id", adminAuthId);
            }
            else
            {
                this.QueryParams.Remove("admin_auth_id");
            }
            return this;
        }
        public GetDs3TargetsSpectraS3Request WithDataPathEndPoint(string dataPathEndPoint)
        {
            this._dataPathEndPoint = dataPathEndPoint;
            if (dataPathEndPoint != null) {
                this.QueryParams.Add("data_path_end_point", dataPathEndPoint);
            }
            else
            {
                this.QueryParams.Remove("data_path_end_point");
            }
            return this;
        }
        public GetDs3TargetsSpectraS3Request WithDataPathHttps(bool? dataPathHttps)
        {
            this._dataPathHttps = dataPathHttps;
            if (dataPathHttps != null) {
                this.QueryParams.Add("data_path_https", dataPathHttps.ToString());
            }
            else
            {
                this.QueryParams.Remove("data_path_https");
            }
            return this;
        }
        public GetDs3TargetsSpectraS3Request WithDataPathPort(int? dataPathPort)
        {
            this._dataPathPort = dataPathPort;
            if (dataPathPort != null) {
                this.QueryParams.Add("data_path_port", dataPathPort.ToString());
            }
            else
            {
                this.QueryParams.Remove("data_path_port");
            }
            return this;
        }
        public GetDs3TargetsSpectraS3Request WithDataPathProxy(string dataPathProxy)
        {
            this._dataPathProxy = dataPathProxy;
            if (dataPathProxy != null) {
                this.QueryParams.Add("data_path_proxy", dataPathProxy);
            }
            else
            {
                this.QueryParams.Remove("data_path_proxy");
            }
            return this;
        }
        public GetDs3TargetsSpectraS3Request WithDataPathVerifyCertificate(bool? dataPathVerifyCertificate)
        {
            this._dataPathVerifyCertificate = dataPathVerifyCertificate;
            if (dataPathVerifyCertificate != null) {
                this.QueryParams.Add("data_path_verify_certificate", dataPathVerifyCertificate.ToString());
            }
            else
            {
                this.QueryParams.Remove("data_path_verify_certificate");
            }
            return this;
        }
        public GetDs3TargetsSpectraS3Request WithDefaultReadPreference(TargetReadPreference? defaultReadPreference)
        {
            this._defaultReadPreference = defaultReadPreference;
            if (defaultReadPreference != null) {
                this.QueryParams.Add("default_read_preference", defaultReadPreference.ToString());
            }
            else
            {
                this.QueryParams.Remove("default_read_preference");
            }
            return this;
        }
        public GetDs3TargetsSpectraS3Request WithLastPage(bool? lastPage)
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
        public GetDs3TargetsSpectraS3Request WithName(string name)
        {
            this._name = name;
            if (name != null) {
                this.QueryParams.Add("name", name);
            }
            else
            {
                this.QueryParams.Remove("name");
            }
            return this;
        }
        public GetDs3TargetsSpectraS3Request WithPageLength(int? pageLength)
        {
            this._pageLength = pageLength;
            if (pageLength != null) {
                this.QueryParams.Add("page_length", pageLength.ToString());
            }
            else
            {
                this.QueryParams.Remove("page_length");
            }
            return this;
        }
        public GetDs3TargetsSpectraS3Request WithPageOffset(int? pageOffset)
        {
            this._pageOffset = pageOffset;
            if (pageOffset != null) {
                this.QueryParams.Add("page_offset", pageOffset.ToString());
            }
            else
            {
                this.QueryParams.Remove("page_offset");
            }
            return this;
        }
        public GetDs3TargetsSpectraS3Request WithPageStartMarker(Guid? pageStartMarker)
        {
            this._pageStartMarker = pageStartMarker.ToString();
            if (pageStartMarker != null) {
                this.QueryParams.Add("page_start_marker", pageStartMarker.ToString());
            }
            else
            {
                this.QueryParams.Remove("page_start_marker");
            }
            return this;
        }
        public GetDs3TargetsSpectraS3Request WithPageStartMarker(string pageStartMarker)
        {
            this._pageStartMarker = pageStartMarker;
            if (pageStartMarker != null) {
                this.QueryParams.Add("page_start_marker", pageStartMarker);
            }
            else
            {
                this.QueryParams.Remove("page_start_marker");
            }
            return this;
        }
        public GetDs3TargetsSpectraS3Request WithPermitGoingOutOfSync(bool? permitGoingOutOfSync)
        {
            this._permitGoingOutOfSync = permitGoingOutOfSync;
            if (permitGoingOutOfSync != null) {
                this.QueryParams.Add("permit_going_out_of_sync", permitGoingOutOfSync.ToString());
            }
            else
            {
                this.QueryParams.Remove("permit_going_out_of_sync");
            }
            return this;
        }
        public GetDs3TargetsSpectraS3Request WithQuiesced(Quiesced? quiesced)
        {
            this._quiesced = quiesced;
            if (quiesced != null) {
                this.QueryParams.Add("quiesced", quiesced.ToString());
            }
            else
            {
                this.QueryParams.Remove("quiesced");
            }
            return this;
        }
        public GetDs3TargetsSpectraS3Request WithState(TargetState? state)
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

        
        public GetDs3TargetsSpectraS3Request() {
            
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
                return "/_rest_/ds3_target";
            }
        }
    }
}