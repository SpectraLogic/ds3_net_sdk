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
using Ds3.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ds3.Helpers
{
    class BulkTransferExecutor
    {
        private readonly Transferrer transferrer;

        public interface Transferrer
        {
            BulkResponse Prime(String bucket, IEnumerable<Ds3Object> ds3Objects);
            void Transfer(Guid jobId, String bucket, Ds3Object ds3Object);
        }

        public BulkTransferExecutor(Transferrer transferrer)
        {
            this.transferrer = transferrer;
        }

        public int Transfer(string bucket, IEnumerable<Ds3Object> ds3Objects)
        {
            using (var response = transferrer.Prime(bucket, ds3Objects))
            {
                //TODO: use the job id instead of Guid.Empty
                return response
                    .ObjectLists
                    .AsParallel()
                    .Select(objects => objects.Objects.Foreach(obj => transferrer.Transfer(Guid.Empty, bucket, obj)))
                    .Sum();
            }
        }
    }

    static class EnumerableExtensions
    {
        public static int Foreach<T>(this IEnumerable<T> enumerable, Action<T> action)
        {
            int objectCount = 0;
            foreach (var item in enumerable)
            {
                action(item);
                objectCount++;
            }
            return objectCount;
        }
    }
}
