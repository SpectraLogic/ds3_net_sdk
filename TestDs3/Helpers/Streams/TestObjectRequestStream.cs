/*
 * ******************************************************************************
 *   Copyright 2014-2018 Spectra Logic Corporation. All Rights Reserved.
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

using Ds3.Helpers.Streams;
using NUnit.Framework;
using System.IO;
using System.Text;

namespace TestDs3.Helpers.Streams
{
    [TestFixture]
    internal class TestObjectRequestStream
    {
        [Test]
        public void TestTotalByteRead()
        {
            var inlineStream = new MemoryStream(Encoding.UTF8.GetBytes("abcdefghij"));
            var stream = new ObjectRequestStream(inlineStream, 0, 10);
            var requestStream = new byte[10];

            Assert.AreEqual(0, stream.Position);
            Assert.AreEqual(0, stream.GetTotalBytesRead());

            stream.Read(requestStream, 0,  10);

            Assert.AreEqual(10, stream.Position);
            Assert.AreEqual(10, stream.GetTotalBytesRead());

            stream.Position = 0;

            Assert.AreEqual(0, stream.Position);
            Assert.AreEqual(0, stream.GetTotalBytesRead());
        }
    }
}
