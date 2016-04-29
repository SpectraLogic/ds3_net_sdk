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
using Ds3.Helpers.ProgressTrackers;
using Ds3.Helpers.RangeTranslators;
using Ds3.Helpers.Strategys;
using Ds3.Helpers.Strategys.ChunkStrategys;
using Ds3.Helpers.Strategys.StreamFactory;
using Ds3.Helpers.Transferrers;
using Ds3.Lang;
using Ds3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;

namespace Ds3.Helpers.Jobs
{
    internal abstract class Job<TSelf, TItem> : IBaseJob<TSelf, TItem>
        where TSelf : IBaseJob<TSelf, TItem>
        where TItem : IComparable<TItem>
    {
        private readonly ITransferrer _transferrer;
        private readonly ILookup<Blob, Range> _rangesForRequests;
        private readonly IRangeTranslator<Blob, TItem> _rangeTranslator;
        private readonly JobItemTracker<TItem> _itemTracker;
        private int _maxParallelRequests = 12;
        private CancellationToken _cancellationToken = CancellationToken.None;
        private readonly IChunkStrategy _chunkStrategy;
        private readonly IStreamFactory<TItem> _streamFactory;
        private readonly IDs3Client _client;
        private readonly MasterObjectList _jobResponse;
        private Func<TItem, Stream> _createStreamForTransferItem;

        public event Action<long> DataTransferred;

        public event Action<TItem> ItemCompleted;

        public Guid JobId { get; private set; }
        public string BucketName { get; private set; }

        public TSelf WithMaxParallelRequests(int maxParallelRequests)
        {
            this._maxParallelRequests = maxParallelRequests;
            return (TSelf)(IBaseJob<TSelf, TItem>)this;
        }

        public TSelf WithCancellationToken(CancellationToken cancellationToken)
        {
            this._cancellationToken = cancellationToken;
            return (TSelf)(IBaseJob<TSelf, TItem>)this;
        }

        protected Job(
            IDs3Client client,
            MasterObjectList jobResponse,
            string bucketName,
            Guid jobId,
            IHelperStrategy<TItem> helperStrategy,
            ITransferrer transferrer,
            ILookup<Blob, Range> rangesForRequests,
            IRangeTranslator<Blob, TItem> rangeTranslator,
            IEnumerable<ContextRange<TItem>> itemsToTrack)
        {
            this._client = client;
            this._jobResponse = jobResponse;
            this.BucketName = bucketName;
            this.JobId = jobId;
            this._chunkStrategy = helperStrategy.GetChunkStrategy();
            this._streamFactory = helperStrategy.GetStreamFactory();
            this._transferrer = transferrer;
            this._rangesForRequests = rangesForRequests;
            this._rangeTranslator = rangeTranslator;

            this._itemTracker = new JobItemTracker<TItem>(itemsToTrack);
            this._itemTracker.DataTransferred += size => this.DataTransferred.Call(size);
            this._itemTracker.ItemCompleted += item =>
            {
                this._streamFactory.CloseStream(item);
                this.ItemCompleted.Call(item);
            };
        }

        public void Transfer(Func<TItem, Stream> createStreamForTransferItem)
        {
            this._createStreamForTransferItem = createStreamForTransferItem;

            Parallel.ForEach(
                this._maxParallelRequests,
                this._cancellationToken,
                this._chunkStrategy.GetNextTransferItems(this._client, this._jobResponse),
                item =>
                {
                    try
                    {
                        this.TransferBlob(item.Client, item.Blob);
                    }
                    catch (Exception)
                    {
                        this._chunkStrategy.Stop();
                        throw;
                    }
                    this._chunkStrategy.CompleteBlob(item.Blob);
                    this._cancellationToken.ThrowIfCancellationRequested();
                }
            );
        }

        private void TransferBlob(IDs3Client client, Blob blob)
        {
            var ranges = this._rangesForRequests[blob];
            var blobLength = ranges.Sum(r => r.Length);

            var stream = this._streamFactory.CreateStream(_createStreamForTransferItem, this._rangeTranslator, blob, blobLength);

            this._transferrer.Transfer(
                client,
                this.BucketName,
                blob.Context,
                blob.Range.Start,
                this.JobId,
                ranges,
                stream
            );

            this._streamFactory.CloseBlob(blob);

            var fullRequestRange = ContextRange.Create(Range.ByLength(0L, blobLength), blob);
            foreach (var contextRange in this._rangeTranslator.Translate(fullRequestRange))
            {
                this._itemTracker.CompleteRange(contextRange);
            }
        }
    }
}