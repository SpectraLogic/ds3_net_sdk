/*
 * ******************************************************************************
 *   Copyright 2014-2016 Spectra Logic Corporation. All Rights Reserved.
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
using System.IO;
using Ds3.Helpers.TransferStrategies;
using Ds3.Models;
using Ds3.Runtime;
using NUnit.Framework;

namespace TestDs3.Helpers.TransferStrategies
{
    [TestFixture]
    internal class TestBestEffort
    {
        [Test]
        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        [TestCase(4)]
        public void TestCanRetry(int currentTry)
        {
            Stream stream = new MemoryStream();
            var currentTryBefore = currentTry;
            BestEffort.ModifyForRetry(stream, 5, ref currentTry, "Test", 0, null);

            Assert.AreEqual(currentTryBefore + 1, currentTry);
        }

        [Test]
        public void TestModifyForRetry1()
        {
            Stream stream = new MemoryStream();
            stream.Position = 10;
            var currentTryBefore = 0;
            BestEffort.ModifyForRetry(stream, 5, ref currentTryBefore, "Test", 0, null);

            Assert.AreEqual(1, currentTryBefore);
            Assert.AreEqual(0, stream.Position);
        }

        [Test]
        public void TestModifyForRetry2()
        {
            Stream stream = new MemoryStream();
            stream.Position = 10;
            var currentTryBefore = 0;
            IEnumerable<Range> rages = new List<Range>();
            ITransferStrategy transferStrategy = new ReadTransferStrategy();
            BestEffort.ModifyForRetry(stream, 5, ref currentTryBefore, "Test", 10, ref rages, ref transferStrategy,
                new Ds3ContentLengthNotMatch("", 0, 0));

            Assert.AreEqual(1, currentTryBefore);
            Assert.AreEqual(9, stream.Position);
        }

        [Test]
        [TestCase(5)]
        [TestCase(6)]
        public void TestModifyForRetryDs3NoMoreRetransmitException(int currentTry)
        {
            try
            {
                BestEffort.ModifyForRetry(null, 5, ref currentTry, "Test", 0, null);
                Assert.Fail();
            }
            catch (Ds3NoMoreRetransmitException ex)
            {
                Assert.AreEqual(currentTry, ex.Retries);
            }
            catch (Exception)
            {
                Assert.Fail();
            }
        }

        [Test]
        public void TestModifyFortRetryWithNonSeekableStream()
        {
            Stream stream = new NonSeekableStream();
            var currentTry = 0;
            Assert.Throws<Ds3NotSupportedStream>(
                () => BestEffort.ModifyForRetry(stream, 1, ref currentTry, "Test", 0, null));
        }
    }
}