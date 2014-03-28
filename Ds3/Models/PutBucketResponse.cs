using System.Net;

namespace Ds3.Models
{
    public class PutBucketResponse : Ds3Response
    {
        public PutBucketResponse(HttpWebResponse response)
            : base(response)
        {
            HandleStatusCode(HttpStatusCode.OK);
        }
    }
}
