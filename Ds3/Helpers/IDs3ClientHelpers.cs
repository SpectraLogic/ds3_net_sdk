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
using System.Linq;
using System.Text;

using Ds3.Models;

namespace Ds3.Helpers
{
    public interface IDs3ClientHelpers
    {
        IWriteJob StartWriteJob(string bucket, IEnumerable<Ds3Object> objectsToWrite);
        IReadJob StartReadJob(string bucket, IEnumerable<Ds3Object> objectsToRead);
        IReadJob StartReadAllJob(string bucket);
        IEnumerable<Ds3Object> ListObjects(string bucketName);
        IEnumerable<Ds3Object> ListObjects(string bucketName, string keyPrefix);
        IEnumerable<Ds3Object> ListObjects(string bucketName, string keyPrefix, int maxKeys);
        void EnsureBucketExists(string bucketName);

        //TODO: automatic job recovery needs to be implemented
        //IWriteJob RecoverWriteJob(Guid jobId);
        //IReadJob RecoverReadJob(Guid jobId);
    }
}
