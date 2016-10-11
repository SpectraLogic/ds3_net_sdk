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

using System;
using System.IO;
using System.Text;
using Ds3.Runtime;
using NUnit.Framework;

namespace TestDs3.Runtime
{
    [TestFixture]
    internal class TestWebStream
    {
        private const int CopyBufferSize = 1;
        private readonly byte[] _byteStringSize26 = Encoding.UTF8.GetBytes("abcdefghijklmnopqrstuvwxyz");

        private static Stream GetRequestedStream(Stream source, long length)
        {
            var webResponseStream = new Ds3WebStream(source, length);
            var requestStream = new MemoryStream();
            if (webResponseStream.Position != 0)
            {
                webResponseStream.Seek(0, SeekOrigin.Begin);
            }
            webResponseStream.CopyTo(requestStream, CopyBufferSize);

            return requestStream;
        }

        [Test]
        public void TestCopyTo()
        {
            var stream = new MemoryStream(_byteStringSize26);

            Assert.AreEqual(
                GetRequestedStream(stream, stream.Length),
                stream);

            Assert.Catch(typeof(Ds3ContentLengthNotMatch), () => GetRequestedStream(stream, stream.Length + 10));
            Assert.Catch(typeof(Ds3ContentLengthNotMatch), () => GetRequestedStream(stream, stream.Length - 10));
        }

        [Test]
        public void TestForOverflow()
        {
            var webResponseStream = new Ds3WebStream(new FakeStream(), int.MaxValue*2L);
            webResponseStream.Read(null, 0, 0);
            webResponseStream.Read(null, 0, 0);
            Assert.AreEqual(webResponseStream.GetBytesRead(), int.MaxValue*2L);
        }
    }

    internal class FakeStream : Stream
    {
        public override bool CanRead => false;

        public override bool CanSeek => false;

        public override bool CanWrite => false;

        public override long Length => 0;

        public override long Position { get; set; }

        public override void Flush()
        {
            throw new NotImplementedException();
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            return int.MaxValue;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotImplementedException();
        }
    }
}