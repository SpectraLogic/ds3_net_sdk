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
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace TestDs3.Helpers.ProgressTrackers
{
    [TestFixture]
    public class TestJobItemTracker
    {
        private static readonly IEnumerable<IEnumerable<Range>> _successes = new[]
        {
            new SuccessCase("entire part") { { 100L, 100L }, { 0L, 100L }, { 200L, 100L } },
            new SuccessCase("first then second part") { { 100L, 75L }, { 175L, 25L }, { 0L, 100L }, { 200L, 100L } },
            new SuccessCase("second then first part") { { 175L, 25L }, { 100L, 75L }, { 0L, 100L }, { 200L, 100L } },
            new SuccessCase("middle then first then second part") { { 125L, 50L }, { 100L, 25L }, { 175L, 25L }, { 0L, 100L }, { 200L, 100L } }
        };
        private static readonly IEnumerable<FailureCase> _failures = new[]
        {
            new FailureCase("completely before", 0L, 99L),
            new FailureCase("just before", 0L, 100L),
            new FailureCase("overlapping before", 50L, 100L),
            new FailureCase("overlapping both", 50L, 200L),
            new FailureCase("overlapping after", 150L, 100L),
            new FailureCase("just after", 200L, 100L),
            new FailureCase("completely after", 201L, 99L),
        };

        [Test, TestCaseSource("_successes")]
        public void CompletionEventsFire(IEnumerable<Range> ranges)
        {
            var tracker = new JobItemTracker<string>(
                new[]
                {
                    Range.ByLength(0L, 100L),
                    Range.ByLength(100L, 100L),
                    Range.ByLength(200L, 100L)
                }.Select(r => ContextRange.Create(r, "foo"))
            );

            var sizes = new List<long>();
            var completions = new List<string>();
            tracker.DataTransferred += sizes.Add;
            tracker.ItemCompleted += completions.Add;

            foreach (var part in ranges)
            {
                tracker.CompleteRange(ContextRange.Create(part, "foo"));
            }

            CollectionAssert.AreEqual(ranges.Select(r => r.Length), sizes);
            CollectionAssert.AreEqual(new[] { "foo" }, completions);
        }

        [Test, TestCaseSource("_failures")]
        public void CompleteRangeFails(FailureCase failureCase)
        {
            var tracker = new JobItemTracker<string>(new[] { ContextRange.Create(Range.ByLength(100L, 100L), "foo") });
            Assert.Throws<ArgumentOutOfRangeException>(() => tracker.CompleteRange(ContextRange.Create(failureCase.Range, "foo")));
        }

        [Test]
        public void CompleteRangeWorksWithMultipleItems()
        {
            var tracker = new JobItemTracker<string>(new[]
            {
                ContextRange.Create(Range.ByLength(100L, 100L), "foo"),
                ContextRange.Create(Range.ByLength(0L, 100L), "bar"),
                ContextRange.Create(Range.ByLength(200L, 100L), "foo"),
                ContextRange.Create(Range.ByLength(300L, 100L), "bar"),
            });
            var sizes = new List<long>();
            var completions = new List<string>();
            tracker.DataTransferred += sizes.Add;
            tracker.ItemCompleted += completions.Add;

            Assert.False(tracker.Completed);

            tracker.CompleteRange(ContextRange.Create(Range.ByLength(0L, 100), "bar"));
            Assert.False(tracker.Completed);
            Assert.AreEqual(new[] { 100L }, sizes);
            Assert.AreEqual(new string[0], completions);

            tracker.CompleteRange(ContextRange.Create(Range.ByLength(320L, 60L), "bar"));
            Assert.False(tracker.Completed);
            Assert.AreEqual(new[] { 100L, 60L }, sizes);
            Assert.AreEqual(new string[0], completions);

            tracker.CompleteRange(ContextRange.Create(Range.ByLength(100L, 100L), "foo"));
            Assert.False(tracker.Completed);
            Assert.AreEqual(new[] { 100L, 60L, 100L }, sizes);
            Assert.AreEqual(new string[0], completions);

            tracker.CompleteRange(ContextRange.Create(Range.ByLength(200L, 100L), "foo"));
            Assert.False(tracker.Completed);
            Assert.AreEqual(new[] { 100L, 60L, 100L, 100L }, sizes);
            Assert.AreEqual(new[] { "foo" }, completions);

            tracker.CompleteRange(ContextRange.Create(Range.ByLength(300L, 20L), "bar"));
            Assert.False(tracker.Completed);
            Assert.AreEqual(new[] { 100L, 60L, 100L, 100L, 20L }, sizes);
            Assert.AreEqual(new[] { "foo" }, completions);

            tracker.CompleteRange(ContextRange.Create(Range.ByLength(380L, 20L), "bar"));
            Assert.True(tracker.Completed);
            Assert.AreEqual(new[] { 100L, 60L, 100L, 100L, 20L, 20L }, sizes);
            Assert.AreEqual(new[] { "foo", "bar" }, completions);
        }

        public class SuccessCase : IEnumerable<Range>
        {
            private readonly string _name;
            private readonly IList<Range> _ranges = new List<Range>();

            public SuccessCase(string name)
            {
                this._name = name;
            }

            public void Add(long offset, long length)
            {
                this._ranges.Add(Range.ByLength(offset, length));
            }

            public IEnumerator<Range> GetEnumerator()
            {
                return _ranges.GetEnumerator();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                return this.GetEnumerator();
            }

            public override string ToString()
            {
                return this._name;
            }
        }

        public class FailureCase
        {
            private readonly string _name;

            public Range Range { get; private set; }

            public FailureCase(string name, long offset, long length)
            {
                this._name = name;
                this.Range = Range.ByLength(offset, length);
            }

            public override string ToString()
            {
                return this._name;
            }
        }
    }
}
