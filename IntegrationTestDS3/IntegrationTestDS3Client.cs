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
using Ds3.Helpers.Streams;
using Ds3.Models;
using IntegrationTestDS3;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Ds3.Helpers.Strategys;

// using TestDs3.Lang;

namespace IntegrationTestDs3
{
    [TestFixture]
    public class IntegrationTestDs3Client
    {
        private static readonly IDictionary<string, string> _emptyHeaders = new Dictionary<string, string>();
        private static readonly IDictionary<string, string> _emptyQueryParams = new Dictionary<string, string>();
        private string[] BOOKS = { "beowulf.txt", "sherlock_holmes.txt", "tale_of_two_cities.txt" };
        private string[] JOYCEBOOKS = { "ulysses.txt" };
        private string[] BIGFILES = { "big_book_file.txt" };

        private static string TESTDIR = "TestObjectData";
        private static string PREFIX = "test_";
        private static string FOLDER = "joyce";
        private static string BIG = "big";
        private static string BIGFORMAXBLOB = "bigForMaxBlob";
        private const long BlobSize = 10 * 1024 * 1024;

        private string testDirectorySrc { get; set; }
        private string testDirectoryBigFolder { get; set; }
        private string testDirectoryBigFolderForMaxBlob { get; set; }
        private string testDirectoryDest { get; set; }
        private string testDirectoryDestPrefix { get; set; }

        private IDs3Client _client { get; set; }
        private Ds3ClientHelpers _helpers { get; set; }

        public string BuildRev { private set; get; }
        public string BuildVersion { private set; get; }
        public string BuildBranch { private set; get; }

        #region setup

        [TestFixtureSetUp]
        public void Startup()
        {
            _client = Ds3TestUtils.CreateClient();
            _helpers = new Ds3ClientHelpers(_client);

            SetupTestData();
        }

        private void SetupTestData()
        {
            var root = Path.GetTempPath() + TESTDIR + Path.DirectorySeparatorChar;
            testDirectorySrc = root + "src" + Path.DirectorySeparatorChar;
            var testDirectorySrcFolder = root + "src" + Path.DirectorySeparatorChar + FOLDER + Path.DirectorySeparatorChar;
            testDirectoryDest = root + "dest" + Path.DirectorySeparatorChar;
            testDirectoryDestPrefix = root + "destPrefix" + Path.DirectorySeparatorChar;

            testDirectoryBigFolder = root + BIG + Path.DirectorySeparatorChar;
            testDirectoryBigFolderForMaxBlob = root + BIGFORMAXBLOB + Path.DirectorySeparatorChar;

            // create and populate a new test dir
            if (Directory.Exists(root))
            {
                Directory.Delete(root, true);
            }
            Directory.CreateDirectory(root);
            Directory.CreateDirectory(testDirectorySrc);
            Directory.CreateDirectory(testDirectorySrcFolder);
            Directory.CreateDirectory(testDirectoryDest);
            Directory.CreateDirectory(testDirectoryDestPrefix);
            Directory.CreateDirectory(testDirectoryBigFolder);
            Directory.CreateDirectory(testDirectoryBigFolderForMaxBlob);

            foreach (var book in BOOKS)
            {
                var writer = File.OpenWrite(testDirectorySrc + book);
                using (var bookstream = ReadResource("IntegrationTestDS3.TestData." + book))
                {
                    bookstream.CopyTo(writer);
                    bookstream.Seek(0, SeekOrigin.Begin);
                }
                writer.Close();
            }
            foreach (var book in JOYCEBOOKS)
            {
                var writer = File.OpenWrite(testDirectorySrcFolder + book);
                using (var bookstream = ReadResource("IntegrationTestDS3.TestData." + book))
                {
                    bookstream.CopyTo(writer);
                    bookstream.Seek(0, SeekOrigin.Begin);
                }
                writer.Close();
            }
            foreach (var bigFile in BIGFILES)
            {
                var writer = File.OpenWrite(testDirectoryBigFolder + bigFile);
                var writerForMaxBlob = File.OpenWrite(testDirectoryBigFolderForMaxBlob + bigFile + "_maxBlob");

                using (var bookstream = ReadResource("IntegrationTestDS3.TestData." + bigFile))
                {
                    bookstream.CopyTo(writer);
                    bookstream.Seek(0, SeekOrigin.Begin);
                    bookstream.CopyTo(writerForMaxBlob);
                    bookstream.Seek(0, SeekOrigin.Begin);
                }

                writer.Close();
                writerForMaxBlob.Close();
            }
        }

