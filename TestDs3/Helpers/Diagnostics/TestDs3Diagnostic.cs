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
using Ds3.Helpers.Ds3Diagnostics;
using NUnit.Framework;

namespace TestDs3.Helpers.Diagnostics
{
    [TestFixture]
    public class TestDs3Diagnostic
    {
        [Test]
        public void TestRunAllWithTargets()
        {
            /************************
            *          BP           *
            *         /  \          *
            *        T1   T2        *
            *       /  \   |        *
            *      T3  T4  T5       *
            *************************/

            var ds3DiagnosticHelper = new Ds3DiagnosticHelper(DiagnosticsStubClients.Client.Object,
                DiagnosticsStubClients.Ds3TargetClientBuilder.Object);
            var ds3DiagnosticResult = ds3DiagnosticHelper.RunAll();

            Assert.AreEqual(Ds3DiagnosticsCode.CacheNearCapacity,
                ds3DiagnosticResult.CacheNearCapacityDiagnosticResult.ClientResult.Code);
            Assert.AreEqual(2, ds3DiagnosticResult.CacheNearCapacityDiagnosticResult.ClientResult.ErrorInfo.Count());
            Assert.AreEqual(2, ds3DiagnosticResult.CacheNearCapacityDiagnosticResult.TargetsResults.Count);
            Assert.AreEqual(2,
                ds3DiagnosticResult.CacheNearCapacityDiagnosticResult.TargetsResults[0].TargetsResults.Count);
            Assert.AreEqual(1,
                ds3DiagnosticResult.CacheNearCapacityDiagnosticResult.TargetsResults[1].TargetsResults.Count);

            Assert.AreEqual(Ds3DiagnosticsCode.OfflineTapes,
                ds3DiagnosticResult.TapesDiagnosticResult.ClientResult.Code);
            Assert.AreEqual(1, ds3DiagnosticResult.TapesDiagnosticResult.ClientResult.ErrorInfo.Count());
            Assert.AreEqual(2, ds3DiagnosticResult.TapesDiagnosticResult.TargetsResults.Count);
            Assert.AreEqual(2, ds3DiagnosticResult.TapesDiagnosticResult.TargetsResults[0].TargetsResults.Count);
            Assert.AreEqual(1, ds3DiagnosticResult.TapesDiagnosticResult.TargetsResults[1].TargetsResults.Count);

            Assert.AreEqual(Ds3DiagnosticsCode.PoweredOffPools,
                ds3DiagnosticResult.PoolsDiagnosticResult.ClientResult.Code);
            Assert.AreEqual(1, ds3DiagnosticResult.PoolsDiagnosticResult.ClientResult.ErrorInfo.Count());
            Assert.AreEqual(2, ds3DiagnosticResult.PoolsDiagnosticResult.TargetsResults.Count);
            Assert.AreEqual(2,
                ds3DiagnosticResult.PoolsDiagnosticResult.TargetsResults[0].TargetsResults.Count);
            Assert.AreEqual(1,
                ds3DiagnosticResult.PoolsDiagnosticResult.TargetsResults[1].TargetsResults.Count);

            Assert.AreEqual(Ds3DiagnosticsCode.ReadingFromTape,
                ds3DiagnosticResult.ReadingFromTapeTasksResult.ClientResult.Code);
            Assert.AreEqual(1, ds3DiagnosticResult.ReadingFromTapeTasksResult.ClientResult.ErrorInfo.Count());
            Assert.AreEqual(2, ds3DiagnosticResult.ReadingFromTapeTasksResult.TargetsResults.Count);
            Assert.AreEqual(2, ds3DiagnosticResult.ReadingFromTapeTasksResult.TargetsResults[0].TargetsResults.Count);
            Assert.AreEqual(1, ds3DiagnosticResult.ReadingFromTapeTasksResult.TargetsResults[1].TargetsResults.Count);

            Assert.AreEqual(Ds3DiagnosticsCode.WritingToTape,
                ds3DiagnosticResult.WritingFromTapeTasksResult.ClientResult.Code);
            Assert.AreEqual(1, ds3DiagnosticResult.WritingFromTapeTasksResult.ClientResult.ErrorInfo.Count());
            Assert.AreEqual(2, ds3DiagnosticResult.WritingFromTapeTasksResult.TargetsResults.Count);
            Assert.AreEqual(2, ds3DiagnosticResult.WritingFromTapeTasksResult.TargetsResults[0].TargetsResults.Count);
            Assert.AreEqual(1, ds3DiagnosticResult.WritingFromTapeTasksResult.TargetsResults[1].TargetsResults.Count);

            DiagnosticsStubClients.VerifyAll();
        }
    }
}