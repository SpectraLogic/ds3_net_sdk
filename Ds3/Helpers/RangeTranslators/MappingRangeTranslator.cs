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
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ds3.Helpers.RangeTranslators
{
    internal class MappingRangeTranslator<TFromContext, TToContext> : IRangeTranslator<TFromContext, TToContext>
        where TFromContext : IComparable<TFromContext>
        where TToContext : IComparable<TToContext>
    {
        private ILookup<TFromContext, RangeMapping<TToContext>> _lookup;

        public MappingRangeTranslator(ILookup<TFromContext, RangeMapping<TToContext>> lookup)
        {
            this._lookup = lookup;
        }

        public IEnumerable<ContextRange<TToContext>> Translate(ContextRange<TFromContext> contextRange)
        {
            foreach (var mapping in this._lookup[contextRange.Context])
            {
                if (contextRange.Range.Overlaps(mapping.From))
                {
                    var rangeToTranslate = contextRange.Range.Intersect(mapping.From);
                    var translatedRange = Range.ByLength(
                        rangeToTranslate.Start + (mapping.To - mapping.From.Start),
                        rangeToTranslate.Length
                    );
                    yield return ContextRange.Create(translatedRange, mapping.Context);
                }
            }
        }
    }
}
