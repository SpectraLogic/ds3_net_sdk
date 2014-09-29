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
using Ds3.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace TestDs3
{
    [TestFixture]
    public class TestReadJob
    {
        [Test]
        public void TransferCallsAllNecessaryClientMethods()
        {
            Action verify = () => { };
            IJob job = new ReadJob(
                BuildClient(ref verify, useRetry: true),
                BuildBulkResponse(new[] { BuildFirstChunk(null), BuildSecondChunk(null) })
            );

            var transfers = new List<long>();
            var objects = new List<string>();
            job.DataTransferred += transfers.Add;
            job.ObjectCompleted += objects.Add;

            var streams = new Dictionary<string, StringStream>
            {
                { "foo", new StringStream() },
                { "bar", new StringStream() }
            };

            var stopwatch = Stopwatch.StartNew();
            job.Transfer(key => streams[key]);
            stopwatch.Stop();

            CollectionAssert.AreEqual(new[] { 10L, 10L, 11L, 11L }, transfers);
            CollectionAssert.AreEquivalent(new[] { "foo", "bar" }, objects);

            foreach (var stream in streams.Values)
            {
                Assert.AreEqual(_payloadString, stream.Result);
            }

            Assert.GreaterOrEqual(stopwatch.ElapsedMilliseconds, 1000);
            Assert.LessOrEqual(stopwatch.ElapsedMilliseconds, 1100);

            verify();
        }

        [Test]
        public void TransferThrowsExceptionWhenJobGoneButNotFinished()
        {
            Action verify = () => { };
            IJob job = new ReadJob(
                BuildClient(ref verify, jobGone: true),
                BuildBulkResponse(new[] { BuildFirstChunk(null), BuildSecondChunk(null) })
            );

            var transfers = new List<long>();
            var objects = new List<string>();
            job.DataTransferred += transfers.Add;
            job.ObjectCompleted += objects.Add;

            var streams = new Dictionary<string, StringStream>
            {
                { "foo", new StringStream() },
                { "bar", new StringStream() }
            };

            Assert.Throws<InvalidOperationException>(() => job.Transfer(key => streams[key]));
        }

        private static readonly Guid _jobId = Guid.Parse("879e42ba-77fb-41fc-84c0-511acc90912b");
        private static readonly Guid _firstNodeId = Guid.Parse("39ca7e02-82e8-4c8a-b74f-c6dab8f399ad");
        private static readonly Guid _secondNodeId = Guid.Parse("a84b3fc6-4b97-400f-a5e3-fed47b93551c");
        private static readonly IEnumerable<Node> _nodeList = new[]
        {
            new Node(_firstNodeId, "black-pearl-1", 80, 443),
            new Node(_secondNodeId, "black-pearl-2", 80, 443)
        };
        private static readonly string _payloadString = "123456789012345678901";
        private static readonly byte[] _payload = Encoding.UTF8.GetBytes(_payloadString);

        private static JobResponse BuildBulkResponse(IEnumerable<JobObjectList> chunks)
        {
            return new JobResponse(
                "bucket",
                _jobId,
                "HIGH",
                "GET",
                DateTime.Parse("9/8/2014 9:25:56 PM"),
                ChunkOrdering.InOrder,
                _nodeList,
                chunks
            );
        }

        private static JobObjectList BuildFirstChunk(Guid? nodeId, bool inCache = false)
        {
            return new JobObjectList(
                Guid.Parse("1f45812d-0b67-492b-b2b5-9fc2f6a0cf3f"),
                0L,
                nodeId,
                new[]
                {
                    new JobObject("foo", 10L, 0L, inCache),
                    new JobObject("bar", 10L, 0L, inCache)
                }
            );
        }

        private static JobObjectList BuildSecondChunk(Guid? nodeId, bool inCache = false)
        {
            return new JobObjectList(
                Guid.Parse("a01e8077-93ad-496a-8464-9963fea3e423"),
                1L,
                nodeId,
                new[]
                {
                    new JobObject("foo", 11L, 10L, inCache),
                    new JobObject("bar", 11L, 10L, inCache)
                }
            );
        }

        private static IDs3Client BuildClient(ref Action verify, bool useRetry = false, bool jobGone = false)
        {
            var mockClient = new Mock<IDs3Client>(MockBehavior.Strict);
            mockClient
                .Setup(client => client.BuildFactory(_nodeList))
                .Returns(BuildClientFactory(ref verify));
            var responses = new Queue<GetAvailableJobChunksResponse>();
            if (useRetry)
            {
                responses.Enqueue(GetAvailableJobChunksResponse.RetryAfter(TimeSpan.FromSeconds(1)));
            }
            responses.Enqueue(GetAvailableJobChunksResponse.Success(BuildBulkResponse(new[]
            {
                BuildFirstChunk(_firstNodeId, true)
            })));
            if (jobGone)
            {
                responses.Enqueue(GetAvailableJobChunksResponse.JobGone);
            }
            responses.Enqueue(GetAvailableJobChunksResponse.Success(BuildBulkResponse(new[]
            {
                BuildFirstChunk(_firstNodeId, true),
                BuildSecondChunk(_secondNodeId, true)
            })));
            mockClient
                .Setup(client => client.GetAvailableJobChunks(It.Is<GetAvailableJobChunksRequest>(r => r.JobId == _jobId)))
                .Returns(responses.Dequeue);

            verify += mockClient.VerifyAll;

            return mockClient.Object;
        }

        private static IDs3ClientFactory BuildClientFactory(ref Action verify)
        {
            var mockClientFactory = new Mock<IDs3ClientFactory>(MockBehavior.Strict);
            mockClientFactory
                .Setup(cf => cf.GetClientForNodeId(_firstNodeId))
                .Returns(BuildGetClient(ref verify, 0L, 10L));
            mockClientFactory
                .Setup(cf => cf.GetClientForNodeId(_secondNodeId))
                .Returns(BuildGetClient(ref verify, 10L, 11L));

            verify += mockClientFactory.VerifyAll;

            return mockClientFactory.Object;
        }

        private static IDs3Client BuildGetClient(ref Action verify, long offset, long length)
        {
            var mockClient = new Mock<IDs3Client>(MockBehavior.Strict);
            SetupGet(mockClient, "foo", offset, length);
            SetupGet(mockClient, "bar", offset, length);

            verify += mockClient.VerifyAll;

            return mockClient.Object;
        }

        private static void SetupGet(Mock<IDs3Client> mockClient, string objectName, long offset, long length)
        {
            mockClient
                .Setup(client => client.GetObject(It.Is<GetObjectRequest>(r =>
                    r.BucketName == "bucket"
                    && r.ObjectName == objectName
                    && r.JobId == _jobId
                    && r.Offset == offset
                )))
                .Returns(new GetObjectResponse(new Dictionary<string, string>()))
                .Callback<GetObjectRequest>(request => request.DestinationStream.Write(_payload, (int)offset, (int)length));
        }

        private class StringStream : MemoryStream
        {
            public string Result { get; private set; }

            protected override void Dispose(bool disposing)
            {
                if (disposing)
                {
                    this.Result = Encoding.UTF8.GetString(this.ToArray());
                }
                base.Dispose(disposing);
            }
        }
    }
}
