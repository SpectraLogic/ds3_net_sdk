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
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;

namespace Ds3.Lang
{
    internal static class Parallel
    {
        public static void ForEach<T>(
            int threadPoolSize,
            CancellationToken cancellationToken,
            IEnumerable<T> items,
            Action<T> action)
        {
            using (var itemEnumerator = items.GetEnumerator())
            {
                var itemGetter = new ThreadSafeEnumerator<T>(itemEnumerator);
                var exceptionsThrown = new ConcurrentQueue<Exception>();
                ThreadStart ts = delegate
                {
                    try
                    {
                        var it = default(T);
                        while (
                            exceptionsThrown.Count == 0
                            && !cancellationToken.IsCancellationRequested
                            && itemGetter.TryGetNext(ref it))
                        {
                            action(it);
                        }
                    }
                    catch (Exception e)
                    {
                        exceptionsThrown.Enqueue(e);
                    }
                };
                var threads = new List<Thread>();
                for (var i = 0; i < threadPoolSize; i++)
                {
                    var thread = new Thread(ts);
                    thread.Start();
                    threads.Add(thread);
                }
                foreach (var thread in threads)
                {
                    thread.Join();
                }
                if (exceptionsThrown.Count > 0)
                {
                    throw new AggregateException(exceptionsThrown);
                }
                cancellationToken.ThrowIfCancellationRequested();
            }
        }
    }
}