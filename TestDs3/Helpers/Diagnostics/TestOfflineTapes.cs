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
    public class TestOfflineTapes
    {
        [Test]
        public void TestGetWithNoOfflineTapes()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.GetTapesSpectraS3(It.IsAny<GetTapesSpectraS3Request>()))
                .Returns(DiagnosticsStubs.EmptyTapes);

            var offlineTapes = new OfflineTapes();
            var offlineTapesResult = offlineTapes.Get(client.Object);

            Assert.AreEqual(Ds3DiagnosticsCode.Ok, offlineTapesResult.Code);
            Assert.AreEqual(null, offlineTapesResult.ErrorMessage);
            Assert.AreEqual(null, offlineTapesResult.ErrorInfo);

            client.VerifyAll();
        }

        [Test]
        public void TestGetWithNullTapes()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.GetTapesSpectraS3(It.IsAny<GetTapesSpectraS3Request>()))
                .Returns(DiagnosticsStubs.NullTapes);

            var offlineTapes = new OfflineTapes();
            var offlineTapesResult = offlineTapes.Get(client.Object);

            Assert.AreEqual(Ds3DiagnosticsCode.NoTapesFound, offlineTapesResult.Code);
            Assert.AreEqual("No tapes found in the system", offlineTapesResult.ErrorMessage);
            Assert.AreEqual(null, offlineTapesResult.ErrorInfo);

            client.VerifyAll();
        }

        [Test]
        public void TestGetWithOneTape()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.GetTapesSpectraS3(It.IsAny<GetTapesSpectraS3Request>()))
                .Returns(DiagnosticsStubs.OneTape);

            var offlineTapes = new OfflineTapes();
            var offlineTapesResult = offlineTapes.Get(client.Object);

            Assert.AreEqual(Ds3DiagnosticsCode.OfflineTapes, offlineTapesResult.Code);
            Assert.AreEqual("Found 1 tapes that are in OFFLINE state", offlineTapesResult.ErrorMessage);
            Assert.AreEqual(1, offlineTapesResult.ErrorInfo.Count());

            client.VerifyAll();
        }

        [Test]
        public void TestGetWithTwoTape()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            client
                .Setup(c => c.GetTapesSpectraS3(It.IsAny<GetTapesSpectraS3Request>()))
                .Returns(DiagnosticsStubs.TwoTapes);

            var offlineTapes = new OfflineTapes();
            var offlineTapesResult = offlineTapes.Get(client.Object);

            Assert.AreEqual(Ds3DiagnosticsCode.OfflineTapes, offlineTapesResult.Code);
            Assert.AreEqual("Found 2 tapes that are in OFFLINE state", offlineTapesResult.ErrorMessage);
            Assert.AreEqual(2, offlineTapesResult.ErrorInfo.Count());

            client.VerifyAll();
        }
    }
}