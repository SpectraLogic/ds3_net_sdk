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
using Ds3.Models;
using Ds3.Runtime;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using TestDs3.Lang;

namespace TestDs3
{
    [TestFixture]
    public class TestDs3Client
    {
        private static readonly IDictionary<string, string> _emptyHeaders = new Dictionary<string, string>();
        private static readonly IDictionary<string, string> _emptyQueryParams = new Dictionary<string, string>();
        private const string JobResponseResourceName = "TestDs3.TestData.ResultingMasterObjectList.xml";
        private const string AllocateJobChunkResponseResourceName = "TestDs3.TestData.AllocateJobChunkResponse.xml";
        private const string GetAvailableJobChunksResponseResourceName = "TestDs3.TestData.GetAvailableJobChunksResponse.xml";
        private const string EmptyGetAvailableJobChunksResponseResourceName = "TestDs3.TestData.EmptyGetAvailableJobChunksResponse.xml";
        private const string GetPhysicalPlacementResponseResourceName = "TestDs3.TestData.GetPhysicalPlacementResponse.xml";
        private const string GetPhysicalPlacementFullDetailsResponseResourceName = "TestDs3.TestData.GetPhysicalPlacementFullDetailsResponse.xml";

        [Test]
        public void TestGetService()
        {
            var responseContent = "<ListAllMyBucketsResult><Owner><ID>ryan</ID><DisplayName>ryan</DisplayName></Owner><Buckets><Bucket><Name>testBucket2</Name><CreationDate>2013-12-11T23:20:09</CreationDate></Bucket><Bucket><Name>bulkTest</Name><CreationDate>2013-12-11T23:20:09</CreationDate></Bucket><Bucket><Name>bulkTest1</Name><CreationDate>2013-12-11T23:20:09</CreationDate></Bucket><Bucket><Name>bulkTest2</Name><CreationDate>2013-12-11T23:20:09</CreationDate></Bucket><Bucket><Name>bulkTest3</Name><CreationDate>2013-12-11T23:20:09</CreationDate></Bucket><Bucket><Name>bulkTest4</Name><CreationDate>2013-12-11T23:20:09</CreationDate></Bucket><Bucket><Name>bulkTest5</Name><CreationDate>2013-12-11T23:20:09</CreationDate></Bucket><Bucket><Name>bulkTest6</Name><CreationDate>2013-12-11T23:20:09</CreationDate></Bucket><Bucket><Name>testBucket3</Name><CreationDate>2013-12-11T23:20:09</CreationDate></Bucket><Bucket><Name>testBucket1</Name><CreationDate>2013-12-11T23:20:09</CreationDate></Bucket><Bucket><Name>testbucket</Name><CreationDate>2013-12-11T23:20:09</CreationDate></Bucket></Buckets></ListAllMyBucketsResult>";
            var expectedBuckets = new[] {
                new { Key = "testBucket2",  CreationDate = "2013-12-11T23:20:09" },
                new { Key = "bulkTest",     CreationDate = "2013-12-11T23:20:09" },
                new { Key = "bulkTest1",    CreationDate = "2013-12-11T23:20:09" },
                new { Key = "bulkTest2",    CreationDate = "2013-12-11T23:20:09" },
                new { Key = "bulkTest3",    CreationDate = "2013-12-11T23:20:09" },
                new { Key = "bulkTest4",    CreationDate = "2013-12-11T23:20:09" },
                new { Key = "bulkTest5",    CreationDate = "2013-12-11T23:20:09" },
                new { Key = "bulkTest6",    CreationDate = "2013-12-11T23:20:09" },
                new { Key = "testBucket3",  CreationDate = "2013-12-11T23:20:09" },
                new { Key = "testBucket1",  CreationDate = "2013-12-11T23:20:09" },
                new { Key = "testbucket",   CreationDate = "2013-12-11T23:20:09" }
            };

            var response = MockNetwork
                .Expecting(HttpVerb.GET, "/", _emptyQueryParams, "")
                .Returning(HttpStatusCode.OK, responseContent, _emptyHeaders)
                .AsClient
                .GetService(new GetServiceRequest());
            Assert.AreEqual("ryan", response.Owner.DisplayName);
            Assert.AreEqual("ryan", response.Owner.Id);

            Assert.AreEqual(expectedBuckets.Length, response.Buckets.Count);
            for (var i = 0; i < expectedBuckets.Length; i++)
            {
                Assert.AreEqual(expectedBuckets[i].Key, response.Buckets[i].Name);
                Assert.AreEqual(expectedBuckets[i].CreationDate, response.Buckets[i].CreationDate);
            }
        }

