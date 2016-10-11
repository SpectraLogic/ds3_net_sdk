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

using System;
using Ds3;
using Ds3.Calls;
using Ds3.Models;

namespace IntegrationTestDS3
{
    /// <summary>
    /// Provides utilities for testing the Advanced Bucket Management commands
    /// </summary>
    static class ABMTestHelper
    {
        /// <summary>
        /// Creates a data persistence rule to link the specified data policy and storage domain
        /// </summary>
        /// <param name="dataPolicyId"></param>
        /// <param name="storageDomainId"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public static PutDataPersistenceRuleSpectraS3Response CreateDataPersistenceRule(
            Guid dataPolicyId,
            Guid storageDomainId,
            IDs3Client client)
        {
            return client.PutDataPersistenceRuleSpectraS3(new PutDataPersistenceRuleSpectraS3Request(
                dataPolicyId,
                DataIsolationLevel.STANDARD,
                storageDomainId,
                DataPersistenceRuleType.PERMANENT));
        }

        /// <summary>
        /// Creates a pool partition with the specified name and pool type
        /// </summary>
        /// <param name="storageDomainId"></param>
        /// <param name="poolPartitionId"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public static PutPoolStorageDomainMemberSpectraS3Response CreatePoolStorageDomainMember(
            Guid storageDomainId,
            Guid poolPartitionId,
            IDs3Client client)
        {
            return
                client.PutPoolStorageDomainMemberSpectraS3(
                    new PutPoolStorageDomainMemberSpectraS3Request(poolPartitionId, storageDomainId));
        }

        /// <summary>
        /// Creates a pool partition with the specified name and pool type
        /// </summary>
        /// <param name="poolPartitionName"></param>
        /// <param name="poolType"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public static PutPoolPartitionSpectraS3Response CreatePoolPartition(string poolPartitionName, PoolType poolType,
            IDs3Client client)
        {
            return client.PutPoolPartitionSpectraS3(new PutPoolPartitionSpectraS3Request(poolPartitionName, poolType));
        }

        /// <summary>
        /// Creates a storage domain
        /// </summary>
        /// <param name="storageDomainName"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        public static PutStorageDomainSpectraS3Response CreateStorageDomain(string storageDomainName, IDs3Client client)
        {
            return client.PutStorageDomainSpectraS3(new PutStorageDomainSpectraS3Request(storageDomainName));
        }

        /// <summary>
        /// Deletes a data persistence rule with the specified ID
        /// </summary>
        public static void DeleteDataPersistenceRule(Guid dataPersistenceRuleId, IDs3Client client)
        {
            client.DeleteDataPersistenceRuleSpectraS3(
                new DeleteDataPersistenceRuleSpectraS3Request(dataPersistenceRuleId.ToString()));
        }

        /// <summary>
        /// Deletes a data policy with the specified name
        /// </summary>
        public static void DeleteDataPolicy(string dataPolicyName, IDs3Client client)
        {
            client.DeleteDataPolicySpectraS3(new DeleteDataPolicySpectraS3Request(dataPolicyName));
        }

        /// <summary>
        /// Deletes a storage domain member with the specified ID
        /// </summary>
        public static void DeleteStorageDomainMember(Guid memberId, IDs3Client client)
        {
            client.DeleteStorageDomainMemberSpectraS3(new DeleteStorageDomainMemberSpectraS3Request(memberId.ToString()));
        }

        /// <summary>
        /// Deletes a storage domain with the specified name
        /// </summary>
        public static void DeleteStorageDomain(string storageDomainName, IDs3Client client)
        {
            client.DeleteStorageDomainSpectraS3(new DeleteStorageDomainSpectraS3Request(storageDomainName));
        }

        /// <summary>
        /// Deletes a pool partition with the specified name
        /// </summary>
        /// <param name="poolPartitionName"></param>
        /// <param name="client"></param>
        public static void DeletePoolPartition(string poolPartitionName, IDs3Client client)
        {
            client.DeletePoolPartitionSpectraS3(new DeletePoolPartitionSpectraS3Request(poolPartitionName));
        }
    }
}