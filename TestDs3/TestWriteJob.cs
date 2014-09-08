using Ds3;
using Ds3.Calls;
using Ds3.Helpers;
using Ds3.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace TestDs3
{
    [TestFixture]
    public class TestWriteJob
    {
        private static readonly Guid _firstNodeId = Guid.Parse("39ca7e02-82e8-4c8a-b74f-c6dab8f399ad");
        private static readonly Guid _secondNodeId = Guid.Parse("a84b3fc6-4b97-400f-a5e3-fed47b93551c");

        private static JobObjectList BuildFirstChunk(Guid? nodeId)
        {
            return new JobObjectList(
                Guid.Parse("1f45812d-0b67-492b-b2b5-9fc2f6a0cf3f"),
                0L,
                nodeId,
                new[]
                {
                    new JobObject("foo", 10L, 0L, false),
                    new JobObject("bar", 10L, 0L, false)
                }
            );
        }

        private static JobObjectList BuildSecondChunk(Guid? nodeId)
        {
            return new JobObjectList(
                Guid.Parse("a01e8077-93ad-496a-8464-9963fea3e423"),
                1L,
                nodeId,
                new[]
                {
                    new JobObject("foo", 11L, 10L, false),
                    new JobObject("bar", 11L, 10L, false)
                }
            );
        }

        private static readonly IEnumerable<Node> _nodeList = new[]
        {
            new Node(_firstNodeId, "black-pearl-1", 80, 443),
            new Node(_secondNodeId, "black-pearl-2", 80, 443)
        };

        private static readonly JobResponse _bulkResponse = new JobResponse(
            "bucket",
            Guid.Parse("879e42ba-77fb-41fc-84c0-511acc90912b"),
            "HIGH",
            "GET",
            DateTime.Parse("9/8/2014 9:25:56 PM"),
            _nodeList,
            new[] { BuildFirstChunk(null), BuildSecondChunk(null) }
        );

        [Test]
        public void TransferCallsAllNecessaryClientMethods()
        {
            Action verify = () => { };
            var client = BuildRootClient(ref verify);

            var writeJob = new WriteJob(client, _bulkResponse);

            var transfers = new ConcurrentQueue<long>();
            var objects = new ConcurrentQueue<string>();
            writeJob.DataTransferred += transfers.Enqueue;
            writeJob.ObjectCompleted += objects.Enqueue;

            writeJob.Transfer(BuildMemoryStream);

            CollectionAssert.AreEqual(new[] { 10L, 10L, 11L, 11L }, transfers);
            CollectionAssert.AreEquivalent(new[] { "foo", "bar" }, objects);

            verify();
        }

        [Test]
        public void WriteJobCanCancelBetweenChunks()
        {
            Action verify = () => { };
            var client = BuildRootClient(ref verify);

            var cancellationTokenSource = new CancellationTokenSource();
            var cancellations = 0;
            var completions = 0;

            var writeJob = new WriteJob(client, _bulkResponse);
            writeJob.WithCancellationToken(cancellationTokenSource.Token);
            writeJob.DataTransferred += size =>
            {
                Interlocked.Increment(ref cancellations);
                cancellationTokenSource.Cancel();
            };
            writeJob.ObjectCompleted += name => Interlocked.Increment(ref completions);

            try
            {
                writeJob.Transfer(BuildMemoryStream);
                Assert.Fail();
            }
            catch (OperationCanceledException)
            {
            }

            Assert.AreEqual(2, cancellations);
            Assert.AreEqual(0, completions);
        }

        [Test]
        public void WriteJobCanCancelMidChunk()
        {
            Assert.Inconclusive();
            //TODO: we need to alter the mocks so we can know that one object
            // will take longer than another and thus receive a cancellation token.
        }

        public static IDs3Client BuildRootClient(ref Action verify)
        {
            var mockClient = new Mock<IDs3Client>(MockBehavior.Strict);
            mockClient
                .Setup(client => client.BuildFactory(_nodeList))
                .Returns(BuildClientFactory(ref verify));
            mockClient
                .Setup(client => client.AllocateJobChunk(It.Is<AllocateJobChunkRequest>(r =>
                    r.ChunkId == Guid.Parse("1f45812d-0b67-492b-b2b5-9fc2f6a0cf3f"))))
                .Returns(AllocateJobChunkResponse.Success(BuildFirstChunk(Guid.Parse("39ca7e02-82e8-4c8a-b74f-c6dab8f399ad"))));
            mockClient
                .Setup(client => client.AllocateJobChunk(It.Is<AllocateJobChunkRequest>(r =>
                    r.ChunkId == Guid.Parse("a01e8077-93ad-496a-8464-9963fea3e423"))))
                .Returns(AllocateJobChunkResponse.Success(BuildSecondChunk(Guid.Parse("a84b3fc6-4b97-400f-a5e3-fed47b93551c"))));

            verify += mockClient.VerifyAll;

            return mockClient.Object;
        }

        private static IDs3ClientFactory BuildClientFactory(ref Action verify)
        {
            var mockClientFactory = new Mock<IDs3ClientFactory>(MockBehavior.Strict);
            mockClientFactory
                .Setup(cf => cf.GetClientForNodeId(_firstNodeId))
                .Returns(BuildPutClient(ref verify, 0L, 10L));
            mockClientFactory
                .Setup(cf => cf.GetClientForNodeId(_secondNodeId))
                .Returns(BuildPutClient(ref verify, 10L, 11L));

            verify += mockClientFactory.VerifyAll;

            return mockClientFactory.Object;
        }

        private static IDs3Client BuildPutClient(ref Action verify, long offset, long length)
        {
            var mockClient = new Mock<IDs3Client>(MockBehavior.Strict);
            SetupPut(mockClient, "foo", offset, length);
            SetupPut(mockClient, "bar", offset, length);

            verify += mockClient.VerifyAll;

            return mockClient.Object;
        }

        private static void SetupPut(Mock<IDs3Client> mockClient, string objectName, long offset, long length)
        {
            mockClient.Setup(client => client.PutObject(It.Is<PutObjectRequest>(r =>
                r.BucketName == "bucket"
                && r.ObjectName == objectName
                && r.JobId == _bulkResponse.JobId
                && r.Offset == offset
                && r.GetContentStream().Length == length
            )));
        }

        private static Stream BuildMemoryStream(string prefix)
        {
            var memoryStream = new MemoryStream();
            var streamWriter = new StreamWriter(memoryStream);
            streamWriter.Write("prefix");
            streamWriter.Write("456789012345678901");
            streamWriter.Flush();
            memoryStream.Position = 0L;
            return memoryStream;
        }
    }
}