        [Test]
        [ExpectedException(typeof(Ds3BadStatusCodeException))]
        public void TestGetBadService()
        {
            MockNetwork
                .Expecting(HttpVerb.GET, "/", _emptyQueryParams, "")
                .Returning(HttpStatusCode.BadRequest, "", _emptyHeaders)
                .AsClient
                .GetService(new GetServiceRequest());
        }

        [Test]
        [ExpectedException(typeof(Ds3BadResponseException))]
        public void TestGetWorseService()
        {
            MockNetwork
                .Expecting(HttpVerb.GET, "/", _emptyQueryParams, "")
                .Returning(HttpStatusCode.OK, "", _emptyHeaders)
                .AsClient
                .GetService(new GetServiceRequest());
        }

        [Test]
        public void TestGetBucket()
        {
            var xmlResponse = "<ListBucketResult><Name>remoteTest16</Name><Prefix/><Marker/><MaxKeys>1000</MaxKeys><IsTruncated>false</IsTruncated><Contents><Key>user/hduser/gutenberg/20417.txt.utf-8</Key><LastModified>2014-01-03T13:26:47.000Z</LastModified><ETag>NOTRETURNED</ETag><Size>674570</Size><StorageClass>STANDARD</StorageClass><Owner><ID>ryan</ID><DisplayName>ryan</DisplayName></Owner></Contents><Contents><Key>user/hduser/gutenberg/5000.txt.utf-8</Key><LastModified>2014-01-03T13:26:47.000Z</LastModified><ETag>NOTRETURNED</ETag><Size>1423803</Size><StorageClass>STANDARD</StorageClass><Owner><ID>ryan</ID><DisplayName>ryan</DisplayName></Owner></Contents><Contents><Key>user/hduser/gutenberg/4300.txt.utf-8</Key><LastModified>2014-01-03T13:26:47.000Z</LastModified><ETag>NOTRETURNED</ETag><Size>1573150</Size><StorageClass>STANDARD</StorageClass><Owner><ID>ryan</ID><DisplayName>ryan</DisplayName></Owner></Contents></ListBucketResult>";
            var expected = new {
                Name = "remoteTest16",
                Prefix = "",
                Marker = "",
                MaxKeys = 1000,
                IsTruncated = false,
                Objects = new[] {
                    new {
                        Key = "user/hduser/gutenberg/20417.txt.utf-8",
                        LastModified = DateTime.Parse("2014-01-03T13:26:47.000Z"),
                        ETag = "NOTRETURNED",
                        Size = 674570,
                        StorageClass = "STANDARD",
                        Owner = new { ID = "ryan", DisplayName = "ryan" }
                    },
                    new {
                        Key = "user/hduser/gutenberg/5000.txt.utf-8",
                        LastModified = DateTime.Parse("2014-01-03T13:26:47.000Z"),
                        ETag = "NOTRETURNED",
                        Size = 1423803,
                        StorageClass = "STANDARD",
                        Owner = new { ID = "ryan", DisplayName = "ryan" }
                    },
                    new {
                        Key = "user/hduser/gutenberg/4300.txt.utf-8",
                        LastModified = DateTime.Parse("2014-01-03T13:26:47.000Z"),
                        ETag = "NOTRETURNED",
                        Size = 1573150,
                        StorageClass = "STANDARD",
                        Owner = new { ID = "ryan", DisplayName = "ryan" }
                    }
                }
            };

            var response = MockNetwork
                .Expecting(HttpVerb.GET, "/remoteTest16", _emptyQueryParams, "")
                .Returning(HttpStatusCode.OK, xmlResponse, _emptyHeaders)
                .AsClient
                .GetBucket(new GetBucketRequest("remoteTest16"));
            Assert.AreEqual(expected.Name, response.Name);
            Assert.AreEqual(expected.Prefix, response.Prefix);
            Assert.AreEqual(expected.Marker, response.Marker);
            Assert.AreEqual(expected.MaxKeys, response.MaxKeys);
            Assert.AreEqual(expected.IsTruncated, response.IsTruncated);

            var responseObjects = response.Objects.ToList();
            Assert.AreEqual(expected.Objects.Length, responseObjects.Count);
            for (var i = 0; i < expected.Objects.Length; i++)
            {
                Assert.AreEqual(expected.Objects[i].Key, responseObjects[i].Name);
                Assert.AreEqual(expected.Objects[i].LastModified, responseObjects[i].LastModified);
                Assert.AreEqual(expected.Objects[i].ETag, responseObjects[i].Etag);
                Assert.AreEqual(expected.Objects[i].Size, responseObjects[i].Size);
                Assert.AreEqual(expected.Objects[i].StorageClass, responseObjects[i].StorageClass);
                Assert.AreEqual(expected.Objects[i].Owner.ID, responseObjects[i].Owner.Id);
                Assert.AreEqual(expected.Objects[i].Owner.DisplayName, responseObjects[i].Owner.DisplayName);
            }
        }

