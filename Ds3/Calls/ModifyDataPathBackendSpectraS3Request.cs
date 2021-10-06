/*
 * ******************************************************************************
 *   Copyright 2014-2017 Spectra Logic Corporation. All Rights Reserved.
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
    public class ModifyDataPathBackendSpectraS3Request : Ds3Request
    {
        
        
        private bool? _activated;
        public bool? Activated
        {
            get { return _activated; }
            set { WithActivated(value); }
        }

        private bool? _allowNewJobRequests;
        public bool? AllowNewJobRequests
        {
            get { return _allowNewJobRequests; }
            set { WithAllowNewJobRequests(value); }
        }

        private int? _autoActivateTimeoutInMins;
        public int? AutoActivateTimeoutInMins
        {
            get { return _autoActivateTimeoutInMins; }
            set { WithAutoActivateTimeoutInMins(value); }
        }

        private AutoInspectMode? _autoInspect;
        public AutoInspectMode? AutoInspect
        {
            get { return _autoInspect; }
            set { WithAutoInspect(value); }
        }

        private int? _cacheAvailableRetryAfterInSeconds;
        public int? CacheAvailableRetryAfterInSeconds
        {
            get { return _cacheAvailableRetryAfterInSeconds; }
            set { WithCacheAvailableRetryAfterInSeconds(value); }
        }

        private Priority? _defaultVerifyDataAfterImport;
        public Priority? DefaultVerifyDataAfterImport
        {
            get { return _defaultVerifyDataAfterImport; }
            set { WithDefaultVerifyDataAfterImport(value); }
        }

        private bool? _defaultVerifyDataPriorToImport;
        public bool? DefaultVerifyDataPriorToImport
        {
            get { return _defaultVerifyDataPriorToImport; }
            set { WithDefaultVerifyDataPriorToImport(value); }
        }

        private double? _iomCacheLimitationPercent;
        public double? IomCacheLimitationPercent
        {
            get { return _iomCacheLimitationPercent; }
            set { WithIomCacheLimitationPercent(value); }
        }

        private bool? _iomEnabled;
        public bool? IomEnabled
        {
            get { return _iomEnabled; }
            set { WithIomEnabled(value); }
        }

        private int? _maxAggregatedBlobsPerChunk;
        public int? MaxAggregatedBlobsPerChunk
        {
            get { return _maxAggregatedBlobsPerChunk; }
            set { WithMaxAggregatedBlobsPerChunk(value); }
        }

        private int? _partiallyVerifyLastPercentOfTapes;
        public int? PartiallyVerifyLastPercentOfTapes
        {
            get { return _partiallyVerifyLastPercentOfTapes; }
            set { WithPartiallyVerifyLastPercentOfTapes(value); }
        }

        private UnavailableMediaUsagePolicy? _unavailableMediaPolicy;
        public UnavailableMediaUsagePolicy? UnavailableMediaPolicy
        {
            get { return _unavailableMediaPolicy; }
            set { WithUnavailableMediaPolicy(value); }
        }

        private int? _unavailablePoolMaxJobRetryInMins;
        public int? UnavailablePoolMaxJobRetryInMins
        {
            get { return _unavailablePoolMaxJobRetryInMins; }
            set { WithUnavailablePoolMaxJobRetryInMins(value); }
        }

        private int? _unavailableTapePartitionMaxJobRetryInMins;
        public int? UnavailableTapePartitionMaxJobRetryInMins
        {
            get { return _unavailableTapePartitionMaxJobRetryInMins; }
            set { WithUnavailableTapePartitionMaxJobRetryInMins(value); }
        }

        
        public ModifyDataPathBackendSpectraS3Request WithActivated(bool? activated)
        {
            this._activated = activated;
            if (activated != null)
            {
                this.QueryParams.Add("activated", activated.ToString());
            }
            else
            {
                this.QueryParams.Remove("activated");
            }
            return this;
        }

        
        public ModifyDataPathBackendSpectraS3Request WithAllowNewJobRequests(bool? allowNewJobRequests)
        {
            this._allowNewJobRequests = allowNewJobRequests;
            if (allowNewJobRequests != null)
            {
                this.QueryParams.Add("allow_new_job_requests", allowNewJobRequests.ToString());
            }
            else
            {
                this.QueryParams.Remove("allow_new_job_requests");
            }
            return this;
        }

        
        public ModifyDataPathBackendSpectraS3Request WithAutoActivateTimeoutInMins(int? autoActivateTimeoutInMins)
        {
            this._autoActivateTimeoutInMins = autoActivateTimeoutInMins;
            if (autoActivateTimeoutInMins != null)
            {
                this.QueryParams.Add("auto_activate_timeout_in_mins", autoActivateTimeoutInMins.ToString());
            }
            else
            {
                this.QueryParams.Remove("auto_activate_timeout_in_mins");
            }
            return this;
        }

        
        public ModifyDataPathBackendSpectraS3Request WithAutoInspect(AutoInspectMode? autoInspect)
        {
            this._autoInspect = autoInspect;
            if (autoInspect != null)
            {
                this.QueryParams.Add("auto_inspect", autoInspect.ToString());
            }
            else
            {
                this.QueryParams.Remove("auto_inspect");
            }
            return this;
        }

        
        public ModifyDataPathBackendSpectraS3Request WithCacheAvailableRetryAfterInSeconds(int? cacheAvailableRetryAfterInSeconds)
        {
            this._cacheAvailableRetryAfterInSeconds = cacheAvailableRetryAfterInSeconds;
            if (cacheAvailableRetryAfterInSeconds != null)
            {
                this.QueryParams.Add("cache_available_retry_after_in_seconds", cacheAvailableRetryAfterInSeconds.ToString());
            }
            else
            {
                this.QueryParams.Remove("cache_available_retry_after_in_seconds");
            }
            return this;
        }

        
        public ModifyDataPathBackendSpectraS3Request WithDefaultVerifyDataAfterImport(Priority? defaultVerifyDataAfterImport)
        {
            this._defaultVerifyDataAfterImport = defaultVerifyDataAfterImport;
            if (defaultVerifyDataAfterImport != null)
            {
                this.QueryParams.Add("default_verify_data_after_import", defaultVerifyDataAfterImport.ToString());
            }
            else
            {
                this.QueryParams.Remove("default_verify_data_after_import");
            }
            return this;
        }

        
        public ModifyDataPathBackendSpectraS3Request WithDefaultVerifyDataPriorToImport(bool? defaultVerifyDataPriorToImport)
        {
            this._defaultVerifyDataPriorToImport = defaultVerifyDataPriorToImport;
            if (defaultVerifyDataPriorToImport != null)
            {
                this.QueryParams.Add("default_verify_data_prior_to_import", defaultVerifyDataPriorToImport.ToString());
            }
            else
            {
                this.QueryParams.Remove("default_verify_data_prior_to_import");
            }
            return this;
        }

        
        public ModifyDataPathBackendSpectraS3Request WithIomCacheLimitationPercent(double? iomCacheLimitationPercent)
        {
            this._iomCacheLimitationPercent = iomCacheLimitationPercent;
            if (iomCacheLimitationPercent != null)
            {
                this.QueryParams.Add("iom_cache_limitation_percent", iomCacheLimitationPercent.ToString());
            }
            else
            {
                this.QueryParams.Remove("iom_cache_limitation_percent");
            }
            return this;
        }

        
        public ModifyDataPathBackendSpectraS3Request WithIomEnabled(bool? iomEnabled)
        {
            this._iomEnabled = iomEnabled;
            if (iomEnabled != null)
            {
                this.QueryParams.Add("iom_enabled", iomEnabled.ToString());
            }
            else
            {
                this.QueryParams.Remove("iom_enabled");
            }
            return this;
        }

        
        public ModifyDataPathBackendSpectraS3Request WithMaxAggregatedBlobsPerChunk(int? maxAggregatedBlobsPerChunk)
        {
            this._maxAggregatedBlobsPerChunk = maxAggregatedBlobsPerChunk;
            if (maxAggregatedBlobsPerChunk != null)
            {
                this.QueryParams.Add("max_aggregated_blobs_per_chunk", maxAggregatedBlobsPerChunk.ToString());
            }
            else
            {
                this.QueryParams.Remove("max_aggregated_blobs_per_chunk");
            }
            return this;
        }

        
        public ModifyDataPathBackendSpectraS3Request WithPartiallyVerifyLastPercentOfTapes(int? partiallyVerifyLastPercentOfTapes)
        {
            this._partiallyVerifyLastPercentOfTapes = partiallyVerifyLastPercentOfTapes;
            if (partiallyVerifyLastPercentOfTapes != null)
            {
                this.QueryParams.Add("partially_verify_last_percent_of_tapes", partiallyVerifyLastPercentOfTapes.ToString());
            }
            else
            {
                this.QueryParams.Remove("partially_verify_last_percent_of_tapes");
            }
            return this;
        }

        
        public ModifyDataPathBackendSpectraS3Request WithUnavailableMediaPolicy(UnavailableMediaUsagePolicy? unavailableMediaPolicy)
        {
            this._unavailableMediaPolicy = unavailableMediaPolicy;
            if (unavailableMediaPolicy != null)
            {
                this.QueryParams.Add("unavailable_media_policy", unavailableMediaPolicy.ToString());
            }
            else
            {
                this.QueryParams.Remove("unavailable_media_policy");
            }
            return this;
        }

        
        public ModifyDataPathBackendSpectraS3Request WithUnavailablePoolMaxJobRetryInMins(int? unavailablePoolMaxJobRetryInMins)
        {
            this._unavailablePoolMaxJobRetryInMins = unavailablePoolMaxJobRetryInMins;
            if (unavailablePoolMaxJobRetryInMins != null)
            {
                this.QueryParams.Add("unavailable_pool_max_job_retry_in_mins", unavailablePoolMaxJobRetryInMins.ToString());
            }
            else
            {
                this.QueryParams.Remove("unavailable_pool_max_job_retry_in_mins");
            }
            return this;
        }

        
        public ModifyDataPathBackendSpectraS3Request WithUnavailableTapePartitionMaxJobRetryInMins(int? unavailableTapePartitionMaxJobRetryInMins)
        {
            this._unavailableTapePartitionMaxJobRetryInMins = unavailableTapePartitionMaxJobRetryInMins;
            if (unavailableTapePartitionMaxJobRetryInMins != null)
            {
                this.QueryParams.Add("unavailable_tape_partition_max_job_retry_in_mins", unavailableTapePartitionMaxJobRetryInMins.ToString());
            }
            else
            {
                this.QueryParams.Remove("unavailable_tape_partition_max_job_retry_in_mins");
            }
            return this;
        }


        
        
        public ModifyDataPathBackendSpectraS3Request()
        {
            
        }

        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.PUT;
            }
        }

        internal override string Path
        {
            get
            {
                return "/_rest_/data_path_backend";
            }
        }
    }
}