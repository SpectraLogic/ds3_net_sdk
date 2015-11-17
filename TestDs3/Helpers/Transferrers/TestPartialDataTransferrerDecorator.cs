/*
 * ******************************************************************************
 *   Copyright 2014 Spectra Logic Corporation. All Rights Reserved.
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
using System.IO;
using NUnit.Framework;
using Moq;
using Ds3;
using Ds3.Runtime;
using Ds3.Helpers.Transferrers;
using Range = Ds3.Models.Range;

namespace TestDs3.Helpers.Transferrers
{
    [TestFixture]
    class TestPartialDataTransferrerDecorator
    {

        [Test]
        public void TestZeroRetries()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            MockHelpers.SetupGetObjectWithContentLengthMismatchException(client, "bar", 0L, "ABCDEFGHIJ", 20L, 10L); // The initial request is for all 20 bytes, but only the first 10 will be sent

            try
            {
                var stream = new MemoryStream(200);
                var exceptionTransferrer = new ReadTransferrer();
                var decorator = new PartialDataTransferrerDecorator(exceptionTransferrer, 0);
                decorator.Transfer(client.Object, JobResponseStubs.BucketName, "bar", 0, JobResponseStubs.JobId, new List<Range>(), stream);
                Assert.Fail();
            }
            catch (Ds3NoMoreRetriesException e) {
                Assert.AreEqual(0, e.Retries);   
            }
        }

        [Test]
        public void Test1Retries()
        {
            var client = new Mock<IDs3Client>(MockBehavior.Strict);
            MockHelpers.SetupGetObjectWithContentLengthMismatchException(client, "bar", 0L, "ABCDEFGHIJ", 20L, 10L);
            MockHelpers.SetupGetObjectWithContentLengthMismatchException(client, "bar", 0L, "ABCDEFGHIJ", 20L, 10L, Range.ByPosition(9, 19));
            try
            {
                var stream = new MemoryStream(200);
                var exceptionTransferrer = new ReadTransferrer();
                var decorator = new PartialDataTransferrerDecorator(exceptionTransferrer, 1);
                decorator.Transfer(client.Object, JobResponseStubs.BucketName, "bar", 0, JobResponseStubs.JobId, new List<Range>(), stream);
                Assert.Fail();
            }
            catch (Ds3NoMoreRetriesException e)
            {
                Assert.AreEqual(1, e.Retries);
            }
        }

    }
}
