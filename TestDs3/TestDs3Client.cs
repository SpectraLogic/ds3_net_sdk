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
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using Ds3;
using Ds3.Calls;
using Ds3.Helpers;
using Ds3.Models;
using Ds3.Runtime;
using NUnit.Framework;
using TestDs3.Lang;

namespace TestDs3
{
    [TestFixture]
    public class TestDs3Client
    {
        private static readonly IDictionary<string, string> EmptyHeaders = new Dictionary<string, string>();
        private static readonly IDictionary<string, string> EmptyQueryParams = new Dictionary<string, string>();
        private const string JobResponseResourceName = "TestDs3.TestData.ResultingMasterObjectList.xml";
        private const string CompletedJobResponseResourceName = "TestDs3.TestData.CompletedMasterObjectList.xml";
        private const string AllocateJobChunkResponseResourceName = "TestDs3.TestData.AllocateJobChunkResponse.xml";

        private const string GetAvailableJobChunksResponseResourceName =
            "TestDs3.TestData.GetAvailableJobChunksResponse.xml";

        private const string EmptyGetAvailableJobChunksResponseResourceName =
            "TestDs3.TestData.EmptyGetAvailableJobChunksResponse.xml";

        private const string GetPhysicalPlacementResponseResourceName =
            "TestDs3.TestData.GetPhysicalPlacementResponse.xml";

        private const string GetPhysicalPlacementFullDetailsResponseResourceName =
            "TestDs3.TestData.GetPhysicalPlacementFullDetailsResponse.xml";

        private const string IdsRequestPayload = "<Ids><Id>id1</Id><Id>id2</Id><Id>id3</Id></Ids>";

        private List<string> IdsRequestPayloadList = new List<string> { "id1", "id2", "id3" };

        private const string SimpleDs3ObjectPayload = "<Objects><Object Name=\"obj1\" /><Object Name=\"obj2\" /><Object Name=\"obj3\" /></Objects>";
        private List<Ds3Object> SimpleDs3Objects = new List<Ds3Object> {
            new Ds3Object("obj1", null),
            new Ds3Object("obj2", null),
            new Ds3Object("obj3", null)
        };

        private static void RunBulkTest(
            string operation,
            Func<IDs3Client, List<Ds3Object>, MasterObjectList> makeCall,
            IDictionary<string, string> additionalQueryParams)
        {
            var files = new[]
            {
                new {Key = "client00obj000000-8000000", Size = 8192000000L},
                new {Key = "client00obj000001-8000000", Size = 8192000000L},
                new {Key = "client00obj000002-8000000", Size = 8192000000L},
                new {Key = "client00obj000003-8000000", Size = 8192000000L},
                new {Key = "client00obj000004-8000000", Size = 8192000000L},
                new {Key = "client00obj000005-8000000", Size = 8192000000L},
                new {Key = "client00obj000006-8000000", Size = 8192000000L},
                new {Key = "client00obj000007-8000000", Size = 8192000000L},
                new {Key = "client00obj000008-8000000", Size = 8192000000L},
                new {Key = "client00obj000009-8000000", Size = 8192000000L}
            };

            var stringRequest = operation == "start_bulk_get"
                ? "<Objects><Object Name=\"client00obj000000-8000000\" /><Object Name=\"client00obj000001-8000000\" /><Object Name=\"client00obj000002-8000000\" /><Object Name=\"client00obj000003-8000000\" /><Object Name=\"client00obj000004-8000000\" /><Object Name=\"client00obj000005-8000000\" /><Object Name=\"client00obj000006-8000000\" /><Object Name=\"client00obj000007-8000000\" /><Object Name=\"client00obj000008-8000000\" /><Object Name=\"client00obj000009-8000000\" /></Objects>"
                : "<Objects><Object Name=\"client00obj000000-8000000\" Size=\"8192000000\" /><Object Name=\"client00obj000001-8000000\" Size=\"8192000000\" /><Object Name=\"client00obj000002-8000000\" Size=\"8192000000\" /><Object Name=\"client00obj000003-8000000\" Size=\"8192000000\" /><Object Name=\"client00obj000004-8000000\" Size=\"8192000000\" /><Object Name=\"client00obj000005-8000000\" Size=\"8192000000\" /><Object Name=\"client00obj000006-8000000\" Size=\"8192000000\" /><Object Name=\"client00obj000007-8000000\" Size=\"8192000000\" /><Object Name=\"client00obj000008-8000000\" Size=\"8192000000\" /><Object Name=\"client00obj000009-8000000\" Size=\"8192000000\" /></Objects>";
            var stringResponse = ReadResource(JobResponseResourceName);

            var inputObjects = files.Select(f => new Ds3Object(f.Key, f.Size)).ToList();

            var queryParams = new Dictionary<string, string> {{"operation", operation}};
            foreach (var kvp in additionalQueryParams)
            {
                queryParams.Add(kvp.Key, kvp.Value);
            }
            var client = MockNetwork
                .Expecting(HttpVerb.PUT, "/_rest_/bucket/bucket8192000000", queryParams, stringRequest)
                .Returning(HttpStatusCode.OK, stringResponse, EmptyHeaders)
                .AsClient;
            CheckJobResponse(makeCall(client, inputObjects));
        }

