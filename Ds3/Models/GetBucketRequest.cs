using System.Net;

namespace Ds3.Models
{
    public class GetBucketRequest : Ds3Request
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
                return HttpVerb.GET;
            }
        }

        public override string Path
        {
            get
            {
                return "/" + BucketName;
            }
        }

        public GetBucketRequest(string bucketName)
        {
            this._bucketName = bucketName;
        }

    }
}
