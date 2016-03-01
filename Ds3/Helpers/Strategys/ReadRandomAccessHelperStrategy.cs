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

using Ds3.Helpers.Strategys.ChunkStrategys;
using Ds3.Helpers.Strategys.StreamFactory;
using System;
using Ds3.Models;

namespace Ds3.Helpers.Strategys
{
    public class ReadRandomAccessHelperStrategy<TItem> : IHelperStrategy<TItem>
                where TItem : IComparable<TItem>

    {
        private readonly object _lock = new object();
        private IChunkStrategy _readRandomAccessChunkStrategy = null;
        private readonly int _retryAfter;
        private IStreamFactory<TItem> _readRandomAccessStreamFactory;

        public ReadRandomAccessHelperStrategy(int retyrAfter)
        {
            this._retryAfter = retyrAfter;
        }

        public IChunkStrategy GetChunkStrategy()
        {
            lock (_lock)
            {
                return
                this._readRandomAccessChunkStrategy ?? (this._readRandomAccessChunkStrategy =
                    new ReadRandomAccessChunkStrategy(this._retryAfter));
            }
        }

        public IStreamFactory<TItem> GetStreamFactory()
        {
            lock (_lock)
            {
                return
                this._readRandomAccessStreamFactory ?? (this._readRandomAccessStreamFactory =
                    new ReadRandomAccessStreamFactory<TItem>());
            }
        }
    }
}