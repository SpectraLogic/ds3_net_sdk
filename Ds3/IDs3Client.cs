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

// This code is auto-generated, do not modify
using System.Collections.Generic;

using Ds3.Calls;
using Ds3.Models;

namespace Ds3
{
    /// <summary>
    /// The main DS3 API interface. Use Ds3Builder to instantiate.
    /// </summary>
    public interface IDs3Client
    {
        
        CompleteMultiPartUploadResponse CompleteMultiPartUpload(CompleteMultiPartUploadRequest request);
        
        DeleteObjectsResponse DeleteObjects(DeleteObjectsRequest request);
        
        GetBucketResponse GetBucket(GetBucketRequest request);
        
        GetServiceResponse GetService(GetServiceRequest request);
        
        HeadBucketResponse HeadBucket(HeadBucketRequest request);
        
        HeadObjectResponse HeadObject(HeadObjectRequest request);
        
        InitiateMultiPartUploadResponse InitiateMultiPartUpload(InitiateMultiPartUploadRequest request);
        
        ListMultiPartUploadPartsResponse ListMultiPartUploadParts(ListMultiPartUploadPartsRequest request);
        
        ListMultiPartUploadsResponse ListMultiPartUploads(ListMultiPartUploadsRequest request);
        
        PutBucketAclForGroupSpectraS3Response PutBucketAclForGroupSpectraS3(PutBucketAclForGroupSpectraS3Request request);
        
        PutBucketAclForUserSpectraS3Response PutBucketAclForUserSpectraS3(PutBucketAclForUserSpectraS3Request request);
        
        PutDataPolicyAclForGroupSpectraS3Response PutDataPolicyAclForGroupSpectraS3(PutDataPolicyAclForGroupSpectraS3Request request);
        
        PutDataPolicyAclForUserSpectraS3Response PutDataPolicyAclForUserSpectraS3(PutDataPolicyAclForUserSpectraS3Request request);
        
        PutGlobalBucketAclForGroupSpectraS3Response PutGlobalBucketAclForGroupSpectraS3(PutGlobalBucketAclForGroupSpectraS3Request request);
        
        PutGlobalBucketAclForUserSpectraS3Response PutGlobalBucketAclForUserSpectraS3(PutGlobalBucketAclForUserSpectraS3Request request);
        
        PutGlobalDataPolicyAclForGroupSpectraS3Response PutGlobalDataPolicyAclForGroupSpectraS3(PutGlobalDataPolicyAclForGroupSpectraS3Request request);
        
        PutGlobalDataPolicyAclForUserSpectraS3Response PutGlobalDataPolicyAclForUserSpectraS3(PutGlobalDataPolicyAclForUserSpectraS3Request request);
        
        GetBucketAclSpectraS3Response GetBucketAclSpectraS3(GetBucketAclSpectraS3Request request);
        
        GetBucketAclsSpectraS3Response GetBucketAclsSpectraS3(GetBucketAclsSpectraS3Request request);
        
        GetDataPolicyAclSpectraS3Response GetDataPolicyAclSpectraS3(GetDataPolicyAclSpectraS3Request request);
        
        GetDataPolicyAclsSpectraS3Response GetDataPolicyAclsSpectraS3(GetDataPolicyAclsSpectraS3Request request);
        
        PutBucketSpectraS3Response PutBucketSpectraS3(PutBucketSpectraS3Request request);
        
        GetBucketSpectraS3Response GetBucketSpectraS3(GetBucketSpectraS3Request request);
        
        GetBucketsSpectraS3Response GetBucketsSpectraS3(GetBucketsSpectraS3Request request);
        
        ModifyBucketSpectraS3Response ModifyBucketSpectraS3(ModifyBucketSpectraS3Request request);
        
        GetCacheFilesystemSpectraS3Response GetCacheFilesystemSpectraS3(GetCacheFilesystemSpectraS3Request request);
        
        GetCacheFilesystemsSpectraS3Response GetCacheFilesystemsSpectraS3(GetCacheFilesystemsSpectraS3Request request);
        
        GetCacheStateSpectraS3Response GetCacheStateSpectraS3(GetCacheStateSpectraS3Request request);
        
        ModifyCacheFilesystemSpectraS3Response ModifyCacheFilesystemSpectraS3(ModifyCacheFilesystemSpectraS3Request request);
        
        GetBucketCapacitySummarySpectraS3Response GetBucketCapacitySummarySpectraS3(GetBucketCapacitySummarySpectraS3Request request);
        
        GetStorageDomainCapacitySummarySpectraS3Response GetStorageDomainCapacitySummarySpectraS3(GetStorageDomainCapacitySummarySpectraS3Request request);
        
        GetSystemCapacitySummarySpectraS3Response GetSystemCapacitySummarySpectraS3(GetSystemCapacitySummarySpectraS3Request request);
        
        GetDataPathBackendSpectraS3Response GetDataPathBackendSpectraS3(GetDataPathBackendSpectraS3Request request);
        
        GetDataPlannerBlobStoreTasksSpectraS3Response GetDataPlannerBlobStoreTasksSpectraS3(GetDataPlannerBlobStoreTasksSpectraS3Request request);
        
        ModifyDataPathBackendSpectraS3Response ModifyDataPathBackendSpectraS3(ModifyDataPathBackendSpectraS3Request request);
        
        PutAzureDataReplicationRuleSpectraS3Response PutAzureDataReplicationRuleSpectraS3(PutAzureDataReplicationRuleSpectraS3Request request);
        
        PutDataPersistenceRuleSpectraS3Response PutDataPersistenceRuleSpectraS3(PutDataPersistenceRuleSpectraS3Request request);
        
        PutDataPolicySpectraS3Response PutDataPolicySpectraS3(PutDataPolicySpectraS3Request request);
        
        PutDs3DataReplicationRuleSpectraS3Response PutDs3DataReplicationRuleSpectraS3(PutDs3DataReplicationRuleSpectraS3Request request);
        
        PutS3DataReplicationRuleSpectraS3Response PutS3DataReplicationRuleSpectraS3(PutS3DataReplicationRuleSpectraS3Request request);
        
        GetAzureDataReplicationRuleSpectraS3Response GetAzureDataReplicationRuleSpectraS3(GetAzureDataReplicationRuleSpectraS3Request request);
        
        GetAzureDataReplicationRulesSpectraS3Response GetAzureDataReplicationRulesSpectraS3(GetAzureDataReplicationRulesSpectraS3Request request);
        
        GetDataPersistenceRuleSpectraS3Response GetDataPersistenceRuleSpectraS3(GetDataPersistenceRuleSpectraS3Request request);
        
        GetDataPersistenceRulesSpectraS3Response GetDataPersistenceRulesSpectraS3(GetDataPersistenceRulesSpectraS3Request request);
        
