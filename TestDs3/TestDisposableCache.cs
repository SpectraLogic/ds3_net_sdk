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

using Ds3.Helpers;
using NUnit.Framework;
using System;

namespace TestDs3
{
    [TestFixture]
    public class TestDisposableCache
    {
        [Test]
        public void CacheReturnsSameInstanceWhenCalledTwice()
        {
            var disposableCache = new DisposableCache<string, Disposable>(name => new Disposable(name));
            var item = disposableCache.Get("foo");
            Assert.AreEqual("foo", item.Name);
            Assert.AreEqual(item, disposableCache.Get("foo"));
        }

        [Test]
        public void CacheDisposesAllItemsExactlyOnce()
        {
            var disposableCache = new DisposableCache<string, Disposable>(name => new Disposable(name));

            var item1 = disposableCache.Get("foo");
            var item2 = disposableCache.Get("bar");

            Assert.AreEqual(0, item1.DisposeCallCount);
            Assert.AreEqual(0, item2.DisposeCallCount);

            disposableCache.Dispose();

            Assert.AreEqual(1, item1.DisposeCallCount);
            Assert.AreEqual(1, item2.DisposeCallCount);

            disposableCache.Dispose();

            Assert.AreEqual(1, item1.DisposeCallCount);
            Assert.AreEqual(1, item2.DisposeCallCount);
        }

        [Test]
        public void CacheCanCloseItemsEarly()
        {
            var disposableCache = new DisposableCache<string, Disposable>(name => new Disposable(name));

            var item1 = disposableCache.Get("foo");
            var item2 = disposableCache.Get("bar");

            Assert.AreEqual(0, item1.DisposeCallCount);
            Assert.AreEqual(0, item2.DisposeCallCount);

            disposableCache.Close("foo");

            Assert.AreEqual(1, item1.DisposeCallCount);
            Assert.AreEqual(0, item2.DisposeCallCount);

            Assert.Throws<ObjectDisposedException>(() => disposableCache.Get("foo"));

            disposableCache.Dispose();

            Assert.AreEqual(1, item1.DisposeCallCount);
            Assert.AreEqual(1, item2.DisposeCallCount);
        }

        [Test]
        public void CacheCanCloseBeforeGet()
        {
            var disposableCache = new DisposableCache<string, Disposable>(name => new Disposable(name));

            disposableCache.Close("foo");

            Assert.Throws<ObjectDisposedException>(() => disposableCache.Get("foo"));
        }

        [Test]
        public void CacheCallsFailWhenDisposed()
        {
            var disposableCache = new DisposableCache<string, Disposable>(name => new Disposable(name));
            disposableCache.Dispose();

            Assert.Throws<ObjectDisposedException>(() => disposableCache.Get("foo"));
            Assert.Throws<ObjectDisposedException>(() => disposableCache.Close("foo"));
        }

        private class Disposable : IDisposable
        {
            private int _disposeCallCount = 0;
            public int DisposeCallCount
            {
                get { return this._disposeCallCount; }
            }

            public string Name { get; private set; }

            public Disposable(string name)
            {
                this.Name = name;
            }

            public void Dispose()
            { 
                Dispose(true);
                GC.SuppressFinalize(this);           
            }

            protected virtual void Dispose(bool disposing)
            {
                if (disposing)
                {
                    this._disposeCallCount++;
                }
            }
        }
    }
}
