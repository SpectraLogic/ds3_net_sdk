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
    public class PutStorageDomainSpectraS3Request : Ds3Request
    {
        
        public string Name { get; private set; }

        
        private long? _autoEjectMediaFullThreshold;
        public long? AutoEjectMediaFullThreshold
        {
            get { return _autoEjectMediaFullThreshold; }
            set { WithAutoEjectMediaFullThreshold(value); }
        }

        private string _autoEjectUponCron;
        public string AutoEjectUponCron
        {
            get { return _autoEjectUponCron; }
            set { WithAutoEjectUponCron(value); }
        }

        private bool? _autoEjectUponJobCancellation;
        public bool? AutoEjectUponJobCancellation
        {
            get { return _autoEjectUponJobCancellation; }
            set { WithAutoEjectUponJobCancellation(value); }
        }

        private bool? _autoEjectUponJobCompletion;
        public bool? AutoEjectUponJobCompletion
        {
            get { return _autoEjectUponJobCompletion; }
            set { WithAutoEjectUponJobCompletion(value); }
        }

        private bool? _autoEjectUponMediaFull;
        public bool? AutoEjectUponMediaFull
        {
            get { return _autoEjectUponMediaFull; }
            set { WithAutoEjectUponMediaFull(value); }
        }

        private LtfsFileNamingMode? _ltfsFileNaming;
        public LtfsFileNamingMode? LtfsFileNaming
        {
            get { return _ltfsFileNaming; }
            set { WithLtfsFileNaming(value); }
        }

        private int? _maxTapeFragmentationPercent;
        public int? MaxTapeFragmentationPercent
        {
            get { return _maxTapeFragmentationPercent; }
            set { WithMaxTapeFragmentationPercent(value); }
        }

        private int? _maximumAutoVerificationFrequencyInDays;
        public int? MaximumAutoVerificationFrequencyInDays
        {
            get { return _maximumAutoVerificationFrequencyInDays; }
            set { WithMaximumAutoVerificationFrequencyInDays(value); }
        }

        private bool? _mediaEjectionAllowed;
        public bool? MediaEjectionAllowed
        {
            get { return _mediaEjectionAllowed; }
            set { WithMediaEjectionAllowed(value); }
        }

        private bool? _secureMediaAllocation;
        public bool? SecureMediaAllocation
        {
            get { return _secureMediaAllocation; }
            set { WithSecureMediaAllocation(value); }
        }

        private Priority? _verifyPriorToAutoEject;
        public Priority? VerifyPriorToAutoEject
        {
            get { return _verifyPriorToAutoEject; }
            set { WithVerifyPriorToAutoEject(value); }
        }

        private WriteOptimization? _writeOptimization;
        public WriteOptimization? WriteOptimization
        {
            get { return _writeOptimization; }
            set { WithWriteOptimization(value); }
        }

        
        public PutStorageDomainSpectraS3Request WithAutoEjectMediaFullThreshold(long? autoEjectMediaFullThreshold)
        {
            this._autoEjectMediaFullThreshold = autoEjectMediaFullThreshold;
            if (autoEjectMediaFullThreshold != null)
            {
                this.QueryParams.Add("auto_eject_media_full_threshold", autoEjectMediaFullThreshold.ToString());
            }
            else
            {
                this.QueryParams.Remove("auto_eject_media_full_threshold");
            }
            return this;
        }

        
        public PutStorageDomainSpectraS3Request WithAutoEjectUponCron(string autoEjectUponCron)
        {
            this._autoEjectUponCron = autoEjectUponCron;
            if (autoEjectUponCron != null)
            {
                this.QueryParams.Add("auto_eject_upon_cron", autoEjectUponCron);
            }
            else
            {
                this.QueryParams.Remove("auto_eject_upon_cron");
            }
            return this;
        }

        
        public PutStorageDomainSpectraS3Request WithAutoEjectUponJobCancellation(bool? autoEjectUponJobCancellation)
        {
            this._autoEjectUponJobCancellation = autoEjectUponJobCancellation;
            if (autoEjectUponJobCancellation != null)
            {
                this.QueryParams.Add("auto_eject_upon_job_cancellation", autoEjectUponJobCancellation.ToString());
            }
            else
            {
                this.QueryParams.Remove("auto_eject_upon_job_cancellation");
            }
            return this;
        }

        
        public PutStorageDomainSpectraS3Request WithAutoEjectUponJobCompletion(bool? autoEjectUponJobCompletion)
        {
            this._autoEjectUponJobCompletion = autoEjectUponJobCompletion;
            if (autoEjectUponJobCompletion != null)
            {
                this.QueryParams.Add("auto_eject_upon_job_completion", autoEjectUponJobCompletion.ToString());
            }
            else
            {
                this.QueryParams.Remove("auto_eject_upon_job_completion");
            }
            return this;
        }

        
        public PutStorageDomainSpectraS3Request WithAutoEjectUponMediaFull(bool? autoEjectUponMediaFull)
        {
            this._autoEjectUponMediaFull = autoEjectUponMediaFull;
            if (autoEjectUponMediaFull != null)
            {
                this.QueryParams.Add("auto_eject_upon_media_full", autoEjectUponMediaFull.ToString());
            }
            else
            {
                this.QueryParams.Remove("auto_eject_upon_media_full");
            }
            return this;
        }

        
        public PutStorageDomainSpectraS3Request WithLtfsFileNaming(LtfsFileNamingMode? ltfsFileNaming)
        {
            this._ltfsFileNaming = ltfsFileNaming;
            if (ltfsFileNaming != null)
            {
                this.QueryParams.Add("ltfs_file_naming", ltfsFileNaming.ToString());
            }
            else
            {
                this.QueryParams.Remove("ltfs_file_naming");
            }
            return this;
        }

        
        public PutStorageDomainSpectraS3Request WithMaxTapeFragmentationPercent(int? maxTapeFragmentationPercent)
        {
            this._maxTapeFragmentationPercent = maxTapeFragmentationPercent;
            if (maxTapeFragmentationPercent != null)
            {
                this.QueryParams.Add("max_tape_fragmentation_percent", maxTapeFragmentationPercent.ToString());
            }
            else
            {
                this.QueryParams.Remove("max_tape_fragmentation_percent");
            }
            return this;
        }

        
        public PutStorageDomainSpectraS3Request WithMaximumAutoVerificationFrequencyInDays(int? maximumAutoVerificationFrequencyInDays)
        {
            this._maximumAutoVerificationFrequencyInDays = maximumAutoVerificationFrequencyInDays;
            if (maximumAutoVerificationFrequencyInDays != null)
            {
                this.QueryParams.Add("maximum_auto_verification_frequency_in_days", maximumAutoVerificationFrequencyInDays.ToString());
            }
            else
            {
                this.QueryParams.Remove("maximum_auto_verification_frequency_in_days");
            }
            return this;
        }

        
        public PutStorageDomainSpectraS3Request WithMediaEjectionAllowed(bool? mediaEjectionAllowed)
        {
            this._mediaEjectionAllowed = mediaEjectionAllowed;
            if (mediaEjectionAllowed != null)
            {
                this.QueryParams.Add("media_ejection_allowed", mediaEjectionAllowed.ToString());
            }
            else
            {
                this.QueryParams.Remove("media_ejection_allowed");
            }
            return this;
        }

        
        public PutStorageDomainSpectraS3Request WithSecureMediaAllocation(bool? secureMediaAllocation)
        {
            this._secureMediaAllocation = secureMediaAllocation;
            if (secureMediaAllocation != null)
            {
                this.QueryParams.Add("secure_media_allocation", secureMediaAllocation.ToString());
            }
            else
            {
                this.QueryParams.Remove("secure_media_allocation");
            }
            return this;
        }

        
        public PutStorageDomainSpectraS3Request WithVerifyPriorToAutoEject(Priority? verifyPriorToAutoEject)
        {
            this._verifyPriorToAutoEject = verifyPriorToAutoEject;
            if (verifyPriorToAutoEject != null)
            {
                this.QueryParams.Add("verify_prior_to_auto_eject", verifyPriorToAutoEject.ToString());
            }
            else
            {
                this.QueryParams.Remove("verify_prior_to_auto_eject");
            }
            return this;
        }

        
        public PutStorageDomainSpectraS3Request WithWriteOptimization(WriteOptimization? writeOptimization)
        {
            this._writeOptimization = writeOptimization;
            if (writeOptimization != null)
            {
                this.QueryParams.Add("write_optimization", writeOptimization.ToString());
            }
            else
            {
                this.QueryParams.Remove("write_optimization");
            }
            return this;
        }


        
        
        public PutStorageDomainSpectraS3Request(string name)
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
                return "/_rest_/storage_domain";
            }
        }
    }
}