        GetDataPoliciesSpectraS3Response GetDataPoliciesSpectraS3(GetDataPoliciesSpectraS3Request request);
        
        GetDataPolicySpectraS3Response GetDataPolicySpectraS3(GetDataPolicySpectraS3Request request);
        
        GetDs3DataReplicationRuleSpectraS3Response GetDs3DataReplicationRuleSpectraS3(GetDs3DataReplicationRuleSpectraS3Request request);
        
        GetDs3DataReplicationRulesSpectraS3Response GetDs3DataReplicationRulesSpectraS3(GetDs3DataReplicationRulesSpectraS3Request request);
        
        GetS3DataReplicationRuleSpectraS3Response GetS3DataReplicationRuleSpectraS3(GetS3DataReplicationRuleSpectraS3Request request);
        
        GetS3DataReplicationRulesSpectraS3Response GetS3DataReplicationRulesSpectraS3(GetS3DataReplicationRulesSpectraS3Request request);
        
        ModifyAzureDataReplicationRuleSpectraS3Response ModifyAzureDataReplicationRuleSpectraS3(ModifyAzureDataReplicationRuleSpectraS3Request request);
        
        ModifyDataPersistenceRuleSpectraS3Response ModifyDataPersistenceRuleSpectraS3(ModifyDataPersistenceRuleSpectraS3Request request);
        
        ModifyDataPolicySpectraS3Response ModifyDataPolicySpectraS3(ModifyDataPolicySpectraS3Request request);
        
        ModifyDs3DataReplicationRuleSpectraS3Response ModifyDs3DataReplicationRuleSpectraS3(ModifyDs3DataReplicationRuleSpectraS3Request request);
        
        ModifyS3DataReplicationRuleSpectraS3Response ModifyS3DataReplicationRuleSpectraS3(ModifyS3DataReplicationRuleSpectraS3Request request);
        
        GetDegradedAzureDataReplicationRulesSpectraS3Response GetDegradedAzureDataReplicationRulesSpectraS3(GetDegradedAzureDataReplicationRulesSpectraS3Request request);
        
        GetDegradedBlobsSpectraS3Response GetDegradedBlobsSpectraS3(GetDegradedBlobsSpectraS3Request request);
        
        GetDegradedBucketsSpectraS3Response GetDegradedBucketsSpectraS3(GetDegradedBucketsSpectraS3Request request);
        
        GetDegradedDataPersistenceRulesSpectraS3Response GetDegradedDataPersistenceRulesSpectraS3(GetDegradedDataPersistenceRulesSpectraS3Request request);
        
        GetDegradedDs3DataReplicationRulesSpectraS3Response GetDegradedDs3DataReplicationRulesSpectraS3(GetDegradedDs3DataReplicationRulesSpectraS3Request request);
        
        GetDegradedS3DataReplicationRulesSpectraS3Response GetDegradedS3DataReplicationRulesSpectraS3(GetDegradedS3DataReplicationRulesSpectraS3Request request);
        
        GetSuspectBlobAzureTargetsSpectraS3Response GetSuspectBlobAzureTargetsSpectraS3(GetSuspectBlobAzureTargetsSpectraS3Request request);
        
        GetSuspectBlobDs3TargetsSpectraS3Response GetSuspectBlobDs3TargetsSpectraS3(GetSuspectBlobDs3TargetsSpectraS3Request request);
        
        GetSuspectBlobPoolsSpectraS3Response GetSuspectBlobPoolsSpectraS3(GetSuspectBlobPoolsSpectraS3Request request);
        
        GetSuspectBlobS3TargetsSpectraS3Response GetSuspectBlobS3TargetsSpectraS3(GetSuspectBlobS3TargetsSpectraS3Request request);
        
        GetSuspectBlobTapesSpectraS3Response GetSuspectBlobTapesSpectraS3(GetSuspectBlobTapesSpectraS3Request request);
        
        GetSuspectBucketsSpectraS3Response GetSuspectBucketsSpectraS3(GetSuspectBucketsSpectraS3Request request);
        
        GetSuspectObjectsSpectraS3Response GetSuspectObjectsSpectraS3(GetSuspectObjectsSpectraS3Request request);
        
        GetSuspectObjectsWithFullDetailsSpectraS3Response GetSuspectObjectsWithFullDetailsSpectraS3(GetSuspectObjectsWithFullDetailsSpectraS3Request request);
        
        PutGroupGroupMemberSpectraS3Response PutGroupGroupMemberSpectraS3(PutGroupGroupMemberSpectraS3Request request);
        
        PutGroupSpectraS3Response PutGroupSpectraS3(PutGroupSpectraS3Request request);
        
        PutUserGroupMemberSpectraS3Response PutUserGroupMemberSpectraS3(PutUserGroupMemberSpectraS3Request request);
        
        GetGroupMemberSpectraS3Response GetGroupMemberSpectraS3(GetGroupMemberSpectraS3Request request);
        
        GetGroupMembersSpectraS3Response GetGroupMembersSpectraS3(GetGroupMembersSpectraS3Request request);
        
        GetGroupSpectraS3Response GetGroupSpectraS3(GetGroupSpectraS3Request request);
        
        GetGroupsSpectraS3Response GetGroupsSpectraS3(GetGroupsSpectraS3Request request);
        
        ModifyGroupSpectraS3Response ModifyGroupSpectraS3(ModifyGroupSpectraS3Request request);
        
        VerifyUserIsMemberOfGroupSpectraS3Response VerifyUserIsMemberOfGroupSpectraS3(VerifyUserIsMemberOfGroupSpectraS3Request request);
        
        AllocateJobChunkSpectraS3Response AllocateJobChunkSpectraS3(AllocateJobChunkSpectraS3Request request);
        
        GetBulkJobSpectraS3Response GetBulkJobSpectraS3(GetBulkJobSpectraS3Request request);
        
        PutBulkJobSpectraS3Response PutBulkJobSpectraS3(PutBulkJobSpectraS3Request request);
        
        VerifyBulkJobSpectraS3Response VerifyBulkJobSpectraS3(VerifyBulkJobSpectraS3Request request);
        
        GetActiveJobSpectraS3Response GetActiveJobSpectraS3(GetActiveJobSpectraS3Request request);
        
        GetActiveJobsSpectraS3Response GetActiveJobsSpectraS3(GetActiveJobsSpectraS3Request request);
        
        GetCanceledJobSpectraS3Response GetCanceledJobSpectraS3(GetCanceledJobSpectraS3Request request);
        
        GetCanceledJobsSpectraS3Response GetCanceledJobsSpectraS3(GetCanceledJobsSpectraS3Request request);
        
        GetCompletedJobSpectraS3Response GetCompletedJobSpectraS3(GetCompletedJobSpectraS3Request request);
        
        GetCompletedJobsSpectraS3Response GetCompletedJobsSpectraS3(GetCompletedJobsSpectraS3Request request);
        
