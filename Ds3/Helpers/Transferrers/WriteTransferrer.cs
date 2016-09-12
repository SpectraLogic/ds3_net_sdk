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
using Ds3.Calls;
using Ds3.Models;
using Ds3.Runtime;

namespace Ds3.Helpers.Transferrers
{
    internal class WriteTransferrer : ITransferrer
    {
        public void Transfer(IDs3Client client, string bucketName, string objectName, long blobOffset, Guid jobId,
            IEnumerable<Range> ranges, Stream stream, IMetadataAccess metadataAccess,
            Action<string, IDictionary<string, string>> metadataListener, int objectTransferAttempts)
        {
            var currentTry = 0;

            while (true)
            {
                var request = new PutObjectRequest(bucketName, objectName, stream)
                    .WithJob(jobId)
                    .WithOffset(blobOffset);

                if (blobOffset == 0 && metadataAccess != null)
                {
                    request.WithMetadata(metadataAccess.GetMetadataValue(objectName));
                }

                try
                {
                    client.PutObject(request);
                    return;
                }
                catch (Exception ex)
                {
                    if (ExceptionClassifier.IsRecoverableException(ex))
                    {
                        BestEffort.ModifyForRetry(stream, objectTransferAttempts, ref currentTry, request.ObjectName, request.Offset.Value, ex);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }


    }
}