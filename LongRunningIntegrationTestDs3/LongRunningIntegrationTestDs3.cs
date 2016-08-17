using Ds3;
using Ds3.Calls;
using Ds3.Helpers;
using Ds3.Helpers.Strategys;
using Ds3.Models;
using IntegrationTestDS3;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace LongRunningIntegrationTestDs3
{
    [TestFixture]
    public class LongRunningIntegrationTestDs3
    {
        private IDs3Client _client;
        private Ds3ClientHelpers _helpers;
        private readonly int? _copyBufferSize = 16 * 1024 * 1024;
        private const string FixtureName = "long_integration_test_ds3client";
        private static TempStorageIds _envStorageIds;

        [OneTimeSetUp]
        public void Startup()
        {
            this._client = Ds3TestUtils.CreateClient(this._copyBufferSize);
            this._helpers = new Ds3ClientHelpers(this._client);

            var dataPolicyId = TempStorageUtil.SetupDataPolicy(FixtureName, false, ChecksumType.Type.MD5, _client);
            _envStorageIds = TempStorageUtil.Setup(FixtureName, dataPolicyId, _client);
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            TempStorageUtil.TearDown(FixtureName, _envStorageIds, _client);
        }

        [Test]
        public void PutLargeNumberOfObjects()
        {
            const string bucketName = "PutLargeNumberOfObjects";
            const int numberOfObjects = 10000;

            try
            {
                const string content = "hi im content";
                var contentBytes = System.Text.Encoding.UTF8.GetBytes(content);

                var objects = new List<Ds3Object>();

                for (var i = 0; i < numberOfObjects; i++)
                {
                    objects.Add(new Ds3Object(Guid.NewGuid().ToString(), contentBytes.Length));
                }

                Ds3TestUtils.PutFiles(_client, bucketName, objects, key => new MemoryStream(contentBytes));

                Assert.AreEqual(numberOfObjects, _client.GetBucket(new GetBucketRequest(bucketName).WithMaxKeys(numberOfObjects)).ResponsePayload.Objects.Count());
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(_client, bucketName);
            }
        }

        [Test]
        public void TestChecksumStreamingWithMultiChunks()
        {
            const string bucketName = "TestChecksumStreamingWithMultiChunks";

            try
            {
                // Creates a bucket if it does not already exist.
                _helpers.EnsureBucketExists(bucketName);

                const long streamLength = 150L * 1024L * 1024L * 1024L; //that way we enforce that we get 2 chunks
                var directoryObjects = new List<Ds3Object> { new Ds3Object("bigFile", streamLength) };

                const long blobSize = 1L * 1024L * 1024L * 1024L;
                var job = _helpers.StartWriteJob(bucketName, directoryObjects, blobSize, new WriteStreamHelperStrategy());

                var md5 = MD5.Create();
                var fileStream = new ChecksumStream(streamLength, this._copyBufferSize.Value);
                var md5Stream = new CryptoStream(fileStream, md5, CryptoStreamMode.Read);

                job.Transfer(foo => md5Stream);

                if (!md5Stream.HasFlushedFinalBlock)
                {
                    md5Stream.FlushFinalBlock();
                }

                Assert.AreEqual("6pqugiiIUgxPkHfKKgq52A==", Convert.ToBase64String(md5.Hash));
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(_client, bucketName);
            }
        }

        [Test]
        public void TestChecksumStreamingWithMultiStreams()
        {
            const string bucketName = "TestChecksumStreamingWithMultiStreams";

            try
            {
                // Creates a bucket if it does not already exist.
                _helpers.EnsureBucketExists(bucketName);

                /* using 1GB file with 100MB blobs size so each stream will have 1 chunk with 10 blobs */
                const long streamLength = 1L * 1024L * 1024L * 1024L;
                const long blobSize = 100L * 1024L * 1024L;

                var directoryObjects = new List<Ds3Object>
                {
                    new Ds3Object("bigFile1", streamLength),
                    new Ds3Object("bigFile2", streamLength),
                    new Ds3Object("bigFile3", streamLength)
                };

                
                var job = _helpers.StartWriteJob(bucketName, directoryObjects, blobSize, new WriteStreamHelperStrategy());

                var cryptoStreams = new Dictionary<string, CryptoStream>();
                var md5s = new Dictionary<string, MD5>();

                directoryObjects.ForEach(obj =>
                {
                    var md5 = MD5.Create();
                    var fileStream = new ChecksumStream(streamLength, this._copyBufferSize.Value);
                    var md5Stream = new CryptoStream(fileStream, md5, CryptoStreamMode.Read);

                    cryptoStreams.Add(obj.Name, md5Stream);
                    md5s.Add(obj.Name, md5);
                });

                job.Transfer(fileName => cryptoStreams[fileName]);

                foreach (var stream in cryptoStreams.Select(pair => pair.Value).Where(stream => !stream.HasFlushedFinalBlock))
                {
                    stream.FlushFinalBlock();
                }

                foreach (var md5 in md5s.Select(pair => pair.Value))
                {
                    Assert.AreEqual("Rt83cCvGZHQGu3eRIdfJIQ==", Convert.ToBase64String(md5.Hash));
                }

            }
            finally
            {
                Ds3TestUtils.DeleteBucket(_client, bucketName);
            }
        }

    }

    internal class ChecksumStream : Stream
    {
        private readonly long _streamLength;
        private readonly byte[] _buffer;
        private long _totalBytesRead;
        private readonly Random _random = new Random(5);

        public ChecksumStream(long streamLength, int copyBufferSize)
        {
            this._streamLength = streamLength;

            this._buffer = new byte[copyBufferSize];
            this._random.NextBytes(this._buffer);
        }

        public override void Flush()
        {
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            throw new NotSupportedException();
        }

        public override void SetLength(long value)
        {
            throw new NotSupportedException();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            if (this._totalBytesRead == this._streamLength)
            {
                return 0;
            }

            if (this._totalBytesRead + count > this._streamLength)
            {
                count = (int)(this._streamLength - _totalBytesRead);
            }

            for (var i = 0; i < count; i++)
            {
                buffer[i + offset] = this._buffer[i];
            }

            _totalBytesRead += count;
            return count;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            throw new NotSupportedException();
        }

        public override bool CanRead
        {
            get { return true; }
        }

        public override bool CanSeek
        {
            get { return false; }
        }

        public override bool CanWrite
        {
            get { return false; }
        }

        public override long Length
        {
            get { return this._streamLength; }
        }

        public override long Position
        {
            get
            {
                throw new NotSupportedException();
            }
            set
            {
                throw new NotSupportedException();
            }
        }
    }
}