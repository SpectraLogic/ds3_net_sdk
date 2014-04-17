using System.Net;
using System.Collections.Generic;

using Ds3.Models;

namespace Ds3.Calls
{
    public class GetObjectRequest : Ds3Request
    {
        public string BucketName { get; private set; }
        public string ObjectName { get; private set; }

        private Range _byteRange;
        public Range ByteRange
        {
            get { return _byteRange; }
            set { WithByteRange(value); }
        }

        /// <summary>
        /// Specifies a range of bytes within the object to retrieve.
        /// </summary>
        /// <param name="byteRange"></param>
        /// <returns></returns>
        public GetObjectRequest WithByteRange(Range byteRange)
        {
            this._byteRange = byteRange;
            return this;
        }

        internal override Range GetByteRange()
        {
            return this._byteRange;
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
                return "/" + BucketName + "/" + ObjectName;
            }
        }

        public GetObjectRequest(Bucket bucket, Ds3Object ds3Object): this (bucket.Name, ds3Object.Name)
        {
        }

        public GetObjectRequest(string bucketName, string ds3ObjectName)
        {
            this.BucketName = bucketName;
            this.ObjectName = ds3ObjectName;
        }
    }
}
