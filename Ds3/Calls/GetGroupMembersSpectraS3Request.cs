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
    public class GetGroupMembersSpectraS3Request : Ds3Request
    {
        
        
        private Guid _groupId;
        public Guid GroupId
        {
            get { return _groupId; }
            set { WithGroupId(value); }
        }

        public GetGroupMembersSpectraS3Request WithGroupId(Guid groupId)
        {
            this._groupId = groupId;
            if (groupId != null) {
                this.QueryParams.Add("group_id", GroupId.ToString());
            }
            else
            {
                this.QueryParams.Remove("group_id");
            }
            return this;
        }

        private bool _lastPage;
        public bool LastPage
        {
            get { return _lastPage; }
            set { WithLastPage(value); }
        }

        public GetGroupMembersSpectraS3Request WithLastPage(bool lastPage)
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

        private Guid _memberGroupId;
        public Guid MemberGroupId
        {
            get { return _memberGroupId; }
            set { WithMemberGroupId(value); }
        }

        public GetGroupMembersSpectraS3Request WithMemberGroupId(Guid memberGroupId)
        {
            this._memberGroupId = memberGroupId;
            if (memberGroupId != null) {
                this.QueryParams.Add("member_group_id", MemberGroupId.ToString());
            }
            else
            {
                this.QueryParams.Remove("member_group_id");
            }
            return this;
        }

        private Guid _memberUserId;
        public Guid MemberUserId
        {
            get { return _memberUserId; }
            set { WithMemberUserId(value); }
        }

        public GetGroupMembersSpectraS3Request WithMemberUserId(Guid memberUserId)
        {
            this._memberUserId = memberUserId;
            if (memberUserId != null) {
                this.QueryParams.Add("member_user_id", MemberUserId.ToString());
            }
            else
            {
                this.QueryParams.Remove("member_user_id");
            }
            return this;
        }

        private int _pageLength;
        public int PageLength
        {
            get { return _pageLength; }
            set { WithPageLength(value); }
        }

        public GetGroupMembersSpectraS3Request WithPageLength(int pageLength)
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

        public GetGroupMembersSpectraS3Request WithPageOffset(int pageOffset)
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

        public GetGroupMembersSpectraS3Request WithPageStartMarker(Guid pageStartMarker)
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

        public GetGroupMembersSpectraS3Request() {
            
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
                return "/_rest_/group_member/";
            }
        }
    }
}