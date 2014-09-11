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
using Ds3.Helpers;
using Ds3.Models;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Concurrent;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace TestDs3
{
    [TestFixture]
    public class TestJob
    {
        private static readonly JobResponse _bulkResponse = new JobResponse(
            "bucket",
            Guid.Parse("879e42ba-77fb-41fc-84c0-511acc90912b"),
            "HIGH",
            "GET",
            DateTime.Parse("9/8/2014 9:25:56 PM"),
            Enumerable.Empty<Node>(),
            Enumerable.Empty<JobObjectList>()
        );

        [Test]
        public void WithMaxParallelRequestsControlsConcurrency()
        {
            var testJob = new TestJobImpl(new Mock<IDs3Client>(MockBehavior.Strict).Object, _bulkResponse);
            testJob.WithMaxParallelRequests(5);
            testJob.Transfer(key => Stream.Null);
            Assert.AreEqual(5, testJob.MaxThreadsRun);
        }

        [Test]
        public async Task WithCancellationTokenAllowsCancellation()
        {
            var testJob = new TestJobImpl(new Mock<IDs3Client>(MockBehavior.Strict).Object, _bulkResponse);

            var cts = new CancellationTokenSource();
            testJob.WithCancellationToken(cts.Token);

            var result = Task.Run(() => testJob.Transfer(key => Stream.Null));
            Thread.Sleep(10);

            cts.Cancel();
            Thread.Sleep(20);

            try
            {
                await result;
                Assert.Fail();
            }
            catch (OperationCanceledException)
            {
            }
        }

        private class TestJobImpl : Job
        {
            public int MaxThreadsRun { get; private set; }

            public TestJobImpl(IDs3Client client, JobResponse bulkResponse)
                : base(client, bulkResponse)
            {
            }

            public override void Transfer(Func<string, Stream> createStreamForObjectKey)
            {
                var elementCount = 0;
                var counts = new ConcurrentQueue<int>();
                InParallel(Enumerable.Repeat(50, 50), milliseconds =>
                {
                    this._cancellationToken.ThrowIfCancellationRequested();
                    counts.Enqueue(Interlocked.Increment(ref elementCount));
                    Thread.Sleep(10);
                    this._cancellationToken.ThrowIfCancellationRequested();
                    Thread.Sleep(10);
                    this._cancellationToken.ThrowIfCancellationRequested();
                    Thread.Sleep(10);
                    this._cancellationToken.ThrowIfCancellationRequested();
                    Thread.Sleep(10);
                    this._cancellationToken.ThrowIfCancellationRequested();
                    Thread.Sleep(10);
                    Interlocked.Decrement(ref elementCount);
                });
                MaxThreadsRun = counts.Max();
            }
        }
    }
}
