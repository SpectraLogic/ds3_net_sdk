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

using Ds3.Calls;
using System.Collections.Generic;

namespace Ds3.Helpers.Strategys.ChunkStrategys
{
    public interface IChunkStrategy
    {
        /// <summary>
        /// Allocate chunks using a specific strategy and returning allocated blobs
        /// </summary>
        /// <param name="client"></param>
        /// <param name="jobResponse"></param>
        /// <returns></returns>
        IEnumerable<TransferItem> GetNextTransferItems(IDs3Client client, JobResponse jobResponse);

        /// <summary>
        /// Marks a blob as complete
        /// </summary>
        /// <param name="blob"></param>
        void CompleteBlob(Blob blob);

        /// <summary>
        /// Send a stop signal
        /// </summary>
        void Stop();
    }
}