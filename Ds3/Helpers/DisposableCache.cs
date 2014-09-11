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

using System;
using System.Collections.Generic;

namespace Ds3.Helpers
{
    internal class DisposableCache<TKey, TDispoable> : IDisposable
        where TDispoable : IDisposable
    {
        private volatile bool _disposed = false;
        private readonly object _lock = new object();
        private readonly Func<TKey, TDispoable> _makeResource;
        private readonly IDictionary<TKey, TDispoable> _values = new Dictionary<TKey, TDispoable>();
        private readonly ISet<TKey> _disposedKeys = new HashSet<TKey>();

        public DisposableCache(Func<TKey, TDispoable> makeResource)
        {
            this._makeResource = makeResource;
        }
        
        public TDispoable Get(TKey key)
        {
            if (this._disposed)
            {
                throw new ObjectDisposedException(this.ToString());
            }
            lock (this._lock)
            {
                TDispoable value;
                if (this._values.TryGetValue(key, out value))
                {
                    return value;
                }
                else if (this._disposedKeys.Contains(key))
                {
                    throw new ObjectDisposedException(key.ToString());
                }
                else
                {
                    value = this._makeResource(key);
                    this._values[key] = value;
                    return value;
                }
            }
        }

        public void Close(TKey key)
        {
            if (this._disposed)
            {
                throw new ObjectDisposedException(this.ToString());
            }
            lock (this._lock)
            {
                TDispoable value;
                if (this._values.TryGetValue(key, out value))
                {
                    this._values.Remove(key);
                    value.Dispose();
                }
                this._disposedKeys.Add(key);
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (this._disposed)
            {
                return;
            }

            if (disposing)
            {
                lock (this._lock)
                {
                    foreach (var item in this._values)
                    {
                        this._disposedKeys.Add(item.Key);
                        item.Value.Dispose();
                    }
                    this._values.Clear();
                }
            }

            this._disposed = true;
        }
    }
}