        [Test]
        public void TestPutBucket()
        {
            MockNetwork
                .Expecting(HttpVerb.PUT, "/bucketName", _emptyQueryParams, "")
                .Returning(HttpStatusCode.OK, "", _emptyHeaders)
                .AsClient
                .PutBucket(new PutBucketRequest("bucketName"));
        }

        [Test]
        public void TestDeleteBucket()
        {
            MockNetwork
                .Expecting(HttpVerb.DELETE, "/bucketName", _emptyQueryParams, "")
                .Returning(HttpStatusCode.NoContent, "", _emptyHeaders)
                .AsClient
                .DeleteBucket(new DeleteBucketRequest("bucketName"));
        }

        [Test]
        public void TestDeleteObject()
        {
            MockNetwork
                .Expecting(HttpVerb.DELETE, "/bucketName/my/file.txt", _emptyQueryParams, "")
                .Returning(HttpStatusCode.NoContent, "", _emptyHeaders)
                .AsClient
                .DeleteObject(new DeleteObjectRequest("bucketName", "my/file.txt"));
        }

        [Test]
        [ExpectedException(typeof(Ds3BadStatusCodeException))]
        public void TestGetBadBucket()
        {
            MockNetwork
                .Expecting(HttpVerb.GET, "/bucketName", _emptyQueryParams, "")
                .Returning(HttpStatusCode.BadRequest, "", _emptyHeaders)
                .AsClient
                .GetBucket(new GetBucketRequest("bucketName"));
        }

        [Test]
        public void TestGetObject()
        {
            var stringResponse = "object contents";

            using (var memoryStream = new MemoryStream())
            {
                var jobId = Guid.Parse("6f7a31c1-7ddc-46d3-975d-be26d9878493");
                var offset = 10L;
                var expectedQueryParams = new Dictionary<string, string>
                {
                    { "job", "6f7a31c1-7ddc-46d3-975d-be26d9878493" },
                    { "offset", "10" }
                };
                MockNetwork
                    .Expecting(HttpVerb.GET, "/bucketName/object", expectedQueryParams, "")
                    .Returning(HttpStatusCode.OK, stringResponse, _emptyHeaders)
                    .AsClient
                    .GetObject(new GetObjectRequest("bucketName", "object", jobId, offset, memoryStream));
                memoryStream.Position = 0L;
                using (var reader = new StreamReader(memoryStream))
                {
                    Assert.AreEqual(stringResponse, reader.ReadToEnd());
                }
            }
        }

        [Test]
        public void TestPutObject()
        {
            var stringRequest = "object content";
            var jobId = Guid.Parse("e5d4adce-e170-4915-ba04-595fab30df81");
            var offset = 10L;
            var expectedQueryParams = new Dictionary<string, string>
            {
                { "job", "e5d4adce-e170-4915-ba04-595fab30df81" },
                { "offset", "10" }
            };
            MockNetwork
                .Expecting(HttpVerb.PUT, "/bucketName/object", expectedQueryParams, stringRequest)
                .Returning(HttpStatusCode.OK, stringRequest, _emptyHeaders)
                .AsClient
                .PutObject(new PutObjectRequest("bucketName", "object", jobId, offset, HelpersForTest.StringToStream(stringRequest)));
        }

        [Test]
        public void TestBulkPut()
        {
            RunBulkTest("start_bulk_put", (client, objects) => client.BulkPut(new BulkPutRequest("bucket8192000000", objects)), _emptyQueryParams);
        }

        [Test]
        public void TestBulkPutWithMaxBlobSize()
        {
            RunBulkTest(
                "start_bulk_put",
                (client, objects) => client.BulkPut(new BulkPutRequest("bucket8192000000", objects).WithMaxBlobSize(1048576L)),
                new Dictionary<string, string> { { "max_upload_size", "1048576" } }
            );
        }

