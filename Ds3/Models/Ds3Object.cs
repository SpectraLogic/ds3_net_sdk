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
    public class Ds3Object
    {
        public Ds3Object(string name) 
            : this(name, null)
        {
        }

        public Ds3Object(string name, long? size)
            : this(name, size, null)
        {
        }

        public Ds3Object(string name, long? size, Owner owner)
            : this(name, size, owner, "", "", Convert.ToDateTime(null))
        {
        }

        public Ds3Object(string name, long? size, Owner owner, string etag)
            : this(name, size, owner, etag, "", Convert.ToDateTime(null))
        {
        }

        public Ds3Object(string name, long? size, Owner owner, string etag, string storageClass, DateTime lastModified)
        {
            this.Name = name;
            this.Size = size;
            this.Owner = owner;
            this.Etag = etag;
            this.StorageClass = storageClass;
            this.LastModified = lastModified;
        }

        public string Name { get; private set; }
        public string Etag { get; private set; }
        public DateTime LastModified { get; private set; }
        public Owner Owner { get; private set; }
        public long? Size { get; private set; }
        public string StorageClass { get; private set; }
    }
}
