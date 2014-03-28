using System.Collections.Generic;

using Ds3.AwsModels;
using Ds3.Runtime;

namespace Ds3.Models
{
    public class BulkPutRequest : BulkRequest
    {
        public BulkPutRequest(string bucketName, List<Ds3Object> objects) 
            : base(bucketName, objects)
        {
            if (!objects.TrueForAll(obj => obj.Size.HasValue))
            {
                throw new Ds3RequestException(Resources.ObjectsMissingSizeException);
            }
            QueryParams.Add("operation", "start_bulk_put");
        }
    }
}
