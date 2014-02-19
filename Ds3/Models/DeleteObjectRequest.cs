using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ds3.Models
{
    public class DeleteObjectRequest : Ds3Request
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
                return HttpVerb.DELETE;
            }
        }

        public override string Path
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
            this._bucketName = bucketName;
            this._objectName = ds3ObjectName;
        }

    }
}
