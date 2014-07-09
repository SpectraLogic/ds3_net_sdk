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

using System.Collections.Generic;
using System.Linq;

namespace Ds3.Models
{
    public class JobObject
    {
        public string Name { get; private set; }
        public IEnumerable<Blob> Blobs { get; private set; }

        internal JobObject(string name, IEnumerable<Blob> blobs)
        {
            this.Name = name;
            this.Blobs = blobs.ToList();
        }
    }
}
