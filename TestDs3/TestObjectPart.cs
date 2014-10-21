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
using NUnit.Framework;
using System.Collections.Generic;

namespace TestDs3
{
    [TestFixture]
    public class TestObjectPart
    {
        [Test]
        public void CompareEqualReturnsZero()
        {
            Assert.AreEqual(0, new ObjectPart(5, 10).CompareTo(new ObjectPart(5, 10)));
        }

        [Test]
        public void CompareWorks()
        {
            var objects = new List<ObjectPart>
            {
                new ObjectPart(3L, 2L),
                new ObjectPart(0L, 2L),
                new ObjectPart(2L, 1L),
                new ObjectPart(0L, 1L)
            };
            objects.Sort();
            var expected = new[]
            {
                new { Offset = 0L, Length = 1L },
                new { Offset = 0L, Length = 2L },
                new { Offset = 2L, Length = 1L },
                new { Offset = 3L, Length = 2L }
            };
            for (int i = 0; i < expected.Length; i++)
            {
                var current = objects[i];
                var expectedObj = expected[i];
                Assert.AreEqual(expectedObj.Offset, current.Offset);
                Assert.AreEqual(expectedObj.Length, current.Length);
            }
        }

        [Test]
        public void EndWorks()
        {
            Assert.AreEqual(122L, new ObjectPart(0L, 123L).End);
            Assert.AreEqual(443L, new ObjectPart(321L, 123L).End);
        }
    }
}
