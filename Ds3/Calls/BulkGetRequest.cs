using System.Collections.Generic;

using Ds3.Models;

namespace Ds3.Calls
{
    public class BulkGetRequest : BulkRequest
    {
        public BulkGetRequest(string bucketName, List<Ds3Object> objects) 
            : base(bucketName, objects)
        {
            QueryParams.Add("operation", "start_bulk_get");
        }      
    }
}
