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

using System.Linq;
using System.Collections.Generic;
using System;

namespace Ds3.Models
{
    public class JobObjectList : Ds3ObjectList
    {
        public IEnumerable<JobObject> ObjectsInCache { get; private set; }

        public JobObjectList(long chunkId, Guid? nodeId, IEnumerable<JobObject> objects, IEnumerable<JobObject> objectsInCache)
            : base(chunkId, nodeId, objects)
        {
            this.ObjectsInCache = objectsInCache.ToList();
        }
    }
}
