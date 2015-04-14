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

using NUnit.Framework;
using System.Collections.Generic;

namespace TestDs3.Lang
{
    [TestFixture]
    public class TestEnumerableComparer
    {
        [Test]
        public void EnumerablesOfDifferingSizesAreDifferent()
        {
            Assert.AreEqual(
                -1,
                new EnumerableComparer<string>()
                .Compare(new[] { "foo" }, new[] { "foo", "bar" })
            );
            Assert.AreEqual(
                1,
                new EnumerableComparer<string>()
                .Compare(new[] { "foo", "bar" }, new[] { "foo" })
            );
        }

        [Test]
        public void SameSizedEnumerablesWithSameContentsAreEqual()
        {
            AssertCompareEqual(new string[0]);
            AssertCompareEqual(new[] { "foo" });
            AssertCompareEqual(new[] { "foo", "bar" });
            AssertCompareEqual(new[] { "foo", "bar", "baz" });
        }

        private static void AssertCompareEqual(string[] x)
        {
            var y = x.Clone() as string[];
            IComparer<IEnumerable<string>> comparer = new EnumerableComparer<string>();
            Assert.AreEqual(0, comparer.Compare(x, y));
        }

        [Test]
        public void SameSizeEnumerablesWithDifferentContentsAreNotEqual()
        {
            AssertInequalities(new[] { "foo1", "bar", "baz" });
            AssertInequalities(new[] { "foo", "bar1", "baz" });
            AssertInequalities(new[] { "foo", "bar", "baz1" });
        }

        private static void AssertInequalities(string[] subject)
        {
            var reference = new[] { "foo", "bar", "baz" };
            IComparer<IEnumerable<string>> comparer = new EnumerableComparer<string>();
            Assert.AreEqual(-1, comparer.Compare(reference, subject));
            Assert.AreEqual(1, comparer.Compare(subject, reference));
        }
    }
}
