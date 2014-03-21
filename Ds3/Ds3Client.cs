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
        
        internal Ds3Client(Network netLayer) {
            this.NetLayer = netLayer;
        }
        
        public GetServiceResponse GetService(GetServiceRequest request){
            return NetLayer.Invoke<GetServiceResponse, GetServiceRequest>(request);
        }

        public Task<GetServiceResponse> GetServiceAsync(GetServiceRequest request)
        {
            return NetLayer.InvokeAsync<GetServiceResponse, GetServiceRequest>(request);
        }

        public GetBucketResponse GetBucket(GetBucketRequest request)
        {
            return NetLayer.Invoke<GetBucketResponse, GetBucketRequest>(request);
        }

        public Task<GetBucketResponse> GetBucketAsync(GetBucketRequest request)
        {
            return NetLayer.InvokeAsync<GetBucketResponse, GetBucketRequest>(request);
        }

        public GetObjectResponse GetObject(GetObjectRequest request)
        {
            return NetLayer.Invoke<GetObjectResponse, GetObjectRequest>(request);
        }

        public Task<GetObjectResponse> GetObjectAsync(GetObjectRequest request)
        {
            return NetLayer.InvokeAsync<GetObjectResponse, GetObjectRequest>(request);
        }

        public PutObjectResponse PutObject(PutObjectRequest request)
        {
            return NetLayer.Invoke<PutObjectResponse, PutObjectRequest>(request);
        }

        public Task<PutObjectResponse> PutObjectAsync(PutObjectRequest request)
        {
            return NetLayer.InvokeAsync<PutObjectResponse, PutObjectRequest>(request);
        }

        public DeleteObjectResponse DeleteObject(DeleteObjectRequest request)
        {
            return NetLayer.Invoke<DeleteObjectResponse, DeleteObjectRequest>(request);
        }

        public Task<DeleteObjectResponse> DeleteObjectAsync(DeleteObjectRequest request)
        {
            return NetLayer.InvokeAsync<DeleteObjectResponse, DeleteObjectRequest>(request);
        }

        public DeleteBucketResponse DeleteBucket(DeleteBucketRequest request)
        {
            return NetLayer.Invoke<DeleteBucketResponse, DeleteBucketRequest>(request);
        }

        public Task<DeleteBucketResponse> DeleteBucketAsync(DeleteBucketRequest request)
        {
            return NetLayer.InvokeAsync<DeleteBucketResponse, DeleteBucketRequest>(request);
        }

        public PutBucketResponse PutBucket(PutBucketRequest request)
        {
            return NetLayer.Invoke<PutBucketResponse, PutBucketRequest>(request);
        }

        public Task<PutBucketResponse> PutBucketAsync(PutBucketRequest request)
        {
            return NetLayer.InvokeAsync<PutBucketResponse, PutBucketRequest>(request);
        }

        public BulkGetResponse BulkGet(BulkGetRequest request)
        {
            return NetLayer.Invoke<BulkGetResponse, BulkGetRequest>(request);
        }

        public Task<BulkGetResponse> BulkGetAsync(BulkGetRequest request)
        {
            return NetLayer.InvokeAsync<BulkGetResponse, BulkGetRequest>(request);
        }

        public BulkPutResponse BulkPut(BulkPutRequest request)
        {
            return NetLayer.Invoke<BulkPutResponse, BulkPutRequest>(request);
        }

        public Task<BulkPutResponse> BulkPutAsync(BulkPutRequest request)
        {
            return NetLayer.InvokeAsync<BulkPutResponse, BulkPutRequest>(request);
        }
    }
}