        [Test]
        public void TestBulkGet()
        {
            RunBulkTest("start_bulk_get", (client, objects) => client.BulkGet(new BulkGetRequest("bucket8192000000", objects)), _emptyQueryParams);
        }

        private void RunBulkTest(
            string operation,
            Func<IDs3Client, List<Ds3Object>, JobResponse> makeCall,
            IDictionary<string, string> additionalQueryParams)
        {
            var files = new[] {
                new { Key = "client00obj000000-8000000", Size = 8192000000L },
                new { Key = "client00obj000001-8000000", Size = 8192000000L },
                new { Key = "client00obj000002-8000000", Size = 8192000000L },
                new { Key = "client00obj000003-8000000", Size = 8192000000L },
                new { Key = "client00obj000004-8000000", Size = 8192000000L },
                new { Key = "client00obj000005-8000000", Size = 8192000000L },
                new { Key = "client00obj000006-8000000", Size = 8192000000L },
                new { Key = "client00obj000007-8000000", Size = 8192000000L },
                new { Key = "client00obj000008-8000000", Size = 8192000000L },
                new { Key = "client00obj000009-8000000", Size = 8192000000L }
            };

            var stringRequest = operation == "start_bulk_get"
                ? "<Objects><Object Name=\"client00obj000000-8000000\" /><Object Name=\"client00obj000001-8000000\" /><Object Name=\"client00obj000002-8000000\" /><Object Name=\"client00obj000003-8000000\" /><Object Name=\"client00obj000004-8000000\" /><Object Name=\"client00obj000005-8000000\" /><Object Name=\"client00obj000006-8000000\" /><Object Name=\"client00obj000007-8000000\" /><Object Name=\"client00obj000008-8000000\" /><Object Name=\"client00obj000009-8000000\" /></Objects>"
                : "<Objects><Object Name=\"client00obj000000-8000000\" Size=\"8192000000\" /><Object Name=\"client00obj000001-8000000\" Size=\"8192000000\" /><Object Name=\"client00obj000002-8000000\" Size=\"8192000000\" /><Object Name=\"client00obj000003-8000000\" Size=\"8192000000\" /><Object Name=\"client00obj000004-8000000\" Size=\"8192000000\" /><Object Name=\"client00obj000005-8000000\" Size=\"8192000000\" /><Object Name=\"client00obj000006-8000000\" Size=\"8192000000\" /><Object Name=\"client00obj000007-8000000\" Size=\"8192000000\" /><Object Name=\"client00obj000008-8000000\" Size=\"8192000000\" /><Object Name=\"client00obj000009-8000000\" Size=\"8192000000\" /></Objects>";
            var stringResponse = ReadResource(JobResponseResourceName);

            var inputObjects = files.Select(f => new Ds3Object(f.Key, f.Size)).ToList();

            var queryParams = new Dictionary<string, string>() { { "operation", operation } };
            foreach (var kvp in additionalQueryParams)
            {
                queryParams.Add(kvp.Key, kvp.Value);
            }
            var client = MockNetwork
                .Expecting(HttpVerb.PUT, "/_rest_/bucket/bucket8192000000", queryParams, stringRequest)
                .Returning(HttpStatusCode.OK, stringResponse, _emptyHeaders)
                .AsClient;
            CheckJobResponse(makeCall(client, inputObjects));
        }

        [Test]
        public void TestGetJob()
        {
            var stringResponse = ReadResource(JobResponseResourceName);
            var client = MockNetwork
                .Expecting(HttpVerb.GET, "/_rest_/job/1a85e743-ec8f-4789-afec-97e587a26936", _emptyQueryParams, "")
                .Returning(HttpStatusCode.OK, stringResponse, _emptyHeaders)
                .AsClient;
            CheckJobResponse(client.GetJob(new GetJobRequest(Guid.Parse("1a85e743-ec8f-4789-afec-97e587a26936"))));
        }

        private static string ReadResource(string resourceName)
        {
            using (var xmlFile = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(xmlFile))
                return reader.ReadToEnd();
        }

