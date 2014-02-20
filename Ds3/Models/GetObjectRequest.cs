using System.Net;

namespace Ds3.Models
{
    public class GetObjectRequest : Ds3Request
    {
        private string _bucketName;

        public string BucketName
        {
            get { return _bucketName; }
        }

        public string _objectName;

        public string ObjectName
        {
            get { return _objectName; }
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
                return "/" + BucketName + "/" + ObjectName;
            }
        }

        public GetObjectRequest(Bucket bucket, Ds3Object ds3Object): this (bucket.Name, ds3Object.Name)
        {
        }

        public GetObjectRequest(string bucketName, string ds3ObjectName)
        {
            this._bucketName = bucketName;
            this._objectName = ds3ObjectName;
        }
    }
}
