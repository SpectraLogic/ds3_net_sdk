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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Parallel = Ds3.Lang.Parallel;

namespace TestDs3.Lang
{
    [TestFixture]
    public class TestParallel
    {
        private static IEnumerable<int> Infinite()
        {
            var i = 0;
            while (true)
            {
                yield return i++;
            }
        }

        [Test]
        [Timeout(1000)]
        public void ForEachIteratesItemsInParallel()
        {
            var strings = LangTestHelpers.RandomStrings(100, 50);
            var results = new ConcurrentQueue<Tuple<int, string>>();
            var threadCount = 10;
            Parallel.ForEach(
                threadCount,
                CancellationToken.None,
                strings,
                it =>
                {
                    Thread.Sleep(10);
                    results.Enqueue(Tuple.Create(Environment.CurrentManagedThreadId, it));
                }
            );
            Assert.AreEqual(threadCount, results.Select(it => it.Item1).Distinct().Count());
            CollectionAssert.AreEquivalent(strings, results.Select(it => it.Item2));
        }

        [Test]
        [Timeout(1000)]
        public void ForEachObeysCancellationToken()
        {
            var cancellationTokenSource = new CancellationTokenSource();
            Task.Run(() =>
            {
                Thread.Sleep(50);
                cancellationTokenSource.Cancel();
            });
            var itemsConsumed = new ConcurrentQueue<int>();
            var itemsFinished = new ConcurrentQueue<int>();
            Assert.Throws<OperationCanceledException>(() => Parallel.ForEach(
                10,
                cancellationTokenSource.Token,
                Enumerable.Range(0, 1000).Select(it =>
                {
                    itemsConsumed.Enqueue(it);
                    return it;
                }),
                it =>
                {
                    Thread.Sleep(5);
                    itemsFinished.Enqueue(it);
                }
            ));
            CollectionAssert.AreEquivalent(itemsConsumed, itemsFinished);
            Assert.GreaterOrEqual(itemsConsumed.Count, 10);
            Assert.LessOrEqual(itemsConsumed.Count, 100);
        }

        [Test]
        [Timeout(1000)]
        public void ForEachTerminatesUponFailure()
        {
            const int threadCount = 12;
            var itemsReturned = 0;
            try
            {
                var throwExceptions = new CountdownEvent(threadCount);
                var threadsRun = new ConcurrentDictionary<int, int>();
                Parallel.ForEach(
                    threadCount,
                    CancellationToken.None,
                    Infinite().Select(it =>
                    {
                        itemsReturned = it;
                        return it;
                    }),
                    i =>
                    {
                        threadsRun.AddOrUpdate(
                            Thread.CurrentThread.ManagedThreadId,
                            k =>
                            {
                                throwExceptions.Signal();
                                return 1;
                            },
                            (k, v) => 1
                        );
                        Thread.Sleep(1);
                        if (throwExceptions.IsSet)
                        {
                            throw new InvalidOperationException("test exception");
                        }
                    }
                );
                Assert.Fail("Should have thrown an AggregateException");
            }
            catch (AggregateException e)
            {
                Assert.AreEqual(threadCount, e.InnerExceptions.Count);
                foreach (var inner in e.InnerExceptions)
                {
                    Assert.IsInstanceOf<InvalidOperationException>(inner);
                }
            }
        }
    }
}