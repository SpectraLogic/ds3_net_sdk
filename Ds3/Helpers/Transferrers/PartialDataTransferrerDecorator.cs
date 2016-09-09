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
using Ds3.Models;
using Ds3.Runtime;

namespace Ds3.Helpers.Transferrers
{
    internal class PartialDataTransferrerDecorator : ITransferrer
    {
        private readonly int _retries;

        private readonly ITransferrer _transferrer;

        internal PartialDataTransferrerDecorator(ITransferrer transferrer, int retries = 5)
        {
            _transferrer = transferrer;
            _retries = retries;
        }

        public void Transfer(IDs3Client client, string bucketName, string objectName, long blobOffset, Guid jobId,
            IEnumerable<Range> ranges, Stream stream, IMetadataAccess metadataAccess,
            Action<string, IDictionary<string, string>> metadataListener, int objectTransferAttemps)
        {
            var currentTry = 0;
            var transferrer = _transferrer;
            var tRanges = ranges;

            while (true)
            {
                try
                {
                    transferrer.Transfer(client, bucketName, objectName, blobOffset, jobId, tRanges, stream,
                        metadataAccess, metadataListener, objectTransferAttemps);
                    return;
                }
                catch (Ds3ContentLengthNotMatch ex)
                {
                    BestEffort.ModifyForRetry(stream, objectTransferAttemps, ref currentTry, objectName, blobOffset, ref tRanges, ref transferrer, ex);
                }
                catch (Exception ex)
                {
                    if (ExceptionClassifier.IsRecoverableException(ex))
                    {
                        BestEffort.ModifyForRetry(stream, _retries, ref currentTry, objectName, blobOffset, ex);
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