        private static void CheckJobResponse(JobResponse response)
        {
            var expectedNodes = new[] {
                new {
                    EndPoint="10.1.18.12",
                    HttpPort=(int?)80,
                    HttpsPort=(int?)443,
                    Id=Guid.Parse("a02053b9-0147-11e4-8d6a-002590c1177c")
                },
                new {
                    EndPoint="10.1.18.13",
                    HttpPort=(int?)null,
                    HttpsPort=(int?)443,
                    Id=Guid.Parse("4ecebf6f-bfd2-40a8-82a6-32fd684fd500")
                },
                new {
                    EndPoint="10.1.18.14",
                    HttpPort=(int?)80,
                    HttpsPort=(int?)null,
                    Id=Guid.Parse("4d5b6669-76f0-49f9-bc2a-9280f40cafa7")
                },
            };
            var expectedObjectLists = new[] {
                new {
                    ChunkNumber=0L,
                    ChunkId = Guid.Parse("f58370c2-2538-4e78-a9f8-e4d2676bdf44"),
                    NodeId=(Guid?)Guid.Parse("a02053b9-0147-11e4-8d6a-002590c1177c"),
                    Objects = new[] {
                        new { Name="client00obj000004-8000000", Length=5368709120L, Offset=0L },
                        new { Name="client00obj000004-8000000", Length=2823290880L, Offset=5368709120L },
                        new { Name="client00obj000003-8000000", Length=2823290880L, Offset=5368709120L },
                        new { Name="client00obj000003-8000000", Length=5368709120L, Offset=0L },
                        new { Name="client00obj000002-8000000", Length=5368709120L, Offset=0L },
                        new { Name="client00obj000002-8000000", Length=2823290880L, Offset=5368709120L },
                        new { Name="client00obj000005-8000000", Length=5368709120L, Offset=0L },
                        new { Name="client00obj000005-8000000", Length=2823290880L, Offset=5368709120L },
                        new { Name="client00obj000006-8000000", Length=5368709120L, Offset=0L },
                        new { Name="client00obj000006-8000000", Length=2823290880L, Offset=5368709120L },
                        new { Name="client00obj000000-8000000", Length=5368709120L, Offset=0L },
                        new { Name="client00obj000000-8000000", Length=2823290880L, Offset=5368709120L },
                        new { Name="client00obj000001-8000000", Length=5368709120L, Offset=0L },
                        new { Name="client00obj000001-8000000", Length=2823290880L, Offset=5368709120L },
                    },
                },
                new {
                    ChunkNumber=1L,
                    ChunkId = Guid.Parse("4137d768-25bb-4942-9d36-b92dfbe75e01"),
                    NodeId=(Guid?)null,
                    Objects = new[] {
                        new { Name="client00obj000008-8000000", Length=2823290880L, Offset=5368709120L },
                        new { Name="client00obj000008-8000000", Length=5368709120L, Offset=0L },
                        new { Name="client00obj000009-8000000", Length=2823290880L, Offset=5368709120L },
                        new { Name="client00obj000009-8000000", Length=5368709120L, Offset=0L },
                        new { Name="client00obj000007-8000000", Length=5368709120L, Offset=0L },
                        new { Name="client00obj000007-8000000", Length=2823290880L, Offset=5368709120L },
                    }
                }
            };
            HelpersForTest.AssertCollectionsEqual(expectedNodes, response.Nodes, (expectedNode, actualNode) =>
            {
                Assert.AreEqual(expectedNode.Id, actualNode.Id);
                Assert.AreEqual(expectedNode.EndPoint, actualNode.EndPoint);
                Assert.AreEqual(expectedNode.HttpPort, actualNode.HttpPort);
                Assert.AreEqual(expectedNode.HttpsPort, actualNode.HttpsPort);
            });
            HelpersForTest.AssertCollectionsEqual(expectedObjectLists, response.ObjectLists, (expectedObjectList, actualObjectList) =>
            {
                Assert.AreEqual(expectedObjectList.ChunkNumber, actualObjectList.ChunkNumber);
                Assert.AreEqual(expectedObjectList.ChunkId, actualObjectList.ChunkId);
                Assert.AreEqual(expectedObjectList.NodeId, actualObjectList.NodeId);
                HelpersForTest.AssertCollectionsEqual(expectedObjectList.Objects, actualObjectList.Objects, (expectedObject, actualObject) =>
                {
                    Assert.AreEqual(expectedObject.Name, actualObject.Name);
                    Assert.AreEqual(expectedObject.Length, actualObject.Length);
                    Assert.AreEqual(expectedObject.Offset, actualObject.Offset);
                });
            });
        }

