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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Ds3;
using Ds3.Calls;
using Ds3.Models;
using Ds3.Runtime;
using Moq;
using NUnit.Framework;

namespace TestDs3.Helpers
{
    internal static class MockHelpers
    {
        internal static readonly Encoding Encoding = new UTF8Encoding(false);

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
                .Callback<GetObjectRequest>(r => { WriteToStream(r.DestinationStream, payload); })
                .Throws(new Ds3ContentLengthNotMatch("Content Length mismatch", expectedLength, returnedLength));
        }

        private static void WriteToStream(Stream stream, string value)
        {
            var buffer = Encoding.GetBytes(value);
            stream.Write(buffer, 0, buffer.Length);
        }

        public static GetBulkJobSpectraS3Request ItIsBulkGetRequest(
            string bucketName,
            JobChunkClientProcessingOrderGuarantee? chunkOrdering,
            IEnumerable<Ds3Object> fullObjects,
            IEnumerable<Ds3PartialObject> partialObjects)
        {
            return Match.Create(
                r =>
                    r.BucketName == bucketName
                    && r.ChunkClientProcessingOrderGuarantee == chunkOrdering
                    && r.FullObjects.Sorted().SequenceEqual(fullObjects.Sorted())
                    && r.PartialObjects.Sorted().SequenceEqual(partialObjects.Sorted()),
                () => new GetBulkJobSpectraS3Request(bucketName, fullObjects, partialObjects)
                    .WithChunkClientProcessingOrderGuarantee(chunkOrdering.Value)
            );
        }

        public static PutBulkJobSpectraS3Request ItIsBulkPutRequest(string bucketName, IEnumerable<Ds3Object> objects,
            long? maxBlobSize)
        {
            return Match.Create(
                r =>
                    r.BucketName == bucketName
                    && r.MaxUploadSize == maxBlobSize
                    && r.Objects.Select(o => new {o.Name, o.Size})
                        .SequenceEqual(objects.Select(o => new {o.Name, o.Size})),
                () => new PutBulkJobSpectraS3Request(bucketName, objects.ToList())
            );
        }

        public static GetJobChunksReadyForClientProcessingSpectraS3Request ItIsGetAvailableJobChunksRequest(Guid jobId)
        {
            return Match.Create(
                it => it.Job == jobId.ToString(),
                () => new GetJobChunksReadyForClientProcessingSpectraS3Request(jobId)
            );
        }

        public static AllocateJobChunkSpectraS3Request ItIsAllocateRequest(Guid chunkId)
        {
            return Match.Create(
                it => it.JobChunkId == chunkId.ToString(),
                () => new AllocateJobChunkSpectraS3Request(chunkId)
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
                    && it.Job == jobId.ToString()
                    && it.Offset == offset
                    && it.GetByteRanges().SequenceEqual(byteRanges),
                () => new GetObjectRequest(
                    bucketName,
                    objectName,
                    It.IsAny<Stream>(),
                    jobId,
                    offset
                )
            );
        }

        public static PutObjectRequest ItIsPutObjectRequest(
            string bucketName,
            string objectName,
            Guid jobId,
            long offset)
        {
            return Match.Create(
                it =>
                    it.BucketName == bucketName
                    && it.ObjectName == objectName
                    && it.Job == jobId.ToString()
                    && it.Offset == offset,
                () => new PutObjectRequest(
                        bucketName,
                        objectName,
                        It.IsAny<Stream>()
                    )
                    .WithJob(jobId)
                    .WithOffset(offset)
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
                .Callback<GetObjectRequest>(r => { WriteToStream(r.DestinationStream, payload); });
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

        public static GetBucketResponse CreateGetBucketResponse(string marker, bool isTruncated, string nextMarker,
            IEnumerable<Contents> ds3ObjectInfos)
        {
            return new GetBucketResponse(
                new ListBucketResult()
                {
                    Name = "mybucket",
                    Prefix = "",
                    Marker = marker,
                    MaxKeys = 2,
                    Truncated = isTruncated,
                    Delimiter = "",
                    NextMarker = nextMarker,
                    CreationDate = DateTime.Now,
                    Objects = ds3ObjectInfos,
                    CommonPrefixes = Enumerable.Empty<string>(),
                    //Metadata = new Dictionary<string, string>()
                }
            );
        }

        public static Contents BuildDs3Object(string key, string eTag, string lastModified, long size)
        {
            var owner = new User
            {
                DisplayName = "person@spectralogic.com",
                Id = Guid.Parse("75aa57f0-9aa0-c8ca-eab4-f8c24e99d10f")
            };
            //"75aa57f09aa0c8caeab4f8c24e99d10f8e7faeebf76c078efc7c6caea54ba06a"

            var contents = new Contents
            {
                Key = key,
                Size = size,
                Owner = owner,
                ETag = eTag,
                StorageClass = "STANDARD",
                LastModified = DateTime.Parse(lastModified)
            };
            return contents;
        }
    }
}