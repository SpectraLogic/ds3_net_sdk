using System.Net;

namespace Ds3.Models
{
    public class BulkGetResponse : Ds3Response
    {
        public BulkGetResponse(HttpWebResponse response)
            : base(response)
        {

        }
    }
}
