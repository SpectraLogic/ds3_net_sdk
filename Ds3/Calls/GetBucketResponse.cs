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
using System.Collections.Generic;

using Ds3.Models;

namespace Ds3.Calls
{
    public class GetBucketResponse
    {
        public string Name { get; private set; }
        public string Prefix { get; private set; }
        public string Marker { get; private set; }
        public int MaxKeys { get; private set; }
        public bool IsTruncated { get; private set; }
        public string Delimiter { get; private set; }
        public string NextMarker { get; private set; }
        public DateTime CreationDate { get; private set; }
        public IEnumerable<Ds3ObjectInfo> Objects { get; private set; }
        public IDictionary<string, string> Metadata { get; private set; }

        public GetBucketResponse(
            string name,
            string prefix,
            string marker,
            int maxKeys,
            bool isTruncated,
            string delimiter,
            string nextMarker,
            DateTime creationDate,
            IEnumerable<Ds3ObjectInfo> objects,
            IDictionary<string, string> metadata)
        {
            this.Name = name;
            this.Prefix = prefix;
            this.Marker = marker;
            this.MaxKeys = maxKeys;
            this.IsTruncated = isTruncated;
            this.Delimiter = delimiter;
            this.NextMarker = nextMarker;
            this.CreationDate = creationDate;
            this.Objects = objects;
            this.Metadata = metadata;
        }
    }
}
