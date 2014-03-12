using System.Net;

namespace Ds3.Models
{
    public class BulkGetResponse : BulkResponse
    {
        public BulkGetResponse(HttpWebResponse response)
            : base(response)
        {            
        }
    }
}
