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

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace TestDs3
{
    internal static class HelpersForTest
    {
        internal static IEnumerable<T> Sorted<T>(this IEnumerable<T> self)
        {
            return self.OrderBy(it => it);
        }

        internal static string StringFromStream(Stream stream)
        {
            using (stream)
            {
                stream.Position = 0L;
                using (var reader = new StreamReader(stream))
                {
                    return reader.ReadToEnd();
                }
            }
        }

        internal static Stream StringToStream(string responseString)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(responseString));
        }

        internal static void AssertCollectionsEqual<T1, T2>(IEnumerable<T1> expected, IEnumerable<T2> actual,
            Action<T1, T2> assertion)
        {
            var expectedList = expected.ToList();
            var actualList = actual.ToList();
            Assert.AreEqual(expectedList.Count, actualList.Count);
            for (var i = 0; i < expectedList.Count; i++)
            {
                assertion(expectedList[i], actualList[i]);
            }
        }
    }
}