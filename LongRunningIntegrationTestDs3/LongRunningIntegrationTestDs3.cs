﻿/*
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

using Ds3;
using Ds3.Calls;
using Ds3.Helpers;
using Ds3.Helpers.Strategies;
using Ds3.Models;
using IntegrationTestDS3;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Ds3.Runtime;
using System.Collections.Concurrent;
using System.Threading;

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

        /* global parameter for resume test */
        private int _filesTransfered = 0;
        private Guid _jobId;

        [OneTimeSetUp]
        public void Startup()
        {
            try
            {
                this._client = Ds3TestUtils.CreateClient(this._copyBufferSize);
                this._helpers = new Ds3ClientHelpers(this._client, retryAfter:5, jobWaitTime:1);

                var dataPolicyId = TempStorageUtil.SetupDataPolicy(FixtureName, false, ChecksumType.Type.MD5, _client);
                _envStorageIds = TempStorageUtil.Setup(FixtureName, dataPolicyId, _client);
            }
            catch (Exception)
            {
                // So long as any SetUp method runs without error, the TearDown method is guaranteed to run.
                // It will not run if a SetUp method fails or throws an exception.
                Teardown();
                throw;
            }
        }

        [OneTimeTearDown]
        public void Teardown()
        {
            TempStorageUtil.TearDown(FixtureName, _envStorageIds, _client);
        }

        [Test]
        public void PutLargeNumberOfObjects()
        {
            if (RuntimeUtils.IsRunningOnMono()) Assert.Ignore();

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

        [Test, TestCase(1), TestCase(2)]
        public void TestAggregationJob(int numberOfThreads)
        {
            string bucketName = $"TestAggregationJob{numberOfThreads}";
            const int numberOfObjects = 10000;

            try
            {
                _helpers.EnsureBucketExists(bucketName);

                const string content = "hi im content";
                var contentBytes = System.Text.Encoding.UTF8.GetBytes(content);
                var exceptionsThrown = new ConcurrentQueue<Exception>();

                var threads = new List<Thread>();
                for (var i = 0; i < numberOfThreads; i++)
                {
                    var thread = new Thread(AggregationTrasfer(bucketName, contentBytes, GetObjects(numberOfObjects, contentBytes.LongLength), exceptionsThrown, null));
                    thread.Start();
                    threads.Add(thread);
                }

                foreach (var thread in threads)
                {
                    thread.Join();
                }

                if (exceptionsThrown.Count == 1)
                {
                    throw exceptionsThrown.ElementAt(0);
                }

                if (exceptionsThrown.Count > 1)
                {
                    throw new AggregateException(exceptionsThrown);
                }

                Assert.AreEqual(numberOfObjects * numberOfThreads, _helpers.ListObjects(bucketName).Count());
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(_client, bucketName);
            }
        }

        public ThreadStart AggregationTrasfer(string bucketName, byte[] contentBytes, IEnumerable<Ds3Object> files, ConcurrentQueue<Exception> exceptionsThrown, CancellationTokenSource cancellationTokenSource)
        {
            return new ThreadStart(() =>
            {
                try
                {
                    var strategy = new WriteAggregateJobsHelperStrategy(files);

                    var job = _helpers.StartWriteJob(bucketName, files, helperStrategy: strategy);
                    if (cancellationTokenSource != null)
                    {
                        job.WithCancellationToken(cancellationTokenSource.Token);
                        job.ItemCompleted += _ => { _filesTransfered++; };
                        _jobId = job.JobId;
                    }

                    job.Transfer(key => new MemoryStream(contentBytes));
                }
                catch(OperationCanceledException)
                {
                    //pass
                }
                catch(AggregateException e)
                {
                    if (e.InnerExceptions.Any(inner => !(inner is OperationCanceledException)))
                    {
                        exceptionsThrown.Enqueue(e);
                    }
                }
                catch (Exception e)
                {
                    exceptionsThrown.Enqueue(e);
                }
            });
        }

        private static IList<Ds3Object> GetObjects(int numberOfObjects, long contentBytesLength)
        {
            var objects = new List<Ds3Object>();
            for (var i = 0; i < numberOfObjects; i++)
            {
                objects.Add(new Ds3Object(Guid.NewGuid().ToString(), contentBytesLength));
            }

            return objects;
        }

        [Test, TestCase(1), TestCase(2)]
        public void TestRecoverAggregatedWriteJob(int numberOfThreads)
        {
            string bucketName = $"TestRecoverAggregatedWriteJob_{numberOfThreads}";
            const int numberOfObjects = 10000;

            try
            {
                _helpers.EnsureBucketExists(bucketName);

                const string content = "hi im content";
                var contentBytes = System.Text.Encoding.UTF8.GetBytes(content);
                var exceptionsThrown = new ConcurrentQueue<Exception>();
                var cancellationTokenSource = new CancellationTokenSource();
                IEnumerable<Ds3Object> objects = null;

                var threads = new List<Thread>();
                for (var i = 0; i < numberOfThreads; i++)
                {
                    Thread thread;
                    if (i == 0)
                    {
                        objects = GetObjects(numberOfObjects, contentBytes.LongLength);
                        thread = new Thread(AggregationTrasfer(bucketName, contentBytes, objects, exceptionsThrown, cancellationTokenSource));
                    }
                    else
                    {
                        thread = new Thread(AggregationTrasfer(bucketName, contentBytes, GetObjects(numberOfObjects, contentBytes.LongLength), exceptionsThrown, null));
                    }

                    thread.Start();
                    threads.Add(thread);
                }

                //wait until we put at least half of the objects from job1
                SpinWait.SpinUntil(() => _filesTransfered > (numberOfObjects / 2));

                //cancel the first job
                cancellationTokenSource.Cancel();

                //wait for the threads to finish
                foreach (var thread in threads)
                {
                    thread.Join();
                }

                if (exceptionsThrown.Count == 1)
                {
                    throw exceptionsThrown.ElementAt(0);
                }

                if (exceptionsThrown.Count > 1)
                {
                    throw new AggregateException(exceptionsThrown);
                }

                Assert.Less(_filesTransfered, numberOfObjects);

                //Make sure the job is still active in order to resume it
                Assert.True(_client.GetActiveJobsSpectraS3(new GetActiveJobsSpectraS3Request()).ResponsePayload.ActiveJobs.Any(activeJob => activeJob.Id == _jobId));

                //resume the job
                var resumedJob = _helpers.RecoverAggregatedWriteJob(_jobId, objects);
                resumedJob.ItemCompleted += _ => { _filesTransfered++; };

                resumedJob.Transfer(key => new MemoryStream(contentBytes));

                Assert.AreEqual(numberOfObjects, _filesTransfered);
                Assert.AreEqual(numberOfObjects * numberOfThreads, _helpers.ListObjects(bucketName).Count());
            }
            finally
            {
                Ds3TestUtils.DeleteBucket(_client, bucketName);
                _filesTransfered = 0;
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

                // Test the PUT
                var putJob = _helpers.StartWriteJob(bucketName, directoryObjects, new Ds3WriteJobOptions { MaxUploadSize = blobSize }, new WriteStreamHelperStrategy());
                using (var fileStream = new ChecksumStream(streamLength, this._copyBufferSize.Value))
                {
                    var md5 = MD5.Create();
                    using (var md5Stream = new CryptoStream(fileStream, md5, CryptoStreamMode.Read))
                    {
                        putJob.Transfer(foo => md5Stream);

                        if (!md5Stream.HasFlushedFinalBlock)
                        {
                            md5Stream.FlushFinalBlock();
                        }

                        Assert.AreEqual("6pqugiiIUgxPkHfKKgq52A==", Convert.ToBase64String(md5.Hash));
                    }
                }

                // Test the GET
                var getJob = _helpers.StartReadAllJob(bucketName, helperStrategy: new ReadStreamHelperStrategy());
                using (Stream fileStream = new ChecksumStream(streamLength, this._copyBufferSize.Value))
                {
                    var md5 = MD5.Create();
                    using (var md5Stream = new CryptoStream(fileStream, md5, CryptoStreamMode.Write))
                    {
                        getJob.Transfer(foo => md5Stream);
                        if (!md5Stream.HasFlushedFinalBlock)
                        {
                            md5Stream.FlushFinalBlock();
                        }

                        Assert.AreEqual("6pqugiiIUgxPkHfKKgq52A==", Convert.ToBase64String(md5.Hash));
                    }
                }
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

                // Test the PUT
                var putJob = _helpers.StartWriteJob(bucketName, directoryObjects, new Ds3WriteJobOptions { MaxUploadSize = blobSize }, new WriteStreamHelperStrategy());

                var putCryptoStreams = new Dictionary<string, CryptoStream>();
                var putMd5s = new Dictionary<string, MD5>();

                directoryObjects.ForEach(obj =>
                {
                    var md5 = MD5.Create();
                    var fileStream = new ChecksumStream(streamLength, this._copyBufferSize.Value);
                    var md5Stream = new CryptoStream(fileStream, md5, CryptoStreamMode.Read);

                    putCryptoStreams.Add(obj.Name, md5Stream);
                    putMd5s.Add(obj.Name, md5);
                });

                putJob.Transfer(fileName => putCryptoStreams[fileName]);

                foreach (var stream in putCryptoStreams.Select(pair => pair.Value).Where(stream => !stream.HasFlushedFinalBlock))
                {
                    stream.FlushFinalBlock();
                }

                foreach (var md5 in putMd5s.Select(pair => pair.Value))
                {
                    Assert.AreEqual("Rt83cCvGZHQGu3eRIdfJIQ==", Convert.ToBase64String(md5.Hash));
                }

                // Test the GET
                var getJob = _helpers.StartReadAllJob(bucketName, helperStrategy: new ReadStreamHelperStrategy());

                var getCryptoStreams = new Dictionary<string, CryptoStream>();
                var getMd5s = new Dictionary<string, MD5>();

                directoryObjects.ForEach(obj =>
                {
                    var md5 = MD5.Create();
                    var fileStream = new ChecksumStream(streamLength, this._copyBufferSize.Value);
                    var md5Stream = new CryptoStream(fileStream, md5, CryptoStreamMode.Write);

                    getCryptoStreams.Add(obj.Name, md5Stream);
                    getMd5s.Add(obj.Name, md5);
                });

                getJob.Transfer(fileName => getCryptoStreams[fileName]);

                foreach (var stream in getCryptoStreams.Select(pair => pair.Value).Where(stream => !stream.HasFlushedFinalBlock))
                {
                    stream.FlushFinalBlock();
                }

                foreach (var md5 in getMd5s.Select(pair => pair.Value))
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
            //Do not write the files to disk when using in Tests
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
            get { return true; }
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