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

using System;
using System.Linq;
using System.Diagnostics;
using System.IO;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

namespace Ds3Examples
{
    class Ds3ExampleClient
    {
        protected IDs3Client _client;
        protected IDs3ClientHelpers _helpers;

        public Ds3ExampleClient(string endpoint, Credentials credentials, string proxy)
        {
            Ds3Builder builder = new Ds3Builder(endpoint, credentials);
            if (!string.IsNullOrEmpty(proxy))
            {
                builder.WithProxy(new Uri(proxy));
            }
            _client = builder.Build();

            // Set up the high-level abstractions.
            _helpers = new Ds3ClientHelpers(_client);
        }

        protected string runPing()
        {
            VerifySystemHealthSpectraS3Response verifySysHealthResponse = VerifySystemHealth();
            HealthVerificationResult verify = verifySysHealthResponse.ResponsePayload;
            Console.WriteLine("VerifySystemHealth() -- {0}ms", verify.MsRequiredToVerifyDataPlannerHealth);

            GetSystemInformationSpectraS3Response sysInfoResponse = GetSystemInfo();
            SystemInformation sysinf = sysInfoResponse.ResponsePayload;

            return string.Format("Object 'ApiVer: {0}' | BackendAct: {1} | SN: {2} | BuildVer: {3} | BuildRev: {4} | BuildPath: {5} ", sysinf.ApiVersion, 
                sysinf.BackendActivated, sysinf.SerialNumber, sysinf.BuildInformation.Version, sysinf.BuildInformation.Revision, sysinf.BuildInformation.Branch);
        }

        protected void runCreateBucket(string bucket)
        {
            // Creates a bucket if it does not already exist.
            _helpers.EnsureBucketExists(bucket);
        }

        protected void runPutFromStream(string bucket, string filename, long size)
        {
            runCreateBucket(bucket);

            // create Ds3Object to store name and size
            var ds3Obj = new Ds3Object(filename, size);
            var ds3Objs = new List<Ds3Object>();
            ds3Objs.Add(ds3Obj);

            // start bulk put for objects list
            IJob job = _helpers.StartWriteJob(bucket, ds3Objs);
            if (clientSwitch.TraceInfo) { Trace.WriteLine(string.Format("runPutFromStream(): Job id {0}", job.JobId)); }
            Console.WriteLine("Started job runPutFromStream()");

            // Provide Func<string, stream> to be called on each object
            job.Transfer(key =>
            {
                var data = new byte[size];
                var stream = new MemoryStream(data);
                for (int i = 0; i < size; i++)
                {
                    stream.WriteByte((byte)(i & 0x7f));
                }

                stream.Seek(0, SeekOrigin.Begin);

                return stream;
            });
        }

        protected void runPut(string bucket, string srcDirectory, string filename)
        {
            // Creates a bucket if it does not already exist.
            runCreateBucket(bucket);

            // get file size, instantiate Ds3Object, add to list
            FileInfo fileInfo =  new FileInfo(Path.Combine(srcDirectory, filename));
            var ds3Obj = new Ds3Object(filename, fileInfo.Length);
            var ds3Objs = new List<Ds3Object>();
            ds3Objs.Add(ds3Obj);

            // Creates a bulk job with the server based on the files in a directory (recursively).
            IJob job = _helpers.StartWriteJob(bucket, ds3Objs);
            if (clientSwitch.TraceInfo) { Trace.WriteLine(string.Format("runPut({1}): Job id {0}", job.JobId, filename)); }
            Console.WriteLine(string.Format("Started job runPut({0})", filename));

            // Provide Func<string, stream> to be called on each object
            job.Transfer(FileHelpers.BuildFilePutter(srcDirectory));
        }

        protected void runBulkPut(string bucket, string srcDirectory, string prefix = "")
        {
            // Creates a bucket if it does not already exist.
            runCreateBucket(bucket);

            // Creates a bulk job with the server based on the files in a directory (recursively).
            IJob job = _helpers.StartWriteJob(bucket, FileHelpers.ListObjectsForDirectory(srcDirectory, prefix));
            if (clientSwitch.TraceInfo) { Trace.WriteLine(string.Format("runBulkPut(): Job id {0}", job.JobId)); }
            Console.WriteLine("Started job runBulkPut()");

            // Transfer all of the files.
            job.Transfer(FileHelpers.BuildFilePutter(srcDirectory, prefix));
        }

