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

using System.Linq;
using Ds3;
using Ds3.Helpers;
using Ds3.Helpers.Strategies.ChunkStrategies;
using Moq;
using NUnit.Framework;
using Range = Ds3.Models.Range;

namespace TestDs3.Helpers.Strategies.ChunkStrategies
{
    using Stubs = JobResponseStubs;

    [TestFixture]
    public class TestWriteNoAllocateChunkStrategy
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

            var source = new WriteNoAllocateChunkStrategy();
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

            var source = new WriteNoAllocateChunkStrategy();
            var transfers1 = source.GetNextTransferItems(client.Object, jobResponse).ToArray();

            CollectionAssert.AreEqual(
                new[]
                {
                    new TransferItem(node1Client, new Blob(Range.ByLength(0, 15), "bar")),
                    new TransferItem(node1Client, new Blob(Range.ByLength(15, 20), "bar"))
                },
                transfers1,
                new TransferItemSourceHelpers.TransferItemComparer()
            );

            client.VerifyAll();
            clientFactory.VerifyAll();
        }
    }
}