using System.IO;

namespace Ds3.Models
{
    public class PutObjectRequest : Ds3Request
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
                return "/" + BucketName + "/" + ObjectName;
            }
        }

        private Stream _content;

        public override Stream GetContentStream()
        {
            return _content;
        }

        private string _bucketName;

        public string BucketName
        {
            get { return _bucketName; }
        }

        private string _objectName;

        public string ObjectName
        {
            get { return _objectName; }
        }

        public PutObjectRequest(Bucket bucket, string objectName, Stream content)
            : this(bucket.Name, objectName, content)
        {
        }

        public PutObjectRequest(string bucketName, string objectName, Stream content)
        {
            this._bucketName = bucketName;
            this._objectName = objectName;
            this._content = content;
        }
    }
}
