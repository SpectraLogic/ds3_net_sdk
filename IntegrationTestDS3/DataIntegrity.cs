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

using Ds3;
using Ds3.Helpers;
using Ds3.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;

namespace IntegrationTestDS3
{
    [TestFixture]
    internal class DataIntegrity
    {
        private IDs3Client _client;
        private readonly List<string> _tempFiles = new List<string>();

        private static string FixtureName = "data_integrity";
        private static TempStorageIds EnvStorageIds;

        [SetUp]
        public void Setup()
        {
            try
            {
                _client = Ds3TestUtils.CreateClient();
                Guid dataPolicyId = TempStorageUtil.SetupDataPolicy(FixtureName, false, ChecksumType.Type.MD5, _client);
                EnvStorageIds = TempStorageUtil.Setup(FixtureName, dataPolicyId, _client);
            }
            catch (Exception)
            {
                // So long as any SetUp method runs without error, the TearDown method is guaranteed to run.
                // It will not run if a SetUp method fails or throws an exception.
                Teardown();
                throw;
            }
        }

        [TearDown]
        public void Teardown()
        {
            foreach (var file in _tempFiles)
            {
                File.Delete(file);
            }
            TempStorageUtil.TearDown(FixtureName, EnvStorageIds, _client);
        }

        [Test]
        public void SingleObjectGet()
        {
            Ds3TestUtils.UsingAllWriteStrategies(writeStrategy =>
            {
                const string bucketName = "SingleObjectGet";
                try
                {
                    Ds3TestUtils.LoadTestData(_client, bucketName, writeStrategy);
                    Ds3TestUtils.UsingAllStringReadStrategies(readStrategy =>
                    {
                        var file = Ds3TestUtils.GetSingleObject(_client, bucketName, "beowulf.txt", helperStrategy: readStrategy);
                        _tempFiles.Add(file);

                        var sha1 = Ds3TestUtils.ComputeSha1(file);
                        Assert.AreEqual("jtpN/ZmICOS8pWseFd0+MX2CII0=", sha1);
                    });
                }
                finally
                {
                    Ds3TestUtils.DeleteBucket(_client, bucketName);
                }
            });
        }

        [Test]
        public void GetPartialData()
        {
            const string bucketName = "GetPartialData";

            try
            {
                Ds3TestUtils.LoadTestData(_client, bucketName);
                Ds3TestUtils.UsingAllDs3PartialObjectReadStrategies(strategy =>
                {
                    var file = Ds3TestUtils.GetSingleObjectWithRange(_client, bucketName, "beowulf.txt", Range.ByLength(0, 1046), strategy);
                    _tempFiles.Add(file);

                    var sha1 = Ds3TestUtils.ComputeSha1(file);
                    Assert.AreEqual("pHmefq7JfKf4Kd3Yh8WjEf1jLAM=", sha1);
                });
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(_client, bucketName);
            }
        }

        [Test]
        public void TestJobEvents()
        {
            const string bucketName = "TestJobEvents";

            try
            {
                Ds3TestUtils.LoadTestData(_client, bucketName);

                var ds3ObjList = new List<Ds3Object>
                {
                    new Ds3Object("beowulf.txt", null)
                };

                var helpers = new Ds3ClientHelpers(_client);

                Ds3TestUtils.UsingAllStringReadStrategies(strategy =>
                {
                    var counter = 0;
                    var dataTransfered = 0L;

                    var job = helpers.StartReadJob(bucketName, ds3ObjList, strategy);

                    job.ItemCompleted += item =>
                    {
                        counter++;
                    };

                    job.DataTransferred += item =>
                    {
                        dataTransfered += item;
                    };

                    job.Transfer(name => Stream.Null);

                    Assert.AreEqual(1, counter);
                    Assert.AreEqual(294059, dataTransfered);
                });
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(_client, bucketName);
            }
        }
    }
}