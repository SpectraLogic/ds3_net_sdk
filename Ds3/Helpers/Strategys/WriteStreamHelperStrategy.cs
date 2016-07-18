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

namespace Ds3.Helpers.Strategys
{
    /// <summary>
    /// The WriteStreamHelperStrategy bundle WriteStreamChunkStrategy with WriteStreamStreamFactory
    /// </summary>
    public class WriteStreamHelperStrategy : IHelperStrategy<string>
    {
        private readonly IChunkStrategy _writeStreamChunkStrategy;
        private readonly IStreamFactory<string> _writeStreamStreamFactory;

        public WriteStreamHelperStrategy(int retryAfter = -1)
        {
            this._writeStreamChunkStrategy = new WriteStreamChunkStrategy(retryAfter);
            this._writeStreamStreamFactory = new WriteStreamStreamFactory();
        }

        public IChunkStrategy GetChunkStrategy()
        {
            return this._writeStreamChunkStrategy;
        }

        public IStreamFactory<string> GetStreamFactory()
        {
            return this._writeStreamStreamFactory;
        }
    }
}