        GetJobChunkDaoSpectraS3Response GetJobChunkDaoSpectraS3(GetJobChunkDaoSpectraS3Request request);
        
        GetJobChunkSpectraS3Response GetJobChunkSpectraS3(GetJobChunkSpectraS3Request request);
        
        GetJobChunksReadyForClientProcessingSpectraS3Response GetJobChunksReadyForClientProcessingSpectraS3(GetJobChunksReadyForClientProcessingSpectraS3Request request);
        
        GetJobSpectraS3Response GetJobSpectraS3(GetJobSpectraS3Request request);
        
        GetJobToReplicateSpectraS3Response GetJobToReplicateSpectraS3(GetJobToReplicateSpectraS3Request request);
        
        GetJobsSpectraS3Response GetJobsSpectraS3(GetJobsSpectraS3Request request);
        
        ModifyActiveJobSpectraS3Response ModifyActiveJobSpectraS3(ModifyActiveJobSpectraS3Request request);
        
        ModifyJobSpectraS3Response ModifyJobSpectraS3(ModifyJobSpectraS3Request request);
        
        ReplicatePutJobSpectraS3Response ReplicatePutJobSpectraS3(ReplicatePutJobSpectraS3Request request);
        
        GetNodeSpectraS3Response GetNodeSpectraS3(GetNodeSpectraS3Request request);
        
        GetNodesSpectraS3Response GetNodesSpectraS3(GetNodesSpectraS3Request request);
        
        ModifyNodeSpectraS3Response ModifyNodeSpectraS3(ModifyNodeSpectraS3Request request);
        
        PutAzureTargetFailureNotificationRegistrationSpectraS3Response PutAzureTargetFailureNotificationRegistrationSpectraS3(PutAzureTargetFailureNotificationRegistrationSpectraS3Request request);
        
        PutDs3TargetFailureNotificationRegistrationSpectraS3Response PutDs3TargetFailureNotificationRegistrationSpectraS3(PutDs3TargetFailureNotificationRegistrationSpectraS3Request request);
        
        PutJobCompletedNotificationRegistrationSpectraS3Response PutJobCompletedNotificationRegistrationSpectraS3(PutJobCompletedNotificationRegistrationSpectraS3Request request);
        
        PutJobCreatedNotificationRegistrationSpectraS3Response PutJobCreatedNotificationRegistrationSpectraS3(PutJobCreatedNotificationRegistrationSpectraS3Request request);
        
        PutJobCreationFailedNotificationRegistrationSpectraS3Response PutJobCreationFailedNotificationRegistrationSpectraS3(PutJobCreationFailedNotificationRegistrationSpectraS3Request request);
        
        PutObjectCachedNotificationRegistrationSpectraS3Response PutObjectCachedNotificationRegistrationSpectraS3(PutObjectCachedNotificationRegistrationSpectraS3Request request);
        
        PutObjectLostNotificationRegistrationSpectraS3Response PutObjectLostNotificationRegistrationSpectraS3(PutObjectLostNotificationRegistrationSpectraS3Request request);
        
        PutObjectPersistedNotificationRegistrationSpectraS3Response PutObjectPersistedNotificationRegistrationSpectraS3(PutObjectPersistedNotificationRegistrationSpectraS3Request request);
        
        PutPoolFailureNotificationRegistrationSpectraS3Response PutPoolFailureNotificationRegistrationSpectraS3(PutPoolFailureNotificationRegistrationSpectraS3Request request);
        
        PutS3TargetFailureNotificationRegistrationSpectraS3Response PutS3TargetFailureNotificationRegistrationSpectraS3(PutS3TargetFailureNotificationRegistrationSpectraS3Request request);
        
        PutStorageDomainFailureNotificationRegistrationSpectraS3Response PutStorageDomainFailureNotificationRegistrationSpectraS3(PutStorageDomainFailureNotificationRegistrationSpectraS3Request request);
        
        PutSystemFailureNotificationRegistrationSpectraS3Response PutSystemFailureNotificationRegistrationSpectraS3(PutSystemFailureNotificationRegistrationSpectraS3Request request);
        
        PutTapeFailureNotificationRegistrationSpectraS3Response PutTapeFailureNotificationRegistrationSpectraS3(PutTapeFailureNotificationRegistrationSpectraS3Request request);
        
        PutTapePartitionFailureNotificationRegistrationSpectraS3Response PutTapePartitionFailureNotificationRegistrationSpectraS3(PutTapePartitionFailureNotificationRegistrationSpectraS3Request request);
        
        GetAzureTargetFailureNotificationRegistrationSpectraS3Response GetAzureTargetFailureNotificationRegistrationSpectraS3(GetAzureTargetFailureNotificationRegistrationSpectraS3Request request);
        
        GetAzureTargetFailureNotificationRegistrationsSpectraS3Response GetAzureTargetFailureNotificationRegistrationsSpectraS3(GetAzureTargetFailureNotificationRegistrationsSpectraS3Request request);
        
        GetDs3TargetFailureNotificationRegistrationSpectraS3Response GetDs3TargetFailureNotificationRegistrationSpectraS3(GetDs3TargetFailureNotificationRegistrationSpectraS3Request request);
        
        GetDs3TargetFailureNotificationRegistrationsSpectraS3Response GetDs3TargetFailureNotificationRegistrationsSpectraS3(GetDs3TargetFailureNotificationRegistrationsSpectraS3Request request);
        
        GetJobCompletedNotificationRegistrationSpectraS3Response GetJobCompletedNotificationRegistrationSpectraS3(GetJobCompletedNotificationRegistrationSpectraS3Request request);
        
        GetJobCompletedNotificationRegistrationsSpectraS3Response GetJobCompletedNotificationRegistrationsSpectraS3(GetJobCompletedNotificationRegistrationsSpectraS3Request request);
        
        GetJobCreatedNotificationRegistrationSpectraS3Response GetJobCreatedNotificationRegistrationSpectraS3(GetJobCreatedNotificationRegistrationSpectraS3Request request);
        
        GetJobCreatedNotificationRegistrationsSpectraS3Response GetJobCreatedNotificationRegistrationsSpectraS3(GetJobCreatedNotificationRegistrationsSpectraS3Request request);
        
        GetJobCreationFailedNotificationRegistrationSpectraS3Response GetJobCreationFailedNotificationRegistrationSpectraS3(GetJobCreationFailedNotificationRegistrationSpectraS3Request request);
        
        GetJobCreationFailedNotificationRegistrationsSpectraS3Response GetJobCreationFailedNotificationRegistrationsSpectraS3(GetJobCreationFailedNotificationRegistrationsSpectraS3Request request);
        
        GetObjectCachedNotificationRegistrationSpectraS3Response GetObjectCachedNotificationRegistrationSpectraS3(GetObjectCachedNotificationRegistrationSpectraS3Request request);
        
