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

using Ds3;
using Ds3.Calls;
using Ds3.Models;
using Ds3.Runtime;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestDs3.Helpers
{
    internal static class MockHelpers
    {
        public static void SetupGetObjectWithContentLengthMismatchException(
             Mock<IDs3Client> client,
             string objectName,
             long offset,
             string payload,
             long expectedLength,
             long returnedLength,
             params Ds3.Models.Range[] byteRanges
         )
        {
            client
                .Setup(c => c.GetObject(ItIsGetObjectRequest(
                    JobResponseStubs.BucketName,
                    objectName,
                    JobResponseStubs.JobId,
                    offset,
                    byteRanges
                    )))
                .Callback<GetObjectRequest>(r =>
                {
                    Console.WriteLine("Writing \"" + payload + "\" to stream");
                    WriteToStream(r.DestinationStream, payload);
                })
                .Throws(new Ds3ContentLengthNotMatch("Content Length mismatch", expectedLength, returnedLength));
        }

        internal static readonly Encoding Encoding = new UTF8Encoding(false);

        private static void WriteToStream(Stream stream, string value)
        {
            var buffer = Encoding.GetBytes(value);
            stream.Write(buffer, 0, buffer.Length);
        }

        public static BulkGetRequest ItIsBulkGetRequest(
            string bucketName,
            ChunkOrdering chunkOrdering,
            IEnumerable<string> fullObjects,
            IEnumerable<Ds3PartialObject> partialObjects)
        {
            return Match.Create(
                r =>
                    r.BucketName == bucketName
                    && r.ChunkOrder == chunkOrdering
                    && r.FullObjects.Sorted().SequenceEqual(fullObjects.Sorted())
                    && r.PartialObjects.Sorted().SequenceEqual(partialObjects.Sorted()),
                () => new BulkGetRequest(bucketName, fullObjects, partialObjects)
                    .WithChunkOrdering(chunkOrdering)
                );
        }

        public static BulkPutRequest ItIsBulkPutRequest(string bucketName, IEnumerable<Ds3Object> objects, long? maxBlobSize)
        {
            return Match.Create(
                r =>
                    r.BucketName == bucketName
                    && r.MaxBlobSize == maxBlobSize
                    && r.Objects.Select(o => new { o.Name, o.Size })
                        .SequenceEqual(objects.Select(o => new { o.Name, o.Size })),
                () => new BulkPutRequest(bucketName, objects.ToList())
                );
        }

        public static GetAvailableJobChunksRequest ItIsGetAvailableJobChunksRequest(Guid jobId)
        {
            return Match.Create(
                it => it.JobId == jobId,
                () => new GetAvailableJobChunksRequest(jobId)
                );
        }

        public static AllocateJobChunkRequest ItIsAllocateRequest(Guid chunkId)
        {
            return Match.Create(
                it => it.ChunkId == chunkId,
                () => new AllocateJobChunkRequest(chunkId)
                );
        }

        public static GetObjectRequest ItIsGetObjectRequest(
            string bucketName,
            string objectName,
            Guid jobId,
            long offset,
            IEnumerable<Ds3.Models.Range> byteRanges)
        {
            return Match.Create(
                it =>
                    it.BucketName == bucketName
                    && it.ObjectName == objectName
                    && it.JobId == jobId
                    && it.Offset == offset
                    && it.GetByteRanges().SequenceEqual(byteRanges),
                () => new GetObjectRequest(
                    bucketName,
                    objectName,
                    jobId,
                    offset,
                    It.IsAny<Stream>()
                    )
                );
        }

        private static PutObjectRequest ItIsPutObjectRequest(
            string bucketName,
            string objectName,
            Guid jobId,
            long offset)
        {
            return Match.Create(
                it =>
                    it.BucketName == bucketName
                    && it.ObjectName == objectName
                    && it.JobId == jobId
                    && it.Offset == offset,
                () => new PutObjectRequest(
                    bucketName,
                    objectName,
                    jobId,
                    offset,
                    It.IsAny<Stream>()
                    )
                );
        }

        public static void SetupGetObject(
            Mock<IDs3Client> client,
            string objectName,
            long offset,
            string payload,
            params Ds3.Models.Range[] byteRanges
            )
        {
            client
                .Setup(c => c.GetObject(ItIsGetObjectRequest(
                    JobResponseStubs.BucketName,
                    objectName,
                    JobResponseStubs.JobId,
                    offset,
                    byteRanges
                    )))
                .Returns(new GetObjectResponse(new Dictionary<string, string>()))
                .Callback<GetObjectRequest>(r =>
                {
                    Console.WriteLine("Writing \"" + payload + "\" to stream");
                    WriteToStream(r.DestinationStream, payload);
                });
        }

        public static void SetupPutObject(
            Mock<IDs3Client> client,
            string objectName,
            long offset,
            string expectedData)
        {
            client
                .Setup(c => c.PutObject(ItIsPutObjectRequest(
                    JobResponseStubs.BucketName,
                    objectName,
                    JobResponseStubs.JobId,
                    offset
                    )))
                .Callback<PutObjectRequest>(r =>
                {
                    using (var stream = r.GetContentStream())
                    using (var reader = new StreamReader(stream, Encoding))
                    {
                        Assert.AreEqual(expectedData, reader.ReadToEnd());
                    }
                });
        }

        public static void CheckContents(Ds3Object contents, string key, long size)
        {
            Assert.AreEqual(key, contents.Name);
            Assert.AreEqual(size, contents.Size);
        }

        public static GetBucketResponse CreateGetBucketResponse(string marker, bool isTruncated, string nextMarker, IEnumerable<Ds3ObjectInfo> ds3objectInfos)
        {
            return new GetBucketResponse(
                name: "mybucket",
                prefix: "",
                marker: marker,
                maxKeys: 2,
                isTruncated: isTruncated,
                delimiter: "",
                nextMarker: nextMarker,
                creationDate: DateTime.Now,
                objects: ds3objectInfos,
                metadata: new Dictionary<string, string>(),
                commonPrefixes: Enumerable.Empty<string>()
                );
        }

        public static Ds3ObjectInfo BuildDs3Object(string key, string eTag, string lastModified, long size)
        {
            var owner = new Owner("person@spectralogic.com", "75aa57f09aa0c8caeab4f8c24e99d10f8e7faeebf76c078efc7c6caea54ba06a");
            return new Ds3ObjectInfo(key, size, owner, eTag, "STANDARD", DateTime.Parse(lastModified));
        }
    }
}