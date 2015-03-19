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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Range = Ds3.Models.Range;

namespace TestDs3.Helpers.TransferItemSources
{
    using Ds3.Models;
    using System.Diagnostics;
    using Stubs = JobResponseStubs;

    [TestFixture]
    public class TestReadTransferItemSource
    {
        [Test, Timeout(1000)]
        public void EnumerateTransfersCanBeStopped()
        {
            var initialJobResponse = Stubs.BuildJobResponse(
                Stubs.Chunk1(null, false, false),
                Stubs.Chunk2(null, false, false),
                Stubs.Chunk3(null, false, false)
            );
            var factory = new Mock<IDs3ClientFactory>(MockBehavior.Strict);
            factory
                .Setup(f => f.GetClientForNodeId(It.IsAny<Guid?>()))
                .Returns(new Mock<IDs3Client>(MockBehavior.Strict).Object);
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.BuildFactory(It.IsAny<IEnumerable<Node>>()))
                .Returns(factory.Object);
            client
                .Setup(c => c.GetAvailableJobChunks(AvailableChunks(Stubs.JobId)))
                .Returns(GetAvailableJobChunksResponse.Success(
                    TimeSpan.FromMinutes(5),
                    Stubs.BuildJobResponse(Stubs.Chunk1(Stubs.NodeId1, true, true))
                ));

            var transferItemSource = new ReadTransferItemSource(_ => {}, client.Object, initialJobResponse);

            var readyToStop = new ManualResetEventSlim();
            var task = Task.Run(() =>
            {
                readyToStop.Wait();
                Thread.Sleep(130);
                transferItemSource.Stop();
            });
            using (var transfers = transferItemSource.EnumerateAvailableTransfers().GetEnumerator())
            {
                for (int i = 0; i < 2; i++)
                {
                    Assert.True(transfers.MoveNext());
                }
                readyToStop.Set();
                var timer = Stopwatch.StartNew();
                Assert.False(transfers.MoveNext());
                timer.Stop();
                Assert.GreaterOrEqual(timer.Elapsed, TimeSpan.FromMilliseconds(100));
            }
            task.Wait();
        }

