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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

using Ds3.Calls;
using Ds3.Models;
using Ds3.ResponseParsers;
using Ds3.Runtime;

namespace Ds3
{
    internal class Ds3Client : IDs3Client
    {
        private INetwork _netLayer;

        internal Ds3Client(INetwork netLayer)
        {
            this._netLayer = netLayer;
        }

        
        public CompleteMultiPartUploadResponse CompleteMultiPartUpload(CompleteMultiPartUploadRequest request)
        {
            return new CompleteMultiPartUploadResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public DeleteObjectsResponse DeleteObjects(DeleteObjectsRequest request)
        {
            return new DeleteObjectsResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetBucketResponse GetBucket(GetBucketRequest request)
        {
            return new GetBucketResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetServiceResponse GetService(GetServiceRequest request)
        {
            return new GetServiceResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public HeadBucketResponse HeadBucket(HeadBucketRequest request)
        {
            return new HeadBucketResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public HeadObjectResponse HeadObject(HeadObjectRequest request)
        {
            return new HeadObjectResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public InitiateMultiPartUploadResponse InitiateMultiPartUpload(InitiateMultiPartUploadRequest request)
        {
            return new InitiateMultiPartUploadResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public ListMultiPartUploadPartsResponse ListMultiPartUploadParts(ListMultiPartUploadPartsRequest request)
        {
            return new ListMultiPartUploadPartsResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public ListMultiPartUploadsResponse ListMultiPartUploads(ListMultiPartUploadsRequest request)
        {
            return new ListMultiPartUploadsResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutBucketAclForGroupSpectraS3Response PutBucketAclForGroupSpectraS3(PutBucketAclForGroupSpectraS3Request request)
        {
            return new PutBucketAclForGroupSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutBucketAclForUserSpectraS3Response PutBucketAclForUserSpectraS3(PutBucketAclForUserSpectraS3Request request)
        {
            return new PutBucketAclForUserSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutDataPolicyAclForGroupSpectraS3Response PutDataPolicyAclForGroupSpectraS3(PutDataPolicyAclForGroupSpectraS3Request request)
        {
            return new PutDataPolicyAclForGroupSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutDataPolicyAclForUserSpectraS3Response PutDataPolicyAclForUserSpectraS3(PutDataPolicyAclForUserSpectraS3Request request)
        {
            return new PutDataPolicyAclForUserSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutGlobalBucketAclForGroupSpectraS3Response PutGlobalBucketAclForGroupSpectraS3(PutGlobalBucketAclForGroupSpectraS3Request request)
        {
            return new PutGlobalBucketAclForGroupSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutGlobalBucketAclForUserSpectraS3Response PutGlobalBucketAclForUserSpectraS3(PutGlobalBucketAclForUserSpectraS3Request request)
        {
            return new PutGlobalBucketAclForUserSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutGlobalDataPolicyAclForGroupSpectraS3Response PutGlobalDataPolicyAclForGroupSpectraS3(PutGlobalDataPolicyAclForGroupSpectraS3Request request)
        {
            return new PutGlobalDataPolicyAclForGroupSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutGlobalDataPolicyAclForUserSpectraS3Response PutGlobalDataPolicyAclForUserSpectraS3(PutGlobalDataPolicyAclForUserSpectraS3Request request)
        {
            return new PutGlobalDataPolicyAclForUserSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetBucketAclSpectraS3Response GetBucketAclSpectraS3(GetBucketAclSpectraS3Request request)
        {
            return new GetBucketAclSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetBucketAclsSpectraS3Response GetBucketAclsSpectraS3(GetBucketAclsSpectraS3Request request)
        {
            return new GetBucketAclsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetDataPolicyAclSpectraS3Response GetDataPolicyAclSpectraS3(GetDataPolicyAclSpectraS3Request request)
        {
            return new GetDataPolicyAclSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetDataPolicyAclsSpectraS3Response GetDataPolicyAclsSpectraS3(GetDataPolicyAclsSpectraS3Request request)
        {
            return new GetDataPolicyAclsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutBucketSpectraS3Response PutBucketSpectraS3(PutBucketSpectraS3Request request)
        {
            return new PutBucketSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetBucketSpectraS3Response GetBucketSpectraS3(GetBucketSpectraS3Request request)
        {
            return new GetBucketSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetBucketsSpectraS3Response GetBucketsSpectraS3(GetBucketsSpectraS3Request request)
        {
            return new GetBucketsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public ModifyBucketSpectraS3Response ModifyBucketSpectraS3(ModifyBucketSpectraS3Request request)
        {
            return new ModifyBucketSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetCacheFilesystemSpectraS3Response GetCacheFilesystemSpectraS3(GetCacheFilesystemSpectraS3Request request)
        {
            return new GetCacheFilesystemSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetCacheFilesystemsSpectraS3Response GetCacheFilesystemsSpectraS3(GetCacheFilesystemsSpectraS3Request request)
        {
            return new GetCacheFilesystemsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetCacheStateSpectraS3Response GetCacheStateSpectraS3(GetCacheStateSpectraS3Request request)
        {
            return new GetCacheStateSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public ModifyCacheFilesystemSpectraS3Response ModifyCacheFilesystemSpectraS3(ModifyCacheFilesystemSpectraS3Request request)
        {
            return new ModifyCacheFilesystemSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetBucketCapacitySummarySpectraS3Response GetBucketCapacitySummarySpectraS3(GetBucketCapacitySummarySpectraS3Request request)
        {
            return new GetBucketCapacitySummarySpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetStorageDomainCapacitySummarySpectraS3Response GetStorageDomainCapacitySummarySpectraS3(GetStorageDomainCapacitySummarySpectraS3Request request)
        {
            return new GetStorageDomainCapacitySummarySpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetSystemCapacitySummarySpectraS3Response GetSystemCapacitySummarySpectraS3(GetSystemCapacitySummarySpectraS3Request request)
        {
            return new GetSystemCapacitySummarySpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetDataPathBackendSpectraS3Response GetDataPathBackendSpectraS3(GetDataPathBackendSpectraS3Request request)
        {
            return new GetDataPathBackendSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetDataPlannerBlobStoreTasksSpectraS3Response GetDataPlannerBlobStoreTasksSpectraS3(GetDataPlannerBlobStoreTasksSpectraS3Request request)
        {
            return new GetDataPlannerBlobStoreTasksSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public ModifyDataPathBackendSpectraS3Response ModifyDataPathBackendSpectraS3(ModifyDataPathBackendSpectraS3Request request)
        {
            return new ModifyDataPathBackendSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutDataPersistenceRuleSpectraS3Response PutDataPersistenceRuleSpectraS3(PutDataPersistenceRuleSpectraS3Request request)
        {
            return new PutDataPersistenceRuleSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutDataPolicySpectraS3Response PutDataPolicySpectraS3(PutDataPolicySpectraS3Request request)
        {
            return new PutDataPolicySpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetDataPersistenceRuleSpectraS3Response GetDataPersistenceRuleSpectraS3(GetDataPersistenceRuleSpectraS3Request request)
        {
            return new GetDataPersistenceRuleSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetDataPersistenceRulesSpectraS3Response GetDataPersistenceRulesSpectraS3(GetDataPersistenceRulesSpectraS3Request request)
        {
            return new GetDataPersistenceRulesSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetDataPoliciesSpectraS3Response GetDataPoliciesSpectraS3(GetDataPoliciesSpectraS3Request request)
        {
            return new GetDataPoliciesSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetDataPolicySpectraS3Response GetDataPolicySpectraS3(GetDataPolicySpectraS3Request request)
        {
            return new GetDataPolicySpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public ModifyDataPersistenceRuleSpectraS3Response ModifyDataPersistenceRuleSpectraS3(ModifyDataPersistenceRuleSpectraS3Request request)
        {
            return new ModifyDataPersistenceRuleSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public ModifyDataPolicySpectraS3Response ModifyDataPolicySpectraS3(ModifyDataPolicySpectraS3Request request)
        {
            return new ModifyDataPolicySpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetDegradedBucketsSpectraS3Response GetDegradedBucketsSpectraS3(GetDegradedBucketsSpectraS3Request request)
        {
            return new GetDegradedBucketsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetDegradedDataPersistenceRulesSpectraS3Response GetDegradedDataPersistenceRulesSpectraS3(GetDegradedDataPersistenceRulesSpectraS3Request request)
        {
            return new GetDegradedDataPersistenceRulesSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutGroupGroupMemberSpectraS3Response PutGroupGroupMemberSpectraS3(PutGroupGroupMemberSpectraS3Request request)
        {
            return new PutGroupGroupMemberSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutGroupSpectraS3Response PutGroupSpectraS3(PutGroupSpectraS3Request request)
        {
            return new PutGroupSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutUserGroupMemberSpectraS3Response PutUserGroupMemberSpectraS3(PutUserGroupMemberSpectraS3Request request)
        {
            return new PutUserGroupMemberSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetGroupMemberSpectraS3Response GetGroupMemberSpectraS3(GetGroupMemberSpectraS3Request request)
        {
            return new GetGroupMemberSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetGroupMembersSpectraS3Response GetGroupMembersSpectraS3(GetGroupMembersSpectraS3Request request)
        {
            return new GetGroupMembersSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetGroupSpectraS3Response GetGroupSpectraS3(GetGroupSpectraS3Request request)
        {
            return new GetGroupSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetGroupsSpectraS3Response GetGroupsSpectraS3(GetGroupsSpectraS3Request request)
        {
            return new GetGroupsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public ModifyGroupSpectraS3Response ModifyGroupSpectraS3(ModifyGroupSpectraS3Request request)
        {
            return new ModifyGroupSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public VerifyUserIsMemberOfGroupSpectraS3Response VerifyUserIsMemberOfGroupSpectraS3(VerifyUserIsMemberOfGroupSpectraS3Request request)
        {
            return new VerifyUserIsMemberOfGroupSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public AllocateJobChunkSpectraS3Response AllocateJobChunkSpectraS3(AllocateJobChunkSpectraS3Request request)
        {
            return new AllocateJobChunkSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetBulkJobSpectraS3Response GetBulkJobSpectraS3(GetBulkJobSpectraS3Request request)
        {
            return new GetBulkJobSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutBulkJobSpectraS3Response PutBulkJobSpectraS3(PutBulkJobSpectraS3Request request)
        {
            return new PutBulkJobSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public VerifyBulkJobSpectraS3Response VerifyBulkJobSpectraS3(VerifyBulkJobSpectraS3Request request)
        {
            return new VerifyBulkJobSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetActiveJobsSpectraS3Response GetActiveJobsSpectraS3(GetActiveJobsSpectraS3Request request)
        {
            return new GetActiveJobsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetCanceledJobsSpectraS3Response GetCanceledJobsSpectraS3(GetCanceledJobsSpectraS3Request request)
        {
            return new GetCanceledJobsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetCompletedJobsSpectraS3Response GetCompletedJobsSpectraS3(GetCompletedJobsSpectraS3Request request)
        {
            return new GetCompletedJobsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetJobChunkSpectraS3Response GetJobChunkSpectraS3(GetJobChunkSpectraS3Request request)
        {
            return new GetJobChunkSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetJobChunksReadyForClientProcessingSpectraS3Response GetJobChunksReadyForClientProcessingSpectraS3(GetJobChunksReadyForClientProcessingSpectraS3Request request)
        {
            return new GetJobChunksReadyForClientProcessingSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetJobSpectraS3Response GetJobSpectraS3(GetJobSpectraS3Request request)
        {
            return new GetJobSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetJobsSpectraS3Response GetJobsSpectraS3(GetJobsSpectraS3Request request)
        {
            return new GetJobsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetPutJobToReplicateSpectraS3Response GetPutJobToReplicateSpectraS3(GetPutJobToReplicateSpectraS3Request request)
        {
            return new GetPutJobToReplicateSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public ModifyJobSpectraS3Response ModifyJobSpectraS3(ModifyJobSpectraS3Request request)
        {
            return new ModifyJobSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public ReplicatePutJobSpectraS3Response ReplicatePutJobSpectraS3(ReplicatePutJobSpectraS3Request request)
        {
            return new ReplicatePutJobSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetNodeSpectraS3Response GetNodeSpectraS3(GetNodeSpectraS3Request request)
        {
            return new GetNodeSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetNodesSpectraS3Response GetNodesSpectraS3(GetNodesSpectraS3Request request)
        {
            return new GetNodesSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public ModifyNodeSpectraS3Response ModifyNodeSpectraS3(ModifyNodeSpectraS3Request request)
        {
            return new ModifyNodeSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutJobCompletedNotificationRegistrationSpectraS3Response PutJobCompletedNotificationRegistrationSpectraS3(PutJobCompletedNotificationRegistrationSpectraS3Request request)
        {
            return new PutJobCompletedNotificationRegistrationSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutJobCreatedNotificationRegistrationSpectraS3Response PutJobCreatedNotificationRegistrationSpectraS3(PutJobCreatedNotificationRegistrationSpectraS3Request request)
        {
            return new PutJobCreatedNotificationRegistrationSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutObjectCachedNotificationRegistrationSpectraS3Response PutObjectCachedNotificationRegistrationSpectraS3(PutObjectCachedNotificationRegistrationSpectraS3Request request)
        {
            return new PutObjectCachedNotificationRegistrationSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutObjectLostNotificationRegistrationSpectraS3Response PutObjectLostNotificationRegistrationSpectraS3(PutObjectLostNotificationRegistrationSpectraS3Request request)
        {
            return new PutObjectLostNotificationRegistrationSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutObjectPersistedNotificationRegistrationSpectraS3Response PutObjectPersistedNotificationRegistrationSpectraS3(PutObjectPersistedNotificationRegistrationSpectraS3Request request)
        {
            return new PutObjectPersistedNotificationRegistrationSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutPoolFailureNotificationRegistrationSpectraS3Response PutPoolFailureNotificationRegistrationSpectraS3(PutPoolFailureNotificationRegistrationSpectraS3Request request)
        {
            return new PutPoolFailureNotificationRegistrationSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutStorageDomainFailureNotificationRegistrationSpectraS3Response PutStorageDomainFailureNotificationRegistrationSpectraS3(PutStorageDomainFailureNotificationRegistrationSpectraS3Request request)
        {
            return new PutStorageDomainFailureNotificationRegistrationSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutSystemFailureNotificationRegistrationSpectraS3Response PutSystemFailureNotificationRegistrationSpectraS3(PutSystemFailureNotificationRegistrationSpectraS3Request request)
        {
            return new PutSystemFailureNotificationRegistrationSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutTapeFailureNotificationRegistrationSpectraS3Response PutTapeFailureNotificationRegistrationSpectraS3(PutTapeFailureNotificationRegistrationSpectraS3Request request)
        {
            return new PutTapeFailureNotificationRegistrationSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutTapePartitionFailureNotificationRegistrationSpectraS3Response PutTapePartitionFailureNotificationRegistrationSpectraS3(PutTapePartitionFailureNotificationRegistrationSpectraS3Request request)
        {
            return new PutTapePartitionFailureNotificationRegistrationSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetJobCompletedNotificationRegistrationSpectraS3Response GetJobCompletedNotificationRegistrationSpectraS3(GetJobCompletedNotificationRegistrationSpectraS3Request request)
        {
            return new GetJobCompletedNotificationRegistrationSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetJobCompletedNotificationRegistrationsSpectraS3Response GetJobCompletedNotificationRegistrationsSpectraS3(GetJobCompletedNotificationRegistrationsSpectraS3Request request)
        {
            return new GetJobCompletedNotificationRegistrationsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetJobCreatedNotificationRegistrationSpectraS3Response GetJobCreatedNotificationRegistrationSpectraS3(GetJobCreatedNotificationRegistrationSpectraS3Request request)
        {
            return new GetJobCreatedNotificationRegistrationSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetJobCreatedNotificationRegistrationsSpectraS3Response GetJobCreatedNotificationRegistrationsSpectraS3(GetJobCreatedNotificationRegistrationsSpectraS3Request request)
        {
            return new GetJobCreatedNotificationRegistrationsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetObjectCachedNotificationRegistrationSpectraS3Response GetObjectCachedNotificationRegistrationSpectraS3(GetObjectCachedNotificationRegistrationSpectraS3Request request)
        {
            return new GetObjectCachedNotificationRegistrationSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetObjectCachedNotificationRegistrationsSpectraS3Response GetObjectCachedNotificationRegistrationsSpectraS3(GetObjectCachedNotificationRegistrationsSpectraS3Request request)
        {
            return new GetObjectCachedNotificationRegistrationsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetObjectLostNotificationRegistrationSpectraS3Response GetObjectLostNotificationRegistrationSpectraS3(GetObjectLostNotificationRegistrationSpectraS3Request request)
        {
            return new GetObjectLostNotificationRegistrationSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetObjectLostNotificationRegistrationsSpectraS3Response GetObjectLostNotificationRegistrationsSpectraS3(GetObjectLostNotificationRegistrationsSpectraS3Request request)
        {
            return new GetObjectLostNotificationRegistrationsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetObjectPersistedNotificationRegistrationSpectraS3Response GetObjectPersistedNotificationRegistrationSpectraS3(GetObjectPersistedNotificationRegistrationSpectraS3Request request)
        {
            return new GetObjectPersistedNotificationRegistrationSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetObjectPersistedNotificationRegistrationsSpectraS3Response GetObjectPersistedNotificationRegistrationsSpectraS3(GetObjectPersistedNotificationRegistrationsSpectraS3Request request)
        {
            return new GetObjectPersistedNotificationRegistrationsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetPoolFailureNotificationRegistrationSpectraS3Response GetPoolFailureNotificationRegistrationSpectraS3(GetPoolFailureNotificationRegistrationSpectraS3Request request)
        {
            return new GetPoolFailureNotificationRegistrationSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetPoolFailureNotificationRegistrationsSpectraS3Response GetPoolFailureNotificationRegistrationsSpectraS3(GetPoolFailureNotificationRegistrationsSpectraS3Request request)
        {
            return new GetPoolFailureNotificationRegistrationsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetStorageDomainFailureNotificationRegistrationSpectraS3Response GetStorageDomainFailureNotificationRegistrationSpectraS3(GetStorageDomainFailureNotificationRegistrationSpectraS3Request request)
        {
            return new GetStorageDomainFailureNotificationRegistrationSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetStorageDomainFailureNotificationRegistrationsSpectraS3Response GetStorageDomainFailureNotificationRegistrationsSpectraS3(GetStorageDomainFailureNotificationRegistrationsSpectraS3Request request)
        {
            return new GetStorageDomainFailureNotificationRegistrationsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetSystemFailureNotificationRegistrationSpectraS3Response GetSystemFailureNotificationRegistrationSpectraS3(GetSystemFailureNotificationRegistrationSpectraS3Request request)
        {
            return new GetSystemFailureNotificationRegistrationSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetSystemFailureNotificationRegistrationsSpectraS3Response GetSystemFailureNotificationRegistrationsSpectraS3(GetSystemFailureNotificationRegistrationsSpectraS3Request request)
        {
            return new GetSystemFailureNotificationRegistrationsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetTapeFailureNotificationRegistrationSpectraS3Response GetTapeFailureNotificationRegistrationSpectraS3(GetTapeFailureNotificationRegistrationSpectraS3Request request)
        {
            return new GetTapeFailureNotificationRegistrationSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetTapeFailureNotificationRegistrationsSpectraS3Response GetTapeFailureNotificationRegistrationsSpectraS3(GetTapeFailureNotificationRegistrationsSpectraS3Request request)
        {
            return new GetTapeFailureNotificationRegistrationsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetTapePartitionFailureNotificationRegistrationSpectraS3Response GetTapePartitionFailureNotificationRegistrationSpectraS3(GetTapePartitionFailureNotificationRegistrationSpectraS3Request request)
        {
            return new GetTapePartitionFailureNotificationRegistrationSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetTapePartitionFailureNotificationRegistrationsSpectraS3Response GetTapePartitionFailureNotificationRegistrationsSpectraS3(GetTapePartitionFailureNotificationRegistrationsSpectraS3Request request)
        {
            return new GetTapePartitionFailureNotificationRegistrationsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetObjectDetailsSpectraS3Response GetObjectDetailsSpectraS3(GetObjectDetailsSpectraS3Request request)
        {
            return new GetObjectDetailsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetObjectsSpectraS3Response GetObjectsSpectraS3(GetObjectsSpectraS3Request request)
        {
            return new GetObjectsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetObjectsWithFullDetailsSpectraS3Response GetObjectsWithFullDetailsSpectraS3(GetObjectsWithFullDetailsSpectraS3Request request)
        {
            return new GetObjectsWithFullDetailsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetPhysicalPlacementForObjectsSpectraS3Response GetPhysicalPlacementForObjectsSpectraS3(GetPhysicalPlacementForObjectsSpectraS3Request request)
        {
            return new GetPhysicalPlacementForObjectsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetPhysicalPlacementForObjectsWithFullDetailsSpectraS3Response GetPhysicalPlacementForObjectsWithFullDetailsSpectraS3(GetPhysicalPlacementForObjectsWithFullDetailsSpectraS3Request request)
        {
            return new GetPhysicalPlacementForObjectsWithFullDetailsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public VerifyPhysicalPlacementForObjectsSpectraS3Response VerifyPhysicalPlacementForObjectsSpectraS3(VerifyPhysicalPlacementForObjectsSpectraS3Request request)
        {
            return new VerifyPhysicalPlacementForObjectsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public VerifyPhysicalPlacementForObjectsWithFullDetailsSpectraS3Response VerifyPhysicalPlacementForObjectsWithFullDetailsSpectraS3(VerifyPhysicalPlacementForObjectsWithFullDetailsSpectraS3Request request)
        {
            return new VerifyPhysicalPlacementForObjectsWithFullDetailsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public CancelImportPoolSpectraS3Response CancelImportPoolSpectraS3(CancelImportPoolSpectraS3Request request)
        {
            return new CancelImportPoolSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public CompactPoolSpectraS3Response CompactPoolSpectraS3(CompactPoolSpectraS3Request request)
        {
            return new CompactPoolSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutPoolPartitionSpectraS3Response PutPoolPartitionSpectraS3(PutPoolPartitionSpectraS3Request request)
        {
            return new PutPoolPartitionSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public FormatForeignPoolSpectraS3Response FormatForeignPoolSpectraS3(FormatForeignPoolSpectraS3Request request)
        {
            return new FormatForeignPoolSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetBlobsOnPoolSpectraS3Response GetBlobsOnPoolSpectraS3(GetBlobsOnPoolSpectraS3Request request)
        {
            return new GetBlobsOnPoolSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetPoolFailuresSpectraS3Response GetPoolFailuresSpectraS3(GetPoolFailuresSpectraS3Request request)
        {
            return new GetPoolFailuresSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetPoolPartitionSpectraS3Response GetPoolPartitionSpectraS3(GetPoolPartitionSpectraS3Request request)
        {
            return new GetPoolPartitionSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetPoolPartitionsSpectraS3Response GetPoolPartitionsSpectraS3(GetPoolPartitionsSpectraS3Request request)
        {
            return new GetPoolPartitionsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetPoolSpectraS3Response GetPoolSpectraS3(GetPoolSpectraS3Request request)
        {
            return new GetPoolSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetPoolsSpectraS3Response GetPoolsSpectraS3(GetPoolsSpectraS3Request request)
        {
            return new GetPoolsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public ImportPoolSpectraS3Response ImportPoolSpectraS3(ImportPoolSpectraS3Request request)
        {
            return new ImportPoolSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public ModifyPoolPartitionSpectraS3Response ModifyPoolPartitionSpectraS3(ModifyPoolPartitionSpectraS3Request request)
        {
            return new ModifyPoolPartitionSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public ModifyPoolSpectraS3Response ModifyPoolSpectraS3(ModifyPoolSpectraS3Request request)
        {
            return new ModifyPoolSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public VerifyPoolSpectraS3Response VerifyPoolSpectraS3(VerifyPoolSpectraS3Request request)
        {
            return new VerifyPoolSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutPoolStorageDomainMemberSpectraS3Response PutPoolStorageDomainMemberSpectraS3(PutPoolStorageDomainMemberSpectraS3Request request)
        {
            return new PutPoolStorageDomainMemberSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutStorageDomainSpectraS3Response PutStorageDomainSpectraS3(PutStorageDomainSpectraS3Request request)
        {
            return new PutStorageDomainSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutTapeStorageDomainMemberSpectraS3Response PutTapeStorageDomainMemberSpectraS3(PutTapeStorageDomainMemberSpectraS3Request request)
        {
            return new PutTapeStorageDomainMemberSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetStorageDomainFailuresSpectraS3Response GetStorageDomainFailuresSpectraS3(GetStorageDomainFailuresSpectraS3Request request)
        {
            return new GetStorageDomainFailuresSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetStorageDomainMemberSpectraS3Response GetStorageDomainMemberSpectraS3(GetStorageDomainMemberSpectraS3Request request)
        {
            return new GetStorageDomainMemberSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetStorageDomainMembersSpectraS3Response GetStorageDomainMembersSpectraS3(GetStorageDomainMembersSpectraS3Request request)
        {
            return new GetStorageDomainMembersSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetStorageDomainSpectraS3Response GetStorageDomainSpectraS3(GetStorageDomainSpectraS3Request request)
        {
            return new GetStorageDomainSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetStorageDomainsSpectraS3Response GetStorageDomainsSpectraS3(GetStorageDomainsSpectraS3Request request)
        {
            return new GetStorageDomainsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public ModifyStorageDomainMemberSpectraS3Response ModifyStorageDomainMemberSpectraS3(ModifyStorageDomainMemberSpectraS3Request request)
        {
            return new ModifyStorageDomainMemberSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public ModifyStorageDomainSpectraS3Response ModifyStorageDomainSpectraS3(ModifyStorageDomainSpectraS3Request request)
        {
            return new ModifyStorageDomainSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetSystemFailuresSpectraS3Response GetSystemFailuresSpectraS3(GetSystemFailuresSpectraS3Request request)
        {
            return new GetSystemFailuresSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetSystemInformationSpectraS3Response GetSystemInformationSpectraS3(GetSystemInformationSpectraS3Request request)
        {
            return new GetSystemInformationSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public VerifySystemHealthSpectraS3Response VerifySystemHealthSpectraS3(VerifySystemHealthSpectraS3Request request)
        {
            return new VerifySystemHealthSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public CancelEjectOnAllTapesSpectraS3Response CancelEjectOnAllTapesSpectraS3(CancelEjectOnAllTapesSpectraS3Request request)
        {
            return new CancelEjectOnAllTapesSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public CancelEjectTapeSpectraS3Response CancelEjectTapeSpectraS3(CancelEjectTapeSpectraS3Request request)
        {
            return new CancelEjectTapeSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public CancelFormatOnAllTapesSpectraS3Response CancelFormatOnAllTapesSpectraS3(CancelFormatOnAllTapesSpectraS3Request request)
        {
            return new CancelFormatOnAllTapesSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public CancelFormatTapeSpectraS3Response CancelFormatTapeSpectraS3(CancelFormatTapeSpectraS3Request request)
        {
            return new CancelFormatTapeSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public CancelImportOnAllTapesSpectraS3Response CancelImportOnAllTapesSpectraS3(CancelImportOnAllTapesSpectraS3Request request)
        {
            return new CancelImportOnAllTapesSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public CancelImportTapeSpectraS3Response CancelImportTapeSpectraS3(CancelImportTapeSpectraS3Request request)
        {
            return new CancelImportTapeSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public CancelOnlineOnAllTapesSpectraS3Response CancelOnlineOnAllTapesSpectraS3(CancelOnlineOnAllTapesSpectraS3Request request)
        {
            return new CancelOnlineOnAllTapesSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public CancelOnlineTapeSpectraS3Response CancelOnlineTapeSpectraS3(CancelOnlineTapeSpectraS3Request request)
        {
            return new CancelOnlineTapeSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public CleanTapeDriveSpectraS3Response CleanTapeDriveSpectraS3(CleanTapeDriveSpectraS3Request request)
        {
            return new CleanTapeDriveSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public PutTapeDensityDirectiveSpectraS3Response PutTapeDensityDirectiveSpectraS3(PutTapeDensityDirectiveSpectraS3Request request)
        {
            return new PutTapeDensityDirectiveSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public EjectAllTapesSpectraS3Response EjectAllTapesSpectraS3(EjectAllTapesSpectraS3Request request)
        {
            return new EjectAllTapesSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public EjectStorageDomainSpectraS3Response EjectStorageDomainSpectraS3(EjectStorageDomainSpectraS3Request request)
        {
            return new EjectStorageDomainSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public EjectTapeSpectraS3Response EjectTapeSpectraS3(EjectTapeSpectraS3Request request)
        {
            return new EjectTapeSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public FormatAllTapesSpectraS3Response FormatAllTapesSpectraS3(FormatAllTapesSpectraS3Request request)
        {
            return new FormatAllTapesSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public FormatTapeSpectraS3Response FormatTapeSpectraS3(FormatTapeSpectraS3Request request)
        {
            return new FormatTapeSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetBlobsOnTapeSpectraS3Response GetBlobsOnTapeSpectraS3(GetBlobsOnTapeSpectraS3Request request)
        {
            return new GetBlobsOnTapeSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetTapeDensityDirectiveSpectraS3Response GetTapeDensityDirectiveSpectraS3(GetTapeDensityDirectiveSpectraS3Request request)
        {
            return new GetTapeDensityDirectiveSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetTapeDensityDirectivesSpectraS3Response GetTapeDensityDirectivesSpectraS3(GetTapeDensityDirectivesSpectraS3Request request)
        {
            return new GetTapeDensityDirectivesSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetTapeDriveSpectraS3Response GetTapeDriveSpectraS3(GetTapeDriveSpectraS3Request request)
        {
            return new GetTapeDriveSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetTapeDrivesSpectraS3Response GetTapeDrivesSpectraS3(GetTapeDrivesSpectraS3Request request)
        {
            return new GetTapeDrivesSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetTapeFailuresSpectraS3Response GetTapeFailuresSpectraS3(GetTapeFailuresSpectraS3Request request)
        {
            return new GetTapeFailuresSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetTapeLibrariesSpectraS3Response GetTapeLibrariesSpectraS3(GetTapeLibrariesSpectraS3Request request)
        {
            return new GetTapeLibrariesSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetTapeLibrarySpectraS3Response GetTapeLibrarySpectraS3(GetTapeLibrarySpectraS3Request request)
        {
            return new GetTapeLibrarySpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetTapePartitionFailuresSpectraS3Response GetTapePartitionFailuresSpectraS3(GetTapePartitionFailuresSpectraS3Request request)
        {
            return new GetTapePartitionFailuresSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetTapePartitionSpectraS3Response GetTapePartitionSpectraS3(GetTapePartitionSpectraS3Request request)
        {
            return new GetTapePartitionSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetTapePartitionWithFullDetailsSpectraS3Response GetTapePartitionWithFullDetailsSpectraS3(GetTapePartitionWithFullDetailsSpectraS3Request request)
        {
            return new GetTapePartitionWithFullDetailsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetTapePartitionsSpectraS3Response GetTapePartitionsSpectraS3(GetTapePartitionsSpectraS3Request request)
        {
            return new GetTapePartitionsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetTapePartitionsWithFullDetailsSpectraS3Response GetTapePartitionsWithFullDetailsSpectraS3(GetTapePartitionsWithFullDetailsSpectraS3Request request)
        {
            return new GetTapePartitionsWithFullDetailsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetTapeSpectraS3Response GetTapeSpectraS3(GetTapeSpectraS3Request request)
        {
            return new GetTapeSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetTapeWithFullDetailsSpectraS3Response GetTapeWithFullDetailsSpectraS3(GetTapeWithFullDetailsSpectraS3Request request)
        {
            return new GetTapeWithFullDetailsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetTapesSpectraS3Response GetTapesSpectraS3(GetTapesSpectraS3Request request)
        {
            return new GetTapesSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetTapesWithFullDetailsSpectraS3Response GetTapesWithFullDetailsSpectraS3(GetTapesWithFullDetailsSpectraS3Request request)
        {
            return new GetTapesWithFullDetailsSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public ImportAllTapesSpectraS3Response ImportAllTapesSpectraS3(ImportAllTapesSpectraS3Request request)
        {
            return new ImportAllTapesSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public ImportTapeSpectraS3Response ImportTapeSpectraS3(ImportTapeSpectraS3Request request)
        {
            return new ImportTapeSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public InspectAllTapesSpectraS3Response InspectAllTapesSpectraS3(InspectAllTapesSpectraS3Request request)
        {
            return new InspectAllTapesSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public InspectTapeSpectraS3Response InspectTapeSpectraS3(InspectTapeSpectraS3Request request)
        {
            return new InspectTapeSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public ModifyTapePartitionSpectraS3Response ModifyTapePartitionSpectraS3(ModifyTapePartitionSpectraS3Request request)
        {
            return new ModifyTapePartitionSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public ModifyTapeSpectraS3Response ModifyTapeSpectraS3(ModifyTapeSpectraS3Request request)
        {
            return new ModifyTapeSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public OnlineAllTapesSpectraS3Response OnlineAllTapesSpectraS3(OnlineAllTapesSpectraS3Request request)
        {
            return new OnlineAllTapesSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public OnlineTapeSpectraS3Response OnlineTapeSpectraS3(OnlineTapeSpectraS3Request request)
        {
            return new OnlineTapeSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public VerifyAllTapesSpectraS3Response VerifyAllTapesSpectraS3(VerifyAllTapesSpectraS3Request request)
        {
            return new VerifyAllTapesSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public VerifyTapeSpectraS3Response VerifyTapeSpectraS3(VerifyTapeSpectraS3Request request)
        {
            return new VerifyTapeSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetUserSpectraS3Response GetUserSpectraS3(GetUserSpectraS3Request request)
        {
            return new GetUserSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public GetUsersSpectraS3Response GetUsersSpectraS3(GetUsersSpectraS3Request request)
        {
            return new GetUsersSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public ModifyUserSpectraS3Response ModifyUserSpectraS3(ModifyUserSpectraS3Request request)
        {
            return new ModifyUserSpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }

        public RegenerateUserSecretKeySpectraS3Response RegenerateUserSecretKeySpectraS3(RegenerateUserSecretKeySpectraS3Request request)
        {
            return new RegenerateUserSecretKeySpectraS3ResponseParser().Parse(request, _netLayer.Invoke(request));
        }
        
        public void AbortMultiPartUpload(AbortMultiPartUploadRequest request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void PutBucket(PutBucketRequest request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.OK);
            }
        }

        public void PutMultiPartUploadPart(PutMultiPartUploadPartRequest request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.OK);
            }
        }

        public void PutObject(PutObjectRequest request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.OK);
            }
        }

        public void DeleteBucket(DeleteBucketRequest request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeleteObject(DeleteObjectRequest request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeleteBucketAclSpectraS3(DeleteBucketAclSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeleteDataPolicyAclSpectraS3(DeleteDataPolicyAclSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeleteBucketSpectraS3(DeleteBucketSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void ForceFullCacheReclaimSpectraS3(ForceFullCacheReclaimSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeleteDataPersistenceRuleSpectraS3(DeleteDataPersistenceRuleSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeleteDataPolicySpectraS3(DeleteDataPolicySpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeleteGroupMemberSpectraS3(DeleteGroupMemberSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeleteGroupSpectraS3(DeleteGroupSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void CancelAllJobsSpectraS3(CancelAllJobsSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void CancelJobSpectraS3(CancelJobSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void ClearAllCanceledJobsSpectraS3(ClearAllCanceledJobsSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void ClearAllCompletedJobsSpectraS3(ClearAllCompletedJobsSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeleteJobCompletedNotificationRegistrationSpectraS3(DeleteJobCompletedNotificationRegistrationSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeleteJobCreatedNotificationRegistrationSpectraS3(DeleteJobCreatedNotificationRegistrationSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeleteObjectCachedNotificationRegistrationSpectraS3(DeleteObjectCachedNotificationRegistrationSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeleteObjectLostNotificationRegistrationSpectraS3(DeleteObjectLostNotificationRegistrationSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeleteObjectPersistedNotificationRegistrationSpectraS3(DeleteObjectPersistedNotificationRegistrationSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeletePoolFailureNotificationRegistrationSpectraS3(DeletePoolFailureNotificationRegistrationSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeleteStorageDomainFailureNotificationRegistrationSpectraS3(DeleteStorageDomainFailureNotificationRegistrationSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeleteSystemFailureNotificationRegistrationSpectraS3(DeleteSystemFailureNotificationRegistrationSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeleteTapeFailureNotificationRegistrationSpectraS3(DeleteTapeFailureNotificationRegistrationSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeleteTapePartitionFailureNotificationRegistrationSpectraS3(DeleteTapePartitionFailureNotificationRegistrationSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeleteFolderRecursivelySpectraS3(DeleteFolderRecursivelySpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void CancelImportOnAllPoolsSpectraS3(CancelImportOnAllPoolsSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void CompactAllPoolsSpectraS3(CompactAllPoolsSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeallocatePoolSpectraS3(DeallocatePoolSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeletePermanentlyLostPoolSpectraS3(DeletePermanentlyLostPoolSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeletePoolFailureSpectraS3(DeletePoolFailureSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeletePoolPartitionSpectraS3(DeletePoolPartitionSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void ForcePoolEnvironmentRefreshSpectraS3(ForcePoolEnvironmentRefreshSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void FormatAllForeignPoolsSpectraS3(FormatAllForeignPoolsSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void ImportAllPoolsSpectraS3(ImportAllPoolsSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void ModifyAllPoolsSpectraS3(ModifyAllPoolsSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void VerifyAllPoolsSpectraS3(VerifyAllPoolsSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeleteStorageDomainFailureSpectraS3(DeleteStorageDomainFailureSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeleteStorageDomainMemberSpectraS3(DeleteStorageDomainMemberSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeleteStorageDomainSpectraS3(DeleteStorageDomainSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeletePermanentlyLostTapeSpectraS3(DeletePermanentlyLostTapeSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeleteTapeDensityDirectiveSpectraS3(DeleteTapeDensityDirectiveSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeleteTapeDriveSpectraS3(DeleteTapeDriveSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeleteTapeFailureSpectraS3(DeleteTapeFailureSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeleteTapePartitionFailureSpectraS3(DeleteTapePartitionFailureSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void DeleteTapePartitionSpectraS3(DeleteTapePartitionSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void EjectStorageDomainBlobsSpectraS3(EjectStorageDomainBlobsSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void ForceTapeEnvironmentRefreshSpectraS3(ForceTapeEnvironmentRefreshSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }

        public void ModifyAllTapePartitionsSpectraS3(ModifyAllTapePartitionsSpectraS3Request request)
        {
            using (var response = _netLayer.Invoke(request))
            {
                ResponseParseUtilities.HandleStatusCode(response, HttpStatusCode.NoContent);
            }
        }
        
        public GetObjectResponse GetObject(GetObjectRequest request)
        {
            return new GetObjectResponseParser(_netLayer.CopyBufferSize).Parse(request, _netLayer.Invoke(request));
        }

        public IDs3ClientFactory BuildFactory(IEnumerable<JobNode> nodes)
        {
            return new Ds3ClientFactory(this, nodes);
        }

        private class Ds3ClientFactory : IDs3ClientFactory
        {
            private readonly IDs3Client _client;
            private readonly IDictionary<Guid, JobNode> _nodes;

            public Ds3ClientFactory(IDs3Client client, IEnumerable<JobNode> nodes)
            {
                this._client = client;
                this._nodes = nodes.ToDictionary(node => node.Id);
            }

            public IDs3Client GetClientForNodeId(Guid? nodeId)
            {
                return this._client;
            }
        }
    }
}