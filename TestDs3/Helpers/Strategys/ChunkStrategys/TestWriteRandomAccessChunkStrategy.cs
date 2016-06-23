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
using System.Collections.Generic;
using System.Linq;
using Ds3;
using Ds3.Calls;
using Ds3.Helpers;
using Ds3.Helpers.Strategys.ChunkStrategys;
using Moq;
using NUnit.Framework;
using Range = Ds3.Models.Range;

namespace TestDs3.Helpers.Strategys.ChunkStrategys
{
    using Stubs = JobResponseStubs;

    [TestFixture]
    public class TestWriteRandomAccessChunkStrategy
    {
        [Test]
        public void TestGetNextTransferItems()
        {
            var jobResponse = Stubs.BuildJobResponse(
                Stubs.Chunk1(Stubs.NodeId1, false, false),
                Stubs.Chunk2(Stubs.NodeId2, false, false)
            );

            var node1Client = new Mock<IDs3Client>(MockBehavior.Strict).Object;

            var clientFactory = new Mock<IDs3ClientFactory>(MockBehavior.Strict);
            clientFactory.Setup(cf => cf.GetClientForNodeId(Stubs.NodeId1)).Returns(node1Client);
            clientFactory.Setup(cf => cf.GetClientForNodeId(Stubs.NodeId2)).Returns(node1Client);

            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client.Setup(c => c.BuildFactory(Stubs.Nodes)).Returns(clientFactory.Object);

            client
                .SetupSequence(c => c.AllocateJobChunkSpectraS3(AllocateMock.Allocate(Stubs.ChunkId1)))
                .Returns(AllocateJobChunkSpectraS3Response.RetryAfter(TimeSpan.FromMinutes(5)))
                .Returns(AllocateJobChunkSpectraS3Response.Success(Stubs.Chunk1(Stubs.NodeId1, false, false)));

            client
                .SetupSequence(c => c.AllocateJobChunkSpectraS3(AllocateMock.Allocate(Stubs.ChunkId2)))
                .Returns(AllocateJobChunkSpectraS3Response.RetryAfter(TimeSpan.FromMinutes(5)))
                .Returns(AllocateJobChunkSpectraS3Response.Success(Stubs.Chunk2(Stubs.NodeId2, false, false)));

            var sleeps = new List<TimeSpan>();

            var source = new WriteRandomAccessChunkStrategy(sleeps.Add, false); //we don't want to really sleep in the tests
            var transfers = source.GetNextTransferItems(client.Object, jobResponse).ToArray();

            CollectionAssert.AreEqual(
                new[]
                {
                    new TransferItem(node1Client, new Blob(Range.ByLength(0, 15), "bar")),
                    new TransferItem(node1Client, new Blob(Range.ByLength(10, 10), "foo")),
                    new TransferItem(node1Client, new Blob(Range.ByLength(0, 10), "foo")),
                    new TransferItem(node1Client, new Blob(Range.ByLength(15, 20), "bar"))

                },
                transfers,
                new TransferItemSourceHelpers.TransferItemComparer()
            );

            CollectionAssert.AreEqual(
                new[] { TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5) },
                sleeps
            );

            client.VerifyAll();
            clientFactory.VerifyAll();
        }

        [Test]
        public void TestGetNextTransferItemsWithCashedBlob()
        {
            var jobResponse = Stubs.BuildJobResponse(
                Stubs.Chunk1(Stubs.NodeId1, false, true),
                Stubs.Chunk2(Stubs.NodeId2, true, false)
            );

            var node1Client = new Mock<IDs3Client>(MockBehavior.Strict).Object;

            var clientFactory = new Mock<IDs3ClientFactory>(MockBehavior.Strict);
            clientFactory.Setup(cf => cf.GetClientForNodeId(Stubs.NodeId1)).Returns(node1Client);
            clientFactory.Setup(cf => cf.GetClientForNodeId(Stubs.NodeId2)).Returns(node1Client);

            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client.Setup(c => c.BuildFactory(Stubs.Nodes)).Returns(clientFactory.Object);

            client
                .SetupSequence(c => c.AllocateJobChunkSpectraS3(AllocateMock.Allocate(Stubs.ChunkId1)))
                .Returns(AllocateJobChunkSpectraS3Response.RetryAfter(TimeSpan.FromMinutes(5)))
                .Returns(AllocateJobChunkSpectraS3Response.Success(Stubs.Chunk1(Stubs.NodeId1, false, true)));

            client
                .SetupSequence(c => c.AllocateJobChunkSpectraS3(AllocateMock.Allocate(Stubs.ChunkId2)))
                .Returns(AllocateJobChunkSpectraS3Response.RetryAfter(TimeSpan.FromMinutes(5)))
                .Returns(AllocateJobChunkSpectraS3Response.Success(Stubs.Chunk2(Stubs.NodeId2, true, false)));

            var sleeps = new List<TimeSpan>();

            var source = new WriteRandomAccessChunkStrategy(sleeps.Add, false); //we don't want to really sleep in the tests
            var transfers = source.GetNextTransferItems(client.Object, jobResponse).ToArray();

            CollectionAssert.AreEqual(
                new[]
                {
                    new TransferItem(node1Client, new Blob(Range.ByLength(0, 15), "bar")),
                    new TransferItem(node1Client, new Blob(Range.ByLength(15, 20), "bar"))

                },
                transfers,
                new TransferItemSourceHelpers.TransferItemComparer()
            );

            CollectionAssert.AreEqual(
                new[] { TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5) },
                sleeps
            );

