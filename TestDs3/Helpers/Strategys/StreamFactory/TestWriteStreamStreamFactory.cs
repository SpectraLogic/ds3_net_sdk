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

using Ds3.Helpers.Strategys.StreamFactory;
using NUnit.Framework;
using System;
using System.IO;
using System.Text;

namespace TestDs3.Helpers.Strategys.StreamFactory
{
    using Stubs = BlobsStub;

    [TestFixture]
    public class TestWriteStreamStreamFactory
    {
        [Test]
        public void TestCreateStreamGetTheSameStreamForStream()
        {
            var factory = new WriteStreamStreamFactory();
            Func<string, Stream> func = name => new MemoryStream(Encoding.UTF8.GetBytes("I am a stream"));

            var stream1 = factory.CreateStream(func, null, Stubs.Blob1, Stubs.Blob1Length);
            var stream2 = factory.CreateStream(func, null, Stubs.Blob2, Stubs.Blob2Length);

            Assert.AreEqual(stream1, stream2);
        }

        [Test]
        public void TestCreateStreamGetDiffrentStreamForDiffrentStreams()
        {
            var factory = new WriteStreamStreamFactory();
            Func<string, Stream> func = name => new MemoryStream(Encoding.UTF8.GetBytes("I am a stream"));

            var stream1 = factory.CreateStream(func, null, Stubs.Blob1, Stubs.Blob1Length);
            var stream2 = factory.CreateStream(func, null, Stubs.Blob3, Stubs.Blob3Length);

            Assert.AreNotEqual(stream1, stream2);
        }

        [Test]
        public void TestCreateStreamGetSameStreamBlob()
        {
            var factory = new WriteStreamStreamFactory();
            Func<string, Stream> func = name => new MemoryStream(Encoding.UTF8.GetBytes("I am a stream"));

            var stream1 = factory.CreateStream(func, null, Stubs.Blob1, Stubs.Blob1Length);
            var stream2 = factory.CreateStream(func, null, Stubs.Blob1, Stubs.Blob1Length);

            Assert.AreEqual(stream1, stream2);
        }

        [Test]
        public void TestCloseStream()
        {
            var factory = new WriteStreamStreamFactory();
            Func<string, Stream> func = name => new MemoryStream(Encoding.UTF8.GetBytes("I am a stream"));

            factory.CreateStream(func, null, Stubs.Blob1, Stubs.Blob1Length);

            factory.CloseStream(Stubs.Blob1.Context);
        }

        [Test]
        [ExpectedException(typeof(StreamNotFoundException))]
        public void TestCloseStreamException()
        {
            var factory = new WriteStreamStreamFactory();
            Func<string, Stream> func = name => new MemoryStream(Encoding.UTF8.GetBytes("I am a stream"));

            factory.CreateStream(func, null, Stubs.Blob1, Stubs.Blob1Length);

            factory.CloseStream(Stubs.Blob1.Context);
            factory.CloseStream(Stubs.Blob3.Context); //should throw StreamNotFoundException
        }
    }
}