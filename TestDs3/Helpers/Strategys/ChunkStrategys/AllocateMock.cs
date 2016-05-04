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
        public static AllocateJobChunkSpectraS3Request Allocate(Guid chunkId)
        {
            return Match.Create(
                r => r.JobChunkId == chunkId.ToString(),
                () => new AllocateJobChunkSpectraS3Request(chunkId)
            );
        }

        public static GetJobChunksReadyForClientProcessingSpectraS3Request AvailableChunks(Guid jobId)
        {
            return Match.Create(
                r => r.Job == jobId.ToString(),
                () => new GetJobChunksReadyForClientProcessingSpectraS3Request(jobId)
            );
        }
    }
}
