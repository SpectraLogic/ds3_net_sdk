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
using System.Threading;
using Ds3.Helpers.Streams;
using NUnit.Framework;

namespace TestDs3.Helpers.Streams
{
    [TestFixture]
    public class TestResourceStore
    {
        private static int RunAccessesConcurrently(IEnumerable<string> keys)
        {
            var resourceStore = new ResourceStore<string, Disposable>(name => new Disposable(name));
            var lck = new object();
            var concurrentCallCount = 0;
            var maxCallCount = 0;
            var threads = new List<Thread>();
            foreach (var key in keys)
            {
                var thisKey = key;
                var thread = new Thread(() => resourceStore.Access(thisKey, item =>
                {
                    lock (lck)
                    {
                        concurrentCallCount++;
                        maxCallCount = Math.Max(maxCallCount, concurrentCallCount);
                    }
                    Thread.Sleep(250);
                    lock (lck)
                    {
                        concurrentCallCount--;
                    }
                }));
                thread.Start();
                threads.Add(thread);
            }
            foreach (var thread in threads)
            {
                thread.Join();
            }
            return maxCallCount;
        }

        private sealed class Disposable : IDisposable
        {
            public Disposable(string name)
            {
                Name = name;
            }

            public int DisposeCallCount { get; private set; }

            public string Name { get; }

            public void Dispose()
            {
                Dispose(true);
                GC.SuppressFinalize(this);
            }

            private void Dispose(bool disposing)
            {
                if (disposing)
                {
                    DisposeCallCount++;
                }
            }
        }

        [Test]
        public void CacheAllowsConcurrentAccessToDifferentObjects()
        {
            Assert.AreEqual(3, RunAccessesConcurrently(new[] {"foo", "bar", "baz"}));
            Assert.AreEqual(2, RunAccessesConcurrently(new[] {"foo", "foo", "baz"}));
        }

        [Test]
        public void CacheCallsFailWhenDisposed()
        {
            var resourceStore = new ResourceStore<string, Disposable>(name => new Disposable(name));
            resourceStore.Dispose();

            Assert.Throws<ObjectDisposedException>(() => resourceStore.Access("foo", it => { }));
            Assert.Throws<ObjectDisposedException>(() => resourceStore.Close("foo"));
        }

        [Test]
        public void CacheCanCloseBeforeGet()
        {
            var resourceStore = new ResourceStore<string, Disposable>(name => new Disposable(name));

            resourceStore.Close("foo");

            Assert.Throws<ObjectDisposedException>(() => resourceStore.Access("foo", it => { }));
        }

        [Test]
        public void CacheCanCloseItemsEarly()
        {
            var resourceStore = new ResourceStore<string, Disposable>(name => new Disposable(name));

            Disposable item1 = null;
            Disposable item2 = null;
            resourceStore.Access("foo", it => item1 = it);
            resourceStore.Access("bar", it => item2 = it);

            Assert.AreEqual(0, item1.DisposeCallCount);
            Assert.AreEqual(0, item2.DisposeCallCount);

            resourceStore.Close("foo");

            Assert.AreEqual(1, item1.DisposeCallCount);
            Assert.AreEqual(0, item2.DisposeCallCount);

            Assert.Throws<ObjectDisposedException>(() => resourceStore.Access("foo", it => { }));

            resourceStore.Dispose();

            Assert.AreEqual(1, item1.DisposeCallCount);
            Assert.AreEqual(1, item2.DisposeCallCount);
        }

        [Test]
        public void CacheDisposesAllItemsExactlyOnce()
        {
            var resourceStore = new ResourceStore<string, Disposable>(name => new Disposable(name));

            Disposable item1 = null;
            Disposable item2 = null;
            resourceStore.Access("foo", it => item1 = it);
            resourceStore.Access("bar", it => item2 = it);

            Assert.AreEqual(0, item1.DisposeCallCount);
            Assert.AreEqual(0, item2.DisposeCallCount);

            resourceStore.Dispose();

            Assert.AreEqual(1, item1.DisposeCallCount);
            Assert.AreEqual(1, item2.DisposeCallCount);

            resourceStore.Dispose();

            Assert.AreEqual(1, item1.DisposeCallCount);
            Assert.AreEqual(1, item2.DisposeCallCount);
        }

        [Test]
        public void CacheLocksConcurrentAccessToSameObject()
        {
            Assert.AreEqual(1, RunAccessesConcurrently(new[] {"foo", "foo", "foo"}));
        }

        [Test]
        public void CacheReturnsSameInstanceWhenCalledTwice()
        {
            var resourceStore = new ResourceStore<string, Disposable>(name => new Disposable(name));

            Disposable firstItem = null;
            resourceStore.Access("foo", item => firstItem = item);
            Assert.AreEqual("foo", firstItem.Name);

            Disposable secondItem = null;
            resourceStore.Access("foo", item => secondItem = item);
            Assert.AreSame(firstItem, secondItem);
        }
    }
}