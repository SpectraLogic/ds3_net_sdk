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
using System.Collections.Generic;
using System.Diagnostics;
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
using Blob = Ds3.Helpers.Blob;
using Range = Ds3.Models.Range;

namespace TestDs3.Helpers.Strategies.ChunkStrategies
{
    [TestFixture]
    public class TestReadStreamChunkStrategy
    {
        [Test]
        public void TestEnumerateTransfersResetRetryAfter()
        {
            var retryAfter = 5;
            var jobResponse = JobResponseStubs.BuildJobResponse(
                JobResponseStubs.Chunk1(JobResponseStubs.NodeId1, false, false)
            );

            var node1Client = new Mock<IDs3Client>(MockBehavior.Strict).Object;

            var clientFactory = new Mock<IDs3ClientFactory>(MockBehavior.Strict);
            clientFactory.Setup(cf => cf.GetClientForNodeId(JobResponseStubs.NodeId1)).Returns(node1Client);

            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client.Setup(c => c.BuildFactory(JobResponseStubs.Nodes)).Returns(clientFactory.Object);

            client
                .Setup(
                    c =>
                        c.GetJobChunksReadyForClientProcessingSpectraS3(
                            AllocateMock.AvailableChunks(JobResponseStubs.JobId)))
                .Returns(() =>
                {
                    if (retryAfter == 1) //after 4 retires we want to success
                    {
                        return GetJobChunksReadyForClientProcessingSpectraS3Response.Success(TimeSpan.FromMinutes(5),
                            jobResponse);
                    }

                    return GetJobChunksReadyForClientProcessingSpectraS3Response.RetryAfter(TimeSpan.FromMinutes(5));
                });

            var source = new ReadStreamChunkStrategy(_ => { retryAfter--; }, retryAfter);

            using (var transfers = source.GetNextTransferItems(client.Object, jobResponse).GetEnumerator())
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
            var jobResponse1 = JobResponseStubs.BuildJobResponse(
                JobResponseStubs.Chunk1(JobResponseStubs.NodeId1, false, false),
                JobResponseStubs.Chunk2(JobResponseStubs.NodeId1, false, false),
                JobResponseStubs.Chunk3(JobResponseStubs.NodeId1, false, false)
            );

            var node1Client = new Mock<IDs3Client>(MockBehavior.Strict).Object;

            var clientFactory = new Mock<IDs3ClientFactory>(MockBehavior.Strict);
            clientFactory.Setup(cf => cf.GetClientForNodeId(JobResponseStubs.NodeId1)).Returns(node1Client);

            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client.Setup(c => c.BuildFactory(JobResponseStubs.Nodes)).Returns(clientFactory.Object);

            client
                .SetupSequence(
                    c =>
                        c.GetJobChunksReadyForClientProcessingSpectraS3(
                            AllocateMock.AvailableChunks(JobResponseStubs.JobId)))
                .Returns(GetJobChunksReadyForClientProcessingSpectraS3Response.RetryAfter(TimeSpan.FromMinutes(5)))
                .Returns(GetJobChunksReadyForClientProcessingSpectraS3Response.Success(TimeSpan.FromMinutes(5),
                    jobResponse1));

            var sleeps = new List<TimeSpan>();

            var source = new ReadStreamChunkStrategy(sleeps.Add);

            using (var transfers = source.GetNextTransferItems(client.Object, jobResponse1).GetEnumerator())
            {
                var transfered = new TransferItem[6];
                for (var i = 0; i < 6; i++)
                {
                    Assert.True(transfers.MoveNext());
                    transfered[i] = transfers.Current;
                    source.CompleteBlob(transfers.Current.Blob);
                }

                Assert.False(transfers.MoveNext());

                CollectionAssert.AreEqual(
                    new[]
                    {
                        new TransferItem(node1Client, new Blob(Range.ByLength(0, 15), "bar")),
                        new TransferItem(node1Client, new Blob(Range.ByLength(0, 10), "foo")),
                        new TransferItem(node1Client, new Blob(Range.ByLength(0, 10), "hello")),
                        new TransferItem(node1Client, new Blob(Range.ByLength(10, 10), "foo")),
                        new TransferItem(node1Client, new Blob(Range.ByLength(15, 20), "bar")),
                        new TransferItem(node1Client, new Blob(Range.ByLength(35, 11), "bar"))
                    },
                    transfered,
                    new TransferItemSourceHelpers.TransferItemComparer()
                );

                CollectionAssert.AreEqual(
                    new[] {TimeSpan.FromMinutes(5)},
                    sleeps
                );
            }

            client.VerifyAll();
            clientFactory.VerifyAll();
        }

