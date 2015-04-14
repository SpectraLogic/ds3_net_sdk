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
using System.Linq;
using System.Net;

using Ds3.Calls;
using Ds3.Models;
using Ds3.ResponseParsers;
using Ds3.Runtime;

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
            return new GetServiceResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetBucketResponse GetBucket(GetBucketRequest request)
        {
            return new GetBucketResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetObjectResponse GetObject(GetObjectRequest request)
        {
            return new GetObjectResponseParser(_netLayer.CopyBufferSize).Parse(request, _netLayer.Invoke(request));
        }

        public HeadObjectResponse HeadObject(HeadObjectRequest request)
        {
            return new HeadObjectResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public void PutObject(PutObjectRequest request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.OK);
            }
        }

        public void DeleteObject(DeleteObjectRequest request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public DeleteObjectListResponse DeleteObjectList(DeleteObjectListRequest request)
        {
            return new DeleteObjectListResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public void DeleteBucket(DeleteBucketRequest request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void PutBucket(PutBucketRequest request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.OK);
            }
        }

        public HeadBucketResponse HeadBucket(HeadBucketRequest request)
        {
            return new HeadBucketResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public JobResponse BulkGet(BulkGetRequest request)
        {
            return new JobResponseParser<BulkGetRequest>().Parse(request, _netLayer.Invoke(request));
        }

        public JobResponse BulkPut(BulkPutRequest request)
        {
            return new JobResponseParser<BulkPutRequest>().Parse(request, _netLayer.Invoke(request));
        }

        public GetJobListResponse GetJobList(GetJobListRequest request)
        {
            return new GetJobListResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public void DeleteJob(DeleteJobRequest request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public JobResponse ModifyJob(ModifyJobRequest request)
        {
            return new JobResponseParser<ModifyJobRequest>().Parse(request, _netLayer.Invoke(request));
        }

        public JobResponse GetJob(GetJobRequest request)
        {
            return new JobResponseParser<GetJobRequest>().Parse(request, _netLayer.Invoke(request));
        }

        public AllocateJobChunkResponse AllocateJobChunk(AllocateJobChunkRequest request)
        {
            return new AllocateJobChunkResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetAvailableJobChunksResponse GetAvailableJobChunks(GetAvailableJobChunksRequest request)
        {
            return new GetAvailableJobChunksResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetPhysicalPlacementResponse GetPhysicalPlacement(GetPhysicalPlacementRequest request)
        {
            return new GetPhysicalPlacementResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public IDs3ClientFactory BuildFactory(IEnumerable<Node> nodes)
        {
            return new Ds3ClientFactory(this, nodes);
        }

        private class Ds3ClientFactory : IDs3ClientFactory
        {
            private readonly IDs3Client _client;
            private readonly IDictionary<Guid, Node> _nodes;

            public Ds3ClientFactory(IDs3Client client, IEnumerable<Node> nodes)
            {
                this._client = client;
                this._nodes = nodes.ToDictionary(node => node.Id);
            }

            public IDs3Client GetClientForNodeId(Guid? nodeId)
            {
                //TODO: this needs to return a client that connects to the specified server id.
                return this._client;
            }
        }
    }
}
