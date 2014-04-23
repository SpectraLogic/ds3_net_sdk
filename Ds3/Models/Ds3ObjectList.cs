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
using System.Collections;
using System.Collections.Generic;

namespace Ds3.Models
{
    public class Ds3ObjectList : IEnumerable<Ds3Object>
    {
        public IEnumerable<Ds3Object> Objects { get; private set; }
        public string ServerId { get; private set; }

        internal Ds3ObjectList(IEnumerable<Ds3Object> objects, string serverId)
        {
            Objects = objects.ToList();
            ServerId = serverId;
        }

        public IEnumerator<Ds3Object> GetEnumerator()
        {
            return Objects.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Objects.GetEnumerator();
        }
    }
}
