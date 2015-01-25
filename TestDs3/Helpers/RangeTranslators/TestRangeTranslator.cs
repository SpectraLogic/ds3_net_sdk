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

using Ds3.Helpers.RangeTranslators;
using Ds3.Models;
using Moq;
using NUnit.Framework;
using Range = Ds3.Models.Range;

namespace TestDs3.Helpers.RangeTranslators
{
    [TestFixture]
    public class TestRangeTranslator
    {
        [Test]
        public void RangeTranslatorCompositionWorks()
        {
            var range_1_1 = ContextRange.Create(Range.ByLength(0L, 10L), 10L);
            var range_2_1 = ContextRange.Create(Range.ByLength(0L, 5L), "foo");
            var range_2_2 = ContextRange.Create(Range.ByLength(5L, 5L), "bar");
            var range_3_1 = ContextRange.Create(Range.ByLength(0L, 2L), false);
            var range_3_2 = ContextRange.Create(Range.ByLength(2L, 3L), false);
            var range_3_3 = ContextRange.Create(Range.ByLength(5L, 2L), true);
            var range_3_4 = ContextRange.Create(Range.ByLength(7L, 3L), true);

            var first = new Mock<IRangeTranslator<long, string>>(MockBehavior.Strict);
            first
                .Setup(rt => rt.Translate(range_1_1))
                .Returns(new[] { range_2_1, range_2_2 });
            var second = new Mock<IRangeTranslator<string, bool>>(MockBehavior.Strict);
            second
                .Setup(rt => rt.Translate(range_2_1))
                .Returns(new[] { range_3_1, range_3_2 });
            second
                .Setup(rt => rt.Translate(range_2_2))
                .Returns(new[] { range_3_3, range_3_4 });
            var composed = first.Object.ComposedWith(second.Object);

            var result = composed.Translate(ContextRange.Create(Range.ByLength(0L, 10L), 10L));
            CollectionAssert.AreEqual(
                new[]
                {
                    range_3_1,
                    range_3_2,
                    range_3_3,
                    range_3_4,
                },
                result
            );
        }
    }
}
