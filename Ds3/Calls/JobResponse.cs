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

using Ds3.Models;

namespace Ds3.Calls
{
    public class JobResponse
    {
        public string BucketName { get; private set; }
        public Guid JobId { get; private set; }
        public string Priority { get; private set; }
        public string RequestType { get; private set; }
        public DateTime StartDate { get; private set; }
        public ChunkOrdering ChunkOrder { get; private set; }
        public IEnumerable<Node> Nodes { get; private set; }
        public IEnumerable<JobObjectList> ObjectLists { get; private set; }

        public JobResponse(
            string bucketName,
            Guid jobId,
            string priority,
            string requestType,
            DateTime startDate,
            ChunkOrdering chunkOrder,
            IEnumerable<Node> nodes,
            IEnumerable<JobObjectList> objectLists)
        {
            this.BucketName = bucketName;
            this.JobId = jobId;
            this.Priority = priority;
            this.RequestType = requestType;
            this.StartDate = startDate;
            this.ChunkOrder = chunkOrder;
            this.Nodes = nodes;
            this.ObjectLists = objectLists;
        }
    }
}
