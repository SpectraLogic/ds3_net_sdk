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

// This code is auto-generated, do not modify
using Ds3.Models;
using Ds3.Runtime;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace Ds3.Calls
{
    public class GetBlobsOnPoolSpectraS3Request : Ds3Request
    {
        
        public string Pool { get; private set; }

        public IEnumerable<Ds3Object> Objects { get; private set; }

        

        
        public GetBlobsOnPoolSpectraS3Request(IEnumerable<Ds3Object> objects, string pool) {
            this.Pool = pool;
            this.Objects = objects.ToList();
            this.QueryParams.Add("operation", "get_physical_placement");
            
        }

        internal override Stream GetContentStream()
        {
            return new XDocument()
                .AddFluent(
                    new XElement("Objects").AddAllFluent(
                        from obj in this.Objects
                        select new XElement("Object")
                            .SetAttributeValueFluent("Name", obj.Name)
                            .SetAttributeValueFluent("Size", ToDs3ObjectSize(obj))
                    )
                )
                .WriteToMemoryStream();
        }

        internal string ToDs3ObjectSize(Ds3Object ds3Object)
        {
            if (ds3Object.Size == null)
            {
                return null;
            }
            return ds3Object.Size.Value.ToString("D");
        }

        internal override long GetContentLength()
        {
            return GetContentStream().Length;
        }

        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.GET;
            }
        }

        internal override string Path
        {
            get
            {
                return "/_rest_/pool/" + Pool;
            }
        }
    }
}