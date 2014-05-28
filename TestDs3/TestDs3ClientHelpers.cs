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
using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Net;
using NUnit.Framework.Constraints;

namespace TestDs3
{
    [TestFixture]
    public class TestDs3ClientHelpers
    {
        [Test]
        public void TestReadObjects()
        {
            var ds3ClientMock = new Mock<IDs3Client>();
            ds3ClientMock
                .Setup(client => client.BulkGet(It.IsAny<BulkGetRequest>()))
                .Returns(new StubBulkGetResponse(_bulkObjectList));
            ds3ClientMock
                .Setup(client => client.GetObject(It.IsAny<GetObjectRequest>()))
                .Returns<GetObjectRequest>(request => new StubGetObjectResponse(request.ObjectName + " contents"));

            var objectsGotten = new List<string>();

            var objectsToGet = new[] {
                new Ds3Object("foo"),
                new Ds3Object("bar"),
                new Ds3Object("baz")
            };
            new Ds3ClientHelpers(ds3ClientMock.Object)
                .StartReadJob("mybucket", objectsToGet)
                .Read((ds3Object, contents) => {
                    Assert.AreEqual(ds3Object.Name + " contents", HelpersForTest.StringFromStream(contents));
                    objectsGotten.Add(ds3Object.Name);
                });

            CollectionAssert.AreEqual(new[] { "baz", "bar", "foo" }, objectsGotten);
        }

        [Test]
        public void TestWriteObjects()
        {
            var objectsPut = new List<string>();
            var objectContentsPut = new List<string>();

            var ds3ClientMock = new Mock<IDs3Client>();
            ds3ClientMock
                .Setup(client => client.BulkPut(It.IsAny<BulkPutRequest>()))
                .Returns(new StubBulkPutResponse(_bulkObjectList));
            ds3ClientMock
                .Setup(client => client.PutObject(It.IsAny<PutObjectRequest>()))
                .Callback<PutObjectRequest>(request => {
                    objectsPut.Add(request.ObjectName);
                    objectContentsPut.Add(HelpersForTest.StringFromStream(request.GetContentStream()));
                })
                .Returns(new StubPutObjectResponse());

            var objectsToPut = new[] {
                new Ds3Object("foo", 12),
                new Ds3Object("bar", 12),
                new Ds3Object("baz", 12)
            };
            new Ds3ClientHelpers(ds3ClientMock.Object)
                .StartWriteJob("mybucket", objectsToPut)
                .Write(ds3Object => HelpersForTest.StringToStream(ds3Object.Name + " contents"));

            CollectionAssert.AreEqual(new[] { "baz", "bar", "foo" }, objectsPut);
            CollectionAssert.AreEqual(new[] { "baz contents", "bar contents", "foo contents" }, objectContentsPut);
        }

        [Test]
        public void TestListObjects()
        {
            var ds3ClientMock = new Mock<IDs3Client>();
            ds3ClientMock
                .Setup(client => client.GetBucket(It.IsAny<GetBucketRequest>()))
                .Returns(new Queue<GetBucketResponse>(new[] {
                    new StubGetBucketResponse(0),
                    new StubGetBucketResponse(1)
                }).Dequeue);

            var objects = new Ds3ClientHelpers(ds3ClientMock.Object).ListObjects("mybucket").ToList();

            Assert.AreEqual(3, objects.Count);
            checkContents(objects[0], "foo", "2cde576e5f5a613e6cee466a681f4929", "2009-10-12T17:50:30.000Z", 12);
            checkContents(objects[1], "bar", "f3f98ff00be128139332bcf4b772be43", "2009-10-14T17:50:31.000Z", 12);
            checkContents(objects[2], "baz", "802d45fcb9a3f7d00f1481362edc0ec9", "2009-10-18T17:50:35.000Z", 12);
        }

        private static void checkContents(
                Ds3Object contents,
                string key,
                string eTag,
                string lastModified,
                long size) {
            Assert.AreEqual(key, contents.Name);
            Assert.AreEqual(eTag, contents.Etag);
            Assert.AreEqual(DateTime.Parse(lastModified), contents.LastModified);
            Assert.AreEqual(size, contents.Size);
        }

