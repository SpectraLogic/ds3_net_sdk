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

using System.IO;
using System.Text;
using Ds3.Lang;
using NUnit.Framework;
using TestDs3.Helpers;

namespace TestDs3.Lang
{
    [TestFixture]
    internal class TestStreamsUtil
    {
        [Test]
        public void TestBufferedCopyTo()
        {
            const string message = "This is a test string to make sure we are buffering correctly";
            var messageStream = new MockStream(message);
            var dstStream = new MemoryStream();

            StreamsUtil.BufferedCopyTo(messageStream, dstStream, 10);

            var testMessage = new UTF8Encoding(false).GetString(dstStream.GetBuffer(), 0, (int) dstStream.Length);

            Assert.AreEqual(message, testMessage);
        }
    }
}