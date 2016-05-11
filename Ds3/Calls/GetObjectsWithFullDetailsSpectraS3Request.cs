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
    public class GetObjectsWithFullDetailsSpectraS3Request : Ds3Request
    {
        
        
        private string _bucketId;
        public string BucketId
        {
            get { return _bucketId; }
            set { WithBucketId(value); }
        }

        private string _folder;
        public string Folder
        {
            get { return _folder; }
            set { WithFolder(value); }
        }

        private bool? _includePhysicalPlacement;
        public bool? IncludePhysicalPlacement
        {
            get { return _includePhysicalPlacement; }
            set { WithIncludePhysicalPlacement(value); }
        }

        private bool? _lastPage;
        public bool? LastPage
        {
            get { return _lastPage; }
            set { WithLastPage(value); }
        }

        private bool? _latest;
        public bool? Latest
        {
            get { return _latest; }
            set { WithLatest(value); }
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

        private S3ObjectType? _type;
        public S3ObjectType? Type
        {
            get { return _type; }
            set { WithType(value); }
        }

        private long? _version;
        public long? Version
        {
            get { return _version; }
            set { WithVersion(value); }
        }

        public GetObjectsWithFullDetailsSpectraS3Request WithBucketId(Guid? bucketId)
        {
            this._bucketId = bucketId.ToString();
            if (bucketId != null) {
                this.QueryParams.Add("bucket_id", bucketId.ToString());
            }
            else
            {
                this.QueryParams.Remove("bucket_id");
            }
            return this;
        }
        public GetObjectsWithFullDetailsSpectraS3Request WithBucketId(string bucketId)
        {
            this._bucketId = bucketId;
            if (bucketId != null) {
                this.QueryParams.Add("bucket_id", bucketId);
            }
            else
            {
                this.QueryParams.Remove("bucket_id");
            }
            return this;
        }
        public GetObjectsWithFullDetailsSpectraS3Request WithFolder(string folder)
        {
            this._folder = folder;
            if (folder != null) {
                this.QueryParams.Add("folder", folder);
            }
            else
            {
                this.QueryParams.Remove("folder");
            }
            return this;
        }
        public GetObjectsWithFullDetailsSpectraS3Request WithIncludePhysicalPlacement(bool? includePhysicalPlacement)
        {
            this._includePhysicalPlacement = includePhysicalPlacement;
            if (includePhysicalPlacement != null) {
                this.QueryParams.Add("include_physical_placement", includePhysicalPlacement.ToString());
            }
            else
            {
                this.QueryParams.Remove("include_physical_placement");
            }
            return this;
        }
        public GetObjectsWithFullDetailsSpectraS3Request WithLastPage(bool? lastPage)
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
        public GetObjectsWithFullDetailsSpectraS3Request WithLatest(bool? latest)
        {
            this._latest = latest;
            if (latest != null) {
                this.QueryParams.Add("latest", latest.ToString());
            }
            else
            {
                this.QueryParams.Remove("latest");
            }
            return this;
        }
        public GetObjectsWithFullDetailsSpectraS3Request WithName(string name)
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
        public GetObjectsWithFullDetailsSpectraS3Request WithPageLength(int? pageLength)
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
        public GetObjectsWithFullDetailsSpectraS3Request WithPageOffset(int? pageOffset)
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
        public GetObjectsWithFullDetailsSpectraS3Request WithPageStartMarker(Guid? pageStartMarker)
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
        public GetObjectsWithFullDetailsSpectraS3Request WithPageStartMarker(string pageStartMarker)
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
        public GetObjectsWithFullDetailsSpectraS3Request WithType(S3ObjectType? type)
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
        public GetObjectsWithFullDetailsSpectraS3Request WithVersion(long? version)
        {
            this._version = version;
            if (version != null) {
                this.QueryParams.Add("version", version.ToString());
            }
            else
            {
                this.QueryParams.Remove("version");
            }
            return this;
        }

        
        public GetObjectsWithFullDetailsSpectraS3Request() {
            
            this.QueryParams.Add("full_details", null);

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
                return "/_rest_/object";
            }
        }
    }
}