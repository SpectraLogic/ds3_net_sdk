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

using System.Net;
using System.Linq;
using System.Collections.Generic;

using Ds3.Runtime;
using Ds3.Models;

namespace Ds3.Calls
{
    public class GetJobResponse : Ds3Response
    {
        public JobInfo JobInfo { get; private set; }
        public IEnumerable<JobObjectList> ObjectLists { get; private set; }

        internal GetJobResponse(IWebResponse response)
            : base(response)
        {
        }

        protected override void ProcessResponse()
        {
            HandleStatusCode(HttpStatusCode.OK);
            using (var stream = response.GetResponseStream())
            {
                var root = XmlExtensions.ReadDocument(stream).ElementOrThrow("Job");
                this.JobInfo = ParseUtilities.ParseJobInfo(root);
                this.ObjectLists = (
                    from objects in root.Elements("Objects")
                    let groupedObjects = objects.Elements("Object").ToLookup(
                        objElement => objElement.AttributeText("State"),
                        ParseUtilities.ParseDs3Object
                    )
                    select new JobObjectList(
                        objects.AttributeTextOrNull("ServerId"),
                        groupedObjects["NOT_IN_CACHE"],
                        groupedObjects["IN_CACHE"]
                    )
                ).ToList();
            }
        }
    }
}
