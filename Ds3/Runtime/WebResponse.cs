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
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace Ds3.Runtime
{
    internal class WebResponse : IWebResponse
    {
        private readonly HttpWebResponse _webResponse;

        internal WebResponse(HttpWebResponse webResponse)
        {
            _webResponse = webResponse;
        }

        public Stream GetResponseStream()
        {
            return _webResponse.GetResponseStream();
        }

        public HttpStatusCode StatusCode
        {
            get { return _webResponse.StatusCode; }
        }

        public void Dispose()
        {
            Dispose(true);
            _webResponse.Close();
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
