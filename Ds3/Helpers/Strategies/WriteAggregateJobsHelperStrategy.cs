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

using Ds3.Helpers.Strategies.ChunkStrategies;
using Ds3.Helpers.Strategies.StreamFactory;
using Ds3.Models;
using System.Collections.Generic;

namespace Ds3.Helpers.Strategies
{
    public class WriteAggregateJobsHelperStrategy : IHelperStrategy<string>
    {
        private readonly IChunkStrategy _writeAggregateJobsChunkStrategy;
        private readonly IStreamFactory<string> _writeRandomAccessStreamFactory;

        public WriteAggregateJobsHelperStrategy(IEnumerable<Ds3Object> objects, int retryAfter = -1)
        {
            this._writeAggregateJobsChunkStrategy = new WriteAggregateJobsChunkStrategy(objects, retryAfter);
            this._writeRandomAccessStreamFactory = new WriteRandomAccessStreamFactory();
        }

        public IChunkStrategy GetChunkStrategy()
        {
            return this._writeAggregateJobsChunkStrategy;
        }

        public IStreamFactory<string> GetStreamFactory()
        {
            return this._writeRandomAccessStreamFactory;
        }
    }
}
