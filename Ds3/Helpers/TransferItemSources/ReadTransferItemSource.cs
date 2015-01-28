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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace Ds3.Helpers.TransferItemSources
{
    internal class ReadTransferItemSource : ITransferItemSource
    {
        private readonly object _blobsRemainingLock = new object();
        private readonly Action<TimeSpan> _wait;
        private readonly IDs3Client _client;
        private readonly Guid _jobId;
        private readonly ISet<Blob> _blobsRemaining;
        private readonly CountdownEvent _numberInProgress = new CountdownEvent(0);
        private readonly ManualResetEventSlim _stopEvent = new ManualResetEventSlim();

        public ReadTransferItemSource(
            IDs3Client client,
            JobResponse initialJobResponse)
            : this(Thread.Sleep, client, initialJobResponse)
        {
        }

        public ReadTransferItemSource(
            Action<TimeSpan> wait,
            IDs3Client client,
            JobResponse initialJobResponse)
        {
            this._wait = wait;
            this._client = client;
            this._jobId = initialJobResponse.JobId;
            this._blobsRemaining = new HashSet<Blob>(Blob.Convert(initialJobResponse));
        }

        public IEnumerable<TransferItem> EnumerateAvailableTransfers()
        {
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
                .GetAvailableJobChunks(new GetAvailableJobChunksRequest(this._jobId))
                .Match((ts, jobResponse) =>
                {
                    var clientFactory = this._client.BuildFactory(jobResponse.Nodes);
                    var result = (
                        from chunk in jobResponse.ObjectLists
                        let transferClient = clientFactory.GetClientForNodeId(chunk.NodeId)
                        from jobObject in chunk.Objects
                        let blob = Blob.Convert(jobObject)
                        where this._blobsRemaining.Contains(blob)
                        select new TransferItem(transferClient, blob)
                    ).ToArray();
                    if (result.Length == 0)
                    {
                        this._wait(ts);
                    }
                    return result;
                },
                ts =>
                {
                    this._wait(ts);
                    return new TransferItem[0];
                });
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
