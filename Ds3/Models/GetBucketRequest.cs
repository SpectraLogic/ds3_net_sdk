using System.Net;

namespace Ds3.Models
{
    public class GetBucketRequest : Ds3Request
    {
        public string BucketName { get; private set; }

        private string _marker;
        public string Marker
        {
            get { return _marker; }
            set { WithMarker(value); }
        }

        public GetBucketRequest WithMarker(string marker)
        {
            this._marker = marker;
            if (marker != null)
            {
                this.QueryParams.Add("marker", marker);
            }
            else
            {
                this.QueryParams.Remove("marker");
            }
            return this;
        }

        private int? _maxKeys;
        public int? MaxKeys
        {
            get { return _maxKeys; }
            set { WithMaxKeys(value); }
        }

        public GetBucketRequest WithMaxKeys(int? maxKeys)
        {
            this._maxKeys = maxKeys;
            if (maxKeys != null)
            {
                this.QueryParams.Add("max-keys", maxKeys.ToString());
            }
            else
            {
                this.QueryParams.Remove("max-keys");
            }
            return this;
        }

        private string _prefix;
        public string Prefix
        {
            get { return _prefix; }
            set { WithPrefix(value); }
        }

        public GetBucketRequest WithPrefix(string prefix)
        {
            this._prefix = prefix;
            if (prefix != null)
            {
                this.QueryParams.Add("prefix", prefix);
            }
            else
            {
                this.QueryParams.Remove("prefix");
            }
            return this;
        }

        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.GET;
            }
        }

        internal override string Path
        {
            get
            {
                return "/" + BucketName;
            }
        }

        public GetBucketRequest(string bucketName)
        {
            this.BucketName = bucketName;
        }
    }
}