        protected void runPutWithChecksum(string bucket, string srcDirectory, string filename, 
                                          string hashString = null, 
                                          ChecksumType.Type checksumType = ChecksumType.Type.MD5)
        {
            _helpers.EnsureBucketExists(bucket);

            FileInfo fileInfo = new FileInfo(Path.Combine(srcDirectory, filename));
            var ds3Obj = new Ds3Object(filename, fileInfo.Length);
            var ds3Objs = new List<Ds3Object>();
            ds3Objs.Add(ds3Obj);

            // create a job
            var job = _helpers.StartWriteJob(bucket, ds3Objs);

            // instantiate a PutObjectRequest
            FileStream fs = File.Open(srcDirectory + Path.DirectorySeparatorChar + filename, FileMode.Open);
            PutObjectRequest putRequest = new PutObjectRequest(bucket, filename, fs)
                .WithJob(job.JobId)
                .WithOffset(0L);

            if (string.IsNullOrEmpty(hashString))
            {
                // Compute checksum
                putRequest.WithChecksum(ChecksumType.Compute, checksumType);
            }
            else
            {
                // or pass in a precomputed Base64 string representation of the hash
                putRequest.WithChecksum(ChecksumType.Value(Convert.FromBase64String(hashString)), checksumType);
            }
            _client.PutObject(putRequest);
            fs.Close();
        }

        protected bool runBulkGet(string bucket, string directory, string prefix)
        {
            // Creates a bulk job with all of the objects in the bucket.
            IJob job = _helpers.StartReadAllJob(bucket);
            // Same as: IJob job = helpers.StartReadJob(bucket, helpers.ListObjects(bucket));
            if (clientSwitch.TraceInfo) { Trace.WriteLine(string.Format("runBulkGet(): Job id {0}", job.JobId)); }
            Console.WriteLine("Started job runBulkGet()");

            // Transfer all of the files.
            job.Transfer(FileHelpers.BuildFileGetter(directory, prefix));

            return true;
        }

        protected bool runGet(string bucket, string directory, string filename)
        {
            // find the desired object 
            var objects = _helpers.ListObjects(bucket);
            var targetobj = (from o in objects
                         where o.Name == filename
                         select o);

            // get it
            IJob job = _helpers.StartReadJob(bucket, targetobj);
            if (clientSwitch.TraceInfo) { Trace.WriteLine(string.Format("runGet({1}): Job id {0}", job.JobId, filename)); }
            Console.WriteLine(string.Format("Started job runGet({0})", filename));

            // Transfer all of the files.
            job.Transfer(FileHelpers.BuildFileGetter(directory, string.Empty));

            return true;
        }

        public void runDeleteObject(string bucketname, string objectName)
        {
            var request = new Ds3.Calls.DeleteObjectRequest(bucketname, objectName);
            _client.DeleteObject(request);
        }

        public void runDeleteBucket(string bucketname)
        {
            var request = new Ds3.Calls.DeleteBucketRequest(bucketname);
            _client.DeleteBucket(request);
        }

        public void runDeleteFolder(string bucketname, string folderName)
        {
            var request = new Ds3.Calls.DeleteFolderRecursivelySpectraS3Request(bucketname, folderName);
            _client.DeleteFolderRecursivelySpectraS3(request);
        }

        protected int runListObjects(string bucket)
        {
            var items = _helpers.ListObjects(bucket);

            Console.WriteLine("------ in {0}: ", bucket);
            // Loop through all of the objects in the bucket.
            foreach (var obj in items)
            {
                Console.WriteLine("Object '{0}' of size {1}.", obj.Name, obj.Size);
            }
            return items.Count<Ds3Object>();
        }
        
        protected bool runDeleteObjects(string bucket)
        {
            var items = _helpers.ListObjects(bucket);

            Console.WriteLine("----- In {0}: ", bucket);

            // Loop through all of the objects in the bucket.
            foreach (var obj in items)
            {
                Console.WriteLine("Deleting '{0}' of size {1}.", obj.Name, obj.Size);
                runDeleteObject(bucket, obj.Name);
            }
            return true;
        }

        protected long runListBuckets()
        {
            var buckets = _client.GetService(new GetServiceRequest()).ResponsePayload.Buckets;
            foreach (var bucket in buckets)
            {
                Console.WriteLine("Bucket '{0}'.", bucket.Name);
            }
            return buckets.Count();
        }

        protected bool runListAll()
        {
            var buckets = _client.GetService(new GetServiceRequest()).ResponsePayload.Buckets;
            foreach (var bucket in buckets)
            {
                runListObjects(bucket.Name);
            }
            return buckets.Count() > 0;
        }

        protected bool runCleanAll(string match)
        {
            var buckets = _client.GetService(new GetServiceRequest()).ResponsePayload.Buckets;
            var matchbuckets = from b in buckets
                               where b.Name.StartsWith(match)
                               select b;
            foreach (var bucket in matchbuckets)
            {
                runDeleteObjects(bucket.Name);
                runDeleteBucket(bucket.Name);
            }
            return buckets.Count() > 0;
        }

