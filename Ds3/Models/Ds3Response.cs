using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Net;
using System.Xml;

using Ds3.Runtime;
using System.Diagnostics;
using System.Xml.Serialization;
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
                Trace.WriteLine(Resources.FormatXmlException, "DS3_Response");
                Trace.WriteLine(e, "DS3_Response");
                return Xml;
            }
        }

        protected void HandleStatusCode(HttpStatusCode expectedStatusCode)
        {
            HttpStatusCode actualStatusCode = response.StatusCode;
            if (!actualStatusCode.Equals(expectedStatusCode))
            {
                using (var responseStream = response.GetResponseStream())
                {
                    var error = (Error)new XmlSerializer(typeof(Error)).Deserialize(responseStream);
                    throw new Ds3BadStatusCodeException(expectedStatusCode, actualStatusCode, MapErrorFromSerializationEntity(error));
                }
            }
        }

        private static Ds3Error MapErrorFromSerializationEntity(Error error)
        {
            return new Ds3Error(error.Code, error.Message, error.Resource, error.RequestId);
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
