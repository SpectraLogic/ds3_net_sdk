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

namespace Ds3.Helpers.Strategys
{
    public class WriteStreamHelperStrategy : IHelperStrategy<string>
    {
        private readonly object _lock = new object();
        private IChunkStrategy _writeStreamChunkStrategy;
        private IStreamFactory<string> _writeStreamStreamFactory;

        public IChunkStrategy GetChunkStrategy()
        {
            lock (_lock)
            {
                return this._writeStreamChunkStrategy ??
                    (this._writeStreamChunkStrategy = new WriteStreamChunkStrategy());
            }
        }

        public IStreamFactory<string> GetStreamFactory()
        {
            lock (_lock)
            {
                return this._writeStreamStreamFactory ??
                       (this._writeStreamStreamFactory = new WriteStreamStreamFactory());
                       //(this._writeStreamStreamFactory = new WriteRandomAccessStreamFactory());
            }
        }
    }
}