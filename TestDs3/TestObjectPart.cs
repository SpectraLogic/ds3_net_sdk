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
