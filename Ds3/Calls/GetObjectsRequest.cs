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
        private string _bucketId;
        public string BucketId { 
            get {return _bucketId; }
            set { WithBucketId(value); }
        }
        protected GetObjectsRequest WithBucketId(string bucket)
        {
            this._bucketId = bucket;
            if (!string.IsNullOrEmpty(bucket))
            {
                this.QueryParams.Add("bucketId", bucket);
            }
            else
            {
                this.QueryParams.Remove("bucketId");
            }
            return this;
        }

        private string _objectName;
        public string ObjectName 
        { 
            get { return _objectId; }
            set { WithObjectName(value); }
        }
        protected GetObjectsRequest WithObjectName(string name)
        {
            this._objectName = name;
            if (!string.IsNullOrEmpty(name))
            {
                this.QueryParams.Add("name", name);
            }
            else
            {
                this.QueryParams.Remove("name");
            }
            return this;
        }

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

        public GetObjectsRequest()
        {            
        }

        
    }
}