        [Test]
        public void TestGetJobList()
        {
            var responseContent = "<Jobs><Job BucketName=\"bucketName\" JobId=\"a4a586a1-cb80-4441-84e2-48974e982d51\" Priority=\"NORMAL\" RequestType=\"PUT\" StartDate=\"2014-05-22T18:24:00.000Z\"/></Jobs>";
            var client = MockNetwork
                .Expecting(HttpVerb.GET, "/_rest_/job", new Dictionary<string, string>(), "")
                .Returning(HttpStatusCode.OK, responseContent, _emptyHeaders)
                .AsClient;

            var jobs = client.GetJobList(new GetJobListRequest()).Jobs.ToList();
            Assert.AreEqual(1, jobs.Count);
            CheckJobInfo(jobs[0]);
        }

        private static void CheckJobInfo(JobInfo jobInfo)
        {
            Assert.AreEqual("bucketName", jobInfo.BucketName);
            Assert.AreEqual(Guid.Parse("a4a586a1-cb80-4441-84e2-48974e982d51"), jobInfo.JobId);
            Assert.AreEqual("NORMAL", jobInfo.Priority);
            Assert.AreEqual("PUT", jobInfo.RequestType);
            Assert.AreEqual("2014-05-22T18:24:00.000Z", jobInfo.StartDate);
        }

        [Test]
        public void AllocateChunkReturnsChunkWithNodeWhenAllocated()
        {
            var responseContent = ReadResource(AllocateJobChunkResponseResourceName);
            var queryParams = new Dictionary<string, string> { { "operation", "allocate" } };
            var client = MockNetwork
                .Expecting(HttpVerb.PUT, "/_rest_/job_chunk/f58370c2-2538-4e78-a9f8-e4d2676bdf44", queryParams, "")
                .Returning(HttpStatusCode.OK, responseContent, _emptyHeaders)
                .AsClient;
            var response = client.AllocateJobChunk(new AllocateJobChunkRequest(Guid.Parse("f58370c2-2538-4e78-a9f8-e4d2676bdf44")));
            JobObjectList chunkResult = null;
            response.Match(
                chunk => chunkResult = chunk,
                retryAfter => Assert.Fail(),
                Assert.Fail
            );
            Assert.NotNull(chunkResult);
            Assert.AreEqual(Guid.Parse("a02053b9-0147-11e4-8d6a-002590c1177c"), chunkResult.NodeId);
            Assert.AreEqual(Guid.Parse("f58370c2-2538-4e78-a9f8-e4d2676bdf44"), chunkResult.ChunkId);
            Assert.AreEqual(0, chunkResult.ChunkNumber);
            Assert.AreEqual(14, chunkResult.Objects.Count());
        }

        [Test]
        public void AllocateChunkReturnsRetryWhen503AndHeader()
        {
            var queryParams = new Dictionary<string, string> { { "operation", "allocate" } };
            var headers = new Dictionary<string, string> { { "Retry-After", "300" } };
            var client = MockNetwork
                .Expecting(HttpVerb.PUT, "/_rest_/job_chunk/f58370c2-2538-4e78-a9f8-e4d2676bdf44", queryParams, "")
                .Returning(HttpStatusCode.ServiceUnavailable, "", headers)
                .AsClient;
            var response = client.AllocateJobChunk(new AllocateJobChunkRequest(Guid.Parse("f58370c2-2538-4e78-a9f8-e4d2676bdf44")));
            TimeSpan? retryResult = null;
            response.Match(
                chunk => Assert.Fail(),
                retryAfter => retryResult = retryAfter,
                Assert.Fail
            );
            Assert.NotNull(retryResult);
            Assert.AreEqual(TimeSpan.FromMinutes(5), retryResult.Value);
        }

        [Test]
        public void AllocateChunkReturnsChunkGoneWhen404()
        {
            var queryParams = new Dictionary<string, string> { { "operation", "allocate" } };
            var client = MockNetwork
                .Expecting(HttpVerb.PUT, "/_rest_/job_chunk/f58370c2-2538-4e78-a9f8-e4d2676bdf44", queryParams, "")
                .Returning(HttpStatusCode.NotFound, "", _emptyHeaders)
                .AsClient;
            var response = client.AllocateJobChunk(new AllocateJobChunkRequest(Guid.Parse("f58370c2-2538-4e78-a9f8-e4d2676bdf44")));
            var chunkIsGone = false;
            response.Match(
                chunk => Assert.Fail(),
                retryAfter => Assert.Fail(),
                () => chunkIsGone = true
            );
            Assert.IsTrue(chunkIsGone);
        }

