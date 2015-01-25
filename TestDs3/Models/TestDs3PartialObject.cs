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
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace TestDs3.Models
{
    [TestFixture]
    public class TestDs3PartialObject
    {
        [Test]
        public void EqualsWorks()
        {
            var item = new Ds3PartialObject(Range.ByLength(1, 10), "foo");
            Assert.True(item.Equals(item));
            Assert.True(item.Equals(new Ds3PartialObject (Range.ByLength(1, 10), "foo")));
            Assert.False(item.Equals(new Ds3PartialObject(Range.ByLength(1, 10), "bar")));
            Assert.False(item.Equals(new Ds3PartialObject(Range.ByLength(0, 10), "foo")));
            Assert.False(item.Equals(new Ds3PartialObject(Range.ByLength(0, 10), "bar")));
            Assert.False(item.Equals(null));
            Assert.False(item.Equals(new { Name = "foo", Range = Range.ByLength(1, 10) }));
        }

        [Test]
        public void GetHashCodeDoesNotThrowException()
        {
            Assert.AreNotEqual(0, new Ds3PartialObject(Range.ByLength(1, 10), "foo").GetHashCode());
        }

        private static IEnumerable<object[]> ComparisonTestCases = new[]
        {
            new object[] { -1, new Ds3PartialObject(Range.ByLength(1, 10), "grand") },
            new object[] { -1, new Ds3PartialObject(Range.ByLength(0, 10), "grand") },
            new object[] { -1, new Ds3PartialObject(Range.ByLength(2, 10), "grand") },

            new object[] { -1, new Ds3PartialObject(Range.ByLength(2, 10), "foo") },
            new object[] { 0, new Ds3PartialObject (Range.ByLength(1, 10), "foo") },
            new object[] { 1, new Ds3PartialObject (Range.ByLength(0, 10), "foo") },

            new object[] { 1, new Ds3PartialObject(Range.ByLength(1, 10), "bar") },
            new object[] { 1, new Ds3PartialObject(Range.ByLength(0, 10), "bar") },
            new object[] { 1, new Ds3PartialObject(Range.ByLength(2, 10), "bar") },
        };

        [Test]
        public void CompareToWithInvalidTypesThrowsException()
        {
            var item = new Ds3PartialObject(Range.ByLength(1, 10), "foo");
            Assert.Throws<ArgumentException>(() => item.CompareTo(new { Name = "foo", Range = Range.ByLength(1, 10) }));
            Assert.Throws<ArgumentNullException>(() => item.CompareTo(null));
        }

        [Test, TestCaseSource("ComparisonTestCases")]
        public void CompareToWorks(int result, Ds3PartialObject other)
        {
            var item = new Ds3PartialObject(Range.ByLength(1, 10), "foo");
            Assert.AreEqual(result, item.CompareTo((object)other));
            Assert.AreEqual(result, item.CompareTo(other));
        }
    }
}