        private static IEnumerable<Ds3ObjectList> _bulkObjectList = new Ds3ObjectList[] {
            new Ds3ObjectList("192.168.56.100", new[] { new Ds3Object("baz", 12) }),
            new Ds3ObjectList("192.168.56.101", new[] { new Ds3Object("bar", 12) }),
            new Ds3ObjectList("192.168.56.100", new[] { new Ds3Object("foo", 12) })
        };
    }

    class StubBulkGetResponse : BulkGetResponse
    {
        private readonly IEnumerable<Ds3ObjectList> _objectLists;

        public StubBulkGetResponse(IEnumerable<Ds3ObjectList> objectLists)
            : base(new DummyWebResponse())
        {
            this._objectLists = objectLists;
        }

        protected override void ProcessResponse()
        {
        }

        public override IEnumerable<Ds3ObjectList> ObjectLists
        {
            get { return this._objectLists; }
        }
    }

    class StubBulkPutResponse : BulkPutResponse
    {
        private readonly IEnumerable<Ds3ObjectList> _objectLists;

        public StubBulkPutResponse(IEnumerable<Ds3ObjectList> objectLists)
            : base(new DummyWebResponse())
        {
            this._objectLists = objectLists;
        }

        protected override void ProcessResponse()
        {
        }

        public override IEnumerable<Ds3ObjectList> ObjectLists
        {
            get { return this._objectLists; }
        }
    }

    class StubGetObjectResponse : GetObjectResponse
    {
        private Stream _contents;

        public StubGetObjectResponse(string contents)
            : base(new DummyWebResponse())
        {
            this._contents = HelpersForTest.StringToStream(contents);
        }

        protected override void ProcessResponse()
        {
        }

        public override Stream Contents
        {
            get
            {
                return this._contents;
            }
        }
    }

    class StubPutObjectResponse : PutObjectResponse
    {
        public StubPutObjectResponse()
            : base(new DummyWebResponse())
        {
        }

        protected override void ProcessResponse()
        {
        }
    }

    class StubGetBucketResponse : GetBucketResponse
    {
        private readonly List<Ds3Object> _ds3Objects;
        private readonly string _marker;
        private readonly string _nextMarker;
        private readonly bool _isTruncated;

        public override string Name { get { return "mybucket"; } }
        public override string Prefix { get { return ""; } }
        public override string Marker { get { return _marker; } }
        public override int MaxKeys { get { return 2; } }
        public override bool IsTruncated { get { return _isTruncated; } }
        public override string Delimiter { get { return ""; } }
        public override string NextMarker { get { return _nextMarker; } }
        public override DateTime CreationDate { get { return DateTime.Now; } }
        public override List<Ds3Object> Objects { get { return _ds3Objects; } }

        public StubGetBucketResponse(int invocationIndex)
            : base(new DummyWebResponse())
        {
            switch (invocationIndex)
            {
                case 0:
                    this._ds3Objects = new List<Ds3Object> {
                        BuildDs3Object("foo", "2cde576e5f5a613e6cee466a681f4929", "2009-10-12T17:50:30.000Z", 12),
                        BuildDs3Object("bar", "f3f98ff00be128139332bcf4b772be43", "2009-10-14T17:50:31.000Z", 12)
                    };
                    this._marker = "";
                    this._nextMarker = "baz";
                    this._isTruncated = true;
                    break;
                case 1:
                    this._ds3Objects = new List<Ds3Object> {
                        BuildDs3Object("baz", "802d45fcb9a3f7d00f1481362edc0ec9", "2009-10-18T17:50:35.000Z", 12)
                    };
                    this._marker = "baz";
                    this._nextMarker = "";
                    this._isTruncated = false;
                    break;
                default:
                    throw new NotSupportedException();
            }
        }

        private static Ds3Object BuildDs3Object(string key, string eTag, string lastModified, long size)
        {
            var owner = new Owner("person@spectralogic.com", "75aa57f09aa0c8caeab4f8c24e99d10f8e7faeebf76c078efc7c6caea54ba06a");
            return new Ds3Object(key, size, owner, eTag, "STANDARD", DateTime.Parse(lastModified));
        }

        protected override void ProcessResponse()
        {
        }
    }

    class DummyWebResponse : IWebResponse
    {
        public Stream GetResponseStream()
        {
            throw new NotSupportedException();
        }

        public HttpStatusCode StatusCode
        {
            get { throw new NotSupportedException(); }
        }

        public void Dispose()
        {
            // Do nothing.
        }
    }
}
