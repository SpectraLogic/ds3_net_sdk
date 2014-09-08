using Ds3.Helpers;
using Ds3.Models;
using NUnit.Framework;
using System.Collections.Generic;

namespace TestDs3
{
    [TestFixture]
    public class TestJobPartTrackerFactory
    {
        [Test]
        public void CreatedTrackerTracksParts()
        {
            var tracker = JobPartTrackerFactory.BuildPartTracker(new[]
            {
                new JobObject("foo", 10L, 0L, false),
                new JobObject("bar", 13L, 0L, false),
                new JobObject("foo", 11L, 10L, true),
                new JobObject("baz", 12L, 0L, true)
            });

            var transfers = new List<long>();
            var objects = new List<string>();
            tracker.DataTransferred += transfers.Add;
            tracker.ObjectCompleted += objects.Add;

            tracker.CompletePart("baz", new ObjectPart(0L, 12L));
            tracker.CompletePart("foo", new ObjectPart(10L, 11L));
            tracker.CompletePart("bar", new ObjectPart(0L, 13L));
            tracker.CompletePart("foo", new ObjectPart(0L, 10L));

            Assert.AreEqual(new[] { 12L, 11L, 13L, 10L }, transfers);
            Assert.AreEqual(new[] { "baz", "bar", "foo" }, objects);
        }
    }
}
