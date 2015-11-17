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

namespace IntegrationTestDS3
{
    [TestFixture]
    class DataIntegrity
    {

        private IDs3Client _client;
        private readonly List<string> _tempFiles = new List<string>();

        [SetUp]
        public void Setup()
        {
            /*
             * Ds3Builder builder = Ds3Builder.FromEnv();
             * this._client = builder.Build();
             * 
             */
            string _endpoint = "http://192.168.56.102:8080";
            string accesskey = "c3BlY3RyYQ==";
            string secretkey = "womvedQo";
            string _proxy = "http://192.168.56.1:9090";
            //string _proxy = "";
            var _credentials = new Credentials(accesskey, secretkey);
            Ds3Builder builder = new Ds3Builder(_endpoint, _credentials);
            if (!string.IsNullOrEmpty(_proxy))
            {
                builder.WithProxy(new Uri(_proxy));
            }
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
            var bucketName = "integrityBucket";

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
            var bucketName = "partialDataBucket";

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
    }
}
