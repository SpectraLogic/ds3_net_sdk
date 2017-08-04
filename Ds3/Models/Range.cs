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
    public struct Range : IComparable, IComparable<Range>
    {
        public static Range ByLength(long start, long length)
        {
            if (length == 0)
            {
                return new Range(start, start, length);
            }
            return new Range(start, start + length - 1, length);
        }

        public static Range ByPosition(long start, long end)
        {
            if (start == 0 && end == 0)
            {
                return new Range(start, start, 0);
            }
            return new Range(start, end, end - start + 1);
        }

        public long Start { get; private set; }
        public long End { get; private set; }
        public long Length { get; private set; }

        private Range(long start, long end, long length)
            : this()
        {
            this.Start = start;
            this.End = end;
            this.Length = length;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Range))
            {
                return false;
            }
            var rangeObj = (Range)obj;
            return
                this.Start == rangeObj.Start
                && this.End == rangeObj.End;
        }

        public override int GetHashCode()
        {
            // Algorithm based on http://stackoverflow.com/a/263416/472522
            unchecked
            {
                int hash = 486187739;
                hash = hash * 16777619 + Start.GetHashCode();
                hash = hash * 16777619 + End.GetHashCode();
                return hash;
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            if (obj is Range)
            {
                return CompareTo((Range)obj);
            }
            throw new ArgumentException(Resources.ExpectedObjectOfSameTypeException, "obj");
        }

        public int CompareTo(Range other)
        {
            return Math.Sign(2 * Math.Sign(this.Start.CompareTo(other.Start)) + Math.Sign(this.End.CompareTo(other.End)));
        }

        public override string ToString()
        {
            return string.Format("[{0}, {1}]", this.Start, this.End);
        }
    }

    public static class RangeExtensions
    {
        public static bool Between(this long self, Range range)
        {
            return self >= range.Start && self <= range.End;
        }

        public static bool Overlaps(this Range first, Range second)
        {
            return
                first.Start.Between(second)
                || first.End.Between(second)
                || second.Start.Between(first)
                || second.End.Between(first);
        }

        public static Range Intersect(this Range first, Range second)
        {
            var result = Range.ByPosition(
                Math.Max(first.Start, second.Start),
                Math.Min(first.End, second.End)
            );
            if (result.Length < 0)
            {
                throw new ArgumentOutOfRangeException(Resources.RangesDoNotOverlapException);
            }
            return result;
        }
    }
}
