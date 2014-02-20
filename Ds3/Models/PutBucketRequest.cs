namespace Ds3.Models
{
    public class PutBucketRequest : Ds3Request
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
                return "/" + BucketName;
            }
        }

        private string _bucketName;

        public string BucketName
        {
            get { return _bucketName; }
        }

        public PutBucketRequest(string bucketName)
        {
            this._bucketName = bucketName;
        }
    }
}
