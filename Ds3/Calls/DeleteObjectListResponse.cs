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

using System.Collections.Generic;
using System.Linq;
using System.Net;

using Ds3.Models;
using Ds3.Runtime;

namespace Ds3.Calls
{
    public class DeleteObjectListResponse
    {
        public IEnumerable<Ds3Object> DeletedObjects { get; private set; }
        public IEnumerable<DeleteDs3ObjectError> DeleteErrors { get; private set; }

        public DeleteObjectListResponse(
            IEnumerable<Ds3Object> deletedObjects,
            IEnumerable<DeleteDs3ObjectError> deleteErrors)
        {
            this.DeletedObjects = deletedObjects;
            this.DeleteErrors = deleteErrors;
        }
    }
}
