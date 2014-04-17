using System.Net;

using Ds3.Runtime;

namespace Ds3.Calls
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
