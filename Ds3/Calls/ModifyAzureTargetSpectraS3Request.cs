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
    public class ModifyAzureTargetSpectraS3Request : Ds3Request
    {
        
        public string AzureTarget { get; private set; }

        
        private string _accountKey;
        public string AccountKey
        {
            get { return _accountKey; }
            set { WithAccountKey(value); }
        }

        private string _accountName;
        public string AccountName
        {
            get { return _accountName; }
            set { WithAccountName(value); }
        }

        private int? _autoVerifyFrequencyInDays;
        public int? AutoVerifyFrequencyInDays
        {
            get { return _autoVerifyFrequencyInDays; }
            set { WithAutoVerifyFrequencyInDays(value); }
        }

        private string _cloudBucketPrefix;
        public string CloudBucketPrefix
        {
            get { return _cloudBucketPrefix; }
            set { WithCloudBucketPrefix(value); }
        }

        private string _cloudBucketSuffix;
        public string CloudBucketSuffix
        {
            get { return _cloudBucketSuffix; }
            set { WithCloudBucketSuffix(value); }
        }

        private TargetReadPreferenceType? _defaultReadPreference;
        public TargetReadPreferenceType? DefaultReadPreference
        {
            get { return _defaultReadPreference; }
            set { WithDefaultReadPreference(value); }
        }

        private bool? _https;
        public bool? Https
        {
            get { return _https; }
            set { WithHttps(value); }
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { WithName(value); }
        }

        private bool? _permitGoingOutOfSync;
        public bool? PermitGoingOutOfSync
        {
            get { return _permitGoingOutOfSync; }
            set { WithPermitGoingOutOfSync(value); }
        }

        private Quiesced? _quiesced;
        public Quiesced? Quiesced
        {
            get { return _quiesced; }
            set { WithQuiesced(value); }
        }

        
        public ModifyAzureTargetSpectraS3Request WithAccountKey(string accountKey)
        {
            this._accountKey = accountKey;
            if (accountKey != null)
            {
                this.QueryParams.Add("account_key", accountKey);
            }
            else
            {
                this.QueryParams.Remove("account_key");
            }
            return this;
        }

        
        public ModifyAzureTargetSpectraS3Request WithAccountName(string accountName)
        {
            this._accountName = accountName;
            if (accountName != null)
            {
                this.QueryParams.Add("account_name", accountName);
            }
            else
            {
                this.QueryParams.Remove("account_name");
            }
            return this;
        }

        
        public ModifyAzureTargetSpectraS3Request WithAutoVerifyFrequencyInDays(int? autoVerifyFrequencyInDays)
        {
            this._autoVerifyFrequencyInDays = autoVerifyFrequencyInDays;
            if (autoVerifyFrequencyInDays != null)
            {
                this.QueryParams.Add("auto_verify_frequency_in_days", autoVerifyFrequencyInDays.ToString());
            }
            else
            {
                this.QueryParams.Remove("auto_verify_frequency_in_days");
            }
            return this;
        }

        
        public ModifyAzureTargetSpectraS3Request WithCloudBucketPrefix(string cloudBucketPrefix)
        {
            this._cloudBucketPrefix = cloudBucketPrefix;
            if (cloudBucketPrefix != null)
            {
                this.QueryParams.Add("cloud_bucket_prefix", cloudBucketPrefix);
            }
            else
            {
                this.QueryParams.Remove("cloud_bucket_prefix");
            }
            return this;
        }

        
        public ModifyAzureTargetSpectraS3Request WithCloudBucketSuffix(string cloudBucketSuffix)
        {
            this._cloudBucketSuffix = cloudBucketSuffix;
            if (cloudBucketSuffix != null)
            {
                this.QueryParams.Add("cloud_bucket_suffix", cloudBucketSuffix);
            }
            else
            {
                this.QueryParams.Remove("cloud_bucket_suffix");
            }
            return this;
        }

        
        public ModifyAzureTargetSpectraS3Request WithDefaultReadPreference(TargetReadPreferenceType? defaultReadPreference)
        {
            this._defaultReadPreference = defaultReadPreference;
            if (defaultReadPreference != null)
            {
                this.QueryParams.Add("default_read_preference", defaultReadPreference.ToString());
            }
            else
            {
                this.QueryParams.Remove("default_read_preference");
            }
            return this;
        }

        
        public ModifyAzureTargetSpectraS3Request WithHttps(bool? https)
        {
            this._https = https;
            if (https != null)
            {
                this.QueryParams.Add("https", https.ToString());
            }
            else
            {
                this.QueryParams.Remove("https");
            }
            return this;
        }

        
        public ModifyAzureTargetSpectraS3Request WithName(string name)
        {
            this._name = name;
            if (name != null)
            {
                this.QueryParams.Add("name", name);
            }
            else
            {
                this.QueryParams.Remove("name");
            }
            return this;
        }

        
        public ModifyAzureTargetSpectraS3Request WithPermitGoingOutOfSync(bool? permitGoingOutOfSync)
        {
            this._permitGoingOutOfSync = permitGoingOutOfSync;
            if (permitGoingOutOfSync != null)
            {
                this.QueryParams.Add("permit_going_out_of_sync", permitGoingOutOfSync.ToString());
            }
            else
            {
                this.QueryParams.Remove("permit_going_out_of_sync");
            }
            return this;
        }

        
        public ModifyAzureTargetSpectraS3Request WithQuiesced(Quiesced? quiesced)
        {
            this._quiesced = quiesced;
            if (quiesced != null)
            {
                this.QueryParams.Add("quiesced", quiesced.ToString());
            }
            else
            {
                this.QueryParams.Remove("quiesced");
            }
            return this;
        }


        
        
        public ModifyAzureTargetSpectraS3Request(string azureTarget)
        {
            this.AzureTarget = azureTarget;
            
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
                return "/_rest_/azure_target/" + AzureTarget;
            }
        }
    }
}