        GetObjectCachedNotificationRegistrationsSpectraS3Response GetObjectCachedNotificationRegistrationsSpectraS3(GetObjectCachedNotificationRegistrationsSpectraS3Request request);
        
        GetObjectLostNotificationRegistrationSpectraS3Response GetObjectLostNotificationRegistrationSpectraS3(GetObjectLostNotificationRegistrationSpectraS3Request request);
        
        GetObjectLostNotificationRegistrationsSpectraS3Response GetObjectLostNotificationRegistrationsSpectraS3(GetObjectLostNotificationRegistrationsSpectraS3Request request);
        
        GetObjectPersistedNotificationRegistrationSpectraS3Response GetObjectPersistedNotificationRegistrationSpectraS3(GetObjectPersistedNotificationRegistrationSpectraS3Request request);
        
        GetObjectPersistedNotificationRegistrationsSpectraS3Response GetObjectPersistedNotificationRegistrationsSpectraS3(GetObjectPersistedNotificationRegistrationsSpectraS3Request request);
        
        GetPoolFailureNotificationRegistrationSpectraS3Response GetPoolFailureNotificationRegistrationSpectraS3(GetPoolFailureNotificationRegistrationSpectraS3Request request);
        
        GetPoolFailureNotificationRegistrationsSpectraS3Response GetPoolFailureNotificationRegistrationsSpectraS3(GetPoolFailureNotificationRegistrationsSpectraS3Request request);
        
        GetS3TargetFailureNotificationRegistrationSpectraS3Response GetS3TargetFailureNotificationRegistrationSpectraS3(GetS3TargetFailureNotificationRegistrationSpectraS3Request request);
        
        GetS3TargetFailureNotificationRegistrationsSpectraS3Response GetS3TargetFailureNotificationRegistrationsSpectraS3(GetS3TargetFailureNotificationRegistrationsSpectraS3Request request);
        
        GetStorageDomainFailureNotificationRegistrationSpectraS3Response GetStorageDomainFailureNotificationRegistrationSpectraS3(GetStorageDomainFailureNotificationRegistrationSpectraS3Request request);
        
        GetStorageDomainFailureNotificationRegistrationsSpectraS3Response GetStorageDomainFailureNotificationRegistrationsSpectraS3(GetStorageDomainFailureNotificationRegistrationsSpectraS3Request request);
        
        GetSystemFailureNotificationRegistrationSpectraS3Response GetSystemFailureNotificationRegistrationSpectraS3(GetSystemFailureNotificationRegistrationSpectraS3Request request);
        
        GetSystemFailureNotificationRegistrationsSpectraS3Response GetSystemFailureNotificationRegistrationsSpectraS3(GetSystemFailureNotificationRegistrationsSpectraS3Request request);
        
        GetTapeFailureNotificationRegistrationSpectraS3Response GetTapeFailureNotificationRegistrationSpectraS3(GetTapeFailureNotificationRegistrationSpectraS3Request request);
        
        GetTapeFailureNotificationRegistrationsSpectraS3Response GetTapeFailureNotificationRegistrationsSpectraS3(GetTapeFailureNotificationRegistrationsSpectraS3Request request);
        
        GetTapePartitionFailureNotificationRegistrationSpectraS3Response GetTapePartitionFailureNotificationRegistrationSpectraS3(GetTapePartitionFailureNotificationRegistrationSpectraS3Request request);
        
        GetTapePartitionFailureNotificationRegistrationsSpectraS3Response GetTapePartitionFailureNotificationRegistrationsSpectraS3(GetTapePartitionFailureNotificationRegistrationsSpectraS3Request request);
        
        GetBlobPersistenceSpectraS3Response GetBlobPersistenceSpectraS3(GetBlobPersistenceSpectraS3Request request);
        
        GetObjectDetailsSpectraS3Response GetObjectDetailsSpectraS3(GetObjectDetailsSpectraS3Request request);
        
        GetObjectsDetailsSpectraS3Response GetObjectsDetailsSpectraS3(GetObjectsDetailsSpectraS3Request request);
        
        GetObjectsWithFullDetailsSpectraS3Response GetObjectsWithFullDetailsSpectraS3(GetObjectsWithFullDetailsSpectraS3Request request);
        
        GetPhysicalPlacementForObjectsSpectraS3Response GetPhysicalPlacementForObjectsSpectraS3(GetPhysicalPlacementForObjectsSpectraS3Request request);
        
        GetPhysicalPlacementForObjectsWithFullDetailsSpectraS3Response GetPhysicalPlacementForObjectsWithFullDetailsSpectraS3(GetPhysicalPlacementForObjectsWithFullDetailsSpectraS3Request request);
        
        VerifyPhysicalPlacementForObjectsSpectraS3Response VerifyPhysicalPlacementForObjectsSpectraS3(VerifyPhysicalPlacementForObjectsSpectraS3Request request);
        
        VerifyPhysicalPlacementForObjectsWithFullDetailsSpectraS3Response VerifyPhysicalPlacementForObjectsWithFullDetailsSpectraS3(VerifyPhysicalPlacementForObjectsWithFullDetailsSpectraS3Request request);
        
        CancelImportPoolSpectraS3Response CancelImportPoolSpectraS3(CancelImportPoolSpectraS3Request request);
        
        CancelVerifyPoolSpectraS3Response CancelVerifyPoolSpectraS3(CancelVerifyPoolSpectraS3Request request);
        
        CompactPoolSpectraS3Response CompactPoolSpectraS3(CompactPoolSpectraS3Request request);
        
        PutPoolPartitionSpectraS3Response PutPoolPartitionSpectraS3(PutPoolPartitionSpectraS3Request request);
        
        FormatForeignPoolSpectraS3Response FormatForeignPoolSpectraS3(FormatForeignPoolSpectraS3Request request);
        
        GetBlobsOnPoolSpectraS3Response GetBlobsOnPoolSpectraS3(GetBlobsOnPoolSpectraS3Request request);
        
        GetPoolFailuresSpectraS3Response GetPoolFailuresSpectraS3(GetPoolFailuresSpectraS3Request request);
        
        GetPoolPartitionSpectraS3Response GetPoolPartitionSpectraS3(GetPoolPartitionSpectraS3Request request);
        
        GetPoolPartitionsSpectraS3Response GetPoolPartitionsSpectraS3(GetPoolPartitionsSpectraS3Request request);
        
        GetPoolSpectraS3Response GetPoolSpectraS3(GetPoolSpectraS3Request request);
        
        GetPoolsSpectraS3Response GetPoolsSpectraS3(GetPoolsSpectraS3Request request);
        
        ImportPoolSpectraS3Response ImportPoolSpectraS3(ImportPoolSpectraS3Request request);
        
