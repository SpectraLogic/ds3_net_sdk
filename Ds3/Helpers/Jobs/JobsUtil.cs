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
