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
using Ds3;
using Ds3.Runtime;
using NUnit.Framework;

namespace TestDs3.Runtime
{
    [TestFixture]
    public class TestS3Signer
    {
        [Test]
        public void SignExtendedMetadata()
        {
            var signature = S3Signer.AuthField(
                new Credentials("aW1hZ2Vu", "Tvjx7nvS"),
                "PUT",
                "Thu, 29 Sep 2016 14:19:08 GMT",
                "/imagen/0x060a2b340101010201010f1213092a6e103251014875058057a0645106EF7E9C",
                new Dictionary<string, string> {{"job", "5853010e-bc89-4d51-b08a-f7202884ec13"}, {"offset", "0"}},
                "WYzyx46nMf4NWQWGdF0vLQ==",
                "",
                new Dictionary<string, string>
                {
                    {"x-amz-meta-accession_id", "645106EF7E9C201606090608001"},
                    {"x-amz-meta-attributes", "0"},
                    {"x-amz-meta-creation_date", "0"},
                    {"x-amz-meta-file-number", "0"},
                    {"x-amz-meta-file_type", "text/plain"},
                    {"x-amz-meta-media_type", "12"},
                    {"x-amz-meta-modification_date", "1465445122"},
                    {
                        "x-amz-meta-original_file_name",
                        "\\\\172.18.7.30\\DPIShare\\BlackPearl_Landing\\Original_video\\645106EF7E9C\\2016\\0609\\0608\\001\\000000\\000000.idx"
                    },
                    {"x-amz-meta-profile", "-1"},
                    {"x-amz-meta-profile-name", "Original_video"},
                    {"x-amz-meta-relationship_id", "[BK-IMG-MCC]:[1]:[1463]"},
                    {"x-amz-meta-status", "0"}
                }
            );

            Assert.AreEqual("AWS aW1hZ2Vu:wVPEi7wKED5IilaTrNaDiFjkJUc=", signature);
        }
    }
}