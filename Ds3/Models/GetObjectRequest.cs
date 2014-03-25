using System.Net;

using Ds3.AwsModels;

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

        private string _prefix;        

        public string Prefix {
            get {
                return _prefix;
            }

            set
            {
                WithPrefix(value);
            }
        }

        public GetObjectRequest WithPrefix(string prefix)
        {
            this._prefix = prefix;
            this.QueryParams.Add("prefix", prefix);
            return this;
        }

        private string _nextMarker;
        public string NextMarker {
            get { return _nextMarker; }
            set { WithNextMarker(value); }
        }

        public GetObjectRequest WithNextMarker(string nextMarker)
        {
            this._nextMarker = nextMarker;
            this.QueryParams.Add("marker", nextMarker);
            return this;
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
