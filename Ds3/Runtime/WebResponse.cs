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

namespace Ds3.Runtime
{
    internal class WebResponse : IWebResponse
    {
        private readonly HttpWebResponse _webResponse;
        private IDictionary<string, string> _headers;

        internal WebResponse(HttpWebResponse webResponse)
        {
            _webResponse = webResponse;
        }

        public Stream GetResponseStream()
        {
            return new WebStream(_webResponse.GetResponseStream(), _webResponse.ContentLength);
        }

        public HttpStatusCode StatusCode
        {
            get { return _webResponse.StatusCode; }
        }

        public IDictionary<string, string> Headers
        {
            get { return _headers ?? (_headers = ConvertToDictionary(_webResponse.Headers)); }
        }

        private static IDictionary<string, string> ConvertToDictionary(WebHeaderCollection headers)
        {
            return headers.Keys.Cast<string>().ToDictionary(key => key.ToLowerInvariant(), key => headers[key]);
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