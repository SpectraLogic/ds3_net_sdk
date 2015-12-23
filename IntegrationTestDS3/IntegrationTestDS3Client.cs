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
using Ds3.Helpers;
using Ds3.Helpers.Streams;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading;
using IntegrationTestDS3;

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
        private static string TESTBUCKET = "TestBucket" + DateTime.Now.Ticks;
        private static string PREFIX = "test_";
        private static string FOLDER = "joyce";
        private static string BIG = "big";
        private static string BIGFORMAXBLOB = "bigForMaxBlob";
        private static long MAXBLOBSIZE = 10 * 1024 * 1024;

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
            string root = Path.GetTempPath() + TESTDIR + Path.DirectorySeparatorChar;
            testDirectorySrc = root + "src" + Path.DirectorySeparatorChar;
            string testDirectorySrcFolder = root + "src" + Path.DirectorySeparatorChar + FOLDER + Path.DirectorySeparatorChar;
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
                TextWriter writer = new StreamWriter(testDirectorySrc + book);
                using (Stream bookstream = ReadResource("IntegrationTestDS3.TestData." + book))
                {
                    using (StreamReader booktext = new StreamReader(bookstream))
                    {
                        writer.Write(booktext);
                    }
                }
                writer.Close();
            }
            foreach (var book in JOYCEBOOKS)
            {
                TextWriter writer = new StreamWriter(testDirectorySrcFolder + book);
                using (Stream bookstream = ReadResource("IntegrationTestDS3.TestData." + book))
                {
                    using (StreamReader booktext = new StreamReader(bookstream))
                    {
                        writer.Write(booktext);
                    }
                }
                writer.Close();
            }
            foreach (var bigFile in BIGFILES)
            {
                TextWriter writer = new StreamWriter(testDirectoryBigFolder + bigFile);
                TextWriter writerForMaxBlob = new StreamWriter(testDirectoryBigFolderForMaxBlob + bigFile + "_maxBlob");

                using (Stream bookstream = ReadResource("IntegrationTestDS3.TestData." + bigFile))
                {
                    using (StreamReader booktext = new StreamReader(bookstream))
                    {
                        writer.Write(booktext);
                        writerForMaxBlob.Write(booktext);
                    }
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
            VerifySystemHealthRequest request = new VerifySystemHealthRequest();
            VerifySystemHealthResponse response = _client.VerifySystemHealth(request);

            Assert.GreaterOrEqual(response.MillisToVerify, 0);
        }

        [Test]
        public void GetSystemInfo()
        {
            // get valid data and populate properties
            GetSystemInformationRequest request = new GetSystemInformationRequest();
            GetSystemInformationResponse response = _client.GetSystemInformation(request);
            Assert.IsNotNullOrEmpty(response.BuildVersion);
            BuildBranch = response.BuildBranch;
            BuildRev = response.BuildRev;
            BuildVersion = response.BuildVersion;
        }
        #endregion ping

        #region independent tests
        /* Assume SetUp has completed 
        ** but does not rely on others
        **/

        [Test]
        public void TestPutWithChecksum()
        {
            // grab a file
            var directoryObjects = FileHelpers.ListObjectsForDirectory(testDirectorySrc);
            Assert.IsNotEmpty(directoryObjects);
            Ds3Object testObject = directoryObjects.First<Ds3Object>();
            var ds3Objs = new List<Ds3Object>();
            ds3Objs.Add(testObject);

            // create or ensure bucket
            _helpers.EnsureBucketExists(TESTBUCKET);
            // create a job
            var job = _helpers.StartWriteJob(TESTBUCKET, ds3Objs);

            // instantiate a PutObjectRequest
            FileStream fs = File.Open(testDirectorySrc + Path.DirectorySeparatorChar + testObject.Name, FileMode.Open);
            PutObjectRequest putRequest = new PutObjectRequest(TESTBUCKET, testObject.Name, job.JobId, 0L, fs);
            putRequest.WithChecksum(Checksum.Compute, Checksum.ChecksumType.Crc32C);
            _client.PutObject(putRequest);
            fs.Close();

            //clean up
            DeleteObject(TESTBUCKET, testObject.Name);
        }

        [Test]
        public void TestPutWithSuppliedChecksum()
        {
            // "123456789" has a well known checksum
            string content = "123456789";
            string testChecksumCrc32C = "4waSgw==";

            Ds3Object testObject = new Ds3Object("numbers.txt", 9);
            var ds3Objs = new List<Ds3Object> {testObject};

            using (var stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content)))
            {
                // create or ensure bucket
                _helpers.EnsureBucketExists(TESTBUCKET);
                // create a job
                var job = _helpers.StartWriteJob(TESTBUCKET, ds3Objs);
                PutObjectRequest putRequest = new PutObjectRequest(TESTBUCKET, testObject.Name, job.JobId, 0L, stream);
                putRequest.WithChecksum(Checksum.Value(Convert.FromBase64String(testChecksumCrc32C)), Checksum.ChecksumType.Crc32C);
                _client.PutObject(putRequest);
            }
            //clean up
            DeleteObject(TESTBUCKET, testObject.Name);
        }

        [Test]
        [ExpectedException(typeof(Ds3.Runtime.Ds3BadStatusCodeException))]
        public void TestPutWithBadChecksum()
        {
            // "123456789" has a well known checksum
            string content = "123456789";
            string testBadChecksumCrc32C = "4awSwg=="; // transposed two pairs

            Ds3Object testObject = new Ds3Object("numbers_badcrc.txt", 9);
            var ds3Objs = new List<Ds3Object>();
            ds3Objs.Add(testObject);


            using (MemoryStream stream = new MemoryStream(System.Text.Encoding.UTF8.GetBytes(content)))
            {
                // create or ensure bucket
                _helpers.EnsureBucketExists(TESTBUCKET);
                // create a job
                var job = _helpers.StartWriteJob(TESTBUCKET, ds3Objs);
                PutObjectRequest putRequest = new PutObjectRequest(TESTBUCKET, testObject.Name, job.JobId, 0L, stream);
                putRequest.WithChecksum(Checksum.Value(Convert.FromBase64String(testBadChecksumCrc32C)), Checksum.ChecksumType.Crc32C);
                _client.PutObject(putRequest);
            }
            // CURRENT SIMULATOR CREATES THIS OBJECT -- PROBABLY SHOULD NOT
            DeleteObject(TESTBUCKET, testObject.Name);
        }

        [Test]
        public void TestSpecialCharacterInObjectName()
        {
            var bucketName = TESTBUCKET;
            _helpers.EnsureBucketExists(bucketName);

            var fileName = "varsity1314/_projects/VARSITY 13-14/_versions/Varsity 13-14 (2015-10-05 1827)/_project/Trash/PCMAC HD.avb";
            var obj = new Ds3Object(fileName, 1024);
            var objs = new List<Ds3Object>();
            objs.Add(obj);
            var job = _helpers.StartWriteJob(bucketName, objs);

            job.Transfer(key =>
            {
                var data = new byte[1024];
                var stream = new MemoryStream(data);
                for (int i = 0; i < 1024; i++)
                {
                    stream.WriteByte(97);
                }

                stream.Seek(0, SeekOrigin.Begin);

                return stream;
            });

            // Is it there?
            var putfile = from f in ListBucketObjects()
                          where f.Name == fileName
                          select f;
            Assert.AreEqual(1, putfile.Count());
            DeleteObject(bucketName, fileName);
        }

        [Test]
        public void TestBulkPutWithBlob()
        {
            // Creates a bucket if it does not already exist.
            _helpers.EnsureBucketExists(TESTBUCKET);

            // Creates a bulk job with the server based on the files in a directory (recursively).
            var directoryObjects = FileHelpers.ListObjectsForDirectory(testDirectoryBigFolder, string.Empty);
            Assert.Greater(directoryObjects.Count(), 0);
            IJob job = _helpers.StartWriteJob(TESTBUCKET, directoryObjects, MAXBLOBSIZE);

            // Transfer all of the files.
            job.Transfer(FileHelpers.BuildFilePutter(testDirectoryBigFolder, string.Empty));

            VerifyFiles(testDirectoryBigFolder);
        }

        [Test]
        public void TestPutObjectWithMaxBlob()
        {
            // Creates a bucket if it does not already exist.
            _helpers.EnsureBucketExists(TESTBUCKET);

            var directoryObjects = FileHelpers.ListObjectsForDirectory(testDirectoryBigFolderForMaxBlob, string.Empty).ToList();
            var bulkResult = _client.BulkPut(new BulkPutRequest(TESTBUCKET, directoryObjects).WithMaxBlobSize(MAXBLOBSIZE));

            var chunkIds = new HashSet<Guid>();
            foreach (var obj in bulkResult.ObjectLists)
            {
                chunkIds.Add(obj.ChunkId);
            }

            while (chunkIds.Count > 0)
            {
                var availableChunks = _client.GetAvailableJobChunks(new GetAvailableJobChunksRequest(bulkResult.JobId));

                availableChunks.Match(
                    (time, response) =>
                    {
                        // for each chunk that is available, check to make sure
                        // we have not sent it, and if not, send that object
                        AsyncUpload(_client, chunkIds, response, bulkResult);
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

            VerifyFiles(testDirectoryBigFolderForMaxBlob);
        }

        private void VerifyFiles(string folder)
        {
            // Creates a bulk job with all of the objects in the bucket.
            IJob job = _helpers.StartReadAllJob(TESTBUCKET);

            // Transfer all of the files.
            job.Transfer(FileHelpers.BuildFileGetter(folder, PREFIX));

            foreach (var file in Directory.GetFiles(folder))
            {
                var fileName = Path.GetFileName(file);
                if (fileName.StartsWith(PREFIX)) continue;

                var origFile = File.OpenRead(Path.GetFullPath(file));
                var newFile = File.OpenRead(Path.GetFullPath(string.Format("{0}{1}{2}", folder, PREFIX, fileName)));

                Assert.AreEqual(
                    System.Security.Cryptography.MD5.Create().ComputeHash(origFile),
                    System.Security.Cryptography.MD5.Create().ComputeHash(newFile)
                    );

                origFile.Close();
                newFile.Close();
            }
        }

        private void AsyncUpload(IDs3Client client, ICollection<Guid> chunkIds, JobResponse response, JobResponse bulkResult)
        {
            Parallel.ForEach(response.ObjectLists,
                chunk =>
                {
                    if (!chunkIds.Contains(chunk.ChunkId)) return;
                    chunkIds.Remove(chunk.ChunkId);

                    // it is possible that if we start resending a chunk, due to the program crashing, that
                    // some objects will already be in cache.  Check to make sure that they are not, and then
                    // send the object to Spectra S3

                    Parallel.ForEach(chunk,
                        obj =>
                        {
                            if (obj.InCache) return;
                            PutObject(client, obj, bulkResult);
                        });
                    Console.WriteLine();
                });
        }

        private void PutObject(IDs3Client client, JobObject obj, JobResponse bulkResult)
        {
            var fileToPut = File.OpenRead(testDirectoryBigFolderForMaxBlob + obj.Name);
            var contentStream = new PutObjectRequestStream(fileToPut, obj.Offset, obj.Length);
            var putObjectRequest = new PutObjectRequest(
                bulkResult.BucketName,
                obj.Name,
                bulkResult.JobId,
                obj.Offset,
                contentStream
                );

            client.PutObject(putObjectRequest);
            fileToPut.Close();
        }


        #endregion

        #region sequential tests
        /*  Nunit runs tests in lex order  
        ** so one test can be contingent on predecessors
        ** Test0900 cleans up test bucket
        **/
        [Test]
        public void Test0050BulkPutNoPrefix()
        {
            // Creates a bucket if it does not already exist.
            _helpers.EnsureBucketExists(TESTBUCKET);

            var antefolder = ListBucketObjects();
            int antefoldercount = antefolder.Count();

            // Creates a bulk job with the server based on the files in a directory (recursively).
            var directoryobjects = FileHelpers.ListObjectsForDirectory(testDirectorySrc, string.Empty);
            Assert.Greater(directoryobjects.Count(), 0);
            IJob job = _helpers.StartWriteJob(TESTBUCKET, directoryobjects);

            // Transfer all of the files.
            job.Transfer(FileHelpers.BuildFilePutter(testDirectorySrc, string.Empty));

            // all files there?
            var postfolder = ListBucketObjects();
            int postfoldercount = postfolder.Count();
            Assert.AreEqual(postfoldercount - antefoldercount, directoryobjects.Count());
        }

        [Test]
        public void Test0060BulkPutWithPrefix()
        {
            // Creates a bucket if it does not already exist.
            _helpers.EnsureBucketExists(TESTBUCKET);

            var antefolder = ListBucketObjects();
            int antefoldercount = antefolder.Count();

            // Creates a bulk job with the server based on the files in a directory (recursively).
            var directoryobjects = FileHelpers.ListObjectsForDirectory(testDirectorySrc, PREFIX);
            Assert.Greater(directoryobjects.Count(), 0);
            IJob job = _helpers.StartWriteJob(TESTBUCKET, directoryobjects);

            // Transfer all of the files.
            job.Transfer(FileHelpers.BuildFilePutter(testDirectorySrc, PREFIX));

            // put all files?
            var postfolder = ListBucketObjects();
            int postfoldercount = postfolder.Count();
            Assert.AreEqual(postfoldercount - antefoldercount, directoryobjects.Count());
        }

        [Test]
        public void Test0110BulkGetWithPrefix()
        {

            var antefolder = FileHelpers.ListObjectsForDirectory(testDirectoryDestPrefix, PREFIX);
            int antefoldercount = antefolder.Count();

            // Creates a bulk job with all of the objects in the bucket.
            IJob job = _helpers.StartReadAllJob(TESTBUCKET);

            // Transfer all of the files.
            job.Transfer(FileHelpers.BuildFileGetter(testDirectoryDestPrefix, PREFIX));

            var postfolder = FileHelpers.ListObjectsForDirectory(testDirectoryDestPrefix, PREFIX);
            int postfoldercount = postfolder.Count();
            Assert.Greater(postfoldercount, antefoldercount);
        }

        [Test]
        public void Test0120BulkGetWithoutPrefix()
        {

            var antefolder = FileHelpers.ListObjectsForDirectory(testDirectoryDest, string.Empty);
            int antefoldercount = antefolder.Count();

            // Creates a bulk job with all of the objects in the bucket.
            IJob job = _helpers.StartReadAllJob(TESTBUCKET);

            // Transfer all of the files.
            job.Transfer(FileHelpers.BuildFileGetter(testDirectoryDest, string.Empty));

            var postfolder = FileHelpers.ListObjectsForDirectory(testDirectoryDest, string.Empty);
            int postfoldercount = postfolder.Count();
            Assert.Greater(postfoldercount, antefoldercount);
        }

        [Test]
        public void Test0500DeleteFolder()
        {
            // now it's there
            var antefolder = ListBucketObjects();
            int antefoldercount = antefolder.Count();
            Assert.Greater(antefoldercount, 0);

            // get all with folder name
            IEnumerable<Ds3Object> folderitems = (from o in antefolder
                                                  where o.Name.StartsWith(FOLDER)
                                                  select o);
            int foldercount = folderitems.Count();
            Assert.Greater(foldercount, 0);

            // delete it
            DeleteFolderRequest request = new DeleteFolderRequest(TESTBUCKET, FOLDER);
            _client.DeleteFolder(request);

            // now it's gone
            var postfolder = ListBucketObjects();
            int postfoldercount = postfolder.Count();
            Assert.AreEqual(antefoldercount - postfoldercount, foldercount);
        }

        IEnumerable<Ds3Object> ListBucketObjects()
        {
            var request = new GetObjectsRequest()
            {
                BucketId = TESTBUCKET
            };
            return _client.GetObjects(request).Objects;
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
        public void Test0520GetBadBucket()
        {
            var request = new GetBucketRequest("NoBucket" + DateTime.Now.Ticks);
            _client.GetBucket(request);
        }

        [Test]
        public void Test0910DeleteObject()
        {
            var antefolder = ListBucketObjects();
            int antefoldercount = antefolder.Count();
            // delete the first book
            string book = BOOKS.First<string>();
            DeleteObject(TESTBUCKET, book);

            // one less ?
            var postfolder = ListBucketObjects();
            int postfoldercount = postfolder.Count();
            Assert.AreEqual(antefoldercount - postfoldercount, 1);
        }

        [Test]
        [ExpectedException(typeof(Ds3.Runtime.Ds3BadStatusCodeException))]
        public void Test0915DeleteDeletedObject()
        {
            // delete the first book, again
            string book = BOOKS.First<string>();
            DeleteObject(TESTBUCKET, book);
        }

        [Test]
        public void Test0920DeleteObjectWithPrefix()
        {
            var antefolder = ListBucketObjects();
            int antefoldercount = antefolder.Count();
            // delete the dirst book
            string book = BOOKS.First<string>();
            DeleteObject(TESTBUCKET, PREFIX + book);

            // one less ?
            var postfolder = ListBucketObjects();
            int postfoldercount = postfolder.Count();
            Assert.AreEqual(antefoldercount - postfoldercount, 1);
        }

        [Test]
        public void Test0990CleanUp()
        {
            var items = _helpers.ListObjects(TESTBUCKET);

            // Loop through all of the objects in the bucket.
            foreach (var obj in items)
            {
                DeleteObject(TESTBUCKET, obj.Name);
            }
            DeleteBucket(TESTBUCKET);
        }

        void DeleteObject(string bucketname, string objectName)
        {
            var request = new DeleteObjectRequest(bucketname, objectName);
            _client.DeleteObject(request);
        }

        void DeleteBucket(string bucketname)
        {
            var request = new DeleteBucketRequest(bucketname);
            _client.DeleteBucket(request);
        }


        #endregion sequential tests

        [TestFixtureTearDown]
        public void CleanUp()
        {
            // sequential tests will delete TESTBUCKET
            // individual tests will not
            // so create if necessary , then delete
            _helpers.EnsureBucketExists(TESTBUCKET);
            var items = _helpers.ListObjects(TESTBUCKET);

            // Loop through all of the objects in the bucket.
            foreach (var obj in items)
            {
                DeleteObject(TESTBUCKET, obj.Name);
            }
            DeleteBucket(TESTBUCKET);
        }
    }
}
