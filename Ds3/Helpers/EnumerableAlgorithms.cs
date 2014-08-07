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
using System.Linq;

namespace Ds3.Helpers
{
    internal static class EnumerableAlgorithms
    {
        /// <summary>
        /// For each list of strings in the input, returns all of the strings
        /// that have not appeared in any previous list of strings.
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<string>> FirstMentionsPerRow(this IEnumerable<IEnumerable<string>> rows)
        {
            var mentioned = new HashSet<string>();
            foreach (var row in rows)
            {
                yield return row
                    .Where(name => !mentioned.Contains(name))
                    .Select(name =>
                    {
                        mentioned.Add(name);
                        return name;
                    })
                    .ToList();
            }
        }

        /// <summary>
        /// For each list of strings in the input, returns all of the strings
        /// that do not appear in any later list of strings.
        /// </summary>
        /// <param name="rows"></param>
        /// <returns></returns>
        public static IEnumerable<IEnumerable<string>> LastMentionsPerRow(this IEnumerable<IEnumerable<string>> rows)
        {
            return FirstMentionsPerRow(rows.Reverse()).Reverse();
        }

        /// <summary>
        /// Iterates over all three IEnumerable instances in parallel and calls
        /// the provided function with an element from each one.
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <typeparam name="TThird"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="third"></param>
        /// <param name="action"></param>
        public static void ForEach<TFirst, TSecond, TThird>(
            IEnumerable<TFirst> first,
            IEnumerable<TSecond> second,
            IEnumerable<TThird> third,
            Action<TFirst, TSecond, TThird> action)
        {
            using (var firstEnumerator = first.GetEnumerator())
            using (var secondEnumerator = second.GetEnumerator())
            using (var thirdEnumerator = third.GetEnumerator())
            {
                while (true)
                {
                    var firstHasNext = firstEnumerator.MoveNext();
                    var secondHasNext = secondEnumerator.MoveNext();
                    var thirdHasNext = thirdEnumerator.MoveNext();

                    if (firstHasNext && secondHasNext && thirdHasNext)
                    {
                        action(firstEnumerator.Current, secondEnumerator.Current, thirdEnumerator.Current);
                    }
                    else if (firstHasNext || secondHasNext || thirdHasNext)
                    {
                        throw new ArgumentOutOfRangeException(Resources.EnumeratorsNotOfEqualLengthException);
                    }
                    else
                    {
                        break;
                    }
                };
            }
        }
    }
}
