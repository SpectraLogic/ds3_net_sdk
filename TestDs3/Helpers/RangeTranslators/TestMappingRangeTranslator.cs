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

using Ds3.Helpers.RangeTranslators;
using Ds3.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace TestDs3.Helpers.RangeTranslators
{
    [TestFixture]
    public class TestMappingRangeTranslator
    {
        [Test]
        public void MappingTranslateWorks()
        {
            var translator = new MappingRangeTranslator<string, string>(
                new KeyValuePairList<string, RangeMapping<string>>
                {
                    { "foo", new RangeMapping<string>(Range.ByLength(0L, 100L), 1000L, "foo1") },
                    { "foo", new RangeMapping<string>(Range.ByLength(100L, 100L), 2000L, "foo1") },
                    { "foo", new RangeMapping<string>(Range.ByLength(200L, 100L), 3000L, "foo1") },
                    { "bar", new RangeMapping<string>(Range.ByLength(200L, 100L), 4000L, "bar1") },
                    { "foo", new RangeMapping<string>(Range.ByLength(300L, 100L), 0L, "foo2") }
                }.ToLookup(kvp => kvp.Key, kvp => kvp.Value)
            );
            var result = translator.Translate(ContextRange.Create(Range.ByLength(50L, 300L), "foo"));
            CollectionAssert.AreEqual(
                new[]
                {
                    ByPosition(1050L, 1099L, "foo1"),
                    ByPosition(2000L, 2099, "foo1"),
                    ByPosition(3000, 3099L, "foo1"),
                    ByPosition(0L, 49L, "foo2"),
                },
                result
            );
        }

        private class KeyValuePairList<TKey, TValue> : List<KeyValuePair<TKey, TValue>>
        {
            public void Add(TKey key, TValue value)
            {
                this.Add(new KeyValuePair<TKey, TValue>(key, value));
            }

            public ILookup<TKey, TValue> ToLookup()
            {
                return this.ToLookup(kvp => kvp.Key, kvp => kvp.Value);
            }
        }

        private static ContextRange<string> ByPosition(long start, long end, string context)
        {
            return ContextRange.Create(Range.ByPosition(start, end), context);
        }
    }
}
