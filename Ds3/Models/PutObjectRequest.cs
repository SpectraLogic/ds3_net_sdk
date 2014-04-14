using System.IO;

namespace Ds3.Models
{
    public class PutObjectRequest : Ds3Request
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
                return "/" + BucketName + "/" + ObjectName;
            }
        }

        private Stream _content;

        internal override Stream GetContentStream()
        {
            return _content;
        }

        public string BucketName { get; private set; }
        public string ObjectName { get; private set; }

        public PutObjectRequest(Bucket bucket, string objectName, Stream content)
            : this(bucket.Name, objectName, content)
        {
        }

        public PutObjectRequest(string bucketName, string objectName, Stream content)
        {
            this.BucketName = bucketName;
            this.ObjectName = objectName;
            this._content = content;
        }
    }
}