        [Test]
        public void TestGetNextTransferItemsCanBeStopped()
        {
            var initialJobResponse = JobResponseStubs.BuildJobResponse(
                JobResponseStubs.Chunk1(null, false, false),
                JobResponseStubs.Chunk2(null, false, false),
                JobResponseStubs.Chunk3(null, false, false)
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
                .Setup(
                    c =>
                        c.GetJobChunksReadyForClientProcessingSpectraS3(
                            AllocateMock.AvailableChunks(JobResponseStubs.JobId)))
                .Returns(GetJobChunksReadyForClientProcessingSpectraS3Response.Success(
                    TimeSpan.FromMinutes(5),
                    JobResponseStubs.BuildJobResponse(JobResponseStubs.Chunk1(JobResponseStubs.NodeId1, true, true))
                ));

            var source = new ReadStreamChunkStrategy(_ => { });

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
        public void TestGetNextTransferItemsRetryAfter()
        {
            var jobResponse = JobResponseStubs.BuildJobResponse(
                JobResponseStubs.Chunk1(JobResponseStubs.NodeId1, false, false));

            var clientFactory = new Mock<IDs3ClientFactory>(MockBehavior.Strict);

            var client = new Mock<IDs3Client>(MockBehavior.Strict);

            client
                .Setup(
                    c =>
                        c.GetJobChunksReadyForClientProcessingSpectraS3(
                            AllocateMock.AvailableChunks(JobResponseStubs.JobId)))
                .Returns(GetJobChunksReadyForClientProcessingSpectraS3Response.RetryAfter(TimeSpan.FromMinutes(5)));

            var sleeps = new List<TimeSpan>();

            var source = new ReadStreamChunkStrategy(sleeps.Add, 2);

            using (var transfers = source.GetNextTransferItems(client.Object, jobResponse).GetEnumerator())
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

            CollectionAssert.AreEqual(
                new[] {TimeSpan.FromMinutes(5), TimeSpan.FromMinutes(5)},
                sleeps
            );

            source = new ReadStreamChunkStrategy(_ => { }, 0);
            using (var transfers = source.GetNextTransferItems(client.Object, jobResponse).GetEnumerator())
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

            source = new ReadStreamChunkStrategy(_ => { }, 1);
            using (var transfers = source.GetNextTransferItems(client.Object, jobResponse).GetEnumerator())
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

            source = new ReadStreamChunkStrategy(_ => { }, 2);
            using (var transfers = source.GetNextTransferItems(client.Object, jobResponse).GetEnumerator())
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

            source = new ReadStreamChunkStrategy(_ => { }, 100);
            using (var transfers = source.GetNextTransferItems(client.Object, jobResponse).GetEnumerator())
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

            client.VerifyAll();
            clientFactory.VerifyAll();
        }

        [Test]
        [Timeout(1000)]
        public void TestGetNextTransferItemsRetryGetChunks()
        {
            var initialJobResponse = JobResponseStubs.BuildJobResponse(
                JobResponseStubs.Chunk1(JobResponseStubs.NodeId1, false, false),
                JobResponseStubs.Chunk2(JobResponseStubs.NodeId1, false, false)
            );

            var node1Client = new Mock<IDs3Client>(MockBehavior.Strict);

            var factory = new Mock<IDs3ClientFactory>(MockBehavior.Strict);

            factory.Setup(cf => cf.GetClientForNodeId(JobResponseStubs.NodeId1)).Returns(node1Client.Object);

            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.BuildFactory(It.IsAny<IEnumerable<JobNode>>()))
                .Returns(factory.Object);
            client
                .Setup(
                    c =>
                        c.GetJobChunksReadyForClientProcessingSpectraS3(
                            AllocateMock.AvailableChunks(JobResponseStubs.JobId)))
                .Returns(
                    GetJobChunksReadyForClientProcessingSpectraS3Response.Success(
                        TimeSpan.FromMinutes(5),
                        JobResponseStubs.BuildJobResponse(JobResponseStubs.Chunk1(JobResponseStubs.NodeId1, true, true))));

            var source = new ReadStreamChunkStrategy(_ => { }, 5);


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
            var initialJobResponse = JobResponseStubs.BuildJobResponse(
                JobResponseStubs.Chunk1(JobResponseStubs.NodeId1, false, false),
                JobResponseStubs.Chunk2(JobResponseStubs.NodeId1, false, false),
                JobResponseStubs.Chunk3(JobResponseStubs.NodeId1, false, false)
            );

            var node1Client = new Mock<IDs3Client>(MockBehavior.Strict);

            var factory = new Mock<IDs3ClientFactory>(MockBehavior.Strict);

            factory.Setup(cf => cf.GetClientForNodeId(JobResponseStubs.NodeId1)).Returns(node1Client.Object);

            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.BuildFactory(It.IsAny<IEnumerable<JobNode>>()))
                .Returns(factory.Object);


            var first = true;
            client
                .Setup(
                    c =>
                        c.GetJobChunksReadyForClientProcessingSpectraS3(
                            AllocateMock.AvailableChunks(JobResponseStubs.JobId)))
                .Returns(() =>
                {
                    if (first)
                    {
                        first = false;
                        return GetJobChunksReadyForClientProcessingSpectraS3Response.Success(TimeSpan.FromMinutes(5),
                            JobResponseStubs.BuildJobResponse(JobResponseStubs.Chunk1(JobResponseStubs.NodeId1, true,
                                true)));
                    }

                    return GetJobChunksReadyForClientProcessingSpectraS3Response.Success(TimeSpan.FromMinutes(5),
                        JobResponseStubs.BuildJobResponse(JobResponseStubs.Chunk2(JobResponseStubs.NodeId1, true, true)));
                });


            var source = new ReadStreamChunkStrategy(_ => { }, 5);


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
    }
}