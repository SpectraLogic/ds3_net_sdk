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
using System.IO;
using System.Text;

using Moq;
using NUnit.Framework;

using Ds3.Helpers;

namespace TestDs3
{
    [TestFixture]
    public class TestWindowedStream
    {
        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void TestCannotWrapNonSeekableStream()
        {
            var mockStream = new Mock<Stream>(MockBehavior.Strict);
            mockStream.Setup(ws => ws.CanSeek).Returns(false);
            mockStream.Setup(ws => ws.Length).Returns(1024L);
            new WindowedStream(mockStream.Object, new Mock<ICriticalSectionExecutor>().Object, 0L, 1024L);
        }

        [Test]
        public void TestCanReadAndWriteWhenStreamCanReadAndWrite()
        {
            TestReadWriteProperties(false, false);
            TestReadWriteProperties(false, true);
            TestReadWriteProperties(true, false);
            TestReadWriteProperties(true, true);
        }

        private static void TestReadWriteProperties(bool canRead, bool canWrite)
        {
            var mockStream = new Mock<Stream>(MockBehavior.Strict);
            mockStream.Setup(ws => ws.CanSeek).Returns(true);
            mockStream.Setup(ws => ws.Length).Returns(1024L);
            mockStream.Setup(ws => ws.CanRead).Returns(canRead);
            mockStream.Setup(ws => ws.CanWrite).Returns(canWrite);

            var windowedStream = new WindowedStream(mockStream.Object, new Mock<ICriticalSectionExecutor>().Object, 0L, 1024L);
            Assert.AreEqual(canRead, windowedStream.CanRead);
            Assert.AreEqual(canWrite, windowedStream.CanWrite);
        }

        [Test]
        public void TestReadStreamSections()
        {
            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes("aabbbcccc")))
            {
                var criticalSectionExecutor = new CriticalSectionExecutor();
                var streamOfAs = new WindowedStream(memoryStream, criticalSectionExecutor, 0L, 2L);
                var streamOfBs = new WindowedStream(memoryStream, criticalSectionExecutor, 2L, 3L);
                var streamOfCs = new WindowedStream(memoryStream, criticalSectionExecutor, 5L, 4L);

                Assert.AreEqual(2L, streamOfAs.Length);
                Assert.AreEqual(3L, streamOfBs.Length);
                Assert.AreEqual(4L, streamOfCs.Length);

                using (var streamReader = new StreamReader(streamOfAs, Encoding.UTF8))
                {
                    Assert.AreEqual("aa", streamReader.ReadToEnd());
                }
                using (var streamReader = new StreamReader(streamOfBs, Encoding.UTF8))
                {
                    Assert.AreEqual("bbb", streamReader.ReadToEnd());
                }
                using (var streamReader = new StreamReader(streamOfCs, Encoding.UTF8))
                {
                    Assert.AreEqual("cccc", streamReader.ReadToEnd());
                }
            }
        }

        [Test]
        public void TestWriteStreamSections()
        {
            using (var memoryStream = new MemoryStream())
            {
                var criticalSectionExecutor = new CriticalSectionExecutor();
                var streamOfAs = new WindowedStream(memoryStream, criticalSectionExecutor, 0L, 2L);
                var streamOfBs = new WindowedStream(memoryStream, criticalSectionExecutor, 2L, 3L);
                var streamOfCs = new WindowedStream(memoryStream, criticalSectionExecutor, 5L, 4L);

                Assert.AreEqual(2L, streamOfAs.Length);
                Assert.AreEqual(3L, streamOfBs.Length);
                Assert.AreEqual(4L, streamOfCs.Length);

                streamOfCs.Write(Encoding.UTF8.GetBytes("cccc"), 0, 4);
                streamOfBs.Write(Encoding.UTF8.GetBytes("bbb"), 0, 3);
                streamOfAs.Write(Encoding.UTF8.GetBytes("aa"), 0, 2);

                memoryStream.Position = 0L;
                using (var streamReader = new StreamReader(memoryStream, Encoding.UTF8))
                {
                    Assert.AreEqual("aabbbcccc", streamReader.ReadToEnd());
                }
            }
        }

        [Test]
        public void TestReadPositionTracking()
        {
            using (var memoryStream = new MemoryStream(Encoding.UTF8.GetBytes("aabbbcccc")))
            {
                var criticalSectionExecutor = new CriticalSectionExecutor();
                var windowedStream = new WindowedStream(memoryStream, criticalSectionExecutor, 2L, 7L);

                var buffer = new byte[10];

                Assert.AreEqual(3, windowedStream.Read(buffer, 0, 3));
                Assert.AreEqual("bbb", Encoding.UTF8.GetString(buffer, 0, 3));

                Assert.AreEqual(4, windowedStream.Read(buffer, 0, 10));
                Assert.AreEqual("cccc", Encoding.UTF8.GetString(buffer, 0, 4));
            }
        }

        [Test]
        public void TestWritePositionTracking()
        {
            using (var memoryStream = new MemoryStream())
            {
                var criticalSectionExecutor = new CriticalSectionExecutor();
                var windowedStream = new WindowedStream(memoryStream, criticalSectionExecutor, 2L, 7L);

                var buffer = new byte[10];
                Encoding.UTF8.GetBytes("bbb", 0, 3, buffer, 0);
                windowedStream.Write(buffer, 0, 3);
                Encoding.UTF8.GetBytes("cccc", 0, 4, buffer, 0);
                windowedStream.Write(buffer, 0, 4);

                memoryStream.Position = 0L;
                using (var streamReader = new StreamReader(memoryStream))
                {
                    Assert.AreEqual("\0\0bbbcccc", streamReader.ReadToEnd());
                }
            }
        }
    }
}
