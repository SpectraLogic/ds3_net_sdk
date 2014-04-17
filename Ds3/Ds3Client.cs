using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Ds3.Runtime;
using Ds3.Models;

namespace Ds3
{
    /// <summary>
    /// The main DS3 API class. Use Ds3Builder to instantiate.
    /// </summary>
    public class Ds3Client
    {
        private INetwork _netLayer;

        internal Ds3Client(INetwork netLayer)
        {
            this._netLayer = netLayer;
        }

        /// <summary>
        /// Retrieves the list of buckets currently on the DS3 server given the client's credentials.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetServiceResponse GetService(GetServiceRequest request)
        {
            return new GetServiceResponse(_netLayer.Invoke(request));
        }

        /// <summary>
        /// Retrieves the list of objects in the specified bucket. Note that
        /// the server may choose to limit the number of objects specified in its
        /// reply, so you may have to call this multiple times using the
        /// request.WithMarker() method.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetBucketResponse GetBucket(GetBucketRequest request)
        {
            return new GetBucketResponse(_netLayer.Invoke(request));
        }

        /// <summary>
        /// Retrieves a Stream of the contents of the specified object.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public GetObjectResponse GetObject(GetObjectRequest request)
        {
            return new GetObjectResponse(_netLayer.Invoke(request));
        }

        /// <summary>
        /// Submits the contents of a Stream to a specified object.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PutObjectResponse PutObject(PutObjectRequest request)
        {
            return new PutObjectResponse(_netLayer.Invoke(request));
        }

        /// <summary>
        /// Deletes the specified object.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DeleteObjectResponse DeleteObject(DeleteObjectRequest request)
        {
            return new DeleteObjectResponse(_netLayer.Invoke(request));
        }

        /// <summary>
        /// Deletes the specified bucket.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public DeleteBucketResponse DeleteBucket(DeleteBucketRequest request)
        {
            return new DeleteBucketResponse(_netLayer.Invoke(request));
        }

        /// <summary>
        /// Creates the specified bucket.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public PutBucketResponse PutBucket(PutBucketRequest request)
        {
            return new PutBucketResponse(_netLayer.Invoke(request));
        }

        /// <summary>
        /// Primes a DS3 bulk get for better performance.
        /// Note that this request requires that each Ds3Object have both the
        /// name and the size set. Subsequent GetObject operations should be
        /// performed in the order specified by the BulkGetResponse.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public BulkGetResponse BulkGet(BulkGetRequest request)
        {
            return new BulkGetResponse(_netLayer.Invoke(request));
        }

        /// <summary>
        /// Primes a DS3 bulk put for better performance.
        /// Note that this request requires that each Ds3Object have both the
        /// name and the size set. Subsequent PutObject operations should be
        /// performed in the order specified by the BulkPutResponse.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public BulkPutResponse BulkPut(BulkPutRequest request)
        {
            return new BulkPutResponse(_netLayer.Invoke(request));
        }
    }
}
