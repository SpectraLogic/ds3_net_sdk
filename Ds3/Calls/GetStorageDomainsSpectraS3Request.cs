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
    public class GetStorageDomainsSpectraS3Request : Ds3Request
    {
        
        
        private string _autoEjectUponCron;
        public string AutoEjectUponCron
        {
            get { return _autoEjectUponCron; }
            set { WithAutoEjectUponCron(value); }
        }

        private bool? _autoEjectUponJobCancellation;
        public bool? AutoEjectUponJobCancellation
        {
            get { return _autoEjectUponJobCancellation; }
            set { WithAutoEjectUponJobCancellation(value); }
        }

        private bool? _autoEjectUponJobCompletion;
        public bool? AutoEjectUponJobCompletion
        {
            get { return _autoEjectUponJobCompletion; }
            set { WithAutoEjectUponJobCompletion(value); }
        }

        private bool? _autoEjectUponMediaFull;
        public bool? AutoEjectUponMediaFull
        {
            get { return _autoEjectUponMediaFull; }
            set { WithAutoEjectUponMediaFull(value); }
        }

        private bool? _lastPage;
        public bool? LastPage
        {
            get { return _lastPage; }
            set { WithLastPage(value); }
        }

        private bool? _mediaEjectionAllowed;
        public bool? MediaEjectionAllowed
        {
            get { return _mediaEjectionAllowed; }
            set { WithMediaEjectionAllowed(value); }
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

        private WriteOptimization? _writeOptimization;
        public WriteOptimization? WriteOptimization
        {
            get { return _writeOptimization; }
            set { WithWriteOptimization(value); }
        }

        public GetStorageDomainsSpectraS3Request WithAutoEjectUponCron(string autoEjectUponCron)
        {
            this._autoEjectUponCron = autoEjectUponCron;
            if (autoEjectUponCron != null) {
                this.QueryParams.Add("auto_eject_upon_cron", autoEjectUponCron);
            }
            else
            {
                this.QueryParams.Remove("auto_eject_upon_cron");
            }
            return this;
        }
        public GetStorageDomainsSpectraS3Request WithAutoEjectUponJobCancellation(bool? autoEjectUponJobCancellation)
        {
            this._autoEjectUponJobCancellation = autoEjectUponJobCancellation;
            if (autoEjectUponJobCancellation != null) {
                this.QueryParams.Add("auto_eject_upon_job_cancellation", autoEjectUponJobCancellation.ToString());
            }
            else
            {
                this.QueryParams.Remove("auto_eject_upon_job_cancellation");
            }
            return this;
        }
        public GetStorageDomainsSpectraS3Request WithAutoEjectUponJobCompletion(bool? autoEjectUponJobCompletion)
        {
            this._autoEjectUponJobCompletion = autoEjectUponJobCompletion;
            if (autoEjectUponJobCompletion != null) {
                this.QueryParams.Add("auto_eject_upon_job_completion", autoEjectUponJobCompletion.ToString());
            }
            else
            {
                this.QueryParams.Remove("auto_eject_upon_job_completion");
            }
            return this;
        }
        public GetStorageDomainsSpectraS3Request WithAutoEjectUponMediaFull(bool? autoEjectUponMediaFull)
        {
            this._autoEjectUponMediaFull = autoEjectUponMediaFull;
            if (autoEjectUponMediaFull != null) {
                this.QueryParams.Add("auto_eject_upon_media_full", autoEjectUponMediaFull.ToString());
            }
            else
            {
                this.QueryParams.Remove("auto_eject_upon_media_full");
            }
            return this;
        }
        public GetStorageDomainsSpectraS3Request WithLastPage(bool? lastPage)
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
        public GetStorageDomainsSpectraS3Request WithMediaEjectionAllowed(bool? mediaEjectionAllowed)
        {
            this._mediaEjectionAllowed = mediaEjectionAllowed;
            if (mediaEjectionAllowed != null) {
                this.QueryParams.Add("media_ejection_allowed", mediaEjectionAllowed.ToString());
            }
            else
            {
                this.QueryParams.Remove("media_ejection_allowed");
            }
            return this;
        }
        public GetStorageDomainsSpectraS3Request WithName(string name)
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
        public GetStorageDomainsSpectraS3Request WithPageLength(int? pageLength)
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
        public GetStorageDomainsSpectraS3Request WithPageOffset(int? pageOffset)
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
        public GetStorageDomainsSpectraS3Request WithPageStartMarker(Guid? pageStartMarker)
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
        public GetStorageDomainsSpectraS3Request WithPageStartMarker(string pageStartMarker)
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
        public GetStorageDomainsSpectraS3Request WithWriteOptimization(WriteOptimization? writeOptimization)
        {
            this._writeOptimization = writeOptimization;
            if (writeOptimization != null) {
                this.QueryParams.Add("write_optimization", writeOptimization.ToString());
            }
            else
            {
                this.QueryParams.Remove("write_optimization");
            }
            return this;
        }

        
        public GetStorageDomainsSpectraS3Request() {
            
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
                return "/_rest_/storage_domain/";
            }
        }
    }
}