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
using System.IO;
using System.Reflection;

using NUnit.Framework;

using TestDs3Client.Configuration;
using Ds3Client.Configuration;
using Config = Ds3Client.Configuration.Configuration;

namespace TestDs3Client
{
    [TestFixture]
    public class TestConfigurationParser
    {
        [Test]
        public void TestParseSuccess()
        {
            using (var xmlFile = Assembly.GetExecutingAssembly().GetManifestResourceStream("TestDs3Client.Configuration.TestData.SuccessConfig.xml"))
            {
                var config = ConfigurationParser.Parse(xmlFile);
                Assert.IsNull(config.Name);
                Assert.AreEqual("http://the.end.point/the/path", config.Endpoint.ToString());
                Assert.AreEqual("c3BlY3RyYQ==", config.AccessKey);
                Assert.AreEqual("bG9naWM=", config.SecretKey);
                Assert.IsFalse(config.IsSelected);
            }
        }

        [Test]
        public void TestParseFail1()
        {
            using (var xmlFile = Assembly.GetExecutingAssembly().GetManifestResourceStream("TestDs3Client.Configuration.TestData.FailConfig1.xml"))
            {
                var config = ConfigurationParser.Parse(xmlFile);
                Assert.IsNull(config.Name);
                Assert.AreEqual("http://the.end.point/the/path", config.Endpoint.ToString());
                Assert.AreEqual(null, config.AccessKey);
                Assert.AreEqual("bG9naWM=", config.SecretKey);
                Assert.IsFalse(config.IsSelected);
            }
        }

        [Test]
        public void TestParseInvalidUriConfig()
        {
            using (var xmlFile = Assembly.GetExecutingAssembly().GetManifestResourceStream("TestDs3Client.Configuration.TestData.InvalidUriConfig.xml"))
            {
                var config = ConfigurationParser.Parse(xmlFile);
                Assert.IsNull(config.Name);
                Assert.IsNull(config.Endpoint);
                Assert.AreEqual("c3BlY3RyYQ==", config.AccessKey);
                Assert.AreEqual("bG9naWM=", config.SecretKey);
                Assert.IsFalse(config.IsSelected);
            }
        }

        [Test]
        public void TestUnparseSuccess()
        {
            using (var xmlFile = Assembly.GetExecutingAssembly().GetManifestResourceStream("TestDs3Client.Configuration.TestData.SuccessConfig.xml"))
            {
                var config = new Config
                {
                    Name = "SuccessConfig",
                    Endpoint = new Uri("http://the.end.point/the/path"),
                    AccessKey = "c3BlY3RyYQ==",
                    SecretKey = "bG9naWM="
                };
                Assert.AreEqual(ReadFromStream(xmlFile), ConvertStreamToString(stream => ConfigurationParser.Unparse(config, stream)));
            }
        }

        [Test]
        public void TestCorrectValidateConfiguration()
        {
            ConfigurationParser.ValidateConfiguration(Helpers.SuccessConfig);
        }

        [Test]
        [ExpectedException(typeof(ConfigurationException))]
        public void TestBadEndpointValidateConfiguration1()
        {
            var config = Helpers.CopyConfig(Helpers.SuccessConfig);
            config.Endpoint = new Uri("htttp://a.valid/endpoint");
            ConfigurationParser.ValidateConfiguration(config);
        }

        [Test]
        [ExpectedException(typeof(ConfigurationException))]
        public void TestBadEndpointValidateConfiguration2()
        {
            var config = Helpers.CopyConfig(Helpers.SuccessConfig);
            config.Endpoint = null;
            ConfigurationParser.ValidateConfiguration(config);
        }

        [Test]
        [ExpectedException(typeof(ConfigurationException))]
        public void TestInvalidNameValidateConfiguration()
        {
            var config = Helpers.CopyConfig(Helpers.SuccessConfig);
            config.Name = @"Foo\bar";
            ConfigurationParser.ValidateConfiguration(config);
        }

        private static string ConvertStreamToString(Action<Stream> writeToStream)
        {
            using (var memoryStream = new MemoryStream())
            {
                writeToStream(memoryStream);
                memoryStream.Position = 0L;
                return ReadFromStream(memoryStream);
            }
        }

        private static string ReadFromStream(Stream stream)
        {
            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }
    }
}
