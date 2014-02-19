using System.Net;

namespace Ds3.Models
{
    public class DeleteBucketRequest : Ds3Request
    {
        private string _bucketName;

        public string BucketName
        {
            get { return _bucketName; }
        }

        public override HttpVerb Verb
        {
            get
            {
                return HttpVerb.DELETE;
            }
        }

        public override string Path
        {
            get
            {
                return "/" + BucketName;
            }
        }

        public override HttpStatusCode StatusCode
        {
            get { return HttpStatusCode.NoContent; }
        }

        public DeleteBucketRequest(string bucketName)
        {
            this._bucketName = bucketName;
        }
    }
}