        ModifyPoolPartitionSpectraS3Response ModifyPoolPartitionSpectraS3(ModifyPoolPartitionSpectraS3Request request);
        
        ModifyPoolSpectraS3Response ModifyPoolSpectraS3(ModifyPoolSpectraS3Request request);
        
        VerifyPoolSpectraS3Response VerifyPoolSpectraS3(VerifyPoolSpectraS3Request request);
        
        PutPoolStorageDomainMemberSpectraS3Response PutPoolStorageDomainMemberSpectraS3(PutPoolStorageDomainMemberSpectraS3Request request);
        
        PutStorageDomainSpectraS3Response PutStorageDomainSpectraS3(PutStorageDomainSpectraS3Request request);
        
        PutTapeStorageDomainMemberSpectraS3Response PutTapeStorageDomainMemberSpectraS3(PutTapeStorageDomainMemberSpectraS3Request request);
        
        GetStorageDomainFailuresSpectraS3Response GetStorageDomainFailuresSpectraS3(GetStorageDomainFailuresSpectraS3Request request);
        
        GetStorageDomainMemberSpectraS3Response GetStorageDomainMemberSpectraS3(GetStorageDomainMemberSpectraS3Request request);
        
        GetStorageDomainMembersSpectraS3Response GetStorageDomainMembersSpectraS3(GetStorageDomainMembersSpectraS3Request request);
        
        GetStorageDomainSpectraS3Response GetStorageDomainSpectraS3(GetStorageDomainSpectraS3Request request);
        
        GetStorageDomainsSpectraS3Response GetStorageDomainsSpectraS3(GetStorageDomainsSpectraS3Request request);
        
        ModifyStorageDomainMemberSpectraS3Response ModifyStorageDomainMemberSpectraS3(ModifyStorageDomainMemberSpectraS3Request request);
        
        ModifyStorageDomainSpectraS3Response ModifyStorageDomainSpectraS3(ModifyStorageDomainSpectraS3Request request);
        
        GetFeatureKeysSpectraS3Response GetFeatureKeysSpectraS3(GetFeatureKeysSpectraS3Request request);
        
        GetSystemFailuresSpectraS3Response GetSystemFailuresSpectraS3(GetSystemFailuresSpectraS3Request request);
        
        GetSystemInformationSpectraS3Response GetSystemInformationSpectraS3(GetSystemInformationSpectraS3Request request);
        
        ResetInstanceIdentifierSpectraS3Response ResetInstanceIdentifierSpectraS3(ResetInstanceIdentifierSpectraS3Request request);
        
        VerifySystemHealthSpectraS3Response VerifySystemHealthSpectraS3(VerifySystemHealthSpectraS3Request request);
        
        CancelEjectOnAllTapesSpectraS3Response CancelEjectOnAllTapesSpectraS3(CancelEjectOnAllTapesSpectraS3Request request);
        
        CancelEjectTapeSpectraS3Response CancelEjectTapeSpectraS3(CancelEjectTapeSpectraS3Request request);
        
        CancelFormatOnAllTapesSpectraS3Response CancelFormatOnAllTapesSpectraS3(CancelFormatOnAllTapesSpectraS3Request request);
        
        CancelFormatTapeSpectraS3Response CancelFormatTapeSpectraS3(CancelFormatTapeSpectraS3Request request);
        
        CancelImportOnAllTapesSpectraS3Response CancelImportOnAllTapesSpectraS3(CancelImportOnAllTapesSpectraS3Request request);
        
        CancelImportTapeSpectraS3Response CancelImportTapeSpectraS3(CancelImportTapeSpectraS3Request request);
        
        CancelOnlineOnAllTapesSpectraS3Response CancelOnlineOnAllTapesSpectraS3(CancelOnlineOnAllTapesSpectraS3Request request);
        
        CancelOnlineTapeSpectraS3Response CancelOnlineTapeSpectraS3(CancelOnlineTapeSpectraS3Request request);
        
        CancelVerifyOnAllTapesSpectraS3Response CancelVerifyOnAllTapesSpectraS3(CancelVerifyOnAllTapesSpectraS3Request request);
        
        CancelVerifyTapeSpectraS3Response CancelVerifyTapeSpectraS3(CancelVerifyTapeSpectraS3Request request);
        
        CleanTapeDriveSpectraS3Response CleanTapeDriveSpectraS3(CleanTapeDriveSpectraS3Request request);
        
        PutTapeDensityDirectiveSpectraS3Response PutTapeDensityDirectiveSpectraS3(PutTapeDensityDirectiveSpectraS3Request request);
        
        EjectAllTapesSpectraS3Response EjectAllTapesSpectraS3(EjectAllTapesSpectraS3Request request);
        
        EjectStorageDomainSpectraS3Response EjectStorageDomainSpectraS3(EjectStorageDomainSpectraS3Request request);
        
        EjectTapeSpectraS3Response EjectTapeSpectraS3(EjectTapeSpectraS3Request request);
        
        FormatAllTapesSpectraS3Response FormatAllTapesSpectraS3(FormatAllTapesSpectraS3Request request);
        
        FormatTapeSpectraS3Response FormatTapeSpectraS3(FormatTapeSpectraS3Request request);
        
        GetBlobsOnTapeSpectraS3Response GetBlobsOnTapeSpectraS3(GetBlobsOnTapeSpectraS3Request request);
        
        GetTapeDensityDirectiveSpectraS3Response GetTapeDensityDirectiveSpectraS3(GetTapeDensityDirectiveSpectraS3Request request);
        
        GetTapeDensityDirectivesSpectraS3Response GetTapeDensityDirectivesSpectraS3(GetTapeDensityDirectivesSpectraS3Request request);
        
        GetTapeDriveSpectraS3Response GetTapeDriveSpectraS3(GetTapeDriveSpectraS3Request request);
        
        GetTapeDrivesSpectraS3Response GetTapeDrivesSpectraS3(GetTapeDrivesSpectraS3Request request);
        
        GetTapeFailuresSpectraS3Response GetTapeFailuresSpectraS3(GetTapeFailuresSpectraS3Request request);
        
        GetTapeLibrariesSpectraS3Response GetTapeLibrariesSpectraS3(GetTapeLibrariesSpectraS3Request request);
        
        GetTapeLibrarySpectraS3Response GetTapeLibrarySpectraS3(GetTapeLibrarySpectraS3Request request);
        
        GetTapePartitionFailuresSpectraS3Response GetTapePartitionFailuresSpectraS3(GetTapePartitionFailuresSpectraS3Request request);
        
        GetTapePartitionSpectraS3Response GetTapePartitionSpectraS3(GetTapePartitionSpectraS3Request request);
        
        GetTapePartitionWithFullDetailsSpectraS3Response GetTapePartitionWithFullDetailsSpectraS3(GetTapePartitionWithFullDetailsSpectraS3Request request);
        
