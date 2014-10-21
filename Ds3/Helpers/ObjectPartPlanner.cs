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

namespace Ds3.Helpers
{
    internal class ObjectPartPlanner
    {
        public static IEnumerable<ObjectPart> PlanParts(long partSize, long jobOffset, long jobLength)
        {
            var offset = jobOffset;
            var length = jobLength;
            while (true)
            {
                var chunkLength = Math.Min(length, partSize);
                if (chunkLength <= 0)
                {
                    yield break;
                }
                yield return new ObjectPart(offset, chunkLength);
                offset += chunkLength;
                length -= chunkLength;
            }
        }
    }
}
