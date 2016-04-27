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
using Ds3.Models;
using System;
using System.Net;

namespace Ds3.Calls
{
    public class GetJobChunksReadyForClientProcessingSpectraS3Request : Ds3Request
    {
        
        public Guid Job { get; private set; }

        
        private int _preferredNumberOfChunks;
        public int PreferredNumberOfChunks
        {
            get { return _preferredNumberOfChunks; }
            set { WithPreferredNumberOfChunks(value); }
        }

        public GetJobChunksReadyForClientProcessingSpectraS3Request WithPreferredNumberOfChunks(int preferredNumberOfChunks)
        {
            this._preferredNumberOfChunks = preferredNumberOfChunks;
            if (preferredNumberOfChunks != null) {
                this.QueryParams.Add("preferred_number_of_chunks", PreferredNumberOfChunks.ToString());
            }
            else
            {
                this.QueryParams.Remove("preferred_number_of_chunks");
            }
            return this;
        }

        public GetJobChunksReadyForClientProcessingSpectraS3Request(Guid job) {
            this.Job = job;
            
            this.QueryParams.Add("job", Job.ToString());

        }

        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.GET;
            }
        }

        internal override string Path
        {
            get
            {
                return "/_rest_/job_chunk/";
            }
        }
    }
}