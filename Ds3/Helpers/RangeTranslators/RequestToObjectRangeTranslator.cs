/*
 * ******************************************************************************
 *   Copyright 2014-2017 Spectra Logic Corporation. All Rights Reserved.
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

using Ds3.Lang;
using Ds3.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ds3.Helpers.RangeTranslators
{
    internal class RequestToObjectRangeTranslator : MappingRangeTranslator<Blob, string>
    {
        private readonly ILookup<Blob, Range> _rangesForRequests;

        public RequestToObjectRangeTranslator(ILookup<Blob, Range> rangesForRequests)
            : base(rangesForRequests.Select(CreateRangeMappings).ToLookup())
        {
            this._rangesForRequests = rangesForRequests;
        }

        private static IGrouping<Blob, RangeMapping<string>> CreateRangeMappings(
            IGrouping<Blob, Range> grp)
        {
            return MapRangesForContext(grp.Key.Context, grp).ToGrouping(grp.Key);
        }

        private static IEnumerable<RangeMapping<string>> MapRangesForContext(
            string context,
            IEnumerable<Range> ranges)
        {
            var positionSoFar = 0L;
            foreach (var range in ranges)
            {
                yield return new RangeMapping<string>(
                    Range.ByLength(positionSoFar, range.Length),
                    range.Start,
                    context
                );
                positionSoFar += range.Length;
            }
        }
    }
}
