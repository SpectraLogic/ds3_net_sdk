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
using System.Collections.Generic;
using Ds3.Models;

namespace Ds3.Helpers.Strategies.ChunkStrategies
{
    public class ReadStreamChunkStrategy : IChunkStrategy
    {
        public IEnumerable<TransferItem> GetNextTransferItems(IDs3Client client, MasterObjectList jobResponse)
        {
            throw new NotImplementedException();
        }

        public void CompleteBlob(Blob blob)
        {
            throw new NotImplementedException();
        }

        public void Stop()
        {
            throw new NotImplementedException();
        }
    }
}
