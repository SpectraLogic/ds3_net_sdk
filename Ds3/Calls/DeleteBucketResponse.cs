using System.Net;

using Ds3.Runtime;

namespace Ds3.Calls
{
    public class DeleteBucketResponse : Ds3Response
    {
        internal DeleteBucketResponse(IWebResponse response)
            : base(response)
        {
            HandleStatusCode(HttpStatusCode.NoContent);
        }
    }
}
