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

            client
                .Setup(c => c.GetDs3TargetsSpectraS3(It.IsAny<GetDs3TargetsSpectraS3Request>()))
                .Returns(DiagnosticsStubs.NoTargets);

            var ds3DiagnosticHelper = new Ds3DiagnosticHelper(client.Object);
            var ds3DiagnosticResult = ds3DiagnosticHelper.Get(new CacheNearCapacityDiagnostic());

            Assert.AreEqual(Ds3DiagnosticsCode.CacheNearCapacity, ds3DiagnosticResult.ClientResult.Code);
            Assert.AreEqual(2, ds3DiagnosticResult.ClientResult.ErrorInfo.Count());

            client.VerifyAll();
        }

        [Test]
        public void TestNoPoolsGet()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);

            client
                .Setup(c => c.GetPoolsSpectraS3(It.IsAny<GetPoolsSpectraS3Request>()))
                .Returns(DiagnosticsStubs.NoPools);

            client
                .Setup(c => c.GetDs3TargetsSpectraS3(It.IsAny<GetDs3TargetsSpectraS3Request>()))
                .Returns(DiagnosticsStubs.NoTargets);

            var ds3DiagnosticHelper = new Ds3DiagnosticHelper(client.Object);
            var ds3DiagnosticResult = ds3DiagnosticHelper.Get(new NoPoolsDiagnostic());


            Assert.AreEqual(Ds3DiagnosticsCode.NoPoolsFound, ds3DiagnosticResult.ClientResult.Code);

            client.VerifyAll();
        }

        [Test]
        public void TestNoTapeGet()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);

            client
                .Setup(c => c.GetTapesSpectraS3(It.IsAny<GetTapesSpectraS3Request>()))
                .Returns(DiagnosticsStubs.NoTapes);

            client
                .Setup(c => c.GetDs3TargetsSpectraS3(It.IsAny<GetDs3TargetsSpectraS3Request>()))
                .Returns(DiagnosticsStubs.NoTargets);

            var ds3DiagnosticHelper = new Ds3DiagnosticHelper(client.Object);
            var ds3DiagnosticResult = ds3DiagnosticHelper.Get(new NoTapesDiagnostic());


            Assert.AreEqual(Ds3DiagnosticsCode.NoTapesFound, ds3DiagnosticResult.ClientResult.Code);

            client.VerifyAll();
        }

        [Test]
        public void TestOfflineTapeGet()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);

            client
                .Setup(c => c.GetTapesSpectraS3(It.IsAny<GetTapesSpectraS3Request>()))
                .Returns(DiagnosticsStubs.OneTape);

            client
                .Setup(c => c.GetDs3TargetsSpectraS3(It.IsAny<GetDs3TargetsSpectraS3Request>()))
                .Returns(DiagnosticsStubs.NoTargets);

            var ds3DiagnosticHelper = new Ds3DiagnosticHelper(client.Object);
            var ds3DiagnosticResult = ds3DiagnosticHelper.Get(new OfflineTapesDiagnostic());


            Assert.AreEqual(Ds3DiagnosticsCode.OfflineTapes, ds3DiagnosticResult.ClientResult.Code);
            Assert.AreEqual(1, ds3DiagnosticResult.ClientResult.ErrorInfo.Count());

            client.VerifyAll();
        }

        [Test]
        public void TestPoweredOffPoolsGet()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);

            client
                .Setup(c => c.GetPoolsSpectraS3(It.IsAny<GetPoolsSpectraS3Request>()))
                .Returns(DiagnosticsStubs.OnePoweredOffPool);

            client
                .Setup(c => c.GetDs3TargetsSpectraS3(It.IsAny<GetDs3TargetsSpectraS3Request>()))
                .Returns(DiagnosticsStubs.NoTargets);

            var ds3DiagnosticHelper = new Ds3DiagnosticHelper(client.Object);
            var ds3DiagnosticResult = ds3DiagnosticHelper.Get(new PoweredOffPoolsDiagnostic());


            Assert.AreEqual(Ds3DiagnosticsCode.PoweredOffPools, ds3DiagnosticResult.ClientResult.Code);
            Assert.AreEqual(1, ds3DiagnosticResult.ClientResult.ErrorInfo.Count());

            client.VerifyAll();
        }

        [Test]
        public void TestReadingFromTapeGet()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);

            client
                .Setup(
                    c =>
                        c.GetDataPlannerBlobStoreTasksSpectraS3(
                            It.IsAny<GetDataPlannerBlobStoreTasksSpectraS3Request>()))
                .Returns(DiagnosticsStubs.OneReadingTasks);

            client
                .Setup(c => c.GetDs3TargetsSpectraS3(It.IsAny<GetDs3TargetsSpectraS3Request>()))
                .Returns(DiagnosticsStubs.NoTargets);

            var ds3DiagnosticHelper = new Ds3DiagnosticHelper(client.Object);
            var ds3DiagnosticResult = ds3DiagnosticHelper.Get(new ReadFromTapeDiagnostic());


            Assert.AreEqual(Ds3DiagnosticsCode.ReadingFromTape, ds3DiagnosticResult.ClientResult.Code);
            Assert.AreEqual(1, ds3DiagnosticResult.ClientResult.ErrorInfo.Count());

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
                .SetupSequence(c => c.GetTapesSpectraS3(It.IsAny<GetTapesSpectraS3Request>()))
                .Returns(DiagnosticsStubs.OneTape)
                .Returns(DiagnosticsStubs.NoTapes);

            client
                .SetupSequence(c => c.GetPoolsSpectraS3(It.IsAny<GetPoolsSpectraS3Request>()))
                .Returns(DiagnosticsStubs.OnePoweredOffPool)
                .Returns(DiagnosticsStubs.NoPools);

            client
                .SetupSequence(
                    c =>
                        c.GetDataPlannerBlobStoreTasksSpectraS3(
                            It.IsAny<GetDataPlannerBlobStoreTasksSpectraS3Request>()))
                .Returns(DiagnosticsStubs.OneReadingTasks)
                .Returns(DiagnosticsStubs.OneWritingTasks);

            client
                .Setup(c => c.GetDs3TargetsSpectraS3(It.IsAny<GetDs3TargetsSpectraS3Request>()))
                .Returns(DiagnosticsStubs.NoTargets);

            var ds3DiagnosticHelper = new Ds3DiagnosticHelper(client.Object);
            var ds3DiagnosticResult = ds3DiagnosticHelper.RunAll();

            Assert.AreEqual(Ds3DiagnosticsCode.CacheNearCapacity,
                ds3DiagnosticResult.CacheNearCapacityDiagnosticResult.ClientResult.Code);
            Assert.AreEqual(2, ds3DiagnosticResult.CacheNearCapacityDiagnosticResult.ClientResult.ErrorInfo.Count());

            Assert.AreEqual(Ds3DiagnosticsCode.OfflineTapes,
                ds3DiagnosticResult.OfflineTapesDiagnosticResult.ClientResult.Code);
            Assert.AreEqual(1, ds3DiagnosticResult.OfflineTapesDiagnosticResult.ClientResult.ErrorInfo.Count());

            Assert.AreEqual(Ds3DiagnosticsCode.NoTapesFound,
                ds3DiagnosticResult.NoTapesDiagnosticResult.ClientResult.Code);

            Assert.AreEqual(Ds3DiagnosticsCode.PoweredOffPools,
                ds3DiagnosticResult.PoweredOffPoolsDiagnosticResult.ClientResult.Code);
            Assert.AreEqual(1, ds3DiagnosticResult.PoweredOffPoolsDiagnosticResult.ClientResult.ErrorInfo.Count());

            Assert.AreEqual(Ds3DiagnosticsCode.NoPoolsFound,
                ds3DiagnosticResult.NoPoolsDiagnosticResult.ClientResult.Code);

            Assert.AreEqual(Ds3DiagnosticsCode.ReadingFromTape,
                ds3DiagnosticResult.ReadingFromTapeTasksResult.ClientResult.Code);
            Assert.AreEqual(1, ds3DiagnosticResult.ReadingFromTapeTasksResult.ClientResult.ErrorInfo.Count());

            Assert.AreEqual(Ds3DiagnosticsCode.WritingToTape,
                ds3DiagnosticResult.WritingFromTapeTasksResult.ClientResult.Code);
            Assert.AreEqual(1, ds3DiagnosticResult.WritingFromTapeTasksResult.ClientResult.ErrorInfo.Count());

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
                .Returns(DiagnosticsStubs.NoTapes)
                .Returns(DiagnosticsStubs.OneTape);

            client
                .SetupSequence(c => c.GetPoolsSpectraS3(It.IsAny<GetPoolsSpectraS3Request>()))
                .Returns(DiagnosticsStubs.NoPoweredOffPools)
                .Returns(DiagnosticsStubs.OnePoweredOffPool);

            client
                .SetupSequence(
                    c =>
                        c.GetDataPlannerBlobStoreTasksSpectraS3(
                            It.IsAny<GetDataPlannerBlobStoreTasksSpectraS3Request>()))
                .Returns(DiagnosticsStubs.NoReadingTasks)
                .Returns(DiagnosticsStubs.NoWritingTasks);

            client
                .Setup(c => c.GetDs3TargetsSpectraS3(It.IsAny<GetDs3TargetsSpectraS3Request>()))
                .Returns(DiagnosticsStubs.NoTargets);

            var ds3DiagnosticHelper = new Ds3DiagnosticHelper(client.Object);
            var ds3DiagnosticResult = ds3DiagnosticHelper.RunAll();

            Assert.AreEqual(Ds3DiagnosticsCode.Ok,
                ds3DiagnosticResult.CacheNearCapacityDiagnosticResult.ClientResult.Code);
            Assert.AreEqual(Ds3DiagnosticsCode.Ok, ds3DiagnosticResult.OfflineTapesDiagnosticResult.ClientResult.Code);
            Assert.AreEqual(Ds3DiagnosticsCode.Ok, ds3DiagnosticResult.NoTapesDiagnosticResult.ClientResult.Code);
            Assert.AreEqual(Ds3DiagnosticsCode.Ok, ds3DiagnosticResult.PoweredOffPoolsDiagnosticResult.ClientResult.Code);
            Assert.AreEqual(Ds3DiagnosticsCode.Ok, ds3DiagnosticResult.NoPoolsDiagnosticResult.ClientResult.Code);
            Assert.AreEqual(Ds3DiagnosticsCode.Ok, ds3DiagnosticResult.ReadingFromTapeTasksResult.ClientResult.Code);
            Assert.AreEqual(Ds3DiagnosticsCode.Ok, ds3DiagnosticResult.WritingFromTapeTasksResult.ClientResult.Code);

            client.VerifyAll();
        }

        [Test]
        public void TestWritingToTapeGet()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);

            client
                .Setup(
                    c =>
                        c.GetDataPlannerBlobStoreTasksSpectraS3(
                            It.IsAny<GetDataPlannerBlobStoreTasksSpectraS3Request>()))
                .Returns(DiagnosticsStubs.OneWritingTasks);

            client
                .Setup(c => c.GetDs3TargetsSpectraS3(It.IsAny<GetDs3TargetsSpectraS3Request>()))
                .Returns(DiagnosticsStubs.NoTargets);

            var ds3DiagnosticHelper = new Ds3DiagnosticHelper(client.Object);
            var ds3DiagnosticResult = ds3DiagnosticHelper.Get(new WriteToTapeDiagnostic());


            Assert.AreEqual(Ds3DiagnosticsCode.WritingToTape, ds3DiagnosticResult.ClientResult.Code);
            Assert.AreEqual(1, ds3DiagnosticResult.ClientResult.ErrorInfo.Count());

            client.VerifyAll();
        }
    }
}