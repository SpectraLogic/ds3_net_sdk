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
    /// The WriteStreamChunkStrategy will allocate chunk at a time for each stream in the job and return the allocated blobs
    /// </summary>
    public class WriteStreamChunkStrategy : IChunkStrategy
    {
        private IDs3Client _client;
        private readonly object _lock = new object();
        private Dictionary<Guid, bool> _allocatedChunks;
        private Dictionary<string, IList<Guid>> _streamToChunkDictionary;
        private JobResponse _jobResponse;
        private readonly CountdownEvent _numberInProgress = new CountdownEvent(0);
        private readonly ManualResetEventSlim _stopEvent = new ManualResetEventSlim();
        private List<TransferItem> _currentTransferItemsList = new List<TransferItem>();
        private readonly Action<TimeSpan> _wait;

        public WriteStreamChunkStrategy()
            :this(Thread.Sleep)
        {
        }

        public WriteStreamChunkStrategy(Action<TimeSpan> wait)
        {
            this._wait = wait;
        }

        public IEnumerable<TransferItem> GetNextTransferItems(IDs3Client client, JobResponse jobResponse)
        {
            this._client = client;
            this._jobResponse = jobResponse;

            lock (this._lock)
            {
                _allocatedChunks = new Dictionary<Guid, bool>(jobResponse.ObjectLists.Select(chunk => chunk)
                    .ToDictionary(
                        chunk => chunk.ChunkId,
                        chunk => false));

                this._streamToChunkDictionary = GetStreamToChunksDictionary();
            }

            // Flatten all batches into a single enumerable.
            return EnumerateTransferItemBatches().SelectMany(it => it);
        }

        /* Return a dictionary mapping a string (stream name) to all of that stream chunks */
        private Dictionary<string, IList<Guid>> GetStreamToChunksDictionary()
        {
            var result = new Dictionary<string, IList<Guid>>();

            foreach (var chunk in _jobResponse.ObjectLists)
            {
                var chunkId = chunk.ChunkId;
                foreach (var blob in chunk.Objects)
                {
                    IList<Guid> chunks;
                    var blobName = blob.Name;

                    if (result.TryGetValue(blobName, out chunks))
                    {
                        if (!chunks.Contains(chunkId)) //in case the file has more than one blob in the same chunk
                        {
                            chunks.Insert(chunks.Count, chunkId); //Insert the chunkId to the back of the list
                        }
                    }
                    else
                    {
                        chunks = new List<Guid> { chunkId };
                        result.Add(blob.Name, chunks);
                    }
                }
            }

            return result;
        }

        private IEnumerable<IEnumerable<TransferItem>> EnumerateTransferItemBatches()
        {
            // If the wait handle resumed because of _numberInProgress, continue iterating (that's the 0 == ...).
            // Otherwise it resumed because of the stop, so we'll terminate.
            while (0 == WaitHandle.WaitAny(new[] { this._numberInProgress.WaitHandle, this._stopEvent.WaitHandle }))
            {
                TransferItem[] transferItems;
                lock (this._lock)
                {
                    if (_currentTransferItemsList.Count == 0)
                    {
                        var nextStreamsChunkToAllocate = GetNextStreamsChunkToAllocate(); //get the next chunk for each stream
                        if (nextStreamsChunkToAllocate.Count == 0)
                        {
                            yield break;
                        }

                        _currentTransferItemsList = GetNextTransfers(nextStreamsChunkToAllocate);
                    }

                    transferItems = GetNextItemsFromList(_currentTransferItemsList);
                    _numberInProgress.Reset(transferItems.Length); //reset the counter to the number of blobs we are going to transfer
                }

                // Return the current batch.
                yield return transferItems;
            }
        }

        private static TransferItem[] GetNextItemsFromList(IEnumerable<TransferItem> currentTransferItemsList)
        {
            var result = new HashSet<TransferItem>();

            foreach (var item in currentTransferItemsList)
            {
                if (!result.Select(transferItem => transferItem.Blob.Context).Contains(item.Blob.Context))
                {
                    result.Add(item);
                }
            }

            return result.ToArray();
        }

        private ISet<Guid> GetNextStreamsChunkToAllocate()
        {
            var result = new HashSet<Guid>();
            var streamsToRemove = new HashSet<string>();
            foreach (var item in this._streamToChunkDictionary)
            {
                try
                {
                    // return the first chunk that has not been allocated yet
                    var first = item.Value.First(it => !this._allocatedChunks[it]);
                    result.Add(first);
                }
                catch (InvalidOperationException)
                {
                    streamsToRemove.Add(item.Key); // mark this stream to be removed from the stream dictionary
                }
            }

            // remove all the finished stream from the dictionary to improve performance on the next GetNextStreamsChunkToAllocate
            foreach (var stream in streamsToRemove)
            {
                this._streamToChunkDictionary.Remove(stream);
            }

            return result;
        }

        private List<TransferItem> GetNextTransfers(IEnumerable<Guid> nextStreamsChunkToAllocate)
        {
            var clientFactory = this._client.BuildFactory(this._jobResponse.Nodes);

            return (
                from chunkId in nextStreamsChunkToAllocate
                let allocatedChunk = AllocateChunk(this._client, chunkId, this._allocatedChunks)
                where allocatedChunk != null
                let transferClient = clientFactory.GetClientForNodeId(allocatedChunk.NodeId)
                from jobObject in allocatedChunk.Objects
                let blob = Blob.Convert(jobObject)
                where !jobObject.InCache
                select new TransferItem(transferClient, blob)
                ).ToList();
        }

        private JobObjectList AllocateChunk(IDs3Client client, Guid chunkId, IDictionary<Guid, bool> allocatedChunks)
        {
            JobObjectList chunk = null;
            var chunkGone = false;
            while (chunk == null && !chunkGone)
            {
                client
                    .AllocateJobChunk(new AllocateJobChunkRequest(chunkId))
                    .Match(
                        allocatedChunk =>
                        {
                            allocatedChunks[chunkId] = true; //mark this chunk as allocated
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

        public void CompleteBlob(Blob blob)
        {
            lock (_lock)
            {
                var toDelete = _currentTransferItemsList.First(item => item.Blob == blob);
                _currentTransferItemsList.Remove(toDelete);
            }
            this._numberInProgress.Signal();
        }

        public void Stop()
        {
            this._stopEvent.Set();
        }
    }
}