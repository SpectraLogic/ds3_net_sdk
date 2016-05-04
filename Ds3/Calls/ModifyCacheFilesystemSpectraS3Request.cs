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
    public class ModifyCacheFilesystemSpectraS3Request : Ds3Request
    {
        
        public string CacheFilesystem { get; private set; }

        
        private double? _autoReclaimInitiateThreshold;
        public double? AutoReclaimInitiateThreshold
        {
            get { return _autoReclaimInitiateThreshold; }
            set { WithAutoReclaimInitiateThreshold(value); }
        }

        private double? _autoReclaimTerminateThreshold;
        public double? AutoReclaimTerminateThreshold
        {
            get { return _autoReclaimTerminateThreshold; }
            set { WithAutoReclaimTerminateThreshold(value); }
        }

        private double? _burstThreshold;
        public double? BurstThreshold
        {
            get { return _burstThreshold; }
            set { WithBurstThreshold(value); }
        }

        private long? _maxCapacityInBytes;
        public long? MaxCapacityInBytes
        {
            get { return _maxCapacityInBytes; }
            set { WithMaxCapacityInBytes(value); }
        }

        public ModifyCacheFilesystemSpectraS3Request WithAutoReclaimInitiateThreshold(double? autoReclaimInitiateThreshold)
        {
            this._autoReclaimInitiateThreshold = autoReclaimInitiateThreshold;
            if (autoReclaimInitiateThreshold != null) {
                this.QueryParams.Add("auto_reclaim_initiate_threshold", autoReclaimInitiateThreshold.ToString());
            }
            else
            {
                this.QueryParams.Remove("auto_reclaim_initiate_threshold");
            }
            return this;
        }
        public ModifyCacheFilesystemSpectraS3Request WithAutoReclaimTerminateThreshold(double? autoReclaimTerminateThreshold)
        {
            this._autoReclaimTerminateThreshold = autoReclaimTerminateThreshold;
            if (autoReclaimTerminateThreshold != null) {
                this.QueryParams.Add("auto_reclaim_terminate_threshold", autoReclaimTerminateThreshold.ToString());
            }
            else
            {
                this.QueryParams.Remove("auto_reclaim_terminate_threshold");
            }
            return this;
        }
        public ModifyCacheFilesystemSpectraS3Request WithBurstThreshold(double? burstThreshold)
        {
            this._burstThreshold = burstThreshold;
            if (burstThreshold != null) {
                this.QueryParams.Add("burst_threshold", burstThreshold.ToString());
            }
            else
            {
                this.QueryParams.Remove("burst_threshold");
            }
            return this;
        }
        public ModifyCacheFilesystemSpectraS3Request WithMaxCapacityInBytes(long? maxCapacityInBytes)
        {
            this._maxCapacityInBytes = maxCapacityInBytes;
            if (maxCapacityInBytes != null) {
                this.QueryParams.Add("max_capacity_in_bytes", maxCapacityInBytes.ToString());
            }
            else
            {
                this.QueryParams.Remove("max_capacity_in_bytes");
            }
            return this;
        }

        
        public ModifyCacheFilesystemSpectraS3Request(string cacheFilesystem) {
            this.CacheFilesystem = cacheFilesystem;
            
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
                return "/_rest_/cache_filesystem/" + CacheFilesystem;
            }
        }
    }
}