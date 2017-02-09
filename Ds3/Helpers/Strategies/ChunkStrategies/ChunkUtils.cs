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

using System.Collections.Generic;
using System.Linq;
using Ds3.Models;

namespace Ds3.Helpers.Strategies.ChunkStrategies
{
    internal static class ChunkUtils
    {
        public static bool HasTheSameChunks(IEnumerable<int> lastAvailableChunks, IEnumerable<int> newAvailableChunks)
        {
            return !lastAvailableChunks.Except(newAvailableChunks).Any() &&
                   !newAvailableChunks.Except(lastAvailableChunks).Any();
        }

        public static IEnumerable<int> GetChunkNumbers(MasterObjectList jobResponse)
        {
            return jobResponse.Objects.Select(o => o.ChunkNumber);
        }
    }
}
