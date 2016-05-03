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
    public class GetCompletedJobsSpectraS3Request : Ds3Request
    {
        
        
        private Guid _bucketId;
        public Guid BucketId
        {
            get { return _bucketId; }
            set { WithBucketId(value); }
        }

        public GetCompletedJobsSpectraS3Request WithBucketId(Guid bucketId)
        {
            this._bucketId = bucketId;
            if (bucketId != null) {
                this.QueryParams.Add("bucket_id", BucketId.ToString());
            }
            else
            {
                this.QueryParams.Remove("bucket_id");
            }
            return this;
        }

        private JobChunkClientProcessingOrderGuarantee _chunkClientProcessingOrderGuarantee;
        public JobChunkClientProcessingOrderGuarantee ChunkClientProcessingOrderGuarantee
        {
            get { return _chunkClientProcessingOrderGuarantee; }
            set { WithChunkClientProcessingOrderGuarantee(value); }
        }

        public GetCompletedJobsSpectraS3Request WithChunkClientProcessingOrderGuarantee(JobChunkClientProcessingOrderGuarantee chunkClientProcessingOrderGuarantee)
        {
            this._chunkClientProcessingOrderGuarantee = chunkClientProcessingOrderGuarantee;
            if (chunkClientProcessingOrderGuarantee != null) {
                this.QueryParams.Add("chunk_client_processing_order_guarantee", ChunkClientProcessingOrderGuarantee.ToString());
            }
            else
            {
                this.QueryParams.Remove("chunk_client_processing_order_guarantee");
            }
            return this;
        }

        private bool _lastPage;
        public bool LastPage
        {
            get { return _lastPage; }
            set { WithLastPage(value); }
        }

        public GetCompletedJobsSpectraS3Request WithLastPage(bool lastPage)
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

        private string _name;
        public string Name
        {
            get { return _name; }
            set { WithName(value); }
        }

        public GetCompletedJobsSpectraS3Request WithName(string name)
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

        public GetCompletedJobsSpectraS3Request WithPageLength(int pageLength)
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

        public GetCompletedJobsSpectraS3Request WithPageOffset(int pageOffset)
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

        public GetCompletedJobsSpectraS3Request WithPageStartMarker(Guid pageStartMarker)
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

        private Priority _priority;
        public Priority Priority
        {
            get { return _priority; }
            set { WithPriority(value); }
        }

        public GetCompletedJobsSpectraS3Request WithPriority(Priority priority)
        {
            this._priority = priority;
            if (priority != null) {
                this.QueryParams.Add("priority", Priority.ToString());
            }
            else
            {
                this.QueryParams.Remove("priority");
            }
            return this;
        }

        private DateTime _rechunked;
        public DateTime Rechunked
        {
            get { return _rechunked; }
            set { WithRechunked(value); }
        }

        public GetCompletedJobsSpectraS3Request WithRechunked(DateTime rechunked)
        {
            this._rechunked = rechunked;
            if (rechunked != null) {
                this.QueryParams.Add("rechunked", Rechunked.ToString());
            }
            else
            {
                this.QueryParams.Remove("rechunked");
            }
            return this;
        }

        private JobRequestType _requestType;
        public JobRequestType RequestType
        {
            get { return _requestType; }
            set { WithRequestType(value); }
        }

        public GetCompletedJobsSpectraS3Request WithRequestType(JobRequestType requestType)
        {
            this._requestType = requestType;
            if (requestType != null) {
                this.QueryParams.Add("request_type", RequestType.ToString());
            }
            else
            {
                this.QueryParams.Remove("request_type");
            }
            return this;
        }

        private bool _truncated;
        public bool Truncated
        {
            get { return _truncated; }
            set { WithTruncated(value); }
        }

        public GetCompletedJobsSpectraS3Request WithTruncated(bool truncated)
        {
            this._truncated = truncated;
            if (truncated != null) {
                this.QueryParams.Add("truncated", Truncated.ToString());
            }
            else
            {
                this.QueryParams.Remove("truncated");
            }
            return this;
        }

        private Guid _userId;
        public Guid UserId
        {
            get { return _userId; }
            set { WithUserId(value); }
        }

        public GetCompletedJobsSpectraS3Request WithUserId(Guid userId)
        {
            this._userId = userId;
            if (userId != null) {
                this.QueryParams.Add("user_id", UserId.ToString());
            }
            else
            {
                this.QueryParams.Remove("user_id");
            }
            return this;
        }

        public GetCompletedJobsSpectraS3Request() {
            
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
                return "/_rest_/completed_job/";
            }
        }
    }
}