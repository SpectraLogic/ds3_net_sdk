namespace Ds3.Calls
{
    public class PutBucketRequest : Ds3Request
    {
        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.PUT;
            }
        }

        internal override string Path
        {
            get
            {
                return "/" + BucketName;
            }
        }

        public string BucketName { get; private set; }

        public PutBucketRequest(string bucketName)
        {
            this.BucketName = bucketName;
        }
    }
}