        GetTapePartitionsSpectraS3Response GetTapePartitionsSpectraS3(GetTapePartitionsSpectraS3Request request);
        
        GetTapePartitionsWithFullDetailsSpectraS3Response GetTapePartitionsWithFullDetailsSpectraS3(GetTapePartitionsWithFullDetailsSpectraS3Request request);
        
        GetTapeSpectraS3Response GetTapeSpectraS3(GetTapeSpectraS3Request request);
        
        GetTapesSpectraS3Response GetTapesSpectraS3(GetTapesSpectraS3Request request);
        
        ImportTapeSpectraS3Response ImportTapeSpectraS3(ImportTapeSpectraS3Request request);
        
        InspectAllTapesSpectraS3Response InspectAllTapesSpectraS3(InspectAllTapesSpectraS3Request request);
        
        InspectTapeSpectraS3Response InspectTapeSpectraS3(InspectTapeSpectraS3Request request);
        
        ModifyTapePartitionSpectraS3Response ModifyTapePartitionSpectraS3(ModifyTapePartitionSpectraS3Request request);
        
        ModifyTapeSpectraS3Response ModifyTapeSpectraS3(ModifyTapeSpectraS3Request request);
        
        OnlineAllTapesSpectraS3Response OnlineAllTapesSpectraS3(OnlineAllTapesSpectraS3Request request);
        
        OnlineTapeSpectraS3Response OnlineTapeSpectraS3(OnlineTapeSpectraS3Request request);
        
        RawImportTapeSpectraS3Response RawImportTapeSpectraS3(RawImportTapeSpectraS3Request request);
        
        VerifyAllTapesSpectraS3Response VerifyAllTapesSpectraS3(VerifyAllTapesSpectraS3Request request);
        
        VerifyTapeSpectraS3Response VerifyTapeSpectraS3(VerifyTapeSpectraS3Request request);
        
        PutAzureTargetBucketNameSpectraS3Response PutAzureTargetBucketNameSpectraS3(PutAzureTargetBucketNameSpectraS3Request request);
        
        PutAzureTargetReadPreferenceSpectraS3Response PutAzureTargetReadPreferenceSpectraS3(PutAzureTargetReadPreferenceSpectraS3Request request);
        
        GetAzureTargetBucketNamesSpectraS3Response GetAzureTargetBucketNamesSpectraS3(GetAzureTargetBucketNamesSpectraS3Request request);
        
        GetAzureTargetFailuresSpectraS3Response GetAzureTargetFailuresSpectraS3(GetAzureTargetFailuresSpectraS3Request request);
        
        GetAzureTargetReadPreferenceSpectraS3Response GetAzureTargetReadPreferenceSpectraS3(GetAzureTargetReadPreferenceSpectraS3Request request);
        
        GetAzureTargetReadPreferencesSpectraS3Response GetAzureTargetReadPreferencesSpectraS3(GetAzureTargetReadPreferencesSpectraS3Request request);
        
        GetAzureTargetSpectraS3Response GetAzureTargetSpectraS3(GetAzureTargetSpectraS3Request request);
        
        GetAzureTargetsSpectraS3Response GetAzureTargetsSpectraS3(GetAzureTargetsSpectraS3Request request);
        
        GetBlobsOnAzureTargetSpectraS3Response GetBlobsOnAzureTargetSpectraS3(GetBlobsOnAzureTargetSpectraS3Request request);
        
        ModifyAzureTargetSpectraS3Response ModifyAzureTargetSpectraS3(ModifyAzureTargetSpectraS3Request request);
        
        RegisterAzureTargetSpectraS3Response RegisterAzureTargetSpectraS3(RegisterAzureTargetSpectraS3Request request);
        
        VerifyAzureTargetSpectraS3Response VerifyAzureTargetSpectraS3(VerifyAzureTargetSpectraS3Request request);
        
        PutDs3TargetReadPreferenceSpectraS3Response PutDs3TargetReadPreferenceSpectraS3(PutDs3TargetReadPreferenceSpectraS3Request request);
        
        GetBlobsOnDs3TargetSpectraS3Response GetBlobsOnDs3TargetSpectraS3(GetBlobsOnDs3TargetSpectraS3Request request);
        
        GetDs3TargetDataPoliciesSpectraS3Response GetDs3TargetDataPoliciesSpectraS3(GetDs3TargetDataPoliciesSpectraS3Request request);
        
        GetDs3TargetFailuresSpectraS3Response GetDs3TargetFailuresSpectraS3(GetDs3TargetFailuresSpectraS3Request request);
        
        GetDs3TargetReadPreferenceSpectraS3Response GetDs3TargetReadPreferenceSpectraS3(GetDs3TargetReadPreferenceSpectraS3Request request);
        
        GetDs3TargetReadPreferencesSpectraS3Response GetDs3TargetReadPreferencesSpectraS3(GetDs3TargetReadPreferencesSpectraS3Request request);
        
        GetDs3TargetSpectraS3Response GetDs3TargetSpectraS3(GetDs3TargetSpectraS3Request request);
        
        GetDs3TargetsSpectraS3Response GetDs3TargetsSpectraS3(GetDs3TargetsSpectraS3Request request);
        
        ModifyDs3TargetSpectraS3Response ModifyDs3TargetSpectraS3(ModifyDs3TargetSpectraS3Request request);
        
        RegisterDs3TargetSpectraS3Response RegisterDs3TargetSpectraS3(RegisterDs3TargetSpectraS3Request request);
        
        VerifyDs3TargetSpectraS3Response VerifyDs3TargetSpectraS3(VerifyDs3TargetSpectraS3Request request);
        
        PutS3TargetBucketNameSpectraS3Response PutS3TargetBucketNameSpectraS3(PutS3TargetBucketNameSpectraS3Request request);
        
        PutS3TargetReadPreferenceSpectraS3Response PutS3TargetReadPreferenceSpectraS3(PutS3TargetReadPreferenceSpectraS3Request request);
        
        GetBlobsOnS3TargetSpectraS3Response GetBlobsOnS3TargetSpectraS3(GetBlobsOnS3TargetSpectraS3Request request);
        
        GetS3TargetBucketNamesSpectraS3Response GetS3TargetBucketNamesSpectraS3(GetS3TargetBucketNamesSpectraS3Request request);
        
        GetS3TargetFailuresSpectraS3Response GetS3TargetFailuresSpectraS3(GetS3TargetFailuresSpectraS3Request request);
        
        GetS3TargetReadPreferenceSpectraS3Response GetS3TargetReadPreferenceSpectraS3(GetS3TargetReadPreferenceSpectraS3Request request);
        
        GetS3TargetReadPreferencesSpectraS3Response GetS3TargetReadPreferencesSpectraS3(GetS3TargetReadPreferencesSpectraS3Request request);
        