        private static Stream ReadResource(string resourceName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName);
        }

        #endregion setup

        #region ping

        [Test]
        public void DoIntegrationPing()
        {
            var request = new VerifySystemHealthSpectraS3Request();
            var response = _client.VerifySystemHealthSpectraS3(request);

            Assert.GreaterOrEqual(response.ResponsePayload.MsRequiredToVerifyDataPlannerHealth, 0);
        }

        [Test]
        public void GetSystemInfo()
        {
            // get valid data and populate properties
            var request = new GetSystemInformationSpectraS3Request();
            var response = _client.GetSystemInformationSpectraS3(request).ResponsePayload;
            Assert.IsNotNullOrEmpty(response.BuildInformation.Version);
            BuildBranch = response.BuildInformation.Branch;
            BuildRev = response.BuildInformation.Revision;
            BuildVersion = response.BuildInformation.Version;
        }

        #endregion ping

        #region independent tests

        /* Assume SetUp has completed
        ** but does not rely on others
        **/

        [Test]
        public void TestPutWithChecksum()
        {
            const string bucketName = "TestPutWithChecksum";

            //This test is supported only for BP 1.2
            if (!IsTestSupported(_client, 1.2))
            {
                Assert.Ignore();
            }

            try
            {
                // grab a file
                var directoryObjects = FileHelpers.ListObjectsForDirectory(testDirectorySrc);
                Assert.IsNotEmpty(directoryObjects);
                var testObject = directoryObjects.First();
                var ds3Objs = new List<Ds3Object> { testObject };

                // create or ensure bucket
                _helpers.EnsureBucketExists(bucketName);
                // create a job
                var job = _helpers.StartWriteJob(bucketName, ds3Objs);

                // instantiate a PutObjectRequest
                var fs = File.Open(testDirectorySrc + Path.DirectorySeparatorChar + testObject.Name,
                    FileMode.Open);
                var putRequest = new PutObjectRequest(bucketName, testObject.Name, fs)
                    .WithJob(job.JobId)
                    .WithOffset(0L);
                putRequest.WithChecksum(ChecksumType.Compute, ChecksumType.Type.CRC_32C);
                _client.PutObject(putRequest);
                fs.Close();
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(_client, bucketName);
            }
        }

