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
using System.Net;

namespace Ds3.Calls
{
    public class PutDataPolicySpectraS3Request : Ds3Request
    {
        
        public string Name { get; private set; }

        
        private bool _blobbingEnabled;
        public bool BlobbingEnabled
        {
            get { return _blobbingEnabled; }
            set { WithBlobbingEnabled(value); }
        }

        public PutDataPolicySpectraS3Request WithBlobbingEnabled(bool blobbingEnabled)
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

        private ChecksumType.Type _checksumType;
        public ChecksumType.Type ChecksumType
        {
            get { return _checksumType; }
            set { WithChecksumType(value); }
        }

        public PutDataPolicySpectraS3Request WithChecksumType(ChecksumType.Type checksumType)
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

        private Long? _defaultBlobSize;
        public Long? DefaultBlobSize
        {
            get { return _defaultBlobSize; }
            set { WithDefaultBlobSize(value); }
        }

        public PutDataPolicySpectraS3Request WithDefaultBlobSize(Long? defaultBlobSize)
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

        public PutDataPolicySpectraS3Request WithDefaultGetJobPriority(Priority defaultGetJobPriority)
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

        private Priority _defaultPutJobPriority;
        public Priority DefaultPutJobPriority
        {
            get { return _defaultPutJobPriority; }
            set { WithDefaultPutJobPriority(value); }
        }

        public PutDataPolicySpectraS3Request WithDefaultPutJobPriority(Priority defaultPutJobPriority)
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

        private Priority _defaultVerifyJobPriority;
        public Priority DefaultVerifyJobPriority
        {
            get { return _defaultVerifyJobPriority; }
            set { WithDefaultVerifyJobPriority(value); }
        }

        public PutDataPolicySpectraS3Request WithDefaultVerifyJobPriority(Priority defaultVerifyJobPriority)
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

        private bool _endToEndCrcRequired;
        public bool EndToEndCrcRequired
        {
            get { return _endToEndCrcRequired; }
            set { WithEndToEndCrcRequired(value); }
        }

        public PutDataPolicySpectraS3Request WithEndToEndCrcRequired(bool endToEndCrcRequired)
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

        private Priority _rebuildPriority;
        public Priority RebuildPriority
        {
            get { return _rebuildPriority; }
            set { WithRebuildPriority(value); }
        }

        public PutDataPolicySpectraS3Request WithRebuildPriority(Priority rebuildPriority)
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

        private VersioningLevel _versioning;
        public VersioningLevel Versioning
        {
            get { return _versioning; }
            set { WithVersioning(value); }
        }

        public PutDataPolicySpectraS3Request WithVersioning(VersioningLevel versioning)
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

        public PutDataPolicySpectraS3Request(string name) {
            this.Name = name;
            
            this.QueryParams.Add("name", Name);

        }

        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.POST
            }
        }

        internal override string Path
        {
            get
            {
                return "/_rest_/data_policy/";
            }
        }
    }
}