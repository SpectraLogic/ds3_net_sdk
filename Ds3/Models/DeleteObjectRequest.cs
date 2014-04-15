using System.Net;

using Ds3.AwsModels;

namespace Ds3.Models
{
    public class DeleteObjectRequest : Ds3Request
    {
        public string BucketName { get; private set; }
        public string ObjectName { get; private set; }

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
                return "/" + BucketName + "/" + ObjectName;
            }
        }

        public DeleteObjectRequest(Bucket bucket, Ds3Object ds3Object): this (bucket.Name, ds3Object.Name)
        {
        }

        public DeleteObjectRequest(string bucketName, string ds3ObjectName)
        {
            this.BucketName = bucketName;
            this.ObjectName = ds3ObjectName;
        }

    }
}
