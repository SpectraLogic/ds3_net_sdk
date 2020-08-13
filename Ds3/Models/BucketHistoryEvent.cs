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

using System;
using System.Collections.Generic;

namespace Ds3.Models
{
    public class BucketHistoryEvent
    {
        public Guid BucketId { get; set; }
        public Guid Id { get; set; }
        public DateTime? ObjectCreationDate { get; set; }
        public string ObjectName { get; set; }
        public long? SequenceNumber { get; set; }
        public BucketHistoryEventType Type { get; set; }
        public Guid VersionId { get; set; }
    }
}
