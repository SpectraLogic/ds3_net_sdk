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
        public void TestCacheGet()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);

            client
                .Setup(c => c.GetCacheStateSpectraS3(It.IsAny<GetCacheStateSpectraS3Request>()))
                .Returns(DiagnosticsStubs.TwoNearCapacity);

            var ds3DiagnosticHelper = new Ds3DiagnosticHelper(client.Object);
            var ds3DiagnosticResult = ds3DiagnosticHelper.Get(new CacheNearCapacity());

            Assert.AreEqual(Ds3DiagnosticsCode.CacheNearCapacity, ds3DiagnosticResult.Code);
            Assert.AreEqual(2, ds3DiagnosticResult.ErrorInfo.Count());

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


            var ds3DiagnosticHelper = new Ds3DiagnosticHelper(client.Object);
            var ds3DiagnosticResult = ds3DiagnosticHelper.RunAll();

            Assert.AreEqual(Ds3DiagnosticsCode.CacheNearCapacity, ds3DiagnosticResult.CacheNearCapacityDiagnostic.Code);
            Assert.AreEqual(2, ds3DiagnosticResult.CacheNearCapacityDiagnostic.ErrorInfo.Count());

            Assert.AreEqual(Ds3DiagnosticsCode.OfflineTapes, ds3DiagnosticResult.OfflineTapeDiagnostic.Code);
            Assert.AreEqual(1, ds3DiagnosticResult.OfflineTapeDiagnostic.ErrorInfo.Count());

            client.VerifyAll();
        }

        [Test]
        public void TestRunAllAllOk()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);

            client
                .Setup(c => c.GetCacheStateSpectraS3(It.IsAny<GetCacheStateSpectraS3Request>()))
                .Returns(DiagnosticsStubs.NoNearCapacity);

            client
                .SetupSequence(c => c.GetTapesSpectraS3(It.IsAny<GetTapesSpectraS3Request>()))
                .Returns(DiagnosticsStubs.OneTape)
                .Returns(DiagnosticsStubs.NoTapes);


            var ds3DiagnosticHelper = new Ds3DiagnosticHelper(client.Object);
            var ds3DiagnosticResult = ds3DiagnosticHelper.RunAll();

            Assert.AreEqual(Ds3DiagnosticsCode.Ok, ds3DiagnosticResult.CacheNearCapacityDiagnostic.Code);
            Assert.AreEqual(Ds3DiagnosticsCode.Ok, ds3DiagnosticResult.OfflineTapeDiagnostic.Code);

            client.VerifyAll();
        }

        [Test]
        public void TestTapeGet()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);

            client
                .Setup(c => c.GetTapesSpectraS3(It.IsAny<GetTapesSpectraS3Request>()))
                .Returns(DiagnosticsStubs.OneTape);

            var ds3DiagnosticHelper = new Ds3DiagnosticHelper(client.Object);
            var ds3DiagnosticResult = ds3DiagnosticHelper.Get(new OfflineTapes());


            Assert.AreEqual(Ds3DiagnosticsCode.OfflineTapes, ds3DiagnosticResult.Code);
            Assert.AreEqual(1, ds3DiagnosticResult.ErrorInfo.Count());

            client.VerifyAll();
        }
    }
}