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
using System.Net;
using System.Collections.Generic;

using Ds3.Models;
using System.IO;

namespace Ds3.Calls
{
    public class GetObjectRequest : Ds3Request
    {
        private readonly Stream _destinationStream;
        public string BucketName { get; private set; }
        public string ObjectName { get; private set; }
        public Guid JobId { get; private set; }

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

        internal Stream DestinationStream
        {
            get { return this._destinationStream; }
        }

        [Obsolete]
        public GetObjectRequest(Bucket bucket, Ds3Object ds3Object, Stream destinationStream)
            : this(bucket.Name, ds3Object.Name, destinationStream)
        {
        }

        [Obsolete]
        public GetObjectRequest(string bucketName, string ds3ObjectName, Stream destinationStream)
            : this(bucketName, ds3ObjectName, Guid.Empty, destinationStream)
        {
        }

        public GetObjectRequest(string bucketName, string ds3ObjectName, Guid jobId, Stream destinationStream)
        {
            this._destinationStream = destinationStream;
            this.BucketName = bucketName;
            this.ObjectName = ds3ObjectName;
            this.JobId = jobId;
            if (jobId != Guid.Empty)
            {
                QueryParams.Add("job", jobId.ToString());
            }
        }
    }
}
