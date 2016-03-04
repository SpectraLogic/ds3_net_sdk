using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ds3.Calls;
using Moq;

namespace TestDs3.Helpers.Strategys.ChunkStrategys
{
    public static class AllocateMock
    {
        public static AllocateJobChunkRequest Allocate(Guid chunkId)
        {
            return Match.Create(
                r => r.ChunkId == chunkId,
                () => new AllocateJobChunkRequest(chunkId)
            );
        }

        public static GetAvailableJobChunksRequest AvailableChunks(Guid jobId)
        {
            return Match.Create(
                r => r.JobId == jobId,
                () => new GetAvailableJobChunksRequest(jobId)
            );
        }
    }
}
