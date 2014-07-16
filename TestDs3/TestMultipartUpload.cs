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
using System.Net;

using NUnit.Framework;

using Ds3.Calls;

namespace TestDs3
{
    [TestFixture]
    public class TestMultipartUpload
    {
        private static readonly IDictionary<string, string> _emptyHeaders = new Dictionary<string, string>();
        private const string _initiateResponseContent =
            "<InitiateMultipartUploadResult>"
            + "  <Bucket>bucket_name</Bucket>"
            + "  <Key>object_name</Key>"
            + "  <UploadId>VXBsb2FkIElEIGZvciA2aWWpbmcncyBteS1tb3ZpZS5tMnRzIHVwbG9hZA</UploadId>"
            + "</InitiateMultipartUploadResult>";

        [Test]
        public void TestInitiateMultipartUploadWithNoJobInfo()
        {
            var queryParams = new Dictionary<string, string> { { "uploads", "" } };
            var request = new InitiateMultipartUploadRequest("bucket_name", "object_name");
            TestInitiateMultipartUpload(queryParams, request);
        }

        [Test]
        public void TestInitiateMultipartUploadWithJobInfo()
        {
            var queryParams = new Dictionary<string, string>
            {
                { "uploads", "" },
                { "job", "5dc673bb-0b9e-4a56-83d7-8494ec25ffa9" },
                { "offset", "123456" }
            };
            var request = new InitiateMultipartUploadRequest(
                "bucket_name",
                "object_name",
                Guid.Parse("5dc673bb-0b9e-4a56-83d7-8494ec25ffa9"),
                123456
            );
            TestInitiateMultipartUpload(queryParams, request);
        }

        private static void TestInitiateMultipartUpload(Dictionary<string, string> queryParams, InitiateMultipartUploadRequest request)
        {
            var response = MockNetwork
                .Expecting(HttpVerb.POST, "/bucket_name/object_name", queryParams, "")
                .Returning(HttpStatusCode.OK, _initiateResponseContent, _emptyHeaders)
                .AsClient
                .InitiateMultipartUpload(request);
            Assert.AreEqual("bucket_name", response.BucketName);
            Assert.AreEqual("object_name", response.ObjectName);
            Assert.AreEqual("VXBsb2FkIElEIGZvciA2aWWpbmcncyBteS1tb3ZpZS5tMnRzIHVwbG9hZA", response.UploadId);
        }

        [Test]
        public void TestPutPart()
        {
            var queryParams = new Dictionary<string, string>
            {
                { "partNumber", "1234" },
                { "uploadId", "VXBsb2FkIElEIGZvciA2aWWpbmcncyBteS1tb3ZpZS5tMnRzIHVwbG9hZA" }
            };
            var responseHeaders = new Dictionary<string, string>
            {
                { "ETag", "e2b5712a78c5cd8224e90e670de9fcac" }
            };
            var putContent = "this is the put content";
            var response = MockNetwork
                .Expecting(HttpVerb.PUT, "/bucket_name/object_name", queryParams, putContent)
                .Returning(HttpStatusCode.OK, _initiateResponseContent, responseHeaders)
                .AsClient
                .PutPart(new PutPartRequest(
                    "bucket_name",
                    "object_name",
                    1234,
                    "VXBsb2FkIElEIGZvciA2aWWpbmcncyBteS1tb3ZpZS5tMnRzIHVwbG9hZA",
                    HelpersForTest.StringToStream(putContent)
                ));
            Assert.AreEqual("e2b5712a78c5cd8224e90e670de9fcac", response.Etag);
        }
    }
}
