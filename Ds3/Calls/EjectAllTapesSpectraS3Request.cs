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
    public class EjectAllTapesSpectraS3Request : Ds3Request
    {
        
        
        private string _ejectLabel;
        public string EjectLabel
        {
            get { return _ejectLabel; }
            set { WithEjectLabel(value); }
        }

        public EjectAllTapesSpectraS3Request WithEjectLabel(string ejectLabel)
        {
            this._ejectLabel = ejectLabel;
            if (ejectLabel != null) {
                this.QueryParams.Add("eject_label", EjectLabel);
            }
            else
            {
                this.QueryParams.Remove("eject_label");
            }
            return this;
        }

        private string _ejectLocation;
        public string EjectLocation
        {
            get { return _ejectLocation; }
            set { WithEjectLocation(value); }
        }

        public EjectAllTapesSpectraS3Request WithEjectLocation(string ejectLocation)
        {
            this._ejectLocation = ejectLocation;
            if (ejectLocation != null) {
                this.QueryParams.Add("eject_location", EjectLocation);
            }
            else
            {
                this.QueryParams.Remove("eject_location");
            }
            return this;
        }

        public EjectAllTapesSpectraS3Request() {
            this.QueryParams.Add("operation", "eject");
            
        }

        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.PUT
            }
        }

        internal override string Path
        {
            get
            {
                return "/_rest_/tape/";
            }
        }
    }
}