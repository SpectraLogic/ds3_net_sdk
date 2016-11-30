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

namespace TestDs3.Helpers.Diagnostics
{
    public static class DiagnosticsStubClients
    {
        public static readonly Mock<IDs3TargetClientBuilder> Ds3TargetClientBuilder =
            new Mock<IDs3TargetClientBuilder>(MockBehavior.Strict);

        public static readonly Mock<IDs3Client> Client = new Mock<IDs3Client>(MockBehavior.Strict);
        private static readonly Mock<IDs3Client> ClientT1 = new Mock<IDs3Client>(MockBehavior.Strict);
        private static readonly Mock<IDs3Client> ClientT2 = new Mock<IDs3Client>(MockBehavior.Strict);
        private static readonly Mock<IDs3Client> ClientT3 = new Mock<IDs3Client>(MockBehavior.Strict);
        private static readonly Mock<IDs3Client> ClientT4 = new Mock<IDs3Client>(MockBehavior.Strict);
        private static readonly Mock<IDs3Client> ClientT5 = new Mock<IDs3Client>(MockBehavior.Strict);

        static DiagnosticsStubClients()
        {
            /* Client Mock Setups */
            Client
                .Setup(c => c.GetCacheStateSpectraS3(It.IsAny<GetCacheStateSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.TwoNearCapacity);

            Client
                .Setup(c => c.GetTapesSpectraS3(It.IsAny<GetTapesSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.OneTape);

            Client
                .Setup(c => c.GetPoolsSpectraS3(It.IsAny<GetPoolsSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.OnePoweredOffPool);

            Client
                .SetupSequence(
                    c =>
                        c.GetDataPlannerBlobStoreTasksSpectraS3(
                            It.IsAny<GetDataPlannerBlobStoreTasksSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.OneReadingTasks)
                .Returns(DiagnosticsStubResponses.OneWritingTasks);

            Client
                .Setup(c => c.GetDs3TargetsSpectraS3(It.IsAny<GetDs3TargetsSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.ClientTargets);


            /* Target 1 Mock Setups */
            ClientT1
                .Setup(c => c.GetCacheStateSpectraS3(It.IsAny<GetCacheStateSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.NoNearCapacity);

            ClientT1
                .Setup(c => c.GetTapesSpectraS3(It.IsAny<GetTapesSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.NoTapes);

            ClientT1
                .Setup(c => c.GetPoolsSpectraS3(It.IsAny<GetPoolsSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.NoPoweredOffPools);

            ClientT1
                .SetupSequence(
                    c =>
                        c.GetDataPlannerBlobStoreTasksSpectraS3(
                            It.IsAny<GetDataPlannerBlobStoreTasksSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.NoReadingTasks)
                .Returns(DiagnosticsStubResponses.NoWritingTasks);

            ClientT1
                .Setup(c => c.GetDs3TargetsSpectraS3(It.IsAny<GetDs3TargetsSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.ClientT1Targets);

            /* Target 2 Mock setups */
            ClientT2
                .Setup(c => c.GetCacheStateSpectraS3(It.IsAny<GetCacheStateSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.NoNearCapacity);

            ClientT2
                .Setup(c => c.GetTapesSpectraS3(It.IsAny<GetTapesSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.NoTapes);

            ClientT2
                .Setup(c => c.GetPoolsSpectraS3(It.IsAny<GetPoolsSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.NoPoweredOffPools);

            ClientT2
                .SetupSequence(
                    c =>
                        c.GetDataPlannerBlobStoreTasksSpectraS3(
                            It.IsAny<GetDataPlannerBlobStoreTasksSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.NoReadingTasks)
                .Returns(DiagnosticsStubResponses.NoWritingTasks);

            ClientT2
                .Setup(c => c.GetDs3TargetsSpectraS3(It.IsAny<GetDs3TargetsSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.ClientT2Targets);

            /* Target 3 Mock setups */
            ClientT3
                .Setup(c => c.GetCacheStateSpectraS3(It.IsAny<GetCacheStateSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.NoNearCapacity);

            ClientT3
                .Setup(c => c.GetTapesSpectraS3(It.IsAny<GetTapesSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.NoTapes);

            ClientT3
                .Setup(c => c.GetPoolsSpectraS3(It.IsAny<GetPoolsSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.NoPoweredOffPools);

            ClientT3
                .SetupSequence(
                    c =>
                        c.GetDataPlannerBlobStoreTasksSpectraS3(
                            It.IsAny<GetDataPlannerBlobStoreTasksSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.NoReadingTasks)
                .Returns(DiagnosticsStubResponses.NoWritingTasks);

            ClientT3
                .Setup(c => c.GetDs3TargetsSpectraS3(It.IsAny<GetDs3TargetsSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.NoTargets);

            /* Target 4 Mock setups */
            ClientT4
                .Setup(c => c.GetCacheStateSpectraS3(It.IsAny<GetCacheStateSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.NoNearCapacity);

            ClientT4
                .Setup(c => c.GetTapesSpectraS3(It.IsAny<GetTapesSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.NoTapes);

            ClientT4
                .Setup(c => c.GetPoolsSpectraS3(It.IsAny<GetPoolsSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.NoPoweredOffPools);

            ClientT4
                .SetupSequence(
                    c =>
                        c.GetDataPlannerBlobStoreTasksSpectraS3(
                            It.IsAny<GetDataPlannerBlobStoreTasksSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.NoReadingTasks)
                .Returns(DiagnosticsStubResponses.NoWritingTasks);

            ClientT4
                .Setup(c => c.GetDs3TargetsSpectraS3(It.IsAny<GetDs3TargetsSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.NoTargets);

            /* Target 5 Mock setups */
            ClientT5
                .Setup(c => c.GetCacheStateSpectraS3(It.IsAny<GetCacheStateSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.NoNearCapacity);

            ClientT5
                .Setup(c => c.GetTapesSpectraS3(It.IsAny<GetTapesSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.NoTapes);

            ClientT5
                .Setup(c => c.GetPoolsSpectraS3(It.IsAny<GetPoolsSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.NoPoweredOffPools);

            ClientT5
                .SetupSequence(
                    c =>
                        c.GetDataPlannerBlobStoreTasksSpectraS3(
                            It.IsAny<GetDataPlannerBlobStoreTasksSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.NoReadingTasks)
                .Returns(DiagnosticsStubResponses.NoWritingTasks);

            ClientT5
                .Setup(c => c.GetDs3TargetsSpectraS3(It.IsAny<GetDs3TargetsSpectraS3Request>()))
                .Returns(DiagnosticsStubResponses.NoTargets);

            /* Ds3TargetClientBuilder Mock setups */
            Ds3TargetClientBuilder
                .Setup(b => b.Build(It.Is<string>(s => s.Equals("T1")), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(ClientT1.Object);

            Ds3TargetClientBuilder
                .Setup(b => b.Build(It.Is<string>(s => s.Equals("T2")), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(ClientT2.Object);

            Ds3TargetClientBuilder
                .Setup(b => b.Build(It.Is<string>(s => s.Equals("T3")), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(ClientT3.Object);

            Ds3TargetClientBuilder
                .Setup(b => b.Build(It.Is<string>(s => s.Equals("T4")), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(ClientT4.Object);

            Ds3TargetClientBuilder
                .Setup(b => b.Build(It.Is<string>(s => s.Equals("T5")), It.IsAny<string>(), It.IsAny<string>()))
                .Returns(ClientT5.Object);
        }

        public static void VerifyAll()
        {
            Client.VerifyAll();
            Client.VerifyAll();
            ClientT1.VerifyAll();
            ClientT2.VerifyAll();
            Ds3TargetClientBuilder.VerifyAll();
        }
    }
}