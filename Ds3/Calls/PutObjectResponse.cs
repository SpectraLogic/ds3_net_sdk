using System.Net;

using Ds3.Runtime;

namespace Ds3.Calls
{
    public class PutObjectResponse : Ds3Response
    {
        internal PutObjectResponse(IWebResponse response)
            : base(response)
        {
            HandleStatusCode(HttpStatusCode.OK);
        }
    }
}
