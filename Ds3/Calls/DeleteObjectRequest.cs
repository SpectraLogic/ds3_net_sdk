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

using System.Net;

using Ds3.Models;

namespace Ds3.Calls
{
    public class DeleteObjectRequest : Ds3Request
    {
        public string BucketName { get; private set; }
        public string ObjectName { get; private set; }

        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.DELETE;
            }
        }

        internal override string Path
        {
            get
            {
                return "/" + BucketName + "/" + ObjectName;
            }
        }

        public DeleteObjectRequest(Bucket bucket, Ds3Object ds3Object): this (bucket.Name, ds3Object.Name)
        {
        }

        public DeleteObjectRequest(string bucketName, string ds3ObjectName)
        {
            this.BucketName = bucketName;
            this.ObjectName = ds3ObjectName;
        }

    }
}
