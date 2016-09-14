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

using System.Text;
using NUnit.Framework;
using Ds3.Helpers.Streams;
using System.IO;

namespace TestDs3.Helpers.Streams
{

    [TestFixture]
    class TestPutObjectRequestStream
    {
        const int CopyBufferSize = 1;
        byte[] byteStringSize26 = Encoding.UTF8.GetBytes("abcdefghijklmnopqrstuvwxyz");
        byte[] first10Bytes = Encoding.UTF8.GetBytes("abcdefghij");
        byte[] second10Bytes = Encoding.UTF8.GetBytes("klmnopqrst");
        byte[] last6Bytes = Encoding.UTF8.GetBytes("uvwxyz");

        [Test]
        public void TestCopyToInOrder()
        {
            var stream = new MemoryStream(byteStringSize26);

            Assert.AreEqual(
                GetRequestedStream(stream, 0, 10),
                new MemoryStream(first10Bytes));

            Assert.AreEqual(
                GetRequestedStream(stream, 10, 10),
                new MemoryStream(second10Bytes));

            Assert.AreEqual(
                GetRequestedStream(stream, 20, 6),
                new MemoryStream(last6Bytes));
        }

        [Test]
        public void TestCopyToNotInOrder()
        {
            var stream = new MemoryStream(byteStringSize26);

            Assert.AreEqual(
                GetRequestedStream(stream, 10, 10),
                new MemoryStream(second10Bytes));

            Assert.AreEqual(
                GetRequestedStream(stream, 20, 6),
                new MemoryStream(last6Bytes));

            Assert.AreEqual(
                GetRequestedStream(stream, 0, 10),
                new MemoryStream(first10Bytes));
        }

        private Stream GetRequestedStream(Stream source, long offset, long lenght)
        {
            var putObjectRequestStream = new PutObjectRequestStream(source, offset, lenght);
            var requestStream = new MemoryStream();
            if (putObjectRequestStream.Position != 0)
            {
                putObjectRequestStream.Seek(0, SeekOrigin.Begin);
            }
            putObjectRequestStream.CopyTo(requestStream, CopyBufferSize);

            return requestStream;
        }
    }
}
