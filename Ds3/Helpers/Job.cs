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
using System.Threading;

using Ds3.Calls;
using Ds3.Models;

namespace Ds3.Helpers
{
    internal abstract class Job : IJob
    {
        private readonly IDs3Client _client;
        protected readonly JobResponse _bulkResponse;
        protected int _maxParallelRequests = 0;
        protected long _partSize = 32L * 1024L * 1024L
            //TODO: Until the server supports multipart upload properly we don't want to use it.
            // For now we'll just set the threshold to something ridiculous.
            * 1024L * 1024L * 1024L;
        private static readonly TimeSpan _defaultRetryAfter = TimeSpan.FromSeconds(5 * 60);

        protected Job(IDs3Client client, JobResponse bulkResponse)
        {
            this._client = client;
            this._bulkResponse = bulkResponse;
        }

        protected abstract void TransferChunk(IDs3Client clientForNode, Dictionary<string, Stream> objectStreams, IEnumerable<JobObject> jobObjects);

        public Guid JobId
        {
            get { return this._bulkResponse.JobId; }
        }

        public string BucketName
        {
            get { return this._bulkResponse.BucketName; }
        }

        public IJob WithMaxParallelRequests(int maxParallelRequests)
        {
            this._maxParallelRequests = maxParallelRequests;
            return this;
        }

        public IJob WithPartSize(long partSize)
        {
            this._partSize = partSize;
            return this;
        }

        public void Transfer(Func<string, Stream> createStreamForObjectKey)
        {
            var objectNamesPerChunk = this._bulkResponse.ObjectLists.Select(objectList => objectList.Select(obj => obj.Name));
            var clientFactory = _client.BuildFactory(this._bulkResponse.Nodes);

            var objectStreams = new Dictionary<string, Stream>();
            UsingAll(objectStreams.Values, delegate
            {
                EnumerableAlgorithms.ForEach(
                    this._bulkResponse.ObjectLists,
                    objectNamesPerChunk.FirstMentionsPerRow(),
                    objectNamesPerChunk.LastMentionsPerRow(),
                    (objectList, namesToOpen, namesToClose) =>
                    {
                        foreach (var nameToOpen in namesToOpen)
                        {
                            objectStreams.Add(nameToOpen, createStreamForObjectKey(nameToOpen));
                        }

                        var chunk = WaitForAvailableChunk(clientFactory.GetClientForNodeId(objectList.NodeId), objectList.ChunkId);
                        TransferChunk(clientFactory.GetClientForNodeId(chunk.NodeId), objectStreams, chunk.Objects);

                        foreach (var nameToClose in namesToClose)
                        {
                            objectStreams[nameToClose].Close();
                            objectStreams.Remove(nameToClose);
                        }
                    }
                );
            });
        }

        private static JobObjectList WaitForAvailableChunk(IDs3Client client, Guid chunkId)
        {
            JobObjectList result = null;
            do
            {
                client
                    .AllocateJobChunk(new AllocateJobChunkRequest(chunkId))
                    .Match(chunk => result = chunk, () => SleepFor(_defaultRetryAfter), SleepFor);
            }
            while (result == null);
            return result;
        }

        private static void SleepFor(TimeSpan retryAfter)
        {
            Thread.Sleep(Convert.ToInt32(retryAfter.TotalMilliseconds));
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
