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
    public class AllocateJobChunkSpectraS3Request : Ds3Request
    {
        
        public string JobChunkId { get; private set; }

        

        
        public AllocateJobChunkSpectraS3Request(Guid jobChunkId) {
            this.JobChunkId = jobChunkId.ToString();
            this.QueryParams.Add("operation", "allocate");
            
        }

        public AllocateJobChunkSpectraS3Request(string jobChunkId) {
            this.JobChunkId = jobChunkId;
            this.QueryParams.Add("operation", "allocate");
            
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
                return "/_rest_/job_chunk/" + JobChunkId.ToString();
            }
        }
    }
}