        GetS3TargetSpectraS3Response GetS3TargetSpectraS3(GetS3TargetSpectraS3Request request);
        
        GetS3TargetsSpectraS3Response GetS3TargetsSpectraS3(GetS3TargetsSpectraS3Request request);
        
        ModifyS3TargetSpectraS3Response ModifyS3TargetSpectraS3(ModifyS3TargetSpectraS3Request request);
        
        RegisterS3TargetSpectraS3Response RegisterS3TargetSpectraS3(RegisterS3TargetSpectraS3Request request);
        
        VerifyS3TargetSpectraS3Response VerifyS3TargetSpectraS3(VerifyS3TargetSpectraS3Request request);
        
        DelegateCreateUserSpectraS3Response DelegateCreateUserSpectraS3(DelegateCreateUserSpectraS3Request request);
        
        GetUserSpectraS3Response GetUserSpectraS3(GetUserSpectraS3Request request);
        
        GetUsersSpectraS3Response GetUsersSpectraS3(GetUsersSpectraS3Request request);
        
        ModifyUserSpectraS3Response ModifyUserSpectraS3(ModifyUserSpectraS3Request request);
        
        RegenerateUserSecretKeySpectraS3Response RegenerateUserSecretKeySpectraS3(RegenerateUserSecretKeySpectraS3Request request);
        
        void AbortMultiPartUpload(AbortMultiPartUploadRequest request);
        
        void PutBucket(PutBucketRequest request);
        
        void PutMultiPartUploadPart(PutMultiPartUploadPartRequest request);
        
        void PutObject(PutObjectRequest request);
        
        void DeleteBucket(DeleteBucketRequest request);
        
        void DeleteObject(DeleteObjectRequest request);
        
        void DeleteBucketAclSpectraS3(DeleteBucketAclSpectraS3Request request);
        
        void DeleteDataPolicyAclSpectraS3(DeleteDataPolicyAclSpectraS3Request request);
        
        void DeleteBucketSpectraS3(DeleteBucketSpectraS3Request request);
        
        void ForceFullCacheReclaimSpectraS3(ForceFullCacheReclaimSpectraS3Request request);
        
        void DeleteAzureDataReplicationRuleSpectraS3(DeleteAzureDataReplicationRuleSpectraS3Request request);
        
        void DeleteDataPersistenceRuleSpectraS3(DeleteDataPersistenceRuleSpectraS3Request request);
        
        void DeleteDataPolicySpectraS3(DeleteDataPolicySpectraS3Request request);
        
        void DeleteDs3DataReplicationRuleSpectraS3(DeleteDs3DataReplicationRuleSpectraS3Request request);
        
        void DeleteS3DataReplicationRuleSpectraS3(DeleteS3DataReplicationRuleSpectraS3Request request);
        
        void ClearSuspectBlobAzureTargetsSpectraS3(ClearSuspectBlobAzureTargetsSpectraS3Request request);
        
        void ClearSuspectBlobDs3TargetsSpectraS3(ClearSuspectBlobDs3TargetsSpectraS3Request request);
        
        void ClearSuspectBlobPoolsSpectraS3(ClearSuspectBlobPoolsSpectraS3Request request);
        
        void ClearSuspectBlobS3TargetsSpectraS3(ClearSuspectBlobS3TargetsSpectraS3Request request);
        
        void ClearSuspectBlobTapesSpectraS3(ClearSuspectBlobTapesSpectraS3Request request);
        
        void MarkSuspectBlobAzureTargetsAsDegradedSpectraS3(MarkSuspectBlobAzureTargetsAsDegradedSpectraS3Request request);
        
        void MarkSuspectBlobDs3TargetsAsDegradedSpectraS3(MarkSuspectBlobDs3TargetsAsDegradedSpectraS3Request request);
        
        void MarkSuspectBlobPoolsAsDegradedSpectraS3(MarkSuspectBlobPoolsAsDegradedSpectraS3Request request);
        
        void MarkSuspectBlobS3TargetsAsDegradedSpectraS3(MarkSuspectBlobS3TargetsAsDegradedSpectraS3Request request);
        
        void MarkSuspectBlobTapesAsDegradedSpectraS3(MarkSuspectBlobTapesAsDegradedSpectraS3Request request);
        
        void DeleteGroupMemberSpectraS3(DeleteGroupMemberSpectraS3Request request);
        
        void DeleteGroupSpectraS3(DeleteGroupSpectraS3Request request);
        
        void CancelActiveJobSpectraS3(CancelActiveJobSpectraS3Request request);
        
        void CancelAllActiveJobsSpectraS3(CancelAllActiveJobsSpectraS3Request request);
        
        void CancelAllJobsSpectraS3(CancelAllJobsSpectraS3Request request);
        
        void CancelJobSpectraS3(CancelJobSpectraS3Request request);
        
        void ClearAllCanceledJobsSpectraS3(ClearAllCanceledJobsSpectraS3Request request);
        
        void ClearAllCompletedJobsSpectraS3(ClearAllCompletedJobsSpectraS3Request request);
        
        void TruncateActiveJobSpectraS3(TruncateActiveJobSpectraS3Request request);
        
        void TruncateAllActiveJobsSpectraS3(TruncateAllActiveJobsSpectraS3Request request);
        
        void TruncateAllJobsSpectraS3(TruncateAllJobsSpectraS3Request request);
        
        void TruncateJobSpectraS3(TruncateJobSpectraS3Request request);
        
        void VerifySafeToCreatePutJobSpectraS3(VerifySafeToCreatePutJobSpectraS3Request request);
        
        void DeleteAzureTargetFailureNotificationRegistrationSpectraS3(DeleteAzureTargetFailureNotificationRegistrationSpectraS3Request request);
        
        void DeleteDs3TargetFailureNotificationRegistrationSpectraS3(DeleteDs3TargetFailureNotificationRegistrationSpectraS3Request request);
        
        void DeleteJobCompletedNotificationRegistrationSpectraS3(DeleteJobCompletedNotificationRegistrationSpectraS3Request request);
        
        void DeleteJobCreatedNotificationRegistrationSpectraS3(DeleteJobCreatedNotificationRegistrationSpectraS3Request request);
        
        void DeleteJobCreationFailedNotificationRegistrationSpectraS3(DeleteJobCreationFailedNotificationRegistrationSpectraS3Request request);
        
        void DeleteObjectCachedNotificationRegistrationSpectraS3(DeleteObjectCachedNotificationRegistrationSpectraS3Request request);
        
        void DeleteObjectLostNotificationRegistrationSpectraS3(DeleteObjectLostNotificationRegistrationSpectraS3Request request);
        
        void DeleteObjectPersistedNotificationRegistrationSpectraS3(DeleteObjectPersistedNotificationRegistrationSpectraS3Request request);
        
