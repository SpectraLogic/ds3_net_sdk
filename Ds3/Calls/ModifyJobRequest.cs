/*
 * ******************************************************************************
 *   Copyright 2014 Spectra Logic Corporation. All Rights Reserved.
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

using System;

namespace Ds3.Calls
{
    public class ModifyJobRequest : Ds3Request
    {
        private string _priority;

        public Guid JobId { get; private set; }
        public string Priority {
            get
            {
                return this._priority;
            }
            set
            {
                this.WithPriority(value);
            }
        }

        public ModifyJobRequest WithPriority(string priority)
        {
            this._priority = priority;
            if (priority == null)
            {
                this.QueryParams.Remove("priority");
            }
            else
            {
                this.QueryParams.Add("priority", priority);
            }
            return this;
        }

        public ModifyJobRequest(Guid jobId)
        {
            this.JobId = jobId;
            this._priority = null;
        }

        internal override HttpVerb Verb
        {
            get { return HttpVerb.PUT; }
        }

        internal override string Path
        {
            get { return "_rest_/job/" + this.JobId.ToString(); }
        }
    }
}
