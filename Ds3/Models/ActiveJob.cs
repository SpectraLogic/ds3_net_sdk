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

// This code is auto-generated, do not modify

using System;
using System.Collections.Generic;

namespace Ds3.Models
{
    public class ActiveJob
    {
        public bool Aggregating { get; set; }
        public Guid BucketId { get; set; }
        public long CachedSizeInBytes { get; set; }
        public JobChunkClientProcessingOrderGuarantee ChunkClientProcessingOrderGuarantee { get; set; }
        public long CompletedSizeInBytes { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ErrorMessage { get; set; }
        public Guid Id { get; set; }
        public bool ImplicitJobIdResolution { get; set; }
        public bool MinimizeSpanningAcrossMedia { get; set; }
        public bool Naked { get; set; }
        public string Name { get; set; }
        public long OriginalSizeInBytes { get; set; }
        public Priority Priority { get; set; }
        public DateTime? Rechunked { get; set; }
        public bool Replicating { get; set; }
        public JobRequestType RequestType { get; set; }
        public bool Truncated { get; set; }
        public bool TruncatedDueToTimeout { get; set; }
        public Guid UserId { get; set; }
        public bool VerifyAfterWrite { get; set; }
    }
}