            client.VerifyAll();
            clientFactory.VerifyAll();
        }

        [Test]
        public void TestGetNextTransferItemsWithAggregationNonCachedObjects()
        {
            var jobResponse = Stubs.BuildJobResponse(
                Stubs.Chunk1(Stubs.NodeId1, false, false),
                Stubs.Chunk2(Stubs.NodeId2, false, false)
                );
 
            var nodeClient = new Mock<IDs3Client>(MockBehavior.Strict).Object;
 
            var clientFactory = new Mock<IDs3ClientFactory>(MockBehavior.Strict);
            clientFactory.Setup(cf => cf.GetClientForNodeId(Stubs.NodeId1)).Returns(nodeClient);
            clientFactory.Setup(cf => cf.GetClientForNodeId(Stubs.NodeId2)).Returns(nodeClient);
 
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client.Setup(c => c.BuildFactory(Stubs.Nodes)).Returns(clientFactory.Object);
            client
                .SetupSequence(c => c.AllocateJobChunkSpectraS3(AllocateMock.Allocate(Stubs.ChunkId1)))
                .Returns(AllocateJobChunkSpectraS3Response.Success(Stubs.Chunk1(Stubs.NodeId1, false, false)));
            client
                 .SetupSequence(c => c.AllocateJobChunkSpectraS3(AllocateMock.Allocate(Stubs.ChunkId2)))
                 .Returns(AllocateJobChunkSpectraS3Response.Success(Stubs.Chunk2(Stubs.NodeId2, false, false)));
 
            var source = new WriteRandomAccessChunkStrategy(time => { }, true);
            var transfers = source.GetNextTransferItems(client.Object, jobResponse).ToArray();

            CollectionAssert.AreEqual(
                new[]
                {
                    new TransferItem(nodeClient, new Blob(Range.ByLength(0, 15), "bar")),
                    new TransferItem(nodeClient, new Blob(Range.ByLength(10, 10), "foo")),
                    new TransferItem(nodeClient, new Blob(Range.ByLength(0, 10), "foo")),
                    new TransferItem(nodeClient, new Blob(Range.ByLength(15, 20), "bar")),
                },
                transfers,
                new TransferItemSourceHelpers.TransferItemComparer()
            );
            client.VerifyAll();
            clientFactory.VerifyAll();
        }
 
        [Test]
        public void TestGetNextTransferItemsWithAggregationWithCachedObjects()
        {
            var jobResponse = Stubs.BuildJobResponse(
            Stubs.Chunk1(Stubs.NodeId1, true, true),
            Stubs.Chunk2(Stubs.NodeId2, false, false)
            );
 
            var nodeClient = new Mock<IDs3Client>(MockBehavior.Strict).Object;
 
            var clientFactory = new Mock<IDs3ClientFactory>(MockBehavior.Strict);
            clientFactory.Setup(cf => cf.GetClientForNodeId(Stubs.NodeId2)).Returns(nodeClient);
 
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client.Setup(c => c.BuildFactory(Stubs.Nodes)).Returns(clientFactory.Object);
            client
                .SetupSequence(c => c.AllocateJobChunkSpectraS3(AllocateMock.Allocate(Stubs.ChunkId2)))
                .Returns(AllocateJobChunkSpectraS3Response.Success(Stubs.Chunk2(Stubs.NodeId2, false, false)));
 
            var source = new WriteRandomAccessChunkStrategy(time => { }, true);
            var transfers = source.GetNextTransferItems(client.Object, jobResponse).ToArray();

            CollectionAssert.AreEqual(
                new[]
                {
                    new TransferItem(nodeClient, new Blob(Range.ByLength(0, 10), "foo")),
                    new TransferItem(nodeClient, new Blob(Range.ByLength(15, 20), "bar")),
                },
                transfers,
                new TransferItemSourceHelpers.TransferItemComparer());
 
 
            client.VerifyAll();
            clientFactory.VerifyAll();
        }
    }
}
