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
    public class ModifyDataPathBackendSpectraS3Request : Ds3Request
    {
        
        
        private bool? _activated;
        public bool? Activated
        {
            get { return _activated; }
            set { WithActivated(value); }
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

        private ImportConflictResolutionMode? _defaultImportConflictResolutionMode;
        public ImportConflictResolutionMode? DefaultImportConflictResolutionMode
        {
            get { return _defaultImportConflictResolutionMode; }
            set { WithDefaultImportConflictResolutionMode(value); }
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
        public ModifyDataPathBackendSpectraS3Request WithDefaultImportConflictResolutionMode(ImportConflictResolutionMode? defaultImportConflictResolutionMode)
        {
            this._defaultImportConflictResolutionMode = defaultImportConflictResolutionMode;
            if (defaultImportConflictResolutionMode != null)
            {
                this.QueryParams.Add("default_import_conflict_resolution_mode", defaultImportConflictResolutionMode.ToString());
            }
            else
            {
                this.QueryParams.Remove("default_import_conflict_resolution_mode");
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