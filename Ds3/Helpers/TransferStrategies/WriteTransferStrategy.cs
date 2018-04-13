/*
 * ******************************************************************************
 *   Copyright 2014-2017 Spectra Logic Corporation. All Rights Reserved.
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

namespace Ds3.Helpers.TransferStrategies
{
    internal class WriteTransferStrategy : ITransferStrategy
    {
        public void Transfer(TransferStrategyOptions transferStrategyOptions)
        {
            var currentTry = 0;

            while (true)
            {
                var request = new PutObjectRequest(transferStrategyOptions.BucketName, transferStrategyOptions.ObjectName, transferStrategyOptions.Stream)
                    .WithJob(transferStrategyOptions.JobId)
                    .WithOffset(transferStrategyOptions.BlobOffset);

                if (transferStrategyOptions.MetadataAccess != null)
                {
                    request.WithMetadata(transferStrategyOptions.MetadataAccess.GetMetadataValue(transferStrategyOptions.ObjectName));
                }

                if (transferStrategyOptions.Checksum != null)
                {
                    request.WithChecksum(transferStrategyOptions.Checksum, transferStrategyOptions.ChecksumType);
                }

                try
                {
                    transferStrategyOptions.Client.PutObject(request);
                    return;
                }
                catch (Exception ex)
                {
                    if (ex.IsRecoverableException())
                    {
                        BestEffort.ModifyForRetry(transferStrategyOptions.Stream, transferStrategyOptions.ObjectTransferAttempts, ref currentTry, request.ObjectName, request.Offset.Value, ex);
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