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

using Moq;
using NUnit.Framework;

using Ds3;
using Ds3.Calls;
using Ds3.Helpers;
using Ds3.Models;
using System.Text;

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
                .Returns(CreateJobResponse());
            ds3ClientMock
                .Setup(client => client.GetObject(It.IsAny<GetObjectRequest>()))
                .Returns<GetObjectRequest>(request =>
                {
                    HelpersForTest.StringToStream(request.ObjectName + " contents").CopyTo(request.DestinationStream);
                    return new GetObjectResponse(new Dictionary<string, string>());
                });
            var ds3ClientFactoryMock = new Mock<IDs3ClientFactory>(MockBehavior.Strict);
            ds3ClientFactoryMock
                .Setup(factory => factory.GetClientForNodeId(It.IsAny<Guid?>()))
                .Returns(ds3ClientMock.Object);
            ds3ClientMock
                .Setup(client => client.BuildFactory(It.IsAny<IEnumerable<Node>>()))
                .Returns(ds3ClientFactoryMock.Object);

            var objectsGotten = new List<string>();

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
                    objectsGotten.Add(key);
                    return stream;
                });
            foreach (var kvp in streams)
            {
                Assert.AreEqual(kvp.Key + " contents", kvp.Value.Result);
            }

            CollectionAssert.AreEquivalent(new[] { "baz", "bar", "foo" }, objectsGotten);
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
        public void TestWriteObjects()
        {
            var objectsPut = new List<string>();
            var objectContentsPut = new Dictionary<string, string>();

            var ds3ClientMock = new Mock<IDs3Client>(MockBehavior.Strict);
            ds3ClientMock
                .Setup(client => client.BulkPut(It.IsAny<BulkPutRequest>()))
                .Returns(CreateJobResponse());
            ds3ClientMock
                .Setup(client => client.PutObject(It.IsAny<PutObjectRequest>()))
                .Callback<PutObjectRequest>(request => {
                    objectsPut.Add(request.ObjectName);
                    objectContentsPut.Add(request.ObjectName, HelpersForTest.StringFromStream(request.GetContentStream()));
                })
                .Returns(new PutObjectResponse());
            var ds3ClientFactoryMock = new Mock<IDs3ClientFactory>(MockBehavior.Strict);
            ds3ClientFactoryMock
                .Setup(factory => factory.GetClientForNodeId(It.IsAny<Guid?>()))
                .Returns(ds3ClientMock.Object);
            ds3ClientMock
                .Setup(client => client.BuildFactory(It.IsAny<IEnumerable<Node>>()))
                .Returns(ds3ClientFactoryMock.Object);

            var objectsToPut = new[] {
                new Ds3Object("foo", 12),
                new Ds3Object("bar", 12),
                new Ds3Object("baz", 12)
            };
            new Ds3ClientHelpers(ds3ClientMock.Object)
                .StartWriteJob("mybucket", objectsToPut)
                .Transfer(key => HelpersForTest.StringToStream(key + " contents"));

            var expectedkeys = new[] { "baz", "bar", "foo" };
            CollectionAssert.AreEquivalent(expectedkeys, objectsPut);
            CollectionAssert.AreEqual(new[] { "baz contents", "bar contents", "foo contents" }, expectedkeys.Select(key => objectContentsPut[key]));
        }

        private static JobResponse CreateJobResponse()
        {
            return new JobResponse(
                "mybucket",
                Guid.Parse("3ad595b2-38cb-447d-9e1d-a1125ba19f33"),
                Enumerable.Empty<Node>(),
                new Ds3ObjectList[] {
                    new Ds3ObjectList(0, null, new[] { new JobObject("baz", new[] { new Blob(Guid.Parse("3748ee9e-5633-42c9-b97d-ed6bf4c0166d"), 12, 0) }) }),
                    new Ds3ObjectList(1, null, new[] { new JobObject("bar", new[] { new Blob(Guid.Parse("7243f517-39dc-4ace-a8ff-42c10721219f"), 12, 0) }) }),
                    new Ds3ObjectList(2, null, new[] { new JobObject("foo", new[] { new Blob(Guid.Parse("071837f5-342b-42ec-a177-5dc0bcacf4c4"), 12, 0) }) })
                }
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
                metadata: new Dictionary<string, string>()
            );
        }

        private static Ds3ObjectInfo BuildDs3Object(string key, string eTag, string lastModified, long size)
        {
            var owner = new Owner("person@spectralogic.com", "75aa57f09aa0c8caeab4f8c24e99d10f8e7faeebf76c078efc7c6caea54ba06a");
            return new Ds3ObjectInfo(key, size, owner, eTag, "STANDARD", DateTime.Parse(lastModified));
        }
    }
}
