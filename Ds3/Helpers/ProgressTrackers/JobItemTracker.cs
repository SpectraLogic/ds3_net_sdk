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
using Ds3.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Ds3.Helpers.ProgressTrackers
{
    /// <summary>
    /// A "job item" represents one of the things that the SDK client requests.
    /// In the case of regular GET and PUT jobs, this is an object. In the
    /// case of a partial object GET, this is a requested byte range for an
    /// object. If the user requests multiple byte ranges for the same object,
    /// each range counts as a "job item";
    /// </summary>
    internal class JobItemTracker<T> where T : IComparable<T>
    {
        private readonly IDictionary<T, IRangeTransferTracker> _trackers;
        private readonly ISet<T> _activeJobItems;
        private readonly object _activeJobItemsLock = new object();

        public event Action<long> DataTransferred;
        public event Action<T> ItemCompleted;


        public JobItemTracker(IEnumerable<ContextRange<T>> ranges)
            : this(BuildTrackers(ranges))
        {
        }

        private static IDictionary<T, IRangeTransferTracker> BuildTrackers(IEnumerable<ContextRange<T>> ranges)
        {
            return ranges
                .GroupBy(
                    range => range.Context,
                    range => range.Range
                )
                .ToDictionary(
                    grp => grp.Key,
                    grp => new ConcurrentRangeTransferTracker(new RangeTransferTracker(grp))
                        as IRangeTransferTracker
                );
        }

        public JobItemTracker(IDictionary<T, IRangeTransferTracker> trackers)
        {
            this._trackers = trackers;
            this._activeJobItems = new HashSet<T>(this._trackers.Keys);
            foreach (var kvp in trackers)
            {
                kvp.Value.DataTransferred += size => this.DataTransferred.Call(size);
                kvp.Value.Completed += () => OnItemCompleted(kvp.Key);
            }
        }

        public void CompleteRange(ContextRange<T> contextRange)
        {
            this._trackers[contextRange.Context].CompleteRange(contextRange.Range);
        }

        private void OnItemCompleted(T obj)
        {
            lock (this._activeJobItemsLock)
            {
                this._activeJobItems.Remove(obj);
            }
            this.ItemCompleted.Call(obj);
        }

        public bool Completed
        {
            get
            {
                lock (this._activeJobItemsLock)
                {
                    return this._activeJobItems.Count == 0;
                }
            }
        }
    }
}
