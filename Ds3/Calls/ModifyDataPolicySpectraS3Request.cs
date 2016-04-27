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
    public class ModifyDataPolicySpectraS3Request : Ds3Request
    {
        
        public string DataPolicy { get; private set; }

        
        private bool _blobbingEnabled;
        public bool BlobbingEnabled
        {
            get { return _blobbingEnabled; }
            set { WithBlobbingEnabled(value); }
        }

        public ModifyDataPolicySpectraS3Request WithBlobbingEnabled(bool blobbingEnabled)
        {
            this._blobbingEnabled = blobbingEnabled;
            if (blobbingEnabled != null) {
                this.QueryParams.Add("blobbing_enabled", BlobbingEnabled.ToString());
            }
            else
            {
                this.QueryParams.Remove("blobbing_enabled");
            }
            return this;
        }

        private ChecksumType.Type _checksumType;
        public ChecksumType.Type ChecksumType
        {
            get { return _checksumType; }
            set { WithChecksumType(value); }
        }

        public ModifyDataPolicySpectraS3Request WithChecksumType(ChecksumType.Type checksumType)
        {
            this._checksumType = checksumType;
            if (checksumType != null) {
                this.QueryParams.Add("checksum_type", ChecksumType.ToString());
            }
            else
            {
                this.QueryParams.Remove("checksum_type");
            }
            return this;
        }

        private long? _defaultBlobSize;
        public long? DefaultBlobSize
        {
            get { return _defaultBlobSize; }
            set { WithDefaultBlobSize(value); }
        }

        public ModifyDataPolicySpectraS3Request WithDefaultBlobSize(long? defaultBlobSize)
        {
            this._defaultBlobSize = defaultBlobSize;
            if (defaultBlobSize != null) {
                this.QueryParams.Add("default_blob_size", DefaultBlobSize.ToString());
            }
            else
            {
                this.QueryParams.Remove("default_blob_size");
            }
            return this;
        }

        private Priority _defaultGetJobPriority;
        public Priority DefaultGetJobPriority
        {
            get { return _defaultGetJobPriority; }
            set { WithDefaultGetJobPriority(value); }
        }

        public ModifyDataPolicySpectraS3Request WithDefaultGetJobPriority(Priority defaultGetJobPriority)
        {
            this._defaultGetJobPriority = defaultGetJobPriority;
            if (defaultGetJobPriority != null) {
                this.QueryParams.Add("default_get_job_priority", DefaultGetJobPriority.ToString());
            }
            else
            {
                this.QueryParams.Remove("default_get_job_priority");
            }
            return this;
        }

        private Priority _defaultPutJobPriority;
        public Priority DefaultPutJobPriority
        {
            get { return _defaultPutJobPriority; }
            set { WithDefaultPutJobPriority(value); }
        }

        public ModifyDataPolicySpectraS3Request WithDefaultPutJobPriority(Priority defaultPutJobPriority)
        {
            this._defaultPutJobPriority = defaultPutJobPriority;
            if (defaultPutJobPriority != null) {
                this.QueryParams.Add("default_put_job_priority", DefaultPutJobPriority.ToString());
            }
            else
            {
                this.QueryParams.Remove("default_put_job_priority");
            }
            return this;
        }

        private Priority _defaultVerifyJobPriority;
        public Priority DefaultVerifyJobPriority
        {
            get { return _defaultVerifyJobPriority; }
            set { WithDefaultVerifyJobPriority(value); }
        }

        public ModifyDataPolicySpectraS3Request WithDefaultVerifyJobPriority(Priority defaultVerifyJobPriority)
        {
            this._defaultVerifyJobPriority = defaultVerifyJobPriority;
            if (defaultVerifyJobPriority != null) {
                this.QueryParams.Add("default_verify_job_priority", DefaultVerifyJobPriority.ToString());
            }
            else
            {
                this.QueryParams.Remove("default_verify_job_priority");
            }
            return this;
        }

        private bool _endToEndCrcRequired;
        public bool EndToEndCrcRequired
        {
            get { return _endToEndCrcRequired; }
            set { WithEndToEndCrcRequired(value); }
        }

        public ModifyDataPolicySpectraS3Request WithEndToEndCrcRequired(bool endToEndCrcRequired)
        {
            this._endToEndCrcRequired = endToEndCrcRequired;
            if (endToEndCrcRequired != null) {
                this.QueryParams.Add("end_to_end_crc_required", EndToEndCrcRequired.ToString());
            }
            else
            {
                this.QueryParams.Remove("end_to_end_crc_required");
            }
            return this;
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { WithName(value); }
        }

        public ModifyDataPolicySpectraS3Request WithName(string name)
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

        private Priority _rebuildPriority;
        public Priority RebuildPriority
        {
            get { return _rebuildPriority; }
            set { WithRebuildPriority(value); }
        }

        public ModifyDataPolicySpectraS3Request WithRebuildPriority(Priority rebuildPriority)
        {
            this._rebuildPriority = rebuildPriority;
            if (rebuildPriority != null) {
                this.QueryParams.Add("rebuild_priority", RebuildPriority.ToString());
            }
            else
            {
                this.QueryParams.Remove("rebuild_priority");
            }
            return this;
        }

        private VersioningLevel _versioning;
        public VersioningLevel Versioning
        {
            get { return _versioning; }
            set { WithVersioning(value); }
        }

        public ModifyDataPolicySpectraS3Request WithVersioning(VersioningLevel versioning)
        {
            this._versioning = versioning;
            if (versioning != null) {
                this.QueryParams.Add("versioning", Versioning.ToString());
            }
            else
            {
                this.QueryParams.Remove("versioning");
            }
            return this;
        }

        public ModifyDataPolicySpectraS3Request(string dataPolicy) {
            this.DataPolicy = dataPolicy;
            
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
                return "/_rest_/data_policy/" + DataPolicy;
            }
        }
    }
}