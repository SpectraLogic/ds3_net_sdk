using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.IO;

namespace Ds3.Models
{
    public class GetServiceResponse : Ds3Response
    {
        public GetServiceResponse(HttpWebResponse responseStream)
            : base(responseStream)
        {
            Console.WriteLine("Got content.");
        }

        public string Reponse()
        {
            Stream content = response.GetResponseStream();
            byte[] contentBuff = new byte[response.ContentLength];
            content.Read(contentBuff, 0, (int)response.ContentLength);            
            
            return FormatXml(System.Text.Encoding.UTF8.GetString(contentBuff));
        }            
    }
}
