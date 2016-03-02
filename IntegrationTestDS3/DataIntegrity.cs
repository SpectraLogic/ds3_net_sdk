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
using Ds3.Helpers.Strategys;
using Ds3.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace IntegrationTestDS3
{
    [TestFixture]
    internal class DataIntegrity
    {
        private IDs3Client _client;
        private readonly List<string> _tempFiles = new List<string>();

        [SetUp]
        public void Setup()
        {
            _client = Ds3TestUtils.CreateClient();
        }

        [TearDown]
        public void Teardown()
        {
            foreach (var file in _tempFiles)
            {
                File.Delete(file);
            }
        }

        [Test]
        public void SingleObjectGet()
        {
            Ds3TestUtils.UsingAllWriteStrategys(writeStrategy =>
            {
                const string bucketName = "SingleObjectGet";
                try
                {
                    Ds3TestUtils.LoadTestData(_client, bucketName, writeStrategy);
                    Ds3TestUtils.UsingAllStringReadStrategys(readStrategy =>
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
                Ds3TestUtils.UsingAllDs3PartialObjectReadStrategys(strategy =>
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
        public void PutLargeNumberOfObjects()
        {
            const string bucketName = "PutLargeNumberOfObjects";

            try
            {
                const string content = "hi im content";
                var contentBytes = System.Text.Encoding.UTF8.GetBytes(content);

                var objects = new List<Ds3Object>();

                for (var i = 0; i < 1000; i++)
                {
                    objects.Add(new Ds3Object(Guid.NewGuid().ToString(), contentBytes.Length));
                }

                Ds3TestUtils.PutFiles(_client, bucketName, objects, key => new MemoryStream(contentBytes));

                Assert.AreEqual(1000, _client.GetBucket(new GetBucketRequest(bucketName)).Objects.Count());
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

                Ds3TestUtils.UsingAllStringReadStrategys(strategy =>
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