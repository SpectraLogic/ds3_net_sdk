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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ds3.Lang
{
    internal static class Lookup
    {
        public static ILookup<TKey, TValue> ToLookup<TKey, TValue>(this IEnumerable<IGrouping<TKey, TValue>> items)
        {
            return new LookupImpl<TKey, TValue>(items);
        }

        private class LookupImpl<TKey, TValue> : ILookup<TKey, TValue>
        {
            private readonly Dictionary<TKey, IGrouping<TKey, TValue>> _dictionary;

            public LookupImpl(IEnumerable<IGrouping<TKey, TValue>> items)
            {
                this._dictionary = items.ToDictionary(i => i.Key);
            }

            public bool Contains(TKey key)
            {
                return this._dictionary.ContainsKey(key);
            }

            public int Count
            {
                get { return this._dictionary.Count; }
            }

            public IEnumerable<TValue> this[TKey key]
            {
                get { return this._dictionary[key]; }
            }

            public IEnumerator<IGrouping<TKey, TValue>> GetEnumerator()
            {
                return this._dictionary.Values.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }
    }
}
