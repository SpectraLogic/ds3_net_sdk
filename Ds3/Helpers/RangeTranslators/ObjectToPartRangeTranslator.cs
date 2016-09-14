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

using Ds3.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ds3.Helpers.RangeTranslators
{
    internal class ObjectToPartRangeTranslator : MappingRangeTranslator<string, Ds3PartialObject>
    {
        public ObjectToPartRangeTranslator(IEnumerable<Ds3PartialObject> parts)
            : base(MapObjectRangesToPartRanges(parts))
        {
        }

        private static ILookup<string, RangeMapping<Ds3PartialObject>> MapObjectRangesToPartRanges(
            IEnumerable<Ds3PartialObject> parts)
        {
            return parts.ToLookup(
                part => part.Name,
                part => new RangeMapping<Ds3PartialObject>(part.Range, 0L, part)
            );
        }
    }
}
