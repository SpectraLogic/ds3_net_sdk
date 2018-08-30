/*
 * ******************************************************************************
 *   Copyright 2014-2017 Spectra Logic Corporation. All Rights Reserved.
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
    public class Ds3Object : IComparable
    {
        public string Name { get; private set; }
        public long? Size { get; private set; }
        public string VersionId { get; private set; }

        public Ds3Object(string name, long? size) : this(name, size, null)
        {
        }

        public Ds3Object(string name, long? size, string versionId)
        {
            this.Name = name;
            this.Size = size;
            this.VersionId = versionId;
        }

        public override bool Equals(object obj)
        {
            var ds3Obj = obj as Ds3Object;
            if (ds3Obj == null)
            {
                return false;
            }
            return this.Name == ds3Obj.Name && this.Size == ds3Obj.Size && this.VersionId == ds3Obj.VersionId;
        }

        public override int GetHashCode()
        {
            // Algorithm based on http://stackoverflow.com/a/263416/472522
            unchecked
            {
                int hash = 512927377;
                hash = hash * 15485863 + this.Name.GetHashCode();
                hash = hash * 15485863 + this.Size.GetHashCode();
                if (this.VersionId != null)
                {
                    hash = hash * 15485863 + this.VersionId.GetHashCode();
                }
                return hash;
            }
        }

        public override string ToString()
        {
            return string.Format("Ds3Object[{0}, {1}, {2}]", this.Name, this.Size, this.VersionId);
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            var ds3Object = obj as Ds3Object;
            if (ds3Object == null)
            {
                throw new ArgumentException(Resources.ExpectedObjectOfSameTypeException, "obj");
            }
            return Math.Sign(
                Math.Sign(this.Name.CompareTo(ds3Object.Name))
                + 2 * Math.Sign(this.Size == ds3Object.Size ? 0: 1)
                + 3 * Math.Sign(this.VersionId == ds3Object.VersionId ? 0: 1)
            );
        }
    }
}
