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
    public class BlobStoreTaskInformation
    {
        public DateTime DateScheduled { get; set; }
        public DateTime? DateStarted { get; set; }
        public string Description { get; set; }
        public Guid? DriveId { get; set; }
        public long Id { get; set; }
        public string Name { get; set; }
        public Guid? PoolId { get; set; }
        public Priority Priority { get; set; }
        public BlobStoreTaskState State { get; set; }
        public Guid? TapeId { get; set; }
        public Guid? TargetId { get; set; }
        public string TargetType { get; set; }
    }
}