        [Test]
        public void GetAvailableChunksReturnsJobWhenSuccess()
        {
            var queryParams = new Dictionary<string, string> { { "job", "1a85e743-ec8f-4789-afec-97e587a26936" } };
            var responseContent = ReadResource(GetAvailableJobChunksResponseResourceName);
            var client = MockNetwork
                .Expecting(HttpVerb.GET, "/_rest_/job_chunk", queryParams, "")
                .Returning(HttpStatusCode.OK, responseContent, new Dictionary<string, string> { { "retry-after", "123" } })
                .AsClient;

            var response = client.GetAvailableJobChunks(new GetAvailableJobChunksRequest(Guid.Parse("1a85e743-ec8f-4789-afec-97e587a26936")));
            TimeSpan timeSpan = TimeSpan.MinValue;
            JobResponse jobChunks = null;
            response.Match(
                (ts, jobResponse) => { timeSpan = ts; jobChunks = jobResponse; },
                () => Assert.Fail(),
                retryAfter => Assert.Fail()
            );
            Assert.NotNull(jobChunks);
            var expectedNodeIds = new[]
            {
                Guid.Parse("a02053b9-0147-11e4-8d6a-002590c1177c"),
                Guid.Parse("95e97010-8e70-4733-926c-aeeb21796848")
            };
            CollectionAssert.AreEqual(expectedNodeIds, jobChunks.ObjectLists.Select(chunk => chunk.NodeId.Value));
            Assert.AreEqual(TimeSpan.FromSeconds(123), timeSpan);
        }

        [Test]
        public void GetAvailableChunksReturnsJobGoneWhen404()
        {
            var queryParams = new Dictionary<string, string> { { "job", "1a85e743-ec8f-4789-afec-97e587a26936" } };
            var client = MockNetwork
                .Expecting(HttpVerb.GET, "/_rest_/job_chunk", queryParams, "")
                .Returning(HttpStatusCode.NotFound, "", _emptyHeaders)
                .AsClient;

            var response = client.GetAvailableJobChunks(new GetAvailableJobChunksRequest(Guid.Parse("1a85e743-ec8f-4789-afec-97e587a26936")));
            var wasJobGone = false;
            response.Match(
                (ts, jobResponse) => Assert.Fail(),
                () => wasJobGone = true,
                retryAfter => Assert.Fail()
            );
            Assert.IsTrue(wasJobGone);
        }

        [Test]
        public void GetAvailableChunksReturnsRetryAfterWhenNoNodes()
        {
            var queryParams = new Dictionary<string, string> { { "job", "1a85e743-ec8f-4789-afec-97e587a26936" } };
            var headers = new Dictionary<string, string> { { "Retry-After", "300" } };
            var responseContent = ReadResource(EmptyGetAvailableJobChunksResponseResourceName);
            var client = MockNetwork
                .Expecting(HttpVerb.GET, "/_rest_/job_chunk", queryParams, "")
                .Returning(HttpStatusCode.OK, responseContent, headers)
                .AsClient;

            var response = client.GetAvailableJobChunks(new GetAvailableJobChunksRequest(Guid.Parse("1a85e743-ec8f-4789-afec-97e587a26936")));
            TimeSpan? retryValue = null;
            response.Match(
                (ts, jobResponse) => Assert.Fail(),
                () => Assert.Fail(),
                retryAfter => retryValue = retryAfter
            );
            Assert.NotNull(retryValue);
            Assert.AreEqual(TimeSpan.FromMinutes(5), retryValue.Value);
        }

        private readonly static Tape _testTape = new Tape
        {
            AssignedToBucket = false,
            AvailableRawCapacity = 10000L,
            BarCode = "t1",
            BucketId = "7a6a0b80-4c24-4f8c-9779-527e863c5470",
            DescriptionForIdentification = "A really cool tape",
            EjectDate = new DateTime(2015, 4, 13, 8, 9, 18, 443),
            EjectLabel = "Bin X",
            EjectLocation = "New Jersey",
            EjectPending = null,
            FullOfData = false,
            Id = "11eaa8ec-e287-4852-a5f8-c06b8dd90aec",
            LastAccessed = null,
            LastCheckpoint = "",
            LastModified = new DateTime(2015, 4, 9, 8, 11, 48, 305),
            LastVerified = null,
            PartitionId = "9d1ab9db-f488-461d-b04c-31001496c05e",
            PreviousState = null,
            SerialNumber = "123456",
            State = TapeState.Ejected,
            TotalRawCapacity = 20000,
            Type = TapeType.Lto5,
            WriteProtected = false,
        };

