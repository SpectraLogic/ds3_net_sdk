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
