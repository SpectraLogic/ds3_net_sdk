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
    public class TestPoweredOffPools
    {
        [Test]
        public void TestGetWithNoPoweredOffPools()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.GetPoolsSpectraS3(It.IsAny<GetPoolsSpectraS3Request>()))
                .Returns(DiagnosticsStubs.NoPoweredOffPools);

            var poweredOffPools = new PoweredOffPoolsDiagnostic();
            var poweredOffPoolsResult = poweredOffPools.Get(client.Object);

            Assert.AreEqual(Ds3DiagnosticsCode.Ok, poweredOffPoolsResult.Code);
            Assert.AreEqual(null, poweredOffPoolsResult.ErrorMessage);
            Assert.AreEqual(null, poweredOffPoolsResult.ErrorInfo);

            client.VerifyAll();
        }


        [Test]
        public void TestGetWithOnePoweredOffPool()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.GetPoolsSpectraS3(It.IsAny<GetPoolsSpectraS3Request>()))
                .Returns(DiagnosticsStubs.OnePoweredOffPool);

            var poweredOffPools = new PoweredOffPoolsDiagnostic();
            var poweredOffPoolsResult = poweredOffPools.Get(client.Object);

            Assert.AreEqual(Ds3DiagnosticsCode.PoweredOffPools, poweredOffPoolsResult.Code);
            Assert.AreEqual(string.Format(DiagnosticsMessages.FoundPowerOffPools, 1), poweredOffPoolsResult.ErrorMessage);
            Assert.AreEqual(1, poweredOffPoolsResult.ErrorInfo.Count());

            client.VerifyAll();
        }

        [Test]
        public void TestGetWithTwoTape()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.GetPoolsSpectraS3(It.IsAny<GetPoolsSpectraS3Request>()))
                .Returns(DiagnosticsStubs.TwoPoweredOffPool);

            var poweredOffPools = new PoweredOffPoolsDiagnostic();
            var poweredOffPoolsResult = poweredOffPools.Get(client.Object);

            Assert.AreEqual(Ds3DiagnosticsCode.PoweredOffPools, poweredOffPoolsResult.Code);
            Assert.AreEqual(string.Format(DiagnosticsMessages.FoundPowerOffPools, 2), poweredOffPoolsResult.ErrorMessage);
            Assert.AreEqual(2, poweredOffPoolsResult.ErrorInfo.Count());

            client.VerifyAll();
        }
    }
}
