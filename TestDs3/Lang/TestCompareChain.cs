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

namespace TestDs3.Lang
{
    [TestFixture]
    public class TestCompareChain
    {
        [Test]
        public void CompareChainStartsAtZero()
        {
            Assert.AreEqual(0, CompareChain.Of("foo", "bar").Result);
        }

        [Test]
        public void CompareChainReturnsFirstNonZeroDefaultComparerResult()
        {
            Assert.AreEqual(
                -1,
                CompareChain.Of(new { foo = "foo", bar = "bar" }, new { foo = "foo1", bar = "bar" })
                    .Value(it => it.foo)
                    .Value(it => it.bar)
                    .Result
            );
            Assert.AreEqual(
                1,
                CompareChain.Of(new { foo = "foo", bar = "bar1" }, new { foo = "foo", bar = "bar" })
                    .Value(it => it.foo)
                    .Value(it => it.bar)
                    .Result
            );
            Assert.AreEqual(
                0,
                CompareChain.Of(new { foo = "foo", bar = "bar" }, new { foo = "foo", bar = "bar" })
                    .Value(it => it.foo)
                    .Value(it => it.bar)
                    .Result
            );
        }

        [Test]
        public void CompareChainCanCompareNullables()
        {
            Assert.AreEqual(
                0,
                CompareChain.Of(new { foo = (int?)1 }, new { foo = (int?)1 })
                    .Value(it => it.foo)
                    .Result
            );
            Assert.AreEqual(
                -1,
                CompareChain.Of(new { foo = (int?)null }, new { foo = (int?)1 })
                    .Value(it => it.foo)
                    .Result
            );
            Assert.AreEqual(
                1,
                CompareChain.Of(new { foo = (int?)1 }, new { foo = (int?)null })
                    .Value(it => it.foo)
                    .Result
            );
            Assert.AreEqual(
                -1,
                CompareChain.Of(new { foo = (int?)1 }, new { foo = (int?)2 })
                    .Value(it => it.foo)
                    .Result
            );
            Assert.AreEqual(
                1,
                CompareChain.Of(new { foo = (int?)2 }, new { foo = (int?)1 })
                    .Value(it => it.foo)
                    .Result
            );
        }
    }
}
