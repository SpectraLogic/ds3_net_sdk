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
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Ds3.Calls;
using Ds3.Models;

namespace Ds3.Helpers.Strategies.ChunkStrategies
{
    /// <summary>
    ///  The ReadStreamChunkStrategy will get the available job chunks, allocate those chunks and send the blobs in order
    ///  NOTE: To use this strategy the job must be created with the JobChunkClientProcessingOrderGuarantee.IN_ORDER
    /// </summary>
    public class ReadStreamChunkStrategy : IChunkStrategy
    {
        private readonly object _lock = new object();
        private readonly CountdownEvent _numberInProgress = new CountdownEvent(0);
        private readonly ManualResetEventSlim _stopEvent = new ManualResetEventSlim();
        private readonly Action<TimeSpan> _wait;

        public readonly RetryAfter RetryAfer;
        public readonly RetryAfter SameChunksRetryAfter;

        private ISet<Blob> _blobsRemaining;
        private IEnumerable<TransferItem> _blobsToSend = new List<TransferItem>();
        private IDs3Client _client;
        private MasterObjectList _jobResponse;
        private IEnumerable<int> _lastAvailableChunks = null;

        public ReadStreamChunkStrategy(int retryAfter = -1)
            : this(Thread.Sleep, retryAfter)
        {
        }

        public ReadStreamChunkStrategy(Action<TimeSpan> wait, int retryAfter = -1)
        {
            RetryAfer = new RetryAfter(wait, retryAfter);
            SameChunksRetryAfter = new RetryAfter(wait, retryAfter);
            _wait = wait;
        }

        public IEnumerable<TransferItem> GetNextTransferItems(IDs3Client client, MasterObjectList jobResponse)
        {
            _client = client;
            _jobResponse = jobResponse;

            lock (_lock)
            {
                _blobsRemaining = new HashSet<Blob>(Blob.Convert(jobResponse));
            }

            // Flatten all batches into a single enumerable.
            return EnumerateTransferItemBatches().SelectMany(it => it);
        }

        public void CompleteBlob(Blob blob)
        {
            lock (_lock)
            {
                _blobsRemaining.Remove(blob);
            }
            _numberInProgress.Signal();
        }

        public void Stop()
        {
            _stopEvent.Set();
        }

        private IEnumerable<IEnumerable<TransferItem>> EnumerateTransferItemBatches()
        {
            // If the wait handle resumed because of _numberInProgress, continue iterating (that's the 0 == ...).
            // Otherwise it resumed because of the stop, so we'll terminate.
            while (0 == WaitHandle.WaitAny(new[] {_numberInProgress.WaitHandle, _stopEvent.WaitHandle}))
            {
                // Get the current batch of transfer items.
                TransferItem[] transferItems;
                lock (_lock)
                {
                    if (!_blobsToSend.Any())
                    {
                        if (_blobsRemaining.Count == 0)
                        {
                            yield break;
                        }

                        _blobsToSend = GetNextTransfers();
                    }

                    transferItems = GetNextItemsFromList(ref _blobsToSend);
                    
                    //reset the counter to the number of blobs we are going to transfer
                    _numberInProgress.Reset(transferItems.Length);
                }

                // Return the current batch.
                yield return transferItems;
            }
        }

        private static TransferItem[] GetNextItemsFromList(ref IEnumerable<TransferItem> currentTransferItemsList)
        {
            var result = new HashSet<TransferItem>();

            foreach (var item in currentTransferItemsList)
            {
                if (!result.Select(transferItem => transferItem.Blob.Context).Contains(item.Blob.Context))
                {
                    result.Add(item);
                }
            }

            currentTransferItemsList = currentTransferItemsList.Except(result);

            return result.ToArray();
        }

        private TransferItem[] GetNextTransfers()
        {
            return _client
                .GetJobChunksReadyForClientProcessingSpectraS3(
                    new GetJobChunksReadyForClientProcessingSpectraS3Request(_jobResponse.JobId))
                .Match((ts, jobResponse) =>
                    {
                        if (_lastAvailableChunks != null &&
                            GotTheSameChunks(_lastAvailableChunks, GetChunksNumbers(jobResponse)))
                        {
                            SameChunksRetryAfter.RetryAfterFunc(ts);
                            return new TransferItem[0];
                        }

                        _lastAvailableChunks = GetChunksNumbers(jobResponse);

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
                        SameChunksRetryAfter.Reset();
                        return result;
                    },
                    ts =>
                    {
                        RetryAfer.RetryAfterFunc(ts);
                        return new TransferItem[0];
                    });
        }

        //TODO extrat to Util
        private static bool GotTheSameChunks(IEnumerable<int> lastAvailableChunks, IEnumerable<int> newAvailableChunks)
        {
            return !lastAvailableChunks.Except(newAvailableChunks).Any() &&
                   !newAvailableChunks.Except(lastAvailableChunks).Any();
        }

        //TODO extrat to Util
        private static IEnumerable<int> GetChunksNumbers(MasterObjectList jobResponse)
        {
            return jobResponse.Objects.Select(o => o.ChunkNumber);
        }
    }
}