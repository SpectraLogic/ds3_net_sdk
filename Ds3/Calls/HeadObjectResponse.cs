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

// This code is auto-generated, do not modify

using System.Collections.Generic;
using Ds3.Models;

using Ds3.Runtime;

namespace Ds3.Calls
{
    public class HeadObjectResponse
    {
        public IDictionary<long, string> BlobChecksums { get; private set; }
        public ChecksumType.Type BlobChecksumType { get; private set; }
        public IDictionary<string, string> Metadata { get; private set; }
        public string ETag { get; private set; }
        public long Length { get; private set; }

        public HeadObjectResponse(IDictionary<long, string> blobChecksums, ChecksumType.Type blobChecksumType, long length, string eTag, IDictionary<string, string> metadata)
        {
            this.BlobChecksums = blobChecksums;
            this.BlobChecksumType = blobChecksumType;
            this.Length = length;
            this.ETag = eTag;
            this.Metadata = metadata;
        }
    }
}