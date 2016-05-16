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
        PutDataPersistenceRuleSpectraS3Response PutDataPersistenceRuleSpectraS3(PutDataPersistenceRuleSpectraS3Request request);
        PutDataPolicySpectraS3Response PutDataPolicySpectraS3(PutDataPolicySpectraS3Request request);
        GetDataPersistenceRuleSpectraS3Response GetDataPersistenceRuleSpectraS3(GetDataPersistenceRuleSpectraS3Request request);
        GetDataPersistenceRulesSpectraS3Response GetDataPersistenceRulesSpectraS3(GetDataPersistenceRulesSpectraS3Request request);
        GetDataPoliciesSpectraS3Response GetDataPoliciesSpectraS3(GetDataPoliciesSpectraS3Request request);
        GetDataPolicySpectraS3Response GetDataPolicySpectraS3(GetDataPolicySpectraS3Request request);
        ModifyDataPersistenceRuleSpectraS3Response ModifyDataPersistenceRuleSpectraS3(ModifyDataPersistenceRuleSpectraS3Request request);
        ModifyDataPolicySpectraS3Response ModifyDataPolicySpectraS3(ModifyDataPolicySpectraS3Request request);
        GetDegradedBucketsSpectraS3Response GetDegradedBucketsSpectraS3(GetDegradedBucketsSpectraS3Request request);
        GetDegradedDataPersistenceRulesSpectraS3Response GetDegradedDataPersistenceRulesSpectraS3(GetDegradedDataPersistenceRulesSpectraS3Request request);
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
        GetActiveJobsSpectraS3Response GetActiveJobsSpectraS3(GetActiveJobsSpectraS3Request request);
        GetCanceledJobsSpectraS3Response GetCanceledJobsSpectraS3(GetCanceledJobsSpectraS3Request request);
        GetCompletedJobsSpectraS3Response GetCompletedJobsSpectraS3(GetCompletedJobsSpectraS3Request request);
        GetJobChunkSpectraS3Response GetJobChunkSpectraS3(GetJobChunkSpectraS3Request request);
        GetJobChunksReadyForClientProcessingSpectraS3Response GetJobChunksReadyForClientProcessingSpectraS3(GetJobChunksReadyForClientProcessingSpectraS3Request request);
        GetJobSpectraS3Response GetJobSpectraS3(GetJobSpectraS3Request request);
        GetJobsSpectraS3Response GetJobsSpectraS3(GetJobsSpectraS3Request request);
        GetPutJobToReplicateSpectraS3Response GetPutJobToReplicateSpectraS3(GetPutJobToReplicateSpectraS3Request request);
        ModifyJobSpectraS3Response ModifyJobSpectraS3(ModifyJobSpectraS3Request request);
        ReplicatePutJobSpectraS3Response ReplicatePutJobSpectraS3(ReplicatePutJobSpectraS3Request request);
        GetNodeSpectraS3Response GetNodeSpectraS3(GetNodeSpectraS3Request request);
        GetNodesSpectraS3Response GetNodesSpectraS3(GetNodesSpectraS3Request request);
        ModifyNodeSpectraS3Response ModifyNodeSpectraS3(ModifyNodeSpectraS3Request request);
        PutJobCompletedNotificationRegistrationSpectraS3Response PutJobCompletedNotificationRegistrationSpectraS3(PutJobCompletedNotificationRegistrationSpectraS3Request request);
        PutJobCreatedNotificationRegistrationSpectraS3Response PutJobCreatedNotificationRegistrationSpectraS3(PutJobCreatedNotificationRegistrationSpectraS3Request request);
        PutObjectCachedNotificationRegistrationSpectraS3Response PutObjectCachedNotificationRegistrationSpectraS3(PutObjectCachedNotificationRegistrationSpectraS3Request request);
        PutObjectLostNotificationRegistrationSpectraS3Response PutObjectLostNotificationRegistrationSpectraS3(PutObjectLostNotificationRegistrationSpectraS3Request request);
        PutObjectPersistedNotificationRegistrationSpectraS3Response PutObjectPersistedNotificationRegistrationSpectraS3(PutObjectPersistedNotificationRegistrationSpectraS3Request request);
        PutPoolFailureNotificationRegistrationSpectraS3Response PutPoolFailureNotificationRegistrationSpectraS3(PutPoolFailureNotificationRegistrationSpectraS3Request request);
        PutStorageDomainFailureNotificationRegistrationSpectraS3Response PutStorageDomainFailureNotificationRegistrationSpectraS3(PutStorageDomainFailureNotificationRegistrationSpectraS3Request request);
        PutSystemFailureNotificationRegistrationSpectraS3Response PutSystemFailureNotificationRegistrationSpectraS3(PutSystemFailureNotificationRegistrationSpectraS3Request request);
        PutTapeFailureNotificationRegistrationSpectraS3Response PutTapeFailureNotificationRegistrationSpectraS3(PutTapeFailureNotificationRegistrationSpectraS3Request request);
        PutTapePartitionFailureNotificationRegistrationSpectraS3Response PutTapePartitionFailureNotificationRegistrationSpectraS3(PutTapePartitionFailureNotificationRegistrationSpectraS3Request request);
        GetJobCompletedNotificationRegistrationSpectraS3Response GetJobCompletedNotificationRegistrationSpectraS3(GetJobCompletedNotificationRegistrationSpectraS3Request request);
        GetJobCompletedNotificationRegistrationsSpectraS3Response GetJobCompletedNotificationRegistrationsSpectraS3(GetJobCompletedNotificationRegistrationsSpectraS3Request request);
        GetJobCreatedNotificationRegistrationSpectraS3Response GetJobCreatedNotificationRegistrationSpectraS3(GetJobCreatedNotificationRegistrationSpectraS3Request request);
        GetJobCreatedNotificationRegistrationsSpectraS3Response GetJobCreatedNotificationRegistrationsSpectraS3(GetJobCreatedNotificationRegistrationsSpectraS3Request request);
        GetObjectCachedNotificationRegistrationSpectraS3Response GetObjectCachedNotificationRegistrationSpectraS3(GetObjectCachedNotificationRegistrationSpectraS3Request request);
        GetObjectCachedNotificationRegistrationsSpectraS3Response GetObjectCachedNotificationRegistrationsSpectraS3(GetObjectCachedNotificationRegistrationsSpectraS3Request request);
        GetObjectLostNotificationRegistrationSpectraS3Response GetObjectLostNotificationRegistrationSpectraS3(GetObjectLostNotificationRegistrationSpectraS3Request request);
        GetObjectLostNotificationRegistrationsSpectraS3Response GetObjectLostNotificationRegistrationsSpectraS3(GetObjectLostNotificationRegistrationsSpectraS3Request request);
        GetObjectPersistedNotificationRegistrationSpectraS3Response GetObjectPersistedNotificationRegistrationSpectraS3(GetObjectPersistedNotificationRegistrationSpectraS3Request request);
        GetObjectPersistedNotificationRegistrationsSpectraS3Response GetObjectPersistedNotificationRegistrationsSpectraS3(GetObjectPersistedNotificationRegistrationsSpectraS3Request request);
        GetPoolFailureNotificationRegistrationSpectraS3Response GetPoolFailureNotificationRegistrationSpectraS3(GetPoolFailureNotificationRegistrationSpectraS3Request request);
        GetPoolFailureNotificationRegistrationsSpectraS3Response GetPoolFailureNotificationRegistrationsSpectraS3(GetPoolFailureNotificationRegistrationsSpectraS3Request request);
        GetStorageDomainFailureNotificationRegistrationSpectraS3Response GetStorageDomainFailureNotificationRegistrationSpectraS3(GetStorageDomainFailureNotificationRegistrationSpectraS3Request request);
        GetStorageDomainFailureNotificationRegistrationsSpectraS3Response GetStorageDomainFailureNotificationRegistrationsSpectraS3(GetStorageDomainFailureNotificationRegistrationsSpectraS3Request request);
        GetSystemFailureNotificationRegistrationSpectraS3Response GetSystemFailureNotificationRegistrationSpectraS3(GetSystemFailureNotificationRegistrationSpectraS3Request request);
        GetSystemFailureNotificationRegistrationsSpectraS3Response GetSystemFailureNotificationRegistrationsSpectraS3(GetSystemFailureNotificationRegistrationsSpectraS3Request request);
        GetTapeFailureNotificationRegistrationSpectraS3Response GetTapeFailureNotificationRegistrationSpectraS3(GetTapeFailureNotificationRegistrationSpectraS3Request request);
        GetTapeFailureNotificationRegistrationsSpectraS3Response GetTapeFailureNotificationRegistrationsSpectraS3(GetTapeFailureNotificationRegistrationsSpectraS3Request request);
        GetTapePartitionFailureNotificationRegistrationSpectraS3Response GetTapePartitionFailureNotificationRegistrationSpectraS3(GetTapePartitionFailureNotificationRegistrationSpectraS3Request request);
        GetTapePartitionFailureNotificationRegistrationsSpectraS3Response GetTapePartitionFailureNotificationRegistrationsSpectraS3(GetTapePartitionFailureNotificationRegistrationsSpectraS3Request request);
        GetObjectSpectraS3Response GetObjectSpectraS3(GetObjectSpectraS3Request request);
        GetObjectsSpectraS3Response GetObjectsSpectraS3(GetObjectsSpectraS3Request request);
        GetObjectsWithFullDetailsSpectraS3Response GetObjectsWithFullDetailsSpectraS3(GetObjectsWithFullDetailsSpectraS3Request request);
        GetPhysicalPlacementForObjectsSpectraS3Response GetPhysicalPlacementForObjectsSpectraS3(GetPhysicalPlacementForObjectsSpectraS3Request request);
        GetPhysicalPlacementForObjectsWithFullDetailsSpectraS3Response GetPhysicalPlacementForObjectsWithFullDetailsSpectraS3(GetPhysicalPlacementForObjectsWithFullDetailsSpectraS3Request request);
        VerifyPhysicalPlacementForObjectsSpectraS3Response VerifyPhysicalPlacementForObjectsSpectraS3(VerifyPhysicalPlacementForObjectsSpectraS3Request request);
        VerifyPhysicalPlacementForObjectsWithFullDetailsSpectraS3Response VerifyPhysicalPlacementForObjectsWithFullDetailsSpectraS3(VerifyPhysicalPlacementForObjectsWithFullDetailsSpectraS3Request request);
        CancelImportPoolSpectraS3Response CancelImportPoolSpectraS3(CancelImportPoolSpectraS3Request request);
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
        GetSystemFailuresSpectraS3Response GetSystemFailuresSpectraS3(GetSystemFailuresSpectraS3Request request);
        GetSystemInformationSpectraS3Response GetSystemInformationSpectraS3(GetSystemInformationSpectraS3Request request);
        VerifySystemHealthSpectraS3Response VerifySystemHealthSpectraS3(VerifySystemHealthSpectraS3Request request);
        CancelEjectOnAllTapesSpectraS3Response CancelEjectOnAllTapesSpectraS3(CancelEjectOnAllTapesSpectraS3Request request);
        CancelEjectTapeSpectraS3Response CancelEjectTapeSpectraS3(CancelEjectTapeSpectraS3Request request);
        CancelFormatOnAllTapesSpectraS3Response CancelFormatOnAllTapesSpectraS3(CancelFormatOnAllTapesSpectraS3Request request);
        CancelFormatTapeSpectraS3Response CancelFormatTapeSpectraS3(CancelFormatTapeSpectraS3Request request);
        CancelImportOnAllTapesSpectraS3Response CancelImportOnAllTapesSpectraS3(CancelImportOnAllTapesSpectraS3Request request);
        CancelImportTapeSpectraS3Response CancelImportTapeSpectraS3(CancelImportTapeSpectraS3Request request);
        CancelOnlineOnAllTapesSpectraS3Response CancelOnlineOnAllTapesSpectraS3(CancelOnlineOnAllTapesSpectraS3Request request);
        CancelOnlineTapeSpectraS3Response CancelOnlineTapeSpectraS3(CancelOnlineTapeSpectraS3Request request);
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
        GetTapeWithFullDetailsSpectraS3Response GetTapeWithFullDetailsSpectraS3(GetTapeWithFullDetailsSpectraS3Request request);
        GetTapesSpectraS3Response GetTapesSpectraS3(GetTapesSpectraS3Request request);
        GetTapesWithFullDetailsSpectraS3Response GetTapesWithFullDetailsSpectraS3(GetTapesWithFullDetailsSpectraS3Request request);
        ImportAllTapesSpectraS3Response ImportAllTapesSpectraS3(ImportAllTapesSpectraS3Request request);
        ImportTapeSpectraS3Response ImportTapeSpectraS3(ImportTapeSpectraS3Request request);
        InspectAllTapesSpectraS3Response InspectAllTapesSpectraS3(InspectAllTapesSpectraS3Request request);
        InspectTapeSpectraS3Response InspectTapeSpectraS3(InspectTapeSpectraS3Request request);
        ModifyTapePartitionSpectraS3Response ModifyTapePartitionSpectraS3(ModifyTapePartitionSpectraS3Request request);
        ModifyTapeSpectraS3Response ModifyTapeSpectraS3(ModifyTapeSpectraS3Request request);
        OnlineAllTapesSpectraS3Response OnlineAllTapesSpectraS3(OnlineAllTapesSpectraS3Request request);
        OnlineTapeSpectraS3Response OnlineTapeSpectraS3(OnlineTapeSpectraS3Request request);
        VerifyAllTapesSpectraS3Response VerifyAllTapesSpectraS3(VerifyAllTapesSpectraS3Request request);
        VerifyTapeSpectraS3Response VerifyTapeSpectraS3(VerifyTapeSpectraS3Request request);
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
        void HeadObject(HeadObjectRequest request);
        void DeleteBucketAclSpectraS3(DeleteBucketAclSpectraS3Request request);
        void DeleteDataPolicyAclSpectraS3(DeleteDataPolicyAclSpectraS3Request request);
        void DeleteBucketSpectraS3(DeleteBucketSpectraS3Request request);
        void ForceFullCacheReclaimSpectraS3(ForceFullCacheReclaimSpectraS3Request request);
        void DeleteDataPersistenceRuleSpectraS3(DeleteDataPersistenceRuleSpectraS3Request request);
        void DeleteDataPolicySpectraS3(DeleteDataPolicySpectraS3Request request);
        void DeleteGroupMemberSpectraS3(DeleteGroupMemberSpectraS3Request request);
        void DeleteGroupSpectraS3(DeleteGroupSpectraS3Request request);
        void CancelAllJobsSpectraS3(CancelAllJobsSpectraS3Request request);
        void CancelJobSpectraS3(CancelJobSpectraS3Request request);
        void ClearAllCanceledJobsSpectraS3(ClearAllCanceledJobsSpectraS3Request request);
        void ClearAllCompletedJobsSpectraS3(ClearAllCompletedJobsSpectraS3Request request);
        void DeleteJobCompletedNotificationRegistrationSpectraS3(DeleteJobCompletedNotificationRegistrationSpectraS3Request request);
        void DeleteJobCreatedNotificationRegistrationSpectraS3(DeleteJobCreatedNotificationRegistrationSpectraS3Request request);
        void DeleteObjectCachedNotificationRegistrationSpectraS3(DeleteObjectCachedNotificationRegistrationSpectraS3Request request);
        void DeleteObjectLostNotificationRegistrationSpectraS3(DeleteObjectLostNotificationRegistrationSpectraS3Request request);
        void DeleteObjectPersistedNotificationRegistrationSpectraS3(DeleteObjectPersistedNotificationRegistrationSpectraS3Request request);
        void DeletePoolFailureNotificationRegistrationSpectraS3(DeletePoolFailureNotificationRegistrationSpectraS3Request request);
        void DeleteStorageDomainFailureNotificationRegistrationSpectraS3(DeleteStorageDomainFailureNotificationRegistrationSpectraS3Request request);
        void DeleteSystemFailureNotificationRegistrationSpectraS3(DeleteSystemFailureNotificationRegistrationSpectraS3Request request);
        void DeleteTapeFailureNotificationRegistrationSpectraS3(DeleteTapeFailureNotificationRegistrationSpectraS3Request request);
        void DeleteTapePartitionFailureNotificationRegistrationSpectraS3(DeleteTapePartitionFailureNotificationRegistrationSpectraS3Request request);
        void DeleteFolderRecursivelySpectraS3(DeleteFolderRecursivelySpectraS3Request request);
        void CancelImportOnAllPoolsSpectraS3(CancelImportOnAllPoolsSpectraS3Request request);
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
        void DeleteStorageDomainFailureSpectraS3(DeleteStorageDomainFailureSpectraS3Request request);
        void DeleteStorageDomainMemberSpectraS3(DeleteStorageDomainMemberSpectraS3Request request);
        void DeleteStorageDomainSpectraS3(DeleteStorageDomainSpectraS3Request request);
        void DeletePermanentlyLostTapeSpectraS3(DeletePermanentlyLostTapeSpectraS3Request request);
        void DeleteTapeDensityDirectiveSpectraS3(DeleteTapeDensityDirectiveSpectraS3Request request);
        void DeleteTapeDriveSpectraS3(DeleteTapeDriveSpectraS3Request request);
        void DeleteTapeFailureSpectraS3(DeleteTapeFailureSpectraS3Request request);
        void DeleteTapePartitionFailureSpectraS3(DeleteTapePartitionFailureSpectraS3Request request);
        void DeleteTapePartitionSpectraS3(DeleteTapePartitionSpectraS3Request request);
        void EjectStorageDomainBlobsSpectraS3(EjectStorageDomainBlobsSpectraS3Request request);
        void ForceTapeEnvironmentRefreshSpectraS3(ForceTapeEnvironmentRefreshSpectraS3Request request);
        void ModifyAllTapePartitionsSpectraS3(ModifyAllTapePartitionsSpectraS3Request request);
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