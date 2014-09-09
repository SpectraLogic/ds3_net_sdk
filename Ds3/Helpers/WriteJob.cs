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
using System;
using System.IO;
using System.Linq;
using System.Threading;

namespace Ds3.Helpers
{
    internal class WriteJob : Job
    {
        public WriteJob(IDs3Client client, JobResponse jobResponse)
            : base(client, jobResponse)
        {
        }

        public override void Transfer(Func<string, Stream> createStreamForObjectKey)
        {
            var filteredChunks = this._bulkResponse.ObjectLists
                .Select(FilterChunk)
                .Where(chunk => chunk.Any())
                .ToList();

            using (var streamCache = new DisposableCache<string, StreamWindowFactory>(key =>
                new StreamWindowFactory(createStreamForObjectKey(key))))
            {
                var partTracker = JobPartTrackerFactory.BuildPartTracker(filteredChunks.SelectMany());
                partTracker.DataTransferred += OnDataTransferred;
                partTracker.ObjectCompleted += OnObjectCompleted;
                partTracker.ObjectCompleted += streamCache.Close;

                var clientFactory = this._client.BuildFactory(this._bulkResponse.Nodes);
                foreach (var chunk in filteredChunks.Select(EnsureAllocated).Where(chunk => chunk.Any()))
                {
                    var client = clientFactory.GetClientForNodeId(chunk.NodeId);
                    InParallel(chunk, jobObject =>
                    {
                        client.PutObject(new PutObjectRequest(
                            this._bulkResponse.BucketName,
                            jobObject.Name,
                            this._bulkResponse.JobId,
                            jobObject.Offset,
                            streamCache.Get(jobObject.Name).Get(jobObject.Offset, jobObject.Length)
                        ));
                        partTracker.CompletePart(
                            jobObject.Name,
                            new ObjectPart(jobObject.Offset, jobObject.Length)
                        );
                    });
                }
            }
        }

        private JobObjectList EnsureAllocated(JobObjectList filteredChunk)
        {
            return filteredChunk.NodeId == null
                ? FilterChunk(AllocateChunk(filteredChunk.ChunkId))
                : filteredChunk;
        }

        private JobObjectList FilterChunk(JobObjectList objectList)
        {
            return new JobObjectList(
                objectList.ChunkId,
                objectList.ChunkNumber,
                objectList.NodeId,
                objectList.Where(obj => !obj.InCache).ToList()
            );
        }

        private JobObjectList AllocateChunk(Guid chunkId)
        {
            JobObjectList chunk = null;
            while (chunk == null)
            {
                this._client
                    .AllocateJobChunk(new AllocateJobChunkRequest(chunkId))
                    .Match(allocatedChunk => { chunk = allocatedChunk; }, Thread.Sleep);
            }
            return chunk;
        }
    }
}