        [Test]
        public void GetPhysicalPlacementReturnsPartialDetails()
        {
            var requestString = "<Objects><Object Name=\"o1\" /><Object Name=\"o2\" /><Object Name=\"o3\" /><Object Name=\"o4\" /></Objects>";
            var queryParams = new Dictionary<string, string>
            {
                { "operation", "get_physical_placement" },
            };
            var responseContent = ReadResource(GetPhysicalPlacementResponseResourceName);
            var client = MockNetwork
                .Expecting(HttpVerb.PUT, "/_rest_/bucket/test_bucket", queryParams, requestString)
                .Returning(HttpStatusCode.OK, responseContent, _emptyHeaders)
                .AsClient;

            var result = client.GetPhysicalPlacement(new GetPhysicalPlacementRequest("test_bucket", new[] { "o1", "o2", "o3", "o4" }));
            Assert.Throws<InvalidOperationException>(() => { var placements = result.ObjectPlacements; });
            CollectionAssert.AreEqual(new[] { _testTape }, result.Tapes.ToArray(), new TapeComparer());
        }

        private static readonly Ds3ObjectPlacement _testObjectPlacement = new Ds3ObjectPlacement
        {
            Name = "foo_object",
            Offset = 10L,
            Length = 20L,
            Tapes = new[] { _testTape }
        };

        [Test]
        public void GetPhysicalPlacementReturnsFullDetails()
        {
            var requestString = "<Objects><Object Name=\"o1\" /><Object Name=\"o2\" /><Object Name=\"o3\" /><Object Name=\"o4\" /></Objects>";
            var queryParams = new Dictionary<string, string>
            {
                { "operation", "get_physical_placement" },
                { "full_details", "" },
            };
            var responseContent = ReadResource(GetPhysicalPlacementFullDetailsResponseResourceName);
            var client = MockNetwork
                .Expecting(HttpVerb.PUT, "/_rest_/bucket/test_bucket", queryParams, requestString)
                .Returning(HttpStatusCode.OK, responseContent, _emptyHeaders)
                .AsClient;

            var result = client.GetPhysicalPlacement(
                new GetPhysicalPlacementRequest("test_bucket", new[] { "o1", "o2", "o3", "o4" })
                    .WithFullDetails()
            );
            Assert.Throws<InvalidOperationException>(() => { var placements = result.Tapes; });
            CollectionAssert.AreEqual( new[] { _testObjectPlacement }, result.ObjectPlacements.ToArray(), new Ds3ObjectPlacementComparer());
        }

        private class Ds3ObjectPlacementComparer : IComparer, IComparer<Ds3ObjectPlacement>
        {
            public int Compare(object x, object y)
            {
                return this.Compare(x as Ds3ObjectPlacement, y as Ds3ObjectPlacement);
            }

            public int Compare(Ds3ObjectPlacement x, Ds3ObjectPlacement y)
            {
                if (x == null || y == null)
                {
                    throw new ArgumentNullException();
                }
                return CompareChain.Of(x, y)
                    .Value(op => op.Name)
                    .Value(op => op.Offset)
                    .Value(op => op.Length)
                    .Value(op => op.Tapes, new EnumerableComparer<Tape>(new TapeComparer()))
                    .Result;
            }
        }

        private class TapeComparer : IComparer, IComparer<Tape>
        {
            public int Compare(object x, object y)
            {
                return this.Compare(x as Tape, y as Tape);
            }

            public int Compare(Tape x, Tape y)
            {
                if (x == null || y == null)
                {
                    throw new ArgumentNullException();
                }

                return CompareChain.Of(x, y)
                    .Value(t => t.AssignedToBucket)
        			.Value(t => t.AvailableRawCapacity)
        			.Value(t => t.BarCode)
        			.Value(t => t.BucketId)
        			.Value(t => t.DescriptionForIdentification)
        			.Value(t => t.EjectDate)
        			.Value(t => t.EjectLabel)
        			.Value(t => t.EjectLocation)
        			.Value(t => t.EjectPending)
        			.Value(t => t.FullOfData)
        			.Value(t => t.Id)
        			.Value(t => t.LastAccessed)
        			.Value(t => t.LastCheckpoint)
        			.Value(t => t.LastModified)
        			.Value(t => t.LastVerified)
        			.Value(t => t.PartitionId)
        			.Value(t => t.PreviousState)
        			.Value(t => t.SerialNumber)
        			.Value(t => t.State)
        			.Value(t => t.TotalRawCapacity)
        			.Value(t => t.Type)
        			.Value(t => t.WriteProtected)
                    .Result;
            }
        }
    }
}
