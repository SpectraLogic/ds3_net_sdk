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

using System.Collections.Generic;
using System.Linq;
using Ds3;
using Ds3.Calls;
using Ds3.Helpers.Ds3Diagnostics;
using Moq;
using NUnit.Framework;

namespace TestDs3.Helpers.Diagnostics
{
    [TestFixture]
    public class TestDs3Diagnostic
    {
        [Test]
        public void TestRunAllAllOk()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);

            client
                .Setup(c => c.GetCacheStateSpectraS3(It.IsAny<GetCacheStateSpectraS3Request>()))
                .Returns(DiagnosticsStubs.NoNearCapacity);

            client
                .Setup(c => c.GetTapesSpectraS3(It.IsAny<GetTapesSpectraS3Request>()))
                .Returns(DiagnosticsStubs.EmptyTapes);


            var ds3Diagnostic = new Ds3Diagnostic(client.Object);
            Assert.IsEmpty(ds3Diagnostic.RunAll());

            client.VerifyAll();
        }

        [Test]
        public void TestRunAll()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);

            client
                .Setup(c => c.GetCacheStateSpectraS3(It.IsAny<GetCacheStateSpectraS3Request>()))
                .Returns(DiagnosticsStubs.TwoNearCapacity);

            client
                .Setup(c => c.GetTapesSpectraS3(It.IsAny<GetTapesSpectraS3Request>()))
                .Returns(DiagnosticsStubs.OneTape);


            var ds3Diagnostic = new Ds3Diagnostic(client.Object);

            var expected = new List<Ds3DiagnosticsResult>
            {
                Ds3DiagnosticsResult.CacheNearCapacity,
                Ds3DiagnosticsResult.OfflineTapes
            };

            Assert.AreEqual(expected, ds3Diagnostic.RunAll());
            Assert.AreEqual(2, ds3Diagnostic.CacheFilesystemInformation.Count());
            Assert.AreEqual(1, ds3Diagnostic.OfflineTapes.Count());

            client.VerifyAll();
        }

        [Test]
        public void TestCacheGet()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);

            client
                .Setup(c => c.GetCacheStateSpectraS3(It.IsAny<GetCacheStateSpectraS3Request>()))
                .Returns(DiagnosticsStubs.TwoNearCapacity);

            var ds3Diagnostic = new Ds3Diagnostic(client.Object);
            Assert.AreEqual(2, ds3Diagnostic.Get(new CacheNearCapacity()).Count());

            client.VerifyAll();
        }

        [Test]
        public void TestTapeGet()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);

            client
                .Setup(c => c.GetTapesSpectraS3(It.IsAny<GetTapesSpectraS3Request>()))
                .Returns(DiagnosticsStubs.OneTape);

            var ds3Diagnostic = new Ds3Diagnostic(client.Object);
            Assert.AreEqual(1, ds3Diagnostic.Get(new OfflineTapes()).Count());

            client.VerifyAll();
        }
    }
}