        [Test, Timeout(10000)]
        public void EnumerateTransfersStreamsNewlyAvailableTransferItems()
        {
            var jobResponse1 = Stubs.BuildJobResponse(
                Stubs.Chunk1(null, false, false),
                Stubs.Chunk2(null, false, false),
                Stubs.Chunk3(null, false, false)
            );
            var jobResponse2 = Stubs.BuildJobResponse(
                Stubs.Chunk2(Stubs.NodeId2, true, true)
            );
            var jobResponse3 = Stubs.BuildJobResponse(
                Stubs.Chunk2(Stubs.NodeId2, true, true),
                Stubs.Chunk1(Stubs.NodeId1, true, true)
            );
            var jobResponse4 = Stubs.BuildJobResponse(
                Stubs.Chunk3(Stubs.NodeId2, true, true)
            );

            var node1Client = new Mock<IDs3Client>(MockBehavior.Strict).Object;
            var node2Client = new Mock<IDs3Client>(MockBehavior.Strict).Object;

            var clientFactory = new Mock<IDs3ClientFactory>(MockBehavior.Strict);
            clientFactory.Setup(cf => cf.GetClientForNodeId(Stubs.NodeId1)).Returns(node1Client);
            clientFactory.Setup(cf => cf.GetClientForNodeId(Stubs.NodeId2)).Returns(node2Client);

            var actionSequence = Queue(new { Item = (object)null, Type = "" });

            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client.Setup(c => c.BuildFactory(Stubs.Nodes)).Returns(clientFactory.Object);
            var chunkResponses = new[]
            {
                GetAvailableJobChunksResponse.RetryAfter(TimeSpan.FromMinutes(5)),
                GetAvailableJobChunksResponse.Success(TimeSpan.FromMinutes(11), jobResponse2),
                GetAvailableJobChunksResponse.Success(TimeSpan.FromMinutes(8), jobResponse2),
                GetAvailableJobChunksResponse.Success(TimeSpan.FromMinutes(11), jobResponse3),
                GetAvailableJobChunksResponse.Success(TimeSpan.FromMinutes(7), jobResponse3),
                GetAvailableJobChunksResponse.Success(TimeSpan.FromMinutes(6), jobResponse3),
                GetAvailableJobChunksResponse.RetryAfter(TimeSpan.FromMinutes(4)),
                GetAvailableJobChunksResponse.Success(TimeSpan.FromMinutes(11), jobResponse4)
            };
            var chunkResponseQueue = new Queue<GetAvailableJobChunksResponse>(chunkResponses);
            client
                .Setup(c => c.GetAvailableJobChunks(AvailableChunks(Stubs.JobId)))
                .Returns(() =>
                {
                    var r = chunkResponseQueue.Dequeue();
                    actionSequence.Enqueue(new { Item = (object)r, Type = "Allocated"});
                    return r;
                });

            var sleeps = new List<TimeSpan>();

            var source = new ReadTransferItemSource(sleeps.Add, client.Object, jobResponse1);

            var blobs = new[]
            {
                new Blob(Range.ByLength(0, 10), "foo"),
                new Blob(Range.ByLength(15, 20), "bar"),
                new Blob(Range.ByLength(0, 15), "bar"),
                new Blob(Range.ByLength(10, 10), "foo"),
                new Blob(Range.ByLength(0, 10), "hello"),
                new Blob(Range.ByLength(35, 11), "bar")
            };
            var producerConsumer = new ProducerConsumer(1);
            var completeBlobsTask = Task.Run(() =>
            {
                for (int i = 0; i < blobs.Length; i++)
                {
                    producerConsumer.Read(() =>
                        actionSequence.Enqueue(new { Item = (object)blobs[i], Type = "Completed" })
                    );
                    source.CompleteBlob(blobs[i]);
                }
            });
            CollectionAssert.AreEqual(
                new[]
                {
                    new TransferItem(node2Client, blobs[0]),
                    new TransferItem(node2Client, blobs[1]),
                    new TransferItem(node1Client, blobs[2]),
                    new TransferItem(node1Client, blobs[3]),
                    new TransferItem(node2Client, blobs[4]),
                    new TransferItem(node2Client, blobs[5]),
                },
                source
                    .EnumerateAvailableTransfers()
                    .Select(ti =>
                    {
                        producerConsumer.Write(() =>
                            actionSequence.Enqueue(new { Item = (object)ti.Blob, Type = "Returned" })
                        );
                        return ti;
                    })
                    .ToArray(),
                new TransferItemSourceHelpers.TransferItemComparer()
            );
            completeBlobsTask.Wait();
            var results = actionSequence.ToArray();
            CollectionAssert.AreEqual(
                new[]
                {
                    new { Item = (object)chunkResponses[0], Type = "Allocated" },
                    new { Item = (object)chunkResponses[1], Type = "Allocated" },
                    new { Item = (object)blobs[0], Type = "Returned" },
                    new { Item = (object)blobs[0], Type = "Completed" },
                    new { Item = (object)blobs[1], Type = "Returned" },
                    new { Item = (object)blobs[1], Type = "Completed" },
                    new { Item = (object)chunkResponses[2], Type = "Allocated" },
                    new { Item = (object)chunkResponses[3], Type = "Allocated" },
                    new { Item = (object)blobs[2], Type = "Returned" },
                    new { Item = (object)blobs[2], Type = "Completed" },
                    new { Item = (object)blobs[3], Type = "Returned" },
                    new { Item = (object)blobs[3], Type = "Completed" },
                    new { Item = (object)chunkResponses[4], Type = "Allocated" },
                    new { Item = (object)chunkResponses[5], Type = "Allocated" },
                    new { Item = (object)chunkResponses[6], Type = "Allocated" },
                    new { Item = (object)chunkResponses[7], Type = "Allocated" },
                    new { Item = (object)blobs[4], Type = "Returned" },
                    new { Item = (object)blobs[4], Type = "Completed" },
                    new { Item = (object)blobs[5], Type = "Returned" },
                    new { Item = (object)blobs[5], Type = "Completed" },
                },
                results
            );
            CollectionAssert.AreEqual(
                new[]
                {
                    TimeSpan.FromMinutes(5),
                    TimeSpan.FromMinutes(8),
                    TimeSpan.FromMinutes(7),
                    TimeSpan.FromMinutes(6),
                    TimeSpan.FromMinutes(4)
                },
                sleeps
            );

            clientFactory.VerifyAll();
            client.VerifyAll();
        }

        private class ProducerConsumer
        {
            private readonly Semaphore _writerSemaphore;
            private readonly Semaphore _readerSemaphore;

            public ProducerConsumer(int resourceCount)
            {
                this._writerSemaphore = new Semaphore(resourceCount, resourceCount);
                this._readerSemaphore = new Semaphore(0, resourceCount);
            }

            public void Write(Action action)
            {
                this._writerSemaphore.WaitOne();
                action();
                this._readerSemaphore.Release();
            }

            public void Read(Action action)
            {
                this._readerSemaphore.WaitOne();
                action();
                this._writerSemaphore.Release();
            }
        }

        private static ConcurrentQueue<T> Queue<T>(T example)
        {
            return new ConcurrentQueue<T>();
        }

        private static GetAvailableJobChunksRequest AvailableChunks(Guid jobId)
        {
            return Match.Create(
                r => r.JobId == jobId,
                () => new GetAvailableJobChunksRequest(jobId)
            );
        }
    }
}
