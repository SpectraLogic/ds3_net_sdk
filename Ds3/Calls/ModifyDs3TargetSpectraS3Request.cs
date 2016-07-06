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
    public class ModifyDs3TargetSpectraS3Request : Ds3Request
    {
        
        public string Ds3Target { get; private set; }

        
        private Ds3TargetAccessControlReplication? _accessControlReplication;
        public Ds3TargetAccessControlReplication? AccessControlReplication
        {
            get { return _accessControlReplication; }
            set { WithAccessControlReplication(value); }
        }

        private string _adminAuthId;
        public string AdminAuthId
        {
            get { return _adminAuthId; }
            set { WithAdminAuthId(value); }
        }

        private string _adminSecretKey;
        public string AdminSecretKey
        {
            get { return _adminSecretKey; }
            set { WithAdminSecretKey(value); }
        }

        private string _dataPathEndPoint;
        public string DataPathEndPoint
        {
            get { return _dataPathEndPoint; }
            set { WithDataPathEndPoint(value); }
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

        private string _replicatedUserDefaultDataPolicy;
        public string ReplicatedUserDefaultDataPolicy
        {
            get { return _replicatedUserDefaultDataPolicy; }
            set { WithReplicatedUserDefaultDataPolicy(value); }
        }

        public ModifyDs3TargetSpectraS3Request WithAccessControlReplication(Ds3TargetAccessControlReplication? accessControlReplication)
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
        public ModifyDs3TargetSpectraS3Request WithAdminAuthId(string adminAuthId)
        {
            this._adminAuthId = adminAuthId;
            if (adminAuthId != null)
            {
                this.QueryParams.Add("admin_auth_id", adminAuthId);
            }
            else
            {
                this.QueryParams.Remove("admin_auth_id");
            }
            return this;
        }
        public ModifyDs3TargetSpectraS3Request WithAdminSecretKey(string adminSecretKey)
        {
            this._adminSecretKey = adminSecretKey;
            if (adminSecretKey != null)
            {
                this.QueryParams.Add("admin_secret_key", adminSecretKey);
            }
            else
            {
                this.QueryParams.Remove("admin_secret_key");
            }
            return this;
        }
        public ModifyDs3TargetSpectraS3Request WithDataPathEndPoint(string dataPathEndPoint)
        {
            this._dataPathEndPoint = dataPathEndPoint;
            if (dataPathEndPoint != null)
            {
                this.QueryParams.Add("data_path_end_point", dataPathEndPoint);
            }
            else
            {
                this.QueryParams.Remove("data_path_end_point");
            }
            return this;
        }
        public ModifyDs3TargetSpectraS3Request WithDataPathHttps(bool? dataPathHttps)
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
        public ModifyDs3TargetSpectraS3Request WithDataPathPort(int? dataPathPort)
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
        public ModifyDs3TargetSpectraS3Request WithDataPathProxy(string dataPathProxy)
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
        public ModifyDs3TargetSpectraS3Request WithDataPathVerifyCertificate(bool? dataPathVerifyCertificate)
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
        public ModifyDs3TargetSpectraS3Request WithDefaultReadPreference(TargetReadPreference? defaultReadPreference)
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
        public ModifyDs3TargetSpectraS3Request WithName(string name)
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
        public ModifyDs3TargetSpectraS3Request WithPermitGoingOutOfSync(bool? permitGoingOutOfSync)
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
        public ModifyDs3TargetSpectraS3Request WithQuiesced(Quiesced? quiesced)
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
        public ModifyDs3TargetSpectraS3Request WithReplicatedUserDefaultDataPolicy(string replicatedUserDefaultDataPolicy)
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

        
        public ModifyDs3TargetSpectraS3Request(string ds3Target)
        {
            this.Ds3Target = ds3Target;
            
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
                return "/_rest_/ds3_target/" + Ds3Target;
            }
        }
    }
}