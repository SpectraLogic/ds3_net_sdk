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

namespace Ds3.Models
{
    public class Ds3ObjectInfo : Ds3Object
    {
        public string Etag { get; private set; }
        public DateTime LastModified { get; private set; }
        public Owner Owner { get; private set; }
        public string StorageClass { get; private set; }

        public Ds3ObjectInfo(string name, long? size, Owner owner, string etag, string storageClass, DateTime lastModified)
            : base(name, size)
        {
            this.Owner = owner;
            this.Etag = etag;
            this.StorageClass = storageClass;
            this.LastModified = lastModified;
        }
    }
}
