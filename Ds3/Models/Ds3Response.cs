using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Net;

using Ds3.Runtime;
namespace Ds3.Models
{
    public class Ds3Response : IDisposable
    {        
        protected HttpWebResponse response;

        public Ds3Response(HttpWebResponse response)
        {
            this.response = response;
        }

        public string FormatXml(String Xml)
        {
            try
            {
                XDocument doc = XDocument.Parse(Xml);
                return doc.ToString();
            }
            catch (Exception)
            {
                return Xml;
            }
        }

        protected void handleStatusCode(HttpStatusCode expectedStatusCode)
        {
            HttpStatusCode actualStatusCode = response.StatusCode;
            if (!actualStatusCode.Equals(expectedStatusCode))
            {
                throw new Ds3RequestException(expectedStatusCode, actualStatusCode);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            response.Close();

            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }
    }
}
