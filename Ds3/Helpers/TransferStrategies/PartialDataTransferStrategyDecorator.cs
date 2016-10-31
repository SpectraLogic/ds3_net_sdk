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

namespace Ds3.Helpers.TransferStrategies
{
    internal class PartialDataTransferStrategyDecorator : ITransferStrategy
    {
        private readonly int _retries;

        private readonly ITransferStrategy _transferStrategy;

        internal PartialDataTransferStrategyDecorator(ITransferStrategy transferStrategy, int retries = 5)
        {
            _transferStrategy = transferStrategy;
            _retries = retries;
        }

        public void Transfer(TransferStrategyOptions transferStrategyOptions)
        {
            var currentTry = 0;
            var transferStrategy = _transferStrategy;
            var tRanges = transferStrategyOptions.Ranges;

            while (true)
            {
                try
                {
                    transferStrategyOptions.Ranges = tRanges;
                    transferStrategy.Transfer(transferStrategyOptions);
                    return;
                }
                catch (Ds3ContentLengthNotMatch ex)
                {
                    BestEffort.ModifyForRetry(transferStrategyOptions.Stream, transferStrategyOptions.ObjectTransferAttempts, 
                        ref currentTry, transferStrategyOptions.ObjectName, transferStrategyOptions.BlobOffset, ref tRanges, ref transferStrategy, ex);
                }
                catch (Exception ex)
                {
                    if (ExceptionClassifier.IsRecoverableException(ex))
                    {
                        BestEffort.ModifyForRetry(transferStrategyOptions.Stream, _retries, ref currentTry, transferStrategyOptions.ObjectName, transferStrategyOptions.BlobOffset, ex);
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