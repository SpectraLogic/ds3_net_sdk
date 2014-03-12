using System.IO;
using System.Net;
using System.Xml.Serialization;
using System.Collections.Generic;

using Ds3.AwsModels;

namespace Ds3.Models
{
    public class BulkPutResponse : BulkResponse
    {
        public BulkPutResponse(HttpWebResponse response) : base(response)
        {            
        }
    }
}
