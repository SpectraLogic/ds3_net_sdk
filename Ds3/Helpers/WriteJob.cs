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

using System;
using System.Collections.Generic;
using System.IO;

using Ds3.Calls;
using Ds3.Models;

namespace Ds3.Helpers
{
    internal class WriteJob : Job, IWriteJob
    {
        private ModifyPutRequest _modifier;

        public WriteJob(IDs3ClientFactory clientFactory, Guid jobId, string bucketName, IEnumerable<Ds3ObjectList> objectLists)
            : base(clientFactory, jobId, bucketName, objectLists)
        {
        }

        public void Write(ObjectPutter putter)
        {
            this.TransferAll((client, jobId, bucket, ds3Object) =>
            {
                var request = new PutObjectRequest(bucket, ds3Object.Name, jobId, putter(ds3Object));
                if (this._modifier != null)
                {
                    this._modifier(request);
                }
                using (client.PutObject(request))
                {
                }
            });
        }

        public IWriteJob WithRequestModifier(ModifyPutRequest modifier)
        {
            this._modifier = modifier;
            return this;
        }
    }
}
