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

        public void Transfer(TransferrerOptions transferrerOptions)
        {
            var currentTry = 0;
            var transferrer = _transferrer;
            var tRanges = transferrerOptions.Ranges;

            while (true)
            {
                try
                {
                    transferrerOptions.Ranges = tRanges;
                    transferrer.Transfer(transferrerOptions);
                    return;
                }
                catch (Ds3ContentLengthNotMatch ex)
                {
                    BestEffort.ModifyForRetry(transferrerOptions.Stream, transferrerOptions.ObjectTransferAttempts, 
                        ref currentTry, transferrerOptions.ObjectName, transferrerOptions.BlobOffset, ref tRanges, ref transferrer, ex);
                }
                catch (Exception ex)
                {
                    if (ExceptionClassifier.IsRecoverableException(ex))
                    {
                        BestEffort.ModifyForRetry(transferrerOptions.Stream, _retries, ref currentTry, transferrerOptions.ObjectName, transferrerOptions.BlobOffset, ex);
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