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

using System.Collections.Generic;
using Ds3.Helpers;
using NUnit.Framework;

namespace TestDs3.Helpers
{
    [TestFixture]
    public class TestMetadataUtils
    {
        [Test]
        public void TestGetUriEscapeMetadataWithEmptyDic()
        {
            var emptyMetadata = new Dictionary<string, string>();
            CollectionAssert.AreEqual(emptyMetadata, MetadataUtils.GetUriEscapeMetadata(new Dictionary<string, string>()));
        }

        [Test]
        public void TestGetUriEscapeMetadataWithEnglishDic()
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

            CollectionAssert.AreEqual(expected, MetadataUtils.GetUriEscapeMetadata(metadata));
        }

        [Test]
        public void TestGetUriEscapeMetadataWithHebrewDic()
        {
            var metadata = new Dictionary<string, string>
            {
                {"מפתח 1", "ערך 1"},
                {"מפתח 2", "ערך 2"}
            };

            var expected = new Dictionary<string, string>
            {
                {"%D7%9E%D7%A4%D7%AA%D7%97%201", "%D7%A2%D7%A8%D7%9A%201"},
                {"%D7%9E%D7%A4%D7%AA%D7%97%202", "%D7%A2%D7%A8%D7%9A%202"}
            };

            CollectionAssert.AreEqual(expected, MetadataUtils.GetUriEscapeMetadata(metadata));
        }

        [Test]
        public void TestGetUriUnEscapeMetadataWithEmptyDic()
        {
            var emptyMetadata = new Dictionary<string, string>();
            CollectionAssert.AreEqual(emptyMetadata, MetadataUtils.GetUriUnEscapeMetadata(new Dictionary<string, string>()));
        }

        [Test]
        public void TestGetUriUnEscapeMetadataWithEnglishDic()
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

            CollectionAssert.AreEqual(expected, MetadataUtils.GetUriUnEscapeMetadata(metadata));
        }

        [Test]
        public void TestGetUriUnEscapeMetadataWithHebrewDic()
        {
            var expected = new Dictionary<string, string>
            {
                {"מפתח 1", "ערך 1"},
                {"מפתח 2", "ערך 2"}
            };

            var metadata = new Dictionary<string, string>
            {
                {"%D7%9E%D7%A4%D7%AA%D7%97%201", "%D7%A2%D7%A8%D7%9A%201"},
                {"%D7%9E%D7%A4%D7%AA%D7%97%202", "%D7%A2%D7%A8%D7%9A%202"}
            };

            CollectionAssert.AreEqual(expected, MetadataUtils.GetUriUnEscapeMetadata(metadata));
        }
    }
}
