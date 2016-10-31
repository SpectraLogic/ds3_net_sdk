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

using System;
using System.Collections.Generic;
using System.IO;
using Ds3.Models;

namespace Ds3.Helpers.TransferStrategies
{
    internal interface ITransferStrategy
    {
        void Transfer(TransferStrategyOptions transferStrategyOptions);
    }

    internal class TransferStrategyOptions
    {
        public IDs3Client Client { get; set; }
        public string BucketName { get; set; }
        public string ObjectName { get; set; }
        public long BlobOffset { get; set; }
        public Guid JobId { get; set; }
        public IEnumerable<Range> Ranges { get; set; }
        public Stream Stream { get; set; }
        public IMetadataAccess MetadataAccess { get; set; }
        public Action<string, IDictionary<string, string>> MetadataListener { get; set; }
        public int ObjectTransferAttempts { get; set; }
        public ChecksumType Checksum { get; set; }
        public ChecksumType.Type ChecksumType { get; set; }
    }
}