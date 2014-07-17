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

using Ds3.Calls;
using Ds3.Models;

namespace Ds3.Helpers
{
    internal class WriteJob : Job
    {
        public WriteJob(IDs3Client client, JobResponse jobResponse)
            : base(client, jobResponse)
        {
        }

        protected override void TransferChunk(IDs3Client clientForNode, Dictionary<string, Stream> objectStreams, IEnumerable<JobObject> jobObjects)
        {
            var filteredJobObjects = jobObjects.Where(obj => !obj.InCache).AsParallel();
            (
                from jobObject in (
                    _maxParallelRequests > 0
                        ? filteredJobObjects.WithDegreeOfParallelism(_maxParallelRequests)
                        : filteredJobObjects
                )
                let objectUploadState = PrepareForUpload(clientForNode, objectStreams[jobObject.Name], jobObject)
                from uploadFunc in objectUploadState.PartActions
                let uploadResult = uploadFunc()
                where objectUploadState.UploadId != null
                group uploadResult
                    by new { jobObject.Name, objectUploadState.UploadId }
                    into completionGroup
                    select new CompleteMultipartUploadRequest(
                        this._bulkResponse.BucketName,
                        completionGroup.Key.Name,
                        completionGroup.Key.UploadId,
                        completionGroup.OrderBy(part => part.PartNumber)
                    )
            ).ForAll(request => clientForNode.CompleteMultipartUpload(request));
        }

        private UploadState PrepareForUpload(IDs3Client client, Stream stream, JobObject jobObject)
        {
            var parts = ObjectSplitter.SplitObject(_partSize, jobObject.Offset, jobObject.Length).ToList();
            var criticalSectionExecutor = new CriticalSectionExecutor();
            if (parts.Count > 1)
            {
                var uploadId = client
                    .InitiateMultipartUpload(new InitiateMultipartUploadRequest(
                        this._bulkResponse.BucketName,
                        jobObject.Name,
                        this._bulkResponse.JobId,
                        jobObject.Offset
                    ))
                    .UploadId;
                return new UploadState(
                    uploadId,
                    parts.Select(part => new Func<UploadPart>(delegate
                    {
                        var etag = client
                            .PutPart(new PutPartRequest(
                                this._bulkResponse.BucketName,
                                jobObject.Name,
                                part.PartNumber,
                                uploadId,
                                new WindowedStream(stream, criticalSectionExecutor, part.Offset, part.Length)
                            ))
                            .Etag;
                        return new UploadPart(part.PartNumber, etag);
                    }))
                );
            }
            else
            {
                var objectPutter = new Func<UploadPart>(delegate
                {
                    client.PutObject(new PutObjectRequest(
                        this._bulkResponse.BucketName,
                        jobObject.Name,
                        this._bulkResponse.JobId,
                        jobObject.Offset,
                        new WindowedStream(stream, criticalSectionExecutor, jobObject.Offset, jobObject.Length)
                    ));
                    return null;
                });
                return new UploadState(null, new[] { objectPutter });
            }
        }

        private class UploadState
        {
            public string UploadId { get; private set; }
            public IEnumerable<Func<UploadPart>> PartActions { get; private set; }

            public UploadState(string uploadId, IEnumerable<Func<UploadPart>> partActions)
            {
                this.UploadId = uploadId;
                this.PartActions = partActions;
            }
        }
    }
}
