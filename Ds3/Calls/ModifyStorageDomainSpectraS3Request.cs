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
    public class ModifyStorageDomainSpectraS3Request : Ds3Request
    {
        
        public string StorageDomain { get; private set; }

        
        private string _autoEjectUponCron;
        public string AutoEjectUponCron
        {
            get { return _autoEjectUponCron; }
            set { WithAutoEjectUponCron(value); }
        }

        public ModifyStorageDomainSpectraS3Request WithAutoEjectUponCron(string autoEjectUponCron)
        {
            this._autoEjectUponCron = autoEjectUponCron;
            if (autoEjectUponCron != null) {
                this.QueryParams.Add("auto_eject_upon_cron", AutoEjectUponCron);
            }
            else
            {
                this.QueryParams.Remove("auto_eject_upon_cron");
            }
            return this;
        }

        private bool _autoEjectUponJobCancellation;
        public bool AutoEjectUponJobCancellation
        {
            get { return _autoEjectUponJobCancellation; }
            set { WithAutoEjectUponJobCancellation(value); }
        }

        public ModifyStorageDomainSpectraS3Request WithAutoEjectUponJobCancellation(bool autoEjectUponJobCancellation)
        {
            this._autoEjectUponJobCancellation = autoEjectUponJobCancellation;
            if (autoEjectUponJobCancellation != null) {
                this.QueryParams.Add("auto_eject_upon_job_cancellation", AutoEjectUponJobCancellation.ToString());
            }
            else
            {
                this.QueryParams.Remove("auto_eject_upon_job_cancellation");
            }
            return this;
        }

        private bool _autoEjectUponJobCompletion;
        public bool AutoEjectUponJobCompletion
        {
            get { return _autoEjectUponJobCompletion; }
            set { WithAutoEjectUponJobCompletion(value); }
        }

        public ModifyStorageDomainSpectraS3Request WithAutoEjectUponJobCompletion(bool autoEjectUponJobCompletion)
        {
            this._autoEjectUponJobCompletion = autoEjectUponJobCompletion;
            if (autoEjectUponJobCompletion != null) {
                this.QueryParams.Add("auto_eject_upon_job_completion", AutoEjectUponJobCompletion.ToString());
            }
            else
            {
                this.QueryParams.Remove("auto_eject_upon_job_completion");
            }
            return this;
        }

        private bool _autoEjectUponMediaFull;
        public bool AutoEjectUponMediaFull
        {
            get { return _autoEjectUponMediaFull; }
            set { WithAutoEjectUponMediaFull(value); }
        }

        public ModifyStorageDomainSpectraS3Request WithAutoEjectUponMediaFull(bool autoEjectUponMediaFull)
        {
            this._autoEjectUponMediaFull = autoEjectUponMediaFull;
            if (autoEjectUponMediaFull != null) {
                this.QueryParams.Add("auto_eject_upon_media_full", AutoEjectUponMediaFull.ToString());
            }
            else
            {
                this.QueryParams.Remove("auto_eject_upon_media_full");
            }
            return this;
        }

        private LtfsFileNamingMode _ltfsFileNaming;
        public LtfsFileNamingMode LtfsFileNaming
        {
            get { return _ltfsFileNaming; }
            set { WithLtfsFileNaming(value); }
        }

        public ModifyStorageDomainSpectraS3Request WithLtfsFileNaming(LtfsFileNamingMode ltfsFileNaming)
        {
            this._ltfsFileNaming = ltfsFileNaming;
            if (ltfsFileNaming != null) {
                this.QueryParams.Add("ltfs_file_naming", LtfsFileNaming.ToString());
            }
            else
            {
                this.QueryParams.Remove("ltfs_file_naming");
            }
            return this;
        }

        private int _maxTapeFragmentationPercent;
        public int MaxTapeFragmentationPercent
        {
            get { return _maxTapeFragmentationPercent; }
            set { WithMaxTapeFragmentationPercent(value); }
        }

        public ModifyStorageDomainSpectraS3Request WithMaxTapeFragmentationPercent(int maxTapeFragmentationPercent)
        {
            this._maxTapeFragmentationPercent = maxTapeFragmentationPercent;
            if (maxTapeFragmentationPercent != null) {
                this.QueryParams.Add("max_tape_fragmentation_percent", MaxTapeFragmentationPercent.ToString());
            }
            else
            {
                this.QueryParams.Remove("max_tape_fragmentation_percent");
            }
            return this;
        }

        private int _maximumAutoVerificationFrequencyInDays;
        public int MaximumAutoVerificationFrequencyInDays
        {
            get { return _maximumAutoVerificationFrequencyInDays; }
            set { WithMaximumAutoVerificationFrequencyInDays(value); }
        }

        public ModifyStorageDomainSpectraS3Request WithMaximumAutoVerificationFrequencyInDays(int maximumAutoVerificationFrequencyInDays)
        {
            this._maximumAutoVerificationFrequencyInDays = maximumAutoVerificationFrequencyInDays;
            if (maximumAutoVerificationFrequencyInDays != null) {
                this.QueryParams.Add("maximum_auto_verification_frequency_in_days", MaximumAutoVerificationFrequencyInDays.ToString());
            }
            else
            {
                this.QueryParams.Remove("maximum_auto_verification_frequency_in_days");
            }
            return this;
        }

        private bool _mediaEjectionAllowed;
        public bool MediaEjectionAllowed
        {
            get { return _mediaEjectionAllowed; }
            set { WithMediaEjectionAllowed(value); }
        }

        public ModifyStorageDomainSpectraS3Request WithMediaEjectionAllowed(bool mediaEjectionAllowed)
        {
            this._mediaEjectionAllowed = mediaEjectionAllowed;
            if (mediaEjectionAllowed != null) {
                this.QueryParams.Add("media_ejection_allowed", MediaEjectionAllowed.ToString());
            }
            else
            {
                this.QueryParams.Remove("media_ejection_allowed");
            }
            return this;
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { WithName(value); }
        }

        public ModifyStorageDomainSpectraS3Request WithName(string name)
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

        private Priority _verifyPriorToAutoEject;
        public Priority VerifyPriorToAutoEject
        {
            get { return _verifyPriorToAutoEject; }
            set { WithVerifyPriorToAutoEject(value); }
        }

        public ModifyStorageDomainSpectraS3Request WithVerifyPriorToAutoEject(Priority verifyPriorToAutoEject)
        {
            this._verifyPriorToAutoEject = verifyPriorToAutoEject;
            if (verifyPriorToAutoEject != null) {
                this.QueryParams.Add("verify_prior_to_auto_eject", VerifyPriorToAutoEject.ToString());
            }
            else
            {
                this.QueryParams.Remove("verify_prior_to_auto_eject");
            }
            return this;
        }

        private WriteOptimization _writeOptimization;
        public WriteOptimization WriteOptimization
        {
            get { return _writeOptimization; }
            set { WithWriteOptimization(value); }
        }

        public ModifyStorageDomainSpectraS3Request WithWriteOptimization(WriteOptimization writeOptimization)
        {
            this._writeOptimization = writeOptimization;
            if (writeOptimization != null) {
                this.QueryParams.Add("write_optimization", WriteOptimization.ToString());
            }
            else
            {
                this.QueryParams.Remove("write_optimization");
            }
            return this;
        }

        public ModifyStorageDomainSpectraS3Request(string storageDomain) {
            this.StorageDomain = storageDomain;
            
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
                return "/_rest_/storage_domain/" + StorageDomain;
            }
        }
    }
}