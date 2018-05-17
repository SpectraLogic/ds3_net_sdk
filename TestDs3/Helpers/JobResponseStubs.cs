/*
 * ******************************************************************************
 *   Copyright 2014-2017 Spectra Logic Corporation. All Rights Reserved.
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
using Ds3.Models;

namespace TestDs3.Helpers
{
    internal static class JobResponseStubs
    {
        public const string BucketName = "test-bucket";
        public static readonly Guid JobId = Guid.Parse("b99b7fe5-b787-4b53-9dfb-1a0f7d336ada");
        public static readonly Guid NodeId1 = Guid.Parse("3eec8bb6-6baa-4e96-b104-18d6ebfb282e");
        public static readonly Guid NodeId2 = Guid.Parse("288f64a9-bf8e-4dfe-b881-68b95bcc3089");
        public static readonly Guid ChunkId1 = Guid.Parse("b63a0481-7020-4306-9a9e-384102adf901");
        public static readonly Guid ChunkId2 = Guid.Parse("ea100286-97d5-47f2-b1ee-a917262bc02d");
        public static readonly Guid ChunkId3 = Guid.Parse("8698c831-bbf4-4eb7-b7a9-22a4215771f6");

        public static readonly JobNode[] Nodes = {
            new JobNode
            {
                Id = NodeId1,
                EndPoint = "http://192.168.10.1",
                HttpPort = 80,
                HttpsPort = 443
            },
            new JobNode
            {
                Id = NodeId2,
                EndPoint = "http://192.168.10.2",
                HttpPort = 80,
                HttpsPort = 443
            }
        };

        public static readonly string[] PartialFailureObjectNames = new[]
        {
            "bar"
        };

        public static readonly string[] ObjectNames = new[]
        {
            "foo",
            "bar",
            "hello"
        };

        public static MasterObjectList BuildJobResponse(params Objects[] chunks)
        {
            return new MasterObjectList
            {
                BucketName = BucketName,
                JobId = JobId,
                Priority = Priority.HIGH,
                RequestType = JobRequestType.GET,
                StartDate = new DateTime(2015, 1, 11, 11, 54, 0),
                ChunkClientProcessingOrderGuarantee = JobChunkClientProcessingOrderGuarantee.NONE,
                Nodes = Nodes,
                Objects = chunks,
                Status = JobStatus.IN_PROGRESS
            };
        }

        public static MasterObjectList BuildPutJobResponse(params Objects[] chunks)
        {
            return new MasterObjectList
            {
                BucketName = BucketName,
                JobId = JobId,
                Priority = Priority.HIGH,
                RequestType = JobRequestType.PUT,
                StartDate = new DateTime(2015, 1, 11, 11, 54, 0),
                ChunkClientProcessingOrderGuarantee = JobChunkClientProcessingOrderGuarantee.NONE,
                Nodes = Nodes,
                Objects = chunks,
                Status = JobStatus.IN_PROGRESS
            };
        }

        public static Objects ReadFailureChunk(Guid? nodeId, bool inCache)
        {
            return new Objects
            {
                ChunkId = ChunkId1,
                ChunkNumber = 1,
                NodeId = nodeId,
                ObjectsList = new[]
                {
                    new BulkObject
                    {
                        Name = "bar",
                        Length = 20,
                        Offset = 0,
                        InCache = inCache
                    }
                }
            };
        }

        public static Objects Chunk1(Guid? nodeId, bool firstInCache, bool secondInCache)
        {
            return new Objects
            {
                ChunkId = ChunkId1,
                ChunkNumber = 1,
                NodeId = nodeId,
                ObjectsList = new[]
                {
                    new BulkObject
                    {
                        Name = "bar",
                        Length = 15,
                        Offset = 0,
                        InCache = firstInCache
                    },
                    new BulkObject
                    {
                        Name = "foo",
                        Length = 10,
                        Offset = 0,
                        InCache = secondInCache
                    },
                }
            };
        }

        public static Objects Chunk1Filtered(Guid? nodeId, bool firstInCache, bool secondInCache)
        {
            return new Objects
            {
                ChunkId = ChunkId1,
                ChunkNumber = 1,
                NodeId = nodeId,
                ObjectsList = new[]
                {
                    new BulkObject
                    {
                        Name = "foo",
                        Length = 10,
                        Offset = 0,
                        InCache = secondInCache
                    },
                }
            };
        }

        public static Objects Chunk2(Guid? nodeId, bool firstInCache, bool secondInCache)
        {
            return new Objects
            {
                ChunkId = ChunkId2,
                ChunkNumber = 2,
                NodeId = nodeId,
                ObjectsList = new[]
                {
                    new BulkObject
                    {
                        Name = "foo",
                        Length = 10,
                        Offset = 10,
                        InCache = firstInCache
                    },
                    new BulkObject
                    {
                        Name = "bar",
                        Length = 20,
                        Offset = 15,
                        InCache = secondInCache
                    },
                }
            };
        }
        public static Objects Chunk2Filtered(Guid? nodeId, bool firstInCache, bool secondInCache)
        {
            return new Objects
            {
                ChunkId = ChunkId2,
                ChunkNumber = 2,
                NodeId = nodeId,
                ObjectsList = new[]
                {
                    new BulkObject
                    {
                        Name = "foo",
                        Length = 10,
                        Offset = 10,
                        InCache = firstInCache
                    }
                }
            };
        }


        public static Objects Chunk3(Guid? nodeId, bool firstInCache, bool secondInCache)
        {
            return new Objects
            {
                ChunkId = ChunkId3,
                ChunkNumber = 3,
                NodeId = nodeId,
                ObjectsList = new[]
                {
                    new BulkObject
                    {
                        Name = "hello",
                        Length = 10,
                        Offset = 0,
                        InCache = firstInCache
                    },
                    new BulkObject
                    {
                        Name = "bar",
                        Length = 11,
                        Offset = 35,
                        InCache = secondInCache
                    }
                }
            };
        }
    }
}