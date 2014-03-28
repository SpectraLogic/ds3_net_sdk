using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Ds3.Runtime;
using Ds3.Models;

namespace Ds3
{
    public class Ds3Client
    {
        private Network NetLayer;

        internal Ds3Client(Network netLayer)
        {
            this.NetLayer = netLayer;
        }

        public GetServiceResponse GetService(GetServiceRequest request)
        {
            return new GetServiceResponse(NetLayer.Invoke<GetServiceRequest>(request));
        }

        public GetBucketResponse GetBucket(GetBucketRequest request)
        {
            return new GetBucketResponse(NetLayer.Invoke<GetBucketRequest>(request));
        }

        public GetObjectResponse GetObject(GetObjectRequest request)
        {
            return new GetObjectResponse(NetLayer.Invoke<GetObjectRequest>(request));
        }

        public PutObjectResponse PutObject(PutObjectRequest request)
        {
            return new PutObjectResponse(NetLayer.Invoke<PutObjectRequest>(request));
        }

        public DeleteObjectResponse DeleteObject(DeleteObjectRequest request)
        {
            return new DeleteObjectResponse(NetLayer.Invoke<DeleteObjectRequest>(request));
        }

        public DeleteBucketResponse DeleteBucket(DeleteBucketRequest request)
        {
            return new DeleteBucketResponse(NetLayer.Invoke<DeleteBucketRequest>(request));
        }

        public PutBucketResponse PutBucket(PutBucketRequest request)
        {
            return new PutBucketResponse(NetLayer.Invoke<PutBucketRequest>(request));
        }

        public BulkGetResponse BulkGet(BulkGetRequest request)
        {
            return new BulkGetResponse(NetLayer.Invoke<BulkGetRequest>(request));
        }

        public BulkPutResponse BulkPut(BulkPutRequest request)
        {
            return new BulkPutResponse(NetLayer.Invoke<BulkPutRequest>(request));
        }
    }
}