        void DeletePoolFailureNotificationRegistrationSpectraS3(DeletePoolFailureNotificationRegistrationSpectraS3Request request);
        
        void DeleteS3TargetFailureNotificationRegistrationSpectraS3(DeleteS3TargetFailureNotificationRegistrationSpectraS3Request request);
        
        void DeleteStorageDomainFailureNotificationRegistrationSpectraS3(DeleteStorageDomainFailureNotificationRegistrationSpectraS3Request request);
        
        void DeleteSystemFailureNotificationRegistrationSpectraS3(DeleteSystemFailureNotificationRegistrationSpectraS3Request request);
        
        void DeleteTapeFailureNotificationRegistrationSpectraS3(DeleteTapeFailureNotificationRegistrationSpectraS3Request request);
        
        void DeleteTapePartitionFailureNotificationRegistrationSpectraS3(DeleteTapePartitionFailureNotificationRegistrationSpectraS3Request request);
        
        void DeleteFolderRecursivelySpectraS3(DeleteFolderRecursivelySpectraS3Request request);
        
        void CancelImportOnAllPoolsSpectraS3(CancelImportOnAllPoolsSpectraS3Request request);
        
        void CancelVerifyOnAllPoolsSpectraS3(CancelVerifyOnAllPoolsSpectraS3Request request);
        
        void CompactAllPoolsSpectraS3(CompactAllPoolsSpectraS3Request request);
        
        void DeallocatePoolSpectraS3(DeallocatePoolSpectraS3Request request);
        
        void DeletePermanentlyLostPoolSpectraS3(DeletePermanentlyLostPoolSpectraS3Request request);
        
        void DeletePoolFailureSpectraS3(DeletePoolFailureSpectraS3Request request);
        
        void DeletePoolPartitionSpectraS3(DeletePoolPartitionSpectraS3Request request);
        
        void ForcePoolEnvironmentRefreshSpectraS3(ForcePoolEnvironmentRefreshSpectraS3Request request);
        
        void FormatAllForeignPoolsSpectraS3(FormatAllForeignPoolsSpectraS3Request request);
        
        void ImportAllPoolsSpectraS3(ImportAllPoolsSpectraS3Request request);
        
        void ModifyAllPoolsSpectraS3(ModifyAllPoolsSpectraS3Request request);
        
        void VerifyAllPoolsSpectraS3(VerifyAllPoolsSpectraS3Request request);
        
        void ConvertStorageDomainToDs3TargetSpectraS3(ConvertStorageDomainToDs3TargetSpectraS3Request request);
        
        void DeleteStorageDomainFailureSpectraS3(DeleteStorageDomainFailureSpectraS3Request request);
        
        void DeleteStorageDomainMemberSpectraS3(DeleteStorageDomainMemberSpectraS3Request request);
        
        void DeleteStorageDomainSpectraS3(DeleteStorageDomainSpectraS3Request request);
        
        void ForceFeatureKeyValidationSpectraS3(ForceFeatureKeyValidationSpectraS3Request request);
        
        void DeletePermanentlyLostTapeSpectraS3(DeletePermanentlyLostTapeSpectraS3Request request);
        
        void DeleteTapeDensityDirectiveSpectraS3(DeleteTapeDensityDirectiveSpectraS3Request request);
        
        void DeleteTapeDriveSpectraS3(DeleteTapeDriveSpectraS3Request request);
        
        void DeleteTapeFailureSpectraS3(DeleteTapeFailureSpectraS3Request request);
        
        void DeleteTapePartitionFailureSpectraS3(DeleteTapePartitionFailureSpectraS3Request request);
        
        void DeleteTapePartitionSpectraS3(DeleteTapePartitionSpectraS3Request request);
        
        void EjectStorageDomainBlobsSpectraS3(EjectStorageDomainBlobsSpectraS3Request request);
        
        void ForceTapeEnvironmentRefreshSpectraS3(ForceTapeEnvironmentRefreshSpectraS3Request request);
        
        void ImportAllTapesSpectraS3(ImportAllTapesSpectraS3Request request);
        
        void ModifyAllTapePartitionsSpectraS3(ModifyAllTapePartitionsSpectraS3Request request);
        
        void RawImportAllTapesSpectraS3(RawImportAllTapesSpectraS3Request request);
        
        void ForceTargetEnvironmentRefreshSpectraS3(ForceTargetEnvironmentRefreshSpectraS3Request request);
        
        void DeleteAzureTargetBucketNameSpectraS3(DeleteAzureTargetBucketNameSpectraS3Request request);
        
        void DeleteAzureTargetFailureSpectraS3(DeleteAzureTargetFailureSpectraS3Request request);
        
        void DeleteAzureTargetReadPreferenceSpectraS3(DeleteAzureTargetReadPreferenceSpectraS3Request request);
        
        void DeleteAzureTargetSpectraS3(DeleteAzureTargetSpectraS3Request request);
        
        void ImportAzureTargetSpectraS3(ImportAzureTargetSpectraS3Request request);
        
        void ModifyAllAzureTargetsSpectraS3(ModifyAllAzureTargetsSpectraS3Request request);
        
        void DeleteDs3TargetFailureSpectraS3(DeleteDs3TargetFailureSpectraS3Request request);
        
        void DeleteDs3TargetReadPreferenceSpectraS3(DeleteDs3TargetReadPreferenceSpectraS3Request request);
        
        void DeleteDs3TargetSpectraS3(DeleteDs3TargetSpectraS3Request request);
        
        void ModifyAllDs3TargetsSpectraS3(ModifyAllDs3TargetsSpectraS3Request request);
        
        void PairBackRegisteredDs3TargetSpectraS3(PairBackRegisteredDs3TargetSpectraS3Request request);
        
        void DeleteS3TargetBucketNameSpectraS3(DeleteS3TargetBucketNameSpectraS3Request request);
        
        void DeleteS3TargetFailureSpectraS3(DeleteS3TargetFailureSpectraS3Request request);
        
        void DeleteS3TargetReadPreferenceSpectraS3(DeleteS3TargetReadPreferenceSpectraS3Request request);
        
        void DeleteS3TargetSpectraS3(DeleteS3TargetSpectraS3Request request);
        
        void ImportS3TargetSpectraS3(ImportS3TargetSpectraS3Request request);
        
        void ModifyAllS3TargetsSpectraS3(ModifyAllS3TargetsSpectraS3Request request);
        
        void DelegateDeleteUserSpectraS3(DelegateDeleteUserSpectraS3Request request);
        
        GetObjectResponse GetObject(GetObjectRequest request);

        /// <summary>
        /// For multi-node support (planned), this provides a means of creating
        /// a client that connects to the specified node id.
        /// </summary>
        /// <param name="nodes"></param>
        /// <returns></returns>
        IDs3ClientFactory BuildFactory(IEnumerable<JobNode> nodes);
    }
}