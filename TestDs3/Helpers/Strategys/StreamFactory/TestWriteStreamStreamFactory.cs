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
        public void TestCreateStreamSaveTheSameStreamForStream()
        {
            var factory = new WriteStreamStreamFactory();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes("I am a stream"));
            Func<string, Stream> func = name => stream;

            factory.CreateStream(func, null, Stubs.Blob1, Stubs.Blob1Length);
            factory.CreateStream(func, null, Stubs.Blob2, Stubs.Blob2Length);

            Assert.AreEqual(1, factory.GetStreamStore().Count);
            Assert.AreEqual(stream, factory.GetStreamStore()["bar"]);
        }

        [Test]
        public void TestCreateStreamSaveDiffrentStreamForDiffrentStreams()
        {
            var factory = new WriteStreamStreamFactory();
            Func<string, Stream> func = name => new MemoryStream(Encoding.UTF8.GetBytes("I am a stream"));

            factory.CreateStream(func, null, Stubs.Blob1, Stubs.Blob1Length);
            factory.CreateStream(func, null, Stubs.Blob3, Stubs.Blob3Length);

            Assert.AreEqual(2, factory.GetStreamStore().Count);
        }

        [Test]
        public void TestCreateStreamSaveSameStreamBlob()
        {
            var factory = new WriteStreamStreamFactory();
            var stream = new MemoryStream(Encoding.UTF8.GetBytes("I am a stream"));
            Func <string, Stream> func = name => stream;

            factory.CreateStream(func, null, Stubs.Blob1, Stubs.Blob1Length);
            factory.CreateStream(func, null, Stubs.Blob1, Stubs.Blob1Length);

            Assert.AreEqual(1, factory.GetStreamStore().Count);
            Assert.AreEqual(stream, factory.GetStreamStore()["bar"]);
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
        public void TestCloseStreamException()
        {
            var factory = new WriteStreamStreamFactory();
            Func<string, Stream> func = name => new MemoryStream(Encoding.UTF8.GetBytes("I am a stream"));

            factory.CreateStream(func, null, Stubs.Blob1, Stubs.Blob1Length);

            factory.CloseStream(Stubs.Blob1.Context);
            Assert.Throws<StreamNotFoundException>(() => factory.CloseStream(Stubs.Blob3.Context));
        }
    }
}