using System.Collections.Generic;

using Ds3.AwsModels;

namespace Ds3.Models
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
