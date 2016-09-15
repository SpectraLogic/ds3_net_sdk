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

using Ds3.Helpers;
using Ds3.Helpers.RangeTranslators;
using Ds3.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace TestDs3.Helpers.RangeTranslators
{
    [TestFixture]
    public class TestPartialObjectRangeUtilities
    {
        private static IEnumerable<Range> _blobs = new[]
        {
            Range.ByLength(0L, 10L),
            Range.ByLength(10L, 10L),
            Range.ByLength(20L, 10L),
            Range.ByLength(30L, 10L),
        };

        private static IEnumerable<Range> _parts1 = new[]
        {
            Range.ByLength(5L, 10L),
            Range.ByLength(15L, 20L),
        };

        private static IEnumerable<Range> _parts2 = new[]
        {
            Range.ByLength(7L, 10L),
            Range.ByLength(18L, 20L),
        };

        private static IEnumerable<ContextRange<Range>> _expectedResults1 = new[]
        { 
            ContextRange.Create(Range.ByPosition(5L, 9L), Range.ByLength(0L, 10L)),
            ContextRange.Create(Range.ByPosition(10L, 14L), Range.ByLength(10L, 10L)),
            ContextRange.Create(Range.ByPosition(15L, 19L), Range.ByLength(10L, 10L)),
            ContextRange.Create(Range.ByPosition(20L, 29L), Range.ByLength(20L, 10L)),
            ContextRange.Create(Range.ByPosition(30L, 34L), Range.ByLength(30L, 10L)),
        };

        private static IEnumerable<ContextRange<Range>> _expectedResults2 = new[]
        { 
            ContextRange.Create(Range.ByPosition(7L, 9L), Range.ByLength(0L, 10L)),
            ContextRange.Create(Range.ByPosition(10L, 16L), Range.ByLength(10L, 10L)),
            ContextRange.Create(Range.ByPosition(18L, 19L), Range.ByLength(10L, 10L)),
            ContextRange.Create(Range.ByPosition(20L, 29L), Range.ByLength(20L, 10L)),
            ContextRange.Create(Range.ByPosition(30L, 37L), Range.ByLength(30L, 10L)),
        };

        [Test]
        public void RangesForRequestsReturnsExpectedLookup()
        {
            var result = PartialObjectRangeUtilities.RangesForRequests(
                _blobs.Select(p => new Ds3.Helpers.Blob(p, "bar")).Concat(_blobs.Select(p => new Ds3.Helpers.Blob(p, "foo"))),
                _parts1.Select(p => new Ds3PartialObject(p, "bar")).Concat(_parts2.Select(p => new Ds3PartialObject(p, "foo")))
            );
            CollectionAssert.AreEqual(
                (
                    from cr in _expectedResults1
                    let blob = new Ds3.Helpers.Blob(cr.Context, "bar")
                    orderby blob, cr.Range
                    select new { Blob = blob, cr.Range }
                ).Concat(
                    from cr in _expectedResults2
                    let blob = new Ds3.Helpers.Blob(cr.Context, "foo")
                    orderby blob, cr.Range
                    select new { Blob = blob, cr.Range }
                ),
                (
                    from grp in result
                    from r in grp
                    orderby grp.Key, r
                    select new { Blob = grp.Key, Range = r }
                )
            );
        }

        [Test]
        public void RangesForObjectRequestsReturnsBlobContextRanges()
        {
            Assert.AreEqual(
                _expectedResults1.Sorted().ToArray(),
                PartialObjectRangeUtilities.RangesForObjectRequests(_blobs, _parts1).Sorted().ToArray()
            );
            Assert.AreEqual(
                _expectedResults2.Sorted().ToArray(),
                PartialObjectRangeUtilities.RangesForObjectRequests(_blobs, _parts2).Sorted().ToArray()
            );
        }

        [Test]
        public void ObjectPartsForFullObjectsReturnsExpectedParts()
        {
            var fullObjects = new[] { "foo", "bar", "baz" };
            var blobs = new[]
            {
                new Ds3.Helpers.Blob(Range.ByLength(10L, 10L), "hello"),
                new Ds3.Helpers.Blob(Range.ByLength(0L, 123L), "baz"),
                new Ds3.Helpers.Blob(Range.ByLength(15L, 15L), "bar"),
                new Ds3.Helpers.Blob(Range.ByLength(100L, 10L), "foo"),
                new Ds3.Helpers.Blob(Range.ByLength(0L, 15L), "bar"),
                new Ds3.Helpers.Blob(Range.ByLength(0L, 100L), "foo"),
            };
            CollectionAssert.AreEquivalent(
                new[]
                {
                    new Ds3PartialObject(Range.ByLength(0L, 123L), "baz"),
                    new Ds3PartialObject(Range.ByLength(0L, 110L), "foo"),
                    new Ds3PartialObject(Range.ByLength(0L, 30L), "bar"),
                },
                PartialObjectRangeUtilities.ObjectPartsForFullObjects(fullObjects, blobs)
            );
        }
    }
}
