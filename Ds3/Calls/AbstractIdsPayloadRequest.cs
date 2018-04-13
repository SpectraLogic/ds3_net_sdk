﻿/*
 * ******************************************************************************
 *   Copyright 2014-2017 Spectra Logic Corporation. All Rights Reserved.
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
    /*
     * Abstract class that handles requests with payloads of ids, in format:
     * <Ids><Id>id1</Id><Id>id2</Id>...</Ids>
     */
    public abstract class AbstractIdsPayloadRequest : Ds3Request
    {
        internal List<string> ids;

        internal long contentLength;

        public AbstractIdsPayloadRequest(IEnumerable<string> ids)
        {
            this.ids = ids.ToList();
        }

        internal override Stream GetContentStream()
        {
            return new XDocument()
                .AddFluent(
                    new XElement("Ids").AddAllFluent(
                        from curId in this.ids
                        select new XElement("Id").SetValueFluent(curId)
                        )
                ).WriteToMemoryStream();
        }
        
        internal override long GetContentLength()
        {
            return GetContentStream().Length;
        }
    }
}