        protected bool runGetObjects(string bucket, string name, int length, int offset, S3ObjectType type, long version)
        {
            var items = GetObjects(bucket, name, length, offset, type, version);

            // Loop through all of the objects in the bucket.
            foreach (var obj in items)
            {
                Console.WriteLine("Object '{0}' | {1} | {2} | {3} ", obj.Name, obj.Version, obj.Type, obj.CreationDate);
            }
            return true;
        }

        protected bool runListDir(string sourcedir, string prefix)
        {
            var items =  FileHelpers.ListObjectsForDirectory(sourcedir, prefix);

            // Loop through all of the objects in the bucket.
            foreach (var obj in items)
            {
                Console.WriteLine("Object '{0}' ", obj.Name);
            }
            return true;
        }


        public IEnumerable<S3Object> GetObjects(string bucketName, string objectName, int length, int offset, S3ObjectType type, long version)
        {
            var request = new Ds3.Calls.GetObjectsSpectraS3Request()
            {
                BucketId = bucketName,
                Name = objectName,
                PageLength = length,
                PageOffset = offset,
                Type = type,
                Version = version
            };
            var response = _client.GetObjectsSpectraS3(request);
            foreach (var ds3Object in response.ResponsePayload.S3Objects)
            {
                yield return ds3Object;
            }
        }
  
        public GetSystemInformationSpectraS3Response GetSystemInfo()
        {
            var request = new Ds3.Calls.GetSystemInformationSpectraS3Request();
            return _client.GetSystemInformationSpectraS3(request);
        }

        public VerifySystemHealthSpectraS3Response VerifySystemHealth()
        {
            var request = new Ds3.Calls.VerifySystemHealthSpectraS3Request();
            return _client.VerifySystemHealthSpectraS3(request);
        }


        #region main()

        private static TraceSwitch clientSwitch = new TraceSwitch("clientSwitch","test switch");

