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

using Ds3.Helpers;
using NUnit.Framework;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Collections;

namespace TestDs3
{
    [TestFixture]
    public class TestObjectPartTracker
    {
        private static readonly IEnumerable<IEnumerable<ObjectPart>> _successes = new[]
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
        public void CompletionEventsFire(IEnumerable<ObjectPart> parts)
        {
            IObjectPartTracker tracker = new ObjectPartTracker(new[]
            {
                new ObjectPart(0L, 100L),
                new ObjectPart(100L, 100L),
                new ObjectPart(200L, 100L)
            });

            var events = new List<long?>();
            tracker.DataTransferred += size => events.Add(size);
            tracker.Completed += () => events.Add(null);

            foreach (var part in parts)
            {
                tracker.CompletePart(part);
            }

            var expectedEvents = parts
                .Select(part => (long?)part.Length)
                .Concat(new long?[] { null })
                .ToList();
            CollectionAssert.AreEqual(expectedEvents, events);
        }

        [Test, TestCaseSource("_failures")]
        public void CompletePartFails(ObjectPart part)
        {
            IObjectPartTracker tracker = new ObjectPartTracker(new[] { new ObjectPart(100L, 100L) });
            Assert.Throws<InvalidOperationException>(() => tracker.CompletePart(part));
        }

        [Test]
        public void InvalidPartsFail()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => new ObjectPartTracker(new[]
            {
                new ObjectPart(0L, 100L),
                new ObjectPart(99L, 100L)
            }));
        }

        [Test]
        public void ContainsPartWorks()
        {
            IObjectPartTracker tracker = new ObjectPartTracker(new[] { new ObjectPart(0L, 100L), new ObjectPart(100L, 100L) });
            Assert.IsTrue(tracker.ContainsPart(new ObjectPart(100L, 100L)));
            Assert.IsFalse(tracker.ContainsPart(new ObjectPart(100L, 50L)));
            Assert.IsFalse(tracker.ContainsPart(new ObjectPart(150L, 50L)));
        }

        private class SuccessCase : IEnumerable<ObjectPart>
        {
            private readonly string _name;
            private readonly IList<ObjectPart> _parts = new List<ObjectPart>();

            public SuccessCase(string name)
            {
                this._name = name;
            }

            public void Add(long offset, long length)
            {
                this._parts.Add(new ObjectPart(offset, length));
            }

            public IEnumerator<ObjectPart> GetEnumerator()
            {
                return _parts.GetEnumerator();
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

        private class FailureCase : ObjectPart
        {
            private readonly string _name;

            public FailureCase(string name, long offset, long length)
                : base(offset, length)
            {
                this._name = name;
            }

            public override string ToString()
            {
                return this._name;
            }
        }
    }
}
