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

using Ds3.Calls;
using Ds3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Ds3.Helpers.Strategies.ChunkStrategies
{
    /// <summary>
    /// The WriteRandomAccessChunkStrategy will allocate chunks as needed and return the allocated blobs
    /// </summary>
    public class WriteRandomAccessChunkStrategy : IChunkStrategy
    {
        private IDs3Client _client;
        private MasterObjectList _jobResponse;

        private readonly object _chunksRemainingLock = new object();
        private ISet<Guid> _toAllocateChunks;

        public readonly RetryAfter RetryAfer;

        public WriteRandomAccessChunkStrategy(int retryAfter = -1)
            : this(Thread.Sleep, retryAfter)
        {
        }

        public WriteRandomAccessChunkStrategy(Action<TimeSpan> wait, int retryAfter = -1)
        {
            this.RetryAfer = new RetryAfter(wait, retryAfter);
        }

        public IEnumerable<TransferItem> GetNextTransferItems(IDs3Client client, MasterObjectList jobResponse)
        {
            this._client = client;
            this._jobResponse = jobResponse;

            lock (this._chunksRemainingLock)
            {
                _toAllocateChunks = new HashSet<Guid>(jobResponse.Objects.Select(chunk => chunk.ChunkId));
            }

            // Flatten all batches into a single enumerable.
            return EnumerateTransferItemBatches().SelectMany(it => it);
        }

        public void CompleteBlob(Blob blob)
        {
            // We just trust the server state to figure this out.
        }

        public void Stop()
        {
            // We don't have to do anything special because it's all on-demand.
        }

        private IEnumerable<IEnumerable<TransferItem>> EnumerateTransferItemBatches()
        {
            //Loop as long as we still have unallocated chunks
            while (true)
            {
                // Get the current batch of transfer items.
                TransferItem[] transferItems;
                lock (this._chunksRemainingLock)
                {
                    if (this._toAllocateChunks.Count == 0)
                    {
                        yield break;
                    }
                    transferItems = GetNextTransfers(); //get the next chunk to transfer
                }

                // Return the current batch.
                yield return transferItems;
            }
        }

        private TransferItem[] GetNextTransfers()
        {
            var clientFactory = this._client.BuildFactory(this._jobResponse.Nodes);
            var transferItem = new HashSet<TransferItem>();
            var chunkId = this._toAllocateChunks.First(); //take the fist chunk in the set to allocate
            var allocatedChunk = AllocateChunk(this._client, chunkId);

            if (allocatedChunk != null)
            {
                var transferClient = clientFactory.GetClientForNodeId(allocatedChunk.NodeId);
                foreach (var jobObject in allocatedChunk.ObjectsList)
                {
                    var blob = Blob.Convert(jobObject);
                    if (!(bool)jobObject.InCache)
                    {
                        transferItem.Add(new TransferItem(transferClient, blob));
                    }
                }
                this._toAllocateChunks.Remove(chunkId); //remove the allocated chunk from the set
            }
            return transferItem.ToArray();
        }

        private Objects AllocateChunk(IDs3Client client, Guid chunkId)
        {
            Objects chunk = null;
            var chunkGone = false;
            while (chunk == null && !chunkGone)
            {
                client
                    .AllocateJobChunkSpectraS3(new AllocateJobChunkSpectraS3Request(chunkId))
                    .Match(
                        allocatedChunk =>
                        {
                            chunk = allocatedChunk;
                            this.RetryAfer.Reset(); // Reset the number of retries to the initial value
                        },
                        this.RetryAfer.RetryAfterFunc,
                        () =>
                        {
                            chunkGone = true;
                        }
                    );
            }
            return chunk;
        }
    }
}