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

using Ds3.Helpers.ProgressTrackers;
using Ds3.Helpers.RangeTranslators;
using Ds3.Helpers.Streams;
using Ds3.Helpers.TransferItemSources;
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
        private readonly ITransferItemSource _transferItemSource;
        private readonly ITransferrer _transferrer;
        private readonly ILookup<Blob, Range> _rangesForRequests;
        private readonly IRangeTranslator<Blob, TItem> _rangeTranslator;
        private readonly JobItemTracker<TItem> _itemTracker;

        private IResourceStore<TItem, Stream> _resourceStore;

        private int _maxParallelRequests = 12;
        private CancellationToken _cancellationToken = CancellationToken.None;

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
            string bucketName,
            Guid jobId,
            ITransferItemSource transferItemSource,
            ITransferrer transferrer,
            ILookup<Blob, Range> rangesForRequests,
            IRangeTranslator<Blob, TItem> rangeTranslator,
            IEnumerable<ContextRange<TItem>> itemsToTrack)
        {
            this.BucketName = bucketName;
            this.JobId = jobId;
            this._transferItemSource = transferItemSource;
            this._transferrer = transferrer;
            this._rangesForRequests = rangesForRequests;
            this._rangeTranslator = rangeTranslator;

            this._itemTracker = new JobItemTracker<TItem>(itemsToTrack);
            this._itemTracker.DataTransferred += size => this.DataTransferred.Call(size);
            this._itemTracker.ItemCompleted += item =>
            {
                this._resourceStore.Close(item);
                this.ItemCompleted.Call(item);
            };
        }

        public void Transfer(Func<TItem, Stream> createStreamForTransferItem)
        {
            using (var streamStore = new ResourceStore<TItem, Stream>(createStreamForTransferItem))
            {
                // Set this to the instance so ItemCompleted can close it.
                this._resourceStore = streamStore;
                Parallel.ForEach(
                    this._maxParallelRequests,
                    this._cancellationToken,
                    this._transferItemSource.EnumerateAvailableTransfers(),
                    item =>
                    {
                        try
                        {
                            this.TransferBlob(item.Client, item.Blob);
                        }
                        catch (Exception)
                        {
                            this._transferItemSource.Stop();
                            throw;
                        }
                        this._transferItemSource.CompleteBlob(item.Blob);
                        this._cancellationToken.ThrowIfCancellationRequested();
                    }
                );
            }
        }

        private void TransferBlob(IDs3Client client, Blob blob)
        {
            var ranges = this._rangesForRequests[blob];
            var getLength = ranges.Sum(r => r.Length);
            this._transferrer.Transfer(
                client,
                this.BucketName,
                blob.Context,
                blob.Range.Start,
                this.JobId,
                ranges,
                new StreamTranslator<TItem, Blob>(
                    this._rangeTranslator,
                    this._resourceStore,
                    blob,
                    getLength
                )
            );
            var fullRequestRange = ContextRange.Create(Range.ByLength(0L, getLength), blob);
            foreach (var contextRange in this._rangeTranslator.Translate(fullRequestRange))
            {
                this._itemTracker.CompleteRange(contextRange);
            }
        }
    }
}
