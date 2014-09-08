using Ds3.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace TestDs3
{
    [TestFixture]
    public class TestJobPartTracker
    {
        [Test]
        public void TrackerCallsTrackers()
        {
            var fooTracker = new MockObjectPartTracker(new List<bool> { false, true });
            var barTracker = new MockObjectPartTracker(new List<bool> { true, false });
            var trackers = new Dictionary<string, IObjectPartTracker>
            {
                { "foo", fooTracker },
                { "bar", barTracker }
            };
            var jobPartTracker = new JobPartTracker(trackers);

            var partsRemoved = new[]
            {
                new ObjectPart(10, 11),
                new ObjectPart(12, 13)
            };
            jobPartTracker.CompletePart("foo", partsRemoved[0]);
            jobPartTracker.CompletePart("foo", partsRemoved[1]);

            var partsChecked = new[]
            {
                new ObjectPart(14, 15),
                new ObjectPart(16, 17)
            };
            Assert.IsFalse(jobPartTracker.ContainsPart("foo", partsChecked[0]));
            Assert.IsTrue(jobPartTracker.ContainsPart("foo", partsChecked[1]));

            CollectionAssert.AreEqual(fooTracker.PartsRemoved, partsRemoved);
            CollectionAssert.AreEqual(fooTracker.PartsChecked, partsChecked);

            CollectionAssert.IsEmpty(barTracker.PartsRemoved);
            CollectionAssert.IsEmpty(barTracker.PartsChecked);
        }

        [Test]
        public void TrackerEventsForward()
        {
            var fooTracker = new MockObjectPartTracker(new List<bool>());
            var barTracker = new MockObjectPartTracker(new List<bool>());
            var trackers = new Dictionary<string, IObjectPartTracker>
            {
                { "foo", fooTracker },
                { "bar", barTracker }
            };

            var sizes = new List<long>();
            var objects = new List<string>();

            var jobPartTracker = new JobPartTracker(trackers);
            jobPartTracker.DataTransferred += sizes.Add;
            jobPartTracker.ObjectCompleted += objects.Add;

            fooTracker.OnDataTransferred(10);
            barTracker.OnDataTransferred(11);
            fooTracker.OnDataTransferred(12);

            barTracker.OnCompleted();
            fooTracker.OnCompleted();

            CollectionAssert.AreEqual(new[] { 10, 11, 12 }, sizes);
            CollectionAssert.AreEqual(new[] { "bar", "foo" }, objects);
        }

        private class MockObjectPartTracker : IObjectPartTracker
        {
            private readonly IList<ObjectPart> _partsRemoved = new List<ObjectPart>();
            private readonly IList<ObjectPart> _partsChecked = new List<ObjectPart>();
            private readonly IList<bool> _containsPartResponses;

            public MockObjectPartTracker(IList<bool> containsPartResponses)
            {
                this._containsPartResponses = containsPartResponses;
            }

            public IEnumerable<ObjectPart> PartsRemoved
            {
                get { return this._partsRemoved; }
            }
            public IEnumerable<ObjectPart> PartsChecked
            {
                get { return this._partsChecked; }
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

            public void CompletePart(ObjectPart partToRemove)
            {
                this._partsRemoved.Add(partToRemove);
            }

            public bool ContainsPart(ObjectPart part)
            {
                this._partsChecked.Add(part);
                var result = this._containsPartResponses[0];
                this._containsPartResponses.RemoveAt(0);
                return result;
            }
        }
    }
}
