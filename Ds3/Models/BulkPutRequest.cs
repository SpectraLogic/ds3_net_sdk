using System.Collections.Generic;

using Ds3.AwsModels;

namespace Ds3.Models
{
    public class BulkPutRequest : BulkRequest
    {
        public BulkPutRequest(string bucketName, List<Ds3Object> objects) 
            : base(bucketName, objects)
        {
            QueryParams.Add("operation", "start_bulk_put");
        }
    }
}
