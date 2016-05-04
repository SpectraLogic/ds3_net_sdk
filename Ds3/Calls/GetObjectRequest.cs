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
    public class GetObjectRequest : Ds3Request
    {
        
        public string BucketName { get; private set; }

        public string ObjectName { get; private set; }

        public string Job { get; private set; }

        public long Offset { get; private set; }

        

        private IEnumerable<Range> _byteRanges = Enumerable.Empty<Range>();
        public IEnumerable<Range> ByteRanges
        {
            get { return _byteRanges; }
            set { WithByteRanges(value); }
        }

        public GetObjectRequest WithByteRanges(IEnumerable<Range> byteRanges)
        {
            this._byteRanges = byteRanges;
            return this;
        }

        internal override IEnumerable<Range> GetByteRanges()
        {
            return this._byteRanges;
        }

        internal Stream DestinationStream { get; private set; }

        public GetObjectRequest(string bucketName, string objectName, Stream destinationStream, Guid job, long offset) {
            this.BucketName = bucketName;
            this.ObjectName = objectName;
            this.Job = job.ToString();
            this.Offset = offset;
            this.DestinationStream = destinationStream;
            
            if (job != null)
            {
                QueryParams.Add("job", job.ToString());
                QueryParams.Add("offset", offset.ToString());
            }
        }
        public GetObjectRequest(string bucketName, string objectName, Stream destinationStream, string job, long offset) {
            this.BucketName = bucketName;
            this.ObjectName = objectName;
            this.Job = job;
            this.Offset = offset;
            this.DestinationStream = destinationStream;
            
            if (job != null)
            {
                QueryParams.Add("job", job.ToString());
                QueryParams.Add("offset", offset.ToString());
            }
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
    }
}