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
using System.IO;
using System.Linq;
using System.Text;

using Moq;
using NUnit.Framework;

using Ds3;
using Ds3.Calls;
using Ds3.Helpers;
using Ds3.Models;

namespace TestDs3
{
    [TestFixture]
    public class TestDs3ClientHelpers
    {
        [Test]
        public void TestReadObjects()
        {
            var ds3ClientMock = new Mock<IDs3Client>(MockBehavior.Strict);
            ds3ClientMock
                .Setup(client => client.BulkGet(It.IsAny<BulkGetRequest>()))
                .Returns(CreateJobResponse("GET", new[] {
                    new JobObjectList(Guid.Parse("e36c4e47-9de7-4b56-8e2b-7ac062e6a094"), 0, null, new[] {
                        new JobObject("baz", 20, 0, false)
                    }),
                    new JobObjectList(Guid.Parse("ea581f53-da37-4bac-ab51-6aa0a61ec558"), 1, null, new[] {
                        new JobObject("baz", 6, 20, false),
                        new JobObject("bar", 14, 0, false)
                    }),
                    new JobObjectList(Guid.Parse("9d3098ba-e652-4dce-a237-321c31f343b5"), 2, null, new[] {
                        new JobObject("foo", 10, 0, false)
                    })
                }));
            var requests =
                new[] {
                    new { ObjectName = "baz", Offset = 0L, Contents = "12345678901234567890" },
                    new { ObjectName = "baz", Offset = 20L, Contents = "123456" },
                    new { ObjectName = "bar", Offset = 0L, Contents = "12345678901234" },
                    new { ObjectName = "foo", Offset = 0L, Contents = "1234567890" }
                }
                .ToDictionary(
                    req => new { req.ObjectName, req.Offset },
                    req => req.Contents
                );
            ds3ClientMock
                .Setup(client => client.GetObject(It.IsAny<GetObjectRequest>()))
                .Returns<GetObjectRequest>(request =>
                {
                    HelpersForTest
                        .StringToStream(requests[new { request.ObjectName, request.Offset }])
                        .CopyTo(request.DestinationStream);
                    return new GetObjectResponse(new Dictionary<string, string>());
                });
            var ds3ClientFactoryMock = new Mock<IDs3ClientFactory>(MockBehavior.Strict);
            ds3ClientFactoryMock
                .Setup(factory => factory.GetClientForNodeId(It.IsAny<Guid?>()))
                .Returns(ds3ClientMock.Object);
            ds3ClientMock
                .Setup(client => client.BuildFactory(It.IsAny<IEnumerable<Node>>()))
                .Returns(ds3ClientFactoryMock.Object);

            var objectsToGet = new[] {
                new Ds3Object("foo", null),
                new Ds3Object("bar", null),
                new Ds3Object("baz", null)
            };
            var streams = new Dictionary<string, StringStream>();
            new Ds3ClientHelpers(ds3ClientMock.Object)
                .StartReadJob("mybucket", objectsToGet)
                .Transfer(key => {
                    var stream = new StringStream();
                    streams.Add(key, stream);
                    return stream;
                });

            var expectedContents = new Dictionary<string, string> {
                { "baz", "12345678901234567890123456" },
                { "bar", "12345678901234" },
                { "foo", "1234567890" }
            };
            foreach (var kvp in streams)
            {
                Assert.AreEqual(expectedContents[kvp.Key], kvp.Value.Result);
            }
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

        [Test]
        public void TestWriteObjectsWhenAllSmallerThanPartSize()
        {
            var bucket = "mybucket";

            var clientMock = new BulkPutClientMock(CreateJobResponse(
                "PUT",
                new[] {
                    new JobObjectList(Guid.Parse("12bdf500-7a1f-422f-9c56-6dc05fd82db7"), 0, null, new[] {
                        new JobObject("baz", 12, 0, false),
                        new JobObject("bar", 12, 0, false)
                    }),
                    new JobObjectList(Guid.Parse("9116b513-75db-4b1b-acf4-f39eec3dbcdc"), 1, null, new[] {
                        new JobObject("foo", 12, 0, false)
                    })
                }
            ));

            var objectsToPut = new[] {
                new Ds3Object("foo", 12),
                new Ds3Object("bar", 12),
                new Ds3Object("baz", 12)
            };
            new Ds3ClientHelpers(clientMock.Object)
                .StartWriteJob(bucket, objectsToPut)
                .Transfer(key => HelpersForTest.StringToStream(key + " contents"));

            CollectionAssert.AreEquivalent(
                from key in new[] { "bar", "baz", "foo" }
                orderby key
                select new { BucketName = bucket, ObjectName = key, JobId = _jobId, Offset = 0L, Content = key + " contents" },
                from put in clientMock.Puts
                orderby put.ObjectName
                let content = clientMock.PutContents[put.ObjectName]
                select new { put.BucketName, put.ObjectName, put.JobId, put.Offset, Content = content }
            );
        }

        [Test]
        public void TestWriteObjectsWhenGreaterThanPartSize()
        {
            var bucket = "mybucket";

            var clientMock = new BulkPutClientMock(CreateJobResponse(
                "PUT",
                new[] {
                    new JobObjectList(Guid.Parse("7c53f8a2-d5e5-441a-9631-1821d35f1f0e"), 0, null, new[] {
                        new JobObject("baz", 12, 0, false),
                        new JobObject("bar", 12, 0, false)
                    }),
                    new JobObjectList(Guid.Parse("07dc0f5d-b4ea-4f91-a1cf-aaa684a56742"), 1, null, new[] {
                        new JobObject("foo", 12, 0, false)
                    })
                }
            ));

            var objectsToPut = new[] {
                new Ds3Object("foo", 12),
                new Ds3Object("bar", 12),
                new Ds3Object("baz", 12)
            };
            new Ds3ClientHelpers(clientMock.Object)
                .StartWriteJob(bucket, objectsToPut)
                //TODO.WithPartSize(4L) // We expect a real client to restrict the part size to within S3 spec (5mb - 5gb).
                .Transfer(key => HelpersForTest.StringToStream(key + " contents"));

            CollectionAssert.AreEquivalent(
                new[] {
                    new { BucketName = bucket, ObjectName = "bar", JobId = _jobId, Offset = 0L },
                    new { BucketName = bucket, ObjectName = "baz", JobId = _jobId, Offset = 0L },
                    new { BucketName = bucket, ObjectName = "foo", JobId = _jobId, Offset = 0L }
                },
                from initiate in clientMock.Initiates
                orderby initiate.ObjectName
                select new { initiate.BucketName, initiate.ObjectName, initiate.JobId, initiate.Offset }
            );

            CollectionAssert.AreEquivalent(
                from objectName in new[] { "bar", "baz", "foo" }
                let uploadId = clientMock.UploadIdFor(objectName)
                from part in new[] { 
                    new { PartNumber = 1, Content = objectName + " " },
                    new { PartNumber = 2, Content = "cont" },
                    new { PartNumber = 3, Content = "ents" },
                }
                select new { BucketName = bucket, ObjectName = objectName, part.PartNumber, UploadId = uploadId, part.Content },
                from part in clientMock.Parts
                orderby part.ObjectName, part.PartNumber
                select new { part.BucketName, part.ObjectName, part.PartNumber, part.UploadId, Content = clientMock.PartContents[Tuple.Create(part.ObjectName, part.PartNumber)] }
            );

            HelpersForTest.AssertCollectionsEqual(
                from objectName in new[] { "bar", "baz", "foo" }
                let etag = clientMock.ETagFor(objectName)
                select new
                {
                    BucketName = bucket,
                    ObjectName = objectName,
                    UploadId = clientMock.UploadIdFor(objectName),
                    Parts =
                        from partNumber in Enumerable.Range(1, 3)
                        select new UploadPart(partNumber, etag)
                },
                from complete in clientMock.Completes
                orderby complete.ObjectName
                select new { complete.BucketName, complete.ObjectName, complete.UploadId, complete.Parts },
                (expected, actual) =>
                {
                    Assert.AreEqual(expected.BucketName, actual.BucketName);
                    Assert.AreEqual(expected.ObjectName, actual.ObjectName);
                    Assert.AreEqual(expected.UploadId, actual.UploadId);
                    HelpersForTest.AssertCollectionsEqual(expected.Parts, actual.Parts, (expectedPart, actualPart) =>
                    {
                        Assert.AreEqual(expectedPart.PartNumber, actualPart.PartNumber);
                        Assert.AreEqual(expectedPart.Etag, actualPart.Etag);
                    });
                }
            );
        }

        [Test]
        public void TestWriteObjectsWhenMixedSinglepartAndMultipart()
        {
            var bucket = "mybucket";

            var clientMock = new BulkPutClientMock(CreateJobResponse(
                "PUT",
                new[] {
                    new JobObjectList(Guid.Parse("6076c8a7-80a7-46e3-a662-8e64cf33a772"), 0, null, new[] {
                        new JobObject("baz", 20, 0, false)
                    }),
                    new JobObjectList(Guid.Parse("9c234dfd-0ae1-402f-9fed-92674eb3b38a"), 1, null, new[] {
                        new JobObject("baz", 6, 20, false),
                        new JobObject("bar", 14, 0, false)
                    }),
                    new JobObjectList(Guid.Parse("b31c2971-690b-4a7c-a6e9-f75bff629b3a"), 2, null, new[] {
                        new JobObject("foo", 10, 0, false)
                    })
                }
            ));

            var objectsToPut = new[] {
                new Ds3Object("foo", 10),
                new Ds3Object("bar", 14),
                new Ds3Object("baz", 26)
            };
            var originalObjectContents = new Dictionary<string, string>
            {
                { "foo", "1234567890" },
                { "bar", "12345678901234" },
                { "baz", "12345678901234567890123456" }
            };
            new Ds3ClientHelpers(clientMock.Object)
                .StartWriteJob(bucket, objectsToPut)
                //TODO.WithPartSize(8L) // We expect a real client to restrict the part size to within S3 spec (5mb - 5gb).
                .Transfer(key => HelpersForTest.StringToStream(originalObjectContents[key]));

            CollectionAssert.AreEquivalent(
                new[] { new { BucketName = bucket, ObjectName = "baz", JobId = _jobId, Offset = 20L, Content = "123456" } },
                from put in clientMock.Puts
                select new { put.BucketName, put.ObjectName, put.JobId, put.Offset, Content = clientMock.PutContents[put.ObjectName] }
            );

            CollectionAssert.AreEquivalent(
                new[] {
                    new { BucketName = bucket, ObjectName = "bar", JobId = _jobId, Offset = 0L },
                    new { BucketName = bucket, ObjectName = "baz", JobId = _jobId, Offset = 0L },
                    new { BucketName = bucket, ObjectName = "foo", JobId = _jobId, Offset = 0L }
                },
                from initiate in clientMock.Initiates
                orderby initiate.ObjectName, initiate.Offset
                select new { initiate.BucketName, initiate.ObjectName, initiate.JobId, initiate.Offset }
            );

            CollectionAssert.AreEquivalent(
                new[] {
                    new { BucketName = bucket, ObjectName = "bar", PartNumber = 1, UploadId = clientMock.UploadIdFor("bar"), Content = "12345678" },
                    new { BucketName = bucket, ObjectName = "bar", PartNumber = 2, UploadId = clientMock.UploadIdFor("bar"), Content = "901234" },
                    new { BucketName = bucket, ObjectName = "baz", PartNumber = 1, UploadId = clientMock.UploadIdFor("baz"), Content = "12345678" },
                    new { BucketName = bucket, ObjectName = "baz", PartNumber = 2, UploadId = clientMock.UploadIdFor("baz"), Content = "90123456" },
                    new { BucketName = bucket, ObjectName = "baz", PartNumber = 3, UploadId = clientMock.UploadIdFor("baz"), Content = "7890" },
                    new { BucketName = bucket, ObjectName = "foo", PartNumber = 1, UploadId = clientMock.UploadIdFor("foo"), Content = "12345678" },
                    new { BucketName = bucket, ObjectName = "foo", PartNumber = 2, UploadId = clientMock.UploadIdFor("foo"), Content = "90" }
                },
                from part in clientMock.Parts
                let content = clientMock.PartContents[Tuple.Create(part.ObjectName, part.PartNumber)]
                orderby part.ObjectName, part.PartNumber, content
                select new { part.BucketName, part.ObjectName, part.PartNumber, part.UploadId, Content = content }
            );

            HelpersForTest.AssertCollectionsEqual(
                from completionData in new[] {
                    new { ObjectName = "bar", PartCount = 2 },
                    new { ObjectName = "baz", PartCount = 3 },
                    new { ObjectName = "foo", PartCount = 2 }
                }
                let objectName = completionData.ObjectName
                let etag = clientMock.ETagFor(objectName)
                orderby objectName, completionData.PartCount
                select new
                {
                    BucketName = bucket,
                    ObjectName = objectName,
                    UploadId = clientMock.UploadIdFor(objectName),
                    Parts =
                        from partNumber in Enumerable.Range(1, completionData.PartCount)
                        select new UploadPart(partNumber, etag)
                },
                from complete in clientMock.Completes
                orderby complete.ObjectName, complete.Parts.Count()
                select new { complete.BucketName, complete.ObjectName, complete.UploadId, complete.Parts },
                (expected, actual) =>
                {
                    Assert.AreEqual(expected.BucketName, actual.BucketName);
                    Assert.AreEqual(expected.ObjectName, actual.ObjectName);
                    Assert.AreEqual(expected.UploadId, actual.UploadId);
                    HelpersForTest.AssertCollectionsEqual(expected.Parts, actual.Parts, (expectedPart, actualPart) =>
                    {
                        Assert.AreEqual(expectedPart.PartNumber, actualPart.PartNumber);
                        Assert.AreEqual(expectedPart.Etag, actualPart.Etag);
                    });
                }
            );
        }

        private class BulkPutClientMock
        {
            public IDs3Client Object { get; private set; }

            public IList<PutObjectRequest> Puts { get; private set; }
            public IDictionary<string, string> PutContents { get; private set; }

            public IList<InitiateMultipartUploadRequest> Initiates { get; private set; }
            public IList<PutPartRequest> Parts { get; private set; }
            public IList<CompleteMultipartUploadRequest> Completes { get; private set; }
            public IDictionary<Tuple<string, int>, string> PartContents { get; private set; }

            private readonly object _recorderLock = new object();

            public BulkPutClientMock(JobResponse jobResponse)
            {
                this.Puts = new List<PutObjectRequest>();
                this.PutContents = new Dictionary<string, string>();

                this.Initiates = new List<InitiateMultipartUploadRequest>();
                this.Parts = new List<PutPartRequest>();
                this.Completes = new List<CompleteMultipartUploadRequest>();
                this.PartContents = new Dictionary<Tuple<string, int>, string>();

                this.Object = CreateMock(jobResponse);
            }

            public string UploadIdFor(string objectName)
            {
                return objectName + "/14f2dde1-06cc-41e8-8f34-093580f9e49a";
            }

            public string ETagFor(string objectName)
            {
                return objectName + "/5f735ed8-6842-4d14-ba78-d99cd1fbf24c";
            }

            private IDs3Client CreateMock(JobResponse jobResponse)
            {
                var ds3ClientMock = new Mock<IDs3Client>(MockBehavior.Strict);
                ds3ClientMock
                    .Setup(client => client.BulkPut(It.IsAny<BulkPutRequest>()))
                    .Returns(jobResponse);
                ds3ClientMock
                    .Setup(client => client.PutObject(It.IsAny<PutObjectRequest>()))
                    .Callback<PutObjectRequest>(request =>
                    {
                        lock (this._recorderLock)
                        {
                            this.PutContents.Add(request.ObjectName, HelpersForTest.StringFromStream(request.GetContentStream()));
                            this.Puts.Add(request);
                        }
                    });
                ds3ClientMock
                    .Setup(client => client.InitiateMultipartUpload(It.IsAny<InitiateMultipartUploadRequest>()))
                    .Returns<InitiateMultipartUploadRequest>(request => new InitiateMultipartUploadResponse(
                        request.BucketName,
                        request.ObjectName,
                        this.UploadIdFor(request.ObjectName)
                    ))
                    .Callback<InitiateMultipartUploadRequest>(request =>
                    {
                        lock (this._recorderLock)
                        {
                            this.Initiates.Add(request);
                        }
                    });
                ds3ClientMock
                    .Setup(client => client.PutPart(It.IsAny<PutPartRequest>()))
                    .Returns<PutPartRequest>(request => new PutPartResponse(this.ETagFor(request.ObjectName)))
                    .Callback<PutPartRequest>(request =>
                    {
                        lock (this._recorderLock)
                        {
                            this.PartContents.Add(Tuple.Create(request.ObjectName, request.PartNumber), HelpersForTest.StringFromStream(request.GetContentStream()));
                            this.Parts.Add(request);
                        }
                    });
                ds3ClientMock
                    .Setup(client => client.CompleteMultipartUpload(It.IsAny<CompleteMultipartUploadRequest>()))
                    .Returns<CompleteMultipartUploadRequest>(request => new CompleteMultipartUploadResponse(
                        string.Format("http://dummy-server/{0}/{1}", request.BucketName, request.ObjectName),
                        request.BucketName,
                        request.ObjectName,
                        request.ObjectName + "/eb7e724e-ba62-407b-856f-11fe08e949e2"
                    ))
                    .Callback<CompleteMultipartUploadRequest>(request =>
                    {
                        lock (this._recorderLock)
                        {
                            this.Completes.Add(request);
                        }
                    });
                var ds3ClientFactoryMock = new Mock<IDs3ClientFactory>(MockBehavior.Strict);
                ds3ClientFactoryMock
                    .Setup(factory => factory.GetClientForNodeId(It.IsAny<Guid?>()))
                    .Returns(ds3ClientMock.Object);
                ds3ClientMock
                    .Setup(client => client.BuildFactory(It.IsAny<IEnumerable<Node>>()))
                    .Returns(ds3ClientFactoryMock.Object);
                return ds3ClientMock.Object;
            }
        }

        private static readonly Guid _jobId = Guid.Parse("3ad595b2-38cb-447d-9e1d-a1125ba19f33");

        private static JobResponse CreateJobResponse(string requestType, IEnumerable<JobObjectList> objectLists)
        {
            return new JobResponse(
                bucketName: "mybucket",
                jobId: _jobId,
                priority: "NORMAL",
                requestType: requestType,
                startDate: DateTime.Parse("2014-07-09T19:41:34.000Z"),
                nodes: Enumerable.Empty<Node>(),
                objectLists: objectLists
            );
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
