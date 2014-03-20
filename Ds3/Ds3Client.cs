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

        //TODO Add in another constructor where the endpoint is a uri
        public Ds3Client(string endpoint, Credentials creds) {
            this.Creds = creds;
            this.Endpoint = new Uri(endpoint);         
        }
        
        public GetServiceResponse GetService(GetServiceRequest request){
            return Network.Invoke<GetServiceResponse, GetServiceRequest>(request, this.Endpoint, this.Creds);
        }

        public Task<GetServiceResponse> GetServiceAsync(GetServiceRequest request)
        {
            return Network.InvokeAsync<GetServiceResponse, GetServiceRequest>(request, this.Endpoint, this.Creds);
        }

        public GetBucketResponse GetBucket(GetBucketRequest request)
        {
            return Network.Invoke<GetBucketResponse, GetBucketRequest>(request, this.Endpoint, this.Creds);
        }

        public Task<GetBucketResponse> GetBucketAsync(GetBucketRequest request)
        {
            return Network.InvokeAsync<GetBucketResponse, GetBucketRequest>(request, this.Endpoint, this.Creds);
        }

        public GetObjectResponse GetObject(GetObjectRequest request)
        {
            return Network.Invoke<GetObjectResponse, GetObjectRequest>(request, this.Endpoint, this.Creds);
        }

        public Task<GetObjectResponse> GetObjectAsync(GetObjectRequest request)
        {
            return Network.InvokeAsync<GetObjectResponse, GetObjectRequest>(request, this.Endpoint, this.Creds);
        }

        public PutObjectResponse PutObject(PutObjectRequest request)
        {
            return Network.Invoke<PutObjectResponse, PutObjectRequest>(request, this.Endpoint, this.Creds);
        }

        public Task<PutObjectResponse> PutObjectAsync(PutObjectRequest request)
        {
            return Network.InvokeAsync<PutObjectResponse, PutObjectRequest>(request, this.Endpoint, this.Creds);
        }

        public DeleteObjectResponse DeleteObject(DeleteObjectRequest request)
        {
            return Network.Invoke<DeleteObjectResponse, DeleteObjectRequest>(request, this.Endpoint, this.Creds);
        }

        public Task<DeleteObjectResponse> DeleteObjectAsync(DeleteObjectRequest request)
        {
            return Network.InvokeAsync<DeleteObjectResponse, DeleteObjectRequest>(request, this.Endpoint, this.Creds);
        }

        public DeleteBucketResponse DeleteBucket(DeleteBucketRequest request)
        {
            return Network.Invoke<DeleteBucketResponse, DeleteBucketRequest>(request, this.Endpoint, this.Creds);
        }

        public Task<DeleteBucketResponse> DeleteBucketAsync(DeleteBucketRequest request)
        {
            return Network.InvokeAsync<DeleteBucketResponse, DeleteBucketRequest>(request, this.Endpoint, this.Creds);
        }

        public PutBucketResponse PutBucket(PutBucketRequest request)
        {
            return Network.Invoke<PutBucketResponse, PutBucketRequest>(request, this.Endpoint, this.Creds);
        }

        public Task<PutBucketResponse> PutBucketAsync(PutBucketRequest request)
        {
            return Network.InvokeAsync<PutBucketResponse, PutBucketRequest>(request, this.Endpoint, this.Creds);
        }

        public BulkGetResponse BulkGet(BulkGetRequest request)
        {
            return Network.Invoke<BulkGetResponse, BulkGetRequest>(request, this.Endpoint, this.Creds);
        }

        public Task<BulkGetResponse> BulkGetAsync(BulkGetRequest request)
        {
            return Network.InvokeAsync<BulkGetResponse, BulkGetRequest>(request, this.Endpoint, this.Creds);
        }

        public BulkPutResponse BulkPut(BulkPutRequest request)
        {
            return Network.Invoke<BulkPutResponse, BulkPutRequest>(request, this.Endpoint, this.Creds);
        }

        public Task<BulkPutResponse> BulkPutAsync(BulkPutRequest request)
        {
            return Network.InvokeAsync<BulkPutResponse, BulkPutRequest>(request, this.Endpoint, this.Creds);
        }
    }
}
