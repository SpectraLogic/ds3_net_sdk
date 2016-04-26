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
    public class PutPoolPartitionSpectraS3Request : Ds3Request
    {
        
        public string Name { get; private set; }

        public PoolType Type { get; private set; }

        
        public PutPoolPartitionSpectraS3Request(string name, PoolType type) {
            this.Name = name;
            this.Type = type;
            
            this.QueryParams.Add("name", Name);

            this.QueryParams.Add("type", type.ToString());

        }

        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.POST
            }
        }

        internal override string Path
        {
            get
            {
                return "/_rest_/pool_partition/";
            }
        }
    }
}