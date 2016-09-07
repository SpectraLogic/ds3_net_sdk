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
    public class RegisterDs3TargetSpectraS3Request : Ds3Request
    {
        
        public string AdminAuthId { get; private set; }

        public string AdminSecretKey { get; private set; }

        public string DataPathEndPoint { get; private set; }

        public string Name { get; private set; }

        
        private Ds3TargetAccessControlReplication? _accessControlReplication;
        public Ds3TargetAccessControlReplication? AccessControlReplication
        {
            get { return _accessControlReplication; }
            set { WithAccessControlReplication(value); }
        }

        private bool? _dataPathHttps;
        public bool? DataPathHttps
        {
            get { return _dataPathHttps; }
            set { WithDataPathHttps(value); }
        }

        private int? _dataPathPort;
        public int? DataPathPort
        {
            get { return _dataPathPort; }
            set { WithDataPathPort(value); }
        }

        private string _dataPathProxy;
        public string DataPathProxy
        {
            get { return _dataPathProxy; }
            set { WithDataPathProxy(value); }
        }

        private bool? _dataPathVerifyCertificate;
        public bool? DataPathVerifyCertificate
        {
            get { return _dataPathVerifyCertificate; }
            set { WithDataPathVerifyCertificate(value); }
        }

        private TargetReadPreference? _defaultReadPreference;
        public TargetReadPreference? DefaultReadPreference
        {
            get { return _defaultReadPreference; }
            set { WithDefaultReadPreference(value); }
        }

        private bool? _permitGoingOutOfSync;
        public bool? PermitGoingOutOfSync
        {
            get { return _permitGoingOutOfSync; }
            set { WithPermitGoingOutOfSync(value); }
        }

        private string _replicatedUserDefaultDataPolicy;
        public string ReplicatedUserDefaultDataPolicy
        {
            get { return _replicatedUserDefaultDataPolicy; }
            set { WithReplicatedUserDefaultDataPolicy(value); }
        }

        
        public RegisterDs3TargetSpectraS3Request WithAccessControlReplication(Ds3TargetAccessControlReplication? accessControlReplication)
        {
            this._accessControlReplication = accessControlReplication;
            if (accessControlReplication != null)
            {
                this.QueryParams.Add("access_control_replication", accessControlReplication.ToString());
            }
            else
            {
                this.QueryParams.Remove("access_control_replication");
            }
            return this;
        }

        
        public RegisterDs3TargetSpectraS3Request WithDataPathHttps(bool? dataPathHttps)
        {
            this._dataPathHttps = dataPathHttps;
            if (dataPathHttps != null)
            {
                this.QueryParams.Add("data_path_https", dataPathHttps.ToString());
            }
            else
            {
                this.QueryParams.Remove("data_path_https");
            }
            return this;
        }

        
        public RegisterDs3TargetSpectraS3Request WithDataPathPort(int? dataPathPort)
        {
            this._dataPathPort = dataPathPort;
            if (dataPathPort != null)
            {
                this.QueryParams.Add("data_path_port", dataPathPort.ToString());
            }
            else
            {
                this.QueryParams.Remove("data_path_port");
            }
            return this;
        }

        
        public RegisterDs3TargetSpectraS3Request WithDataPathProxy(string dataPathProxy)
        {
            this._dataPathProxy = dataPathProxy;
            if (dataPathProxy != null)
            {
                this.QueryParams.Add("data_path_proxy", dataPathProxy);
            }
            else
            {
                this.QueryParams.Remove("data_path_proxy");
            }
            return this;
        }

        
        public RegisterDs3TargetSpectraS3Request WithDataPathVerifyCertificate(bool? dataPathVerifyCertificate)
        {
            this._dataPathVerifyCertificate = dataPathVerifyCertificate;
            if (dataPathVerifyCertificate != null)
            {
                this.QueryParams.Add("data_path_verify_certificate", dataPathVerifyCertificate.ToString());
            }
            else
            {
                this.QueryParams.Remove("data_path_verify_certificate");
            }
            return this;
        }

        
        public RegisterDs3TargetSpectraS3Request WithDefaultReadPreference(TargetReadPreference? defaultReadPreference)
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

        
        public RegisterDs3TargetSpectraS3Request WithPermitGoingOutOfSync(bool? permitGoingOutOfSync)
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

        
        public RegisterDs3TargetSpectraS3Request WithReplicatedUserDefaultDataPolicy(string replicatedUserDefaultDataPolicy)
        {
            this._replicatedUserDefaultDataPolicy = replicatedUserDefaultDataPolicy;
            if (replicatedUserDefaultDataPolicy != null)
            {
                this.QueryParams.Add("replicated_user_default_data_policy", replicatedUserDefaultDataPolicy);
            }
            else
            {
                this.QueryParams.Remove("replicated_user_default_data_policy");
            }
            return this;
        }


        
        
        public RegisterDs3TargetSpectraS3Request(string adminAuthId, string adminSecretKey, string dataPathEndPoint, string name)
        {
            this.AdminAuthId = adminAuthId;
            this.AdminSecretKey = adminSecretKey;
            this.DataPathEndPoint = dataPathEndPoint;
            this.Name = name;
            
            this.QueryParams.Add("admin_auth_id", adminAuthId);

            this.QueryParams.Add("admin_secret_key", adminSecretKey);

            this.QueryParams.Add("data_path_end_point", dataPathEndPoint);

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
                return "/_rest_/ds3_target";
            }
        }
    }
}