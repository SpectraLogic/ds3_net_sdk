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
using Ds3.Models;
using System;

namespace IntegrationTestDS3
{
    public class TempStorageUtil
    {
        /// <summary>
        /// Sets up a temporary data policy with a temporary storage domain and partition
        /// for use in integration tests where the BP may not currently have access to a
        /// partition.
        /// </summary>
        /// <param name="fixtureName">Name of fixutre being run under this domain. This is
        /// used to prevent race conditions between test setup and teardown</param>
        /// <param name="dataPolicyId"></param>
        /// <param name="client"></param>
        /// <returns>Storage domain and data persistence Ids for use in teardown</returns>
        public static TempStorageIds Setup(
            string fixtureName, 
            Guid dataPolicyId, 
            IDs3Client client)
        {
            //Create storage domain
            PutStorageDomainSpectraS3Response storageDomainResponse = ABMTestHelper.CreateStorageDomain(fixtureName, client);

            //Crate pool partition
            PutPoolPartitionSpectraS3Response poolPartitionResponse = ABMTestHelper.CreatePoolPartition(fixtureName, PoolType.ONLINE, client);

            //Create storage domain member linking pool partition to storage domain
            PutPoolStorageDomainMemberSpectraS3Response memberResponse = ABMTestHelper.CreatePoolStorageDomainMember(
                storageDomainResponse.ResponsePayload.Id,
                poolPartitionResponse.ResponsePayload.Id,
                client);

            //Create data persistence rule
            PutDataPersistenceRuleSpectraS3Response dataPersistenceResponse = ABMTestHelper.CreateDataPersistenceRule(
                dataPolicyId,
                storageDomainResponse.ResponsePayload.Id,
                client);

            return new TempStorageIds(memberResponse.ResponsePayload.Id, dataPersistenceResponse.ResponsePayload.Id);
        }

        /// <summary>
        /// Tears down the temporary test environment
        /// </summary>
        /// <param name="fixtureName"></param>
        /// <param name="ids"></param>
        /// <param name="client"></param>
        public static void TearDown(
            string fixtureName,
            TempStorageIds ids,
            IDs3Client client)
        {
            ABMTestHelper.DeleteDataPersistenceRule(ids.DataPersistenceRuleId, client);
            ABMTestHelper.DeleteDataPolicy(fixtureName, client);
            ABMTestHelper.DeleteStorageDomainMember(ids.StorageDomainMemberId, client);
            ABMTestHelper.DeleteStorageDomain(fixtureName, client);
            ABMTestHelper.DeletePoolPartition(fixtureName, client);
        }

        /// <summary>
        /// Creates a Data Policy with the specified checksum type and end-to-end crc requirement
        /// and makes it the default data policy for spectra user
        /// </summary>
        /// <param name="fixtureName"></param>
        /// <param name="withEndToEndCrcRequired"></param>
        /// <param name="checksumType"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public static Guid SetupDataPolicy(
            string fixtureName,
            bool withEndToEndCrcRequired,
            ChecksumType.Type checksumType,
            IDs3Client client)
        {
            PutDataPolicySpectraS3Request dataPolicyRequest = new PutDataPolicySpectraS3Request(fixtureName)
                .WithEndToEndCrcRequired(withEndToEndCrcRequired);

            if (checksumType != ChecksumType.Type.NONE)
            {
                dataPolicyRequest.WithChecksumType(checksumType);
            }
            PutDataPolicySpectraS3Response dataPolicyResponse = client.PutDataPolicySpectraS3(dataPolicyRequest);

            client.ModifyUserSpectraS3(new ModifyUserSpectraS3Request("spectra")
                .WithDefaultDataPolicyId(dataPolicyResponse.ResponsePayload.Id));

            return dataPolicyResponse.ResponsePayload.Id;
        }
    }

    /// <summary>
    /// Used to store the UUID of the storage domain member and the data persistence rule 
    /// when setting up the testing environment. These IDs are stored for teardown of 
    /// testing environment.
    /// </summary>
    public class TempStorageIds
    {
        public Guid StorageDomainMemberId { get; private set; }
        public Guid DataPersistenceRuleId { get; private set; }

        public TempStorageIds(Guid storageDomainMemberId, Guid dataPersistenceRuleId)
        {
            this.StorageDomainMemberId = storageDomainMemberId;
            this.DataPersistenceRuleId = dataPersistenceRuleId;
        }
    }
}
