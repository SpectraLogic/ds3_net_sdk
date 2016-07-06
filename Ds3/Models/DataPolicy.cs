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
    public class DataPolicy
    {
        public bool AlwaysForcePutJobCreation { get; set; }
        public bool AlwaysMinimizeSpanningAcrossMedia { get; set; }
        public bool AlwaysReplicateDeletes { get; set; }
        public bool BlobbingEnabled { get; set; }
        public ChecksumType.Type ChecksumType { get; set; }
        public DateTime CreationDate { get; set; }
        public long? DefaultBlobSize { get; set; }
        public Priority DefaultGetJobPriority { get; set; }
        public Priority DefaultPutJobPriority { get; set; }
        public Priority DefaultVerifyJobPriority { get; set; }
        public bool EndToEndCrcRequired { get; set; }
        public Guid Id { get; set; }
        public bool LtfsObjectNamingAllowed { get; set; }
        public string Name { get; set; }
        public Priority RebuildPriority { get; set; }
        public VersioningLevel Versioning { get; set; }
    }
}
