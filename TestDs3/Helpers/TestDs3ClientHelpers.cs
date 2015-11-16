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
using Ds3.Runtime;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Range = Ds3.Models.Range;

namespace TestDs3.Helpers
{
    using Stubs = JobResponseStubs;

    [TestFixture]
    public class TestDs3ClientHelpers
    {
        private static readonly Encoding _encoding = new UTF8Encoding(false);

        [Test, Timeout(1000)]
        public void BasicReadTransfer()
        {
            var initialJobResponse = Stubs.BuildJobResponse(
                Stubs.Chunk1(null, false, false),
                Stubs.Chunk2(null, false, false),
                Stubs.Chunk3(null, false, false)
            );
            var availableJobResponse = Stubs.BuildJobResponse(
                Stubs.Chunk1(Stubs.NodeId2, true, true),
                Stubs.Chunk2(Stubs.NodeId2, true, true),
                Stubs.Chunk3(Stubs.NodeId1, true, true)
            );

            var node1Client = new Mock<IDs3Client>(MockBehavior.Strict);
            SetupGetObject(node1Client, "hello", 0L, "ABCDefGHIJ");
            SetupGetObject(node1Client, "bar", 35L, "zABCDEFGHIJ");

            var node2Client = new Mock<IDs3Client>(MockBehavior.Strict);
            SetupGetObject(node2Client, "bar", 0L, "0123456789abcde");
            SetupGetObject(node2Client, "foo", 10L, "klmnopqrst");
            SetupGetObject(node2Client, "foo", 0L, "abcdefghij");
            SetupGetObject(node2Client, "bar", 15L, "fghijklmnopqrstuvwxy");

            var clientFactory = new Mock<IDs3ClientFactory>(MockBehavior.Strict);
            clientFactory
                .Setup(cf => cf.GetClientForNodeId(Stubs.NodeId1))
                .Returns(node1Client.Object);
            clientFactory
                .Setup(cf => cf.GetClientForNodeId(Stubs.NodeId2))
                .Returns(node2Client.Object);

            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.BuildFactory(Stubs.Nodes))
                .Returns(clientFactory.Object);
            client
                .Setup(c => c.BulkGet(ItIsBulkGetRequest(
                    Stubs.BucketName,
                    ChunkOrdering.None,
                    Stubs.ObjectNames,
                    Enumerable.Empty<Ds3PartialObject>()
                )))
                .Returns(initialJobResponse);
            client
                .Setup(c => c.GetAvailableJobChunks(ItIsGetAvailableJobChunksRequest(Stubs.JobId)))
                .Returns(GetAvailableJobChunksResponse.Success(TimeSpan.FromMinutes(1), availableJobResponse));

            var job = new Ds3ClientHelpers(client.Object).StartReadJob(
                Stubs.BucketName,
                Stubs.ObjectNames.Select(name => new Ds3Object(name, null))
            );

            var dataTransfers = new ConcurrentQueue<long>();
            var itemsCompleted = new ConcurrentQueue<string>();
            job.DataTransferred += dataTransfers.Enqueue;
            job.ItemCompleted += itemsCompleted.Enqueue;

            var streams = new ConcurrentDictionary<string, MockStream>();
            job.Transfer(key => streams.GetOrAdd(key, k => new MockStream()));

            node1Client.VerifyAll();
            node2Client.VerifyAll();
            clientFactory.VerifyAll();
            client.VerifyAll();

            CollectionAssert.AreEqual(
                new[]
                {
                    new { Key = "bar", Value = "0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJ" },
                    new { Key = "foo", Value = "abcdefghijklmnopqrst" },
                    new { Key = "hello", Value = "ABCDefGHIJ" },
                },
                from item in streams
                orderby item.Key
                select new { item.Key, Value = _encoding.GetString(item.Value.Result) }
            );
            CollectionAssert.AreEquivalent(new[] { 15L, 20L, 11L, 10L, 10L, 10L }, dataTransfers);
            CollectionAssert.AreEquivalent(Stubs.ObjectNames, itemsCompleted);
        }

