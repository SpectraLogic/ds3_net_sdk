using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Net;
using System.Xml;
using System.Diagnostics;
using System.Xml.Serialization;

using Ds3.Runtime;

namespace Ds3.Models
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
                Ds3Error error;
                try
                {
                    error = MapErrorFromSerializationEntity((Error)new XmlSerializer(typeof(Error)).Deserialize(new StringReader(responseContent)));
                }
                catch (InvalidOperationException)
                {
                    error = null;
                }
                throw new Ds3BadStatusCodeException(expectedStatusCode, actualStatusCode, error, responseContent);
            }
        }

        private static string GetResponseContent(IWebResponse response)
        {
            using (var stream = response.GetResponseStream())
            using (var reader = new StreamReader(stream))
                return reader.ReadToEnd();
        }

        private static Ds3Error MapErrorFromSerializationEntity(Error error)
        {
            return new Ds3Error(error.Code, error.Message, error.Resource, error.RequestId);
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
