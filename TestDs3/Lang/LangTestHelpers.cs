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
using System.Linq;

namespace TestDs3.Lang
{
    internal static class LangTestHelpers
    {
        public static IEnumerable<string> RandomStrings(int stringCount, int stringSizes)
        {
            var chars = Enumerable.Range(0, 26).Select(i => (char) ('a' + i)).ToArray();
            return RandomStrings(new Random(), stringCount, stringSizes, chars).ToArray();
        }

        private static IEnumerable<string> RandomStrings(Random rand, int stringCount, int stringSizes, char[] chars)
        {
            var buffer = new char[stringSizes];
            for (var i = 0; i < stringCount; i++)
            {
                PopulateWithRandom(rand, buffer, chars);
                yield return new string(buffer);
            }
        }

        private static void PopulateWithRandom<T>(Random rand, IList<T> dest, IReadOnlyList<T> allowed)
        {
            for (var i = 0; i < dest.Count; i++)
            {
                dest[i] = allowed[rand.Next(allowed.Count)];
            }
        }
    }
}