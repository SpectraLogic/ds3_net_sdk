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
    public class ModifyJobSpectraS3Request : Ds3Request
    {
        
        public Guid JobId { get; private set; }

        
        private DateTime _createdAt;
        public DateTime CreatedAt
        {
            get { return _createdAt; }
            set { WithCreatedAt(value); }
        }

        public ModifyJobSpectraS3Request WithCreatedAt(DateTime createdAt)
        {
            this._createdAt = createdAt;
            if (createdAt != null) {
                this.QueryParams.Add("created_at", createdAt.ToString());
            }
            else
            {
                this.QueryParams.Remove("created_at");
            }
            return this;
        }

        private string _name;
        public string Name
        {
            get { return _name; }
            set { WithName(value); }
        }

        public ModifyJobSpectraS3Request WithName(string name)
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

        private Priority _priority;
        public Priority Priority
        {
            get { return _priority; }
            set { WithPriority(value); }
        }

        public ModifyJobSpectraS3Request WithPriority(Priority priority)
        {
            this._priority = priority;
            if (priority != null) {
                this.QueryParams.Add("priority", priority.ToString());
            }
            else
            {
                this.QueryParams.Remove("priority");
            }
            return this;
        }

        public ModifyJobSpectraS3Request(Guid jobId) {
            this.JobId = jobId;
            
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
                return "/_rest_/job/" + jobId.ToString();
            }
        }
    }
}