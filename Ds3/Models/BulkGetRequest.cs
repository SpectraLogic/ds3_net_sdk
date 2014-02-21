using System.IO;
using System.Collections.Generic;

namespace Ds3.Models
{
    class BulkGetRequest : Ds3Request
    {

        public override HttpVerb Verb
        {
            get
            {
                return HttpVerb.PUT;
            }
        }

        public override string Path
        {
            get
            {
                return "/__rest__/" + BucketName;
            }
        }

        private Stream _content;

        public override Stream Content
        {
            get
            {
                return _content;
            }
        }

        private string _bucketName;

        public string BucketName
        {
            get { return _bucketName; }
        }

        public BulkGetRequest(string bucketName, List<Ds3Object> objectList)
        { 
        
        }
    }
}
