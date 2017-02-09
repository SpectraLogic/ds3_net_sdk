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

using System;
using System.Collections.Generic;
using Ds3.Models;
using System.Linq;

namespace Ds3.Helpers.Jobs
{
    public class JobsUtil
    {
        public static IEnumerable<Range> RetryRanges(IEnumerable<Range> oldRanges, long bytesRead, long contentLength)
        {
            List<Range> newRanges = new List<Range>();

            if (oldRanges == null || oldRanges.Count() == 0)
            {
                newRanges.Add(Range.ByPosition(bytesRead - 1, contentLength - 1));
            }
            else
            {
                bool foundRangeIntercept = false;
                long rangeCount = 0;
                foreach (var range in oldRanges)
                {
                    if (!foundRangeIntercept)
                    {
                        if (rangeCount + range.Length < bytesRead)
                        {
                            rangeCount += range.Length;
                        }
                        else
                        {
                            foundRangeIntercept = true;

                            long bytesIntoRange = bytesRead - rangeCount;

                            var newRange = Range.ByLength(range.Start + bytesIntoRange - 1, range.Length - bytesIntoRange + 1);

                            newRanges.Add(newRange);
                        }
                    }
                    else
                    {
                        newRanges.Add(range);
                    }
                }
            }

            return newRanges;
        }
    }
}
