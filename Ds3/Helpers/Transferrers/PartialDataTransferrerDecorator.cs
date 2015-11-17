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
using Ds3.Models;
using Ds3.Helpers.Jobs;
using System.IO;
using Ds3.Runtime;

namespace Ds3.Helpers.Transferrers
{
    internal class PartialDataTransferrerDecorator : ITransferrer
    {

        private readonly ITransferrer _transferrer;
        private readonly int _retries;

        internal PartialDataTransferrerDecorator(ITransferrer transferrer, int retries = 5) {
            _transferrer = transferrer;
            _retries = retries;
        }

        public void Transfer(
           IDs3Client client,
           string bucketName,
           string objectName,
           long blobOffset,
           Guid jobId,
           IEnumerable<Range> ranges,
           Stream stream) 
        {

            var currentTry = 0;
            var transferrer = _transferrer;
            var _ranges = ranges;

            while (true)
            {
                try
                {
                    transferrer.Transfer(client, bucketName, objectName, blobOffset, jobId, _ranges, stream);
                    break;
                }
                catch (Ds3ContentLengthNotMatch exception)
                {

                    if (_retries != -1 && currentTry >= _retries)
                    {
                        throw new Ds3NoMoreRetriesException(Resources.TooManyRetriesForPartialData, exception, currentTry);
                    }

                    // Issue a partial get for the remainder of the request
                    // Seek back one byte to make sure that the connection did not fail part way through a byte
                    stream.Seek(-1, SeekOrigin.Current);

                    _ranges = JobsUtil.RetryRanges(_ranges, exception.BytesRead, exception.ContentLength);
                    transferrer = new PartialReadTransferrer();

                    currentTry++;
                }
            }
        }
    }
}
