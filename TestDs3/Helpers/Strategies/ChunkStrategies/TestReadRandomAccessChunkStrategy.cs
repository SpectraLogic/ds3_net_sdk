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

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ds3;
using Ds3.Calls;
using Ds3.Helpers.Strategies.ChunkStrategies;
using Ds3.Lang;
using Ds3.Models;
using Ds3.Runtime;
using Moq;
using NUnit.Framework;
using Range = Ds3.Models.Range;

namespace TestDs3.Helpers.Strategies.ChunkStrategies
{
    using Stubs = JobResponseStubs;

    [TestFixture]
    public class TestReadRandomAccessChunkStrategy
    {
        private static ConcurrentQueue<T> Queue<T>(T example)
        {
            return new ConcurrentQueue<T>();
        }

        private class ProducerConsumer
        {
            private readonly Semaphore _readerSemaphore;
            private readonly Semaphore _writerSemaphore;

            public ProducerConsumer(int resourceCount)
            {
                _writerSemaphore = new Semaphore(resourceCount, resourceCount);
                _readerSemaphore = new Semaphore(0, resourceCount);
            }

            public void Write(Action action)
            {
                _writerSemaphore.WaitOne();
                action();
                _readerSemaphore.Release();
            }

            public void Read(Action action)
            {
                _readerSemaphore.WaitOne();
                action();
                _writerSemaphore.Release();
            }
        }

        [Test]
        public void TestEnumerateTransfersResetRetryAfter()
        {
            var retryAfter = 5;
            var initialJobResponse = Stubs.BuildJobResponse(
                Stubs.Chunk1(null, false, false)
            );

            var factory = new Mock<IDs3ClientFactory>(MockBehavior.Strict);
            factory
                .Setup(f => f.GetClientForNodeId(It.IsAny<Guid?>()))
                .Returns(new Mock<IDs3Client>(MockBehavior.Strict).Object);
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.BuildFactory(It.IsAny<IEnumerable<JobNode>>()))
                .Returns(factory.Object);
            client
                .Setup(c => c.GetJobChunksReadyForClientProcessingSpectraS3(AllocateMock.AvailableChunks(Stubs.JobId)))
                .Returns(() =>
                {
                    if (retryAfter == 1) //after 4 retires we want to success
                    {
                        return GetJobChunksReadyForClientProcessingSpectraS3Response.Success(
                            TimeSpan.FromMinutes(0),
                            Stubs.BuildJobResponse(Stubs.Chunk1(Stubs.NodeId1, true, true)));
                    }

                    return GetJobChunksReadyForClientProcessingSpectraS3Response.RetryAfter(TimeSpan.FromSeconds(0));
                });

            var source = new ReadRandomAccessChunkStrategy(_ => { retryAfter--; }, retryAfter);


            using (var transfers = source.GetNextTransferItems(client.Object, initialJobResponse).GetEnumerator())
            {
                Assert.True(source.RetryAfer.RetryAfterLeft == 5);
                transfers.MoveNext();
                Assert.True(source.RetryAfer.RetryAfterLeft == 5);
                    //we want to make sure that the retryAfter value was reseted
                transfers.MoveNext();
            }
        }

        [Test]
        public void TestGetNextTransferItems()
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

            var actionSequence = Queue(new {Item = (object) null, Type = ""});

            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client.Setup(c => c.BuildFactory(Stubs.Nodes)).Returns(clientFactory.Object);
            var chunkResponses = new[]
            {
                GetJobChunksReadyForClientProcessingSpectraS3Response.RetryAfter(TimeSpan.FromMinutes(5)),
                GetJobChunksReadyForClientProcessingSpectraS3Response.Success(TimeSpan.FromMinutes(11), jobResponse2),
                GetJobChunksReadyForClientProcessingSpectraS3Response.Success(TimeSpan.FromMinutes(8), jobResponse2),
                GetJobChunksReadyForClientProcessingSpectraS3Response.Success(TimeSpan.FromMinutes(11), jobResponse3),
                GetJobChunksReadyForClientProcessingSpectraS3Response.Success(TimeSpan.FromMinutes(7), jobResponse3),
                GetJobChunksReadyForClientProcessingSpectraS3Response.Success(TimeSpan.FromMinutes(6), jobResponse3),
                GetJobChunksReadyForClientProcessingSpectraS3Response.RetryAfter(TimeSpan.FromMinutes(4)),
                GetJobChunksReadyForClientProcessingSpectraS3Response.Success(TimeSpan.FromMinutes(11), jobResponse4)
            };
            var chunkResponseQueue = new Queue<GetJobChunksReadyForClientProcessingSpectraS3Response>(chunkResponses);
            client
                .Setup(c => c.GetJobChunksReadyForClientProcessingSpectraS3(AllocateMock.AvailableChunks(Stubs.JobId)))
                .Returns(() =>
                {
                    var r = chunkResponseQueue.Dequeue();
                    actionSequence.Enqueue(new {Item = (object) r, Type = "Allocated"});
                    return r;
                });

