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
using NUnit.Framework;
using Ds3;
using Ds3.Models;
using System.IO;
using System.Linq;
using Ds3.Calls;
using Ds3.Helpers;

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
             var builder = Ds3Builder.FromEnv();
             _client = builder.Build();
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
            const string bucketName = "integrityBucket";

            try
            {
                Ds3TestUtils.LoadTestData(_client, bucketName);

                var file = Ds3TestUtils.GetSingleObject(_client, bucketName, "beowulf.txt");
                _tempFiles.Add(file);

                var sha1 = Ds3TestUtils.ComputeSha1(file);

                Assert.AreEqual("jtpN/ZmICOS8pWseFd0+MX2CII0=", sha1);
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(_client, bucketName);
            }
        }

        [Test]
        public void GetPartialData()
        {
            const string bucketName = "partialDataBucket";

            try
            {
                Ds3TestUtils.LoadTestData(_client, bucketName);

                var file = Ds3TestUtils.GetSingleObjectWithRange(_client, bucketName, "beowulf.txt", Range.ByLength(0,1046));
                _tempFiles.Add(file);

                var sha1 = Ds3TestUtils.ComputeSha1(file);

                Assert.AreEqual("pHmefq7JfKf4Kd3Yh8WjEf1jLAM=", sha1);
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(_client, bucketName);
            }
        }

        [Test]
        public void PutLargeNumberOfObjects()
        {

            const string bucketName = "lotsOfFiles";

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
        public void TestEventEmiter()
        {
            const string bucketName = "eventEmitter";

            try
            {
                var counter = 0;
                Ds3TestUtils.LoadTestData(_client, bucketName);

                var ds3ObjList = new List<Ds3Object> 
                {
                    new Ds3Object("beowulf.txt", null)
                };

                var helpers = new Ds3ClientHelpers(_client);

                var job = helpers.StartReadJob(bucketName, ds3ObjList);

                job.ItemCompleted += item =>
                {
                    Console.WriteLine(@"Got completed event for " + item);
                    counter++;
                };
                job.Transfer(name => Stream.Null);

                Assert.AreEqual(1, counter);
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(_client, bucketName);
            }
        }
    }
}
