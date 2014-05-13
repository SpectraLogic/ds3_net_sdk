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
using System.IO;
using System.Collections.Generic;
using Ds3.Models;

namespace Ds3.Calls
{
    public abstract class Ds3Request
    {
        public class Range
        {
            public long Start { get; private set; }
            public long End { get; private set; }

            public Range(long start, long end)
            {
                this.Start = start;
                this.End = end;
            }
        }

        internal abstract HttpVerb Verb
        {
            get;
        }
        
        internal abstract string Path
        {
            get;   
        }

        internal virtual Stream GetContentStream()
        {
            return Stream.Null;
        }

        internal virtual Checksum Md5
        {
            get { return Checksum.None; }
        }

        internal virtual Range GetByteRange()
        {
            return null;
        }

        private Dictionary<string, string> _queryParams = new Dictionary<string, string>();
        internal virtual Dictionary<string,string> QueryParams
        {
            get { return _queryParams; }
        }

        private Dictionary<string, string> _headers = new Dictionary<string, string>();
        internal virtual Dictionary<string, string> Headers
        {
            get { return _headers; }
        }
    }

    internal enum HttpVerb {GET, PUT, POST, DELETE, HEAD, PATCH};
}
