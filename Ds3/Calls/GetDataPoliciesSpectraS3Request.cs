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
    public class GetDataPoliciesSpectraS3Request : Ds3Request
    {
        
        
        private bool? _alwaysForcePutJobCreation;
        public bool? AlwaysForcePutJobCreation
        {
            get { return _alwaysForcePutJobCreation; }
            set { WithAlwaysForcePutJobCreation(value); }
        }

        private bool? _alwaysMinimizeSpanningAcrossMedia;
        public bool? AlwaysMinimizeSpanningAcrossMedia
        {
            get { return _alwaysMinimizeSpanningAcrossMedia; }
            set { WithAlwaysMinimizeSpanningAcrossMedia(value); }
        }

        private bool? _alwaysReplicateDeletes;
        public bool? AlwaysReplicateDeletes
        {
            get { return _alwaysReplicateDeletes; }
            set { WithAlwaysReplicateDeletes(value); }
        }

        private ChecksumType.Type? _checksumType;
        public ChecksumType.Type? ChecksumType
        {
            get { return _checksumType; }
            set { WithChecksumType(value); }
        }

        private bool? _endToEndCrcRequired;
        public bool? EndToEndCrcRequired
        {
            get { return _endToEndCrcRequired; }
            set { WithEndToEndCrcRequired(value); }
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

        
        public GetDataPoliciesSpectraS3Request WithAlwaysForcePutJobCreation(bool? alwaysForcePutJobCreation)
        {
            this._alwaysForcePutJobCreation = alwaysForcePutJobCreation;
            if (alwaysForcePutJobCreation != null)
            {
                this.QueryParams.Add("always_force_put_job_creation", alwaysForcePutJobCreation.ToString());
            }
            else
            {
                this.QueryParams.Remove("always_force_put_job_creation");
            }
            return this;
        }

        
        public GetDataPoliciesSpectraS3Request WithAlwaysMinimizeSpanningAcrossMedia(bool? alwaysMinimizeSpanningAcrossMedia)
        {
            this._alwaysMinimizeSpanningAcrossMedia = alwaysMinimizeSpanningAcrossMedia;
            if (alwaysMinimizeSpanningAcrossMedia != null)
            {
                this.QueryParams.Add("always_minimize_spanning_across_media", alwaysMinimizeSpanningAcrossMedia.ToString());
            }
            else
            {
                this.QueryParams.Remove("always_minimize_spanning_across_media");
            }
            return this;
        }

        
        public GetDataPoliciesSpectraS3Request WithAlwaysReplicateDeletes(bool? alwaysReplicateDeletes)
        {
            this._alwaysReplicateDeletes = alwaysReplicateDeletes;
            if (alwaysReplicateDeletes != null)
            {
                this.QueryParams.Add("always_replicate_deletes", alwaysReplicateDeletes.ToString());
            }
            else
            {
                this.QueryParams.Remove("always_replicate_deletes");
            }
            return this;
        }

        
        public GetDataPoliciesSpectraS3Request WithChecksumType(ChecksumType.Type? checksumType)
        {
            this._checksumType = checksumType;
            if (checksumType != null)
            {
                this.QueryParams.Add("checksum_type", checksumType.ToString());
            }
            else
            {
                this.QueryParams.Remove("checksum_type");
            }
            return this;
        }

        
        public GetDataPoliciesSpectraS3Request WithEndToEndCrcRequired(bool? endToEndCrcRequired)
        {
            this._endToEndCrcRequired = endToEndCrcRequired;
            if (endToEndCrcRequired != null)
            {
                this.QueryParams.Add("end_to_end_crc_required", endToEndCrcRequired.ToString());
            }
            else
            {
                this.QueryParams.Remove("end_to_end_crc_required");
            }
            return this;
        }

        
        public GetDataPoliciesSpectraS3Request WithLastPage(bool? lastPage)
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

        
        public GetDataPoliciesSpectraS3Request WithName(string name)
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

        
        public GetDataPoliciesSpectraS3Request WithPageLength(int? pageLength)
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

        
        public GetDataPoliciesSpectraS3Request WithPageOffset(int? pageOffset)
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

        
        public GetDataPoliciesSpectraS3Request WithPageStartMarker(Guid? pageStartMarker)
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

        
        public GetDataPoliciesSpectraS3Request WithPageStartMarker(string pageStartMarker)
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


        
        
        public GetDataPoliciesSpectraS3Request()
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
                return "/_rest_/data_policy";
            }
        }
    }
}