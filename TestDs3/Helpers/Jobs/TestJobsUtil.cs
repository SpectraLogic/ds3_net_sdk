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

using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Ds3.Models;
using Ds3.Helpers.Jobs;

namespace TestDs3.Helpers.Jobs
{
    internal class TestJobsUtil
    {
        [Test]
        public void NoRanges()
        {
            var oldRanges = new List<Range>();
            var newRanges = JobsUtil.RetryRanges(oldRanges, 12, 20);

            Assert.AreEqual(1, newRanges.Count());

            var range = newRanges.ElementAt(0);
            Assert.AreEqual(11, range.Start);
            Assert.AreEqual(19, range.End);
        }

        [Test]
        public void OneRange()
        {
            var oldRanges = new List<Range>();
            oldRanges.Add(Range.ByPosition(0, 19));

            var newRanges = JobsUtil.RetryRanges(oldRanges, 10, 20);

            Assert.AreEqual(1, newRanges.Count());

            var range = newRanges.ElementAt(0);
            Assert.AreEqual(9, range.Start);
            Assert.AreEqual(19, range.End);
        }

        [Test]
        public void TwoRangesWithFailureInFirst()
        {
            var oldRanges = new List<Range>();
            oldRanges.Add(Range.ByPosition(10, 19));
            oldRanges.Add(Range.ByPosition(30, 49));

            var contentLength = oldRanges.Sum(rangeIter => rangeIter.Length);

            Assert.AreEqual(30, contentLength);

            var newRanges = JobsUtil.RetryRanges(oldRanges, 5, contentLength);

            Assert.AreEqual(2, newRanges.Count());

            var range1 = newRanges.ElementAt(0);
            Assert.AreEqual(14, range1.Start);
            Assert.AreEqual(19, range1.End);

            var range2 = newRanges.ElementAt(1);
            Assert.AreEqual(30, range2.Start);
            Assert.AreEqual(49, range2.End);
        }

        [Test]
        public void TwoRangesWithFailureInSecond()
        {
            var oldRanges = new List<Range>();

            oldRanges.Add(Range.ByPosition(10, 19));
            oldRanges.Add(Range.ByPosition(30, 49));
            
            var contentLength = oldRanges.Sum(rangeIter => rangeIter.Length);

            Assert.AreEqual(30, contentLength);

            var newRanges = JobsUtil.RetryRanges(oldRanges, 15, contentLength);

            Assert.AreEqual(1, newRanges.Count());

            var range = newRanges.ElementAt(0);
            Assert.AreEqual(34, range.Start);
            Assert.AreEqual(49, range.End);
        }

        [Test]
        public void FailureOnLastByteOfFirstRange()
        {
            var oldRanges = new List<Range>();

            oldRanges.Add(Range.ByPosition(10, 19));
            oldRanges.Add(Range.ByPosition(30, 49));

            var contentLength = oldRanges.Sum(rangeIter => rangeIter.Length);

            Assert.AreEqual(30, contentLength);

            var newRanges = JobsUtil.RetryRanges(oldRanges, 10, contentLength);

            Assert.AreEqual(2, newRanges.Count());

            var range1 = newRanges.ElementAt(0);
            Assert.AreEqual(19, range1.Start);
            Assert.AreEqual(19, range1.End);

            var range2 = newRanges.ElementAt(1);
            Assert.AreEqual(30, range2.Start);
            Assert.AreEqual(49, range2.End);
        }
    }
}