        private static string ReadResource(string resourceName)
        {
            using (var xmlFile = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
            using (var reader = new StreamReader(xmlFile))
                return reader.ReadToEnd();
        }

        private static void CheckJobResponse(MasterObjectList response)
        {
            var expectedNodes = new[]
            {
                new JobNode
                {
                    EndPoint = "10.1.18.12",
                    HttpPort = 80,
                    HttpsPort = 443,
                    Id = Guid.Parse("a02053b9-0147-11e4-8d6a-002590c1177c")
                },
                new JobNode
                {
                    EndPoint = "10.1.18.13",
                    HttpPort = null,
                    HttpsPort = 443,
                    Id = Guid.Parse("4ecebf6f-bfd2-40a8-82a6-32fd684fd500")
                },
                new JobNode
                {
                    EndPoint = "10.1.18.14",
                    HttpPort = 80,
                    HttpsPort = null,
                    Id = Guid.Parse("4d5b6669-76f0-49f9-bc2a-9280f40cafa7")
                },
            };
            var expectedObjectLists = new[]
            {
                new Objects
                {
                    ChunkNumber = 0,
                    ChunkId = Guid.Parse("f58370c2-2538-4e78-a9f8-e4d2676bdf44"),
                    NodeId = Guid.Parse("a02053b9-0147-11e4-8d6a-002590c1177c"),
                    ObjectsList = new[]
                    {
                        new BulkObject {Name = "client00obj000004-8000000", Length = 5368709120L, Offset = 0L},
                        new BulkObject {Name = "client00obj000004-8000000", Length = 2823290880L, Offset = 5368709120L},
                        new BulkObject {Name = "client00obj000003-8000000", Length = 2823290880L, Offset = 5368709120L},
                        new BulkObject {Name = "client00obj000003-8000000", Length = 5368709120L, Offset = 0L},
                        new BulkObject {Name = "client00obj000002-8000000", Length = 5368709120L, Offset = 0L},
                        new BulkObject {Name = "client00obj000002-8000000", Length = 2823290880L, Offset = 5368709120L},
                        new BulkObject {Name = "client00obj000005-8000000", Length = 5368709120L, Offset = 0L},
                        new BulkObject {Name = "client00obj000005-8000000", Length = 2823290880L, Offset = 5368709120L},
                        new BulkObject {Name = "client00obj000006-8000000", Length = 5368709120L, Offset = 0L},
                        new BulkObject {Name = "client00obj000006-8000000", Length = 2823290880L, Offset = 5368709120L},
                        new BulkObject {Name = "client00obj000000-8000000", Length = 5368709120L, Offset = 0L},
                        new BulkObject {Name = "client00obj000000-8000000", Length = 2823290880L, Offset = 5368709120L},
                        new BulkObject {Name = "client00obj000001-8000000", Length = 5368709120L, Offset = 0L},
                        new BulkObject {Name = "client00obj000001-8000000", Length = 2823290880L, Offset = 5368709120L},
                    },
                },
                new Objects
                {
                    ChunkNumber = 1,
                    ChunkId = Guid.Parse("4137d768-25bb-4942-9d36-b92dfbe75e01"),
                    NodeId = null,
                    ObjectsList = new[]
                    {
                        new BulkObject {Name = "client00obj000008-8000000", Length = 2823290880L, Offset = 5368709120L},
                        new BulkObject {Name = "client00obj000008-8000000", Length = 5368709120L, Offset = 0L},
                        new BulkObject {Name = "client00obj000009-8000000", Length = 2823290880L, Offset = 5368709120L},
                        new BulkObject {Name = "client00obj000009-8000000", Length = 5368709120L, Offset = 0L},
                        new BulkObject {Name = "client00obj000007-8000000", Length = 5368709120L, Offset = 0L},
                        new BulkObject {Name = "client00obj000007-8000000", Length = 2823290880L, Offset = 5368709120L},
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
            HelpersForTest.AssertCollectionsEqual(expectedObjectLists, response.Objects,
                (expectedObjectList, actualObjectList) =>
                {
                    Assert.AreEqual(expectedObjectList.ChunkNumber, actualObjectList.ChunkNumber);
                    Assert.AreEqual(expectedObjectList.ChunkId, actualObjectList.ChunkId);
                    Assert.AreEqual(expectedObjectList.NodeId, actualObjectList.NodeId);
                    HelpersForTest.AssertCollectionsEqual(expectedObjectList.ObjectsList, actualObjectList.ObjectsList,
                        (expectedObject, actualObject) =>
                        {
                            Assert.AreEqual(expectedObject.Name, actualObject.Name);
                            Assert.AreEqual(expectedObject.Length, actualObject.Length);
                            Assert.AreEqual(expectedObject.Offset, actualObject.Offset);
                        });
                });
        }

        private static void CheckJobInfo(Job jobInfo)
        {
            Assert.AreEqual("bucketName", jobInfo.BucketName);
            Assert.AreEqual(Guid.Parse("a4a586a1-cb80-4441-84e2-48974e982d51"), jobInfo.JobId);
            Assert.AreEqual(Priority.NORMAL, jobInfo.Priority);
            Assert.AreEqual(JobRequestType.PUT, jobInfo.RequestType);
            Assert.AreEqual(DateTime.Parse("2014-05-22T18:24:00.000Z"), jobInfo.StartDate);
        }

        private static void RunPrefixTest(string prefix, string testDirName)
        {
            var root = Path.GetTempPath() + testDirName + Path.DirectorySeparatorChar;
            var src = root + "src" + Path.DirectorySeparatorChar;
            var dest = root + "dest" + Path.DirectorySeparatorChar;
            var destPut = root + "destput" + Path.DirectorySeparatorChar;
            string[] files = {"one.txt", "two.txt", "three.txt"};
            var testdata = "On the shore dimly seen, through the mists of the deep";
            testdata += "Where our foe's haughty host, in dread silence reposes.";

            // create and populate a new test dir
            if (Directory.Exists(root))
            {
                Directory.Delete(root, true);
            }
            Directory.CreateDirectory(root);
            Directory.CreateDirectory(src);
            Directory.CreateDirectory(dest);
            Directory.CreateDirectory(destPut);

            foreach (var file in files)
            {
                TextWriter writer = new StreamWriter(src + file);
                writer.WriteLine(testdata);
                writer.Close();
            }

            //Copy with prefix
            var srcFiles = FileHelpers.ListObjectsForDirectory(src, prefix);
            foreach (var file in srcFiles)
            {
                TextWriter writer = new StreamWriter(dest + file.Name);
                writer.WriteLine(testdata);
                writer.Close();
            }

            // normally would be used for objects coming from device
            var srcFilesNoPrefix = FileHelpers.ListObjectsForDirectory(src, string.Empty);
            var fGet = FileHelpers.BuildFileGetter(destPut, prefix);
            foreach (var file in srcFilesNoPrefix)
            {
                var stream = fGet(file.Name);
                Assert.IsNotNull(stream);
                stream.WriteByte(0x48);
                stream.WriteByte(0x69);
                stream.Flush();
                stream.Close();
            }

            // Look in source with FilePutter (should remove prefix to find source file)
            var fPut = FileHelpers.BuildFilePutter(src, prefix);
            var destFiles = Directory.EnumerateFiles(dest);
            Assert.AreEqual(destFiles.Count(), files.Length);
            var destPutFiles = Directory.EnumerateFiles(destPut);
            Assert.AreEqual(destPutFiles.Count(), files.Length);
            CollectionAssert.AreEquivalent(JustFilenames(destFiles), JustFilenames(destPutFiles));
            foreach (var path in destFiles)
            {
                var file = Path.GetFileName(path);
                var stream = fPut(file);
                var size = stream.Length;
                Assert.GreaterOrEqual(size, testdata.Length);
            }
        }

        private static IEnumerable<string> JustFilenames(IEnumerable<string> fullpaths)
        {
            return fullpaths.Select(Path.GetFileName).ToList();
        }

        private static readonly Tape TestTape = new Tape
        {
            AssignedToStorageDomain = false,
            AvailableRawCapacity = 10000L,
            BarCode = "t1",
            BucketId = Guid.Parse("7a6a0b80-4c24-4f8c-9779-527e863c5470"),
            DescriptionForIdentification = "A really cool tape",
            EjectDate = new DateTime(2015, 4, 13, 8, 9, 18, 443),
            EjectLabel = "Bin X",
            EjectLocation = "New Jersey",
            EjectPending = null,
            FullOfData = false,
            Id = Guid.Parse("11eaa8ec-e287-4852-a5f8-c06b8dd90aec"),
            LastAccessed = null,
            LastCheckpoint = null,
            LastModified = new DateTime(2015, 4, 9, 8, 11, 48, 305),
            LastVerified = null,
            PartitionId = Guid.Parse("9d1ab9db-f488-461d-b04c-31001496c05e"),
            PreviousState = null,
            SerialNumber = "123456",
            State = TapeState.EJECTED,
            TotalRawCapacity = 20000,
            Type = TapeType.LTO5,
            WriteProtected = false,
        };

        private static readonly PhysicalPlacement TestObjectPlacement = new PhysicalPlacement
        {
            Tapes = new[] {TestTape}
        };

        private class BulkObjectComparer : IComparer, IComparer<BulkObject>
        {
            public int Compare(object x, object y)
            {
                return Compare(x as BulkObject, y as BulkObject);
            }

            public int Compare(BulkObject x, BulkObject y)
            {
                if (x == null || y == null)
                {
                    throw new ArgumentNullException();
                }

                return CompareChain.Of(x, y)
                    .Value(op => op.Name)
                    .Value(op => op.Offset)
                    .Value(op => op.Length)
                    .Value(op => op.PhysicalPlacement, new PhysicalPlacementComparer())
                    .Result;
            }
        }

        private class PhysicalPlacementComparer : IComparer, IComparer<PhysicalPlacement>
        {
            public int Compare(object x, object y)
            {
                return Compare(x as PhysicalPlacement, y as PhysicalPlacement);
            }

            public int Compare(PhysicalPlacement x, PhysicalPlacement y)
            {
                if (x == null || y == null)
                {
                    throw new ArgumentNullException();
                }
                return CompareChain.Of(x, y)
                    .Value(op => op.Tapes, new EnumerableComparer<Tape>(new TapeComparer()))
                    .Result;
            }
        }

        private class TapeComparer : IComparer, IComparer<Tape>
        {
            public int Compare(object x, object y)
            {
                return Compare(x as Tape, y as Tape);
            }

            public int Compare(Tape x, Tape y)
            {
                if (x == null || y == null)
                {
                    throw new ArgumentNullException();
                }
                return CompareChain.Of(x, y)
                    .Value(t => t.AssignedToStorageDomain)
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

        [Test]
        public void AllocateChunkReturnsChunkGoneWhen404()
        {
            var queryParams = new Dictionary<string, string> {{"operation", "allocate"}};
            var client = MockNetwork
                .Expecting(HttpVerb.PUT, "/_rest_/job_chunk/f58370c2-2538-4e78-a9f8-e4d2676bdf44", queryParams, "")
                .Returning(HttpStatusCode.NotFound, "", EmptyHeaders)
                .AsClient;
            var response =
                client.AllocateJobChunkSpectraS3(
                    new AllocateJobChunkSpectraS3Request(Guid.Parse("f58370c2-2538-4e78-a9f8-e4d2676bdf44")));
            var chunkIsGone = false;
            response.Match(
                chunk => Assert.Fail(),
                retryAfter => Assert.Fail(),
                () => chunkIsGone = true
            );
            Assert.IsTrue(chunkIsGone);
        }

        [Test]
        public void AllocateChunkReturnsChunkWithNodeWhenAllocated()
        {
            var responseContent = ReadResource(AllocateJobChunkResponseResourceName);
            var queryParams = new Dictionary<string, string> {{"operation", "allocate"}};
            var client = MockNetwork
                .Expecting(HttpVerb.PUT, "/_rest_/job_chunk/f58370c2-2538-4e78-a9f8-e4d2676bdf44", queryParams, "")
                .Returning(HttpStatusCode.OK, responseContent, EmptyHeaders)
                .AsClient;
            var response =
                client.AllocateJobChunkSpectraS3(
                    new AllocateJobChunkSpectraS3Request(Guid.Parse("f58370c2-2538-4e78-a9f8-e4d2676bdf44")));
            Objects chunkResult = null;
            response.Match(
                chunk => chunkResult = chunk,
                retryAfter => Assert.Fail(),
                Assert.Fail
            );
            Assert.NotNull(chunkResult);
            Assert.AreEqual(Guid.Parse("a02053b9-0147-11e4-8d6a-002590c1177c"), chunkResult.NodeId);
            Assert.AreEqual(Guid.Parse("f58370c2-2538-4e78-a9f8-e4d2676bdf44"), chunkResult.ChunkId);
            Assert.AreEqual(0, chunkResult.ChunkNumber);
            Assert.AreEqual(14, chunkResult.ObjectsList.Count());
        }

        [Test]
        public void AllocateChunkReturnsRetryWhen503AndHeader()
        {
            var queryParams = new Dictionary<string, string> {{"operation", "allocate"}};
            var headers = new Dictionary<string, string> {{"Retry-After", "300"}};
            var client = MockNetwork
                .Expecting(HttpVerb.PUT, "/_rest_/job_chunk/f58370c2-2538-4e78-a9f8-e4d2676bdf44", queryParams, "")
                .Returning(HttpStatusCode.ServiceUnavailable, "", headers)
                .AsClient;
            var response =
                client.AllocateJobChunkSpectraS3(
                    new AllocateJobChunkSpectraS3Request(Guid.Parse("f58370c2-2538-4e78-a9f8-e4d2676bdf44")));
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
        public void GetAvailableChunksReturnsJobGoneWhen404()
        {
            var queryParams = new Dictionary<string, string> {{"job", "1a85e743-ec8f-4789-afec-97e587a26936"}};
            var client = MockNetwork
                .Expecting(HttpVerb.GET, "/_rest_/job_chunk", queryParams, "")
                .Returning(HttpStatusCode.NotFound, "", EmptyHeaders)
                .AsClient;

            var response =
                client.GetJobChunksReadyForClientProcessingSpectraS3(
                    new GetJobChunksReadyForClientProcessingSpectraS3Request(
                        Guid.Parse("1a85e743-ec8f-4789-afec-97e587a26936")));
            var wasJobGone = false;
            response.Match(
                (ts, jobResponse) => Assert.Fail(),
                () => wasJobGone = true,
                retryAfter => Assert.Fail()
            );
            Assert.IsTrue(wasJobGone);
        }

        [Test]
        public void GetAvailableChunksReturnsJobWhenSuccess()
        {
            var queryParams = new Dictionary<string, string> {{"job", "1a85e743-ec8f-4789-afec-97e587a26936"}};
            var responseContent = ReadResource(GetAvailableJobChunksResponseResourceName);
            var client = MockNetwork
                .Expecting(HttpVerb.GET, "/_rest_/job_chunk", queryParams, "")
                .Returning(HttpStatusCode.OK, responseContent, new Dictionary<string, string> {{"retry-after", "123"}})
                .AsClient;

            var response =
                client.GetJobChunksReadyForClientProcessingSpectraS3(
                    new GetJobChunksReadyForClientProcessingSpectraS3Request(
                        Guid.Parse("1a85e743-ec8f-4789-afec-97e587a26936")));
            var timeSpan = TimeSpan.MinValue;
            MasterObjectList jobChunks = null;
            response.Match(
                (ts, jobResponse) =>
                {
                    timeSpan = ts;
                    jobChunks = jobResponse;
                },
                Assert.Fail,
                retryAfter => Assert.Fail()
            );
            Assert.NotNull(jobChunks);
            var expectedNodeIds = new[]
            {
                Guid.Parse("a02053b9-0147-11e4-8d6a-002590c1177c"),
                Guid.Parse("95e97010-8e70-4733-926c-aeeb21796848")
            };
            CollectionAssert.AreEqual(expectedNodeIds, jobChunks.Objects.Select(chunk => chunk.NodeId.Value));
            Assert.AreEqual(TimeSpan.FromSeconds(123), timeSpan);
        }

        [Test]
        public void GetAvailableChunksReturnsRetryAfterWhenNoNodes()
        {
            var queryParams = new Dictionary<string, string> {{"job", "1a85e743-ec8f-4789-afec-97e587a26936"}};
            var headers = new Dictionary<string, string> {{"Retry-After", "300"}};
            var responseContent = ReadResource(EmptyGetAvailableJobChunksResponseResourceName);
            var client = MockNetwork
                .Expecting(HttpVerb.GET, "/_rest_/job_chunk", queryParams, "")
                .Returning(HttpStatusCode.OK, responseContent, headers)
                .AsClient;

            var response =
                client.GetJobChunksReadyForClientProcessingSpectraS3(
                    new GetJobChunksReadyForClientProcessingSpectraS3Request(
                        Guid.Parse("1a85e743-ec8f-4789-afec-97e587a26936")));
            TimeSpan? retryValue = null;
            response.Match(
                (ts, jobResponse) => Assert.Fail(),
                Assert.Fail,
                retryAfter => retryValue = retryAfter
            );
            Assert.NotNull(retryValue);
            Assert.AreEqual(TimeSpan.FromMinutes(5), retryValue.Value);
        }

        [Test]
        public void GetPhysicalPlacementReturnsFullDetails()
        {
            var requestString =
                "<Objects><Object Name=\"o1\" /><Object Name=\"o2\" /><Object Name=\"o3\" /><Object Name=\"o4\" /></Objects>";
            var queryParams = new Dictionary<string, string>
            {
                {"operation", "get_physical_placement"},
                {"full_details", null},
            };
            var responseContent = ReadResource(GetPhysicalPlacementFullDetailsResponseResourceName);
            var client = MockNetwork
                .Expecting(HttpVerb.PUT, "/_rest_/bucket/test_bucket", queryParams, requestString)
                .Returning(HttpStatusCode.OK, responseContent, EmptyHeaders)
                .AsClient;

            IEnumerable<Ds3Object> objects = new[]
            {
                new Ds3Object("o1", null),
                new Ds3Object("o2", null),
                new Ds3Object("o3", null),
                new Ds3Object("o4", null)
            };

            var response = client
                .GetPhysicalPlacementForObjectsWithFullDetailsSpectraS3(
                    new GetPhysicalPlacementForObjectsWithFullDetailsSpectraS3Request("test_bucket", objects));
            IEnumerable<BulkObject> result = response.ResponsePayload.Objects.ToArray();

            IEnumerable<BulkObject> expected = new[]
            {
                new BulkObject
                {
                    Name = "foo_object",
                    Offset = 10L,
                    Length = 20L,
                    PhysicalPlacement = TestObjectPlacement
                }
            };
            CollectionAssert.AreEqual(expected, result, new BulkObjectComparer());
        }

        [Test]
        public void GetPhysicalPlacementReturnsPartialDetails()
        {
            var requestString =
                "<Objects><Object Name=\"o1\" /><Object Name=\"o2\" /><Object Name=\"o3\" /><Object Name=\"o4\" /></Objects>";
            var queryParams = new Dictionary<string, string>
            {
                {"operation", "get_physical_placement"},
            };
            var responseContent = ReadResource(GetPhysicalPlacementResponseResourceName);
            var client = MockNetwork
                .Expecting(HttpVerb.PUT, "/_rest_/bucket/test_bucket", queryParams, requestString)
                .Returning(HttpStatusCode.OK, responseContent, EmptyHeaders)
                .AsClient;

            var objects = new[]
            {
                new Ds3Object("o1", null),
                new Ds3Object("o2", null),
                new Ds3Object("o3", null),
                new Ds3Object("o4", null)
            };

            var result =
                client.GetPhysicalPlacementForObjectsSpectraS3(
                    new GetPhysicalPlacementForObjectsSpectraS3Request("test_bucket", objects)).ResponsePayload;
            CollectionAssert.AreEqual(new[] {TestTape}, result.Tapes.ToList(), new TapeComparer());
        }

        [Test]
        public void TestBulkGet()
        {
            RunBulkTest("start_bulk_get",
                (client, objects) =>
                    client.GetBulkJobSpectraS3(new GetBulkJobSpectraS3Request("bucket8192000000", objects))
                        .ResponsePayload, EmptyQueryParams);
        }

        [Test]
        public void TestBulkPut()
        {
            RunBulkTest("start_bulk_put", (client, objects) => client.PutBulkJobSpectraS3(
                new PutBulkJobSpectraS3Request("bucket8192000000", objects)).ResponsePayload, EmptyQueryParams);
        }

        [Test]
        public void TestBulkPutWithDs3MaxJobsException()
        {
            var queryParams = new Dictionary<string, string> {{"operation", "start_bulk_put"}};
            const string stringRequest = "<Objects><Object Name=\"obj1\" Size=\"1234\" /></Objects>";
            var mockClient = MockNetwork
                .Expecting(HttpVerb.PUT, "/_rest_/bucket/bucket", queryParams, stringRequest)
                .Returning(HttpStatusCode.ServiceUnavailable, "", EmptyHeaders)
                .AsClient;

            var files = new[]
            {
                new {Key = "obj1", Size = 1234L}
            };
            var inputObjects = files.Select(f => new Ds3Object(f.Key, f.Size)).ToList();

            Assert.Throws<Ds3MaxJobsException>(
                () => mockClient.PutBulkJobSpectraS3(new PutBulkJobSpectraS3Request("bucket", inputObjects)));
        }

        [Test]
        public void TestBulkPutWithMaxBlobSize()
        {
            RunBulkTest(
                "start_bulk_put",
                (client, objects) =>
                    client.PutBulkJobSpectraS3(
                            new PutBulkJobSpectraS3Request("bucket8192000000", objects).WithMaxUploadSize(1048576L))
                        .ResponsePayload,
                new Dictionary<string, string> {{"max_upload_size", "1048576"}}
            );
        }

        [Test]
        public void TestDeleteBucket()
        {
            MockNetwork
                .Expecting(HttpVerb.DELETE, "/bucketName", EmptyQueryParams, "")
                .Returning(HttpStatusCode.NoContent, "", EmptyHeaders)
                .AsClient
                .DeleteBucket(new DeleteBucketRequest("bucketName"));
        }

        [Test]
        public void TestDeleteFolder()
        {
            var expectedQueryParams = new Dictionary<string, string>
            {
                {"bucket_id", "testdelete"},
                {"recursive", null}
            };
            MockNetwork
                .Expecting(HttpVerb.DELETE, "/_rest_/folder/coffeehouse/jk", expectedQueryParams, "")
                .Returning(HttpStatusCode.NoContent, "", EmptyHeaders)
                .AsClient
                .DeleteFolderRecursivelySpectraS3(new DeleteFolderRecursivelySpectraS3Request("testdelete",
                    "coffeehouse/jk"));
        }

        [Test]
        public void TestDeleteFolderMissingBucket()
        {
            var expectedQueryParams = new Dictionary<string, string>
            {
                {"bucket_id", "nosuchbucket"},
                {"recursive", null}
            };
            Assert.Throws<Ds3BadStatusCodeException>(() => MockNetwork
                .Expecting(HttpVerb.DELETE, "/_rest_/folder/badfoldername", expectedQueryParams, "")
                .Returning(HttpStatusCode.NotFound, "", EmptyHeaders)
                .AsClient
                .DeleteFolderRecursivelySpectraS3(new DeleteFolderRecursivelySpectraS3Request("nosuchbucket",
                    "badfoldername")));
        }

        [Test]
        public void TestDeleteFolderMissingFolder()
        {
            var expectedQueryParams = new Dictionary<string, string>
            {
                {"bucket_id", "testdelete"},
                {"recursive", null}
            };
            Assert.Throws<Ds3BadStatusCodeException>(() => MockNetwork
                .Expecting(HttpVerb.DELETE, "/_rest_/folder/badfoldername", expectedQueryParams, "")
                .Returning(HttpStatusCode.NotFound, "", EmptyHeaders)
                .AsClient
                .DeleteFolderRecursivelySpectraS3(new DeleteFolderRecursivelySpectraS3Request("testdelete",
                    "badfoldername")));
        }

        [Test]
        public void TestDeleteObject()
        {
            MockNetwork
                .Expecting(HttpVerb.DELETE, "/bucketName/my/file.txt", EmptyQueryParams, "")
                .Returning(HttpStatusCode.NoContent, "", EmptyHeaders)
                .AsClient
                .DeleteObject(new DeleteObjectRequest("bucketName", "my/file.txt"));
        }

        [Test]
        public void TestDs3WebRequestCreateFunc()
        {
            const string responseString =
                "<Data><DatabaseFilesystemFreeSpace> NORMAL </DatabaseFilesystemFreeSpace><MsRequiredToVerifyDataPlannerHealth> 1 </MsRequiredToVerifyDataPlannerHealth></Data>";
            var client = new Ds3Client(new Network(
                null, null, 0, 0, 0, 0, 0,
                (request, stream) =>
                        new MockWebRequest(new MockWebResponse(responseString, HttpStatusCode.OK, EmptyHeaders))));

            client.VerifySystemHealthSpectraS3(new VerifySystemHealthSpectraS3Request());
        }

        [Test]
        public void TestGetBadBucket()
        {
            Assert.Throws<Ds3BadStatusCodeException>(() => MockNetwork
                .Expecting(HttpVerb.GET, "/bucketName", EmptyQueryParams, "")
                .Returning(HttpStatusCode.BadRequest, "", EmptyHeaders)
                .AsClient
                .GetBucket(new GetBucketRequest("bucketName")));
        }

        [Test]
        public void TestGetBadService()
        {
            Assert.Throws<Ds3BadStatusCodeException>(() => MockNetwork
                .Expecting(HttpVerb.GET, "/", EmptyQueryParams, "")
                .Returning(HttpStatusCode.BadRequest, "", EmptyHeaders)
                .AsClient
                .GetService(new GetServiceRequest()));
        }

        [Test]
        public void TestGetBucket()
        {
            var id = "ef2fdcac-3c80-410a-8fcb-b567c31dd33d";
            var xmlResponse = "<ListBucketResult><CreationDate>2014-01-03T13:26:47.000Z</CreationDate><Name>remoteTest16</Name><CommonPrefixes/><Marker/><MaxKeys>1000</MaxKeys><IsTruncated>false</IsTruncated><Contents><Key>user/hduser/gutenberg/20417.txt.utf-8</Key><LastModified>2014-01-03T13:26:47.000Z</LastModified><ETag>NOTRETURNED</ETag><Size>674570</Size><StorageClass>STANDARD</StorageClass><Owner>"
                              + "<ID>" + id +
                              "</ID><DisplayName>ryan</DisplayName></Owner></Contents><Contents><Key>user/hduser/gutenberg/5000.txt.utf-8</Key><LastModified>2014-01-03T13:26:47.000Z</LastModified><ETag>NOTRETURNED</ETag><Size>1423803</Size><StorageClass>STANDARD</StorageClass><Owner>"
                              + "<ID>" + id +
                              "</ID><DisplayName>ryan</DisplayName></Owner></Contents><Contents><Key>user/hduser/gutenberg/4300.txt.utf-8</Key><LastModified>2014-01-03T13:26:47.000Z</LastModified><ETag>NOTRETURNED</ETag><Size>1573150</Size><StorageClass>STANDARD</StorageClass><Owner>"
                              + "<ID>" + id +
                              "</ID><DisplayName>ryan</DisplayName></Owner></Contents></ListBucketResult>";
            var expected = new ListBucketResult
            {
                Name = "remoteTest16",
                Prefix = null,
                Marker = null,
                MaxKeys = 1000,
                Truncated = false,
                Objects = new[]
                {
                    new Contents
                    {
                        Key = "user/hduser/gutenberg/20417.txt.utf-8",
                        LastModified = DateTime.Parse("2014-01-03T13:26:47.000Z"),
                        ETag = "NOTRETURNED",
                        Size = 674570,
                        StorageClass = "STANDARD",
                        Owner = new User {Id = Guid.Parse(id), DisplayName = "ryan"}
                    },
                    new Contents
                    {
                        Key = "user/hduser/gutenberg/5000.txt.utf-8",
                        LastModified = DateTime.Parse("2014-01-03T13:26:47.000Z"),
                        ETag = "NOTRETURNED",
                        Size = 1423803,
                        StorageClass = "STANDARD",
                        Owner = new User {Id = Guid.Parse(id), DisplayName = "ryan"}
                    },
                    new Contents
                    {
                        Key = "user/hduser/gutenberg/4300.txt.utf-8",
                        LastModified = DateTime.Parse("2014-01-03T13:26:47.000Z"),
                        ETag = "NOTRETURNED",
                        Size = 1573150,
                        StorageClass = "STANDARD",
                        Owner = new User {Id = Guid.Parse(id), DisplayName = "ryan"}
                    }
                }
            };

            var response = MockNetwork
                .Expecting(HttpVerb.GET, "/remoteTest16", EmptyQueryParams, "")
                .Returning(HttpStatusCode.OK, xmlResponse, EmptyHeaders)
                .AsClient
                .GetBucket(new GetBucketRequest("remoteTest16"))
                .ResponsePayload;
            Assert.AreEqual(expected.Name, response.Name);
            Assert.AreEqual(expected.Prefix, response.Prefix);
            Assert.AreEqual(expected.Marker, response.Marker);
            Assert.AreEqual(expected.MaxKeys, response.MaxKeys);
            Assert.AreEqual(expected.Truncated, response.Truncated);

            var responseObjects = response.Objects.ToList();
            var expectedObjects = expected.Objects.ToList();
            Assert.AreEqual(expectedObjects.Count, responseObjects.Count);
            for (var i = 0; i < expectedObjects.Count; i++)
            {
                Assert.AreEqual(expectedObjects[i].Key, responseObjects[i].Key);
                Assert.AreEqual(expectedObjects[i].LastModified, responseObjects[i].LastModified);
                Assert.AreEqual(expectedObjects[i].ETag, responseObjects[i].ETag);
                Assert.AreEqual(expectedObjects[i].Size, responseObjects[i].Size);
                Assert.AreEqual(expectedObjects[i].StorageClass, responseObjects[i].StorageClass);
                Assert.AreEqual(expectedObjects[i].Owner.Id, responseObjects[i].Owner.Id);
                Assert.AreEqual(expectedObjects[i].Owner.DisplayName, responseObjects[i].Owner.DisplayName);
            }
        }

        [Test]
        public void TestGetCompletedJob()
        {
            var stringResponse = ReadResource(CompletedJobResponseResourceName);
            var client = MockNetwork
                .Expecting(HttpVerb.GET, "/_rest_/job/350225fb-ec92-4456-a09d-5ffb7c7830bb", EmptyQueryParams, "")
                .Returning(HttpStatusCode.OK, stringResponse, EmptyHeaders)
                .AsClient;
            var jobId = Guid.Parse("350225fb-ec92-4456-a09d-5ffb7c7830bb");
            var job = client.GetJobSpectraS3(new GetJobSpectraS3Request(jobId)).ResponsePayload;
            Assert.AreEqual("avid-bucket", job.BucketName);
            Assert.AreEqual(JobChunkClientProcessingOrderGuarantee.IN_ORDER, job.ChunkClientProcessingOrderGuarantee);
            Assert.AreEqual(jobId, job.JobId);
            CollectionAssert.AreEqual(Enumerable.Empty<Objects>(), job.Nodes);
            CollectionAssert.AreEqual(Enumerable.Empty<Objects>(), job.Objects);
            Assert.AreEqual(Priority.NORMAL, job.Priority);
            Assert.AreEqual(JobRequestType.PUT, job.RequestType);
            Assert.AreEqual(new DateTime(2015, 5, 5, 17, 9, 30, 0, DateTimeKind.Utc), job.StartDate.ToUniversalTime());
            Assert.AreEqual(JobStatus.COMPLETED, job.Status);
        }

        [Test]
        public void TestGetJob()
        {
            var stringResponse = ReadResource(JobResponseResourceName);
            var client = MockNetwork
                .Expecting(HttpVerb.GET, "/_rest_/job/1a85e743-ec8f-4789-afec-97e587a26936", EmptyQueryParams, "")
                .Returning(HttpStatusCode.OK, stringResponse, EmptyHeaders)
                .AsClient;
            CheckJobResponse(
                client.GetJobSpectraS3(new GetJobSpectraS3Request(Guid.Parse("1a85e743-ec8f-4789-afec-97e587a26936")))
                    .ResponsePayload);
        }

        [Test]
        public void TestGetJobList()
        {
            var responseContent =
                "<Jobs><Job EntirelyInCache=\"false\" WriteOptimization=\"CAPACITY\" UserId=\"c041e1a1-1900-4e7d-814b-3bd018df047c\" OriginalSizeInBytes=\"0\" Naked=\"false\" CompletedSizeInBytes=\"0\" ChunkClientProcessingOrderGuarantee=\"IN_ORDER\" CachedSizeInBytes=\"0\" Aggregating=\"false\" BucketName=\"bucketName\" JobId=\"a4a586a1-cb80-4441-84e2-48974e982d51\" Priority=\"NORMAL\" RequestType=\"PUT\" StartDate=\"2014-05-22T18:24:00.000Z\" Status=\"IN_PROGRESS\"/></Jobs>";
            var client = MockNetwork
                .Expecting(HttpVerb.GET, "/_rest_/job", new Dictionary<string, string>(), "")
                .Returning(HttpStatusCode.OK, responseContent, EmptyHeaders)
                .AsClient;

            var jobs = client.GetJobsSpectraS3(new GetJobsSpectraS3Request()).ResponsePayload.Jobs.ToList();
            Assert.AreEqual(1, jobs.Count);
            CheckJobInfo(jobs[0]);
        }

        [Test]
        public void TestGetJobListWithBucketName()
        {
            var responseContent = "<Jobs><Job EntirelyInCache=\"false\" Aggregating=\"false\" BucketName=\"bucketName\" CachedSizeInBytes=\"0\" "
                                  +
                                  "ChunkClientProcessingOrderGuarantee=\"IN_ORDER\" CompletedSizeInBytes=\"0\" OriginalSizeInBytes=\"0\" "
                                  +
                                  "JobId=\"a4a586a1-cb80-4441-84e2-48974e982d51\" Naked=\"false\" Priority=\"NORMAL\" RequestType=\"PUT\" "
                                  + "UserId=\"c041e1a1-1900-4e7d-814b-3bd018df047c\" WriteOptimization=\"CAPACITY\" "
                                  + "StartDate=\"2014-05-22T18:24:00.000Z\" Status=\"IN_PROGRESS\"/></Jobs>";
            var queryParams = new Dictionary<string, string>
            {
                {"bucket_id", "bucketName"}
            };
            var client = MockNetwork
                .Expecting(HttpVerb.GET, "/_rest_/job", queryParams, "")
                .Returning(HttpStatusCode.OK, responseContent, EmptyHeaders)
                .AsClient;


            var response = client.GetJobsSpectraS3(new GetJobsSpectraS3Request().WithBucketId("bucketName"));
            var jobs = response.ResponsePayload.Jobs.ToList();
            Assert.AreEqual(1, jobs.Count);
            CheckJobInfo(jobs[0]);
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
                    {"job", "6f7a31c1-7ddc-46d3-975d-be26d9878493"},
                    {"offset", "10"}
                };
                MockNetwork
                    .Expecting(HttpVerb.GET, "/bucketName/object", expectedQueryParams, "")
                    .Returning(HttpStatusCode.OK, stringResponse, EmptyHeaders)
                    .AsClient
                    .GetObject(new GetObjectRequest("bucketName", "object", memoryStream, jobId, offset));
                memoryStream.Position = 0L;
                using (var reader = new StreamReader(memoryStream))
                {
                    Assert.AreEqual(stringResponse, reader.ReadToEnd());
                }
            }
        }

        [Test]
        public void TestGetObjects()
        {
            var xmlResponse = "<Data><S3Object><Latest>true</Latest><BucketId>b25a51b1-3dc8-4ecb-86d0-de334314e0a8</BucketId><CreationDate>2015-08-26 16:11:51.643</CreationDate><Id>82c8caee-9ad2-4c2c-912a-7d7f1dba850e</Id><Name>dont_get_around_much_anymore.mp4</Name><Type>DATA</Type><Version>1</Version></S3Object>"
                              +
                              "<S3Object><Latest>true</Latest><BucketId>b25a51b1-3dc8-4ecb-86d0-de334314e0a8</BucketId><CreationDate>2015-08-26 16:11:52.457</CreationDate><Id>7dad06d1-d492-497e-828d-ca8c53cf5e6d</Id><Name>is_you_is_or_is_you_aint.mp4</Name><Type>DATA</Type><Version>1</Version></S3Object>"
                              +
                              "<S3Object><Latest>true</Latest><BucketId>b25a51b1-3dc8-4ecb-86d0-de334314e0a8</BucketId><CreationDate>2015-08-26 16:11:52.012</CreationDate><Id>bcf24226-9c5e-423d-99fb-4939a61cd1fb</Id><Name>youre_just_in_love.mp4</Name><Type>DATA</Type><Version>1</Version></S3Object></Data>";
            var expected = new
            {
                Objects = new[]
                {
                    new S3Object
                    {
                        BucketId = Guid.Parse("b25a51b1-3dc8-4ecb-86d0-de334314e0a8"),
                        Name = "dont_get_around_much_anymore.mp4",
                        Id = Guid.Parse("82c8caee-9ad2-4c2c-912a-7d7f1dba850e"),
                        CreationDate = DateTime.Parse("2015-08-26 16:11:51.643"),
                        Type = S3ObjectType.DATA,
                        Version = 1L
                    },
                    new S3Object
                    {
                        BucketId = Guid.Parse("b25a51b1-3dc8-4ecb-86d0-de334314e0a8"),
                        Name = "is_you_is_or_is_you_aint.mp4",
                        Id = Guid.Parse("7dad06d1-d492-497e-828d-ca8c53cf5e6d"),
                        CreationDate = DateTime.Parse("2015-08-26 16:11:52.457"),
                        Type = S3ObjectType.DATA,
                        Version = 1L
                    },
                    new S3Object
                    {
                        BucketId = Guid.Parse("b25a51b1-3dc8-4ecb-86d0-de334314e0a8"),
                        Name = "youre_just_in_love.mp4",
                        Id = Guid.Parse("bcf24226-9c5e-423d-99fb-4939a61cd1fb"),
                        CreationDate = DateTime.Parse("2015-08-26 16:11:52.012"),
                        Type = S3ObjectType.DATA,
                        Version = 1L
                    }
                }
            };

            var expectedQueryParams = new Dictionary<string, string>
            {
                {"bucket_id", "videos"},
                {"name", "%mp4"}
            };
            var response = MockNetwork
                .Expecting(HttpVerb.GET, "/_rest_/object", expectedQueryParams, "")
                .Returning(HttpStatusCode.OK, xmlResponse, EmptyHeaders)
                .AsClient
                .GetObjectsDetailsSpectraS3(new GetObjectsDetailsSpectraS3Request()
                    .WithBucketId("videos")
                    .WithName("%mp4"));

            Assert.AreEqual(response.PagingTruncated, null);
            Assert.AreEqual(response.PagingTotalResultCount, null);

            var responseObjects = response.ResponsePayload.S3Objects.ToList();
            Assert.AreEqual(expected.Objects.Length, responseObjects.Count);
            for (var i = 0; i < expected.Objects.Length; i++)
            {
                Assert.AreEqual(expected.Objects[i].Name, responseObjects[i].Name);
                Assert.AreEqual(expected.Objects[i].CreationDate, responseObjects[i].CreationDate);
                Assert.AreEqual(expected.Objects[i].Id, responseObjects[i].Id);
                Assert.AreEqual(expected.Objects[i].BucketId, responseObjects[i].BucketId);
                Assert.AreEqual(expected.Objects[i].Type.ToString(), responseObjects[i].Type.ToString());
                Assert.AreEqual(expected.Objects[i].Version, responseObjects[i].Version);
            }
        }

        [Test]
        public void TestGetObjectsBadBucket()
        {
            var expectedQueryParams = new Dictionary<string, string>
            {
                {"bucketId", "badbucket"},
                {"name", "nofiles"}
            };
            var response = MockNetwork
                .Expecting(HttpVerb.GET, "/_rest_/object/", expectedQueryParams, "")
                .Returning(HttpStatusCode.NotFound, string.Empty, EmptyHeaders)
                .AsClient;
        }

        [Test]
        public void TestGetObjectsNoFiles()
        {
            const string xmlResponse = @"<Data></Data>";
            var expectedQueryParams = new Dictionary<string, string>
            {
                {"name", "nofiles"}
            };
            var response = MockNetwork
                .Expecting(HttpVerb.GET, "/_rest_/object/", expectedQueryParams, "")
                .Returning(HttpStatusCode.OK, xmlResponse, EmptyHeaders)
                .AsClient;
        }

        [Test]
        public void TestGetObjectsParseHeaders()
        {
            const string xmlResponse =
                "<Data><S3Object><Latest>true</Latest><BucketId>b25a51b1-3dc8-4ecb-86d0-de334314e0a8</BucketId><CreationDate>2015-08-26 16:11:51.643</CreationDate><Id>82c8caee-9ad2-4c2c-912a-7d7f1dba850e</Id><Name>dont_get_around_much_anymore.mp4</Name><Type>DATA</Type><Version>1</Version></S3Object>"
                +
                "<S3Object><Latest>true</Latest><BucketId>b25a51b1-3dc8-4ecb-86d0-de334314e0a8</BucketId><CreationDate>2015-08-26 16:11:52.457</CreationDate><Id>7dad06d1-d492-497e-828d-ca8c53cf5e6d</Id><Name>is_you_is_or_is_you_aint.mp4</Name><Type>DATA</Type><Version>1</Version></S3Object>"
                +
                "<S3Object><Latest>true</Latest><BucketId>b25a51b1-3dc8-4ecb-86d0-de334314e0a8</BucketId><CreationDate>2015-08-26 16:11:52.012</CreationDate><Id>bcf24226-9c5e-423d-99fb-4939a61cd1fb</Id><Name>youre_just_in_love.mp4</Name><Type>DATA</Type><Version>1</Version></S3Object></Data>";

            var expectedQueryParams = new Dictionary<string, string>
            {
                {"bucket_id", "videos"},
                {"name", "%mp4"}
            };

            var headers = new Dictionary<string, string>
            {
                {"page-truncated", "2"},
                {"total-result-count", "4"}
            };

            var response = MockNetwork
                .Expecting(HttpVerb.GET, "/_rest_/object", expectedQueryParams, "")
                .Returning(HttpStatusCode.OK, xmlResponse, headers)
                .AsClient
                .GetObjectsDetailsSpectraS3(new GetObjectsDetailsSpectraS3Request()
                    .WithBucketId("videos")
                    .WithName("%mp4"));

            Assert.AreEqual(response.PagingTruncated, 2);
            Assert.AreEqual(response.PagingTotalResultCount, 4);
        }

        [Test]
        public void TestGetService()
        {
            var id = "ef2fdcac-3c80-410a-8fcb-b567c31dd33d";
            var responseContent = "<ListAllMyBucketsResult><Owner><ID>" + id +
                                  "</ID><DisplayName>ryan</DisplayName></Owner><Buckets><Bucket>"
                                  +
                                  "<Name>testBucket2</Name><CreationDate>2013-12-11T23:20:09</CreationDate></Bucket><Bucket><Name>bulkTest</Name>"
                                  +
                                  "<CreationDate>2013-12-11T23:20:09</CreationDate></Bucket><Bucket><Name>bulkTest1</Name><CreationDate>2013-12-11T23:20:09</CreationDate>"
                                  +
                                  "</Bucket><Bucket><Name>bulkTest2</Name><CreationDate>2013-12-11T23:20:09</CreationDate></Bucket><Bucket><Name>bulkTest3</Name>"
                                  +
                                  "<CreationDate>2013-12-11T23:20:09</CreationDate></Bucket><Bucket><Name>bulkTest4</Name><CreationDate>2013-12-11T23:20:09</CreationDate>"
                                  +
                                  "</Bucket><Bucket><Name>bulkTest5</Name><CreationDate>2013-12-11T23:20:09</CreationDate></Bucket><Bucket><Name>bulkTest6</Name>"
                                  +
                                  "<CreationDate>2013-12-11T23:20:09</CreationDate></Bucket><Bucket><Name>testBucket3</Name><CreationDate>2013-12-11T23:20:09</CreationDate>"
                                  +
                                  "</Bucket><Bucket><Name>testBucket1</Name><CreationDate>2013-12-11T23:20:09</CreationDate></Bucket><Bucket><Name>testbucket</Name>"
                                  +
                                  "<CreationDate>2013-12-11T23:20:09</CreationDate></Bucket></Buckets></ListAllMyBucketsResult>";
            var expectedBuckets = new[]
            {
                new BucketDetails {Name = "testBucket2", CreationDate = DateTime.Parse("2013-12-11T23:20:09")},
                new BucketDetails {Name = "bulkTest", CreationDate = DateTime.Parse("2013-12-11T23:20:09")},
                new BucketDetails {Name = "bulkTest1", CreationDate = DateTime.Parse("2013-12-11T23:20:09")},
                new BucketDetails {Name = "bulkTest2", CreationDate = DateTime.Parse("2013-12-11T23:20:09")},
                new BucketDetails {Name = "bulkTest3", CreationDate = DateTime.Parse("2013-12-11T23:20:09")},
                new BucketDetails {Name = "bulkTest4", CreationDate = DateTime.Parse("2013-12-11T23:20:09")},
                new BucketDetails {Name = "bulkTest5", CreationDate = DateTime.Parse("2013-12-11T23:20:09")},
                new BucketDetails {Name = "bulkTest6", CreationDate = DateTime.Parse("2013-12-11T23:20:09")},
                new BucketDetails {Name = "testBucket3", CreationDate = DateTime.Parse("2013-12-11T23:20:09")},
                new BucketDetails {Name = "testBucket1", CreationDate = DateTime.Parse("2013-12-11T23:20:09")},
                new BucketDetails {Name = "testbucket", CreationDate = DateTime.Parse("2013-12-11T23:20:09")}
            };

            var response = MockNetwork
                .Expecting(HttpVerb.GET, "/", EmptyQueryParams, "")
                .Returning(HttpStatusCode.OK, responseContent, EmptyHeaders)
                .AsClient
                .GetService(new GetServiceRequest()).ResponsePayload;
            Assert.AreEqual("ryan", response.Owner.DisplayName);
            Assert.AreEqual(id, response.Owner.Id.ToString());

            Assert.AreEqual(expectedBuckets.Length, response.Buckets.ToList().Count);
            for (var i = 0; i < expectedBuckets.Length; i++)
            {
                Assert.AreEqual(expectedBuckets[i].Name, response.Buckets.ToList()[i].Name);
                Assert.AreEqual(expectedBuckets[i].CreationDate, response.Buckets.ToList()[i].CreationDate);
            }
        }

        [Test]
        public void TestGetSystemInfo()
        {
            var id = "ef2fdcac-3c80-410a-8fcb-b567c31dd33d";
            var xmlResponse = "<Data><ApiVersion>91C76B3B5B01A306A0DFA94C9EE3549A.767D11668247E20543EFC3B1C76117BA</ApiVersion>"
                              + "<BackendActivated>false</BackendActivated>"
                              +
                              "<BuildInformation><Branch>//BlueStorm/r1.x</Branch><Revision>1154042</Revision><Version>1.2.0</Version></BuildInformation>"
                              + "<InstanceId>" + id + "</InstanceId>"
                              + "<Now>0</Now>"
                              + "<SerialNumber>5003048001dbd7b3</SerialNumber></Data>";
            var expected = new GetSystemInformationSpectraS3Response(
                new SystemInformation
                {
                    ApiVersion = "91C76B3B5B01A306A0DFA94C9EE3549A.767D11668247E20543EFC3B1C76117BA",
                    BuildInformation = new BuildInformation
                    {
                        Branch = "//BlueStorm/r1.x",
                        Revision = "1154042",
                        Version = "1.2.0"
                    },
                    SerialNumber = "5003048001dbd7b3"
                });
            var response = MockNetwork
                .Expecting(HttpVerb.GET, "/_rest_/system_information", EmptyQueryParams, "")
                .Returning(HttpStatusCode.OK, xmlResponse, EmptyHeaders)
                .AsClient
                .GetSystemInformationSpectraS3(new GetSystemInformationSpectraS3Request());
            Assert.AreEqual(response.ResponsePayload.ApiVersion, expected.ResponsePayload.ApiVersion);
            Assert.AreEqual(response.ResponsePayload.BuildInformation.Branch,
                expected.ResponsePayload.BuildInformation.Branch);
            Assert.AreEqual(response.ResponsePayload.BuildInformation.Revision,
                expected.ResponsePayload.BuildInformation.Revision);
            Assert.AreEqual(response.ResponsePayload.BuildInformation.Version,
                expected.ResponsePayload.BuildInformation.Version);
            Assert.AreEqual(response.ResponsePayload.SerialNumber, expected.ResponsePayload.SerialNumber);
        }

        [Test]
        public void TestGetWorseService()
        {
            Assert.Throws<Ds3BadResponseException>(() => MockNetwork
                .Expecting(HttpVerb.GET, "/", EmptyQueryParams, "")
                .Returning(HttpStatusCode.OK, "", EmptyHeaders)
                .AsClient
                .GetService(new GetServiceRequest()));
        }

        [Test]
        public void TestNoPrefix()
        {
            const string testDir = "testnoprefix";
            RunPrefixTest(string.Empty, testDir);
        }

        [Test]
        public void TestPrefix()
        {
            var prefix = "prefix_";
            var testdir = "testprefix";
            RunPrefixTest(prefix, testdir);
        }


        [Test]
        public void TestPutBucket()
        {
            MockNetwork
                .Expecting(HttpVerb.PUT, "/bucketName", EmptyQueryParams, "")
                .Returning(HttpStatusCode.OK, "", EmptyHeaders)
                .AsClient
                .PutBucket(new PutBucketRequest("bucketName"));
        }

        [Test]
        public void TestPutObject()
        {
            var stringRequest = "object content";
            var jobId = Guid.Parse("e5d4adce-e170-4915-ba04-595fab30df81");
            var offset = 10L;
            var expectedQueryParams = new Dictionary<string, string>
            {
                {"job", "e5d4adce-e170-4915-ba04-595fab30df81"},
                {"offset", "10"}
            };
            MockNetwork
                .Expecting(HttpVerb.PUT, "/bucketName/object", expectedQueryParams, stringRequest)
                .Returning(HttpStatusCode.OK, stringRequest, EmptyHeaders)
                .AsClient
                .PutObject(new PutObjectRequest("bucketName", "object", HelpersForTest.StringToStream(stringRequest))
                    .WithJob(jobId)
                    .WithOffset(offset));
        }

        [Test]
        public void TestPutObjectWithCRC()
        {
            const string stringRequest = "object content";
            var jobId = Guid.Parse("e5d4adce-e170-4915-ba04-595fab30df81");
            const long offset = 10L;
            var expectedQueryParams = new Dictionary<string, string>
            {
                {"job", "e5d4adce-e170-4915-ba04-595fab30df81"},
                {"offset", "10"},
            };
            var expectedHeaders = new Dictionary<string, string>
            {
                {"Content-CRC32C", "4waSgw=="}
            };
            MockNetwork
                .Expecting(HttpVerb.PUT, "/bucketName/object", expectedQueryParams, stringRequest)
                .Returning(HttpStatusCode.OK, stringRequest, expectedHeaders)
                .AsClient
                .PutObject(new PutObjectRequest("bucketName", "object", HelpersForTest.StringToStream(stringRequest))
                    .WithJob(jobId)
                    .WithOffset(offset)
                    .WithChecksum(ChecksumType.Value(Convert.FromBase64String("4waSgw==")), ChecksumType.Type.CRC_32C));
        }

        [Test]
        public void TestPutObjectWithMetadata()
        {
            var stringRequest = "object content";
            var jobId = Guid.Parse("e5d4adce-e170-4915-ba04-595fab30df81");
            var offset = 10L;
            var expectedQueryParams = new Dictionary<string, string>
            {
                {"job", "e5d4adce-e170-4915-ba04-595fab30df81"},
                {"offset", "10"}
            };
            IDictionary<string, string> metadata = new Dictionary<string, string>
            {
                {"test1", "test1value"},
                {"test2", ""},
                {"test3", null}
            };
            IDictionary<string, string> expectedHeaders = new Dictionary<string, string>
            {
                {"Naming-Convention", "s3"},
                {"x-amz-meta-test1", "test1value"}
            };
            MockNetwork
                .Expecting(HttpVerb.PUT, "/bucketName/object", expectedQueryParams, expectedHeaders, stringRequest)
                .Returning(HttpStatusCode.OK, stringRequest, EmptyHeaders)
                .AsClient
                .PutObject(new PutObjectRequest("bucketName", "object", HelpersForTest.StringToStream(stringRequest))
                    .WithJob(jobId)
                    .WithOffset(offset)
                    .WithMetadata(metadata));
        }

        [Test]
        public void TestPutObjectWithPutObjectRequestStream()
        {
            var stringRequest = "object content";
            var jobId = Guid.Parse("e5d4adce-e170-4915-ba04-595fab30df81");
            var offset = 10L;
            var expectedQueryParams = new Dictionary<string, string>
            {
                {"job", "e5d4adce-e170-4915-ba04-595fab30df81"},
                {"offset", "10"}
            };

            var stream = HelpersForTest.StringToStream(stringRequest);
            var putObjectRequestStream = new Ds3.Helpers.Streams.ObjectRequestStream(stream, offset, stream.Length);
            MockNetwork
                .Expecting(HttpVerb.PUT, "/bucketName/object", expectedQueryParams, stringRequest)
                .Returning(HttpStatusCode.OK, stringRequest, EmptyHeaders)
                .AsClient
                .PutObject(new PutObjectRequest("bucketName", "object", putObjectRequestStream)
                    .WithJob(jobId)
                    .WithOffset(offset));
        }

        [Test]
        public void TestRedirectRetryCount()
        {
            var client = new Ds3Client(new Network(
                null, null, 2, 0, 0, 0, 0,
                (request, stream) =>
                        new MockWebRequest(new MockWebResponse("", HttpStatusCode.TemporaryRedirect, EmptyHeaders))));
            try
            {
                client.VerifySystemHealthSpectraS3(new VerifySystemHealthSpectraS3Request());
            }
            catch (Ds3RedirectLimitException e)
            {
                Assert.AreEqual(2, e.Retries);
            }
        }

        [Test]
        public void TestStartWriteJobWithDs3MaxJobsException()
        {
            var queryParams = new Dictionary<string, string> {{"operation", "start_bulk_put"}};
            const string stringRequest = "<Objects><Object Name=\"obj1\" Size=\"1234\" /></Objects>";
            var mockClient = MockNetwork
                .Expecting(HttpVerb.PUT, "/_rest_/bucket/bucket", queryParams, stringRequest)
                .Returning(HttpStatusCode.ServiceUnavailable, "", EmptyHeaders)
                .AsClient;

            var files = new[]
            {
                new {Key = "obj1", Size = 1234L}
            };
            var inputObjects = files.Select(f => new Ds3Object(f.Key, f.Size)).ToList();

            var helpers = new Ds3ClientHelpers(mockClient, jobRetries: 3, jobWaitTime: 0);

            Assert.Throws<Ds3MaxJobsException>(() => helpers.StartWriteJob("bucket", inputObjects));
        }

        [Test]
        public void TestVerifySystemHealth()
        {
            const string xmlResponse = @"<Data><MsRequiredToVerifyDataPlannerHealth>666</MsRequiredToVerifyDataPlannerHealth><DatabaseFilesystemFreeSpace>NORMAL</DatabaseFilesystemFreeSpace></Data>";
            var healthVerificationResult = new HealthVerificationResult {MsRequiredToVerifyDataPlannerHealth = 666L};
            var expected = new VerifySystemHealthSpectraS3Response(healthVerificationResult);
            var response = MockNetwork
                .Expecting(HttpVerb.GET, "/_rest_/system_health", EmptyQueryParams, "")
                .Returning(HttpStatusCode.OK, xmlResponse, EmptyHeaders)
                .AsClient
                .VerifySystemHealthSpectraS3(new VerifySystemHealthSpectraS3Request());
            Assert.AreEqual(
                response.ResponsePayload.MsRequiredToVerifyDataPlannerHealth,
                expected.ResponsePayload.MsRequiredToVerifyDataPlannerHealth);
        }

        [Test]
        public void TestVerifySystemHealthConnectFail()
        {
            Assert.Throws<Ds3BadStatusCodeException>(() => MockNetwork
                .Expecting(HttpVerb.GET, "/_rest_/system_health", EmptyQueryParams, "")
                .Returning(HttpStatusCode.ServiceUnavailable, "", EmptyHeaders)
                .AsClient
                .VerifySystemHealthSpectraS3(new VerifySystemHealthSpectraS3Request()));
        }

        [Test]
        public void TestPutMultiPartUploadPart()
        {
            const string expectedRequestContent = "this is the part content";
            const string bucketName = "BucketName";
            const string objectName = "ObjectName";
            const int partNumber = 2;
            const string uploadId = "VXBsb2FkIElEIGZvciA2aWWpbmcncyBteS1tb3ZpZS5tMnRzIHVwbG9hZA";
            const string eTag = "b54357faf0632cce46e942fa68356b38";

            var queryParams = new Dictionary<string, string> {
                { "part_number", partNumber.ToString() },
                { "upload_id", uploadId }
            };

            var headers = new Dictionary<string, string> { { "eTag", eTag } };

            MockNetwork
                .Expecting(HttpVerb.PUT, "/BucketName/ObjectName", queryParams, expectedRequestContent)
                .Returning(HttpStatusCode.OK, "", headers)
                .AsClient
                .PutMultiPartUploadPart(new PutMultiPartUploadPartRequest(
                    bucketName, 
                    objectName, 
                    partNumber, 
                    HelpersForTest.StringToStream(expectedRequestContent), 
                    uploadId));
        }
        
        [Test]
        public void TestDeleteObjects()
        {
            const string expectedRequestContent = "<Delete><Object><Key>obj1</Key></Object><Object><Key>obj2</Key></Object><Object><Key>obj3</Key></Object></Delete>";
            const string responsePayload = "<DeleteResult><Deleted><Key>obj1</Key></Deleted><Error><Key>obj2</Key><Key>obj3</Key><Code>AccessDenied</Code><Message>Access Denied</Message></Error></DeleteResult>";
            const string bucketName = "BucketName";

            var objects = new List<Ds3Object> {
                new Ds3Object("obj1", null),
                new Ds3Object("obj2", null),
                new Ds3Object("obj3", null)
            };

            var queryParams = new Dictionary<string, string> { { "delete", null } };

            MockNetwork
                .Expecting(HttpVerb.POST, "/" + bucketName, queryParams, expectedRequestContent)
                .Returning(HttpStatusCode.OK, responsePayload, EmptyHeaders)
                .AsClient
                .DeleteObjects(new DeleteObjectsRequest(bucketName, objects));
        }

        [Test]
        public void TestClearSuspectBobAzureTargets()
        {
            MockNetwork
                .Expecting(HttpVerb.DELETE, "/_rest_/suspect_blob_azure_target", EmptyQueryParams, IdsRequestPayload)
                .Returning(HttpStatusCode.NoContent, "", EmptyHeaders)
                .AsClient
                .ClearSuspectBlobAzureTargetsSpectraS3(new ClearSuspectBlobAzureTargetsSpectraS3Request(IdsRequestPayloadList));
        }

        [Test]
        public void TestClearSuspectBobPools()
        {
            MockNetwork
                .Expecting(HttpVerb.DELETE, "/_rest_/suspect_blob_pool", EmptyQueryParams, IdsRequestPayload)
                .Returning(HttpStatusCode.NoContent, "", EmptyHeaders)
                .AsClient
                .ClearSuspectBlobPoolsSpectraS3(new ClearSuspectBlobPoolsSpectraS3Request(IdsRequestPayloadList));
        }

        [Test]
        public void TestClearSuspectBobS3Targets()
        {
            MockNetwork
                .Expecting(HttpVerb.DELETE, "/_rest_/suspect_blob_s3_target", EmptyQueryParams, IdsRequestPayload)
                .Returning(HttpStatusCode.NoContent, "", EmptyHeaders)
                .AsClient
                .ClearSuspectBlobS3TargetsSpectraS3(new ClearSuspectBlobS3TargetsSpectraS3Request(IdsRequestPayloadList));
        }

        [Test]
        public void TestClearSuspectBobTapes()
        {
            MockNetwork
                .Expecting(HttpVerb.DELETE, "/_rest_/suspect_blob_tape", EmptyQueryParams, IdsRequestPayload)
                .Returning(HttpStatusCode.NoContent, "", EmptyHeaders)
                .AsClient
                .ClearSuspectBlobTapesSpectraS3(new ClearSuspectBlobTapesSpectraS3Request(IdsRequestPayloadList));
        }

        [Test]
        public void TestMarkSuspectBlobAzureTargetsAsDegraded()
        {
            MockNetwork
                .Expecting(HttpVerb.PUT, "/_rest_/suspect_blob_azure_target", EmptyQueryParams, IdsRequestPayload)
                .Returning(HttpStatusCode.NoContent, "", EmptyHeaders)
                .AsClient
                .MarkSuspectBlobAzureTargetsAsDegradedSpectraS3(new MarkSuspectBlobAzureTargetsAsDegradedSpectraS3Request(IdsRequestPayloadList));
        }

        [Test]
        public void TestMarkSuspectBlobDs3TargetsTargetsAsDegraded()
        {
            MockNetwork
                .Expecting(HttpVerb.PUT, "/_rest_/suspect_blob_ds3_target", EmptyQueryParams, IdsRequestPayload)
                .Returning(HttpStatusCode.NoContent, "", EmptyHeaders)
                .AsClient
                .MarkSuspectBlobDs3TargetsAsDegradedSpectraS3(new MarkSuspectBlobDs3TargetsAsDegradedSpectraS3Request(IdsRequestPayloadList));
        }

        [Test]
        public void TestMarkSuspectBlobPoolsAsDegraded()
        {
            MockNetwork
                .Expecting(HttpVerb.PUT, "/_rest_/suspect_blob_pool", EmptyQueryParams, IdsRequestPayload)
                .Returning(HttpStatusCode.NoContent, "", EmptyHeaders)
                .AsClient
                .MarkSuspectBlobPoolsAsDegradedSpectraS3(new MarkSuspectBlobPoolsAsDegradedSpectraS3Request(IdsRequestPayloadList));
        }

        [Test]
        public void TestMarkSuspectBlobS3TargetsAsDegraded()
        {
            MockNetwork
                .Expecting(HttpVerb.PUT, "/_rest_/suspect_blob_s3_target", EmptyQueryParams, IdsRequestPayload)
                .Returning(HttpStatusCode.NoContent, "", EmptyHeaders)
                .AsClient
                .MarkSuspectBlobS3TargetsAsDegradedSpectraS3(new MarkSuspectBlobS3TargetsAsDegradedSpectraS3Request(IdsRequestPayloadList));
        }

        [Test]
        public void TestMarkSuspectBlobTapesAsDegraded()
        {
            MockNetwork
                .Expecting(HttpVerb.PUT, "/_rest_/suspect_blob_tape", EmptyQueryParams, IdsRequestPayload)
                .Returning(HttpStatusCode.NoContent, "", EmptyHeaders)
                .AsClient
                .MarkSuspectBlobTapesAsDegradedSpectraS3(new MarkSuspectBlobTapesAsDegradedSpectraS3Request(IdsRequestPayloadList));
        }

        [Test]
        public void TestGetBulkJobWithObjectNames()
        {
            const string responsePayload = "<MasterObjectList Aggregating=\"false\" BucketName=\"default_bucket_name\" CachedSizeInBytes=\"0\" ChunkClientProcessingOrderGuarantee=\"IN_ORDER\" CompletedSizeInBytes=\"0\" EntirelyInCache=\"false\" JobId=\"1e66c043-e741-436a-8f5c-561320922fda\" Naked=\"false\" Name=\"GET by null\" OriginalSizeInBytes=\"0\" Priority=\"LOW\" RequestType=\"GET\" StartDate=\"2017-03-23T23:24:06.000Z\" Status=\"IN_PROGRESS\" UserId=\"fcc976f8-afda-4a3c-a4f8-565cea8b9c08\" UserName=\"default_user_name\"><Nodes><Node EndPoint=\"NOT_INITIALIZED_YET\" Id=\"acda9183-9b30-4de6-88cc-3f073051e978\"/></Nodes><Objects ChunkId=\"5aaa294b-45b0-458d-92a2-a6ca0ae6068c\" ChunkNumber=\"1\"><Object Id=\"0b56d39c-5711-4d9f-b161-c730b3acf1ae\" InCache=\"false\" Latest=\"true\" Length=\"10\" Name=\"o2\" Offset=\"0\" Version=\"1\"/></Objects><Objects ChunkId=\"80f5f6f2-a3e4-4b15-ac68-c0184eed38f2\" ChunkNumber=\"2\"><Object Id=\"5008ebef-95fa-4cf6-9be0-88d0ed20f450\" InCache=\"false\" Latest=\"true\" Length=\"10\" Name=\"o1\" Offset=\"0\" Version=\"1\"/></Objects></MasterObjectList>";
            const string bucketName = "BucketName";

            var queryParams = new Dictionary<string, string> { { "operation", "start_bulk_get" } };

            MockNetwork
                .Expecting(HttpVerb.PUT, "/_rest_/bucket/" + bucketName, queryParams, SimpleDs3ObjectPayload)
                .Returning(HttpStatusCode.OK, responsePayload, EmptyHeaders)
                .AsClient
                .GetBulkJobSpectraS3(new GetBulkJobSpectraS3Request(bucketName, SimpleDs3Objects));
        }

        [Test]
        public void TestGetBulkJobWithPartialObjects()
        {
            const string expectedRequestPayload = "<Objects><Object Name=\"obj1\" Offset=\"0\" Length=\"100\" /><Object Name=\"obj2\" Offset=\"0\" Length=\"199\" /><Object Name=\"obj2\" Offset=\"200\" Length=\"100\" /></Objects>";
            const string responsePayload = "<MasterObjectList Aggregating=\"false\" BucketName=\"default_bucket_name\" CachedSizeInBytes=\"0\" ChunkClientProcessingOrderGuarantee=\"IN_ORDER\" CompletedSizeInBytes=\"0\" EntirelyInCache=\"false\" JobId=\"1e66c043-e741-436a-8f5c-561320922fda\" Naked=\"false\" Name=\"GET by null\" OriginalSizeInBytes=\"0\" Priority=\"LOW\" RequestType=\"GET\" StartDate=\"2017-03-23T23:24:06.000Z\" Status=\"IN_PROGRESS\" UserId=\"fcc976f8-afda-4a3c-a4f8-565cea8b9c08\" UserName=\"default_user_name\"><Nodes><Node EndPoint=\"NOT_INITIALIZED_YET\" Id=\"acda9183-9b30-4de6-88cc-3f073051e978\"/></Nodes><Objects ChunkId=\"5aaa294b-45b0-458d-92a2-a6ca0ae6068c\" ChunkNumber=\"1\"><Object Id=\"0b56d39c-5711-4d9f-b161-c730b3acf1ae\" InCache=\"false\" Latest=\"true\" Length=\"10\" Name=\"o2\" Offset=\"0\" Version=\"1\"/></Objects><Objects ChunkId=\"80f5f6f2-a3e4-4b15-ac68-c0184eed38f2\" ChunkNumber=\"2\"><Object Id=\"5008ebef-95fa-4cf6-9be0-88d0ed20f450\" InCache=\"false\" Latest=\"true\" Length=\"10\" Name=\"o1\" Offset=\"0\" Version=\"1\"/></Objects></MasterObjectList>";
            const string bucketName = "BucketName";
            var partialObjects = new List<Ds3PartialObject> {
                new Ds3PartialObject(Range.ByLength(0, 100), "obj1"),
                new Ds3PartialObject(Range.ByLength(0, 199), "obj2"),
                new Ds3PartialObject(Range.ByLength(200, 100), "obj2")
            };

            var queryParams = new Dictionary<string, string> { { "operation", "start_bulk_get" } };

            MockNetwork
                .Expecting(HttpVerb.PUT, "/_rest_/bucket/" + bucketName, queryParams, expectedRequestPayload)
                .Returning(HttpStatusCode.OK, responsePayload, EmptyHeaders)
                .AsClient
                .GetBulkJobSpectraS3(new GetBulkJobSpectraS3Request(bucketName, new List<string>(), partialObjects));
        }

        [Test]
        public void TestGetBulkJobWithMixedObjects()
        {
            const string expectedRequestPayload = "<Objects><Object Name=\"obj1\" /><Object Name=\"obj2\" Offset=\"2\" Length=\"20\" /><Object Name=\"obj3\" Offset=\"3\" Length=\"30\" /></Objects>";
            const string responsePayload = "<MasterObjectList Aggregating=\"false\" BucketName=\"default_bucket_name\" CachedSizeInBytes=\"0\" ChunkClientProcessingOrderGuarantee=\"IN_ORDER\" CompletedSizeInBytes=\"0\" EntirelyInCache=\"false\" JobId=\"1e66c043-e741-436a-8f5c-561320922fda\" Naked=\"false\" Name=\"GET by null\" OriginalSizeInBytes=\"0\" Priority=\"LOW\" RequestType=\"GET\" StartDate=\"2017-03-23T23:24:06.000Z\" Status=\"IN_PROGRESS\" UserId=\"fcc976f8-afda-4a3c-a4f8-565cea8b9c08\" UserName=\"default_user_name\"><Nodes><Node EndPoint=\"NOT_INITIALIZED_YET\" Id=\"acda9183-9b30-4de6-88cc-3f073051e978\"/></Nodes><Objects ChunkId=\"5aaa294b-45b0-458d-92a2-a6ca0ae6068c\" ChunkNumber=\"1\"><Object Id=\"0b56d39c-5711-4d9f-b161-c730b3acf1ae\" InCache=\"false\" Latest=\"true\" Length=\"10\" Name=\"o2\" Offset=\"0\" Version=\"1\"/></Objects><Objects ChunkId=\"80f5f6f2-a3e4-4b15-ac68-c0184eed38f2\" ChunkNumber=\"2\"><Object Id=\"5008ebef-95fa-4cf6-9be0-88d0ed20f450\" InCache=\"false\" Latest=\"true\" Length=\"10\" Name=\"o1\" Offset=\"0\" Version=\"1\"/></Objects></MasterObjectList>";
            const string bucketName = "BucketName";
            var partialObjects = new List<Ds3PartialObject> {
                new Ds3PartialObject(Range.ByLength(2, 20), "obj2"),
                new Ds3PartialObject(Range.ByLength(3, 30), "obj3")
            };

            var fullObjects = new List<string> { "obj1" };

            var queryParams = new Dictionary<string, string> { { "operation", "start_bulk_get" } };

            MockNetwork
                .Expecting(HttpVerb.PUT, "/_rest_/bucket/" + bucketName, queryParams, expectedRequestPayload)
                .Returning(HttpStatusCode.OK, responsePayload, EmptyHeaders)
                .AsClient
                .GetBulkJobSpectraS3(new GetBulkJobSpectraS3Request(bucketName, fullObjects, partialObjects));
        }

        [Test]
        public void TestGetPhysicalPlacementForObjects()
        {
            const string responsePayload = "<Data><AzureTargets/><Ds3Targets/><Pools/><S3Targets/><Tapes/></Data>";
            const string bucketName = "BucketName";

            var queryParams = new Dictionary<string, string> { { "operation", "get_physical_placement" } };

            MockNetwork
                .Expecting(HttpVerb.PUT, "/_rest_/bucket/" + bucketName, queryParams, SimpleDs3ObjectPayload)
                .Returning(HttpStatusCode.OK, responsePayload, EmptyHeaders)
                .AsClient
                .GetPhysicalPlacementForObjectsSpectraS3(new GetPhysicalPlacementForObjectsSpectraS3Request(bucketName, SimpleDs3Objects));
        }

        [Test]
        public void TestGetPhysicalPlacementForObjectsWithFullDetails()
        {
            const string responsePayload = "<Data><Object Bucket=\"b1\" Id=\"a2897bbd-3e0b-4c0f-83d7-29e1e7669bdd\" InCache=\"false\" Latest=\"true\" Length=\"10\" Name=\"o4\" Offset=\"0\" Version=\"1\"><PhysicalPlacement><AzureTargets/><Ds3Targets/><Pools/><S3Targets/><Tapes/></PhysicalPlacement></Object></Data>";
            const string bucketName = "BucketName";

            var queryParams = new Dictionary<string, string> {
                { "operation", "get_physical_placement" },
                { "full_details", null }
            };

            MockNetwork
                .Expecting(HttpVerb.PUT, "/_rest_/bucket/" + bucketName, queryParams, SimpleDs3ObjectPayload)
                .Returning(HttpStatusCode.OK, responsePayload, EmptyHeaders)
                .AsClient
                .GetPhysicalPlacementForObjectsWithFullDetailsSpectraS3(new GetPhysicalPlacementForObjectsWithFullDetailsSpectraS3Request(bucketName, SimpleDs3Objects));
        }

        [Test]
        public void TestVerifyPhysicalPlacementForObjects()
        {
            const string responsePayload = "<Data><AzureTargets/><Ds3Targets/><Pools/><S3Targets/><Tapes><Tape><AssignedToStorageDomain>false</AssignedToStorageDomain><AvailableRawCapacity>10000</AvailableRawCapacity><BarCode>t1</BarCode><BucketId/><DescriptionForIdentification/><EjectDate/><EjectLabel/><EjectLocation/><EjectPending/><FullOfData>false</FullOfData><Id>48d30ecb-84f1-4721-9832-7aa165a1dd77</Id><LastAccessed/><LastCheckpoint/><LastModified/><LastVerified/><PartiallyVerifiedEndOfTape/><PartitionId>76343269-c32a-4cb0-aec4-57a9dccce6ea</PartitionId><PreviousState/><SerialNumber/><State>PENDING_INSPECTION</State><StorageDomainId/><TakeOwnershipPending>false</TakeOwnershipPending><TotalRawCapacity>20000</TotalRawCapacity><Type>LTO5</Type><VerifyPending/><WriteProtected>false</WriteProtected></Tape></Tapes></Data>";
            const string bucketName = "BucketName";

            var queryParams = new Dictionary<string, string> { { "operation", "verify_physical_placement" } };

            MockNetwork
                .Expecting(HttpVerb.GET, "/_rest_/bucket/" + bucketName, queryParams, SimpleDs3ObjectPayload)
                .Returning(HttpStatusCode.OK, responsePayload, EmptyHeaders)
                .AsClient
                .VerifyPhysicalPlacementForObjectsSpectraS3(new VerifyPhysicalPlacementForObjectsSpectraS3Request(bucketName, SimpleDs3Objects));
        }

        [Test]
        public void TestVerifyPhysicalPlacementForObjectsWithFullDetails()
        {
            const string responsePayload = "<Data><Object Bucket=\"b1\" Id=\"15ad85a5-aab6-4d85-bf33-831bcba13b8e\" InCache=\"false\" Latest=\"true\" Length=\"10\" Name=\"o1\" Offset=\"0\" Version=\"1\"><PhysicalPlacement><AzureTargets/><Ds3Targets/><Pools/><S3Targets/><Tapes><Tape><AssignedToStorageDomain>false</AssignedToStorageDomain><AvailableRawCapacity>10000</AvailableRawCapacity><BarCode>t1</BarCode><BucketId/><DescriptionForIdentification/><EjectDate/><EjectLabel/><EjectLocation/><EjectPending/><FullOfData>false</FullOfData><Id>5a7bb215-4aff-4806-b217-5fe01ade6a2c</Id><LastAccessed/><LastCheckpoint/><LastModified/><LastVerified/><PartiallyVerifiedEndOfTape/><PartitionId>2e5b25fc-546e-45b0-951e-8f3d80bb7823</PartitionId><PreviousState/><SerialNumber/><State>PENDING_INSPECTION</State><StorageDomainId/><TakeOwnershipPending>false</TakeOwnershipPending><TotalRawCapacity>20000</TotalRawCapacity><Type>LTO5</Type><VerifyPending/><WriteProtected>false</WriteProtected></Tape></Tapes></PhysicalPlacement></Object></Data>";
            const string bucketName = "BucketName";

            var queryParams = new Dictionary<string, string> {
                { "operation", "verify_physical_placement" },
                { "full_details", null }
            };

            MockNetwork
                .Expecting(HttpVerb.GET, "/_rest_/bucket/" + bucketName, queryParams, SimpleDs3ObjectPayload)
                .Returning(HttpStatusCode.OK, responsePayload, EmptyHeaders)
                .AsClient
                .VerifyPhysicalPlacementForObjectsWithFullDetailsSpectraS3(new VerifyPhysicalPlacementForObjectsWithFullDetailsSpectraS3Request(bucketName, SimpleDs3Objects));
        }

        /* TODO uncomment once JIRA SA-208 has been fixed
        [Test]
        public void TestEjectStorageDomainBlobs()
        {
            const string bucketId = "BucketId";
            const string storageDomainId = "StorageDomainId";

            var queryParams = new Dictionary<string, string> {
                { "operation", "eject" },
                { "blobs", null },
                { "bucket_id", bucketId },
                { "storage_domainId", storageDomainId }
            };

            MockNetwork
                .Expecting(HttpVerb.GET, "/_rest_/tape", queryParams, SimpleDs3ObjectPayload)
                .Returning(HttpStatusCode.NoContent, "", EmptyHeaders)
                .AsClient
                .EjectStorageDomainBlobsSpectraS3(new EjectStorageDomainBlobsSpectraS3Request(bucketId, SimpleDs3Objects, storageDomainId));
        }
        */

        [Test]
        public void TestReplicatePutJob()
        {
            const string responsePayload = "<MasterObjectList Aggregating=\"false\" BucketName=\"existing_bucket\" CachedSizeInBytes=\"0\" ChunkClientProcessingOrderGuarantee=\"IN_ORDER\" CompletedSizeInBytes=\"0\" EntirelyInCache=\"false\" JobId=\"95dcda9b-26d2-4b95-87e2-36ac217d7230\" Naked=\"false\" Name=\"Replicate Untitled\" OriginalSizeInBytes=\"10\" Priority=\"NORMAL\" RequestType=\"PUT\" StartDate=\"2017-03-23T23:24:24.000Z\" Status=\"IN_PROGRESS\" UserId=\"1dc9953a-c778-4cdd-b217-2a6b325cde5e\" UserName=\"test_user\"><Nodes><Node EndPoint=\"NOT_INITIALIZED_YET\" Id=\"782ee70f-692e-4240-8ee1-c049b3a7b91e\"/></Nodes><Objects ChunkId=\"33a7ed12-d7b7-4f85-ac67-b3a2834170cc\" ChunkNumber=\"1\"><Object Id=\"eee15242-d7c1-44dc-b352-811adc6e5c0e\" InCache=\"false\" Latest=\"true\" Length=\"10\" Name=\"o1\" Offset=\"0\" Version=\"1\"/></Objects></MasterObjectList>";
            const string requestPayload = "This is the request payload content";
            const string bucketName = "BucketName";

            var queryParams = new Dictionary<string, string> {
                { "operation", "start_bulk_put" },
                { "replicate", null }
            };

            MockNetwork
                .Expecting(HttpVerb.PUT, "/_rest_/bucket/" + bucketName, queryParams, requestPayload)
                .Returning(HttpStatusCode.OK, responsePayload, EmptyHeaders)
                .AsClient
                .ReplicatePutJobSpectraS3(new ReplicatePutJobSpectraS3Request(bucketName, requestPayload));
        }

        [Test]
        public void TestGetBlobPersistenceRequest()
        {
            const string responsePayload = "This is the response payload content";
            const string requestPayload = "This is the request payload content";

            MockNetwork
                .Expecting(HttpVerb.GET, "/_rest_/blob_persistence", EmptyQueryParams, requestPayload)
                .Returning(HttpStatusCode.OK, responsePayload, EmptyHeaders)
                .AsClient
                .GetBlobPersistenceSpectraS3(new GetBlobPersistenceSpectraS3Request(requestPayload));
        }

        [Test]
        public void TestCompleteMultiPartUpload()
        {
            const string expectedRequestPayload = "<CompleteMultipartUpload><Part><PartNumber>1</PartNumber><ETag>eTag1</ETag></Part><Part><PartNumber>2</PartNumber><ETag>eTag2</ETag></Part><Part><PartNumber>3</PartNumber><ETag>eTag3</ETag></Part></CompleteMultipartUpload>";
            const string responsePayload = "<CompleteMultipartUploadResult><Bucket>BucketName</Bucket><ETag>98d476a1d168aa4c4592ce06cab9880f-3</ETag><Key>test_object</Key><Location>9c49ab34-8d9e-4f4b-9c33-2c90b7d87c7d</Location></CompleteMultipartUploadResult>";
            const string bucketName = "BucketName";
            const string objectName = "ObjectName";
            const string uploadId = "UploadId";

            var queryParams = new Dictionary<string, string> { { "upload_id", uploadId } };

            var parts = new List<Part>
            {
                new Part(1, "eTag1"),
                new Part(2, "eTag2"),
                new Part(3, "eTag3")
            };
            
            var result = MockNetwork.Expecting(HttpVerb.POST, "/" + bucketName + "/" + objectName, queryParams, expectedRequestPayload)
                .Returning(HttpStatusCode.OK, responsePayload, EmptyHeaders)
                .AsClient
                .CompleteMultiPartUpload(new CompleteMultiPartUploadRequest(bucketName, objectName, parts, uploadId));

            Assert.AreEqual(result.ResponsePayload.Bucket, bucketName);
        }
    }
}