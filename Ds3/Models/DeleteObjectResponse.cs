using System.Net;

namespace Ds3.Models
{
    public class DeleteObjectResponse : Ds3Response
    {
        public DeleteObjectResponse(HttpWebResponse response)
            : base(response)
        {
            HandleStatusCode(HttpStatusCode.NoContent);
        }
    }
}
