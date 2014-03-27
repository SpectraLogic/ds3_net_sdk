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

        private string _marker;
        public string Marker
        {
            get { return _marker; }
            set { WithMarker(value); }
        }

        public GetBucketRequest WithMarker(string marker)
        {
            this._marker = marker;
            if (marker == null)
            {
                this.QueryParams.Remove("marker");
            }
            else
            {
                this.QueryParams.Add("marker", marker);
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
            if (maxKeys == null)
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
            if (prefix == null)
            {
                this.QueryParams.Add("prefix", prefix);
            }
            else
            {
                this.QueryParams.Remove("prefix");
            }
            return this;
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
