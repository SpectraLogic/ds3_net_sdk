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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Ds3.Calls;
using Ds3.Models;

namespace Ds3.Helpers
{
    internal abstract class Job<TRequest> : IJob<TRequest>
    {
        private readonly IDs3Client _client;
        private readonly JobResponse _bulkResponse;
        private int _maxParallelRequests = 0;
        private Action<TRequest> _modifier = null;

        protected Job(IDs3Client client, JobResponse bulkResponse)
        {
            this._client = client;
            this._bulkResponse = bulkResponse;
        }

        protected abstract void TransferBlob(IDs3Client client, BlobRequest requestInfo);

        protected class BlobRequest
        {
            public string BucketName { get; private set; }
            public string ObjectName { get; private set; }
            public Guid JobId { get; private set; }
            public Guid BlobId { get; private set; }
            public Stream Stream { get; private set; }

            public BlobRequest(
                string bucketName,
                string objectName,
                Guid jobId,
                Guid blobId,
                Stream stream)
            {
                this.BucketName = bucketName;
                this.ObjectName = objectName;
                this.JobId = jobId;
                this.BlobId = blobId;
                this.Stream = stream;
            }
        }

        public Guid JobId
        {
            get { return this._bulkResponse.JobId; }
        }

        public string BucketName
        {
            get { return this._bulkResponse.BucketName; }
        }

        public IJob<TRequest> WithMaxParallelRequests(int maxParallelRequests)
        {
            this._maxParallelRequests = maxParallelRequests;
            return this;
        }

        public IJob<TRequest> WithRequestModifier(Action<TRequest> modifier)
        {
            this._modifier = modifier;
            return this;
        }

        public void Transfer(Func<string, Stream> createStreamForObjectKey)
        {
            var objectStreams = new Dictionary<string, Stream>();
            UsingAll(objectStreams.Values, delegate
            {
                var objectListsList = this._bulkResponse.ObjectLists as IList<Ds3ObjectList> ?? this._bulkResponse.ObjectLists.ToList();
                var nestedNamesFromObjectLists = objectListsList.Select(objectList => objectList.Select(obj => obj.Name));
                var clientFactory = _client.BuildFactory(this._bulkResponse.Nodes);
                EnumerableAlgorithms.ForEach(
                    objectListsList,
                    nestedNamesFromObjectLists.FirstMentionsPerRow(),
                    nestedNamesFromObjectLists.LastMentionsPerRow(),
                    (objectList, namesToOpen, namesToClose) =>
                    {
                        foreach (var nameToOpen in namesToOpen)
                        {
                            objectStreams.Add(nameToOpen, createStreamForObjectKey(nameToOpen));
                        }

                        TransferChunk(clientFactory.GetClientForNodeId(objectList.NodeId), objectStreams, objectList.Objects);

                        foreach (var nameToClose in namesToClose)
                        {
                            objectStreams[nameToClose].Close();
                            objectStreams.Remove(nameToClose);
                        }
                    }
                );
            });
        }

        private void TransferChunk(IDs3Client clientForNode, Dictionary<string, Stream> objectStreams, IEnumerable<JobObject> jobObjects)
        {
            ParallelForEach(
                from obj in jobObjects
                let streamCoordinator = new CriticalSectionExecutor()
                let stream = objectStreams[obj.Name]
                from blob in obj.Blobs
                select new BlobRequest(
                    this._bulkResponse.BucketName,
                    obj.Name,
                    this._bulkResponse.JobId,
                    blob.Id,
                    new WindowedStream(stream, streamCoordinator, blob.Offset, blob.Length)
                ),
                blobRequest => TransferBlob(clientForNode, blobRequest)
            );
        }

        private void ParallelForEach<T>(IEnumerable<T> items, Action<T> action)
        {
            var options = _maxParallelRequests > 0
                ? new ParallelOptions() { MaxDegreeOfParallelism = _maxParallelRequests }
                : new ParallelOptions();
            Parallel.ForEach(items, options, action);
        }

        private static void UsingAll(IEnumerable<IDisposable> resources, Action action)
        {
            try
            {
                action();
            }
            catch (Exception e)
            {
                var exceptions = new List<Exception>();
                foreach (var resource in resources)
                {
                    try
                    {
                        resource.Dispose();
                    }
                    catch (Exception disposeException)
                    {
                        exceptions.Add(disposeException);
                    }
                }
                if (exceptions.Count > 0)
                {
                    throw new AggregateException(Resources.UsingAllException, new[] { e }.Concat(exceptions));
                }
                throw;
            }
        }
    }
}
