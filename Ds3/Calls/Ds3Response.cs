using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Net;
using System.Xml;
using System.Diagnostics;
using System.Xml.Serialization;

using Ds3.Runtime;
using Ds3.Models;

namespace Ds3.Calls
{
    public class Ds3Response : IDisposable
    {        
        internal IWebResponse response;

        internal Ds3Response(IWebResponse response)
        {
            this.response = response;
        }

        protected void HandleStatusCode(HttpStatusCode expectedStatusCode)
        {
            HttpStatusCode actualStatusCode = response.StatusCode;
            if (!actualStatusCode.Equals(expectedStatusCode))
            {
                var responseContent = GetResponseContent(response);
                var error = ParseError(responseContent);
                throw new Ds3BadStatusCodeException(expectedStatusCode, actualStatusCode, error, responseContent);
            }
        }

        private static string GetResponseContent(IWebResponse response)
        {
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }

        private static Ds3Error ParseError(string responseContent)
        {
            try
            {
                var root = XDocument.Parse(responseContent).ElementOrThrow("Error");
                return new Ds3Error(
                    root.TextOf("Code"),
                    root.TextOf("Message"),
                    root.TextOf("Resource"),
                    root.TextOf("RequestId")
                );
            }
            catch (XmlException)
            {
                return null;
            }
            catch (Ds3BadResponseException)
            {
                return null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            response.Dispose();
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
