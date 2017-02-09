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

using System.Collections.Generic;

namespace Ds3.Lang
{
    internal class ThreadSafeEnumerator<T>
    {
        private readonly object _lock = new object();
        private readonly IEnumerator<T> _items;

        public ThreadSafeEnumerator(IEnumerator<T> items)
        {
            this._items = items;
        }

        public bool TryGetNext(ref T it)
        {
            lock (this._lock)
            {
                if (this._items.MoveNext())
                {
                    it = this._items.Current;
                    return true;
                }

                return false;
            }
        }
    }
}