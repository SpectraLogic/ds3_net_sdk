using System;
using System.Xml.Linq;
using System.Reflection;
using System.Xml;
using System.IO;
using NUnit.Framework;
using Ds3Client;
using Ds3Client.Configuration;
using TestDs3Client.Configuration;
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
            using (var stream = new MemoryStream())
            {
                writeToStream(stream);
                stream.Seek(0, SeekOrigin.Begin);
                return ReadFromStream(stream);
            }
        }

        private static string ReadFromStream(Stream stream)
        {
            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }
    }
}
