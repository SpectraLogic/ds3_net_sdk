using System.Net;

namespace Ds3.Models
{
    public class DeleteBucketRequest : Ds3Request
    {
        public string BucketName { get; private set; }

        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.DELETE;
            }
        }

        internal override string Path
        {
            get
            {
                return "/" + BucketName;
            }
        }

        public DeleteBucketRequest(string bucketName)
        {
            this.BucketName = bucketName;
        }
    }
}
