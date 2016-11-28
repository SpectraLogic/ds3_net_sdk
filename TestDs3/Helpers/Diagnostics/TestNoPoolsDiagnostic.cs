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

using Ds3;
using Ds3.Calls;
using Ds3.Helpers.Ds3Diagnostics;
using Moq;
using NUnit.Framework;

namespace TestDs3.Helpers.Diagnostics
{
    [TestFixture]
    public class TestNoPoolsDiagnostic
    {
        [Test]
        public void TestGetWithNoPools()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.GetPoolsSpectraS3(It.IsAny<GetPoolsSpectraS3Request>()))
                .Returns(DiagnosticsStubs.NoPools);

            var noPools = new NoPools();
            var noPoolsResult = noPools.Get(client.Object);

            Assert.AreEqual(Ds3DiagnosticsCode.NoPoolsFound, noPoolsResult.Code);
            Assert.AreEqual(DiagnosticsMessages.NoPoolsFound, noPoolsResult.ErrorMessage);
            Assert.AreEqual(null, noPoolsResult.ErrorInfo);

            client.VerifyAll();
        }

        [Test]
        public void TestGetWithOneTape()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.GetPoolsSpectraS3(It.IsAny<GetPoolsSpectraS3Request>()))
                .Returns(DiagnosticsStubs.OnePoweredOffPool);

            var noPools = new NoPools();
            var noPoolsResult = noPools.Get(client.Object);

            Assert.AreEqual(Ds3DiagnosticsCode.Ok, noPoolsResult.Code);
            Assert.AreEqual(null, noPoolsResult.ErrorMessage);
            Assert.AreEqual(null, noPoolsResult.ErrorInfo);

            client.VerifyAll();
        }
    }
}
