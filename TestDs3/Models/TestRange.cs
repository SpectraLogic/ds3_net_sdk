/*
 * ******************************************************************************
 *   Copyright 2014-2016 Spectra Logic Corporation. All Rights Reserved.
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
using System.Linq;
using Ds3.Models;
using NUnit.Framework;

namespace TestDs3.Models
{
    [TestFixture]
    public class TestRange
    {
        private static void RangeIsLessThan(long offset1, long length1, long offset2, long length2)
        {
            Assert.AreEqual(-1,
                Range.ByPosition(offset1, length1).CompareTo((object) Range.ByPosition(offset2, length2)));
            Assert.AreEqual(1, Range.ByPosition(offset2, length2).CompareTo((object) Range.ByPosition(offset1, length1)));

            Assert.AreEqual(-1, Range.ByPosition(offset1, length1).CompareTo(Range.ByPosition(offset2, length2)));
            Assert.AreEqual(1, Range.ByPosition(offset2, length2).CompareTo(Range.ByPosition(offset1, length1)));
        }

        private static readonly Range TargetRange = Range.ByPosition(123L, 321L);

        private static readonly IEnumerable<Range> OverlappingRanges = new[]
        {
            Range.ByPosition(-123L, 123L),
            Range.ByPosition(-123L, 12345L),
            Range.ByPosition(321L, 12345L),
            Range.ByPosition(123L, 321L),
            Range.ByPosition(0L, 1000L)
        };

        private static readonly IEnumerable<Range> NotTouchingRanges = new[]
        {
            Range.ByPosition(-123L, 121L),
            Range.ByPosition(323L, 12345L)
        };

        private static readonly IEnumerable<Range> NonOverlappingRanges =
            NotTouchingRanges.Concat(new[]
            {
                Range.ByPosition(-123L, 122L),
                Range.ByPosition(322L, 12345L)
            });

        private static readonly IEnumerable<Range[]> Intersections = new[]
        {
            Range.ByPosition(123L, 123L),
            Range.ByPosition(123L, 321L),
            Range.ByPosition(321L, 321L),
            Range.ByPosition(123L, 321L),
            Range.ByPosition(123L, 321L)
        }.Zip(OverlappingRanges, (output, input) => new[] {input, output});

        [Test]
        public void BetweenWorks()
        {
            var range = Range.ByPosition(123L, 321L);
            foreach (var outside in new[] {-123L, 0L, 122L, 322L, 123456L})
            {
                Assert.False(outside.Between(range));
            }
            foreach (var inside in new[] {123L, 124L, 200L, 320L, 321L})
            {
                Assert.True(inside.Between(range));
            }
        }

        [Test]
        public void ByLengthWorks()
        {
            var range = Range.ByLength(20, 30);
            Assert.AreEqual(20, range.Start);
            Assert.AreEqual(49, range.End);
            Assert.AreEqual(30, range.Length);
        }

        [Test]
        public void ByPositionWorks()
        {
            var range = Range.ByPosition(20, 49);
            Assert.AreEqual(20, range.Start);
            Assert.AreEqual(49, range.End);
            Assert.AreEqual(30, range.Length);
        }

        [Test]
        public void CompareWorks()
        {
            Assert.Throws<ArgumentNullException>(() => Range.ByPosition(0, 10).CompareTo((object) null));
            Assert.Throws<ArgumentException>(() => Range.ByPosition(0, 10).CompareTo(new {Start = 0L, End = 10L}));

            Assert.AreEqual(0, Range.ByPosition(0, 10).CompareTo((object) Range.ByPosition(0, 10)));
            RangeIsLessThan(0, 10, 0, 11);
            RangeIsLessThan(0, 10, 1, 11);
            RangeIsLessThan(0, 10, 1, 10);
            RangeIsLessThan(0, 10, 1, 8);
        }

        [Test]
        public void EqualsWorks()
        {
            Assert.True(Range.ByPosition(0, 10).Equals(Range.ByPosition(0, 10)));
            Assert.False(Range.ByPosition(1, 10).Equals(Range.ByPosition(0, 10)));
            Assert.False(Range.ByPosition(0, 11).Equals(Range.ByPosition(0, 10)));
            Assert.False(Range.ByPosition(1, 11).Equals(Range.ByPosition(0, 10)));
            Assert.False(Range.ByPosition(1, 11).Equals(null));
            Assert.False(Range.ByPosition(1, 11).Equals(new {Start = 1L, End = 11L}));
        }

        [Test]
        public void GetHashCodeDoesNotThrowException()
        {
            Range.ByPosition(1234, 5678).GetHashCode();
        }

        [Test]
        [TestCaseSource(nameof(Intersections))]
        public void IntersectReturnsCommonRangeParts(Range input, Range output)
        {
            Assert.AreEqual(output, TargetRange.Intersect(input));
            Assert.AreEqual(output, input.Intersect(TargetRange));
        }

        [Test]
        [TestCaseSource(nameof(NotTouchingRanges))]
        public void IntersectThrowsException(Range nonOverlappingRange)
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => nonOverlappingRange.Intersect(TargetRange));
            Assert.Throws<ArgumentOutOfRangeException>(() => TargetRange.Intersect(nonOverlappingRange));
        }

        [Test]
        [TestCaseSource(nameof(NonOverlappingRanges))]
        public void OverlapsReturnsFalse(Range nonOverlappingRange)
        {
            Assert.False(nonOverlappingRange.Overlaps(TargetRange));
            Assert.False(TargetRange.Overlaps(nonOverlappingRange));
        }

        [Test]
        [TestCaseSource(nameof(OverlappingRanges))]
        public void OverlapsReturnsTrue(Range overlappingRange)
        {
            Assert.True(overlappingRange.Overlaps(TargetRange));
            Assert.True(TargetRange.Overlaps(overlappingRange));
        }
    }
}