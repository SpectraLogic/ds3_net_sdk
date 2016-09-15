/*
 * ******************************************************************************
 *   Copyright 2014-2016 Spectra Logic Corporation. All Rights Reserved.
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

using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Ds3.Lang
{
    internal static class Grouping
    {
        public static IGrouping<TKey, TValue> ToGrouping<TKey, TValue>(
            this IEnumerable<TValue> self,
            TKey key)
        {
            return new GroupingImpl<TKey, TValue>(key, self);
        }

        private class GroupingImpl<TKey, TValue> : IGrouping<TKey, TValue>
        {
            private readonly IEnumerable<TValue> _values;

            public TKey Key { get; private set; }

            public GroupingImpl(TKey key, IEnumerable<TValue> values)
            {
                this.Key = key;
                this._values = values;
            }

            public IEnumerator<TValue> GetEnumerator()
            {
                return this._values.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }
        }
    }
}
