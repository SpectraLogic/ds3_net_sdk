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
    public class PutDataPolicySpectraS3Request : Ds3Request
    {
        
        public string Name { get; private set; }

        
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

        private bool? _blobbingEnabled;
        public bool? BlobbingEnabled
        {
            get { return _blobbingEnabled; }
            set { WithBlobbingEnabled(value); }
        }

        private ChecksumType.Type? _checksumType;
        public ChecksumType.Type? ChecksumType
        {
            get { return _checksumType; }
            set { WithChecksumType(value); }
        }

        private long? _defaultBlobSize;
        public long? DefaultBlobSize
        {
            get { return _defaultBlobSize; }
            set { WithDefaultBlobSize(value); }
        }

        private Priority? _defaultGetJobPriority;
        public Priority? DefaultGetJobPriority
        {
            get { return _defaultGetJobPriority; }
            set { WithDefaultGetJobPriority(value); }
        }

        private Priority? _defaultPutJobPriority;
        public Priority? DefaultPutJobPriority
        {
            get { return _defaultPutJobPriority; }
            set { WithDefaultPutJobPriority(value); }
        }

        private Priority? _defaultVerifyJobPriority;
        public Priority? DefaultVerifyJobPriority
        {
            get { return _defaultVerifyJobPriority; }
            set { WithDefaultVerifyJobPriority(value); }
        }

        private bool? _endToEndCrcRequired;
        public bool? EndToEndCrcRequired
        {
            get { return _endToEndCrcRequired; }
            set { WithEndToEndCrcRequired(value); }
        }

        private Priority? _rebuildPriority;
        public Priority? RebuildPriority
        {
            get { return _rebuildPriority; }
            set { WithRebuildPriority(value); }
        }

        private VersioningLevel? _versioning;
        public VersioningLevel? Versioning
        {
            get { return _versioning; }
            set { WithVersioning(value); }
        }

        public PutDataPolicySpectraS3Request WithAlwaysForcePutJobCreation(bool? alwaysForcePutJobCreation)
        {
            this._alwaysForcePutJobCreation = alwaysForcePutJobCreation;
            if (alwaysForcePutJobCreation != null) {
                this.QueryParams.Add("always_force_put_job_creation", alwaysForcePutJobCreation.ToString());
            }
            else
            {
                this.QueryParams.Remove("always_force_put_job_creation");
            }
            return this;
        }
        public PutDataPolicySpectraS3Request WithAlwaysMinimizeSpanningAcrossMedia(bool? alwaysMinimizeSpanningAcrossMedia)
        {
            this._alwaysMinimizeSpanningAcrossMedia = alwaysMinimizeSpanningAcrossMedia;
            if (alwaysMinimizeSpanningAcrossMedia != null) {
                this.QueryParams.Add("always_minimize_spanning_across_media", alwaysMinimizeSpanningAcrossMedia.ToString());
            }
            else
            {
                this.QueryParams.Remove("always_minimize_spanning_across_media");
            }
            return this;
        }
        public PutDataPolicySpectraS3Request WithAlwaysReplicateDeletes(bool? alwaysReplicateDeletes)
        {
            this._alwaysReplicateDeletes = alwaysReplicateDeletes;
            if (alwaysReplicateDeletes != null) {
                this.QueryParams.Add("always_replicate_deletes", alwaysReplicateDeletes.ToString());
            }
            else
            {
                this.QueryParams.Remove("always_replicate_deletes");
            }
            return this;
        }
        public PutDataPolicySpectraS3Request WithBlobbingEnabled(bool? blobbingEnabled)
        {
            this._blobbingEnabled = blobbingEnabled;
            if (blobbingEnabled != null) {
                this.QueryParams.Add("blobbing_enabled", blobbingEnabled.ToString());
            }
            else
            {
                this.QueryParams.Remove("blobbing_enabled");
            }
            return this;
        }
        public PutDataPolicySpectraS3Request WithChecksumType(ChecksumType.Type? checksumType)
        {
            this._checksumType = checksumType;
            if (checksumType != null) {
                this.QueryParams.Add("checksum_type", checksumType.ToString());
            }
            else
            {
                this.QueryParams.Remove("checksum_type");
            }
            return this;
        }
        public PutDataPolicySpectraS3Request WithDefaultBlobSize(long? defaultBlobSize)
        {
            this._defaultBlobSize = defaultBlobSize;
            if (defaultBlobSize != null) {
                this.QueryParams.Add("default_blob_size", defaultBlobSize.ToString());
            }
            else
            {
                this.QueryParams.Remove("default_blob_size");
            }
            return this;
        }
        public PutDataPolicySpectraS3Request WithDefaultGetJobPriority(Priority? defaultGetJobPriority)
        {
            this._defaultGetJobPriority = defaultGetJobPriority;
            if (defaultGetJobPriority != null) {
                this.QueryParams.Add("default_get_job_priority", defaultGetJobPriority.ToString());
            }
            else
            {
                this.QueryParams.Remove("default_get_job_priority");
            }
            return this;
        }
        public PutDataPolicySpectraS3Request WithDefaultPutJobPriority(Priority? defaultPutJobPriority)
        {
            this._defaultPutJobPriority = defaultPutJobPriority;
            if (defaultPutJobPriority != null) {
                this.QueryParams.Add("default_put_job_priority", defaultPutJobPriority.ToString());
            }
            else
            {
                this.QueryParams.Remove("default_put_job_priority");
            }
            return this;
        }
        public PutDataPolicySpectraS3Request WithDefaultVerifyJobPriority(Priority? defaultVerifyJobPriority)
        {
            this._defaultVerifyJobPriority = defaultVerifyJobPriority;
            if (defaultVerifyJobPriority != null) {
                this.QueryParams.Add("default_verify_job_priority", defaultVerifyJobPriority.ToString());
            }
            else
            {
                this.QueryParams.Remove("default_verify_job_priority");
            }
            return this;
        }
        public PutDataPolicySpectraS3Request WithEndToEndCrcRequired(bool? endToEndCrcRequired)
        {
            this._endToEndCrcRequired = endToEndCrcRequired;
            if (endToEndCrcRequired != null) {
                this.QueryParams.Add("end_to_end_crc_required", endToEndCrcRequired.ToString());
            }
            else
            {
                this.QueryParams.Remove("end_to_end_crc_required");
            }
            return this;
        }
        public PutDataPolicySpectraS3Request WithRebuildPriority(Priority? rebuildPriority)
        {
            this._rebuildPriority = rebuildPriority;
            if (rebuildPriority != null) {
                this.QueryParams.Add("rebuild_priority", rebuildPriority.ToString());
            }
            else
            {
                this.QueryParams.Remove("rebuild_priority");
            }
            return this;
        }
        public PutDataPolicySpectraS3Request WithVersioning(VersioningLevel? versioning)
        {
            this._versioning = versioning;
            if (versioning != null) {
                this.QueryParams.Add("versioning", versioning.ToString());
            }
            else
            {
                this.QueryParams.Remove("versioning");
            }
            return this;
        }

        
        public PutDataPolicySpectraS3Request(string name)
        {
            this.Name = name;
            
            this.QueryParams.Add("name", name);

        }

        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.POST;
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