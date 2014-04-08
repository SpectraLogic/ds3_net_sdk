using System.Net;

using Ds3.Runtime;

namespace Ds3.Models
{
    public class DeleteObjectResponse : Ds3Response
    {
        internal DeleteObjectResponse(IWebResponse response)
            : base(response)
        {
            HandleStatusCode(HttpStatusCode.NoContent);
        }
    }
}
