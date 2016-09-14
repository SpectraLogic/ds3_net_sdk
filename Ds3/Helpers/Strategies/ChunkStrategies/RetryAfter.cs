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

namespace Ds3.Helpers.Strategies.ChunkStrategies
{
    public class RetryAfter
    {
        private readonly int _retryAfter; // Negative _retryAfter value represent infinity retries
        private readonly Action<TimeSpan> _wait;
        public int RetryAfterLeft { get; private set; } // The number of retries left

        public RetryAfter(Action<TimeSpan> wait, int retryAfter)
        {
            this._wait = wait;
            this._retryAfter = RetryAfterLeft = retryAfter;
        }

        public void Reset()
        {
            RetryAfterLeft = _retryAfter;
        }

        public void RetryAfterFunc(TimeSpan ts)
        {
            if (RetryAfterLeft == 0)
            {
                throw new Ds3NoMoreRetriesException(Resources.NoMoreRetriesException);
            }
            RetryAfterLeft--;

            _wait(ts);
        }
    }
}