        [Test, Timeout(1000)]
        public void PartialReadTransfer()
        {
            var partialObjects = new[]
            {
                new Ds3PartialObject(Range.ByLength(0L, 4L), "foo"),
                new Ds3PartialObject(Range.ByLength(6L, 10L), "foo"),
                new Ds3PartialObject(Range.ByLength(18L, 1L), "foo"),
                new Ds3PartialObject(Range.ByLength(10L, 26L), "bar"),
            };
            var fullObjects = new[] { "hello" };

            var initialJobResponse = Stubs.BuildJobResponse(
                Stubs.Chunk1(null, false, false),
                Stubs.Chunk2(null, false, false),
                Stubs.Chunk3(null, false, false)
            );
            var availableJobResponse = Stubs.BuildJobResponse(
                Stubs.Chunk1(Stubs.NodeId2, true, true),
                Stubs.Chunk2(Stubs.NodeId2, true, true),
                Stubs.Chunk3(Stubs.NodeId1, true, true)
            );

            var node1Client = new Mock<IDs3Client>(MockBehavior.Strict);
            SetupGetObject(node1Client, "hello", 0L, "ABCDefGHIJ", Range.ByLength(0L, 10L));
            SetupGetObject(node1Client, "bar", 35L, "z", Range.ByLength(35L, 1L));

            var node2Client = new Mock<IDs3Client>(MockBehavior.Strict);
            SetupGetObject(node2Client, "bar", 0L, "abcde", Range.ByLength(10L, 5L));
            SetupGetObject(node2Client, "foo", 10L, "klmnop!", Range.ByLength(10L, 6L), Range.ByLength(18L, 1L));
            SetupGetObject(node2Client, "foo", 0L, "abcdghij", Range.ByLength(0L, 4L), Range.ByLength(6L, 4L));
            SetupGetObject(node2Client, "bar", 15L, "fghijklmnopqrstuvwxy", Range.ByLength(15L, 20L));

            var clientFactory = new Mock<IDs3ClientFactory>(MockBehavior.Strict);
            clientFactory
                .Setup(cf => cf.GetClientForNodeId(Stubs.NodeId1))
                .Returns(node1Client.Object);
            clientFactory
                .Setup(cf => cf.GetClientForNodeId(Stubs.NodeId2))
                .Returns(node2Client.Object);

            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.BuildFactory(Stubs.Nodes))
                .Returns(clientFactory.Object);
            client
                .Setup(c => c.BulkGet(ItIsBulkGetRequest(
                    Stubs.BucketName,
                    ChunkOrdering.None,
                    fullObjects,
                    partialObjects
                )))
                .Returns(initialJobResponse);
            client
                .Setup(c => c.GetAvailableJobChunks(ItIsGetAvailableJobChunksRequest(Stubs.JobId)))
                .Returns(GetAvailableJobChunksResponse.Success(TimeSpan.FromMinutes(1), availableJobResponse));

            var job = new Ds3ClientHelpers(client.Object)
                .StartPartialReadJob(Stubs.BucketName, fullObjects, partialObjects);
            CollectionAssert.AreEquivalent(
                partialObjects.Concat(new[] { new Ds3PartialObject(Range.ByLength(0L, 10L), "hello") }),
                job.AllItems
            );

            var dataTransfers = new ConcurrentQueue<long>();
            var itemsCompleted = new ConcurrentQueue<Ds3PartialObject>();
            job.DataTransferred += dataTransfers.Enqueue;
            job.ItemCompleted += itemsCompleted.Enqueue;

            var streams = new ConcurrentDictionary<Ds3PartialObject, MockStream>();
            job.Transfer(key => streams.GetOrAdd(key, k => new MockStream()));

            node1Client.VerifyAll();
            node2Client.VerifyAll();
            clientFactory.VerifyAll();
            client.VerifyAll();

