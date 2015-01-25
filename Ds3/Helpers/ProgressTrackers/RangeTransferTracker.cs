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

using Ds3.Lang;
using Ds3.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ds3.Helpers.ProgressTrackers
{
    internal class RangeTransferTracker : IRangeTransferTracker
    {
        private readonly ISet<Range> _ranges;

        public event Action<long> DataTransferred;
        public event Action Completed;

        public RangeTransferTracker(IEnumerable<Range> ranges)
        {
            ValidateRanges(ranges);
            this._ranges = new SortedSet<Range>(ranges);
        }

        public void CompleteRange(Range rangeToRemove)
        {
            var existingRange = this._ranges.LastOrDefault(range => range.Start <= rangeToRemove.Start);
            if (existingRange.Equals(default(Range)))
            {
                throw new InvalidOperationException(Resources.RangeNotTrackedException);
            }
            if (rangeToRemove.End > existingRange.End)
            {
                throw new InvalidOperationException(Resources.RangeNotTrackedException);
            }
            this._ranges.Remove(existingRange);
            if (rangeToRemove.Start > existingRange.Start)
            {
                this._ranges.Add(Range.ByLength(existingRange.Start, rangeToRemove.Start - existingRange.Start));
            }
            if (rangeToRemove.End < existingRange.End)
            {
                this._ranges.Add(Range.ByLength(rangeToRemove.End + 1, existingRange.End - rangeToRemove.End));
            }
            this.DataTransferred.Call(rangeToRemove.Length);
            if (this._ranges.Count == 0)
            {
                this.Completed.Call();
            }
        }

        private static void ValidateRanges(IEnumerable<Range> ranges)
        {
            var lastEnd = -1L;
            foreach (var range in ranges.OrderBy(range => range.Start))
            {
                if (range.Start <= lastEnd)
                {
                    throw new ArgumentOutOfRangeException();
                }
                lastEnd = range.End;
            }
        }
    }
}
