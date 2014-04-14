using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Ds3.Runtime;
using Ds3.Models;

namespace Ds3
{
    public class Ds3Client
    {
        private INetwork _netLayer;

        internal Ds3Client(INetwork netLayer)
        {
            this._netLayer = netLayer;
        }

        public GetServiceResponse GetService(GetServiceRequest request)
        {
            return new GetServiceResponse(_netLayer.Invoke(request));
        }

        public GetBucketResponse GetBucket(GetBucketRequest request)
        {
            return new GetBucketResponse(_netLayer.Invoke(request));
        }

        public GetObjectResponse GetObject(GetObjectRequest request)
        {
            return new GetObjectResponse(_netLayer.Invoke(request));
        }

        public PutObjectResponse PutObject(PutObjectRequest request)
        {
            return new PutObjectResponse(_netLayer.Invoke(request));
        }

        public DeleteObjectResponse DeleteObject(DeleteObjectRequest request)
        {
            return new DeleteObjectResponse(_netLayer.Invoke(request));
        }

        public DeleteBucketResponse DeleteBucket(DeleteBucketRequest request)
        {
            return new DeleteBucketResponse(_netLayer.Invoke(request));
        }

        public PutBucketResponse PutBucket(PutBucketRequest request)
        {
            return new PutBucketResponse(_netLayer.Invoke(request));
        }

        public BulkGetResponse BulkGet(BulkGetRequest request)
        {
            return new BulkGetResponse(_netLayer.Invoke(request));
        }

        public BulkPutResponse BulkPut(BulkPutRequest request)
        {
            return new BulkPutResponse(_netLayer.Invoke(request));
        }
    }
}
