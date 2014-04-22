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
using System.Collections.Generic;

using Ds3.Models;

namespace Ds3.Calls
{
    public class GetObjectRequest : Ds3Request
    {
        public string BucketName { get; private set; }
        public string ObjectName { get; private set; }

        private Range _byteRange;
        public Range ByteRange
        {
            get { return _byteRange; }
            set { WithByteRange(value); }
        }

        /// <summary>
        /// Specifies a range of bytes within the object to retrieve.
        /// </summary>
        /// <param name="byteRange"></param>
        /// <returns></returns>
        public GetObjectRequest WithByteRange(Range byteRange)
        {
            this._byteRange = byteRange;
            return this;
        }

        internal override Range GetByteRange()
        {
            return this._byteRange;
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
                return "/" + BucketName + "/" + ObjectName;
            }
        }

        public GetObjectRequest(Bucket bucket, Ds3Object ds3Object): this (bucket.Name, ds3Object.Name)
        {
        }

        public GetObjectRequest(string bucketName, string ds3ObjectName)
        {
            this.BucketName = bucketName;
            this.ObjectName = ds3ObjectName;
        }
    }
}
