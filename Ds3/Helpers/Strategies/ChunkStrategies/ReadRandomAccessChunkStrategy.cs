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
using Ds3.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Ds3.Helpers.Strategies.ChunkStrategies
{
    /// <summary>
    /// The ReadRandomAccessChunkStrategy will get the available job chunks and allocate those chunks
    /// </summary>
    public class ReadRandomAccessChunkStrategy : IChunkStrategy
    {
        private readonly object _blobsRemainingLock = new object();
        private readonly Action<TimeSpan> _wait;
        private Guid _jobId;
        private ISet<Blob> _blobsRemaining;
        private readonly CountdownEvent _numberInProgress = new CountdownEvent(0);
        private readonly ManualResetEventSlim _stopEvent = new ManualResetEventSlim();
        private IDs3Client _client;
        private IEnumerable<int> _lastAvailableChunks = null;

        public readonly RetryAfter RetryAfer;
        public readonly RetryAfter SameChunksRetryAfter;

        public ReadRandomAccessChunkStrategy(int retryAfter = -1)
            : this(Thread.Sleep, retryAfter)
        {
        }

        public ReadRandomAccessChunkStrategy(Action<TimeSpan> wait, int retryAfter = -1)
        {
            RetryAfer = new RetryAfter(wait, retryAfter);
            SameChunksRetryAfter = new RetryAfter(wait, retryAfter);
            this._wait = wait;
        }

        public IEnumerable<TransferItem> GetNextTransferItems(IDs3Client client, MasterObjectList jobResponse)
        {
            this._client = client;
            this._jobId = jobResponse.JobId;
            lock (this._blobsRemainingLock)
            {
                this._blobsRemaining = new HashSet<Blob>(Blob.Convert(jobResponse));
            }

            // Flatten all batches into a single enumerable.
            return EnumerateTransferItemBatches().SelectMany(it => it);
        }

        /// <summary>
        /// This generator method yields batches of transfer items. After yielding a
        /// batch, it blocks until the consumer passes each of the batch items to
        /// CompleteBlob. It does so using the _numberInProgress countdown event.
        /// If the consumer calls Stop, the generator terminates.
        /// </summary>
        /// <returns></returns>
        private IEnumerable<IEnumerable<TransferItem>> EnumerateTransferItemBatches()
        {
            // If the wait handle resumed because of _numberInProgress, continue iterating (that's the 0 == ...).
            // Otherwise it resumed because of the stop, so we'll terminate.
            while (0 == WaitHandle.WaitAny(new[] { this._numberInProgress.WaitHandle, this._stopEvent.WaitHandle }))
            {
                // Get the current batch of transfer items.
                TransferItem[] transferItems;
                lock (this._blobsRemainingLock)
                {
                    if (this._blobsRemaining.Count == 0)
                    {
                        yield break;
                    }
                    transferItems = GetNextTransfers();
                }

                // We're about to return more items, so reset the counter.
                if (transferItems.Length > 0)
                {
                    this._numberInProgress.Reset(transferItems.Length);
                }

                // Return the current batch.
                yield return transferItems;
            }
        }

        private TransferItem[] GetNextTransfers()
        {
            return this._client
            .GetJobChunksReadyForClientProcessingSpectraS3(new GetJobChunksReadyForClientProcessingSpectraS3Request(this._jobId))
            .Match((ts, jobResponse) =>
            {
                if (_lastAvailableChunks == null)
                {
                    _lastAvailableChunks = GetChunksNumbers(jobResponse);
                }
                else if (GotTheSameChunks(_lastAvailableChunks, GetChunksNumbers(jobResponse)))
                {
                    this.SameChunksRetryAfter.RetryAfterFunc(ts);
                    return new TransferItem[0];
                }
                else
                {
                    _lastAvailableChunks = GetChunksNumbers(jobResponse);
                }

                var clientFactory = this._client.BuildFactory(jobResponse.Nodes);
                var result = (
                    from chunk in jobResponse.Objects
                    let transferClient = clientFactory.GetClientForNodeId(chunk.NodeId)
                    from jobObject in chunk.ObjectsList
                    let blob = Blob.Convert(jobObject)
                    where this._blobsRemaining.Contains(blob)
                    select new TransferItem(transferClient, blob)
                ).ToArray();
                if (result.Length == 0)
                {
                    _wait(ts);
                }
                this.RetryAfer.Reset();
                this.SameChunksRetryAfter.Reset();
                return result;
            },
            ts =>
            {
                this.RetryAfer.RetryAfterFunc(ts);
                return new TransferItem[0];
            });
        }

        private static bool GotTheSameChunks(IEnumerable<int> lastAvailableChunks, IEnumerable<int> newAvailableChunks)
        {
            return !lastAvailableChunks.Except(newAvailableChunks).Any() &&
                   !newAvailableChunks.Except(lastAvailableChunks).Any();
        }

        private static IEnumerable<int> GetChunksNumbers(MasterObjectList jobResponse)
        {
            return jobResponse.Objects.Select(o => o.ChunkNumber);
        }

        public void CompleteBlob(Blob blob)
        {
            lock (this._blobsRemainingLock)
            {
                this._blobsRemaining.Remove(blob);
            }
            this._numberInProgress.Signal();
        }

        public void Stop()
        {
            this._stopEvent.Set();
        }
    }
}