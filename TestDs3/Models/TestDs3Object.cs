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

using Ds3.Models;
using NUnit.Framework;

namespace TestDs3.Models
{
    [TestFixture]
    public class TestDs3Object
    {
        private static readonly Ds3Object[] Ds3Objects = {
            new Ds3Object("foo", null),
            new Ds3Object("bar", null),
            new Ds3Object("foo", 123L),
            new Ds3Object("bar", 123L),
            new Ds3Object("foo", 321L),
            new Ds3Object("bar", 321L),
        };

        [Test]
        [TestCaseSource(nameof(Ds3Objects))]
        public void Ds3ObjectEqualsReturnsFalseWhenComparingNull(Ds3Object ds3Object)
        {
            Assert.IsFalse(ds3Object.Equals(null));
        }

        [Test]
        public void Ds3ObjectEqualsReturnsFalseWhenComparingOthers()
        {
            for (var i = 0; i < Ds3Objects.Length; i++)
            {
                for (var j = 0; j < Ds3Objects.Length; j++)
                {
                    if (i != j)
                    {
                        Assert.IsFalse(Ds3Objects[i].Equals(Ds3Objects[j]));
                    }
                }
            }
        }

        [Test]
        [TestCaseSource(nameof(Ds3Objects))]
        public void Ds3ObjectEqualsReturnsTrueWhenCopyOfSelf(Ds3Object ds3Object)
        {
            Assert.IsTrue(ds3Object.Equals(new Ds3Object(ds3Object.Name, ds3Object.Size)));
        }

        [Test]
        [TestCaseSource(nameof(Ds3Objects))]
        public void Ds3ObjectEqualsReturnsTrueWhenSameInstance(Ds3Object ds3Object)
        {
            Assert.IsTrue(ds3Object.Equals(ds3Object));
        }

        [Test]
        [TestCaseSource(nameof(Ds3Objects))]
        public void Ds3ObjectsReturnConsistentHashCodes(Ds3Object ds3Object)
        {
            var hashCode1 = ds3Object.GetHashCode();
            Assert.AreNotEqual(hashCode1, 0);
            Assert.AreEqual(hashCode1, ds3Object.GetHashCode());
            Assert.AreEqual(hashCode1, new Ds3Object(ds3Object.Name, ds3Object.Size).GetHashCode());
        }
    }
}