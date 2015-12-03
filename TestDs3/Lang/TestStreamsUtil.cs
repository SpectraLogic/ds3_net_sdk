using System.IO;
using System.Text;
using Ds3.Lang;
using NUnit.Framework;
using TestDs3.Helpers;

namespace TestDs3.Lang
{
    [TestFixture]
    internal class TestStreamsUtil
    {
        [Test]
        public void TestBufferedCopyTo()
        {
            const string message = "This is a test string to make sure we are buffering correctly";
            var messageStream = new MockStream(message);
            var dstStream = new MemoryStream();

            StreamsUtil.BufferedCopyTo(messageStream, dstStream, 10);

            var testMessage = new UTF8Encoding(false).GetString(dstStream.GetBuffer(), 0, (int) dstStream.Length);

            Assert.AreEqual(message, testMessage);
        }
    }
}