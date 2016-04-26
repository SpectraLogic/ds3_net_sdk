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
    public class DeleteStorageDomainMemberSpectraS3Request : Ds3Request
    {
        
        public string StorageDomainMember { get; private set; }

        
        public DeleteStorageDomainMemberSpectraS3Request(string storageDomainMember) {
            this.StorageDomainMember = storageDomainMember;
            
        }

        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.DELETE
            }
        }

        internal override string Path
        {
            get
            {
                return "/_rest_/storage_domain_member/" + StorageDomainMember;
            }
        }
    }
}