        [Test]
        public void TestPutWithSuppliedChecksum()
        {
            const string bucketName = "TestPutWithSuppliedChecksum";
            //This test is supported only for BP 1.2
            if (!IsTestSupported(_client, 1.2))
            {
                Assert.Ignore();
            }

            try
            {
                // "123456789" has a well known checksum
                const string content = "123456789";
                const string testChecksumCrc32C = "4waSgw==";

                var testObject = new Ds3Object("numbers.txt", 9);
                var ds3Objs = new List<Ds3Object> { testObject };

                using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content)))
                {
                    // create or ensure bucket
                    _helpers.EnsureBucketExists(bucketName);
                    // create a job
                    var job = _helpers.StartWriteJob(bucketName, ds3Objs);
                    var putRequest = new PutObjectRequest(bucketName, testObject.Name, stream)
                        .WithJob(job.JobId)
                        .WithOffset(0L);
                    putRequest.WithChecksum(ChecksumType.Value(Convert.FromBase64String(testChecksumCrc32C)),
                        ChecksumType.Type.CRC_32C);
                    _client.PutObject(putRequest);
                }
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(_client, bucketName);
            }
        }

        private static bool IsTestSupported(IDs3Client client, double supportedBlackPearlVersion)
        {
            var buildInfo = client.GetSystemInformationSpectraS3(new GetSystemInformationSpectraS3Request()).ResponsePayload.BuildInformation.Version;
            var buildInfoArr = buildInfo.Split('.');
            var version = double.Parse(string.Format("{0}.{1}", buildInfoArr[0], buildInfoArr[1]));

            return version == supportedBlackPearlVersion;
        }

        [Test]
        [ExpectedException(typeof(Ds3.Runtime.Ds3BadStatusCodeException))]
        public void TestPutWithBadChecksum()
        {
            const string bucketName = "TestPutWithBadChecksum";
            try
            {
                // "123456789" has a well known checksum
                const string content = "123456789";
                const string testBadChecksumCrc32C = "4awSwg=="; // transposed two pairs

                var testObject = new Ds3Object("numbers_badcrc.txt", 9);
                var ds3Objs = new List<Ds3Object> { testObject };

                using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content)))
                {
                    // create or ensure bucket
                    _helpers.EnsureBucketExists(bucketName);
                    // create a job
                    var job = _helpers.StartWriteJob(bucketName, ds3Objs);
                    var putRequest = new PutObjectRequest(bucketName, testObject.Name, stream)
                        .WithJob(job.JobId)
                        .WithOffset(0L);
                    putRequest.WithChecksum(ChecksumType.Value(Convert.FromBase64String(testBadChecksumCrc32C)),
                        ChecksumType.Type.CRC_32C);
                    _client.PutObject(putRequest);
                }
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(_client, bucketName);
            }
        }

        [Test]
        public void TestSpecialCharacterInObjectName()
        {
            const string bucketName = "TestSpecialCharacterInObjectName";
            try
            {
                _helpers.EnsureBucketExists(bucketName);

                const string fileName = "varsity1314/_projects/VARSITY 13-14/_versions/Varsity 13-14 (2015-10-05 1827)/_project/Trash/PCMAC HD.avb";
                var obj = new Ds3Object(fileName, 1024);
                var objs = new List<Ds3Object> { obj };
                var job = _helpers.StartWriteJob(bucketName, objs);

                job.Transfer(key =>
                {
                    var data = new byte[1024];
                    var stream = new MemoryStream(data);
                    for (var i = 0; i < 1024; i++)
                    {
                        stream.WriteByte(97);
                    }

                    stream.Seek(0, SeekOrigin.Begin);

                    return stream;
                });

                // Is it there?
                var putfile =
                    from f in ListBucketObjects(bucketName)
                    where f.Name == fileName
                    select f;

                Assert.AreEqual(1, putfile.Count());
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(_client, bucketName);
            }
        }


        [Test]
        public void TestPlusCharacterInQueryParam()
        {
            const string bucketName = "TestPlusCharacterInQueryParam";
            try
            {
                _helpers.EnsureBucketExists(bucketName);

                const string fileName = "Test+Plus+Character";
                var obj = new Ds3Object(fileName, 1024);
                var objs = new List<Ds3Object> { obj };
                var job = _helpers.StartWriteJob(bucketName, objs);

                job.Transfer(key =>
                {
                    var data = new byte[1024];
                    var stream = new MemoryStream(data);
                    for (var i = 0; i < 1024; i++)
                    {
                        stream.WriteByte(97);
                    }

                    stream.Seek(0, SeekOrigin.Begin);

                    return stream;
                });

                // Does a query param escape properly?
                GetObjectsSpectraS3Request getObjectsWithNameRequest = new GetObjectsSpectraS3Request()
                    .WithName(fileName);

                var getObjectsResponse = _client.GetObjectsSpectraS3(getObjectsWithNameRequest);

                var filename =
                    from f in getObjectsResponse.ResponsePayload.S3Objects
                    where f.Name == fileName
                    select f;

                Assert.AreEqual(1, filename.Count());
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(_client, bucketName);
            }
        }

        [Test]
        public void TestBulkPutWithBlob()
        {
            const string bucketName = "TestBulkPutWithBlob";
            try
            {
                // Creates a bucket if it does not already exist.
                _helpers.EnsureBucketExists(bucketName);

                // Creates a bulk job with the server based on the files in a directory (recursively).
                var directoryObjects = FileHelpers.ListObjectsForDirectory(testDirectoryBigFolder);
                Assert.Greater(directoryObjects.Count(), 0);
                var job = _helpers.StartWriteJob(bucketName, directoryObjects, BlobSize);

                // Transfer all of the files.
                job.Transfer(FileHelpers.BuildFilePutter(testDirectoryBigFolder));

                VerifyFiles(bucketName, testDirectoryBigFolder);
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(_client, bucketName);
            }
        }

        [Test]
        public void TestPutObjectWithMaxBlob()
        {
            const string bucketName = "TestPutObjectWithMaxBlob";
            try
            {
                // Creates a bucket if it does not already exist.
                _helpers.EnsureBucketExists(bucketName);

                var directoryObjects =
                    FileHelpers.ListObjectsForDirectory(testDirectoryBigFolderForMaxBlob, string.Empty).ToList();
                var bulkResult =
                    _client.PutBulkJobSpectraS3(new PutBulkJobSpectraS3Request(bucketName, directoryObjects).WithMaxUploadSize(BlobSize));

                var chunkIds = new HashSet<Guid>();
                foreach (var obj in bulkResult.ResponsePayload.Objects)
                {
                    chunkIds.Add(obj.ChunkId);
                }

                while (chunkIds.Count > 0)
                {
                    var availableChunks =
                        _client.GetJobChunksReadyForClientProcessingSpectraS3(new GetJobChunksReadyForClientProcessingSpectraS3Request(bulkResult.ResponsePayload.JobId));

                    availableChunks.Match(
                        (time, response) =>
                        {
                            // for each chunk that is available, check to make sure
                            // we have not sent it, and if not, send that object
                            AsyncUpload(_client, chunkIds, response, bulkResult.ResponsePayload);
                        },
                        () =>
                        {
                            throw new InvalidOperationException(
                                "The job went away as we were trying to acquire chunk information");
                        },
                        retryAfter =>
                        {
                            // if we did not got some chunks than sleep and retry.  This could mean that the cache is full
                            Thread.Sleep((int)(retryAfter.TotalMilliseconds * 1000));
                        });
                }

                VerifyFiles(bucketName, testDirectoryBigFolderForMaxBlob);
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(_client, bucketName);
            }
        }

        private void VerifyFiles(string bucketName, string folder)
        {
            // Creates a bulk job with all of the objects in the bucket.
            var job = _helpers.StartReadAllJob(bucketName);

            // Transfer all of the files.
            job.Transfer(FileHelpers.BuildFileGetter(folder, PREFIX));

            foreach (var file in Directory.GetFiles(folder))
            {
                var fileName = Path.GetFileName(file);
                if (fileName.StartsWith(PREFIX)) continue;

                var origFile = File.OpenRead(Path.GetFullPath(file));
                var newFile = File.OpenRead(Path.GetFullPath(string.Format("{0}{1}{2}", folder, PREFIX, fileName)));

                Assert.AreEqual(
                    MD5.Create().ComputeHash(origFile),
                    MD5.Create().ComputeHash(newFile)
                    );

                origFile.Close();
                newFile.Close();
            }
        }

        private void AsyncUpload(IDs3Client client, ICollection<Guid> chunkIds, MasterObjectList response, MasterObjectList bulkResult)
        {
            Parallel.ForEach(response.Objects,
                chunk =>
                {
                    if (!chunkIds.Contains(chunk.ChunkId)) return;
                    chunkIds.Remove(chunk.ChunkId);

                    // it is possible that if we start resending a chunk, due to the program crashing, that
                    // some objects will already be in cache.  Check to make sure that they are not, and then
                    // send the object to Spectra S3

                    Parallel.ForEach(chunk.ObjectsList,
                        obj =>
                        {
                            if ((bool)obj.InCache) return;
                            PutObject(client, obj, bulkResult);
                        });
                    Console.WriteLine();
                });
        }

        private void PutObject(IDs3Client client, BulkObject obj, MasterObjectList bulkResult)
        {
            var fileToPut = File.OpenRead(testDirectoryBigFolderForMaxBlob + obj.Name);
            var contentStream = new PutObjectRequestStream(fileToPut, obj.Offset, obj.Length);
            var putObjectRequest = new PutObjectRequest(
                bulkResult.BucketName,
                obj.Name,
                contentStream
                ).WithJob(bulkResult.JobId)
                .WithOffset(obj.Offset);

            client.PutObject(putObjectRequest);
            fileToPut.Close();
        }

        #endregion independent tests

        [Test]
        public void TestBulkPutNoPrefix()
        {
            Ds3TestUtils.UsingAllWriteStrategys(strategy =>
            {
                const string bucketName = "TestBulkPutNoPrefix";
                try
                {
                    // Creates a bucket if it does not already exist.
                    _helpers.EnsureBucketExists(bucketName);

                    // Creates a bulk job with the server based on the files in a directory (recursively).
                    var directoryObjects = FileHelpers.ListObjectsForDirectory(testDirectorySrc);
                    Assert.Greater(directoryObjects.Count(), 0);
                    var job = _helpers.StartWriteJob(bucketName, directoryObjects, helperStrategy: strategy);

                    // Transfer all of the files.
                    job.Transfer(FileHelpers.BuildFilePutter(testDirectorySrc));

                    // all files there?
                    var folderCount = ListBucketObjects(bucketName).Count();
                    Assert.AreEqual(folderCount, directoryObjects.Count());
                }
                finally
                {
                    Ds3TestUtils.DeleteBucket(_client, bucketName);
                }
            });
        }

        [Test]
        public void TestBulkPutWithPrefix()
        {
            const string bucketName = "TestBulkPutWithPrefix";
            Ds3TestUtils.UsingAllWriteStrategys(strategy =>
            {
                try
                {
                    // Creates a bucket if it does not already exist.
                    _helpers.EnsureBucketExists(bucketName);

                    // Creates a bulk job with the server based on the files in a directory (recursively).
                    var directoryObjects = FileHelpers.ListObjectsForDirectory(testDirectorySrc, PREFIX);
                    Assert.Greater(directoryObjects.Count(), 0);
                    var job = _helpers.StartWriteJob(bucketName, directoryObjects, helperStrategy: strategy);

                    // Transfer all of the files.
                    job.Transfer(FileHelpers.BuildFilePutter(testDirectorySrc, PREFIX));

                    // put all files?
                    var folderCount = ListBucketObjects(bucketName).Count();
                    Assert.AreEqual(folderCount, directoryObjects.Count());
                }
                finally
                {
                    Ds3TestUtils.DeleteBucket(_client, bucketName);
                }
            });
        }

        [Test]
        public void TestBulkGetWithPrefix()
        {
            const string bucketName = "TestBulkGetWithPrefix";

            Ds3TestUtils.UsingAllStringReadStrategys(strategy =>
            {
                try
                {
                    Ds3TestUtils.LoadTestData(_client, bucketName);

                    // Creates a bulk job with all of the objects in the bucket.
                    var job = _helpers.StartReadAllJob(bucketName, strategy);

                    // Transfer all of the files.
                    job.Transfer(FileHelpers.BuildFileGetter(testDirectoryDestPrefix, PREFIX));

                    var postFolder = FileHelpers.ListObjectsForDirectory(testDirectoryDestPrefix, PREFIX);
                    var postFolderCount = postFolder.Count();
                    Assert.AreEqual(postFolderCount, ListBucketObjects(bucketName).Count());
                }
                finally
                {
                    Ds3TestUtils.DeleteBucket(_client, bucketName);
                    DeleteFilesFromLocalDirectory(testDirectoryDestPrefix);
                }
            });
        }

        [Test]
        public void TestBulkGetWithoutPrefix()
        {
            const string bucketName = "TestBulkGetWithoutPrefix";

            Ds3TestUtils.UsingAllStringReadStrategys(strategy =>
            {
                try
                {
                    Ds3TestUtils.LoadTestData(_client, bucketName);

                    // Creates a bulk job with all of the objects in the bucket.
                    var job = _helpers.StartReadAllJob(bucketName, strategy);

                    // Transfer all of the files.
                    job.Transfer(FileHelpers.BuildFileGetter(testDirectoryDest));

                    var postFolder = FileHelpers.ListObjectsForDirectory(testDirectoryDest);
                    var postFolderCount = postFolder.Count();
                    Assert.AreEqual(postFolderCount, ListBucketObjects(bucketName).Count());
                }
                finally
                {
                    Ds3TestUtils.DeleteBucket(_client, bucketName);
                    DeleteFilesFromLocalDirectory(testDirectoryDest);
                }
            });
        }

        [Test]
        public void TestDeleteFolder()
        {
            const string bucketName = "TestDeleteFolder";
            try
            {
                // Creates a bucket if it does not already exist.
                _helpers.EnsureBucketExists(bucketName);

                // Creates a bulk job with the server based on the files in a directory (recursively).
                var directoryObjects = FileHelpers.ListObjectsForDirectory(testDirectorySrc);
                Assert.Greater(directoryObjects.Count(), 0);
                var job = _helpers.StartWriteJob(bucketName, directoryObjects);

                // Transfer all of the files.
                job.Transfer(FileHelpers.BuildFilePutter(testDirectorySrc));

                // all files there?
                var folderCount = ListBucketObjects(bucketName).Count();
                Assert.AreEqual(folderCount, directoryObjects.Count());

                // now it's there
                var anteFolder = ListBucketObjects(bucketName);
                var anteFolderCount = anteFolder.Count();
                Assert.Greater(anteFolderCount, 0);

                // get all with folder name
                var folderItems =
                    from o in anteFolder
                    where o.Name.StartsWith(FOLDER)
                    select o;

                folderCount = folderItems.Count();
                Assert.Greater(folderCount, 0);

                // delete it
                var request = new DeleteFolderRecursivelySpectraS3Request(bucketName, FOLDER);
                _client.DeleteFolderRecursivelySpectraS3(request);

                // now it's gone
                var postFolderCount = ListBucketObjects(bucketName).Count();
                Assert.AreEqual(anteFolderCount - postFolderCount, folderCount);
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(_client, bucketName);
            }
        }

        private IEnumerable<S3Object> ListBucketObjects(string bucketName)
        {
            var request = new GetObjectsSpectraS3Request
            {
                BucketId = bucketName
            };
            return _client.GetObjectsSpectraS3(request).ResponsePayload.S3Objects;
        }

        /* current defect in simulator, this will fail,
         * but should be added someday
        [Test]
        [ExpectedException(typeof(Ds3.Runtime.Ds3BadStatusCodeException))]
        public void Test0510DeleteDeletedBucket()
        {
            // delete it
            DeleteFolderRequest request = new Ds3.Calls.DeleteFolderRequest(TESTBUCKET, FOLDER);
            _client.DeleteFolder(request);
        }
        **/

        [Test]
        [ExpectedException(typeof(Ds3.Runtime.Ds3BadStatusCodeException))]
        public void TestGetBadBucket()
        {
            var request = new GetBucketRequest("NoBucket" + DateTime.Now.Ticks);
            _client.GetBucket(request);
        }

        [Test]
        public void TestDeleteObject()
        {
            const string bucketName = "TestDeleteObject";
            try
            {
                Ds3TestUtils.LoadTestData(_client, bucketName);

                var anteFolderCount = ListBucketObjects(bucketName).Count();
                // delete the first book
                var book = BOOKS.First();
                DeleteObject(bucketName, book);

                // one less ?
                var postFolderCount = ListBucketObjects(bucketName).Count();
                Assert.AreEqual(anteFolderCount - postFolderCount, 1);
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(_client, bucketName);
            }
        }

        [Test]
        [ExpectedException(typeof(Ds3.Runtime.Ds3BadStatusCodeException))]
        public void TestDeleteDeletedObject()
        {
            const string bucketName = "TestDeleteDeletedObject";
            try
            {
                Ds3TestUtils.LoadTestData(_client, bucketName);

                var anteFolderCount = ListBucketObjects(bucketName).Count();
                // delete the first book
                var book = BOOKS.First();
                DeleteObject(bucketName, book);

                // one less ?
                var postFolderCount = ListBucketObjects(bucketName).Count();
                Assert.AreEqual(anteFolderCount - postFolderCount, 1);

                book = BOOKS.First();
                DeleteObject(bucketName, book);
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(_client, bucketName);
            }
        }

        [Test]
        public void TestDeleteObjectWithPrefix()
        {
            const string bucketName = "TestDeleteObjectWithPrefix";
            try
            {
                // Creates a bucket if it does not already exist.
                _helpers.EnsureBucketExists(bucketName);

                // Creates a bulk job with the server based on the files in a directory (recursively).
                var directoryObjects = FileHelpers.ListObjectsForDirectory(testDirectorySrc, PREFIX);
                Assert.Greater(directoryObjects.Count(), 0);
                var job = _helpers.StartWriteJob(bucketName, directoryObjects);

                // Transfer all of the files.
                job.Transfer(FileHelpers.BuildFilePutter(testDirectorySrc, PREFIX));

                // put all files?
                var folderCount = ListBucketObjects(bucketName).Count();
                Assert.AreEqual(folderCount, directoryObjects.Count());

                var anteFolderCount = ListBucketObjects(bucketName).Count();

                // delete the first book
                var book = BOOKS.First();
                DeleteObject(bucketName, PREFIX + book);

                // one less ?
                var postFolderCount = ListBucketObjects(bucketName).Count();
                Assert.AreEqual(anteFolderCount - postFolderCount, 1);
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(_client, bucketName);
            }
        }

        [Test]
        public void TestCleanUp()
        {
            const string bucketName = "TestCleanUp";
            Ds3TestUtils.LoadTestData(_client, bucketName);

            var items = _helpers.ListObjects(bucketName);

            // Loop through all of the objects in the bucket.
            foreach (var obj in items)
            {
                DeleteObject(bucketName, obj.Name);
            }
            DeleteBucket(bucketName);
        }

        private void DeleteObject(string bucketname, string objectName)
        {
            var request = new DeleteObjectRequest(bucketname, objectName);
            _client.DeleteObject(request);
        }

        private void DeleteBucket(string bucketname)
        {
            var request = new DeleteBucketRequest(bucketname);
            _client.DeleteBucket(request);
        }

        private static void DeleteFilesFromLocalDirectory(string dir)
        {
            foreach (var file in Directory.GetFiles(dir))
            {
                File.Delete(file);
            }
        }

        [Test]
        public void TestChecksumStreamingWithMultiBlobs()
        {
            const string bucketName = "TestChecksumStreamingWithMultiBlobs";
            try
            {
                // Creates a bucket if it does not already exist.
                _helpers.EnsureBucketExists(bucketName);

                // Creates a bulk job with the server based on the files in a directory (recursively).
                var bigFile = FileHelpers.ListObjectsForDirectory(testDirectoryBigFolder).First(obj => obj.Name.Equals(BIGFILES.First()));
                var directoryObjects = new List<Ds3Object> {bigFile};

                Assert.Greater(directoryObjects.Count(), 0);

                var job = _helpers.StartWriteJob(bucketName, directoryObjects, BlobSize, new WriteStreamHelperStrategy());

                var md5 = MD5.Create();
                var fileStream = File.OpenRead(testDirectoryBigFolder + BIGFILES.First());
                var md5Stream = new CryptoStream(fileStream, md5, CryptoStreamMode.Read);

                job.Transfer(foo => md5Stream);
                if (!md5Stream.HasFlushedFinalBlock)
                {
                    md5Stream.FlushFinalBlock();
                }

                Assert.AreEqual("g1YZyuEkeAU3I3UAydy6DA==", Convert.ToBase64String(md5.Hash));
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(_client, bucketName);
            }
        }
    }
}