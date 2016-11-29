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
    public class TestWriteToTapeDiagnostic
    {
        [Test]
        public void TestGetWithNoReadingTasks()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.GetDataPlannerBlobStoreTasksSpectraS3(It.IsAny<GetDataPlannerBlobStoreTasksSpectraS3Request>()))
                .Returns(DiagnosticsStubs.NoWritingTasks);

            var writeToTapeDiagnostic = new WriteToTapeDiagnostic();
            var writeToTapeResult = writeToTapeDiagnostic.Get(client.Object);

            Assert.AreEqual(Ds3DiagnosticsCode.Ok, writeToTapeResult.Code);
            Assert.AreEqual(null, writeToTapeResult.ErrorMessage);
            Assert.AreEqual(null, writeToTapeResult.ErrorInfo);

            client.VerifyAll();
        }

        [Test]
        public void TestGetWithOneReadingTask()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.GetDataPlannerBlobStoreTasksSpectraS3(It.IsAny<GetDataPlannerBlobStoreTasksSpectraS3Request>()))
                .Returns(DiagnosticsStubs.OneWritingTasks);

            var writeToTapeDiagnostic = new WriteToTapeDiagnostic();
            var writeToTapeResult = writeToTapeDiagnostic.Get(client.Object);

            Assert.AreEqual(Ds3DiagnosticsCode.WritingToTape, writeToTapeResult.Code);
            Assert.AreEqual(string.Format(DiagnosticsMessages.WritingToTape, 1), writeToTapeResult.ErrorMessage);
            Assert.AreEqual(1, writeToTapeResult.ErrorInfo.Count());

            client.VerifyAll();
        }
    }
}
