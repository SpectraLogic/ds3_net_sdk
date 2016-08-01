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

using Ds3.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ds3.Calls
{
    public abstract class Ds3Request
    {
        internal abstract HttpVerb Verb
        {
            get;
        }
        
        internal abstract string Path
        {
            get;   
        }

        internal virtual long GetContentLength()
        {
            return 0;
        }

        internal virtual Stream GetContentStream()
        {
            return Stream.Null;
        }

        internal virtual ChecksumType ChecksumValue
        {
            get { return ChecksumType.None; }
        }

        internal virtual ChecksumType.Type CType
        {
            get { return ChecksumType.Type.NONE; }
        }

        internal virtual IEnumerable<Range> GetByteRanges()
        {
            return Enumerable.Empty<Range>();
        }

        private Dictionary<string, string> _queryParams = new Dictionary<string, string>();
        internal virtual Dictionary<string,string> QueryParams
        {
            get { return _queryParams; }
        }

        private Dictionary<string, string> _headers = new Dictionary<string, string>()
        {
             { "Naming-Convention", "s3" }
        };
        internal virtual Dictionary<string, string> Headers
        {
            get { return _headers; }
        }
        public string getDescription(string paramstring)
        {
            return string.Format(" | {0} {1}{2}{3}", this.Verb.ToString(), this.Path, string.IsNullOrEmpty(paramstring) ? "" : "?", string.IsNullOrEmpty(paramstring) ? "" : paramstring);
        }
    }

    internal enum HttpVerb {GET, PUT, POST, DELETE, HEAD, PATCH};
}
