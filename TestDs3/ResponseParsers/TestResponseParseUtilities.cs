/*
 * ******************************************************************************
 *   Copyright 2014-2017 Spectra Logic Corporation. All Rights Reserved.
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
using Ds3.Models;
using Ds3.ResponseParsers;
using NUnit.Framework;

namespace TestDs3
{
    [TestFixture]
    public class TestResponseParseUtilities
    {
        
        private static readonly Dictionary<string, string> TestHeaders = new Dictionary<string, string>
        {
            {"RequestHandler-Version", "1.CF182CD57551902A475553F26582BC78"},
            {"x-spectra-ltfs-user.headeroffset", "99970"},
            {"ds3-blob-checksum-offset-0", "NOT_COMPUTED"},
            {"x-spectra-ltfs-user.guid", "060a2b340101010101010f0013-000000-709e29c2d1e20085-e7610015b2a9-a854"},
            {"ds3-blob-checksum-type", "MD5"},
            {"x-amz-request-id", "1462"},
            {"x-amz-meta-test-metadata", "testData"}
        };

        [Test]
        public void ExtractCustomMetadataTest()
        {
            var expected = new Dictionary<string, string>
            {
                {"test-metadata", "testData"},
                {"user.headeroffset", "99970"},
                {"user.guid", "060a2b340101010101010f0013-000000-709e29c2d1e20085-e7610015b2a9-a854"}
            };

            var result = ResponseParseUtilities.ExtractCustomMetadata(TestHeaders);
            
            CollectionAssert.AreEquivalent(expected, result);
        }

        [Test]
        public void NormalizeMetadataKeyNameTest()
        {
            Assert.AreEqual(ResponseParseUtilities.NormalizeMetadataKeyName("x-amz-meta-test-metadata"), "test-metadata");
            Assert.AreEqual(ResponseParseUtilities.NormalizeMetadataKeyName("x-spectra-ltfs-user.guid"), "user.guid");
            Assert.AreEqual(ResponseParseUtilities.NormalizeMetadataKeyName("ds3-blob-checksum-type"), "ds3-blob-checksum-type");
        }

        [Test]
        public void ParseJobStatusTest()
        {
            Assert.AreEqual(ResponseParseUtilities.ParseJobStatus("COMPLETED"), JobStatus.COMPLETED);
            Assert.AreEqual(ResponseParseUtilities.ParseJobStatus("CANCELLED"), JobStatus.CANCELED);
            Assert.AreEqual(ResponseParseUtilities.ParseJobStatus("IN_PROGRESS"), JobStatus.IN_PROGRESS);
            Assert.AreEqual(ResponseParseUtilities.ParseJobStatus("All other inputs"), JobStatus.IN_PROGRESS);
        }

        [Test]
        public void ParseIntHeaderTest()
        {
            Assert.IsNull(ResponseParseUtilities.ParseIntHeader("DoesNotExist", TestHeaders));
            Assert.IsNull(ResponseParseUtilities.ParseIntHeader("RequestHandler-Version", TestHeaders));
            Assert.AreEqual(ResponseParseUtilities.ParseIntHeader("x-spectra-ltfs-user.headeroffset", TestHeaders), 99970);
        }
    }
}