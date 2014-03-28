using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Net;
using System.Xml;

using Ds3.Runtime;
using System.Diagnostics;
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
            catch (XmlException e)
            {
                Trace.WriteLine("Encountered an exception when formating xml string.", "DS3_Response");
                Trace.WriteLine(e, "DS3_Response");
                return Xml;
            }
        }

        protected void HandleStatusCode(HttpStatusCode expectedStatusCode)
        {
            HttpStatusCode actualStatusCode = response.StatusCode;
            if (!actualStatusCode.Equals(expectedStatusCode))
            {
                throw new Ds3BadStatusCodeException(expectedStatusCode, actualStatusCode);
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
