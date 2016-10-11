/*
 * ******************************************************************************
 *   Copyright 2014-2016 Spectra Logic Corporation. All Rights Reserved.
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
using System.Reflection;
using System.Security.Cryptography;
using System.Threading;
using System.Threading.Tasks;
using Ds3;
using Ds3.Calls;
using Ds3.Helpers;
using Ds3.Helpers.Strategies;
using Ds3.Helpers.Streams;
using Ds3.Models;
using Ds3.Runtime;
using IntegrationTestDS3;
using NUnit.Framework;

namespace IntegrationTestDs3
{
    [TestFixture]
    public class IntegrationTestDs3Client
    {
        private readonly string[] _books = {"beowulf.txt", "sherlock_holmes.txt", "tale_of_two_cities.txt"};
        private readonly string[] _joyceBooks = {"ulysses.txt"};
        private readonly string[] _bigFiles = {"big_book_file.txt"};

        private const string Testdir = "TestObjectData";
        private const string Prefix = "test_";
        private const string Folder = "joyce";
        private const string Big = "big";
        private const string BigForMaxBlob = "bigForMaxBlob";
        private const string FixtureName = "integration_test_ds3client";
        private const long BlobSize = 10*1024*1024;

        private string TestDirectorySrc { get; set; }
        private string TestDirectoryBigFolder { get; set; }
        private string TestDirectoryBigFolderForMaxBlob { get; set; }
        private string TestDirectoryDest { get; set; }
        private string TestDirectoryDestPrefix { get; set; }

        private IDs3Client Client { get; set; }
        private Ds3ClientHelpers Helpers { get; set; }


        private static TempStorageIds _envStorageIds;
        private static Guid _envDataPolicyId;

        [OneTimeSetUp]
        public void Startup()
        {
            try
            {
                Client = Ds3TestUtils.CreateClient();
                Helpers = new Ds3ClientHelpers(Client);

                SetupTestData();

                _envDataPolicyId = TempStorageUtil.SetupDataPolicy(FixtureName, false, ChecksumType.Type.MD5, Client);
                _envStorageIds = TempStorageUtil.Setup(FixtureName, _envDataPolicyId, Client);
            }
            catch (Exception)
            {
                // So long as any SetUp method runs without error, the TearDown method is guaranteed to run.
                // It will not run if a SetUp method fails or throws an exception.
                Teardown();
                throw;
            }
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            TempStorageUtil.TearDown(FixtureName, _envStorageIds, Client);
        }

        private void SetupTestData()
        {
            var root = Path.GetTempPath() + Testdir + Path.DirectorySeparatorChar;
            TestDirectorySrc = root + "src" + Path.DirectorySeparatorChar;
            var testDirectorySrcFolder = root + "src" + Path.DirectorySeparatorChar + Folder +
                                         Path.DirectorySeparatorChar;
            TestDirectoryDest = root + "dest" + Path.DirectorySeparatorChar;
            TestDirectoryDestPrefix = root + "destPrefix" + Path.DirectorySeparatorChar;

            TestDirectoryBigFolder = root + Big + Path.DirectorySeparatorChar;
            TestDirectoryBigFolderForMaxBlob = root + BigForMaxBlob + Path.DirectorySeparatorChar;

            // create and populate a new test dir
            if (Directory.Exists(root))
            {
                Directory.Delete(root, true);
            }
            Directory.CreateDirectory(root);
            Directory.CreateDirectory(TestDirectorySrc);
            Directory.CreateDirectory(testDirectorySrcFolder);
            Directory.CreateDirectory(TestDirectoryDest);
            Directory.CreateDirectory(TestDirectoryDestPrefix);
            Directory.CreateDirectory(TestDirectoryBigFolder);
            Directory.CreateDirectory(TestDirectoryBigFolderForMaxBlob);

            foreach (var book in _books)
            {
                var writer = File.OpenWrite(TestDirectorySrc + book);
                using (var bookstream = ReadResource("IntegrationTestDS3.TestData." + book))
                {
                    bookstream.CopyTo(writer);
                    bookstream.Seek(0, SeekOrigin.Begin);
                }
                writer.Close();
            }
            foreach (var book in _joyceBooks)
            {
                var writer = File.OpenWrite(testDirectorySrcFolder + book);
                using (var bookstream = ReadResource("IntegrationTestDS3.TestData." + book))
                {
                    bookstream.CopyTo(writer);
                    bookstream.Seek(0, SeekOrigin.Begin);
                }
                writer.Close();
            }
            foreach (var bigFile in _bigFiles)
            {
                var writer = File.OpenWrite(TestDirectoryBigFolder + bigFile);
                var writerForMaxBlob = File.OpenWrite(TestDirectoryBigFolderForMaxBlob + bigFile + "_maxBlob");

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

        private static bool IsTestSupported(IDs3Client client, double supportedBlackPearlVersion)
        {
            var buildInfo =
                client.GetSystemInformationSpectraS3(new GetSystemInformationSpectraS3Request())
                    .ResponsePayload.BuildInformation.Version;
            var buildInfoArr = buildInfo.Split('.');
            var version = double.Parse(string.Format("{0}.{1}", buildInfoArr[0], buildInfoArr[1]));

            return version == supportedBlackPearlVersion;
        }

        private void VerifyFiles(string bucketName, string folder)
        {
            // Creates a bulk job with all of the objects in the bucket.
            var job = Helpers.StartReadAllJob(bucketName);

            // Transfer all of the files.
            job.Transfer(FileHelpers.BuildFileGetter(folder, Prefix));

            foreach (var file in Directory.GetFiles(folder))
            {
                var fileName = Path.GetFileName(file);
                if (fileName.StartsWith(Prefix)) continue;

                var origFile = File.OpenRead(Path.GetFullPath(file));
                var newFile = File.OpenRead(Path.GetFullPath(string.Format("{0}{1}{2}", folder, Prefix, fileName)));

                Assert.AreEqual(
                    MD5.Create().ComputeHash(origFile),
                    MD5.Create().ComputeHash(newFile)
                );

                origFile.Close();
                newFile.Close();
            }
        }

        private void AsyncUpload(IDs3Client client, ICollection<Guid> chunkIds, MasterObjectList response,
            MasterObjectList bulkResult)
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
                            if ((bool) obj.InCache) return;
                            PutObject(client, obj, bulkResult);
                        });
                    Console.WriteLine();
                });
        }

        private void PutObject(IDs3Client client, BulkObject obj, MasterObjectList bulkResult)
        {
            var fileToPut = File.OpenRead(TestDirectoryBigFolderForMaxBlob + obj.Name);
            var contentStream = new ObjectRequestStream(fileToPut, obj.Offset, obj.Length);
            var putObjectRequest = new PutObjectRequest(
                    bulkResult.BucketName,
                    obj.Name,
                    contentStream
                ).WithJob(bulkResult.JobId)
                .WithOffset(obj.Offset);

            client.PutObject(putObjectRequest);
            fileToPut.Close();
        }

        private IEnumerable<S3Object> ListBucketObjects(string bucketName)
        {
            var request = new GetObjectsDetailsSpectraS3Request
            {
                BucketId = bucketName
            };
            return Client.GetObjectsDetailsSpectraS3(request).ResponsePayload.S3Objects;
        }

        private void DeleteObject(string bucketname, string objectName)
        {
            var request = new DeleteObjectRequest(bucketname, objectName);
            Client.DeleteObject(request);
        }

        private void DeleteBucket(string bucketname)
        {
            var request = new DeleteBucketRequest(bucketname);
            Client.DeleteBucket(request);
        }

        private static void DeleteFilesFromLocalDirectory(string dir)
        {
            foreach (var file in Directory.GetFiles(dir))
            {
                File.Delete(file);
            }
        }

        [Test]
        public void DoIntegrationPing()
        {
            var request = new VerifySystemHealthSpectraS3Request();
            var response = Client.VerifySystemHealthSpectraS3(request);

            Assert.GreaterOrEqual(response.ResponsePayload.MsRequiredToVerifyDataPlannerHealth, 0);
        }

        [Test]
        public void GetObjectsWithResume()
        {
            const string bucketName = "GetObjectsWithResume";
            const int numberOfObjects = 1000;

            try
            {
                const string content = "hi im content";
                var contentBytes = System.Text.Encoding.UTF8.GetBytes(content);

                var objects = new List<Ds3Object>();

                for (var i = 0; i < numberOfObjects; i++)
                {
                    objects.Add(new Ds3Object("File" + i, contentBytes.Length));
                }

                Helpers.EnsureBucketExists(bucketName);

                //upload test files
                var putJob = Helpers.StartWriteJob(bucketName, objects);
                putJob.Transfer(key => new MemoryStream(contentBytes));

                //start the read job
                var getJob = Helpers.StartReadJob(bucketName, objects);
                var cancellationTokenSource = new CancellationTokenSource();
                getJob.WithCancellationToken(cancellationTokenSource.Token);

                var filesTransfered = 0;
                getJob.ItemCompleted += s => { filesTransfered++; };


                var thread = new Thread(() =>
                {
                    try
                    {
                        getJob.Transfer(key => new MemoryStream(contentBytes));
                    }
                    catch (Exception)
                    {
                        // pass
                    }
                });
                thread.Start();

                //give the thread a sec to start
                Thread.Sleep(1000);

                //cancel the job
                cancellationTokenSource.Cancel();

                //wait for the thread to finish
                thread.Join();

                Assert.Less(filesTransfered, numberOfObjects);

                //resume the job
                var resumedJob = Helpers.RecoverReadJob(getJob.JobId);

                resumedJob.ItemCompleted += s => { filesTransfered++; };

                resumedJob.Transfer(key => new MemoryStream(contentBytes));

                Assert.AreEqual(numberOfObjects, filesTransfered);
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(Client, bucketName);
            }
        }

        [Test]
        public void GetSystemInfo()
        {
            // get valid data and populate properties
            var request = new GetSystemInformationSpectraS3Request();
            var response = Client.GetSystemInformationSpectraS3(request).ResponsePayload;
            Assert.That(string.IsNullOrEmpty(response.BuildInformation.Version), Is.False,
                "Result string must not be null or empty");
        }

        [Test]
        public void PutObjectsWithResume()
        {
            const string bucketName = "PutObjectsWithResume";
            const int numberOfObjects = 1000;

            try
            {
                const string content = "hi im content";
                var contentBytes = System.Text.Encoding.UTF8.GetBytes(content);

                var objects = new List<Ds3Object>();

                for (var i = 0; i < numberOfObjects; i++)
                {
                    objects.Add(new Ds3Object("File" + i, contentBytes.Length));
                }

                Helpers.EnsureBucketExists(bucketName);
                var job = Helpers.StartWriteJob(bucketName, objects);

                var cancellationTokenSource = new CancellationTokenSource();
                job.WithCancellationToken(cancellationTokenSource.Token);

                var filesTransfered = 0;
                job.ItemCompleted += s => { filesTransfered++; };


                var thread = new Thread(() =>
                {
                    try
                    {
                        job.Transfer(key => new MemoryStream(contentBytes));
                    }
                    catch (Exception)
                    {
                        // pass
                    }
                });
                thread.Start();

                //give the thread a sec to start
                Thread.Sleep(1000);

                //cancel the job
                cancellationTokenSource.Cancel();

                //wait for the thread to finish
                thread.Join();

                Assert.Less(filesTransfered, numberOfObjects);

                //resume the job
                var resumedJob = Helpers.RecoverWriteJob(job.JobId);

                resumedJob.ItemCompleted += s => { filesTransfered++; };

                resumedJob.Transfer(key => new MemoryStream(contentBytes));

                Assert.AreEqual(numberOfObjects, filesTransfered);
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(Client, bucketName);
            }
        }

        [Test]
        public void TestBulkGetWithoutPrefix()
        {
            const string bucketName = "TestBulkGetWithoutPrefix";

            Ds3TestUtils.UsingAllStringReadStrategies(strategy =>
            {
                try
                {
                    Ds3TestUtils.LoadTestData(Client, bucketName);

                    // Creates a bulk job with all of the objects in the bucket.
                    var job = Helpers.StartReadAllJob(bucketName, strategy);

                    // Transfer all of the files.
                    job.Transfer(FileHelpers.BuildFileGetter(TestDirectoryDest));

                    var postFolder = FileHelpers.ListObjectsForDirectory(TestDirectoryDest);
                    var postFolderCount = postFolder.Count();
                    Assert.AreEqual(postFolderCount, ListBucketObjects(bucketName).Count());
                }
                finally
                {
                    Ds3TestUtils.DeleteBucket(Client, bucketName);
                    DeleteFilesFromLocalDirectory(TestDirectoryDest);
                }
            });
        }

        [Test]
        public void TestBulkGetWithPrefix()
        {
            const string bucketName = "TestBulkGetWithPrefix";

            Ds3TestUtils.UsingAllStringReadStrategies(strategy =>
            {
                try
                {
                    Ds3TestUtils.LoadTestData(Client, bucketName);

                    // Creates a bulk job with all of the objects in the bucket.
                    var job = Helpers.StartReadAllJob(bucketName, strategy);

                    // Transfer all of the files.
                    job.Transfer(FileHelpers.BuildFileGetter(TestDirectoryDestPrefix, Prefix));

                    var postFolder = FileHelpers.ListObjectsForDirectory(TestDirectoryDestPrefix, Prefix);
                    var postFolderCount = postFolder.Count();
                    Assert.AreEqual(postFolderCount, ListBucketObjects(bucketName).Count());
                }
                finally
                {
                    Ds3TestUtils.DeleteBucket(Client, bucketName);
                    DeleteFilesFromLocalDirectory(TestDirectoryDestPrefix);
                }
            });
        }

        [Test]
        public void TestBulkPutNoPrefix()
        {
            Ds3TestUtils.UsingAllWriteStrategies(strategy =>
            {
                const string bucketName = "TestBulkPutNoPrefix";
                try
                {
                    // Creates a bucket if it does not already exist.
                    Helpers.EnsureBucketExists(bucketName);

                    // Creates a bulk job with the server based on the files in a directory (recursively).
                    var directoryObjects = FileHelpers.ListObjectsForDirectory(TestDirectorySrc);
                    Assert.Greater(directoryObjects.Count(), 0);
                    var job = Helpers.StartWriteJob(bucketName, directoryObjects, helperStrategy: strategy);

                    // Transfer all of the files.
                    job.Transfer(FileHelpers.BuildFilePutter(TestDirectorySrc));

                    // all files there?
                    var folderCount = ListBucketObjects(bucketName).Count();
                    Assert.AreEqual(folderCount, directoryObjects.Count());
                }
                finally
                {
                    Ds3TestUtils.DeleteBucket(Client, bucketName);
                }
            });
        }

        [Test]
        public void TestBulkPutWithBlob()
        {
            const string bucketName = "TestBulkPutWithBlob";
            try
            {
                // Creates a bucket if it does not already exist.
                Helpers.EnsureBucketExists(bucketName);

                // Creates a bulk job with the server based on the files in a directory (recursively).
                var directoryObjects = FileHelpers.ListObjectsForDirectory(TestDirectoryBigFolder);
                Assert.Greater(directoryObjects.Count(), 0);
                var job = Helpers.StartWriteJob(bucketName, directoryObjects, BlobSize);

                // Transfer all of the files.
                job.Transfer(FileHelpers.BuildFilePutter(TestDirectoryBigFolder));

                VerifyFiles(bucketName, TestDirectoryBigFolder);
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(Client, bucketName);
            }
        }

        [Test]
        public void TestBulkPutWithPrefix()
        {
            const string bucketName = "TestBulkPutWithPrefix";
            Ds3TestUtils.UsingAllWriteStrategies(strategy =>
            {
                try
                {
                    // Creates a bucket if it does not already exist.
                    Helpers.EnsureBucketExists(bucketName);

                    // Creates a bulk job with the server based on the files in a directory (recursively).
                    var directoryObjects = FileHelpers.ListObjectsForDirectory(TestDirectorySrc, Prefix);
                    Assert.Greater(directoryObjects.Count(), 0);
                    var job = Helpers.StartWriteJob(bucketName, directoryObjects, helperStrategy: strategy);

                    // Transfer all of the files.
                    job.Transfer(FileHelpers.BuildFilePutter(TestDirectorySrc, Prefix));

                    // put all files?
                    var folderCount = ListBucketObjects(bucketName).Count();
                    Assert.AreEqual(folderCount, directoryObjects.Count());
                }
                finally
                {
                    Ds3TestUtils.DeleteBucket(Client, bucketName);
                }
            });
        }

        [Test]
        public void TestChecksumStreamingWithMultiBlobs()
        {
            const string bucketName = "TestChecksumStreamingWithMultiBlobs";
            var tempFilename = Path.GetTempFileName();

            try
            {
                // Creates a bucket if it does not already exist.
                Helpers.EnsureBucketExists(bucketName);

                // Creates a bulk job with the server based on the files in a directory (recursively).
                var bigFile =
                    FileHelpers.ListObjectsForDirectory(TestDirectoryBigFolder)
                        .First(obj => obj.Name.Equals(_bigFiles.First()));
                var directoryObjects = new List<Ds3Object> {bigFile};

                Assert.Greater(directoryObjects.Count, 0);

                // Test the PUT
                var putJob = Helpers.StartWriteJob(bucketName, directoryObjects, BlobSize,
                    new WriteStreamHelperStrategy());
                using (Stream fileStream = File.OpenRead(TestDirectoryBigFolder + _bigFiles.First()))
                {
                    var md5 = MD5.Create();
                    using (var md5Stream = new CryptoStream(fileStream, md5, CryptoStreamMode.Read))
                    {
                        putJob.Transfer(foo => md5Stream);
                        if (!md5Stream.HasFlushedFinalBlock)
                        {
                            md5Stream.FlushFinalBlock();
                        }

                        Assert.AreEqual("g1YZyuEkeAU3I3UAydy6DA==", Convert.ToBase64String(md5.Hash));
                    }
                }

                // Test the GET
                var getJob = Helpers.StartReadAllJob(bucketName, new ReadStreamHelperStrategy());
                using (Stream fileStream = new FileStream(tempFilename, FileMode.Truncate, FileAccess.Write))
                {
                    var md5 = MD5.Create();
                    using (var md5Stream = new CryptoStream(fileStream, md5, CryptoStreamMode.Write))
                    {
                        getJob.Transfer(foo => md5Stream);
                        if (!md5Stream.HasFlushedFinalBlock)
                        {
                            md5Stream.FlushFinalBlock();
                        }

                        Assert.AreEqual("g1YZyuEkeAU3I3UAydy6DA==", Convert.ToBase64String(md5.Hash));
                    }
                }
            }
            finally
            {
                File.Delete(tempFilename);
                Ds3TestUtils.DeleteBucket(Client, bucketName);
            }
        }

        [Test]
        public void TestCleanUp()
        {
            const string bucketName = "TestCleanUp";
            Ds3TestUtils.LoadTestData(Client, bucketName);

            var items = Helpers.ListObjects(bucketName);

            // Loop through all of the objects in the bucket.
            foreach (var obj in items)
            {
                DeleteObject(bucketName, obj.Name);
            }
            DeleteBucket(bucketName);
        }

        [Test]
        public void TestDeleteDeletedObject()
        {
            const string bucketName = "TestDeleteDeletedObject";
            try
            {
                Ds3TestUtils.LoadTestData(Client, bucketName);

                var anteFolderCount = ListBucketObjects(bucketName).Count();
                // delete the first book
                var book = _books.First();
                DeleteObject(bucketName, book);

                // one less ?
                var postFolderCount = ListBucketObjects(bucketName).Count();
                Assert.AreEqual(anteFolderCount - postFolderCount, 1);

                book = _books.First();
                Assert.Throws<Ds3BadStatusCodeException>(() => DeleteObject(bucketName, book));
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(Client, bucketName);
            }
        }

        [Test]
        public void TestDeleteFolder()
        {
            const string bucketName = "TestDeleteFolder";
            try
            {
                // Creates a bucket if it does not already exist.
                Helpers.EnsureBucketExists(bucketName);

                // Creates a bulk job with the server based on the files in a directory (recursively).
                var directoryObjects = FileHelpers.ListObjectsForDirectory(TestDirectorySrc);
                Assert.Greater(directoryObjects.Count(), 0);
                var job = Helpers.StartWriteJob(bucketName, directoryObjects);

                // Transfer all of the files.
                job.Transfer(FileHelpers.BuildFilePutter(TestDirectorySrc));

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
                    where o.Name.StartsWith(Folder)
                    select o;

                folderCount = folderItems.Count();
                Assert.Greater(folderCount, 0);

                // delete it
                var request = new DeleteFolderRecursivelySpectraS3Request(bucketName, Folder);
                Client.DeleteFolderRecursivelySpectraS3(request);

                // now it's gone
                var postFolderCount = ListBucketObjects(bucketName).Count();
                Assert.AreEqual(anteFolderCount - postFolderCount, folderCount);
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(Client, bucketName);
            }
        }

        [Test]
        public void TestDeleteObject()
        {
            const string bucketName = "TestDeleteObject";
            try
            {
                Ds3TestUtils.LoadTestData(Client, bucketName);

                var anteFolderCount = ListBucketObjects(bucketName).Count();
                // delete the first book
                var book = _books.First();
                DeleteObject(bucketName, book);

                // one less ?
                var postFolderCount = ListBucketObjects(bucketName).Count();
                Assert.AreEqual(anteFolderCount - postFolderCount, 1);
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(Client, bucketName);
            }
        }

        [Test]
        public void TestDeleteObjectWithPrefix()
        {
            const string bucketName = "TestDeleteObjectWithPrefix";
            try
            {
                // Creates a bucket if it does not already exist.
                Helpers.EnsureBucketExists(bucketName);

                // Creates a bulk job with the server based on the files in a directory (recursively).
                var directoryObjects = FileHelpers.ListObjectsForDirectory(TestDirectorySrc, Prefix);
                Assert.Greater(directoryObjects.Count(), 0);
                var job = Helpers.StartWriteJob(bucketName, directoryObjects);

                // Transfer all of the files.
                job.Transfer(FileHelpers.BuildFilePutter(TestDirectorySrc, Prefix));

                // put all files?
                var folderCount = ListBucketObjects(bucketName).Count();
                Assert.AreEqual(folderCount, directoryObjects.Count());

                var anteFolderCount = ListBucketObjects(bucketName).Count();

                // delete the first book
                var book = _books.First();
                DeleteObject(bucketName, Prefix + book);

                // one less ?
                var postFolderCount = ListBucketObjects(bucketName).Count();
                Assert.AreEqual(anteFolderCount - postFolderCount, 1);
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(Client, bucketName);
            }
        }

        [Test]
        public void TestFilterDs3Object()
        {
            const string bucketName = "TestFilterDs3Object";

            try
            {
                Client.PutBucket(new PutBucketRequest(bucketName));
                var stream = new MemoryStream(new byte[] {});
                Client.PutObject(new PutObjectRequest(bucketName, "i_am_a_folder/", stream));

                stream = new MemoryStream(new byte[] {});
                Client.PutObject(new PutObjectRequest(bucketName, "i_am_a_zero_length_object", stream));

                var objects = Helpers.ListObjects(bucketName);
                Assert.AreEqual(2, objects.Count());

                var objectsFiltered = Ds3ClientHelpers.FilterDs3Objects(objects, Ds3ClientHelpers.FolderFilterPredicate,
                    Ds3ClientHelpers.ZeroLengthFilterPredicate);
                Assert.AreEqual(0, objectsFiltered.Count());
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(Client, bucketName);
            }
        }

        [Test]
        public void TestFolderFilterDs3Object()
        {
            const string bucketName = "TestFolderFilterDs3Object";

            try
            {
                Client.PutBucket(new PutBucketRequest(bucketName));
                var stream = new MemoryStream(new byte[] {});
                Client.PutObject(new PutObjectRequest(bucketName, "i_am_a_folder/", stream));

                var objects = Helpers.ListObjects(bucketName);
                Assert.AreEqual(1, objects.Count());

                var objectsFiltered = Ds3ClientHelpers.FilterDs3Objects(objects, Ds3ClientHelpers.FolderFilterPredicate);
                Assert.AreEqual(0, objectsFiltered.Count());
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(Client, bucketName);
            }
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
        public void TestGetBadBucket()
        {
            var request = new GetBucketRequest("NoBucket" + DateTime.Now.Ticks);
            Assert.Throws<Ds3BadStatusCodeException>(() => Client.GetBucket(request));
        }

        [Test]
        public void TestGetObjectDetails()
        {
            const string bucketName = "TestGetObjectDetails";
            const string objectName = "beowulf.txt";
            try
            {
                Ds3TestUtils.LoadTestData(Client, bucketName);

                var response =
                    Client.GetObjectDetailsSpectraS3(new GetObjectDetailsSpectraS3Request(objectName, bucketName));
                Assert.AreEqual(response.ResponsePayload.Name, objectName);
                Assert.AreEqual(response.ResponsePayload.Type, S3ObjectType.DATA);
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(Client, bucketName);
            }
        }

        [Test]
        public void TestGetObjectsWithFullDetails()
        {
            const string bucketName = "TestGetObjectsWithFullDetails";
            try
            {
                Ds3TestUtils.LoadTestData(Client, bucketName);

                var request =
                    new GetObjectsWithFullDetailsSpectraS3Request().WithIncludePhysicalPlacement(true)
                        .WithBucketId(bucketName);
                var response = Client.GetObjectsWithFullDetailsSpectraS3(request);
                Assert.AreEqual(response.ResponsePayload.DetailedS3Objects.Count(), 4);
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(Client, bucketName);
            }
        }

        [Test]
        public void TestGetPutJobToReplicate()
        {
            const string bucketName = "TestGetPutJobToReplicate";
            const string objectName = "beowulf.txt";
            try
            {
                Client.PutBucketSpectraS3(
                    new PutBucketSpectraS3Request(bucketName).WithDataPolicyId(_envDataPolicyId));
                var objects = new List<Ds3Object>();
                objects.Add(new Ds3Object(objectName, 0));
                var putBulk = Client.PutBulkJobSpectraS3(new PutBulkJobSpectraS3Request(bucketName, objects));

                var getJob =
                    Client.GetJobToReplicateSpectraS3(
                        new GetJobToReplicateSpectraS3Request(putBulk.ResponsePayload.JobId));
                StringAssert.Contains("\"name\":\"beowulf.txt\",\"type\":\"DATA\"", getJob.ResponsePayload);
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(Client, bucketName);
            }
        }

        [Test]
        public void TestJobWithMetadata()
        {
            const string bucketName = "TestJobWithMetadata";

            try
            {
                // grab a file
                var directoryObjects = FileHelpers.ListObjectsForDirectory(TestDirectorySrc);
                Assert.IsNotEmpty(directoryObjects);
                var testObject = directoryObjects.Where(o => o.Name.Equals("beowulf.txt"));

                // create or ensure bucket
                Helpers.EnsureBucketExists(bucketName);

                // create a job with metadata
                var job = Helpers.StartWriteJob(bucketName, testObject);
                job.WithMetadata(new MetadataAccess());

                // Transfer all of the files.
                job.Transfer(FileHelpers.BuildFilePutter(TestDirectorySrc));

                // Creates a bulk job with all of the objects in the bucket.
                job = Helpers.StartReadAllJob(bucketName);

                //add metadata listener
                job.MetadataListener += (fileName, metadata) =>
                {
                    Assert.AreEqual(fileName, "beowulf.txt");
                    CollectionAssert.AreEqual(metadata, new Dictionary<string, string>
                    {
                        {"name", "beowulf.txt"},
                        {"i am a key with spaces", "i am a value with spaces"},
                        {"אני מפתח בעיברית", "אני ערך בעיברית"}
                    });
                };

                // Transfer all of the files.
                job.Transfer(FileHelpers.BuildFileGetter(TestDirectoryDest));
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(Client, bucketName);
            }
        }

        [Test]
        public void TestNullFilterDs3Object()
        {
            const string bucketName = "TestNullFilterDs3Object";

            try
            {
                Client.PutBucket(new PutBucketRequest(bucketName));
                var stream = new MemoryStream(new byte[] {});
                Client.PutObject(new PutObjectRequest(bucketName, "i_am_a_folder/", stream));

                stream = new MemoryStream(new byte[] {});
                Client.PutObject(new PutObjectRequest(bucketName, "i_am_a_zero_length_object", stream));

                var objects = Helpers.ListObjects(bucketName);
                Assert.AreEqual(2, objects.Count());

                var objectsFiltered = Ds3ClientHelpers.FilterDs3Objects(objects, null);
                Assert.AreEqual(2, objectsFiltered.Count());

                objectsFiltered = Ds3ClientHelpers.FilterDs3Objects(objects, Ds3ClientHelpers.FolderFilterPredicate,
                    null);
                Assert.AreEqual(1, objectsFiltered.Count());

                objectsFiltered = Ds3ClientHelpers.FilterDs3Objects(objects, null,
                    Ds3ClientHelpers.ZeroLengthFilterPredicate);
                Assert.AreEqual(0, objectsFiltered.Count());
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(Client, bucketName);
            }
        }

        [Test]
        public void TestOnTransferErrorEvent()
        {
            const string bucketName = "TestOnTransferErrorEvent";
            try
            {
                Helpers.EnsureBucketExists(bucketName);
                const string content = "hi im content";
                var contentBytes = System.Text.Encoding.UTF8.GetBytes(content);

                var stream = new MemoryStream(contentBytes);

                var objects = new List<Ds3Object>
                {
                    new Ds3Object("obj1", contentBytes.Length + 1),
                };

                var job = Helpers.StartWriteJob(bucketName, objects);
                job.OnFailure += (fileName, offset, exception) =>
                {
                    Assert.AreEqual("obj1", fileName);
                    Assert.AreEqual(0, offset);
                    var expectedMessage =
                        $"The Content length ({contentBytes.Length + 1}) not match the number of byte read ({contentBytes.Length})";
                    Assert.AreEqual(expectedMessage, exception.Message);
                };

                Assert.Throws<AggregateException>(() => job.Transfer(s => stream));
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(Client, bucketName);
            }
        }


        [Test]
        public void TestPlusCharacterInQueryParam()
        {
            const string bucketName = "TestPlusCharacterInQueryParam";
            try
            {
                Helpers.EnsureBucketExists(bucketName);

                const string fileName = "Test+Plus+Character";
                var obj = new Ds3Object(fileName, 1024);
                var objs = new List<Ds3Object> {obj};
                var job = Helpers.StartWriteJob(bucketName, objs);

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
                var getObjectsWithNameRequest = new GetObjectsDetailsSpectraS3Request()
                    .WithName(fileName);

                var getObjectsResponse = Client.GetObjectsDetailsSpectraS3(getObjectsWithNameRequest);

                var filename =
                    from f in getObjectsResponse.ResponsePayload.S3Objects
                    where f.Name == fileName
                    select f;

                Assert.AreEqual(1, filename.Count());
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(Client, bucketName);
            }
        }

        [Test]
        public void TestPutObjectWithMaxBlob()
        {
            const string bucketName = "TestPutObjectWithMaxBlob";
            try
            {
                // Creates a bucket if it does not already exist.
                Helpers.EnsureBucketExists(bucketName);

                var directoryObjects =
                    FileHelpers.ListObjectsForDirectory(TestDirectoryBigFolderForMaxBlob, string.Empty).ToList();
                var bulkResult =
                    Client.PutBulkJobSpectraS3(
                        new PutBulkJobSpectraS3Request(bucketName, directoryObjects).WithMaxUploadSize(BlobSize));

                var chunkIds = new HashSet<Guid>();
                foreach (var obj in bulkResult.ResponsePayload.Objects)
                {
                    chunkIds.Add(obj.ChunkId);
                }

                while (chunkIds.Count > 0)
                {
                    var availableChunks =
                        Client.GetJobChunksReadyForClientProcessingSpectraS3(
                            new GetJobChunksReadyForClientProcessingSpectraS3Request(bulkResult.ResponsePayload.JobId));

                    availableChunks.Match(
                        (time, response) =>
                        {
                            // for each chunk that is available, check to make sure
                            // we have not sent it, and if not, send that object
                            AsyncUpload(Client, chunkIds, response, bulkResult.ResponsePayload);
                        },
                        () =>
                        {
                            throw new InvalidOperationException(
                                "The job went away as we were trying to acquire chunk information");
                        },
                        retryAfter =>
                        {
                            // if we did not got some chunks than sleep and retry.  This could mean that the cache is full
                            Thread.Sleep((int) (retryAfter.TotalMilliseconds*1000));
                        });
                }

                VerifyFiles(bucketName, TestDirectoryBigFolderForMaxBlob);
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(Client, bucketName);
            }
        }

        [Test]
        public void TestPutObjectWithSpecifiedLength()
        {
            const string bucketName = "TestPutObjectWithSpecifiedLength";
            try
            {
                Helpers.EnsureBucketExists(bucketName);
                const string content = "hi im content";
                var contentBytes = System.Text.Encoding.UTF8.GetBytes(content);

                var stream = new MemoryStream(contentBytes);
                Client.PutObject(new PutObjectRequest(bucketName, "object", contentBytes.Length, stream));
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(Client, bucketName);
            }
        }

        [Test]
        public void TestPutObjectWithWrongSpecifiedLengthMinus()
        {
            const string bucketName = "TestPutObjectWithWrongSpecifiedLengthMinus";
            try
            {
                Helpers.EnsureBucketExists(bucketName);
                const string content = "hi im content";
                var contentBytes = System.Text.Encoding.UTF8.GetBytes(content);

                var stream = new MemoryStream(contentBytes);
                Assert.Throws<Ds3ContentLengthNotMatch>(
                    () => Client.PutObject(new PutObjectRequest(bucketName, "object", contentBytes.Length - 1, stream)));
            }
            finally
            {
                //wait streams closing to be over
                Thread.Sleep(100);
                Ds3TestUtils.DeleteBucket(Client, bucketName);
            }
        }

        [Test]
        public void TestPutObjectWithWrongSpecifiedLengthPlus()
        {
            const string bucketName = "TestPutObjectWithWrongSpecifiedLengthPlus";
            try
            {
                Helpers.EnsureBucketExists(bucketName);
                const string content = "hi im content";
                var contentBytes = System.Text.Encoding.UTF8.GetBytes(content);

                var stream = new MemoryStream(contentBytes);
                Assert.Throws<Ds3ContentLengthNotMatch>(
                    () => Client.PutObject(new PutObjectRequest(bucketName, "object", contentBytes.Length + 1, stream)));
            }
            finally
            {
                //wait streams closing to be over
                Thread.Sleep(100);
                Ds3TestUtils.DeleteBucket(Client, bucketName);
            }
        }

        [Test]
        public void TestPutWithBadChecksum()
        {
            const string bucketName = "TestPutWithBadChecksum";
            try
            {
                // "123456789" has a well known checksum
                const string content = "123456789";
                const string testBadChecksumCrc32C = "4awSwg=="; // transposed two pairs

                var testObject = new Ds3Object("numbers_badcrc.txt", 9);
                var ds3Objs = new List<Ds3Object> {testObject};

                using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content)))
                {
                    // create or ensure bucket
                    Helpers.EnsureBucketExists(bucketName);
                    // create a job
                    var job = Helpers.StartWriteJob(bucketName, ds3Objs);
                    var putRequest = new PutObjectRequest(bucketName, testObject.Name, stream)
                        .WithJob(job.JobId)
                        .WithOffset(0L);
                    putRequest.WithChecksum(ChecksumType.Value(Convert.FromBase64String(testBadChecksumCrc32C)),
                        ChecksumType.Type.CRC_32C);
                    Assert.Throws<Ds3BadStatusCodeException>(() => Client.PutObject(putRequest));
                }
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(Client, bucketName);
            }
        }

        /* Assume SetUp has completed
        ** but does not rely on others
        **/

        [Test]
        public void TestPutWithChecksum()
        {
            const string bucketName = "TestPutWithChecksum";

            //This test is supported only for BP 1.2
            if (!IsTestSupported(Client, 1.2))
            {
                Assert.Ignore();
            }

            try
            {
                // grab a file
                var directoryObjects = FileHelpers.ListObjectsForDirectory(TestDirectorySrc);
                Assert.IsNotEmpty(directoryObjects);
                var testObject = directoryObjects.First();
                var ds3Objs = new List<Ds3Object> {testObject};

                // create or ensure bucket
                Helpers.EnsureBucketExists(bucketName);
                // create a job
                var job = Helpers.StartWriteJob(bucketName, ds3Objs);

                // instantiate a PutObjectRequest
                var fs = File.Open(TestDirectorySrc + Path.DirectorySeparatorChar + testObject.Name,
                    FileMode.Open);
                var putRequest = new PutObjectRequest(bucketName, testObject.Name, fs)
                    .WithJob(job.JobId)
                    .WithOffset(0L);
                putRequest.WithChecksum(ChecksumType.Compute, ChecksumType.Type.CRC_32C);
                Client.PutObject(putRequest);
                fs.Close();
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(Client, bucketName);
            }
        }

        [Test]
        public void TestPutWithSuppliedChecksum()
        {
            const string bucketName = "TestPutWithSuppliedChecksum";
            //This test is supported only for BP 1.2
            if (!IsTestSupported(Client, 1.2))
            {
                Assert.Ignore();
            }

            try
            {
                // "123456789" has a well known checksum
                const string content = "123456789";
                const string testChecksumCrc32C = "4waSgw==";

                var testObject = new Ds3Object("numbers.txt", 9);
                var ds3Objs = new List<Ds3Object> {testObject};

                using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content)))
                {
                    // create or ensure bucket
                    Helpers.EnsureBucketExists(bucketName);
                    // create a job
                    var job = Helpers.StartWriteJob(bucketName, ds3Objs);
                    var putRequest = new PutObjectRequest(bucketName, testObject.Name, stream)
                        .WithJob(job.JobId)
                        .WithOffset(0L);
                    putRequest.WithChecksum(ChecksumType.Value(Convert.FromBase64String(testChecksumCrc32C)),
                        ChecksumType.Type.CRC_32C);
                    Client.PutObject(putRequest);
                }
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(Client, bucketName);
            }
        }

        [Test]
        public void TestSpecialCharacterInObjectName()
        {
            const string bucketName = "TestSpecialCharacterInObjectName";
            try
            {
                Helpers.EnsureBucketExists(bucketName);

                const string fileName =
                    "varsity1314/_projects/VARSITY 13-14/_versions/Varsity 13-14 (2015-10-05 1827)/_project/Trash/PCMAC HD.avb";
                var obj = new Ds3Object(fileName, 1024);
                var objs = new List<Ds3Object> {obj};
                var job = Helpers.StartWriteJob(bucketName, objs);

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
                Ds3TestUtils.DeleteBucket(Client, bucketName);
            }
        }

        [Test]
        public void TestZeroLengthFilterDs3Object()
        {
            const string bucketName = "TestZeroLengthFilterDs3Object";

            try
            {
                Client.PutBucket(new PutBucketRequest(bucketName));
                var stream = new MemoryStream(new byte[] {});
                Client.PutObject(new PutObjectRequest(bucketName, "i_am_a_zero_length_object", stream));

                var objects = Helpers.ListObjects(bucketName);
                Assert.AreEqual(1, objects.Count());

                var objectsFiltered = Ds3ClientHelpers.FilterDs3Objects(objects,
                    Ds3ClientHelpers.ZeroLengthFilterPredicate);
                Assert.AreEqual(0, objectsFiltered.Count());
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(Client, bucketName);
            }
        }
    }
}