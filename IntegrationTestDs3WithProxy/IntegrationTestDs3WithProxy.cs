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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Ds3;
using Ds3.Helpers;
using Ds3.Models;
using Ds3.Runtime;
using IntegrationTestDS3;
using NUnit.Framework;

namespace IntegrationTestDs3WithProxy
{
    [TestFixture]
    public class IntegrationTestDs3WithProxy
    {
        private IDs3Client _client;

        private const string FixtureName = "integration_test_ds3client_with_proxy";
        private static TempStorageIds _envStorageIds;

        [OneTimeSetUp]
        public void Startup()
        {
            try
            {
                this._client = Ds3Builder.FromEnv().WithProxy(new Uri("http://localhost:9090")).Build();

                var dataPolicyId = TempStorageUtil.SetupDataPolicy(FixtureName, false, ChecksumType.Type.MD5, _client);
                _envStorageIds = TempStorageUtil.Setup(FixtureName, dataPolicyId, _client);
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
            TempStorageUtil.TearDown(FixtureName, _envStorageIds, _client);
        }

        [Test]
        public void TestWithRetransmitFailingPutBlobs()
        {
            const string bucketName = "TestWithRetransmitFailingPutBlobs";
            try
            {
                var helpers = new Ds3ClientHelpers(this._client, objectTransferAttempts:4);
                helpers.EnsureBucketExists(bucketName);
                const string content = "hi im content";
                var contentBytes = System.Text.Encoding.UTF8.GetBytes(content);

                var stream = new MemoryStream(contentBytes);

                var objects = new List<Ds3Object>
                {
                    new Ds3Object("obj1", contentBytes.Length)
                };

                var job = helpers.StartWriteJob(bucketName, objects);

                job.Transfer(s => stream);
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(_client, bucketName);
            }
        }

        [Test]
        public void TestWithRetransmitFailingPutBlobsWithNonSeekableStream()
        {
            const string bucketName = "TestWithRetransmitFailingPutBlobsWithNonSeekableStream";
            try
            {
                var helpers = new Ds3ClientHelpers(this._client, objectTransferAttempts: 4);
                helpers.EnsureBucketExists(bucketName);

                const string content = "hi im content";
                var contentBytes = System.Text.Encoding.UTF8.GetBytes(content);

                var stream = new NonSeekableStream(new MemoryStream(contentBytes));

                var objects = new List<Ds3Object>
                {
                    new Ds3Object("obj1", contentBytes.Length)
                };

                var job = helpers.StartWriteJob(bucketName, objects);

                try
                {
                    job.Transfer(s => stream);
                    Assert.Fail();
                }
                catch (AggregateException age)
                {
                    Assert.AreEqual(typeof(Ds3NotSupportedStream), age.InnerExceptions[0].GetType());
                }
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(_client, bucketName);
            }
        }

        

        [Test]
        public void TestGetBestEffort()
        {
            const string bucketName = "TestGetBestEffort";

            try
            {
                var helpers = new Ds3ClientHelpers(this._client);
                helpers.EnsureBucketExists(bucketName);

                //Upload data for the test
                //3 files: 1 with 3 blobs, 1 with 2 blobs and 1 with 1 blob
                const int blobSize = 10485760; //10MB blob size
                var putJob = helpers.StartWriteJob(bucketName, Utils.Objects, ds3WriteJobOptions: new Ds3WriteJobOptions { MaxUploadSize = blobSize });
                putJob.Transfer(Utils.ReadResource);


                //Getting the data back
                //1 blob will be missing from the 3 blobs object 
                var getJob = helpers.StartReadJob(bucketName, Utils.Objects);

                var dataTransfers = new ConcurrentQueue<long>();
                var itemsCompleted = new ConcurrentQueue<string>();
                getJob.DataTransferred += dataTransfers.Enqueue;
                getJob.ItemCompleted += itemsCompleted.Enqueue;


                Assert.Throws<AggregateException>(() => getJob.Transfer(s => new MemoryStream()));

                CollectionAssert.AreEquivalent(new[] { 10485760, 10485760, 10485760, 8027314, 8160373 }, dataTransfers); //7229224 will have an exception
                CollectionAssert.AreEquivalent(Utils.Objects.Select(obj => obj.Name).Where(obj => !obj.Equals("3_blobs.txt")), itemsCompleted);
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(_client, bucketName);
            }
        }

        [Test]
        public void TestPutBestEffort()
        {
            const string bucketName = "TestPutBestEffort";

            try
            {
                var helpers = new Ds3ClientHelpers(this._client);
                helpers.EnsureBucketExists(bucketName);

                //Upload data for the test
                //3 files: 1 with 3 blobs, 1 with 2 blobs and 1 with 1 blob
                const int blobSize = 10485760; //10MB blob size
                var putJob = helpers.StartWriteJob(bucketName, Utils.Objects, ds3WriteJobOptions: new Ds3WriteJobOptions { MaxUploadSize = blobSize });

                var dataTransfers = new ConcurrentQueue<long>();
                var itemsCompleted = new ConcurrentQueue<string>();
                putJob.DataTransferred += dataTransfers.Enqueue;
                putJob.ItemCompleted += itemsCompleted.Enqueue;


                //1 blob from 3_blobs.txt will throw an exception
                Assert.Throws<AggregateException>(() => putJob.Transfer(Utils.ReadResource));

                CollectionAssert.AreEquivalent(new[] { 10485760, 10485760, 10485760, 8027314, 8160373 }, dataTransfers); //7229224 will have an exception
                CollectionAssert.AreEquivalent(Utils.Objects.Select(obj => obj.Name).Where(obj => !obj.Equals("3_blobs.txt")), itemsCompleted);
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(_client, bucketName);
            }
        }
    }
}
