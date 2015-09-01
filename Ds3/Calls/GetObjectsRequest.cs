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
    public class GetObjectsRequest : Ds3Request
    {
        // bucketId, id, name, page_length, page_offset, type, version
        public string BucketId { get; private set; }
        public string ObjectId { get; private set; }
        public string ObjectName { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DS3ObjectTypes ObjectType { get; private set; }
        public long? Length { get; private set; }
        public long? Offset { get; private set; }
        public long? Version { get; private set; }

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
                return "/_rest_/object/";
            }
        }

        public GetObjectsRequest(string bucketId, string ds3ObjectName)
        {
            this.BucketId = bucketId;
            if (!string.IsNullOrEmpty(bucketId))
            {
                this.QueryParams.Add("bucketId", bucketId);
            }
            this.ObjectName = ds3ObjectName;
            if (!string.IsNullOrEmpty(ds3ObjectName))
            {
                this.QueryParams.Add("name", ds3ObjectName);
            }
        }

        public GetObjectsRequest(string bucketId, string ds3ObjectName, string ds3ObjectId, long length, long offset, DS3ObjectTypes type, long version)
        {
            // bucketId, id, name, page_length, page_offset, type, version
            this.BucketId = bucketId;
            if (!string.IsNullOrEmpty(bucketId))
            {
                this.QueryParams.Add("bucketId", bucketId);
            }
            this.ObjectName = ds3ObjectName;
            if (!string.IsNullOrEmpty(ds3ObjectName))
            {
                this.QueryParams.Add("name", ds3ObjectName);
            }
            this.ObjectId = ds3ObjectId;
            if (!string.IsNullOrEmpty(ds3ObjectId))
            {
                this.QueryParams.Add("id", ds3ObjectId);
            }
            this.Length = length;
            if (length != null &&  length > 0L)
            {
                this.QueryParams.Add("page_length", length.ToString());
            }
            this.Offset = offset;
            if (offset != null &&  offset > 0L)
            {
                this.QueryParams.Add("page_offset", offset.ToString());
            }
            this.Version = version;
            if (version != null &&  version > 0L)
            {
                this.QueryParams.Add("version", version.ToString());
            }
            this.ObjectType = type;
            if (type != null)
            {
                this.QueryParams.Add("type", type.ToString());
            }
        }
    }
}
