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
using System.Linq;

namespace TestDs3.Helpers.RangeTranslators
{
    [TestFixture]
    public class TestObjectToPartRangeTranslator
    {
        [Test]
        public void ObjectToPartTranslateWorks()
        {
            var po1 = new Ds3PartialObject(Range.ByLength(100L, 200L), "foobar");
            var po2 = new Ds3PartialObject(Range.ByLength(300L, 100L), "foobar");
            var po3 = new Ds3PartialObject(Range.ByLength(10000L, 123L), "foobar");
            var translator = new ObjectToPartRangeTranslator(new[] { po1, po2, po3 });
            var result = translator.Translate(ContextRange.Create(Range.ByLength(110L, 250L), "foobar")).ToList();
            CollectionAssert.AreEqual(
                new[]
                {
                    ContextRange.Create(Range.ByLength(10L, 190L), po1),
                    ContextRange.Create(Range.ByLength(0L, 60L), po2),
                },
                result
            );
        }
    }
} 
