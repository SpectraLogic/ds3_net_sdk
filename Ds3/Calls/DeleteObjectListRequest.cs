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

using System.IO;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

using Ds3.Models;
using Ds3.Runtime;

namespace Ds3.Calls
{
    public class DeleteObjectListRequest : Ds3Request
    {
        public string BucketName { get; private set; }
        public IEnumerable<Ds3Object> ObjectsToDelete { get; private set; }

        public DeleteObjectListRequest(string bucketName, IEnumerable<Ds3Object> objectsToDelete)
        {
            this.BucketName = bucketName;
            this.ObjectsToDelete = objectsToDelete;
            this.QueryParams.Add("delete", "");
        }

        internal override Stream GetContentStream()
        {
            return new XDocument()
                .AddFluent(
                    new XElement("Delete").AddAllFluent(
                        from objectToDelete in this.ObjectsToDelete
                        select new XElement("Object").AddFluent(new XElement("Key").SetValueFluent(objectToDelete.Name))
                    )
                )
                .WriteToMemoryStream();
        }

        internal override HttpVerb Verb
        {
            get { return HttpVerb.POST; }
        }

        internal override string Path
        {
            get { return "/" + this.BucketName; }
        }

        internal override Checksum ChecksumObject
        {
            get { return Checksum.Compute; }
        }
    }
}
