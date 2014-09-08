using Ds3.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ds3.Helpers
{
    internal static class JobPartTrackerFactory
    {
        public static JobPartTracker BuildPartTracker(IEnumerable<JobObject> filteredParts)
        {
            return new JobPartTracker(
                filteredParts
                    .GroupBy(
                        part => part.Name,
                        part => new ObjectPart(part.Offset, part.Length)
                    )
                    .ToDictionary(
                        parts => parts.Key,
                        parts => new ConcurrentObjectPartTracker(new ObjectPartTracker(parts)) as IObjectPartTracker
                    )
            );
        }
    }
}
