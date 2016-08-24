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
using Ds3;
using Ds3.Helpers;
using Ds3.Helpers.Strategys;
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
                var helpers = new Ds3ClientHelpers(this._client, objectTransferAttemps:4);
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
                var helpers = new Ds3ClientHelpers(this._client, objectTransferAttemps: 4);
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
    }
}