            var sleeps = new List<TimeSpan>();

            var source = new ReadRandomAccessChunkStrategy(sleeps.Add);

            var blobs = new[]
            {
                new Ds3.Helpers.Blob(Range.ByLength(10, 10), "foo"),
                new Ds3.Helpers.Blob(Range.ByLength(15, 20), "bar"),
                new Ds3.Helpers.Blob(Range.ByLength(0, 15), "bar"),
                new Ds3.Helpers.Blob(Range.ByLength(0, 10), "foo"),
                new Ds3.Helpers.Blob(Range.ByLength(0, 10), "hello"),
                new Ds3.Helpers.Blob(Range.ByLength(35, 11), "bar")
            };
            var producerConsumer = new ProducerConsumer(1);
            var completeBlobsTask = Task.Run(() =>
            {
                for (var i = 0; i < blobs.Length; i++)
                {
                    producerConsumer.Read(() =>
                                actionSequence.Enqueue(new {Item = (object) blobs[i], Type = "Completed"})
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
                source.GetNextTransferItems(client.Object, jobResponse1)
                    .Select(ti =>
                    {
                        producerConsumer.Write(() =>
                                    actionSequence.Enqueue(new {Item = (object) ti.Blob, Type = "Returned"})
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
                    new {Item = (object) chunkResponses[0], Type = "Allocated"},
                    new {Item = (object) chunkResponses[1], Type = "Allocated"},
                    new {Item = (object) blobs[0], Type = "Returned"},
                    new {Item = (object) blobs[0], Type = "Completed"},
                    new {Item = (object) blobs[1], Type = "Returned"},
                    new {Item = (object) blobs[1], Type = "Completed"},
                    new {Item = (object) chunkResponses[2], Type = "Allocated"},
                    new {Item = (object) chunkResponses[3], Type = "Allocated"},
                    new {Item = (object) blobs[2], Type = "Returned"},
                    new {Item = (object) blobs[2], Type = "Completed"},
                    new {Item = (object) blobs[3], Type = "Returned"},
                    new {Item = (object) blobs[3], Type = "Completed"},
                    new {Item = (object) chunkResponses[4], Type = "Allocated"},
                    new {Item = (object) chunkResponses[5], Type = "Allocated"},
                    new {Item = (object) chunkResponses[6], Type = "Allocated"},
                    new {Item = (object) chunkResponses[7], Type = "Allocated"},
                    new {Item = (object) blobs[4], Type = "Returned"},
                    new {Item = (object) blobs[4], Type = "Completed"},
                    new {Item = (object) blobs[5], Type = "Returned"},
                    new {Item = (object) blobs[5], Type = "Completed"},
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

        [Test]
        public void TestGetNextTransferItemsCanBeStopped()
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
                .Setup(c => c.BuildFactory(It.IsAny<IEnumerable<JobNode>>()))
                .Returns(factory.Object);
            client
                .Setup(c => c.GetJobChunksReadyForClientProcessingSpectraS3(AllocateMock.AvailableChunks(Stubs.JobId)))
                .Returns(GetJobChunksReadyForClientProcessingSpectraS3Response.Success(
                    TimeSpan.FromMinutes(5),
                    Stubs.BuildJobResponse(Stubs.Chunk1(Stubs.NodeId1, true, true))
                ));

            var source = new ReadRandomAccessChunkStrategy(_ => { });

            var readyToStop = new ManualResetEventSlim();
            var task = Task.Run(() =>
            {
                readyToStop.Wait();
                Thread.Sleep(130);
                source.Stop();
            });
            using (var transfers = source.GetNextTransferItems(client.Object, initialJobResponse).GetEnumerator())
            {
                for (var i = 0; i < 2; i++)
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

        [Test]
        [Timeout(1000)]
        public void TestGetNextTransferItemsRetryGetChunks()
        {
            var initialJobResponse = Stubs.BuildJobResponse(
                Stubs.Chunk1(Stubs.NodeId1, false, false),
                Stubs.Chunk2(Stubs.NodeId1, false, false)
            );

            var node1Client = new Mock<IDs3Client>(MockBehavior.Strict);

            var factory = new Mock<IDs3ClientFactory>(MockBehavior.Strict);

            factory.Setup(cf => cf.GetClientForNodeId(Stubs.NodeId1)).Returns(node1Client.Object);

            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.BuildFactory(It.IsAny<IEnumerable<JobNode>>()))
                .Returns(factory.Object);
            client
                .Setup(c => c.GetJobChunksReadyForClientProcessingSpectraS3(AllocateMock.AvailableChunks(Stubs.JobId)))
                .Returns(
                    GetJobChunksReadyForClientProcessingSpectraS3Response.Success(
                        TimeSpan.FromMinutes(5),
                        Stubs.BuildJobResponse(Stubs.Chunk1(Stubs.NodeId1, true, true))));

            var source = new ReadRandomAccessChunkStrategy(_ => { }, 5);


            using (var transfers = source.GetNextTransferItems(client.Object, initialJobResponse).GetEnumerator())
            {
                var itemGetter = new ThreadSafeEnumerator<TransferItem>(transfers);
                Assert.Throws<Ds3NoMoreRetriesException>(() =>
                {
                    TransferItem it = null;
                    itemGetter.TryGetNext(ref it); //the first blob in the first chunk will return
                    source.CompleteBlob(it.Blob);
                    itemGetter.TryGetNext(ref it); //the second blob in the first chunk will return
                    source.CompleteBlob(it.Blob);
                    itemGetter.TryGetNext(ref it);
                        //exception will be thrown since we will get the same chunk over and over
                });
            }

            node1Client.VerifyAll();
            factory.VerifyAll();
        }

        [Test]
        [Timeout(2000)]
        public void TestGetNextTransferItemsRetryGetChunks2()
        {
            var initialJobResponse = Stubs.BuildJobResponse(
                Stubs.Chunk1(Stubs.NodeId1, false, false),
                Stubs.Chunk2(Stubs.NodeId1, false, false),
                Stubs.Chunk3(Stubs.NodeId1, false, false)
            );

            var node1Client = new Mock<IDs3Client>(MockBehavior.Strict);

            var factory = new Mock<IDs3ClientFactory>(MockBehavior.Strict);

            factory.Setup(cf => cf.GetClientForNodeId(Stubs.NodeId1)).Returns(node1Client.Object);

            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.BuildFactory(It.IsAny<IEnumerable<JobNode>>()))
                .Returns(factory.Object);


            var first = true;
            client
                .Setup(c => c.GetJobChunksReadyForClientProcessingSpectraS3(AllocateMock.AvailableChunks(Stubs.JobId)))
                .Returns(() =>
                {
                    if (first)
                    {
                        first = false;
                        return GetJobChunksReadyForClientProcessingSpectraS3Response.Success(TimeSpan.FromMinutes(5),
                            Stubs.BuildJobResponse(Stubs.Chunk1(Stubs.NodeId1, true, true)));
                    }

                    return GetJobChunksReadyForClientProcessingSpectraS3Response.Success(TimeSpan.FromMinutes(5),
                        Stubs.BuildJobResponse(Stubs.Chunk2(Stubs.NodeId1, true, true)));
                });


            var source = new ReadRandomAccessChunkStrategy(_ => { }, 5);


            using (var transfers = source.GetNextTransferItems(client.Object, initialJobResponse).GetEnumerator())
            {
                var itemGetter = new ThreadSafeEnumerator<TransferItem>(transfers);
                Assert.Throws<Ds3NoMoreRetriesException>(() =>
                {
                    TransferItem it = null;
                    itemGetter.TryGetNext(ref it); //the first blob in the first chunk will return
                    source.CompleteBlob(it.Blob);

                    itemGetter.TryGetNext(ref it); //the second blob in the first chunk will return
                    source.CompleteBlob(it.Blob);

                    itemGetter.TryGetNext(ref it); //the first blob in the second chunk will return
                    source.CompleteBlob(it.Blob);

                    itemGetter.TryGetNext(ref it); //the second blob in the second chunk will return
                    source.CompleteBlob(it.Blob);

                    itemGetter.TryGetNext(ref it);
                        //exception will be thrown since we will get the same chunk over and over
                });
            }

            node1Client.VerifyAll();
            factory.VerifyAll();
        }

        [Test]
        public void TestGetNextTransferItemsSetRetryAfter()
        {
            var initialJobResponse = Stubs.BuildJobResponse(Stubs.Chunk1(null, false, false));
            var factory = new Mock<IDs3ClientFactory>(MockBehavior.Strict);
            factory
                .Setup(f => f.GetClientForNodeId(It.IsAny<Guid?>()))
                .Returns(new Mock<IDs3Client>(MockBehavior.Strict).Object);
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.BuildFactory(It.IsAny<IEnumerable<JobNode>>()))
                .Returns(factory.Object);
            client
                .Setup(c => c.GetJobChunksReadyForClientProcessingSpectraS3(AllocateMock.AvailableChunks(Stubs.JobId)))
                .Returns(GetJobChunksReadyForClientProcessingSpectraS3Response.RetryAfter(
                    TimeSpan.FromMinutes(0)));

            var source = new ReadRandomAccessChunkStrategy(_ => { }, 0);
            using (var transfers = source.GetNextTransferItems(client.Object, initialJobResponse).GetEnumerator())
            {
                try
                {
                    Assert.True(source.RetryAfer.RetryAfterLeft == 0);
                    transfers.MoveNext(); //Should throw Ds3NoMoreRetriesException
                    Assert.Fail();
                }
                catch (Ds3NoMoreRetriesException ex)
                {
                    Assert.True(source.RetryAfer.RetryAfterLeft == 0);
                    Assert.True(ex.Message.Equals(Resources.NoMoreRetriesException));
                }
            }

            source = new ReadRandomAccessChunkStrategy(_ => { }, 1);
            using (var transfers = source.GetNextTransferItems(client.Object, initialJobResponse).GetEnumerator())
            {
                try
                {
                    Assert.True(source.RetryAfer.RetryAfterLeft == 1);
                    transfers.MoveNext(); //Should throw Ds3NoMoreRetriesException
                    Assert.Fail();
                }
                catch (Ds3NoMoreRetriesException ex)
                {
                    Assert.True(source.RetryAfer.RetryAfterLeft == 0);
                    Assert.True(ex.Message.Equals(Resources.NoMoreRetriesException));
                }
            }

            source = new ReadRandomAccessChunkStrategy(_ => { }, 2);
            using (var transfers = source.GetNextTransferItems(client.Object, initialJobResponse).GetEnumerator())
            {
                try
                {
                    Assert.True(source.RetryAfer.RetryAfterLeft == 2);
                    transfers.MoveNext(); //Should throw Ds3NoMoreRetriesException
                    Assert.Fail();
                }
                catch (Ds3NoMoreRetriesException ex)
                {
                    Assert.True(source.RetryAfer.RetryAfterLeft == 0);
                    Assert.True(ex.Message.Equals(Resources.NoMoreRetriesException));
                }
            }

            source = new ReadRandomAccessChunkStrategy(_ => { }, 100);
            using (var transfers = source.GetNextTransferItems(client.Object, initialJobResponse).GetEnumerator())
            {
                try
                {
                    Assert.True(source.RetryAfer.RetryAfterLeft == 100);
                    transfers.MoveNext(); //Should throw Ds3NoMoreRetriesException
                    Assert.Fail();
                }
                catch (Ds3NoMoreRetriesException ex)
                {
                    Assert.True(source.RetryAfer.RetryAfterLeft == 0);
                    Assert.True(ex.Message.Equals(Resources.NoMoreRetriesException));
                }
            }
        }
    }
}