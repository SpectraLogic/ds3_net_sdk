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

using Ds3.Runtime;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Ds3.Calls
{
    public class GetPhysicalPlacementRequest : Ds3Request
    {
        public string BucketName { get; private set; }
        public IEnumerable<string> Objects { get; private set; }
        public bool FullDetails { get; private set; }

        public GetPhysicalPlacementRequest WithFullDetails()
        {
            this.FullDetails = true;
            this.QueryParams.Add("full_details", "");
            return this;
        }

        public GetPhysicalPlacementRequest(string bucketName, IEnumerable<string> objects)
        {
            this.BucketName = bucketName;
            this.Objects = objects;
            this.QueryParams.Add("operation", "get_physical_placement");
        }

        internal override Stream GetContentStream()
        {
            var objectNodes = this.Objects.Select(name => new XElement("Object").SetAttributeValueFluent("Name", name));
            return new XDocument()
                .AddFluent(new XElement("Objects").AddAllFluent(objectNodes))
                .WriteToMemoryStream();
        }

        internal override HttpVerb Verb
        {
            get { return HttpVerb.PUT; }
        }

        internal override string Path
        {
            get { return "/_rest_/bucket/" + this.BucketName; }
        }
    }
}
