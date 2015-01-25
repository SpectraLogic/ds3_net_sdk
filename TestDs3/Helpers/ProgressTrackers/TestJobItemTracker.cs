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

using Ds3.Helpers.ProgressTrackers;
using Ds3.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace TestDs3.Helpers.ProgressTrackers
{
    [TestFixture]
    public class TestJobItemTracker
    {
        [Test]
        public void TrackerCallsTrackers()
        {
            var fooTracker = new MockRangeTransferTracker(new List<bool> { false, true });
            var barTracker = new MockRangeTransferTracker(new List<bool> { true, false });
            var trackers = new Dictionary<string, IRangeTransferTracker>
            {
                { "foo", fooTracker },
                { "bar", barTracker }
            };
            var jobItemTracker = new JobItemTracker<string>(trackers);

            var rangesRemoved = new[]
            {
                Range.ByLength(10, 11),
                Range.ByLength(12, 13)
            };
            jobItemTracker.CompleteRange(ContextRange.Create(rangesRemoved[0], "foo"));
            jobItemTracker.CompleteRange(ContextRange.Create(rangesRemoved[1], "foo"));

            var rangesChecked = new[]
            {
                Range.ByLength(14, 15),
                Range.ByLength(16, 17)
            };

            CollectionAssert.AreEqual(fooTracker.RangesRemoved, rangesRemoved);
            CollectionAssert.IsEmpty(barTracker.RangesRemoved);
        }

        [Test]
        public void TrackerEventsForward()
        {
            var fooTracker = new MockRangeTransferTracker(new List<bool>());
            var barTracker = new MockRangeTransferTracker(new List<bool>());
            var trackers = new Dictionary<string, IRangeTransferTracker>
            {
                { "foo", fooTracker },
                { "bar", barTracker }
            };

            var sizes = new List<long>();
            var objects = new List<string>();

            var jobItemTracker = new JobItemTracker<string>(trackers);
            jobItemTracker.DataTransferred += sizes.Add;
            jobItemTracker.ItemCompleted += objects.Add;

            fooTracker.OnDataTransferred(10);
            barTracker.OnDataTransferred(11);
            fooTracker.OnDataTransferred(12);

            barTracker.OnCompleted();
            fooTracker.OnCompleted();

            CollectionAssert.AreEqual(new[] { 10, 11, 12 }, sizes);
            CollectionAssert.AreEqual(new[] { "bar", "foo" }, objects);
        }

        [Test]
        public void JobItemTrackerRecordsCompletionState()
        {
            var fooTracker = new MockRangeTransferTracker(new List<bool>());
            var barTracker = new MockRangeTransferTracker(new List<bool>());
            var trackers = new Dictionary<string, IRangeTransferTracker>
            {
                { "foo", fooTracker },
                { "bar", barTracker }
            };
            var jobItemTracker = new JobItemTracker<string>(trackers);
            Assert.False(jobItemTracker.Completed);
            fooTracker.OnCompleted();
            Assert.False(jobItemTracker.Completed);
            barTracker.OnCompleted();
            Assert.True(jobItemTracker.Completed);
        }

        private class MockRangeTransferTracker : IRangeTransferTracker
        {
            private readonly IList<Range> _rangesRemoved = new List<Range>();
            private readonly IList<bool> _containsRangeResponses;

            public MockRangeTransferTracker(IList<bool> containsRangeResponses)
            {
                this._containsRangeResponses = containsRangeResponses;
            }

            public IEnumerable<Range> RangesRemoved
            {
                get { return this._rangesRemoved; }
            }

            public void OnDataTransferred(long size)
            {
                if (this.DataTransferred != null)
                {
                    this.DataTransferred(size);
                }
            }

            public void OnCompleted()
            {
                if (this.Completed != null)
                {
                    this.Completed();
                }
            }

            public event Action<long> DataTransferred;
            public event Action Completed;

            public void CompleteRange(Range rangeToRemove)
            {
                this._rangesRemoved.Add(rangeToRemove);
            }
        }
    }
}
