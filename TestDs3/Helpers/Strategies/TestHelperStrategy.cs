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

using Ds3.Helpers.Strategies;
using Ds3.Helpers.Strategies.ChunkStrategies;
using Ds3.Helpers.Strategies.StreamFactory;
using Ds3.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace TestDs3.Helpers.Strategies
{
    [TestFixture]
    public class TestHelperStrategy
    {
        [Test]
        public void TestReadRandomAccessHelperStrategyUsingDs3PartialObjectAsTItem()
        {
            var helperStrategy = new ReadRandomAccessHelperStrategy<Ds3PartialObject>();
            var chunkStrategy = helperStrategy.GetChunkStrategy();
            var streamFactory = helperStrategy.GetStreamFactory();

            Assert.AreEqual(typeof(ReadRandomAccessChunkStrategy), chunkStrategy.GetType());
            Assert.AreEqual(typeof(ReadRandomAccessStreamFactory<Ds3PartialObject>), streamFactory.GetType());
        }

        [Test]
        public void TestReadRandomAccessHelperStrategyUsingStringAsTItem()
        {
            var helperStrategy = new ReadRandomAccessHelperStrategy<string>();
            var chunkStrategy = helperStrategy.GetChunkStrategy();
            var streamFactory = helperStrategy.GetStreamFactory();

            Assert.AreEqual(typeof(ReadRandomAccessChunkStrategy), chunkStrategy.GetType());
            Assert.AreEqual(typeof(ReadRandomAccessStreamFactory<string>), streamFactory.GetType());
        }

        [Test]
        public void TestReadStreamHelperStrategy()
        {
            var helperStrategy = new ReadStreamHelperStrategy();
            var chunkStrategy = helperStrategy.GetChunkStrategy();
            var streamFactory = helperStrategy.GetStreamFactory();

            Assert.AreEqual(typeof(ReadStreamChunkStrategy), chunkStrategy.GetType());
            Assert.AreEqual(typeof(ReadStreamStreamFactory), streamFactory.GetType());
        }

        [Test]
        public void TestWriteNoAllocateHelperStrategy()
        {
            var helperStrategy = new WriteNoAllocateHelperStrategy();
            var chunkStrategy = helperStrategy.GetChunkStrategy();
            var streamFactory = helperStrategy.GetStreamFactory();

            Assert.AreEqual(typeof(WriteNoAllocateChunkStrategy), chunkStrategy.GetType());
            Assert.AreEqual(typeof(WriteRandomAccessStreamFactory), streamFactory.GetType());
        }

        [Test]
        public void TestWriteRandomAccessHelperStrategy()
        {
            var helperStrategy = new WriteRandomAccessHelperStrategy();
            var chunkStrategy = helperStrategy.GetChunkStrategy();
            var streamFactory = helperStrategy.GetStreamFactory();

            Assert.AreEqual(typeof(WriteRandomAccessChunkStrategy), chunkStrategy.GetType());
            Assert.AreEqual(typeof(WriteRandomAccessStreamFactory), streamFactory.GetType());
        }

        [Test]
        public void TestWriteStreamHelperStrategy()
        {
            var helperStrategy = new WriteStreamHelperStrategy();
            var chunkStrategy = helperStrategy.GetChunkStrategy();
            var streamFactory = helperStrategy.GetStreamFactory();

            Assert.AreEqual(typeof(WriteStreamChunkStrategy), chunkStrategy.GetType());
            Assert.AreEqual(typeof(WriteStreamStreamFactory), streamFactory.GetType());
        }

        [Test]
        public void TestWriteAggregateJobsHelperStrategy()
        {
            var helperStrategy = new WriteAggregateJobsHelperStrategy(new List<Ds3Object> { new Ds3Object("foo", 10) });
            var chunkStrategy = helperStrategy.GetChunkStrategy();
            var streamFactory = helperStrategy.GetStreamFactory();

            Assert.AreEqual(typeof(WriteAggregateJobsChunkStrategy), chunkStrategy.GetType());
            Assert.AreEqual(typeof(WriteRandomAccessStreamFactory), streamFactory.GetType());
        }
    }
}