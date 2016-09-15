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
using System;

namespace Ds3.Helpers.Strategies
{
    /// <summary>
    /// The ReadRandomAccessHelperStrategy bundle ReadRandomAccessChunkStrategy with ReadRandomAccessStreamFactory
    /// </summary>
    /// <typeparam name="TItem"></typeparam>
    public class ReadRandomAccessHelperStrategy<TItem> : IHelperStrategy<TItem>
                where TItem : IComparable<TItem>

    {
        private readonly IChunkStrategy _readRandomAccessChunkStrategy;
        private readonly IStreamFactory<TItem> _readRandomAccessStreamFactory;

        public ReadRandomAccessHelperStrategy(int retryAfter = -1)
        {
            this._readRandomAccessChunkStrategy =
                new ReadRandomAccessChunkStrategy(retryAfter);

            this._readRandomAccessStreamFactory =
                new ReadRandomAccessStreamFactory<TItem>();
        }

        public IChunkStrategy GetChunkStrategy()
        {
            return this._readRandomAccessChunkStrategy;
        }

        public IStreamFactory<TItem> GetStreamFactory()
        {
            return this._readRandomAccessStreamFactory;
        }
    }
}