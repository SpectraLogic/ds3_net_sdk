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

using Ds3.Calls;
using Ds3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Ds3.Helpers.TransferItemSources
{
    internal class WriteTransferItemSource : ITransferItemSource
    {
        private readonly Action<TimeSpan> _wait;
        private readonly IDs3Client _client;
        private readonly JobResponse _jobResponse;

        public WriteTransferItemSource(
            IDs3Client client,
            JobResponse jobResponse)
            : this(Thread.Sleep, client, jobResponse)
        {
        }

        public WriteTransferItemSource(
            Action<TimeSpan> wait,
            IDs3Client client,
            JobResponse jobResponse)
        {
            this._wait = wait;
            this._client = client;
            this._jobResponse = jobResponse;
        }

        public IEnumerable<TransferItem> EnumerateAvailableTransfers()
        {
            var clientFactory = this._client.BuildFactory(this._jobResponse.Nodes);
            return
                from chunk in this._jobResponse.ObjectLists
                let allocatedChunk = AllocateChunk(this._client, chunk.ChunkId)
                where allocatedChunk != null
                let transferClient = clientFactory.GetClientForNodeId(allocatedChunk.NodeId)
                from jobObject in allocatedChunk.Objects
                where !jobObject.InCache
                select new TransferItem(transferClient, Blob.Convert(jobObject));
        }

        public void CompleteBlob(Blob blob)
        {
            // We just trust the server state to figure this out.
        }

        public void Stop()
        {
            // We don't have to do anything special because it's all on-demand.
        }

        private JobObjectList AllocateChunk(IDs3Client client, Guid chunkId)
        {
            JobObjectList chunk = null;
            var chunkGone = false;
            while (chunk == null && !chunkGone)
            {
                // This is an idempotent operation, so we don't care if it's already allocated.
                client
                    .AllocateJobChunk(new AllocateJobChunkRequest(chunkId))
                    .Match(
                        allocatedChunk => { chunk = allocatedChunk; },
                        this._wait,
                        () => { chunkGone = true; }
                    );
            }
            return chunk;
        }
    }
}
