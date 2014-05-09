/*
 * ******************************************************************************
 *   Copyright 2014 Spectra Logic Corporation. All Rights Reserved.
 *   Licensed under the Apache License, Version 2.0 (the "License"). You may not use
 *   this file except in compliance with the License. A copy of the License is located at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 *   or in the "license" file accompanying this file.
 *   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
 *   CONDITIONS OF ANY KIND, either express or implied. See the License for the
 *   specific language governing permissions and limitations under the License.
 * ****************************************************************************
 */

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Ds3.Runtime;
using Ds3.Models;
using Ds3.Calls;

namespace Ds3
{
    internal class Ds3Client : IDs3Client
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

        public HeadBucketResponse HeadBucket(HeadBucketRequest request)
        {
            return new HeadBucketResponse(_netLayer.Invoke(request));
        }

        public BulkGetResponse BulkGet(BulkGetRequest request)
        {
            return new BulkGetResponse(_netLayer.Invoke(request));
        }

        public BulkPutResponse BulkPut(BulkPutRequest request)
        {
            return new BulkPutResponse(_netLayer.Invoke(request));
        }

        public GetJobResponse GetJob(GetJobRequest request)
        {
            return new GetJobResponse(_netLayer.Invoke(request));
        }
    }
}