            var fullObjectPart = new Ds3PartialObject(Range.ByLength(0L, 10L), fullObjects[0]);
            CollectionAssert.AreEqual(
                new[]
                {
                    new { Key = partialObjects[0], Value = "abcd" },
                    new { Key = partialObjects[1], Value = "ghijklmnop" },
                    new { Key = partialObjects[2], Value = "!" },
                    new { Key = partialObjects[3], Value = "abcdefghijklmnopqrstuvwxyz" },
                    new { Key = fullObjectPart, Value = "ABCDefGHIJ" },
                }.OrderBy(it => it.Key).ToArray(),
                (
                    from item in streams
                    orderby item.Key
                    select new { item.Key, Value = _encoding.GetString(item.Value.Result) }
                ).ToArray()
            );
            CollectionAssert.AreEquivalent(
                new[] { 1L, 1L, 4L, 4L, 5L, 6L, 10L, 20L },
                dataTransfers.Sorted().ToArray()
            );
            CollectionAssert.AreEquivalent(partialObjects.Concat(new[] { fullObjectPart }), itemsCompleted);
        }

        [Test, Timeout(1000), TestCase(1048576L), TestCase(null)]
        public void BasicWriteTransfer(long? maxBlobSize)
        {
            var initialJobResponse = Stubs.BuildJobResponse(
                Stubs.Chunk1(null, false, false),
                Stubs.Chunk2(null, false, false),
                Stubs.Chunk3(null, false, false)
            );

            var node1Client = new Mock<IDs3Client>(MockBehavior.Strict);
            SetupPutObject(node1Client, "hello", 0L, "ABCDefGHIJ");
            SetupPutObject(node1Client, "bar", 35L, "zABCDEFGHIJ");

            var node2Client = new Mock<IDs3Client>(MockBehavior.Strict);
            SetupPutObject(node2Client, "bar", 0L, "0123456789abcde");
            SetupPutObject(node2Client, "foo", 10L, "klmnopqrst");
            SetupPutObject(node2Client, "foo", 0L, "abcdefghij");
            SetupPutObject(node2Client, "bar", 15L, "fghijklmnopqrstuvwxy");

            var clientFactory = new Mock<IDs3ClientFactory>(MockBehavior.Strict);
            clientFactory
                .Setup(cf => cf.GetClientForNodeId(Stubs.NodeId1))
                .Returns(node1Client.Object);
            clientFactory
                .Setup(cf => cf.GetClientForNodeId(Stubs.NodeId2))
                .Returns(node2Client.Object);

            var streams = new Dictionary<string, MockStream>
            {
                { "bar", new MockStream("0123456789abcdefghijklmnopqrstuvwxyzABCDEFGHIJ") },
                { "foo", new MockStream("abcdefghijklmnopqrst") },
                { "hello", new MockStream("ABCDefGHIJ") },
            };
            var ds3Objects = Stubs
                .ObjectNames
                .Select(name => new Ds3Object(name, streams[name].Length));

            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.BuildFactory(Stubs.Nodes))
                .Returns(clientFactory.Object);
            client
                .Setup(c => c.BulkPut(ItIsBulkPutRequest(Stubs.BucketName, ds3Objects, maxBlobSize)))
                .Returns(initialJobResponse);
            client
                .Setup(c => c.AllocateJobChunk(ItIsAllocateRequest(Stubs.ChunkId1)))
                .Returns(AllocateJobChunkResponse.Success(Stubs.Chunk1(Stubs.NodeId2, false, false)));
            client
                .Setup(c => c.AllocateJobChunk(ItIsAllocateRequest(Stubs.ChunkId2)))
                .Returns(AllocateJobChunkResponse.Success(Stubs.Chunk2(Stubs.NodeId2, false, false)));
            client
                .Setup(c => c.AllocateJobChunk(ItIsAllocateRequest(Stubs.ChunkId3)))
                .Returns(AllocateJobChunkResponse.Success(Stubs.Chunk3(Stubs.NodeId1, false, false)));

            var job = new Ds3ClientHelpers(client.Object).StartWriteJob(Stubs.BucketName, ds3Objects, maxBlobSize);

            var dataTransfers = new ConcurrentQueue<long>();
            var itemsCompleted = new ConcurrentQueue<string>();
            job.DataTransferred += dataTransfers.Enqueue;
            job.ItemCompleted += itemsCompleted.Enqueue;

            job.Transfer(key => streams[key]);

            node1Client.VerifyAll();
            node2Client.VerifyAll();
            clientFactory.VerifyAll();
            client.VerifyAll();

            CollectionAssert.AreEquivalent(new[] { 15L, 20L, 11L, 10L, 10L, 10L }, dataTransfers);
            CollectionAssert.AreEquivalent(Stubs.ObjectNames, itemsCompleted);
        }

        [Test]
        public void ReadTransferFailsUponTransferrerException()
        {
            var initialJobResponse = Stubs.BuildJobResponse(
                Stubs.Chunk1(null, false, false),
                Stubs.Chunk2(null, false, false),
                Stubs.Chunk3(null, false, false)
            );
            var availableJobResponse = Stubs.BuildJobResponse(
                Stubs.Chunk2(Stubs.NodeId2, true, true)
            );

            var node2Client = new Mock<IDs3Client>(MockBehavior.Strict);
            SetupGetObject(node2Client, "foo", 0L, "abcdefghij");
            node2Client
                .Setup(c => c.GetObject(ItIsGetObjectRequest(
                    Stubs.BucketName,
                    "bar",
                    Stubs.JobId,
                    15L,
                    Enumerable.Empty<Range>()
                )))
                .Throws<NullReferenceException>();

            var clientFactory = new Mock<IDs3ClientFactory>(MockBehavior.Strict);
            clientFactory
                .Setup(cf => cf.GetClientForNodeId(Stubs.NodeId2))
                .Returns(node2Client.Object);

            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.BuildFactory(Stubs.Nodes))
                .Returns(clientFactory.Object);
            client
                .Setup(c => c.BulkGet(ItIsBulkGetRequest(
                    Stubs.BucketName,
                    ChunkOrdering.None,
                    Stubs.ObjectNames,
                    Enumerable.Empty<Ds3PartialObject>()
                )))
                .Returns(initialJobResponse);
            client
                .Setup(c => c.GetAvailableJobChunks(ItIsGetAvailableJobChunksRequest(Stubs.JobId)))
                .Returns(GetAvailableJobChunksResponse.Success(TimeSpan.FromMinutes(1), availableJobResponse));

            var job = new Ds3ClientHelpers(client.Object).StartReadJob(
                Stubs.BucketName,
                Stubs.ObjectNames.Select(name => new Ds3Object(name, null))
            );
            try
            {
                job.Transfer(key => new MockStream());
                Assert.Fail("Should have thrown an exception.");
            }
            catch (AggregateException e)
            {
                Assert.IsInstanceOf<NullReferenceException>(e.InnerException);
            }
        }

        [Test]
        public void TestListObjects()
        {
            var ds3ClientMock = new Mock<IDs3Client>(MockBehavior.Strict);
            ds3ClientMock
                .Setup(client => client.GetBucket(It.IsAny<GetBucketRequest>()))
                .Returns(new Queue<GetBucketResponse>(new[] {
                    CreateGetBucketResponse(
                        marker: "",
                        nextMarker: "baz",
                        isTruncated: true,
                        ds3objectInfos: new List<Ds3ObjectInfo> {
                            BuildDs3Object("foo", "2cde576e5f5a613e6cee466a681f4929", "2009-10-12T17:50:30.000Z", 12),
                            BuildDs3Object("bar", "f3f98ff00be128139332bcf4b772be43", "2009-10-14T17:50:31.000Z", 12)
                        }
                    ),
                    CreateGetBucketResponse(
                        marker: "baz",
                        nextMarker: "",
                        isTruncated: false,
                        ds3objectInfos: new List<Ds3ObjectInfo> {
                            BuildDs3Object("baz", "802d45fcb9a3f7d00f1481362edc0ec9", "2009-10-18T17:50:35.000Z", 12)
                        }
                    )
                }).Dequeue);

            var objects = new Ds3ClientHelpers(ds3ClientMock.Object).ListObjects("mybucket").ToList();

            Assert.AreEqual(3, objects.Count);
            CheckContents(objects[0], "foo", 12);
            CheckContents(objects[1], "bar", 12);
            CheckContents(objects[2], "baz", 12);
        }

        [Test, Timeout(1000)]
        public void PartialObjectReturn()
        {
            var initialJobResponse = Stubs.BuildJobResponse(
                Stubs.ReadFailureChunk(null, false)
            );
            var availableJobResponse = Stubs.BuildJobResponse(
            
                Stubs.ReadFailureChunk(Stubs.NodeId1, true)
            );

            var node1Client = new Mock<IDs3Client>(MockBehavior.Strict);            
            SetupGetObjectWithContentLengthMismatchException(node1Client, "bar", 0L, "ABCDEFGHIJ", 20L, 10L); // The initial request is for all 20 bytes, but only the first 10 will be sent
            SetupGetObject(node1Client, "bar", 0L, "JLMNOPQRSTU", Range.ByPosition(9L, 19L));  // The client will request the full last byte based off of when the client fails

            var clientFactory = new Mock<IDs3ClientFactory>(MockBehavior.Strict);
            clientFactory
                .Setup(cf => cf.GetClientForNodeId(Stubs.NodeId1))
                .Returns(node1Client.Object);
            
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.BuildFactory(Stubs.Nodes))
                .Returns(clientFactory.Object);
            client
                .Setup(c => c.BulkGet(ItIsBulkGetRequest(
                    Stubs.BucketName,
                    ChunkOrdering.None,
                    Stubs.ObjectNames,
                    Enumerable.Empty<Ds3PartialObject>()
                )))
                .Returns(initialJobResponse);
            client
                .Setup(c => c.GetAvailableJobChunks(ItIsGetAvailableJobChunksRequest(Stubs.JobId)))
                .Returns(GetAvailableJobChunksResponse.Success(TimeSpan.FromMinutes(1), availableJobResponse));

            var job = new Ds3ClientHelpers(client.Object).StartReadJob(
                Stubs.BucketName,
                Stubs.ObjectNames.Select(name => new Ds3Object(name, null))
            );

            var dataTransfers = new ConcurrentQueue<long>();
            var itemsCompleted = new ConcurrentQueue<string>();
            job.DataTransferred += dataTransfers.Enqueue;
            job.ItemCompleted += itemsCompleted.Enqueue;

            var streams = new ConcurrentDictionary<string, MockStream>();
            job.Transfer(key => streams.GetOrAdd(key, k => new MockStream()));

            node1Client.VerifyAll();            
            clientFactory.VerifyAll();
            client.VerifyAll();

            // Since we are using a mock for the underlying client, the first request does not write any content to the stream
            CollectionAssert.AreEqual(
                new[]
                {
                    new { Key = "bar", Value = "ABCDEFGHIJLMNOPQRSTU" },
                },
                from item in streams
                orderby item.Key
                select new { item.Key, Value = _encoding.GetString(item.Value.Result) }
            );
            CollectionAssert.AreEquivalent(new[] { 20L }, dataTransfers);
            CollectionAssert.AreEquivalent(Stubs.PartialFailureObjectNames, itemsCompleted);
        }

        private static void WriteToStream(Stream stream, string value)
        {
            var buffer = _encoding.GetBytes(value);
            stream.Write(buffer, 0, buffer.Length);
        }

        private static BulkGetRequest ItIsBulkGetRequest(
            string bucketName,
            ChunkOrdering chunkOrdering,
            IEnumerable<string> fullObjects,
            IEnumerable<Ds3PartialObject> partialObjects)
        {
            return Match.Create(
                r =>
                    r.BucketName == bucketName
                    && r.ChunkOrder == chunkOrdering
                    && r.FullObjects.Sorted().SequenceEqual(fullObjects.Sorted())
                    && r.PartialObjects.Sorted().SequenceEqual(partialObjects.Sorted()),
                () => new BulkGetRequest(bucketName, fullObjects, partialObjects)
                    .WithChunkOrdering(chunkOrdering)
            );
        }

        private static BulkPutRequest ItIsBulkPutRequest(string bucketName, IEnumerable<Ds3Object> objects, long? maxBlobSize)
        {
            return Match.Create(
                r =>
                    r.BucketName == bucketName
                    && r.MaxBlobSize == maxBlobSize
                    && r.Objects.Select(o => new { o.Name, o.Size })
                        .SequenceEqual(objects.Select(o => new { o.Name, o.Size })),
                () => new BulkPutRequest(bucketName, objects.ToList())
            );
        }

        private static GetAvailableJobChunksRequest ItIsGetAvailableJobChunksRequest(Guid jobId)
        {
            return Match.Create(
                it => it.JobId == jobId,
                () => new GetAvailableJobChunksRequest(jobId)
            );
        }

        private static AllocateJobChunkRequest ItIsAllocateRequest(Guid chunkId)
        {
            return Match.Create(
                it => it.ChunkId == chunkId,
                () => new AllocateJobChunkRequest(chunkId)
            );
        }

        private static GetObjectRequest ItIsGetObjectRequest(
            string bucketName,
            string objectName,
            Guid jobId,
            long offset,
            IEnumerable<Range> byteRanges)
        {
            return Match.Create(
                it =>
                    it.BucketName == bucketName
                    && it.ObjectName == objectName
                    && it.JobId == jobId
                    && it.Offset == offset
                    && it.GetByteRanges().SequenceEqual(byteRanges),
                () => new GetObjectRequest(
                    bucketName,
                    objectName,
                    jobId,
                    offset,
                    It.IsAny<Stream>()
                )
            );
        }

        private static PutObjectRequest ItIsPutObjectRequest(
            string bucketName,
            string objectName,
            Guid jobId,
            long offset)
        {
            return Match.Create(
                it =>
                    it.BucketName == bucketName
                    && it.ObjectName == objectName
                    && it.JobId == jobId
                    && it.Offset == offset,
                () => new PutObjectRequest(
                    bucketName,
                    objectName,
                    jobId,
                    offset,
                    It.IsAny<Stream>()
                )
            );
        }

        private static void SetupGetObject(
            Mock<IDs3Client> client,
            string objectName,
            long offset,
            string payload,           
            params Range[] byteRanges
            )
        {
            client
                .Setup(c => c.GetObject(ItIsGetObjectRequest(
                    Stubs.BucketName,
                    objectName,
                    Stubs.JobId,
                    offset,
                    byteRanges
                )))
                .Returns(new GetObjectResponse(new Dictionary<string, string>()))
                .Callback<GetObjectRequest>(r => {
                    Console.WriteLine("Writing \"" + payload + "\" to stream");
                    WriteToStream(r.DestinationStream, payload);
                });
        }

        private static void SetupGetObjectWithContentLengthMismatchException(
            Mock<IDs3Client> client,
            string objectName,
            long offset,
            string payload,
            long expectedLength,
            long returnedLength,
            params Range[] byteRanges
            )
        {
            client
                .Setup(c => c.GetObject(ItIsGetObjectRequest(
                    Stubs.BucketName,
                    objectName,
                    Stubs.JobId,
                    offset,
                    byteRanges
                ))).Callback<GetObjectRequest>(r => {
                    Console.WriteLine("Writing \"" + payload + "\" to stream");
                    WriteToStream(r.DestinationStream, payload);
                })
                .Throws(new Ds3ContentLengthNotMatch("Content Length mismatch", expectedLength, returnedLength));
        }

        private static void SetupPutObject(
            Mock<IDs3Client> client,
            string objectName,
            long offset,
            string expectedData)
        {
            client
                .Setup(c => c.PutObject(ItIsPutObjectRequest(
                    Stubs.BucketName,
                    objectName,
                    Stubs.JobId,
                    offset
                )))
                .Callback<PutObjectRequest>(r =>
                {
                    using (var stream = r.GetContentStream())
                    using (var reader = new StreamReader(stream, _encoding))
                    {
                        Assert.AreEqual(expectedData, reader.ReadToEnd());
                    }
                });
        }

        private static void CheckContents(Ds3Object contents, string key, long size)
        {
            Assert.AreEqual(key, contents.Name);
            Assert.AreEqual(size, contents.Size);
        }

        private static GetBucketResponse CreateGetBucketResponse(string marker, bool isTruncated, string nextMarker, IEnumerable<Ds3ObjectInfo> ds3objectInfos)
        {
            return new GetBucketResponse(
                name: "mybucket",
                prefix: "",
                marker: marker,
                maxKeys: 2,
                isTruncated: isTruncated,
                delimiter: "",
                nextMarker: nextMarker,
                creationDate: DateTime.Now,
                objects: ds3objectInfos,
                metadata: new Dictionary<string, string>(),
                commonPrefixes: Enumerable.Empty<string>()
            );
        }

        private static Ds3ObjectInfo BuildDs3Object(string key, string eTag, string lastModified, long size)
        {
            var owner = new Owner("person@spectralogic.com", "75aa57f09aa0c8caeab4f8c24e99d10f8e7faeebf76c078efc7c6caea54ba06a");
            return new Ds3ObjectInfo(key, size, owner, eTag, "STANDARD", DateTime.Parse(lastModified));
        }
    }
}
