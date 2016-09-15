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

using Ds3.Models;
using System.Collections.Generic;
using System.Linq;

namespace Ds3.Helpers.Strategies.ChunkStrategies
{
    /// <summary>
    /// The WriteNoAllocateChunkStrategy will return all the blobs that were returned in the JobResponse
    /// without allocate cache space for them in BlackPearl
    /// </summary>
    public class WriteNoAllocateChunkStrategy : IChunkStrategy
    {
        public IEnumerable<TransferItem> GetNextTransferItems(IDs3Client client, MasterObjectList jobResponse)
        {
            var clientFactory = client.BuildFactory(jobResponse.Nodes);
            return
                from chunk in jobResponse.Objects
                let transferClient = clientFactory.GetClientForNodeId(chunk.NodeId)
                from jobObject in chunk.ObjectsList
                where !(bool)jobObject.InCache
                select new TransferItem(transferClient, Blob.Convert(jobObject));
        }

        public void CompleteBlob(Blob blob)
        {
            // We just trust the server state to figure this out.
        }

        public void Stop()
        {
            // We don't have to do anything special because it's all on-demand.
        }
    }
}