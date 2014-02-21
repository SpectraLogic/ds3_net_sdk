using System.Net;

namespace Ds3.Models
{
    class BulkPutResponse : Ds3Response
    {
        public BulkPutResponse(HttpWebResponse response) : base(response)
        {

        }
    }
}
