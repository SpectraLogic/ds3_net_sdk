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

using Ds3.Helpers.Strategies.ChunkStrategies;
using Ds3.Helpers.Strategies.StreamFactory;

namespace Ds3.Helpers.Strategies
{
    /// <summary>
    /// The ReadStreamHelperStrategy bundle ReadStreamChunkStrategy with ReadStreamStreamFactory
    /// </summary>
    public class ReadStreamHelperStrategy : IHelperStrategy<string>
    {

        private readonly IChunkStrategy _readStreamChunkStrategy;
        private readonly IStreamFactory<string> _readStreamStreamFactory;

        public ReadStreamHelperStrategy(int retryAfter = -1)
        {
            this._readStreamChunkStrategy = new ReadStreamChunkStrategy(retryAfter);
            this._readStreamStreamFactory = new ReadStreamStreamFactory();
        }

        public IChunkStrategy GetChunkStrategy()
        {
            return this._readStreamChunkStrategy;
        }

        public IStreamFactory<string> GetStreamFactory()
        {
            return this._readStreamStreamFactory;
        }
    }
}
