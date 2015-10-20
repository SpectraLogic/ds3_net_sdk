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
using Ds3.Helpers;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
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

       private static string TESTDIR = "TestObjectData";
       private static string TESTBUCKET = "TestBucket" + DateTime.Now.Ticks;
       private static string PREFIX = "test_";
       private static string FOLDER = "joyce";
       private string testdirectorySrc { get; set; }
       private string testdirectoryDest { get; set; }
       private string testdirectoryDestPrefix { get; set; }

        private IDs3Client _client { get; set; }
        private Ds3ClientHelpers _helpers { get; set; }
        public string _endpoint { private get; set; }
        public string _proxy { private get; set; }
        public Credentials _credentials { private get; set; }

        public string BuildRev { private set; get; }
        public string BuildVersion { private set; get; }
        public string BuildBranch { private set; get; }

       #region setup
        [SetUp]
        public void startup()
        {
            _endpoint = Environment.GetEnvironmentVariable("DS3_ENDPOINT");
            string accesskey = Environment.GetEnvironmentVariable("DS3_ACCESS_KEY");
            string secretkey = Environment.GetEnvironmentVariable("DS3_SECRET_KEY");
            _proxy = Environment.GetEnvironmentVariable("http_proxy");
            _credentials = new Credentials(accesskey, secretkey);
            Ds3Builder builder = new Ds3Builder(_endpoint, _credentials);
            if (!string.IsNullOrEmpty(_proxy))
            {
                builder.WithProxy(new Uri(_proxy));
            }
            _client = builder.Build();
            _helpers = new Ds3ClientHelpers(_client);

            setupTestData();
       }

       private void setupTestData()
       {
           string root = Path.GetTempPath() + TESTDIR + Path.DirectorySeparatorChar;
           testdirectorySrc = root + "src" + Path.DirectorySeparatorChar;
           string testdirectorySrcFolder = root + "src" + Path.DirectorySeparatorChar + FOLDER + Path.DirectorySeparatorChar;
           testdirectoryDest = root + "dest" + Path.DirectorySeparatorChar;
           testdirectoryDestPrefix = root + "destPrefix" + Path.DirectorySeparatorChar;

           // create and populate a new test dir
           if (Directory.Exists(root))
           {
               Directory.Delete(root, true);
           }
           Directory.CreateDirectory(root);
           Directory.CreateDirectory(testdirectorySrc);
           Directory.CreateDirectory(testdirectorySrcFolder);
           Directory.CreateDirectory(testdirectoryDest);
           Directory.CreateDirectory(testdirectoryDestPrefix);

           foreach (var book in BOOKS)
           {
               TextWriter writer = new StreamWriter(testdirectorySrc + book);
               var booktext = ReadResource("IntegrationTestDS3.TestData." + book);
               writer.Write(booktext);
               writer.Close();
           }
           foreach (var book in JOYCEBOOKS)
           {
               TextWriter writer = new StreamWriter(testdirectorySrcFolder + book);
               var booktext = ReadResource("IntegrationTestDS3.TestData." + book);
               writer.Write(booktext);
               writer.Close();
           }
       }

       private static string ReadResource(string resourceName)
       {
           using (var srcFile = Assembly.GetExecutingAssembly().GetManifestResourceStream(resourceName))
           using (var reader = new StreamReader(srcFile)) {
               return reader.ReadToEnd();
           }
       }

       #endregion setup

       #region ping

       [Test]
       public void DoIntegrationPing()
       {
           VerifySystemHealthRequest request = new Ds3.Calls.VerifySystemHealthRequest();
           VerifySystemHealthResponse response = _client.VerifySystemHealth(request);

           Assert.GreaterOrEqual(response.MillisToVerify, 0);
       }

       [Test]
       public void GetSystemInfo()
       {
           // get valid data and populate properties
           GetSystemInformationRequest request = new Ds3.Calls.GetSystemInformationRequest();
           GetSystemInformationResponse response = _client.GetSystemInformation(request);
           Assert.IsNotNullOrEmpty(response.BuildVersion);
           BuildBranch = response.BuildBranch;
           BuildRev = response.BuildRev;
           BuildVersion = response.BuildVersion;
       }
       #endregion ping

       #region sequential tests

       [Test]
       public void Test0010BulkPutNoPrefix()
       {
           // Creates a bucket if it does not already exist.
           _helpers.EnsureBucketExists(TESTBUCKET);

           var antefolder = listBucketObjects();
           int antefoldercount = antefolder.Count();

           // Creates a bulk job with the server based on the files in a directory (recursively).
           var directoryobjects =  FileHelpers.ListObjectsForDirectory(testdirectorySrc, string.Empty);
           Assert.Greater(directoryobjects.Count(), 0);
           IJob job = _helpers.StartWriteJob(TESTBUCKET, directoryobjects);

           // Transfer all of the files.
           job.Transfer(FileHelpers.BuildFilePutter(testdirectorySrc, string.Empty));

           // all files there?
           var postfolder = listBucketObjects();
           int postfoldercount = postfolder.Count();
           Assert.AreEqual(postfoldercount - antefoldercount, directoryobjects.Count());
       }

       [Test]
       public void Test0020BulkPutWithPrefix()
       {
           // Creates a bucket if it does not already exist.
           _helpers.EnsureBucketExists(TESTBUCKET);

           var antefolder = listBucketObjects();
           int antefoldercount = antefolder.Count();

           // Creates a bulk job with the server based on the files in a directory (recursively).
           var directoryobjects = FileHelpers.ListObjectsForDirectory(testdirectorySrc, PREFIX);
           Assert.Greater(directoryobjects.Count(), 0);
           IJob job = _helpers.StartWriteJob(TESTBUCKET, directoryobjects);

           // Transfer all of the files.
           job.Transfer(FileHelpers.BuildFilePutter(testdirectorySrc, PREFIX));

           // put all files?
           var postfolder = listBucketObjects();
           int postfoldercount = postfolder.Count();
           Assert.AreEqual(postfoldercount - antefoldercount, directoryobjects.Count());
       }

       [Test]
       public void Test0110BulkGetWithPrefix()
       {

           var antefolder = FileHelpers.ListObjectsForDirectory(testdirectoryDestPrefix, PREFIX);
           int antefoldercount = antefolder.Count();

           // Creates a bulk job with all of the objects in the bucket.
           IJob job = _helpers.StartReadAllJob(TESTBUCKET);

           // Transfer all of the files.
           job.Transfer(FileHelpers.BuildFileGetter(testdirectoryDestPrefix, PREFIX));

           var postfolder = FileHelpers.ListObjectsForDirectory(testdirectoryDestPrefix, PREFIX);
           int postfoldercount = antefolder.Count();
           Assert.Greater(postfoldercount, antefoldercount);
       }

       [Test]
       public void Test0120BulkGetWithoutPrefix()
       {

           var antefolder = FileHelpers.ListObjectsForDirectory(testdirectoryDest, string.Empty);
           int antefoldercount = antefolder.Count();

           // Creates a bulk job with all of the objects in the bucket.
           IJob job = _helpers.StartReadAllJob(TESTBUCKET);

           // Transfer all of the files.
           job.Transfer(FileHelpers.BuildFileGetter(testdirectoryDest, string.Empty));

           var postfolder = FileHelpers.ListObjectsForDirectory(testdirectoryDest, string.Empty);
           int postfoldercount = antefolder.Count();
           Assert.Greater(postfoldercount, antefoldercount);
       }

       //  [Test] *** DOES NOT WORK IN MONO
       public void Test0500DeleteFolder()
       {
           // now it's there
           var antefolder = listBucketObjects();
           int antefoldercount = antefolder.Count();
           Assert.Greater(antefoldercount, 0);

           // get all with folder name
           IEnumerable<Ds3Object> folderitems = (from o in antefolder
                                                where o.Name.StartsWith(FOLDER)
                                                select o);
           int foldercount = folderitems.Count();
           Assert.Greater(foldercount, 0);

           // delete it
           DeleteFolderRequest request = new Ds3.Calls.DeleteFolderRequest(TESTBUCKET, FOLDER);
           _client.DeleteFolder(request);

           // now it's gone
           var postfolder = listBucketObjects();
           int postfoldercount = postfolder.Count();
           Assert.AreEqual(antefoldercount - postfoldercount, foldercount);
       }

       IEnumerable<Ds3Object> listBucketObjects()
       {
           var request = new Ds3.Calls.GetObjectsRequest()
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
           var request = new Ds3.Calls.GetBucketRequest("NoBucket" + DateTime.Now.Ticks);
           _client.GetBucket(request);
       }

       //  [Test] *** DOES NOT WORK IN MONO
       public void Test0910DeleteObject()
       {
           var antefolder = listBucketObjects();
           int antefoldercount = antefolder.Count();
           // delete the first book 
           string book = BOOKS.First<string>();
           var request = new Ds3.Calls.DeleteObjectRequest(TESTBUCKET, string.Empty + book);
           string debugme = request.ObjectName;
           Console.WriteLine(debugme);
           _client.DeleteObject(request);

           // one less ?
           var postfolder = listBucketObjects();
           int postfoldercount = postfolder.Count();
           Assert.AreEqual(antefoldercount - postfoldercount, 1);
       }

       //  [Test] *** DOES NOT WORK IN MONO
       [ExpectedException(typeof(Ds3.Runtime.Ds3BadStatusCodeException))]
       public void Test0915DeleteDeletedObject()
       {
           // delete the first book, again
           string book = BOOKS.First<string>();
           var request = new Ds3.Calls.DeleteObjectRequest(TESTBUCKET, string.Empty + book);
               _client.DeleteObject(request);
       }

       //  [Test] *** DOES NOT WORK IN MONO
       public void Test0920DeleteObjectWithPrefix()
       {
           var antefolder = listBucketObjects();
           int antefoldercount = antefolder.Count();
           // delete the dirst book
           string book = BOOKS.First<string>();
           var request = new Ds3.Calls.DeleteObjectRequest(TESTBUCKET, PREFIX + book);
           _client.DeleteObject(request);

           // one less ?
           var postfolder = listBucketObjects();
           int postfoldercount = postfolder.Count();
           Assert.AreEqual(antefoldercount - postfoldercount, 1);
       }

       //  [Test] *** DOES NOT WORK IN MONO
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

       [Test]
       public void TestSpecialCharacter()
       {
            _helpers.EnsureBucketExists(TESTBUCKET);

            var fileName = "varsity1314/_projects/VARSITY 13-14/_versions/Varsity 13-14 (2015-10-05 1827)/_project/Trash/PCMAC HD.avb";
            var obj = new Ds3Object(fileName, 1024);
            var objs = new List<Ds3Object>();
            objs.Add(obj);
            try {
            var job = _helpers.StartWriteJob(TESTBUCKET, objs);

            job.Transfer(key => {
                var data = new byte[1024];
                var stream = new MemoryStream(data);
                for (int i = 0; i < 1024; i++) {
                    stream.WriteByte(97);
                }

                stream.Seek(0, SeekOrigin.Begin);

                return stream;
            });
            } finally {
                DeleteObject(TESTBUCKET, fileName);
            }
       }

       public void DeleteObject(string bucketname, string objectName)
       {
           var request = new Ds3.Calls.DeleteObjectRequest(bucketname, objectName);
           _client.DeleteObject(request);
       }

       public void DeleteBucket(string bucketname)
       {
           var request = new Ds3.Calls.DeleteBucketRequest(bucketname);
           _client.DeleteBucket(request);
       }


       #endregion sequential tests

    }
}
