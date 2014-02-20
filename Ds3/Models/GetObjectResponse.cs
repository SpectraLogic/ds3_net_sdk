using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;
using System.IO;


namespace Ds3.Models
{
    public class GetObjectResponse : Ds3Response
    {

        private Stream _contents;

        public Stream Contents
        {
            get { return _contents; }
        }

        public GetObjectResponse(HttpWebResponse responseStream) 
            : base(responseStream)
        {
            handleStatusCode(HttpStatusCode.OK);
            processResponse();   
        }

        private void processResponse()
        {
            _contents = response.GetResponseStream();
        }
    }
}
