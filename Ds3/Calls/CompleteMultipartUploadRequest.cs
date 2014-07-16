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
using System.IO;
using System.Linq;
using System.Xml.Linq;

using Ds3.Models;
using Ds3.Runtime;

namespace Ds3.Calls
{
    public class CompleteMultipartUploadRequest : Ds3Request
    {
        public string BucketName { get; private set; }
        public string ObjectName { get; private set; }
        public string UploadId { get; private set; }
        public IEnumerable<UploadPart> Parts { get; private set; }

        public CompleteMultipartUploadRequest(string bucketName, string objectName, string uploadId, IEnumerable<UploadPart> parts)
        {
            this.BucketName = bucketName;
            this.ObjectName = objectName;
            this.UploadId = uploadId;
            this.Parts = parts.ToList();
        }

        internal override HttpVerb Verb
        {
            get { return HttpVerb.POST; }
        }

        internal override string Path
        {
            get { return "/" + BucketName + "/" + ObjectName; }
        }

        internal override Stream GetContentStream()
        {
            return new XDocument()
                .AddFluent(
                    new XElement("CompleteMultipartUpload").AddAllFluent(
                        from part in this.Parts
                        select new XElement("Part")
                            .AddFluent(new XElement("PartNumber").SetValueFluent(part.PartNumber.ToString()))
                            .AddFluent(new XElement("ETag").SetValueFluent(part.Etag.ToString()))
                    )
                )
                .WriteToMemoryStream();
        }
    }
}
