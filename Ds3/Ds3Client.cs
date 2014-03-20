using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Ds3.Runtime;
using Ds3.Models;

namespace Ds3
{
    public class Ds3Client
    {
        private Credentials Creds;
        private Uri Endpoint;

        public Ds3Client(string endpoint, Credentials creds) {
            this.Creds = creds;
            this.Endpoint = new Uri(endpoint);         
        }
        
        public GetServiceResponse GetService(GetServiceRequest request){
            Task<GetServiceResponse> response = GetServiceAsync(request);

            return response.Result;
        }

        public Task<GetServiceResponse> GetServiceAsync(GetServiceRequest request)
        {
            return Network.Invoke<GetServiceResponse, GetServiceRequest>(request, this.Endpoint, this.Creds);
        }

        public GetBucketResponse GetBucket(GetBucketRequest request)
        {
            Task<GetBucketResponse> response = GetBucketAsync(request);

            return response.Result;
        }

        public Task<GetBucketResponse> GetBucketAsync(GetBucketRequest request)
        {
            return Network.Invoke<GetBucketResponse, GetBucketRequest>(request, this.Endpoint, this.Creds);
        }

        public GetObjectResponse GetObject(GetObjectRequest request)
        {
            Task<GetObjectResponse> response = GetObjectAsync(request);

            return response.Result;
        }

        public Task<GetObjectResponse> GetObjectAsync(GetObjectRequest request)
        {
            return Network.Invoke<GetObjectResponse, GetObjectRequest>(request, this.Endpoint, this.Creds);
        }

        public PutObjectResponse PutObject(PutObjectRequest request)
        {
            Task<PutObjectResponse> response = PutObjectAsync(request);

            return response.Result;
        }

        public Task<PutObjectResponse> PutObjectAsync(PutObjectRequest request)
        {
            return Network.Invoke<PutObjectResponse, PutObjectRequest>(request, this.Endpoint, this.Creds);
        }

        public DeleteObjectResponse DeleteObject(DeleteObjectRequest request)
        {
            Task<DeleteObjectResponse> response = DeleteObjectAsync(request);

            return response.Result;
        }

        public Task<DeleteObjectResponse> DeleteObjectAsync(DeleteObjectRequest request)
        {
            return Network.Invoke<DeleteObjectResponse, DeleteObjectRequest>(request, this.Endpoint, this.Creds);
        }

        public DeleteBucketResponse DeleteBucket(DeleteBucketRequest request)
        {
            Task<DeleteBucketResponse> response = DeleteBucketAsync(request);

            return response.Result;
        }

        public Task<DeleteBucketResponse> DeleteBucketAsync(DeleteBucketRequest request)
        {
            return Network.Invoke<DeleteBucketResponse, DeleteBucketRequest>(request, this.Endpoint, this.Creds);
        }

        public PutBucketResponse PutBucket(PutBucketRequest request)
        {
            Task<PutBucketResponse> response = PutBucketAsync(request);

            return response.Result;
        }

        public Task<PutBucketResponse> PutBucketAsync(PutBucketRequest request)
        {
            return Network.Invoke<PutBucketResponse, PutBucketRequest>(request, this.Endpoint, this.Creds);
        }

        public BulkGetResponse BulkGet(BulkGetRequest request)
        {
            Task<BulkGetResponse> response = BulkGetAsync(request);

            return response.Result;
        }

        public Task<BulkGetResponse> BulkGetAsync(BulkGetRequest request)
        {
            return Network.Invoke<BulkGetResponse, BulkGetRequest>(request, this.Endpoint, this.Creds);
        }

        public BulkPutResponse BulkPut(BulkPutRequest request)
        {
            Task<BulkPutResponse> response = BulkPutAsync(request);

            return response.Result;
        }

        public Task<BulkPutResponse> BulkPutAsync(BulkPutRequest request)
        {
            return Network.Invoke<BulkPutResponse, BulkPutRequest>(request, this.Endpoint, this.Creds);
        }
    }
}
