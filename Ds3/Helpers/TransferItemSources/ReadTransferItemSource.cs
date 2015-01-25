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
        private readonly object _lock = new object();
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
            return EnumerateTransferItemLists().SelectMany(it => it);
        }

        private IEnumerable<IEnumerable<TransferItem>> EnumerateTransferItemLists()
        {
            while (0 == WaitHandle.WaitAny(new[] { this._numberInProgress.WaitHandle, this._stopEvent.WaitHandle }))
            {
                TransferItem[] transferItems;
                lock (this._lock)
                {
                    if (this._blobsRemaining.Count == 0)
                    {
                        yield break;
                    }
                    transferItems = GetNextTransfers();
                }
                if (transferItems.Length > 0)
                {
                    this._numberInProgress.Reset(transferItems.Length);
                }
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
            lock (this._lock)
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
