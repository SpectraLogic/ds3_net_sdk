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

using System.Text;
using NUnit.Framework;
using Ds3.Runtime;
using System.IO;

namespace TestDs3.Helpers.Streams
{

    [TestFixture]
    class TestWebResponseStream
    {
        const int CopyBufferSize = 1;
        byte[] byteStringSize26 = Encoding.UTF8.GetBytes("abcdefghijklmnopqrstuvwxyz");

        [Test]
        public void TestCopyTo()
        {
            var stream = new MemoryStream(byteStringSize26);

            Assert.AreEqual(
                GetRequestedStream(stream, stream.Length),
                stream);

            Assert.Catch(typeof(Ds3ContentLenghtNotMatch), () => GetRequestedStream(stream, stream.Length + 10));
            Assert.Catch(typeof(Ds3ContentLenghtNotMatch), () => GetRequestedStream(stream, stream.Length - 10));

        }

        private Stream GetRequestedStream(Stream source, long lenght)
        {
            var webReponseStream = new WebResponseStream(source, lenght);
            var requestStream = new MemoryStream();
            if (webReponseStream.Position != 0)
            {
                webReponseStream.Seek(0, SeekOrigin.Begin);
            }
            webReponseStream.CopyTo(requestStream, CopyBufferSize);

            return requestStream;
        }
    }
}
