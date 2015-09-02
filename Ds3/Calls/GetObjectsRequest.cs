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
        public string ObjectName { get; private set; }

        private string _objectId;
        public string ObjectId
        {
            get { return _objectId; }
            set { WithObjectId(value); }
        }
        protected GetObjectsRequest WithObjectId(string ds3ObjectId)
        {
            this._objectId = ds3ObjectId;
            if (!string.IsNullOrEmpty(ds3ObjectId))
            {
                this.QueryParams.Add("id", ds3ObjectId);
            }
            else
            {
                this.QueryParams.Remove("id");
            }
            return this;
        }

        private DS3ObjectTypes _objectType;
        public DS3ObjectTypes ObjectType
        { 
            get { return _objectType; }
            set { WithObjectType(value); }
        }
        protected GetObjectsRequest WithObjectType(DS3ObjectTypes type)
        {
            this._objectType = type;
            if (type != DS3ObjectTypes.ALL)
            {
                this.QueryParams.Add("type", type.ToString());
            }
            else
            {
                this.QueryParams.Remove("type");
            }
            return this;
        }

        private long? _length;
        public long? Length
        {
            get { return _length; }
            set { WithLength(value); }
        }
        protected GetObjectsRequest WithLength(long? length)
        {
            this._length = length;
            if (length != null && length > 0L)
            {
                this.QueryParams.Add("page_length", length.ToString());
            }
            else
            {
                this.QueryParams.Remove("page_length");
            }
            return this;
        }

        private long? _offset;
        public long? Offset
        {
            get { return _offset; }
            set { WithOffset(value); }
        }
        protected GetObjectsRequest WithOffset(long? offset)
        {
            this._offset = offset;
            if (offset != null && offset > 0L)
            {
                this.QueryParams.Add("page_offset", offset.ToString());
            }
            else
            {
                this.QueryParams.Remove("page_offset");
            }
            return this;
        }

        private long? _version;
        public long? Version
        {
            get { return _version; }
            set { WithVersion(value); }
        }
        protected GetObjectsRequest WithVersion(long? version)
        {
            this._version = version;
            if (version != null && version > 0L)
            {
                this.QueryParams.Add("version", version.ToString());
            }
            else
            {
                this.QueryParams.Remove("version");
            }
            return this;
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
            this.ObjectType = DS3ObjectTypes.ALL;
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
            this.Length = length;
            this.Offset = offset;
            this.Version = version;
            this.ObjectType = type;
        }
    }
}
