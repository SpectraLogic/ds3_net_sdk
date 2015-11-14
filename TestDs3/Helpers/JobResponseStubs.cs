using Ds3.Calls;
using Ds3.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestDs3.Helpers
{
    class JobResponseStubs
    {
        public const string BucketName = "test-bucket";
        public static readonly Guid JobId = Guid.Parse("b99b7fe5-b787-4b53-9dfb-1a0f7d336ada");
        public static readonly Guid NodeId1 = Guid.Parse("3eec8bb6-6baa-4e96-b104-18d6ebfb282e");
        public static readonly Guid NodeId2 = Guid.Parse("288f64a9-bf8e-4dfe-b881-68b95bcc3089");
        public static readonly Guid ChunkId1 = Guid.Parse("b63a0481-7020-4306-9a9e-384102adf901");
        public static readonly Guid ChunkId2 = Guid.Parse("ea100286-97d5-47f2-b1ee-a917262bc02d");
        public static readonly Guid ChunkId3 = Guid.Parse("8698c831-bbf4-4eb7-b7a9-22a4215771f6");
        public static readonly Node[] Nodes = new[]
        {
            new Node(NodeId1, "http://192.168.10.1", 80, 443),
            new Node(NodeId2, "http://192.168.10.2", 80, 443)
        };
        public static readonly string[] ObjectNames = new[]
        {
            "foo",
            "bar",
            "hello"
        };

        public static JobResponse BuildJobResponse(params JobObjectList[] chunks)
        {
            return new JobResponse(
                BucketName,
                JobId,
                "HIGH",
                "GET",
                new DateTime(2015, 1, 11, 11, 54, 0),
                ChunkOrdering.None,
                Nodes,
                chunks,
                JobStatus.IN_PROGRESS
            );
        }

        public static JobObjectList ReadFailureChunk(Guid? nodeId, bool inCache)
        {
            return new JobObjectList(
                ChunkId1,
                1,
                nodeId,
                new[]
                { 
                    new JobObject("bar", 20, 0, inCache),
                }
            );
        }

        public static JobObjectList Chunk1(Guid? nodeId, bool firstInCache, bool secondInCache)
        {
            return new JobObjectList(
                ChunkId1,
                1,
                nodeId,
                new[]
                {
                    new JobObject("bar", 15, 0, firstInCache),
                    new JobObject("foo", 10, 10, secondInCache),
                }
            );
        }

        public static JobObjectList Chunk2(Guid? nodeId, bool firstInCache, bool secondInCache)
        {
            return new JobObjectList(
                ChunkId2,
                2,
                nodeId,
                new[]
                {
                    new JobObject("foo", 10, 0, firstInCache),
                    new JobObject("bar", 20, 15, secondInCache)
                }
            );
        }

        public static JobObjectList Chunk3(Guid? nodeId, bool firstInCache, bool secondInCache)
        {
            return new JobObjectList(
                ChunkId3,
                3,
                nodeId,
                new[]
                {
                    new JobObject("hello", 10, 0, firstInCache),
                    new JobObject("bar", 11, 35, secondInCache)
                }
            );
        }
    }
}
