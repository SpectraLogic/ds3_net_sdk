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
        public Guid JobId { get; private set; }
        public long Offset { get; private set; }

        internal override IEnumerable<Range> GetByteRanges()
        {
            return Enumerable.Empty<Range>();
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

        internal Stream DestinationStream { get; private set; }

        public GetObjectRequest(string bucketName, string ds3ObjectName, Guid jobId, long offset, Stream destinationStream)
        {
            this.BucketName = bucketName;
            this.ObjectName = ds3ObjectName;
            this.JobId = jobId;
            this.Offset = offset;
            this.DestinationStream = destinationStream;
            if (jobId != Guid.Empty)
            {
                QueryParams.Add("job", jobId.ToString());
                QueryParams.Add("offset", offset.ToString());
            }
        }
    }
}
