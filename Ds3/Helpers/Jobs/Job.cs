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

using Ds3.Helpers.ProgressTrackers;
using Ds3.Helpers.RangeTranslators;
using Ds3.Helpers.Strategies;
using Ds3.Helpers.Strategies.ChunkStrategies;
using Ds3.Helpers.Strategies.StreamFactory;
using Ds3.Lang;
using Ds3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Ds3.Helpers.TransferStrategies;
using Ds3.Runtime;

namespace Ds3.Helpers.Jobs
{
    internal abstract class Job<TSelf, TItem> : IBaseJob<TSelf, TItem>
        where TSelf : IBaseJob<TSelf, TItem>
        where TItem : IComparable<TItem>
    {
        private readonly ITransferStrategy _transferStrategy;
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
        private IMetadataAccess _metadataAccess;
        private readonly int _objectTransferAttempts;
        private ChecksumType _checksum;
        private ChecksumType.Type _checksumType;
        private bool TransferStarted { get; set; }

        public event Action<long> DataTransferred;
        public event Action<TItem> ItemCompleted;
        public event Action<string, IDictionary<string, string>> MetadataListener;
        public event Action<string, long, Exception> OnFailure;


        public Guid JobId { get; private set; }
        public string BucketName { get; private set; }

        public TSelf WithMaxParallelRequests(int maxParallelRequests)
        {
            if (TransferStarted) throw new Ds3AssertException("WithMaxParallelRequests Must always be called before the Transfer method.");

            this._maxParallelRequests = maxParallelRequests;
            return (TSelf)(IBaseJob<TSelf, TItem>)this;
        }

        public TSelf WithCancellationToken(CancellationToken cancellationToken)
        {
            if (TransferStarted) throw new Ds3AssertException("WithCancellationToken Must always be called before the Transfer method.");

            this._cancellationToken = cancellationToken;
            return (TSelf)(IBaseJob<TSelf, TItem>)this;
        }

        public TSelf WithMetadata(IMetadataAccess metadataAccess)
        {
            if (TransferStarted) throw new Ds3AssertException("WithMetadata Must always be called before the Transfer method.");

            this._metadataAccess = metadataAccess;
            return (TSelf)(IBaseJob<TSelf, TItem>)this;
        }

        public TSelf WithChecksum(ChecksumType checksum, ChecksumType.Type checksumType = ChecksumType.Type.MD5)
        {
            if (TransferStarted) throw new Ds3AssertException("WithChecksum Must always be called before the Transfer method.");

            this._checksum = checksum;
            this._checksumType = checksumType;

            return (TSelf)(IBaseJob<TSelf, TItem>)this;
        }

        protected Job(
            IDs3Client client,
            MasterObjectList jobResponse,
            string bucketName,
            Guid jobId,
            IHelperStrategy<TItem> helperStrategy,
            ITransferStrategy transferStrategy,
            ILookup<Blob, Range> rangesForRequests,
            IRangeTranslator<Blob, TItem> rangeTranslator,
            IEnumerable<ContextRange<TItem>> itemsToTrack,
            int objectTransferAttempts)
        {
            this._client = client;
            this._jobResponse = jobResponse;
            this.BucketName = bucketName;
            this.JobId = jobId;
            this._chunkStrategy = helperStrategy.GetChunkStrategy();
            this._streamFactory = helperStrategy.GetStreamFactory();
            this._transferStrategy = transferStrategy;
            this._rangesForRequests = rangesForRequests;
            this._rangeTranslator = rangeTranslator;
            TransferStarted = false;

            this._itemTracker = new JobItemTracker<TItem>(itemsToTrack);
            this._itemTracker.DataTransferred += size => this.DataTransferred.Call(size);
            this._itemTracker.ItemCompleted += item =>
            {
                this._streamFactory.CloseStream(item);
                this.ItemCompleted.Call(item);
            };

            this._objectTransferAttempts = objectTransferAttempts;
        }

        public void Transfer(Func<TItem, Stream> createStreamForTransferItem)
        {
            TransferStarted = true;

            this._createStreamForTransferItem = createStreamForTransferItem;

            //Will transfer as many blobs as possible and return an aggregate exception with all the failed blobs in the end
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
                    catch (Exception ex)
                    {
                        //if we have an OnFailure event then invoke with blob name, offset and the exception
                        OnFailure?.Invoke(item.Blob.Context, item.Blob.Range.Start, ex);

                        throw;
                    }
                    finally
                    {
                        this._chunkStrategy.CompleteBlob(item.Blob);
                        this._cancellationToken.ThrowIfCancellationRequested();
                    }
                }
            );
        }

        private void TransferBlob(IDs3Client client, Blob blob)
        {
            var ranges = this._rangesForRequests[blob];
            var blobLength = ranges.Sum(r => r.Length);

            var stream = this._streamFactory.CreateStream(_createStreamForTransferItem, this._rangeTranslator, blob, blobLength);

            try
            {

                this._transferStrategy.Transfer(new TransferStrategyOptions
                {
                    Client = client,
                    BucketName = BucketName,
                    ObjectName = blob.Context,
                    BlobOffset = blob.Range.Start,
                    JobId = JobId,
                    Ranges = ranges,
                    Stream = stream,
                    MetadataAccess = _metadataAccess,
                    MetadataListener = MetadataListener,
                    ObjectTransferAttempts = _objectTransferAttempts,
                    Checksum = _checksum,
                    ChecksumType = _checksumType
                });
            }
            finally
            {
                this._streamFactory.CloseBlob(blob);
            }

            var fullRequestRange = ContextRange.Create(Range.ByLength(0L, blobLength), blob);
            foreach (var contextRange in this._rangeTranslator.Translate(fullRequestRange))
            {
                this._itemTracker.CompleteRange(contextRange);
            }
        }
    }
}