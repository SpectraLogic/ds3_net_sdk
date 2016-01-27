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

namespace Ds3.Calls
{
    public class GetJobListRequest : Ds3Request
    {
        public string Bucket { get; private set; }

        public GetJobListRequest WithBucket(string bucket)
        {
            if (this.Bucket != null)
            {
                this.QueryParams.Remove("bucketId");
            }
            this.QueryParams.Add("bucketId", bucket);
            this.Bucket = bucket;
            return this;
        }

        internal override HttpVerb Verb
        {
            get { return HttpVerb.GET;  }
        }

        internal override string Path
        {
            get { return "/_rest_/job"; }
        }
    }
}
