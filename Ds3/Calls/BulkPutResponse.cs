using System.IO;
using System.Net;
using System.Xml.Serialization;
using System.Collections.Generic;

using Ds3.Runtime;

namespace Ds3.Calls
{
    public class BulkPutResponse : BulkResponse
    {
        internal BulkPutResponse(IWebResponse response)
            : base(response)
        {            
        }
    }
}
