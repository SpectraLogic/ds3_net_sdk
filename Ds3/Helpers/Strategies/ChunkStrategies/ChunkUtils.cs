using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ds3.Models;

namespace Ds3.Helpers.Strategies.ChunkStrategies
{
    internal static class ChunkUtils
    {
        public static bool GotTheSameChunks(IEnumerable<int> lastAvailableChunks, IEnumerable<int> newAvailableChunks)
        {
            return !lastAvailableChunks.Except(newAvailableChunks).Any() &&
                   !newAvailableChunks.Except(lastAvailableChunks).Any();
        }

        public static IEnumerable<int> GetChunksNumbers(MasterObjectList jobResponse)
        {
            return jobResponse.Objects.Select(o => o.ChunkNumber);
        }
    }
}
