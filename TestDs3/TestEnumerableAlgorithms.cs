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
using System.Collections.Generic;
using System.Linq;

using NUnit.Framework;

using Ds3.Helpers;

namespace TestDs3
{
    [TestFixture]
    public class TestEnumerableAlgorithms
    {
        private readonly IEnumerable<IEnumerable<string>> _inputRowsForMentionTests = new[] {
            new[] { "a", "b" },
            new[] { "a", "c" },
            new[] { "b", "c" },
            new[] { "d" }
        };

        [Test]
        public void TestFirstMentionsPerRow()
        {
            var expectedOpens = new[] {
                new[] { "a", "b" },
                new[] { "c" },
                new string[0],
                new[] { "d" }
            };
            AssertNestedCollectionsEqual(expectedOpens, _inputRowsForMentionTests.FirstMentionsPerRow());
        }

        [Test]
        public void TestLastMentionsPerRow()
        {
            var expectedCloses = new[] {
                new string[0],
                new[] { "a" },
                new[] { "b", "c" },
                new[] { "d" }
            };
            AssertNestedCollectionsEqual(expectedCloses, _inputRowsForMentionTests.LastMentionsPerRow());
        }

        private static void AssertNestedCollectionsEqual<T>(IEnumerable<IEnumerable<T>> expected, IEnumerable<IEnumerable<T>> actual)
        {
            HelpersForTest.AssertCollectionsEqual(expected, actual, (innerExpected, innerActual) =>
                HelpersForTest.AssertCollectionsEqual(innerExpected, innerActual, (expectedItem, actualItem) =>
                    Assert.AreEqual(expectedItem, actualItem)));
        }
    }
}
