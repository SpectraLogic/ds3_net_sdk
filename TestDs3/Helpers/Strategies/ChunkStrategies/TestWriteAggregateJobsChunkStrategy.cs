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

using Ds3;
using Ds3.Calls;
using Ds3.Helpers.Strategies.ChunkStrategies;
using Ds3.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Range = Ds3.Models.Range;

namespace TestDs3.Helpers.Strategies.ChunkStrategies
{
    using Stubs = JobResponseStubs;

    [TestFixture]
    public class TestWriteAggregateJobsChunkStrategy
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
                .Returns(AllocateJobChunkSpectraS3Response.Success(Stubs.Chunk1Filtered(Stubs.NodeId1, false, false)));

            client
                .SetupSequence(c => c.AllocateJobChunkSpectraS3(AllocateMock.Allocate(Stubs.ChunkId2)))
                .Returns(AllocateJobChunkSpectraS3Response.RetryAfter(TimeSpan.FromMinutes(5)))
                .Returns(AllocateJobChunkSpectraS3Response.Success(Stubs.Chunk2Filtered(Stubs.NodeId2, false, false)));

            var sleeps = new List<TimeSpan>();

            var source = new WriteAggregateJobsChunkStrategy(sleeps.Add, new List<Ds3Object> { new Ds3Object("foo", 20) }); //we don't want to really sleep in the tests
            var transfers = source.GetNextTransferItems(client.Object, jobResponse).ToArray();

            CollectionAssert.AreEqual(
                new[]
                {
                    new TransferItem(node1Client, new Ds3.Helpers.Blob(Range.ByLength(0, 10), "foo")),
                    new TransferItem(node1Client, new Ds3.Helpers.Blob(Range.ByLength(10, 10), "foo")),
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
    }
}
