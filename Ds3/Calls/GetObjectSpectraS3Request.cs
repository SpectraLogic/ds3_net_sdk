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
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ds3.Calls
{
    public class GetObjectSpectraS3Request : Ds3Request
    {
        
        public string ObjectName { get; private set; }

        public Guid BucketId { get; private set; }

        
        private IEnumerable<Range> _byteRanges = Enumerable.Empty<Range>();
        public IEnumerable<Range> ByteRanges
        {
            get { return _byteRanges; }
            set { WithByteRanges(value); }
        }

        public GetObjectSpectraS3Request WithByteRanges(IEnumerable<Range> byteRanges)
        {
            this._byteRanges = byteRanges;
            return this;
        }

        internal override IEnumerable<Range> GetByteRanges()
        {
            return this._byteRanges;
        }

        internal Stream DestinationStream { get; private set; }

        public GetObjectSpectraS3Request(string objectName, Guid bucketId, Stream destinationStream) {
            this.ObjectName = objectName;
            this.BucketId = bucketId;
            this.DestinationStream = destinationStream;
            
            this.QueryParams.Add("bucket_id", BucketId.ToString());

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
                return "/_rest_/object/" + ObjectName;
            }
        }
    }
}