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
using System.IO;
using System.Linq;
using System.Threading;

namespace Ds3.Helpers
{
    internal class ReadJob : Job
    {
        public ReadJob(IDs3Client client, JobResponse bulkGetResponse)
            : base(client, bulkGetResponse)
        {
        }

        public override void Transfer(Func<string, Stream> createStreamForObjectKey)
        {
            var objectsRemaining = this._bulkResponse.ObjectLists.Sum(jobObjectList => jobObjectList.Objects.Count());
            using (var streamCache = new DisposableCache<string, StreamWindowFactory>(key =>
                new StreamWindowFactory(createStreamForObjectKey(key))))
            {
                var partTracker = JobPartTrackerFactory.BuildPartTracker(this._bulkResponse.ObjectLists.SelectMany());
                partTracker.DataTransferred += OnDataTransferred;
                partTracker.ObjectCompleted += OnObjectCompleted;
                partTracker.ObjectCompleted += streamCache.Close;
                partTracker.ObjectCompleted += objectName => Interlocked.Decrement(ref objectsRemaining);

                while (objectsRemaining > 0)
                {
                    var availableJobChunks = this._client.GetAvailableJobChunks(new GetAvailableJobChunksRequest(this._bulkResponse.JobId));
                    availableJobChunks.Match(
                        jobResponse =>
                        {
                            var clientFactory = this._client.BuildFactory(jobResponse.Nodes);
                            var transfers = (
                                from objectList in jobResponse.ObjectLists
                                let client = clientFactory.GetClientForNodeId(objectList.NodeId)
                                from jobObject in objectList
                                where partTracker.ContainsPart(jobObject.Name, new ObjectPart(jobObject.Offset, jobObject.Length))
                                select new { client, jobObject }
                            ).ToList();
                            if (!transfers.Any())
                            {
                                //TODO: this comes from elsewhere now
                                //Thread.Sleep(_defaultRetryAfter);
                            }
                            InParallel(
                                transfers,
                                transfer =>
                                {
                                    transfer.client.GetObject(new GetObjectRequest(
                                        jobResponse.BucketName,
                                        transfer.jobObject.Name,
                                        jobResponse.JobId,
                                        transfer.jobObject.Offset,
                                        streamCache
                                            .Get(transfer.jobObject.Name)
                                            .Get(transfer.jobObject.Offset, transfer.jobObject.Length)
                                    ));
                                    partTracker.CompletePart(
                                        transfer.jobObject.Name,
                                        new ObjectPart(transfer.jobObject.Offset, transfer.jobObject.Length)
                                    );
                                }
                            );
                        },
                        () => { throw new Exception(); },//TODO
                        Thread.Sleep//TODO
                    );
                }
            }
        }
    }
}
