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
    public class TestCacheNearCapacity
    {
        [Test]
        public void TestGetWithNoFilesystems()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.GetCacheStateSpectraS3(It.IsAny<GetCacheStateSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.EmptyFilesystems);

            var cacheNearCapacity = new CacheNearCapacityDiagnostic();
            var ds3DiagnosticClient = new Ds3DiagnosticClient
            {
                Client = client.Object
            };
            var cacheNearCapacityResult = cacheNearCapacity.Get(ds3DiagnosticClient);

            Assert.AreEqual(Ds3DiagnosticsCode.NoCacheSystemFound, cacheNearCapacityResult.ClientResult.Code);
            Assert.AreEqual(DiagnosticsMessages.NoCacheFound, cacheNearCapacityResult.ClientResult.ErrorMessage);
            Assert.AreEqual(null, cacheNearCapacityResult.ClientResult.ErrorInfo);

            client.VerifyAll();
        }

        [Test]
        public void TestGetWithNoNearCapacity()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.GetCacheStateSpectraS3(It.IsAny<GetCacheStateSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.NoNearCapacity);

            var cacheNearCapacity = new CacheNearCapacityDiagnostic();
            var ds3DiagnosticClient = new Ds3DiagnosticClient
            {
                Client = client.Object
            };
            var cacheNearCapacityResult = cacheNearCapacity.Get(ds3DiagnosticClient);

            Assert.AreEqual(Ds3DiagnosticsCode.Ok, cacheNearCapacityResult.ClientResult.Code);
            Assert.AreEqual(null, cacheNearCapacityResult.ClientResult.ErrorMessage);
            Assert.AreEqual(null, cacheNearCapacityResult.ClientResult.ErrorInfo);

            client.VerifyAll();
        }

        [Test]
        public void TestGetWithNullFilesystems()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.GetCacheStateSpectraS3(It.IsAny<GetCacheStateSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.NullFilesystems);

            var cacheNearCapacity = new CacheNearCapacityDiagnostic();
            var ds3DiagnosticClient = new Ds3DiagnosticClient
            {
                Client = client.Object
            };
            var cacheNearCapacityResult = cacheNearCapacity.Get(ds3DiagnosticClient);

            Assert.AreEqual(Ds3DiagnosticsCode.NoCacheSystemFound, cacheNearCapacityResult.ClientResult.Code);
            Assert.AreEqual(DiagnosticsMessages.NoCacheFound, cacheNearCapacityResult.ClientResult.ErrorMessage);
            Assert.AreEqual(null, cacheNearCapacityResult.ClientResult.ErrorInfo);

            client.VerifyAll();
        }

        [Test]
        public void TestGetWithOneNearCapacity()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.GetCacheStateSpectraS3(It.IsAny<GetCacheStateSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.OneNearCapacity);

            var cacheNearCapacity = new CacheNearCapacityDiagnostic();
            var ds3DiagnosticClient = new Ds3DiagnosticClient
            {
                Client = client.Object
            };
            var cacheNearCapacityResult = cacheNearCapacity.Get(ds3DiagnosticClient);

            Assert.AreEqual(Ds3DiagnosticsCode.CacheNearCapacity, cacheNearCapacityResult.ClientResult.Code);
            Assert.AreEqual(string.Format(DiagnosticsMessages.FoundCacheNearCapacityLimit, 1),
                cacheNearCapacityResult.ClientResult.ErrorMessage);
            Assert.AreEqual(1, cacheNearCapacityResult.ClientResult.ErrorInfo.Count());

            client.VerifyAll();
        }

        [Test]
        public void TestGetWithTwoNearCapacity()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.GetCacheStateSpectraS3(It.IsAny<GetCacheStateSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.TwoNearCapacity);

            var cacheNearCapacity = new CacheNearCapacityDiagnostic();
            var ds3DiagnosticClient = new Ds3DiagnosticClient
            {
                Client = client.Object
            };
            var cacheNearCapacityResult = cacheNearCapacity.Get(ds3DiagnosticClient);

            Assert.AreEqual(Ds3DiagnosticsCode.CacheNearCapacity, cacheNearCapacityResult.ClientResult.Code);
            Assert.AreEqual(string.Format(DiagnosticsMessages.FoundCacheNearCapacityLimit, 2),
                cacheNearCapacityResult.ClientResult.ErrorMessage);
            Assert.AreEqual(2, cacheNearCapacityResult.ClientResult.ErrorInfo.Count());

            client.VerifyAll();
        }
    }
}