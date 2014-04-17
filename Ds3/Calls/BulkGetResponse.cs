using System.Net;

using Ds3.Runtime;

namespace Ds3.Calls
{
    public class BulkGetResponse : BulkResponse
    {
        internal BulkGetResponse(IWebResponse response)
            : base(response)
        {            
        }
    }
}
