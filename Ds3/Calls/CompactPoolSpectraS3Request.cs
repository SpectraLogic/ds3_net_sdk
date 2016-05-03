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
    public class CompactPoolSpectraS3Request : Ds3Request
    {
        
        public string Pool { get; private set; }

        
        private Priority _priority;
        public Priority Priority
        {
            get { return _priority; }
            set { WithPriority(value); }
        }

        public CompactPoolSpectraS3Request WithPriority(Priority priority)
        {
            this._priority = priority;
            if (priority != null) {
                this.QueryParams.Add("priority", Priority.ToString());
            }
            else
            {
                this.QueryParams.Remove("priority");
            }
            return this;
        }

        public CompactPoolSpectraS3Request(string pool) {
            this.Pool = pool;
            this.QueryParams.Add("operation", "compact");
            
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
                return "/_rest_/pool/" + Pool;
            }
        }
    }
}