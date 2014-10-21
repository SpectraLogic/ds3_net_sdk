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

namespace Ds3.Helpers
{
    public class ObjectPart : IComparable<ObjectPart>
    {
        public long Offset { get; private set; }
        public long Length { get; private set; }
        public long End
        {
            get { return this.Offset + this.Length - 1; }
        }

        public ObjectPart(long offset, long length)
        {
            this.Offset = offset;
            this.Length = length;
        }

        public int CompareTo(ObjectPart other)
        {
            return Math.Sign(this.Length - other.Length)
                + 2 * Math.Sign(this.Offset - other.Offset);
        }
    }
}
