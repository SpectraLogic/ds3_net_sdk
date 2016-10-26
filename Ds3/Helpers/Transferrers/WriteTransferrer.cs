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

using System;
using Ds3.Calls;

namespace Ds3.Helpers.Transferrers
{
    internal class WriteTransferrer : ITransferrer
    {
        public void Transfer(TransferrerOptions transferrerOptions)
        {
            var currentTry = 0;

            while (true)
            {
                var request = new PutObjectRequest(transferrerOptions.BucketName, transferrerOptions.ObjectName, transferrerOptions.Stream)
                    .WithJob(transferrerOptions.JobId)
                    .WithOffset(transferrerOptions.BlobOffset);

                if (transferrerOptions.BlobOffset == 0 && transferrerOptions.MetadataAccess != null)
                {
                    request.WithMetadata(MetadataUtils.GetUriEscapeMetadata(transferrerOptions.MetadataAccess.GetMetadataValue(transferrerOptions.ObjectName)));
                }

                if (transferrerOptions.Checksum != null)
                {
                    request.WithChecksum(transferrerOptions.Checksum, transferrerOptions.ChecksumType);
                }

                try
                {
                    transferrerOptions.Client.PutObject(request);
                    return;
                }
                catch (Exception ex)
                {
                    if (ExceptionClassifier.IsRecoverableException(ex))
                    {
                        BestEffort.ModifyForRetry(transferrerOptions.Stream, transferrerOptions.ObjectTransferAttempts, ref currentTry, request.ObjectName, request.Offset.Value, ex);
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