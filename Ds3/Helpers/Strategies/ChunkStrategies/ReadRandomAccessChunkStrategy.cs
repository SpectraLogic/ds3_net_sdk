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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Ds3.Calls;
using Ds3.Models;

namespace Ds3.Helpers.Strategies.ChunkStrategies
{
    /// <summary>
    /// The ReadRandomAccessChunkStrategy will get the available job chunks and allocate those chunks
    /// </summary>
    public class ReadRandomAccessChunkStrategy : IChunkStrategy
    {
        private readonly object _blobsRemainingLock = new object();
        private readonly CountdownEvent _numberInProgress = new CountdownEvent(0);
        private readonly RetryAfter _sameChunksRetryAfter;
        private readonly ManualResetEventSlim _stopEvent = new ManualResetEventSlim();
        private readonly Action<TimeSpan> _wait;

        public readonly RetryAfter RetryAfer;
        private ISet<Blob> _blobsRemaining;
        private IDs3Client _client;
        private Guid _jobId;
        private IEnumerable<int> _lastAvailableChunks = null;

        public ReadRandomAccessChunkStrategy(int retryAfter = -1)
            : this(Thread.Sleep, retryAfter)
        {
        }

        public ReadRandomAccessChunkStrategy(Action<TimeSpan> wait, int retryAfter = -1)
        {
            RetryAfer = new RetryAfter(wait, retryAfter);
            _sameChunksRetryAfter = new RetryAfter(wait, retryAfter);
            _wait = wait;
        }

        public IEnumerable<TransferItem> GetNextTransferItems(IDs3Client client, MasterObjectList jobResponse)
        {
            _client = client;
            _jobId = jobResponse.JobId;
            lock (_blobsRemainingLock)
            {
                _blobsRemaining = new HashSet<Blob>(Blob.Convert(jobResponse));
            }

            // Flatten all batches into a single enumerable.
            return EnumerateTransferItemBatches().SelectMany(it => it);
        }

        public void CompleteBlob(Blob blob)
        {
            lock (_blobsRemainingLock)
            {
                _blobsRemaining.Remove(blob);
            }
            _numberInProgress.Signal();
        }

        public void Stop()
        {
            _stopEvent.Set();
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
            while (0 == WaitHandle.WaitAny(new[] {_numberInProgress.WaitHandle, _stopEvent.WaitHandle}))
            {
                // Get the current batch of transfer items.
                TransferItem[] transferItems;
                lock (_blobsRemainingLock)
                {
                    if (_blobsRemaining.Count == 0)
                    {
                        yield break;
                    }
                    transferItems = GetNextTransfers();
                }

                // We're about to return more items, so reset the counter.
                if (transferItems.Length > 0)
                {
                    _numberInProgress.Reset(transferItems.Length);
                }

                // Return the current batch.
                yield return transferItems;
            }
        }

        private TransferItem[] GetNextTransfers()
        {
            return _client
                .GetJobChunksReadyForClientProcessingSpectraS3(
                    new GetJobChunksReadyForClientProcessingSpectraS3Request(_jobId))
                .Match((ts, jobResponse) =>
                    {
                        if (_lastAvailableChunks != null &&
                            ChunkUtils.HasTheSameChunks(_lastAvailableChunks, ChunkUtils.GetChunkNumbers(jobResponse)))
                        {
                            _sameChunksRetryAfter.RetryAfterFunc(ts);
                            return new TransferItem[0];
                        }

                        _lastAvailableChunks = ChunkUtils.GetChunkNumbers(jobResponse);

                        var clientFactory = _client.BuildFactory(jobResponse.Nodes);
                        var result = (
                            from chunk in jobResponse.Objects
                            let transferClient = clientFactory.GetClientForNodeId(chunk.NodeId)
                            from jobObject in chunk.ObjectsList
                            let blob = Blob.Convert(jobObject)
                            where _blobsRemaining.Contains(blob)
                            select new TransferItem(transferClient, blob)
                        ).ToArray();
                        if (result.Length == 0)
                        {
                            _wait(ts);
                        }
                        RetryAfer.Reset();
                        _sameChunksRetryAfter.Reset();
                        return result;
                    },
                    ts =>
                    {
                        RetryAfer.RetryAfterFunc(ts);
                        return new TransferItem[0];
                    });
        }
    }
}