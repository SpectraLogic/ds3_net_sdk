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

using System;
using Ds3.Calls;
using Moq;

namespace TestDs3.Helpers.Strategies.ChunkStrategies
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