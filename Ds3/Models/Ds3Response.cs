using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Net;

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
