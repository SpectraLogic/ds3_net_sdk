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

using Ds3.Helpers;
using Ds3.Helpers.RangeTranslators;
using Ds3.Lang;
using Ds3.Models;
using NUnit.Framework;
using System.Linq;

namespace TestDs3.Helpers.RangeTranslators
{
    [TestFixture]
    public class TestRequestToObjectRangeTranslator
    {
        [Test]
        public void CanTranslateRequestRangeToObjectRanges()
        {
            var blobs = new[]
            {
                new Blob(Range.ByLength(0L, 10L), "foo"),
                new Blob(Range.ByLength(10L, 10L), "foo"),
                new Blob(Range.ByLength(20L, 10L), "foo"),
                new Blob(Range.ByLength(0L, 15L), "bar"),
                new Blob(Range.ByLength(15L, 15L), "bar"),
            };
            var rt = new RequestToObjectRangeTranslator(
                new[]
                {
                    new[] { Range.ByLength(5L, 5L) }.ToGrouping(blobs[0]),
                    new[]
                    {
                        Range.ByLength(10L, 5L),
                        Range.ByLength(17L, 3L),
                    }.ToGrouping(blobs[1]),
                    new[] { Range.ByLength(20L, 7L) }.ToGrouping(blobs[2]),
                }.ToLookup()
            );
            CollectionAssert.AreEqual(
                new[]
                {
                    ContextRange.Create(Range.ByPosition(5L, 9L), "foo"),
                },
                rt.Translate(ContextRange.Create(Range.ByLength(0L, 5L), blobs[0])).ToArray()
            );
            CollectionAssert.AreEqual(
                new[]
                {
                    ContextRange.Create(Range.ByPosition(10L, 14L), "foo"),
                    ContextRange.Create(Range.ByPosition(17L, 19L), "foo"),
                },
                rt.Translate(ContextRange.Create(Range.ByLength(0L, 8L), blobs[1])).ToArray()
            );
            CollectionAssert.AreEqual(
                new[]
                {
                    ContextRange.Create(Range.ByPosition(20L, 26L), "foo"),
                },
                rt.Translate(ContextRange.Create(Range.ByLength(0L, 8L), blobs[2])).ToArray()
            );
        }
    }
}
