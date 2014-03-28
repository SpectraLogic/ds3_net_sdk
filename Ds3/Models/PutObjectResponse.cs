using System.Net;

namespace Ds3.Models
{
    public class PutObjectResponse : Ds3Response
    {
        public PutObjectResponse(HttpWebResponse response) : base(response)
        {
            HandleStatusCode(HttpStatusCode.OK);
        }
    }
}
