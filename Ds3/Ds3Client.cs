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
            return NetLayer.Invoke<GetServiceResponse, GetServiceRequest>(request);
        }

        public GetBucketResponse GetBucket(GetBucketRequest request)
        {
            return NetLayer.Invoke<GetBucketResponse, GetBucketRequest>(request);
        }

        public GetObjectResponse GetObject(GetObjectRequest request)
        {
            return NetLayer.Invoke<GetObjectResponse, GetObjectRequest>(request);
        }

        public PutObjectResponse PutObject(PutObjectRequest request)
        {
            return NetLayer.Invoke<PutObjectResponse, PutObjectRequest>(request);
        }

        public DeleteObjectResponse DeleteObject(DeleteObjectRequest request)
        {
            return NetLayer.Invoke<DeleteObjectResponse, DeleteObjectRequest>(request);
        }

        public DeleteBucketResponse DeleteBucket(DeleteBucketRequest request)
        {
            return NetLayer.Invoke<DeleteBucketResponse, DeleteBucketRequest>(request);
        }

        public PutBucketResponse PutBucket(PutBucketRequest request)
        {
            return NetLayer.Invoke<PutBucketResponse, PutBucketRequest>(request);
        }

        public BulkGetResponse BulkGet(BulkGetRequest request)
        {
            return NetLayer.Invoke<BulkGetResponse, BulkGetRequest>(request);
        }

        public BulkPutResponse BulkPut(BulkPutRequest request)
        {
            return NetLayer.Invoke<BulkPutResponse, BulkPutRequest>(request);
        }
    }
}
