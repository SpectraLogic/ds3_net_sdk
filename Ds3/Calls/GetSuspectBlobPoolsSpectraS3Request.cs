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
    public class GetSuspectBlobPoolsSpectraS3Request : Ds3Request
    {
        
        
        private string _blobId;
        public string BlobId
        {
            get { return _blobId; }
            set { WithBlobId(value); }
        }

        private bool? _lastPage;
        public bool? LastPage
        {
            get { return _lastPage; }
            set { WithLastPage(value); }
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

        private string _poolId;
        public string PoolId
        {
            get { return _poolId; }
            set { WithPoolId(value); }
        }

        public GetSuspectBlobPoolsSpectraS3Request WithBlobId(Guid? blobId)
        {
            this._blobId = blobId.ToString();
            if (blobId != null)
            {
                this.QueryParams.Add("blob_id", blobId.ToString());
            }
            else
            {
                this.QueryParams.Remove("blob_id");
            }
            return this;
        }
        public GetSuspectBlobPoolsSpectraS3Request WithBlobId(string blobId)
        {
            this._blobId = blobId;
            if (blobId != null)
            {
                this.QueryParams.Add("blob_id", blobId);
            }
            else
            {
                this.QueryParams.Remove("blob_id");
            }
            return this;
        }
        public GetSuspectBlobPoolsSpectraS3Request WithLastPage(bool? lastPage)
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
        public GetSuspectBlobPoolsSpectraS3Request WithPageLength(int? pageLength)
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
        public GetSuspectBlobPoolsSpectraS3Request WithPageOffset(int? pageOffset)
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
        public GetSuspectBlobPoolsSpectraS3Request WithPageStartMarker(Guid? pageStartMarker)
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
        public GetSuspectBlobPoolsSpectraS3Request WithPageStartMarker(string pageStartMarker)
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
        public GetSuspectBlobPoolsSpectraS3Request WithPoolId(Guid? poolId)
        {
            this._poolId = poolId.ToString();
            if (poolId != null)
            {
                this.QueryParams.Add("pool_id", poolId.ToString());
            }
            else
            {
                this.QueryParams.Remove("pool_id");
            }
            return this;
        }
        public GetSuspectBlobPoolsSpectraS3Request WithPoolId(string poolId)
        {
            this._poolId = poolId;
            if (poolId != null)
            {
                this.QueryParams.Add("pool_id", poolId);
            }
            else
            {
                this.QueryParams.Remove("pool_id");
            }
            return this;
        }

        
        public GetSuspectBlobPoolsSpectraS3Request()
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
                return "/_rest_/suspect_blob_pool";
            }
        }
    }
}