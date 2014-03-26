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

        public string Marker
        {
            get { return this.QueryParams["marker"]; }
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
            : this(bucketName, null)
        {
        }

        public GetBucketRequest(string bucketName, string marker)
            : this(bucketName, marker, 0)
        {
        }

        public GetBucketRequest(string bucketName, string marker, int? maxKeys)
        {
            this._bucketName = bucketName;
            if (marker != null)
                this.QueryParams["marker"] = marker;
            if (maxKeys.HasValue)
                this.QueryParams["max-keys"] = maxKeys.Value.ToString("D");
        }
    }
}
