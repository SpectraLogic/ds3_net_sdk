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

using Ds3;
using Ds3.Calls;
using Ds3.Helpers;
using Ds3.Helpers.TransferItemSources;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Range = Ds3.Models.Range;

namespace TestDs3.Helpers.TransferItemSources
{
    using Stubs = JobResponseStubs;

    [TestFixture]
    public class TestWriteTransferItemSource
    {
        [Test]
        public void EnumerateItemsAllocatesChunks()
        {
            var jobResponse = Stubs.BuildJobResponse(
                Stubs.Chunk1(null, false, false),
                Stubs.Chunk2(Stubs.NodeId2, true, false),
                Stubs.Chunk3(null, false, false)
            );

            var node1Client = new Mock<IDs3Client>(MockBehavior.Strict).Object;
            var node2Client = new Mock<IDs3Client>(MockBehavior.Strict).Object;

            var clientFactory = new Mock<IDs3ClientFactory>(MockBehavior.Strict);
            clientFactory.Setup(cf => cf.GetClientForNodeId(Stubs.NodeId1)).Returns(node1Client);
            clientFactory.Setup(cf => cf.GetClientForNodeId(Stubs.NodeId2)).Returns(node2Client);

            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client.Setup(c => c.BuildFactory(Stubs.Nodes)).Returns(clientFactory.Object);
            client
                .SetupSequence(c => c.AllocateJobChunk(Allocate(Stubs.ChunkId1)))
                .Returns(AllocateJobChunkResponse.RetryAfter(TimeSpan.FromMinutes(5)))
                .Returns(AllocateJobChunkResponse.Success(Stubs.Chunk1(Stubs.NodeId1, false, false)))
                .Returns(AllocateJobChunkResponse.Success(Stubs.Chunk1(Stubs.NodeId1, true, false)))
                .Returns(AllocateJobChunkResponse.ChunkGone);
            client
                .SetupSequence(c => c.AllocateJobChunk(Allocate(Stubs.ChunkId2)))
                .Returns(AllocateJobChunkResponse.Success(Stubs.Chunk2(Stubs.NodeId2, true, false)))
                .Returns(AllocateJobChunkResponse.Success(Stubs.Chunk2(Stubs.NodeId2, true, true)));
            client
                .SetupSequence(c => c.AllocateJobChunk(Allocate(Stubs.ChunkId3)))
                .Returns(AllocateJobChunkResponse.RetryAfter(TimeSpan.FromMinutes(3)))
                .Returns(AllocateJobChunkResponse.RetryAfter(TimeSpan.FromMinutes(1)))
                .Returns(AllocateJobChunkResponse.Success(Stubs.Chunk3(Stubs.NodeId2, false, false)));

            var sleeps = new List<TimeSpan>();
            var source = new WriteTransferItemSource(sleeps.Add, client.Object, jobResponse);
            var transfers1 = source.EnumerateAvailableTransfers().Take(1).ToArray();
            var transfers2 = source.EnumerateAvailableTransfers().Take(2).ToArray();
            var transfers3 = source.EnumerateAvailableTransfers().ToArray();

            CollectionAssert.AreEqual(
                new[]
                {
                    new TransferItem(node1Client, new Blob(Range.ByLength(0, 15), "bar")),
                },
                transfers1,
                new TransferItemSourceHelpers.TransferItemComparer()
            );
            CollectionAssert.AreEqual(
                new[]
                {
                    new TransferItem(node1Client, new Blob(Range.ByLength(10, 10), "foo")),
                    new TransferItem(node2Client, new Blob(Range.ByLength(15, 20), "bar")),
                },
                transfers2,
                new TransferItemSourceHelpers.TransferItemComparer()
            );
            CollectionAssert.AreEqual(
                new[]
                {
                    new TransferItem(node2Client, new Blob(Range.ByLength(0, 10), "hello")),
                    new TransferItem(node2Client, new Blob(Range.ByLength(35, 11), "bar"))
                },
                transfers3,
                new TransferItemSourceHelpers.TransferItemComparer()
            );

            CollectionAssert.AreEqual(
                new[]
                {
                    TimeSpan.FromMinutes(5),
                    TimeSpan.FromMinutes(3),
                    TimeSpan.FromMinutes(1),
                },
                sleeps
            );

            client.VerifyAll();
            clientFactory.VerifyAll();
        }

        private static AllocateJobChunkRequest Allocate(Guid chunkId)
        {
            return Match.Create(
                r => r.ChunkId == chunkId,
                () => new AllocateJobChunkRequest(chunkId)
            );
        }
    }
}
