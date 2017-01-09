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
    public class TestPoolsDiagnostic
    {
        [Test]
        public void TestGetWithNoPools()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.GetPoolsSpectraS3(It.IsAny<GetPoolsSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.NoPools);

            var noPools = new PoolsDiagnostic();
            var ds3DiagnosticClient = new Ds3DiagnosticClient
            {
                Client = client.Object
            };
            var noPoolsResult = noPools.Get(ds3DiagnosticClient);

            Assert.AreEqual(Ds3DiagnosticsCode.NoPoolsFound, noPoolsResult.ClientResult.Code);
            Assert.AreEqual(DiagnosticsMessages.NoPoolsFound, noPoolsResult.ClientResult.ErrorMessage);
            Assert.AreEqual(null, noPoolsResult.ClientResult.ErrorInfo);

            client.VerifyAll();
        }

        [Test]
        public void TestGetWithNoPoweredOffPools()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.GetPoolsSpectraS3(It.IsAny<GetPoolsSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.NoPoweredOffPools);

            var poweredOffPools = new PoolsDiagnostic();
            var ds3DiagnosticClient = new Ds3DiagnosticClient
            {
                Client = client.Object
            };
            var poweredOffPoolsResult = poweredOffPools.Get(ds3DiagnosticClient);

            Assert.AreEqual(Ds3DiagnosticsCode.Ok, poweredOffPoolsResult.ClientResult.Code);
            Assert.AreEqual(null, poweredOffPoolsResult.ClientResult.ErrorMessage);
            Assert.AreEqual(null, poweredOffPoolsResult.ClientResult.ErrorInfo);

            client.VerifyAll();
        }


        [Test]
        public void TestGetWithOnePoweredOffPool()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.GetPoolsSpectraS3(It.IsAny<GetPoolsSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.OnePoweredOffPool);

            var poweredOffPools = new PoolsDiagnostic();
            var ds3DiagnosticClient = new Ds3DiagnosticClient
            {
                Client = client.Object
            };
            var poweredOffPoolsResult = poweredOffPools.Get(ds3DiagnosticClient);

            Assert.AreEqual(Ds3DiagnosticsCode.PoweredOffPools, poweredOffPoolsResult.ClientResult.Code);
            Assert.AreEqual(string.Format(DiagnosticsMessages.FoundPowerOffPools, 1), poweredOffPoolsResult.ClientResult.ErrorMessage);
            Assert.AreEqual(1, poweredOffPoolsResult.ClientResult.ErrorInfo.Count());

            client.VerifyAll();
        }

        [Test]
        public void TestGetWithTwoTape()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.GetPoolsSpectraS3(It.IsAny<GetPoolsSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.TwoPoweredOffPool);

            var poweredOffPools = new PoolsDiagnostic();
            var ds3DiagnosticClient = new Ds3DiagnosticClient
            {
                Client = client.Object
            };
            var poweredOffPoolsResult = poweredOffPools.Get(ds3DiagnosticClient);

            Assert.AreEqual(Ds3DiagnosticsCode.PoweredOffPools, poweredOffPoolsResult.ClientResult.Code);
            Assert.AreEqual(string.Format(DiagnosticsMessages.FoundPowerOffPools, 2), poweredOffPoolsResult.ClientResult.ErrorMessage);
            Assert.AreEqual(2, poweredOffPoolsResult.ClientResult.ErrorInfo.Count());

            client.VerifyAll();
        }
    }
}
