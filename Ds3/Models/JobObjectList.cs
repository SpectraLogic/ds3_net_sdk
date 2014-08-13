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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ds3.Models
{
    public class JobObjectList : IEnumerable<JobObject>
    {
        public Guid ChunkId { get; private set; }
        public long ChunkNumber { get; private set; }
        public Guid? NodeId { get; private set; }
        public IEnumerable<JobObject> Objects { get; private set; }

        internal JobObjectList(Guid chunkId, long chunkNumber, Guid? nodeId, IEnumerable<JobObject> objects)
        {
            this.ChunkId = chunkId;
            this.ChunkNumber = chunkNumber;
            this.NodeId = nodeId;
            this.Objects = objects.ToList();
        }

        public IEnumerator<JobObject> GetEnumerator()
        {
            return Objects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Objects.GetEnumerator();
        }
    }
}
