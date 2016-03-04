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

namespace Ds3.Helpers.Strategys.ChunkStrategys
{
    /// <summary>
    /// The WriteRandomAccessChunkStrategy will allocate chunks as needed and return the allocated blobs
    /// </summary>
    public class WriteRandomAccessChunkStrategy : IChunkStrategy
    {
        private IDs3Client _client;
        private JobResponse _jobResponse;

        private readonly object _chunksRemainingLock = new object();
        private ISet<Guid> _toAllocateChunks;
        private readonly Action<TimeSpan> _wait;

        public WriteRandomAccessChunkStrategy()
            :this(Thread.Sleep)
        {
        }

        public WriteRandomAccessChunkStrategy(Action<TimeSpan> wait)
        {
            this._wait = wait;
        }

        public IEnumerable<TransferItem> GetNextTransferItems(IDs3Client client, JobResponse jobResponse)
        {
            Console.WriteLine("[{0}] GetNextTransferItems", Thread.CurrentThread.ManagedThreadId);

            this._client = client;
            this._jobResponse = jobResponse;

            lock (this._chunksRemainingLock)
            {
                Console.WriteLine("[{0}] number of chunks is {1}", Thread.CurrentThread.ManagedThreadId, jobResponse.ObjectLists.Count());
                Console.WriteLine("[{0}] number of blobs is {1}", Thread.CurrentThread.ManagedThreadId, Blob.Convert(jobResponse).Count());

                _toAllocateChunks = new HashSet<Guid>(jobResponse.ObjectLists.Select(chunk => chunk.ChunkId));
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
            Console.WriteLine("This is thread {0}", Thread.CurrentThread.ManagedThreadId);

            //Loop as long as we still have unallocated chunks
            while (true)
            {
                Console.WriteLine("[{0}] in while", Thread.CurrentThread.ManagedThreadId);
                // Get the current batch of transfer items.

                TransferItem[] transferItems;
                lock (this._chunksRemainingLock)
                {
                    if (this._toAllocateChunks.Count == 0)
                    {
                        Console.WriteLine("[{0}] yield break", Thread.CurrentThread.ManagedThreadId);
                        yield break;
                    }
                    Console.WriteLine("[{0}] GetNextTransfers", Thread.CurrentThread.ManagedThreadId);
                    transferItems = GetNextTransfers(); //get the next chunk to transfer
                }

                // Return the current batch.
                Console.WriteLine("[{0}] yield return with {1} blobs", Thread.CurrentThread.ManagedThreadId, transferItems.Length);
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
                foreach (var jobObject in allocatedChunk.Objects)
                {
                    var blob = Blob.Convert(jobObject);
                    if (!jobObject.InCache)
                    {
                        transferItem.Add(new TransferItem(transferClient, blob));
                    }
                }
                this._toAllocateChunks.Remove(chunkId); //remove the allocated chunk from the set
            }
            return transferItem.ToArray();
        }

        private JobObjectList AllocateChunk(IDs3Client client, Guid chunkId)
        {
            JobObjectList chunk = null;
            var chunkGone = false;
            while (chunk == null && !chunkGone)
            {
                Console.WriteLine("[{0}] AllocateJobChunkRequest for chunkId {1}", Thread.CurrentThread.ManagedThreadId, chunkId);
                client
                    .AllocateJobChunk(new AllocateJobChunkRequest(chunkId))
                    .Match(
                        allocatedChunk =>
                        {
                            chunk = allocatedChunk;
                        },
                        this._wait,
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