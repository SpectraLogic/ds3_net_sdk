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

using Ds3.Lang;
using NUnit.Framework;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace TestDs3.Lang
{
    [TestFixture]
    public class TestThreadSafeEnumerator
    {
        [Test]
        public void CanEnumerateSafely()
        {
            var strings = LangTestHelpers.RandomStrings(1000, 50);
            var results = new ConcurrentQueue<string>();
            using (var stringsEnumerator = strings.GetEnumerator())
            {
                var tse = new ThreadSafeEnumerator<string>(stringsEnumerator);
                var threads = new List<Thread>();
                for (int i = 0; i < 10; i++)
                {
                    var thread = new Thread(() =>
                    {
                        string it = null;
                        while (tse.TryGetNext(ref it))
                        {
                            results.Enqueue(it);
                        }
                    });
                    thread.Start();
                    threads.Add(thread);
                }
                foreach (var thread in threads)
                {
                    thread.Join();
                }
            }
            CollectionAssert.AreEquivalent(strings, results);
        }
    }
}
