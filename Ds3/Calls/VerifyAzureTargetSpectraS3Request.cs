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
    public class VerifyAzureTargetSpectraS3Request : Ds3Request
    {
        
        public string AzureTarget { get; private set; }

        
        private bool? _fullDetails;
        public bool? FullDetails
        {
            get { return _fullDetails; }
            set { WithFullDetails(value); }
        }

        
        public VerifyAzureTargetSpectraS3Request WithFullDetails(bool? fullDetails)
        {
            this._fullDetails = fullDetails;
            if (fullDetails != null)
            {
                this.QueryParams.Add("full_details", fullDetails.ToString());
            }
            else
            {
                this.QueryParams.Remove("full_details");
            }
            return this;
        }


        
        
        public VerifyAzureTargetSpectraS3Request(string azureTarget)
        {
            this.AzureTarget = azureTarget;
            this.QueryParams.Add("operation", "verify");
            
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