        static void Main(string[] args)
        {
            if (clientSwitch.TraceInfo) { Trace.WriteLine("Starting Ds3ExampleClient main()"); }
            if (clientSwitch.TraceWarning) { Trace.WriteLine("Almost out of coffee"); }
            if (clientSwitch.TraceVerbose) { Trace.WriteLine("I think I should share some of my feelings..."); }

            // set the following environment variables or pass in from App.config
            string endpoint = Environment.GetEnvironmentVariable("DS3_ENDPOINT");
            string accesskey = Environment.GetEnvironmentVariable("DS3_ACCESS_KEY");
            string secretkey = Environment.GetEnvironmentVariable("DS3_SECRET_KEY");
            string proxy = Environment.GetEnvironmentVariable("http_proxy");

            if (string.IsNullOrEmpty(endpoint) || string.IsNullOrEmpty(accesskey) || string.IsNullOrEmpty(secretkey))
            {
                string fatalerr = "Must set values for DS3_ENDPOINT, DS3_ACCESS_KEY, and DS3_SECRET_KEY to continue";
                Console.WriteLine(fatalerr);
                if (clientSwitch.TraceWarning) { Trace.WriteLine(fatalerr); }
                return;
            } // if connection paramaters defined

            // out-of-box examples in project
            string bucket = "sdkexamples";
            string testRestoreDirectory = "DataFromBucket";
            string testSourceDirectory = "TestData";
            string testSourceSubDirectory = "FScottKey";
            string testSourceFile = "LoremIpsum.txt";
            string testSourceFile2 = "LoremIpsumCyrillic.txt";
            string binaryFile = "binarytest";
            long binaryFileSize = 4096L;

            string testChecksumFile = "numbers.txt";
            string testChecksumCrc32C = "4waSgw==";

            // set these values to valid locations on local filesystem
            // directory to be copied (should exist and be poulated)
            string sourceDir = "C:\\TestObjectData";
            // destination for restore if not empty (will be created if needed)
            string destDir = "";

            // optional prefix to prepend to filename
            string prefix = "mytest123_";

            // instantiate client
            Ds3ExampleClient exampleClient = new Ds3ExampleClient(endpoint, 
                                    new Credentials(accesskey, secretkey), proxy);

            try
            {
                // set up test files
                Ds3ExampleClient.SetupFiles(testSourceDirectory, testSourceSubDirectory);

                // connect to machine
                string systeminfo = exampleClient.runPing();
                if (clientSwitch.TraceVerbose) { Trace.WriteLine(systeminfo); }

                // List all contents before operations
                Console.WriteLine("\nSTARTING STATE:");
                exampleClient.runListAll();

                // force removal of test bucket from previous executions.
                exampleClient.runCleanAll(bucket);

                #region put objects
                /*************************************************************
                 *** PUT FILES TO DEVICE FROM LOCAL FILESYSTEM  AND STREAM ***
                 *************************************************************/
                // create a bucket on the device
                exampleClient.runCreateBucket(bucket);

                // put a single file into the bucket
                exampleClient.runPut(bucket, testSourceDirectory, testSourceFile);

                // put a single file into the bucket with precomputed checksum
                exampleClient.runPutWithChecksum(bucket, testSourceDirectory, testChecksumFile, testChecksumCrc32C, ChecksumType.Type.CRC_32C);

                // put a single file into the bucket with dynamically generated checksum
                exampleClient.runPutWithChecksum(bucket, testSourceDirectory, testSourceFile2, string.Empty, ChecksumType.Type.MD5);

                // put a file into the bucket from stream
                exampleClient.runPutFromStream(bucket, binaryFile, binaryFileSize);

                // copy the whole directory with a file prefix
                exampleClient.runBulkPut(bucket, testSourceDirectory, prefix);

                // copy a local directory, recursively into the bucket
                if (Directory.Exists(sourceDir))
                {
                    exampleClient.runBulkPut(bucket, sourceDir);
                }
                else
                {
                    if (clientSwitch.TraceInfo) { Trace.WriteLine("set srcDirectory variable to put local data"); }
                }
                // List all contents
                Console.WriteLine("\nAFTER PUT:");
                exampleClient.runListAll();
                
                #endregion put objects

                #region list objects
                /*************************************************
                 ***  LIST OBJECT NAMES FROM DEVICE            ***
                 *************************************************/
                Console.WriteLine("\nLIST:");
                
                // get bucket list
                Console.WriteLine("Buckets:");
                long bucketCount = exampleClient.runListBuckets();

                // get object list
                Console.WriteLine("Objects in {0}:", bucket);
                int objectCount = exampleClient.runListObjects(bucket);
                
                // get object list in pages
                Console.WriteLine("Objects in {0}:", bucket);
                string objectName = null;
                S3ObjectType type = S3ObjectType.DATA;
                long version = 1L;
                int pageSize = objectCount / 3;
                for (int offset = 0; offset < objectCount; offset += pageSize)
                {
                    Console.WriteLine(string.Format("Get {0} (offset = {1})", bucket, offset));
                    exampleClient.runGetObjects(bucket, objectName, pageSize, offset, type, version);
                }

                #endregion listobjects

                #region get objects
                /*************************************************
                 *** RESTORE OBJECTS FROM DEVICE TO FILESYSTEM ***
                 *************************************************/

                // get single file from out-of-box example
                exampleClient.runGet(bucket, testRestoreDirectory, testSourceFile);

                // restore whole bucket into local directory
                if (!string.IsNullOrEmpty(destDir))
                {
                    exampleClient.runBulkGet(bucket, destDir, string.Empty);
                }
                
                #endregion get objects

                #region delete objects

                /*************************************************
                 ***         DELETE FILES FROM DEVICE          ***
                 *************************************************/
                // delete a single object
                exampleClient.runDeleteObject(bucket, testSourceFile);

                // delete all objects in a folder
                exampleClient.runDeleteFolder(bucket, testSourceSubDirectory);

                // delete all objects in a bucket but not the bucket
                exampleClient.runDeleteObjects(bucket);

                // delete an empty bucket
                exampleClient.runDeleteBucket(bucket);

                // List all contents 
                Console.WriteLine("\nAFTER DELETE:");
                exampleClient.runListAll();

                #endregion delete objects

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            // Wait for user input.
            Console.WriteLine("All jobs complete. Hit any key to exit.");
            Console.ReadKey();
        }

        #endregion main()

        #region setup
        
        public static void SetupFiles(string testSourceDirectory, string testSourceSubDirectory)
        {
            string[] files = { "one.txt", "two.txt", "three.txt", "four.txt" };
            string testData = "On the shore dimly seen, through the mists of the deep. ";
            testData += "Where our foe's haughty host in dread silence reposes. ";
            testData += "What is that which the breeze, o'er the towering steep, ";
            testData += "As it fitfully blows, half conceals half discloses? ";
            testData += "Now it catches the gleam of the morning’s first beam, ";
            testData += "In full glory reflected now shines in the stream: ";
            testData += "Tis the star-spangled banner! Oh long may it wave, ";
            testData += "O’er the land of the free and the home of the brave!";
 
            // create and populate a new test dir
            if (Directory.Exists(testSourceDirectory))
            {
                string subdir = Path.Combine(testSourceDirectory, testSourceSubDirectory);
                Directory.CreateDirectory(subdir);
                foreach (var file in files)
                {
                    TextWriter writer = new StreamWriter(Path.Combine(subdir, file));
                    writer.WriteLine(testData);
                    writer.Close();
                }
            }
        }

        #endregion setup
    }
}
