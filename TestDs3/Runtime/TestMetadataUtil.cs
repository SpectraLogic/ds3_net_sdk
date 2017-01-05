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

using Ds3.Runtime;
using NUnit.Framework;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace TestDs3.Runtime
{
    [TestFixture]
    public class TestMetadataUtil
    {
        private static readonly string _encodedPattern = @"^[a-zA-Z0-9\-\!\#\$\&\'\*\-\.\~\^_\`\|\,\=\%]*$";

        private static readonly string _spaces = "String With Spaces";
        private static readonly string _symbols = "1234567890-!@#$%^&*()_+`~[]\\{}|;':\"./<>?\u03C0\u221E\u03CA\u03D5\u03E0";

        private static bool IsEncodedSafe(string str)
        {
            return Regex.Match(str, _encodedPattern).Success;
        }

        [Test]
        public void TestEncodePattern()
        {
            Assert.IsTrue(IsEncodedSafe(""));
            Assert.IsTrue(IsEncodedSafe("abc123"));
            Assert.IsTrue(IsEncodedSafe("!#$&'*-.~^_`|,=%"));

            Assert.IsFalse(IsEncodedSafe(" "));
            Assert.IsFalse(IsEncodedSafe("{"));
            Assert.IsFalse(IsEncodedSafe("}"));
            Assert.IsFalse(IsEncodedSafe("["));
            Assert.IsFalse(IsEncodedSafe("]"));
            Assert.IsFalse(IsEncodedSafe("<"));
            Assert.IsFalse(IsEncodedSafe(">"));
            Assert.IsFalse(IsEncodedSafe("@"));
            Assert.IsFalse(IsEncodedSafe(";"));
            Assert.IsFalse(IsEncodedSafe(":"));
            Assert.IsFalse(IsEncodedSafe("\\"));
            Assert.IsFalse(IsEncodedSafe("\""));
            Assert.IsFalse(IsEncodedSafe("/"));
            Assert.IsFalse(IsEncodedSafe("?"));
            Assert.IsFalse(IsEncodedSafe("+"));

            Assert.IsFalse(IsEncodedSafe("\u03C0"));
            Assert.IsFalse(IsEncodedSafe("\u221E"));
            Assert.IsFalse(IsEncodedSafe("\u03CA"));
            Assert.IsFalse(IsEncodedSafe("\u03D5"));
            Assert.IsFalse(IsEncodedSafe("\u03E0"));
        }

        [Test]
        public void TestEncodeDecodeNullString()
        {
            Assert.IsNull(MetadataUtil.EscapeString(null));
            Assert.IsNull(MetadataUtil.UnescapeString(null));
        }

        [Test]
        public void TestEncodeDecodeWithSpaces()
        {
            var encoded = MetadataUtil.EscapeString(_spaces);
            Assert.IsTrue(IsEncodedSafe(encoded));
            Assert.AreEqual(MetadataUtil.UnescapeString(encoded), _spaces);
        }

        [Test]
        public void TestEncodeDecodeWithSymbols()
        {
            var encoded = MetadataUtil.EscapeString(_symbols);
            Assert.IsTrue(IsEncodedSafe(encoded));
            Assert.AreEqual(MetadataUtil.UnescapeString(encoded), _symbols);
        }

        [Test]
        public void TestEncodeDecodeSpaceAndPlus()
        {
            var input = " +";
            var encoded = MetadataUtil.EscapeString(input);
            Assert.IsTrue(IsEncodedSafe(encoded));
            Assert.AreEqual(MetadataUtil.UnescapeString(encoded), input);
        }

        [Test]
        public void TestEncodeDecodeRange()
        {
            var range = "Range=bytes=0-10,110-120";
            var encoded = MetadataUtil.EscapeString(range);
            Assert.IsTrue(IsEncodedSafe(encoded));
            Assert.AreEqual(encoded, range);
            Assert.AreEqual(MetadataUtil.UnescapeString(encoded), range);
        }

        [Test]
        public void TestDecodeLowerCase()
        {
            var encodedToLower = MetadataUtil.EscapeString(_symbols).ToLower();
            Assert.IsTrue(IsEncodedSafe(encodedToLower));
            Assert.AreEqual(MetadataUtil.UnescapeString(encodedToLower), _symbols);
        }

        [Test]
        public void TestDoubleEncodingDecoding()
        {
            var encoded1 = MetadataUtil.EscapeString(_symbols);
            var encoded2 = MetadataUtil.EscapeString(encoded1);
            Assert.IsTrue(IsEncodedSafe(encoded1));
            Assert.IsTrue(IsEncodedSafe(encoded2));

            var decoded1 = MetadataUtil.UnescapeString(encoded2);
            var decoded2 = MetadataUtil.UnescapeString(decoded1);
            Assert.AreEqual(decoded1, encoded1);
            Assert.AreEqual(decoded2, _symbols);
        }

        [Test]
        public void TestEncodeDecodeMetadata()
        {
            var metadata = new Dictionary<string, string>()
            {
                { _symbols, _symbols },
                { _spaces, _spaces }
            };

            var encoded = MetadataUtil.EscapeMetadata(metadata);
            foreach(KeyValuePair<string, string> entry in encoded)
            {
                Assert.IsTrue(IsEncodedSafe(entry.Key));
                Assert.IsTrue(IsEncodedSafe(entry.Value));
            }

            var decoded = MetadataUtil.UnescapeMetadata(encoded);
            Assert.AreEqual(decoded, metadata);
        }

        [Test]
        public void TestEscapeMetadataWithEmptyDic()
        {
            var emptyMetadata = new Dictionary<string, string>();
            CollectionAssert.AreEqual(emptyMetadata,
                MetadataUtil.EscapeMetadata(new Dictionary<string, string>()));
        }

        [Test]
        public void TestEscapeMetadataWithEnglishDic()
        {
            var metadata = new Dictionary<string, string>
            {
                {"key1", "value1"},
                {"key 2", "value 2"}
            };

            var expected = new Dictionary<string, string>
            {
                {"key1", "value1"},
                {"key%202", "value%202"}
            };

            CollectionAssert.AreEqual(expected, MetadataUtil.EscapeMetadata(metadata));
        }

        [Test]
        public void TestEscapeMetadataWithHebrewDic()
        {
            var metadata = new Dictionary<string, string>
            {
                { "מפתח 1", "ערך 1"},
                { "מפתח 2", "ערך 2"}
            };

            var expected = new Dictionary<string, string>
            {
                {"%D7%9E%D7%A4%D7%AA%D7%97%201", "%D7%A2%D7%A8%D7%9A%201"},
                {"%D7%9E%D7%A4%D7%AA%D7%97%202", "%D7%A2%D7%A8%D7%9A%202"}
            };

            CollectionAssert.AreEqual(expected, MetadataUtil.EscapeMetadata(metadata));
        }

        [Test]
        public void TestUnescapeMetadataWithEmptyDic()
        {
            var emptyMetadata = new Dictionary<string, string>();
            CollectionAssert.AreEqual(emptyMetadata,
                MetadataUtil.UnescapeMetadata(new Dictionary<string, string>()));
        }

        [Test]
        public void TestUnescapeMetadataWithEnglishDic()
        {
            var expected = new Dictionary<string, string>
            {
                {"key1", "value1"},
                {"key 2", "value 2"}
            };

            var metadata = new Dictionary<string, string>
            {
                {"key1", "value1"},
                {"key%202", "value%202"}
            };

            CollectionAssert.AreEqual(expected, MetadataUtil.UnescapeMetadata(metadata));
        }

        [Test]
        public void TestUnescapeMetadataWithHebrewDic()
        {
            var expected = new Dictionary<string, string>
            {
                { "מפתח 1", "ערך 1"},
                { "מפתח 2", "ערך 2"}
            };

            var metadata = new Dictionary<string, string>
            {
                {"%D7%9E%D7%A4%D7%AA%D7%97%201", "%D7%A2%D7%A8%D7%9A%201"},
                {"%D7%9E%D7%A4%D7%AA%D7%97%202", "%D7%A2%D7%A8%D7%9A%202"}
            };

            CollectionAssert.AreEqual(expected, MetadataUtil.UnescapeMetadata(metadata));
        }
    }
}
