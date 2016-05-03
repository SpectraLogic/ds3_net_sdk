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
using Ds3.Models;
using Ds3.Runtime;
using System;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace Ds3.ResponseParsers
{
    public class ModelParsers
    {

        public static PhysicalPlacement ParsePhysicalPlacement(XElement element)
        {
            return new PhysicalPlacement
            {
                Pools = element.Element("Pools").Elements("Pool").Select(ParsePool).ToList(),
                Tapes = element.Element("Tapes").Elements("Tape").Select(ParseTape).ToList()
            };
        }

        public static PhysicalPlacement ParseNullablePhysicalPlacement(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParsePhysicalPlacement(element);
        }
        public static Blob ParseBlob(XElement element)
        {
            return new Blob
            {
                ByteOffset = ParseLong(element.Element("ByteOffset")),
                Checksum = ParseNullableString(element.Element("Checksum")),
                ChecksumType = ParseNullableChecksumType(element.Element("ChecksumType")),
                Id = ParseGuid(element.Element("Id")),
                Length = ParseLong(element.Element("Length")),
                ObjectId = ParseGuid(element.Element("ObjectId"))
            };
        }

        public static Blob ParseNullableBlob(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseBlob(element);
        }
        public static Bucket ParseBucket(XElement element)
        {
            return new Bucket
            {
                CreationDate = ParseDateTime(element.Element("CreationDate")),
                DataPolicyId = ParseGuid(element.Element("DataPolicyId")),
                Id = ParseGuid(element.Element("Id")),
                LastPreferredChunkSizeInBytes = ParseNullableLong(element.Element("LastPreferredChunkSizeInBytes")),
                LogicalUsedCapacity = ParseNullableLong(element.Element("LogicalUsedCapacity")),
                Name = ParseNullableString(element.Element("Name")),
                UserId = ParseGuid(element.Element("UserId"))
            };
        }

        public static Bucket ParseNullableBucket(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseBucket(element);
        }
        public static BucketAcl ParseBucketAcl(XElement element)
        {
            return new BucketAcl
            {
                BucketId = ParseNullableGuid(element.Element("BucketId")),
                GroupId = ParseNullableGuid(element.Element("GroupId")),
                Id = ParseGuid(element.Element("Id")),
                Permission = ParseBucketAclPermission(element.Element("Permission")),
                UserId = ParseNullableGuid(element.Element("UserId"))
            };
        }

        public static BucketAcl ParseNullableBucketAcl(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseBucketAcl(element);
        }
        public static CanceledJob ParseCanceledJob(XElement element)
        {
            return new CanceledJob
            {
                BucketId = ParseGuid(element.Element("BucketId")),
                CachedSizeInBytes = ParseLong(element.Element("CachedSizeInBytes")),
                ChunkClientProcessingOrderGuarantee = ParseJobChunkClientProcessingOrderGuarantee(element.Element("ChunkClientProcessingOrderGuarantee")),
                CompletedSizeInBytes = ParseLong(element.Element("CompletedSizeInBytes")),
                CreatedAt = ParseDateTime(element.Element("CreatedAt")),
                DateCanceled = ParseDateTime(element.Element("DateCanceled")),
                ErrorMessage = ParseNullableString(element.Element("ErrorMessage")),
                Id = ParseGuid(element.Element("Id")),
                Naked = ParseBool(element.Element("Naked")),
                Name = ParseNullableString(element.Element("Name")),
                OriginalSizeInBytes = ParseLong(element.Element("OriginalSizeInBytes")),
                Priority = ParsePriority(element.Element("Priority")),
                Rechunked = ParseNullableDateTime(element.Element("Rechunked")),
                RequestType = ParseJobRequestType(element.Element("RequestType")),
                Truncated = ParseBool(element.Element("Truncated")),
                UserId = ParseGuid(element.Element("UserId"))
            };
        }

        public static CanceledJob ParseNullableCanceledJob(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseCanceledJob(element);
        }
        public static CapacitySummaryContainer ParseCapacitySummaryContainer(XElement element)
        {
            return new CapacitySummaryContainer
            {
                Pool = ParseStorageDomainCapacitySummary(element.Element("Pool")),
                Tape = ParseStorageDomainCapacitySummary(element.Element("Tape"))
            };
        }

        public static CapacitySummaryContainer ParseNullableCapacitySummaryContainer(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseCapacitySummaryContainer(element);
        }
        public static CompletedJob ParseCompletedJob(XElement element)
        {
            return new CompletedJob
            {
                BucketId = ParseGuid(element.Element("BucketId")),
                CachedSizeInBytes = ParseLong(element.Element("CachedSizeInBytes")),
                ChunkClientProcessingOrderGuarantee = ParseJobChunkClientProcessingOrderGuarantee(element.Element("ChunkClientProcessingOrderGuarantee")),
                CompletedSizeInBytes = ParseLong(element.Element("CompletedSizeInBytes")),
                CreatedAt = ParseDateTime(element.Element("CreatedAt")),
                DateCompleted = ParseDateTime(element.Element("DateCompleted")),
                ErrorMessage = ParseNullableString(element.Element("ErrorMessage")),
                Id = ParseGuid(element.Element("Id")),
                Naked = ParseBool(element.Element("Naked")),
                Name = ParseNullableString(element.Element("Name")),
                OriginalSizeInBytes = ParseLong(element.Element("OriginalSizeInBytes")),
                Priority = ParsePriority(element.Element("Priority")),
                Rechunked = ParseNullableDateTime(element.Element("Rechunked")),
                RequestType = ParseJobRequestType(element.Element("RequestType")),
                Truncated = ParseBool(element.Element("Truncated")),
                UserId = ParseGuid(element.Element("UserId"))
            };
        }

        public static CompletedJob ParseNullableCompletedJob(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseCompletedJob(element);
        }
        public static DataPathBackend ParseDataPathBackend(XElement element)
        {
            return new DataPathBackend
            {
                Activated = ParseBool(element.Element("Activated")),
                AutoActivateTimeoutInMins = ParseNullableInt(element.Element("AutoActivateTimeoutInMins")),
                AutoInspect = ParseAutoInspectMode(element.Element("AutoInspect")),
                DefaultImportConflictResolutionMode = ParseImportConflictResolutionMode(element.Element("DefaultImportConflictResolutionMode")),
                Id = ParseGuid(element.Element("Id")),
                LastHeartbeat = ParseDateTime(element.Element("LastHeartbeat")),
                UnavailableMediaPolicy = ParseUnavailableMediaUsagePolicy(element.Element("UnavailableMediaPolicy")),
                UnavailablePoolMaxJobRetryInMins = ParseInt(element.Element("UnavailablePoolMaxJobRetryInMins")),
                UnavailableTapePartitionMaxJobRetryInMins = ParseInt(element.Element("UnavailableTapePartitionMaxJobRetryInMins"))
            };
        }

        public static DataPathBackend ParseNullableDataPathBackend(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDataPathBackend(element);
        }
        public static DataPersistenceRule ParseDataPersistenceRule(XElement element)
        {
            return new DataPersistenceRule
            {
                DataPolicyId = ParseGuid(element.Element("DataPolicyId")),
                Id = ParseGuid(element.Element("Id")),
                IsolationLevel = ParseDataIsolationLevel(element.Element("IsolationLevel")),
                MinimumDaysToRetain = ParseNullableInt(element.Element("MinimumDaysToRetain")),
                State = ParseDataPersistenceRuleState(element.Element("State")),
                StorageDomainId = ParseGuid(element.Element("StorageDomainId")),
                Type = ParseDataPersistenceRuleType(element.Element("Type"))
            };
        }

        public static DataPersistenceRule ParseNullableDataPersistenceRule(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDataPersistenceRule(element);
        }
        public static DataPolicy ParseDataPolicy(XElement element)
        {
            return new DataPolicy
            {
                BlobbingEnabled = ParseBool(element.Element("BlobbingEnabled")),
                ChecksumType = ParseChecksumType(element.Element("ChecksumType")),
                CreationDate = ParseDateTime(element.Element("CreationDate")),
                DefaultBlobSize = ParseNullableLong(element.Element("DefaultBlobSize")),
                DefaultGetJobPriority = ParsePriority(element.Element("DefaultGetJobPriority")),
                DefaultPutJobPriority = ParsePriority(element.Element("DefaultPutJobPriority")),
                DefaultVerifyJobPriority = ParsePriority(element.Element("DefaultVerifyJobPriority")),
                EndToEndCrcRequired = ParseBool(element.Element("EndToEndCrcRequired")),
                Id = ParseGuid(element.Element("Id")),
                LtfsObjectNamingAllowed = ParseBool(element.Element("LtfsObjectNamingAllowed")),
                Name = ParseNullableString(element.Element("Name")),
                RebuildPriority = ParsePriority(element.Element("RebuildPriority")),
                Versioning = ParseVersioningLevel(element.Element("Versioning"))
            };
        }

        public static DataPolicy ParseNullableDataPolicy(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDataPolicy(element);
        }
        public static DataPolicyAcl ParseDataPolicyAcl(XElement element)
        {
            return new DataPolicyAcl
            {
                DataPolicyId = ParseNullableGuid(element.Element("DataPolicyId")),
                GroupId = ParseNullableGuid(element.Element("GroupId")),
                Id = ParseGuid(element.Element("Id")),
                UserId = ParseNullableGuid(element.Element("UserId"))
            };
        }

        public static DataPolicyAcl ParseNullableDataPolicyAcl(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDataPolicyAcl(element);
        }
        public static Group ParseGroup(XElement element)
        {
            return new Group
            {
                BuiltIn = ParseBool(element.Element("BuiltIn")),
                Id = ParseGuid(element.Element("Id")),
                Name = ParseNullableString(element.Element("Name"))
            };
        }

        public static Group ParseNullableGroup(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseGroup(element);
        }
        public static GroupMember ParseGroupMember(XElement element)
        {
            return new GroupMember
            {
                GroupId = ParseGuid(element.Element("GroupId")),
                Id = ParseGuid(element.Element("Id")),
                MemberGroupId = ParseNullableGuid(element.Element("MemberGroupId")),
                MemberUserId = ParseNullableGuid(element.Element("MemberUserId"))
            };
        }

        public static GroupMember ParseNullableGroupMember(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseGroupMember(element);
        }
        public static ActiveJob ParseActiveJob(XElement element)
        {
            return new ActiveJob
            {
                Aggregating = ParseBool(element.Element("Aggregating")),
                BucketId = ParseGuid(element.Element("BucketId")),
                CachedSizeInBytes = ParseLong(element.Element("CachedSizeInBytes")),
                ChunkClientProcessingOrderGuarantee = ParseJobChunkClientProcessingOrderGuarantee(element.Element("ChunkClientProcessingOrderGuarantee")),
                CompletedSizeInBytes = ParseLong(element.Element("CompletedSizeInBytes")),
                CreatedAt = ParseDateTime(element.Element("CreatedAt")),
                ErrorMessage = ParseNullableString(element.Element("ErrorMessage")),
                Id = ParseGuid(element.Element("Id")),
                Naked = ParseBool(element.Element("Naked")),
                Name = ParseNullableString(element.Element("Name")),
                OriginalSizeInBytes = ParseLong(element.Element("OriginalSizeInBytes")),
                Priority = ParsePriority(element.Element("Priority")),
                Rechunked = ParseNullableDateTime(element.Element("Rechunked")),
                RequestType = ParseJobRequestType(element.Element("RequestType")),
                Truncated = ParseBool(element.Element("Truncated")),
                UserId = ParseGuid(element.Element("UserId"))
            };
        }

        public static ActiveJob ParseNullableActiveJob(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseActiveJob(element);
        }
        public static Node ParseNode(XElement element)
        {
            return new Node
            {
                DataPathHttpPort = ParseNullableInt(element.Element("DataPathHttpPort")),
                DataPathHttpsPort = ParseNullableInt(element.Element("DataPathHttpsPort")),
                DataPathIpAddress = ParseNullableString(element.Element("DataPathIpAddress")),
                DnsName = ParseNullableString(element.Element("DnsName")),
                Id = ParseGuid(element.Element("Id")),
                LastHeartbeat = ParseDateTime(element.Element("LastHeartbeat")),
                Name = ParseNullableString(element.Element("Name")),
                SerialNumber = ParseNullableString(element.Element("SerialNumber"))
            };
        }

        public static Node ParseNullableNode(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseNode(element);
        }
        public static S3Object ParseS3Object(XElement element)
        {
            return new S3Object
            {
                BucketId = ParseGuid(element.Element("BucketId")),
                CreationDate = ParseNullableDateTime(element.Element("CreationDate")),
                Id = ParseGuid(element.Element("Id")),
                Latest = ParseBool(element.Element("Latest")),
                Name = ParseNullableString(element.Element("Name")),
                Type = ParseS3ObjectType(element.Element("Type")),
                Version = ParseLong(element.Element("Version"))
            };
        }

        public static S3Object ParseNullableS3Object(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseS3Object(element);
        }
        public static StorageDomain ParseStorageDomain(XElement element)
        {
            return new StorageDomain
            {
                AutoEjectUponCron = ParseNullableString(element.Element("AutoEjectUponCron")),
                AutoEjectUponJobCancellation = ParseBool(element.Element("AutoEjectUponJobCancellation")),
                AutoEjectUponJobCompletion = ParseBool(element.Element("AutoEjectUponJobCompletion")),
                AutoEjectUponMediaFull = ParseBool(element.Element("AutoEjectUponMediaFull")),
                Id = ParseGuid(element.Element("Id")),
                LtfsFileNaming = ParseLtfsFileNamingMode(element.Element("LtfsFileNaming")),
                MaxTapeFragmentationPercent = ParseInt(element.Element("MaxTapeFragmentationPercent")),
                MaximumAutoVerificationFrequencyInDays = ParseInt(element.Element("MaximumAutoVerificationFrequencyInDays")),
                MediaEjectionAllowed = ParseBool(element.Element("MediaEjectionAllowed")),
                Name = ParseNullableString(element.Element("Name")),
                VerifyPriorToAutoEject = ParseNullablePriority(element.Element("VerifyPriorToAutoEject")),
                WriteOptimization = ParseWriteOptimization(element.Element("WriteOptimization"))
            };
        }

        public static StorageDomain ParseNullableStorageDomain(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseStorageDomain(element);
        }
        public static StorageDomainCapacitySummary ParseStorageDomainCapacitySummary(XElement element)
        {
            return new StorageDomainCapacitySummary
            {
                PhysicalAllocated = ParseLong(element.Element("PhysicalAllocated")),
                PhysicalFree = ParseLong(element.Element("PhysicalFree")),
                PhysicalUsed = ParseLong(element.Element("PhysicalUsed"))
            };
        }

        public static StorageDomainCapacitySummary ParseNullableStorageDomainCapacitySummary(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseStorageDomainCapacitySummary(element);
        }
        public static StorageDomainFailure ParseStorageDomainFailure(XElement element)
        {
            return new StorageDomainFailure
            {
                Date = ParseDateTime(element.Element("Date")),
                ErrorMessage = ParseNullableString(element.Element("ErrorMessage")),
                Id = ParseGuid(element.Element("Id")),
                StorageDomainId = ParseGuid(element.Element("StorageDomainId")),
                Type = ParseStorageDomainFailureType(element.Element("Type"))
            };
        }

        public static StorageDomainFailure ParseNullableStorageDomainFailure(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseStorageDomainFailure(element);
        }
        public static StorageDomainMember ParseStorageDomainMember(XElement element)
        {
            return new StorageDomainMember
            {
                Id = ParseGuid(element.Element("Id")),
                PoolPartitionId = ParseNullableGuid(element.Element("PoolPartitionId")),
                State = ParseStorageDomainMemberState(element.Element("State")),
                StorageDomainId = ParseGuid(element.Element("StorageDomainId")),
                TapePartitionId = ParseNullableGuid(element.Element("TapePartitionId")),
                TapeType = ParseNullableTapeType(element.Element("TapeType")),
                WritePreference = ParseWritePreferenceLevel(element.Element("WritePreference"))
            };
        }

        public static StorageDomainMember ParseNullableStorageDomainMember(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseStorageDomainMember(element);
        }
        public static SystemFailure ParseSystemFailure(XElement element)
        {
            return new SystemFailure
            {
                Date = ParseDateTime(element.Element("Date")),
                ErrorMessage = ParseNullableString(element.Element("ErrorMessage")),
                Id = ParseGuid(element.Element("Id")),
                Type = ParseSystemFailureType(element.Element("Type"))
            };
        }

        public static SystemFailure ParseNullableSystemFailure(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseSystemFailure(element);
        }
        public static SpectraUser ParseSpectraUser(XElement element)
        {
            return new SpectraUser
            {
                AuthId = ParseNullableString(element.Element("AuthId")),
                DefaultDataPolicyId = ParseNullableGuid(element.Element("DefaultDataPolicyId")),
                Id = ParseGuid(element.Element("Id")),
                Name = ParseNullableString(element.Element("Name")),
                SecretKey = ParseNullableString(element.Element("SecretKey"))
            };
        }

        public static SpectraUser ParseNullableSpectraUser(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseSpectraUser(element);
        }
        public static JobCompletedNotificationRegistration ParseJobCompletedNotificationRegistration(XElement element)
        {
            return new JobCompletedNotificationRegistration
            {
                CreationDate = ParseDateTime(element.Element("CreationDate")),
                Format = ParseHttpResponseFormatType(element.Element("Format")),
                Id = ParseGuid(element.Element("Id")),
                JobId = ParseNullableGuid(element.Element("JobId")),
                LastFailure = ParseNullableString(element.Element("LastFailure")),
                LastHttpResponseCode = ParseNullableInt(element.Element("LastHttpResponseCode")),
                LastNotification = ParseNullableDateTime(element.Element("LastNotification")),
                NamingConvention = ParseNamingConventionType(element.Element("NamingConvention")),
                NotificationEndPoint = ParseNullableString(element.Element("NotificationEndPoint")),
                NotificationHttpMethod = ParseRequestType(element.Element("NotificationHttpMethod")),
                NumberOfFailuresSinceLastSuccess = ParseInt(element.Element("NumberOfFailuresSinceLastSuccess")),
                UserId = ParseNullableGuid(element.Element("UserId"))
            };
        }

        public static JobCompletedNotificationRegistration ParseNullableJobCompletedNotificationRegistration(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseJobCompletedNotificationRegistration(element);
        }
        public static JobCreatedNotificationRegistration ParseJobCreatedNotificationRegistration(XElement element)
        {
            return new JobCreatedNotificationRegistration
            {
                CreationDate = ParseDateTime(element.Element("CreationDate")),
                Format = ParseHttpResponseFormatType(element.Element("Format")),
                Id = ParseGuid(element.Element("Id")),
                LastFailure = ParseNullableString(element.Element("LastFailure")),
                LastHttpResponseCode = ParseNullableInt(element.Element("LastHttpResponseCode")),
                LastNotification = ParseNullableDateTime(element.Element("LastNotification")),
                NamingConvention = ParseNamingConventionType(element.Element("NamingConvention")),
                NotificationEndPoint = ParseNullableString(element.Element("NotificationEndPoint")),
                NotificationHttpMethod = ParseRequestType(element.Element("NotificationHttpMethod")),
                NumberOfFailuresSinceLastSuccess = ParseInt(element.Element("NumberOfFailuresSinceLastSuccess")),
                UserId = ParseNullableGuid(element.Element("UserId"))
            };
        }

        public static JobCreatedNotificationRegistration ParseNullableJobCreatedNotificationRegistration(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseJobCreatedNotificationRegistration(element);
        }
        public static PoolFailureNotificationRegistration ParsePoolFailureNotificationRegistration(XElement element)
        {
            return new PoolFailureNotificationRegistration
            {
                CreationDate = ParseDateTime(element.Element("CreationDate")),
                Format = ParseHttpResponseFormatType(element.Element("Format")),
                Id = ParseGuid(element.Element("Id")),
                LastFailure = ParseNullableString(element.Element("LastFailure")),
                LastHttpResponseCode = ParseNullableInt(element.Element("LastHttpResponseCode")),
                LastNotification = ParseNullableDateTime(element.Element("LastNotification")),
                NamingConvention = ParseNamingConventionType(element.Element("NamingConvention")),
                NotificationEndPoint = ParseNullableString(element.Element("NotificationEndPoint")),
                NotificationHttpMethod = ParseRequestType(element.Element("NotificationHttpMethod")),
                NumberOfFailuresSinceLastSuccess = ParseInt(element.Element("NumberOfFailuresSinceLastSuccess")),
                UserId = ParseNullableGuid(element.Element("UserId"))
            };
        }

        public static PoolFailureNotificationRegistration ParseNullablePoolFailureNotificationRegistration(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParsePoolFailureNotificationRegistration(element);
        }
        public static S3ObjectCachedNotificationRegistration ParseS3ObjectCachedNotificationRegistration(XElement element)
        {
            return new S3ObjectCachedNotificationRegistration
            {
                CreationDate = ParseDateTime(element.Element("CreationDate")),
                Format = ParseHttpResponseFormatType(element.Element("Format")),
                Id = ParseGuid(element.Element("Id")),
                JobId = ParseNullableGuid(element.Element("JobId")),
                LastFailure = ParseNullableString(element.Element("LastFailure")),
                LastHttpResponseCode = ParseNullableInt(element.Element("LastHttpResponseCode")),
                LastNotification = ParseNullableDateTime(element.Element("LastNotification")),
                NamingConvention = ParseNamingConventionType(element.Element("NamingConvention")),
                NotificationEndPoint = ParseNullableString(element.Element("NotificationEndPoint")),
                NotificationHttpMethod = ParseRequestType(element.Element("NotificationHttpMethod")),
                NumberOfFailuresSinceLastSuccess = ParseInt(element.Element("NumberOfFailuresSinceLastSuccess")),
                UserId = ParseNullableGuid(element.Element("UserId"))
            };
        }

        public static S3ObjectCachedNotificationRegistration ParseNullableS3ObjectCachedNotificationRegistration(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseS3ObjectCachedNotificationRegistration(element);
        }
        public static S3ObjectLostNotificationRegistration ParseS3ObjectLostNotificationRegistration(XElement element)
        {
            return new S3ObjectLostNotificationRegistration
            {
                CreationDate = ParseDateTime(element.Element("CreationDate")),
                Format = ParseHttpResponseFormatType(element.Element("Format")),
                Id = ParseGuid(element.Element("Id")),
                LastFailure = ParseNullableString(element.Element("LastFailure")),
                LastHttpResponseCode = ParseNullableInt(element.Element("LastHttpResponseCode")),
                LastNotification = ParseNullableDateTime(element.Element("LastNotification")),
                NamingConvention = ParseNamingConventionType(element.Element("NamingConvention")),
                NotificationEndPoint = ParseNullableString(element.Element("NotificationEndPoint")),
                NotificationHttpMethod = ParseRequestType(element.Element("NotificationHttpMethod")),
                NumberOfFailuresSinceLastSuccess = ParseInt(element.Element("NumberOfFailuresSinceLastSuccess")),
                UserId = ParseNullableGuid(element.Element("UserId"))
            };
        }

        public static S3ObjectLostNotificationRegistration ParseNullableS3ObjectLostNotificationRegistration(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseS3ObjectLostNotificationRegistration(element);
        }
        public static S3ObjectPersistedNotificationRegistration ParseS3ObjectPersistedNotificationRegistration(XElement element)
        {
            return new S3ObjectPersistedNotificationRegistration
            {
                CreationDate = ParseDateTime(element.Element("CreationDate")),
                Format = ParseHttpResponseFormatType(element.Element("Format")),
                Id = ParseGuid(element.Element("Id")),
                JobId = ParseNullableGuid(element.Element("JobId")),
                LastFailure = ParseNullableString(element.Element("LastFailure")),
                LastHttpResponseCode = ParseNullableInt(element.Element("LastHttpResponseCode")),
                LastNotification = ParseNullableDateTime(element.Element("LastNotification")),
                NamingConvention = ParseNamingConventionType(element.Element("NamingConvention")),
                NotificationEndPoint = ParseNullableString(element.Element("NotificationEndPoint")),
                NotificationHttpMethod = ParseRequestType(element.Element("NotificationHttpMethod")),
                NumberOfFailuresSinceLastSuccess = ParseInt(element.Element("NumberOfFailuresSinceLastSuccess")),
                UserId = ParseNullableGuid(element.Element("UserId"))
            };
        }

        public static S3ObjectPersistedNotificationRegistration ParseNullableS3ObjectPersistedNotificationRegistration(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseS3ObjectPersistedNotificationRegistration(element);
        }
        public static StorageDomainFailureNotificationRegistration ParseStorageDomainFailureNotificationRegistration(XElement element)
        {
            return new StorageDomainFailureNotificationRegistration
            {
                CreationDate = ParseDateTime(element.Element("CreationDate")),
                Format = ParseHttpResponseFormatType(element.Element("Format")),
                Id = ParseGuid(element.Element("Id")),
                LastFailure = ParseNullableString(element.Element("LastFailure")),
                LastHttpResponseCode = ParseNullableInt(element.Element("LastHttpResponseCode")),
                LastNotification = ParseNullableDateTime(element.Element("LastNotification")),
                NamingConvention = ParseNamingConventionType(element.Element("NamingConvention")),
                NotificationEndPoint = ParseNullableString(element.Element("NotificationEndPoint")),
                NotificationHttpMethod = ParseRequestType(element.Element("NotificationHttpMethod")),
                NumberOfFailuresSinceLastSuccess = ParseInt(element.Element("NumberOfFailuresSinceLastSuccess")),
                UserId = ParseNullableGuid(element.Element("UserId"))
            };
        }

        public static StorageDomainFailureNotificationRegistration ParseNullableStorageDomainFailureNotificationRegistration(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseStorageDomainFailureNotificationRegistration(element);
        }
        public static SystemFailureNotificationRegistration ParseSystemFailureNotificationRegistration(XElement element)
        {
            return new SystemFailureNotificationRegistration
            {
                CreationDate = ParseDateTime(element.Element("CreationDate")),
                Format = ParseHttpResponseFormatType(element.Element("Format")),
                Id = ParseGuid(element.Element("Id")),
                LastFailure = ParseNullableString(element.Element("LastFailure")),
                LastHttpResponseCode = ParseNullableInt(element.Element("LastHttpResponseCode")),
                LastNotification = ParseNullableDateTime(element.Element("LastNotification")),
                NamingConvention = ParseNamingConventionType(element.Element("NamingConvention")),
                NotificationEndPoint = ParseNullableString(element.Element("NotificationEndPoint")),
                NotificationHttpMethod = ParseRequestType(element.Element("NotificationHttpMethod")),
                NumberOfFailuresSinceLastSuccess = ParseInt(element.Element("NumberOfFailuresSinceLastSuccess")),
                UserId = ParseNullableGuid(element.Element("UserId"))
            };
        }

        public static SystemFailureNotificationRegistration ParseNullableSystemFailureNotificationRegistration(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseSystemFailureNotificationRegistration(element);
        }
        public static TapeFailureNotificationRegistration ParseTapeFailureNotificationRegistration(XElement element)
        {
            return new TapeFailureNotificationRegistration
            {
                CreationDate = ParseDateTime(element.Element("CreationDate")),
                Format = ParseHttpResponseFormatType(element.Element("Format")),
                Id = ParseGuid(element.Element("Id")),
                LastFailure = ParseNullableString(element.Element("LastFailure")),
                LastHttpResponseCode = ParseNullableInt(element.Element("LastHttpResponseCode")),
                LastNotification = ParseNullableDateTime(element.Element("LastNotification")),
                NamingConvention = ParseNamingConventionType(element.Element("NamingConvention")),
                NotificationEndPoint = ParseNullableString(element.Element("NotificationEndPoint")),
                NotificationHttpMethod = ParseRequestType(element.Element("NotificationHttpMethod")),
                NumberOfFailuresSinceLastSuccess = ParseInt(element.Element("NumberOfFailuresSinceLastSuccess")),
                UserId = ParseNullableGuid(element.Element("UserId"))
            };
        }

        public static TapeFailureNotificationRegistration ParseNullableTapeFailureNotificationRegistration(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapeFailureNotificationRegistration(element);
        }
        public static TapePartitionFailureNotificationRegistration ParseTapePartitionFailureNotificationRegistration(XElement element)
        {
            return new TapePartitionFailureNotificationRegistration
            {
                CreationDate = ParseDateTime(element.Element("CreationDate")),
                Format = ParseHttpResponseFormatType(element.Element("Format")),
                Id = ParseGuid(element.Element("Id")),
                LastFailure = ParseNullableString(element.Element("LastFailure")),
                LastHttpResponseCode = ParseNullableInt(element.Element("LastHttpResponseCode")),
                LastNotification = ParseNullableDateTime(element.Element("LastNotification")),
                NamingConvention = ParseNamingConventionType(element.Element("NamingConvention")),
                NotificationEndPoint = ParseNullableString(element.Element("NotificationEndPoint")),
                NotificationHttpMethod = ParseRequestType(element.Element("NotificationHttpMethod")),
                NumberOfFailuresSinceLastSuccess = ParseInt(element.Element("NumberOfFailuresSinceLastSuccess")),
                UserId = ParseNullableGuid(element.Element("UserId"))
            };
        }

        public static TapePartitionFailureNotificationRegistration ParseNullableTapePartitionFailureNotificationRegistration(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapePartitionFailureNotificationRegistration(element);
        }
        public static CacheFilesystem ParseCacheFilesystem(XElement element)
        {
            return new CacheFilesystem
            {
                AutoReclaimInitiateThreshold = ParseDouble(element.Element("AutoReclaimInitiateThreshold")),
                AutoReclaimTerminateThreshold = ParseDouble(element.Element("AutoReclaimTerminateThreshold")),
                BurstThreshold = ParseDouble(element.Element("BurstThreshold")),
                Id = ParseGuid(element.Element("Id")),
                MaxCapacityInBytes = ParseNullableLong(element.Element("MaxCapacityInBytes")),
                MaxPercentUtilizationOfFilesystem = ParseNullableDouble(element.Element("MaxPercentUtilizationOfFilesystem")),
                NodeId = ParseGuid(element.Element("NodeId")),
                Path = ParseNullableString(element.Element("Path"))
            };
        }

        public static CacheFilesystem ParseNullableCacheFilesystem(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseCacheFilesystem(element);
        }
        public static Pool ParsePool(XElement element)
        {
            return new Pool
            {
                AssignedToStorageDomain = ParseBool(element.Element("AssignedToStorageDomain")),
                AvailableCapacity = ParseLong(element.Element("AvailableCapacity")),
                BucketId = ParseNullableGuid(element.Element("BucketId")),
                Guid = ParseNullableString(element.Element("Guid")),
                Health = ParsePoolHealth(element.Element("Health")),
                Id = ParseGuid(element.Element("Id")),
                LastAccessed = ParseNullableDateTime(element.Element("LastAccessed")),
                LastModified = ParseNullableDateTime(element.Element("LastModified")),
                LastVerified = ParseNullableDateTime(element.Element("LastVerified")),
                Mountpoint = ParseNullableString(element.Element("Mountpoint")),
                Name = ParseNullableString(element.Element("Name")),
                PartitionId = ParseNullableGuid(element.Element("PartitionId")),
                PoweredOn = ParseBool(element.Element("PoweredOn")),
                Quiesced = ParseQuiesced(element.Element("Quiesced")),
                ReservedCapacity = ParseLong(element.Element("ReservedCapacity")),
                State = ParsePoolState(element.Element("State")),
                StorageDomainId = ParseNullableGuid(element.Element("StorageDomainId")),
                TotalCapacity = ParseLong(element.Element("TotalCapacity")),
                Type = ParsePoolType(element.Element("Type")),
                UsedCapacity = ParseLong(element.Element("UsedCapacity"))
            };
        }

        public static Pool ParseNullablePool(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParsePool(element);
        }
        public static PoolFailure ParsePoolFailure(XElement element)
        {
            return new PoolFailure
            {
                Date = ParseDateTime(element.Element("Date")),
                ErrorMessage = ParseNullableString(element.Element("ErrorMessage")),
                Id = ParseGuid(element.Element("Id")),
                PoolId = ParseGuid(element.Element("PoolId")),
                Type = ParsePoolFailureType(element.Element("Type"))
            };
        }

        public static PoolFailure ParseNullablePoolFailure(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParsePoolFailure(element);
        }
        public static PoolPartition ParsePoolPartition(XElement element)
        {
            return new PoolPartition
            {
                Id = ParseGuid(element.Element("Id")),
                Name = ParseNullableString(element.Element("Name")),
                Type = ParsePoolType(element.Element("Type"))
            };
        }

        public static PoolPartition ParseNullablePoolPartition(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParsePoolPartition(element);
        }
        public static Tape ParseTape(XElement element)
        {
            return new Tape
            {
                AssignedToStorageDomain = ParseBool(element.Element("AssignedToStorageDomain")),
                AvailableRawCapacity = ParseNullableLong(element.Element("AvailableRawCapacity")),
                BarCode = ParseNullableString(element.Element("BarCode")),
                BucketId = ParseNullableGuid(element.Element("BucketId")),
                DescriptionForIdentification = ParseNullableString(element.Element("DescriptionForIdentification")),
                EjectDate = ParseNullableDateTime(element.Element("EjectDate")),
                EjectLabel = ParseNullableString(element.Element("EjectLabel")),
                EjectLocation = ParseNullableString(element.Element("EjectLocation")),
                EjectPending = ParseNullableDateTime(element.Element("EjectPending")),
                FullOfData = ParseBool(element.Element("FullOfData")),
                Id = ParseGuid(element.Element("Id")),
                LastAccessed = ParseNullableDateTime(element.Element("LastAccessed")),
                LastCheckpoint = ParseNullableString(element.Element("LastCheckpoint")),
                LastModified = ParseNullableDateTime(element.Element("LastModified")),
                LastVerified = ParseNullableDateTime(element.Element("LastVerified")),
                PartitionId = ParseNullableGuid(element.Element("PartitionId")),
                PreviousState = ParseNullableTapeState(element.Element("PreviousState")),
                SerialNumber = ParseNullableString(element.Element("SerialNumber")),
                State = ParseTapeState(element.Element("State")),
                StorageDomainId = ParseNullableGuid(element.Element("StorageDomainId")),
                TakeOwnershipPending = ParseBool(element.Element("TakeOwnershipPending")),
                TotalRawCapacity = ParseNullableLong(element.Element("TotalRawCapacity")),
                Type = ParseTapeType(element.Element("Type")),
                VerifyPending = ParseNullablePriority(element.Element("VerifyPending")),
                WriteProtected = ParseBool(element.Element("WriteProtected"))
            };
        }

        public static Tape ParseNullableTape(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTape(element);
        }
        public static TapeDensityDirective ParseTapeDensityDirective(XElement element)
        {
            return new TapeDensityDirective
            {
                Density = ParseTapeDriveType(element.Element("Density")),
                Id = ParseGuid(element.Element("Id")),
                PartitionId = ParseGuid(element.Element("PartitionId")),
                TapeType = ParseTapeType(element.Element("TapeType"))
            };
        }

        public static TapeDensityDirective ParseNullableTapeDensityDirective(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapeDensityDirective(element);
        }
        public static TapeDrive ParseTapeDrive(XElement element)
        {
            return new TapeDrive
            {
                ErrorMessage = ParseNullableString(element.Element("ErrorMessage")),
                ForceTapeRemoval = ParseBool(element.Element("ForceTapeRemoval")),
                Id = ParseGuid(element.Element("Id")),
                LastCleaned = ParseNullableDateTime(element.Element("LastCleaned")),
                PartitionId = ParseGuid(element.Element("PartitionId")),
                SerialNumber = ParseNullableString(element.Element("SerialNumber")),
                State = ParseTapeDriveState(element.Element("State")),
                TapeId = ParseNullableGuid(element.Element("TapeId")),
                Type = ParseTapeDriveType(element.Element("Type"))
            };
        }

        public static TapeDrive ParseNullableTapeDrive(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapeDrive(element);
        }
        public static DetailedTapeFailure ParseDetailedTapeFailure(XElement element)
        {
            return new DetailedTapeFailure
            {
                Date = ParseDateTime(element.Element("Date")),
                ErrorMessage = ParseNullableString(element.Element("ErrorMessage")),
                Id = ParseGuid(element.Element("Id")),
                TapeDriveId = ParseGuid(element.Element("TapeDriveId")),
                TapeId = ParseGuid(element.Element("TapeId")),
                Type = ParseTapeFailureType(element.Element("Type"))
            };
        }

        public static DetailedTapeFailure ParseNullableDetailedTapeFailure(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDetailedTapeFailure(element);
        }
        public static TapeLibrary ParseTapeLibrary(XElement element)
        {
            return new TapeLibrary
            {
                Id = ParseGuid(element.Element("Id")),
                ManagementUrl = ParseNullableString(element.Element("ManagementUrl")),
                Name = ParseNullableString(element.Element("Name")),
                SerialNumber = ParseNullableString(element.Element("SerialNumber"))
            };
        }

        public static TapeLibrary ParseNullableTapeLibrary(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapeLibrary(element);
        }
        public static TapePartition ParseTapePartition(XElement element)
        {
            return new TapePartition
            {
                DriveType = ParseNullableTapeDriveType(element.Element("DriveType")),
                ErrorMessage = ParseNullableString(element.Element("ErrorMessage")),
                Id = ParseGuid(element.Element("Id")),
                ImportExportConfiguration = ParseImportExportConfiguration(element.Element("ImportExportConfiguration")),
                LibraryId = ParseGuid(element.Element("LibraryId")),
                Name = ParseNullableString(element.Element("Name")),
                Quiesced = ParseQuiesced(element.Element("Quiesced")),
                SerialNumber = ParseNullableString(element.Element("SerialNumber")),
                State = ParseTapePartitionState(element.Element("State"))
            };
        }

        public static TapePartition ParseNullableTapePartition(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapePartition(element);
        }
        public static TapePartitionFailure ParseTapePartitionFailure(XElement element)
        {
            return new TapePartitionFailure
            {
                Date = ParseDateTime(element.Element("Date")),
                ErrorMessage = ParseNullableString(element.Element("ErrorMessage")),
                Id = ParseGuid(element.Element("Id")),
                PartitionId = ParseGuid(element.Element("PartitionId")),
                Type = ParseTapePartitionFailureType(element.Element("Type"))
            };
        }

        public static TapePartitionFailure ParseNullableTapePartitionFailure(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapePartitionFailure(element);
        }
        public static BulkObject ParseBulkObject(XElement element)
        {
            return new BulkObject
            {
                Id = ParseGuid(element.Element("Id")),
                InCache = ParseNullableBool(element.AttributeText("InCache")),
                Latest = ParseBool(element.AttributeText("Latest")),
                Length = ParseLong(element.AttributeText("Length")),
                Name = ParseNullableString(element.AttributeText("Name")),
                Offset = ParseLong(element.AttributeText("Offset")),
                PhysicalPlacement = ParsePhysicalPlacement(element.Element("PhysicalPlacement")),
                Version = ParseLong(element.AttributeText("Version"))
            };
        }

        public static BulkObject ParseNullableBulkObject(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseBulkObject(element);
        }
        public static BulkObjectList ParseBulkObjectList(XElement element)
        {
            return new BulkObjectList
            {
                Objects = element.Elements("object").Select(ParseBulkObject).ToList()
            };
        }

        public static BulkObjectList ParseNullableBulkObjectList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseBulkObjectList(element);
        }
        public static BuildInformation ParseBuildInformation(XElement element)
        {
            return new BuildInformation
            {
                Branch = ParseNullableString(element.Element("Branch")),
                Revision = ParseNullableString(element.Element("Revision")),
                Version = ParseNullableString(element.Element("Version"))
            };
        }

        public static BuildInformation ParseNullableBuildInformation(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseBuildInformation(element);
        }
        public static BlobStoreTaskInformation ParseBlobStoreTaskInformation(XElement element)
        {
            return new BlobStoreTaskInformation
            {
                DateScheduled = ParseDateTime(element.Element("DateScheduled")),
                DateStarted = ParseNullableDateTime(element.Element("DateStarted")),
                Description = ParseNullableString(element.Element("Description")),
                DriveId = ParseNullableGuid(element.Element("DriveId")),
                DurationInProgress = ParseNullableDuration(element.Element("DurationInProgress")),
                DurationScheduled = ParseNullableDuration(element.Element("DurationScheduled")),
                Id = ParseLong(element.Element("Id")),
                Name = ParseNullableString(element.Element("Name")),
                PoolId = ParseNullableGuid(element.Element("PoolId")),
                Priority = ParsePriority(element.Element("Priority")),
                State = ParseBlobStoreTaskState(element.Element("State")),
                TapeId = ParseNullableGuid(element.Element("TapeId"))
            };
        }

        public static BlobStoreTaskInformation ParseNullableBlobStoreTaskInformation(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseBlobStoreTaskInformation(element);
        }
        public static BlobStoreTasksInformation ParseBlobStoreTasksInformation(XElement element)
        {
            return new BlobStoreTasksInformation
            {
                Tasks = element.Elements("Tasks").Select(ParseBlobStoreTaskInformation).ToList()
            };
        }

        public static BlobStoreTasksInformation ParseNullableBlobStoreTasksInformation(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseBlobStoreTasksInformation(element);
        }
        public static CacheEntryInformation ParseCacheEntryInformation(XElement element)
        {
            return new CacheEntryInformation
            {
                Blob = ParseNullableBlob(element.Element("Blob")),
                State = ParseCacheEntryState(element.Element("State"))
            };
        }

        public static CacheEntryInformation ParseNullableCacheEntryInformation(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseCacheEntryInformation(element);
        }
        public static CacheFilesystemInformation ParseCacheFilesystemInformation(XElement element)
        {
            return new CacheFilesystemInformation
            {
                AvailableCapacityInBytes = ParseLong(element.Element("AvailableCapacityInBytes")),
                CacheFilesystem = ParseCacheFilesystem(element.Element("CacheFilesystem")),
                Entries = element.Elements("Entries").Select(ParseCacheEntryInformation).ToList(),
                Summary = ParseNullableString(element.Element("Summary")),
                TotalCapacityInBytes = ParseLong(element.Element("TotalCapacityInBytes")),
                UnavailableCapacityInBytes = ParseLong(element.Element("UnavailableCapacityInBytes")),
                UsedCapacityInBytes = ParseLong(element.Element("UsedCapacityInBytes"))
            };
        }

        public static CacheFilesystemInformation ParseNullableCacheFilesystemInformation(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseCacheFilesystemInformation(element);
        }
        public static CacheInformation ParseCacheInformation(XElement element)
        {
            return new CacheInformation
            {
                Filesystems = element.Elements("Filesystems").Select(ParseCacheFilesystemInformation).ToList()
            };
        }

        public static CacheInformation ParseNullableCacheInformation(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseCacheInformation(element);
        }
        public static Ds3Bucket ParseDs3Bucket(XElement element)
        {
            return new Ds3Bucket
            {
                CreationDate = ParseDateTime(element.Element("CreationDate")),
                Name = ParseNullableString(element.Element("Name"))
            };
        }

        public static Ds3Bucket ParseNullableDs3Bucket(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDs3Bucket(element);
        }
        public static ListBucketResult ParseListBucketResult(XElement element)
        {
            return new ListBucketResult
            {
                CommonPrefixes = element.Element("CommonPrefixes").Elements("Prefix").Select(ParseString).ToList(),
                CreationDate = ParseDateTime(element.Element("CreationDate")),
                Delimiter = ParseNullableString(element.Element("Delimiter")),
                Marker = ParseNullableString(element.Element("Marker")),
                MaxKeys = ParseInt(element.Element("MaxKeys")),
                Name = ParseNullableString(element.Element("Name")),
                NextMarker = ParseNullableString(element.Element("NextMarker")),
                Objects = element.Elements("Contents").Select(ParseContents).ToList(),
                Prefix = ParseNullableString(element.Element("Prefix")),
                Truncated = ParseBool(element.Element("IsTruncated"))
            };
        }

        public static ListBucketResult ParseNullableListBucketResult(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseListBucketResult(element);
        }
        public static ListAllMyBucketsResult ParseListAllMyBucketsResult(XElement element)
        {
            return new ListAllMyBucketsResult
            {
                Buckets = element.Element("Buckets").Elements("Bucket").Select(ParseDs3Bucket).ToList(),
                Owner = ParseUser(element.Element("Owner"))
            };
        }

        public static ListAllMyBucketsResult ParseNullableListAllMyBucketsResult(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseListAllMyBucketsResult(element);
        }
        public static CompleteMultipartUploadResult ParseCompleteMultipartUploadResult(XElement element)
        {
            return new CompleteMultipartUploadResult
            {
                Bucket = ParseNullableString(element.Element("Bucket")),
                ETag = ParseNullableString(element.Element("ETag")),
                Key = ParseNullableString(element.Element("Key")),
                Location = ParseNullableString(element.Element("Location"))
            };
        }

        public static CompleteMultipartUploadResult ParseNullableCompleteMultipartUploadResult(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseCompleteMultipartUploadResult(element);
        }
        public static DeleteObjectError ParseDeleteObjectError(XElement element)
        {
            return new DeleteObjectError
            {
                Code = ParseNullableString(element.Element("Code")),
                Key = ParseNullableString(element.Element("Key")),
                Message = ParseNullableString(element.Element("Message"))
            };
        }

        public static DeleteObjectError ParseNullableDeleteObjectError(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDeleteObjectError(element);
        }
        public static DeleteResult ParseDeleteResult(XElement element)
        {
            return new DeleteResult
            {
                DeletedObjects = element.Elements("Deleted").Select(ParseS3ObjectToDelete).ToList(),
                Errors = element.Elements("Error").Select(ParseDeleteObjectError).ToList()
            };
        }

        public static DeleteResult ParseNullableDeleteResult(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDeleteResult(element);
        }
        public static DetailedTape ParseDetailedTape(XElement element)
        {
            return new DetailedTape
            {
                AssignedToStorageDomain = ParseBool(element.Element("AssignedToStorageDomain")),
                AvailableRawCapacity = ParseNullableLong(element.Element("AvailableRawCapacity")),
                BarCode = ParseNullableString(element.Element("BarCode")),
                BucketId = ParseNullableGuid(element.Element("BucketId")),
                DescriptionForIdentification = ParseNullableString(element.Element("DescriptionForIdentification")),
                EjectDate = ParseNullableDateTime(element.Element("EjectDate")),
                EjectLabel = ParseNullableString(element.Element("EjectLabel")),
                EjectLocation = ParseNullableString(element.Element("EjectLocation")),
                EjectPending = ParseNullableDateTime(element.Element("EjectPending")),
                FullOfData = ParseBool(element.Element("FullOfData")),
                Id = ParseGuid(element.Element("Id")),
                LastAccessed = ParseNullableDateTime(element.Element("LastAccessed")),
                LastCheckpoint = ParseNullableString(element.Element("LastCheckpoint")),
                LastModified = ParseNullableDateTime(element.Element("LastModified")),
                LastVerified = ParseNullableDateTime(element.Element("LastVerified")),
                MostRecentFailure = ParseDetailedTapeFailure(element.Element("MostRecentFailure")),
                PartitionId = ParseNullableGuid(element.Element("PartitionId")),
                PreviousState = ParseNullableTapeState(element.Element("PreviousState")),
                SerialNumber = ParseNullableString(element.Element("SerialNumber")),
                State = ParseTapeState(element.Element("State")),
                StorageDomainId = ParseNullableGuid(element.Element("StorageDomainId")),
                TakeOwnershipPending = ParseBool(element.Element("TakeOwnershipPending")),
                TotalRawCapacity = ParseNullableLong(element.Element("TotalRawCapacity")),
                Type = ParseTapeType(element.Element("Type")),
                VerifyPending = ParseNullablePriority(element.Element("VerifyPending")),
                WriteProtected = ParseBool(element.Element("WriteProtected"))
            };
        }

        public static DetailedTape ParseNullableDetailedTape(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDetailedTape(element);
        }
        public static DetailedTapePartition ParseDetailedTapePartition(XElement element)
        {
            return new DetailedTapePartition
            {
                DriveType = ParseNullableTapeDriveType(element.Element("DriveType")),
                DriveTypes = element.Elements("DriveTypes").Select(ParseTapeDriveType).ToList(),
                ErrorMessage = ParseNullableString(element.Element("ErrorMessage")),
                Id = ParseGuid(element.Element("Id")),
                ImportExportConfiguration = ParseImportExportConfiguration(element.Element("ImportExportConfiguration")),
                LibraryId = ParseGuid(element.Element("LibraryId")),
                Name = ParseNullableString(element.Element("Name")),
                Quiesced = ParseQuiesced(element.Element("Quiesced")),
                SerialNumber = ParseNullableString(element.Element("SerialNumber")),
                State = ParseTapePartitionState(element.Element("State")),
                TapeTypes = element.Elements("TapeTypes").Select(ParseTapeType).ToList()
            };
        }

        public static DetailedTapePartition ParseNullableDetailedTapePartition(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDetailedTapePartition(element);
        }
        public static Error ParseError(XElement element)
        {
            return new Error
            {
                Code = ParseNullableString(element.Element("Code")),
                HttpErrorCode = ParseInt(element.Element("HttpErrorCode")),
                Message = ParseNullableString(element.Element("Message")),
                Resource = ParseNullableString(element.Element("Resource")),
                ResourceId = ParseLong(element.Element("ResourceId"))
            };
        }

        public static Error ParseNullableError(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseError(element);
        }
        public static InitiateMultipartUploadResult ParseInitiateMultipartUploadResult(XElement element)
        {
            return new InitiateMultipartUploadResult
            {
                Bucket = ParseNullableString(element.Element("Bucket")),
                Key = ParseNullableString(element.Element("Key")),
                UploadId = ParseNullableString(element.Element("UploadId"))
            };
        }

        public static InitiateMultipartUploadResult ParseNullableInitiateMultipartUploadResult(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseInitiateMultipartUploadResult(element);
        }
        public static Job ParseJob(XElement element)
        {
            return new Job
            {
                Aggregating = ParseBool(element.AttributeText("Aggregating")),
                BucketName = ParseNullableString(element.AttributeText("BucketName")),
                CachedSizeInBytes = ParseLong(element.AttributeText("CachedSizeInBytes")),
                ChunkClientProcessingOrderGuarantee = ParseJobChunkClientProcessingOrderGuarantee(element.AttributeText("ChunkClientProcessingOrderGuarantee")),
                CompletedSizeInBytes = ParseLong(element.AttributeText("CompletedSizeInBytes")),
                JobId = ParseGuid(element.AttributeText("JobId")),
                Naked = ParseBool(element.AttributeText("Naked")),
                Name = ParseNullableString(element.AttributeText("Name")),
                Nodes = element.Element("Nodes").Elements("Node").Select(ParseDs3Node).ToList(),
                OriginalSizeInBytes = ParseLong(element.AttributeText("OriginalSizeInBytes")),
                Priority = ParsePriority(element.AttributeText("Priority")),
                RequestType = ParseJobRequestType(element.AttributeText("RequestType")),
                StartDate = ParseDateTime(element.AttributeText("StartDate")),
                Status = ParseJobStatus(element.AttributeText("Status")),
                UserId = ParseGuid(element.AttributeText("UserId")),
                UserName = ParseNullableString(element.AttributeText("UserName")),
                WriteOptimization = ParseWriteOptimization(element.AttributeText("WriteOptimization"))
            };
        }

        public static Job ParseNullableJob(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseJob(element);
        }
        public static Objects ParseObjects(XElement element)
        {
            return new Objects
            {
                ChunkId = ParseGuid(element.AttributeText("ChunkId")),
                ChunkNumber = ParseInt(element.AttributeText("ChunkNumber")),
                NodeId = ParseNullableGuid(element.AttributeText("NodeId")),
                ObjectsList = element.Elements("object").Select(ParseBulkObject).ToList()
            };
        }

        public static Objects ParseNullableObjects(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseObjects(element);
        }
        public static MasterObjectList ParseMasterObjectList(XElement element)
        {
            return new MasterObjectList
            {
                Aggregating = ParseBool(element.AttributeText("Aggregating")),
                BucketName = ParseNullableString(element.AttributeText("BucketName")),
                CachedSizeInBytes = ParseLong(element.AttributeText("CachedSizeInBytes")),
                ChunkClientProcessingOrderGuarantee = ParseJobChunkClientProcessingOrderGuarantee(element.AttributeText("ChunkClientProcessingOrderGuarantee")),
                CompletedSizeInBytes = ParseLong(element.AttributeText("CompletedSizeInBytes")),
                JobId = ParseGuid(element.AttributeText("JobId")),
                Naked = ParseBool(element.AttributeText("Naked")),
                Name = ParseNullableString(element.AttributeText("Name")),
                Nodes = element.Element("Nodes").Elements("Node").Select(ParseDs3Node).ToList(),
                Objects = element.Elements("Objects").Select(ParseObjects).ToList(),
                OriginalSizeInBytes = ParseLong(element.AttributeText("OriginalSizeInBytes")),
                Priority = ParsePriority(element.AttributeText("Priority")),
                RequestType = ParseJobRequestType(element.AttributeText("RequestType")),
                StartDate = ParseDateTime(element.AttributeText("StartDate")),
                Status = ParseJobStatus(element.AttributeText("Status")),
                UserId = ParseGuid(element.AttributeText("UserId")),
                UserName = ParseNullableString(element.AttributeText("UserName")),
                WriteOptimization = ParseWriteOptimization(element.AttributeText("WriteOptimization"))
            };
        }

        public static MasterObjectList ParseNullableMasterObjectList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseMasterObjectList(element);
        }
        public static JobList ParseJobList(XElement element)
        {
            return new JobList
            {
                Jobs = element.Element("Jobs").Elements("Job").Select(ParseJob).ToList()
            };
        }

        public static JobList ParseNullableJobList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseJobList(element);
        }
        public static ListPartsResult ParseListPartsResult(XElement element)
        {
            return new ListPartsResult
            {
                Bucket = ParseNullableString(element.Element("Bucket")),
                Key = ParseNullableString(element.Element("Key")),
                MaxParts = ParseInt(element.Element("MaxParts")),
                NextPartNumberMarker = ParseInt(element.Element("NextPartNumberMarker")),
                Owner = ParseUser(element.Element("Owner")),
                PartNumberMarker = ParseNullableInt(element.Element("PartNumberMarker")),
                Parts = element.Elements("Part").Select(ParseMultiPartUploadPart).ToList(),
                Truncated = ParseBool(element.Element("IsTruncated")),
                UploadId = ParseGuid(element.Element("UploadId"))
            };
        }

        public static ListPartsResult ParseNullableListPartsResult(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseListPartsResult(element);
        }
        public static ListMultiPartUploadsResult ParseListMultiPartUploadsResult(XElement element)
        {
            return new ListMultiPartUploadsResult
            {
                Bucket = ParseNullableString(element.Element("Bucket")),
                CommonPrefixes = element.Element("CommonPrefixes").Elements("Prefix").Select(ParseString).ToList(),
                Delimiter = ParseNullableString(element.Element("Delimiter")),
                KeyMarker = ParseNullableString(element.Element("KeyMarker")),
                MaxUploads = ParseInt(element.Element("MaxUploads")),
                NextKeyMarker = ParseNullableString(element.Element("NextKeyMarker")),
                NextUploadIdMarker = ParseNullableString(element.Element("NextUploadIdMarker")),
                Prefix = ParseNullableString(element.Element("Prefix")),
                Truncated = ParseBool(element.Element("IsTruncated")),
                UploadIdMarker = ParseNullableString(element.Element("UploadIdMarker")),
                Uploads = element.Elements("Upload").Select(ParseMultiPartUpload).ToList()
            };
        }

        public static ListMultiPartUploadsResult ParseNullableListMultiPartUploadsResult(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseListMultiPartUploadsResult(element);
        }
        public static MultiPartUpload ParseMultiPartUpload(XElement element)
        {
            return new MultiPartUpload
            {
                Initiated = ParseDateTime(element.Element("Initiated")),
                Key = ParseNullableString(element.Element("Key")),
                Owner = ParseUser(element.Element("Owner")),
                UploadId = ParseGuid(element.Element("UploadId"))
            };
        }

        public static MultiPartUpload ParseNullableMultiPartUpload(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseMultiPartUpload(element);
        }
        public static MultiPartUploadPart ParseMultiPartUploadPart(XElement element)
        {
            return new MultiPartUploadPart
            {
                ETag = ParseNullableString(element.Element("ETag")),
                LastModified = ParseDateTime(element.Element("LastModified")),
                PartNumber = ParseInt(element.Element("PartNumber"))
            };
        }

        public static MultiPartUploadPart ParseNullableMultiPartUploadPart(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseMultiPartUploadPart(element);
        }
        public static Ds3Node ParseDs3Node(XElement element)
        {
            return new Ds3Node
            {
                EndPoint = ParseNullableString(element.AttributeText("EndPoint")),
                HttpPort = ParseNullableInt(element.AttributeText("HttpPort")),
                HttpsPort = ParseNullableInt(element.AttributeText("HttpsPort")),
                Id = ParseGuid(element.AttributeText("Id"))
            };
        }

        public static Ds3Node ParseNullableDs3Node(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDs3Node(element);
        }
        public static Contents ParseContents(XElement element)
        {
            return new Contents
            {
                ETag = ParseNullableString(element.Element("ETag")),
                Key = ParseNullableString(element.Element("Key")),
                LastModified = ParseDateTime(element.Element("LastModified")),
                Owner = ParseUser(element.Element("Owner")),
                Size = ParseLong(element.Element("Size")),
                StorageClass = ParseString(element.Element("StorageClass"))
            };
        }

        public static Contents ParseNullableContents(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseContents(element);
        }
        public static S3ObjectToDelete ParseS3ObjectToDelete(XElement element)
        {
            return new S3ObjectToDelete
            {
                Key = ParseNullableString(element.Element("Key"))
            };
        }

        public static S3ObjectToDelete ParseNullableS3ObjectToDelete(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseS3ObjectToDelete(element);
        }
        public static User ParseUser(XElement element)
        {
            return new User
            {
                DisplayName = ParseNullableString(element.Element("DisplayName")),
                Id = ParseGuid(element.Element("iD"))
            };
        }

        public static User ParseNullableUser(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseUser(element);
        }
        public static DetailedS3Object ParseDetailedS3Object(XElement element)
        {
            return new DetailedS3Object
            {
                Blobs = ParseBulkObjectList(element.Element("Blobs")),
                BlobsBeingPersisted = ParseNullableInt(element.Element("BlobsBeingPersisted")),
                BlobsDegraded = ParseNullableInt(element.Element("BlobsDegraded")),
                BlobsInCache = ParseNullableInt(element.Element("BlobsInCache")),
                BlobsTotal = ParseNullableInt(element.Element("BlobsTotal")),
                BucketId = ParseGuid(element.Element("BucketId")),
                CreationDate = ParseNullableDateTime(element.Element("CreationDate")),
                ETag = ParseNullableString(element.Element("ETag")),
                Id = ParseGuid(element.Element("Id")),
                Latest = ParseBool(element.Element("Latest")),
                Name = ParseNullableString(element.Element("Name")),
                Owner = ParseNullableString(element.Element("Owner")),
                Size = ParseLong(element.Element("Size")),
                Type = ParseS3ObjectType(element.Element("Type")),
                Version = ParseLong(element.Element("Version"))
            };
        }

        public static DetailedS3Object ParseNullableDetailedS3Object(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDetailedS3Object(element);
        }
        public static SystemInformation ParseSystemInformation(XElement element)
        {
            return new SystemInformation
            {
                ApiVersion = ParseNullableString(element.Element("ApiVersion")),
                BackendActivated = ParseBool(element.Element("BackendActivated")),
                BuildInformation = ParseBuildInformation(element.Element("BuildInformation")),
                SerialNumber = ParseNullableString(element.Element("SerialNumber"))
            };
        }

        public static SystemInformation ParseNullableSystemInformation(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseSystemInformation(element);
        }
        public static HealthVerificationResult ParseHealthVerificationResult(XElement element)
        {
            return new HealthVerificationResult
            {
                DatabaseFilesystemFreeSpace = ParseDatabasePhysicalSpaceState(element.Element("DatabaseFilesystemFreeSpace")),
                MsRequiredToVerifyDataPlannerHealth = ParseLong(element.Element("MsRequiredToVerifyDataPlannerHealth"))
            };
        }

        public static HealthVerificationResult ParseNullableHealthVerificationResult(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseHealthVerificationResult(element);
        }
        public static NamedDetailedTapePartition ParseNamedDetailedTapePartition(XElement element)
        {
            return new NamedDetailedTapePartition
            {
                DriveType = ParseNullableTapeDriveType(element.Element("DriveType")),
                DriveTypes = element.Elements("DriveTypes").Select(ParseTapeDriveType).ToList(),
                ErrorMessage = ParseNullableString(element.Element("ErrorMessage")),
                Id = ParseGuid(element.Element("Id")),
                ImportExportConfiguration = ParseImportExportConfiguration(element.Element("ImportExportConfiguration")),
                LibraryId = ParseGuid(element.Element("LibraryId")),
                Name = ParseNullableString(element.Element("Name")),
                Quiesced = ParseQuiesced(element.Element("Quiesced")),
                SerialNumber = ParseNullableString(element.Element("SerialNumber")),
                State = ParseTapePartitionState(element.Element("State")),
                TapeTypes = element.Elements("TapeTypes").Select(ParseTapeType).ToList()
            };
        }

        public static NamedDetailedTapePartition ParseNullableNamedDetailedTapePartition(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseNamedDetailedTapePartition(element);
        }
        public static NamedDetailedTape ParseNamedDetailedTape(XElement element)
        {
            return new NamedDetailedTape
            {
                AssignedToStorageDomain = ParseBool(element.Element("AssignedToStorageDomain")),
                AvailableRawCapacity = ParseNullableLong(element.Element("AvailableRawCapacity")),
                BarCode = ParseNullableString(element.Element("BarCode")),
                BucketId = ParseNullableGuid(element.Element("BucketId")),
                DescriptionForIdentification = ParseNullableString(element.Element("DescriptionForIdentification")),
                EjectDate = ParseNullableDateTime(element.Element("EjectDate")),
                EjectLabel = ParseNullableString(element.Element("EjectLabel")),
                EjectLocation = ParseNullableString(element.Element("EjectLocation")),
                EjectPending = ParseNullableDateTime(element.Element("EjectPending")),
                FullOfData = ParseBool(element.Element("FullOfData")),
                Id = ParseGuid(element.Element("Id")),
                LastAccessed = ParseNullableDateTime(element.Element("LastAccessed")),
                LastCheckpoint = ParseNullableString(element.Element("LastCheckpoint")),
                LastModified = ParseNullableDateTime(element.Element("LastModified")),
                LastVerified = ParseNullableDateTime(element.Element("LastVerified")),
                MostRecentFailure = ParseDetailedTapeFailure(element.Element("MostRecentFailure")),
                PartitionId = ParseNullableGuid(element.Element("PartitionId")),
                PreviousState = ParseNullableTapeState(element.Element("PreviousState")),
                SerialNumber = ParseNullableString(element.Element("SerialNumber")),
                State = ParseTapeState(element.Element("State")),
                StorageDomainId = ParseNullableGuid(element.Element("StorageDomainId")),
                TakeOwnershipPending = ParseBool(element.Element("TakeOwnershipPending")),
                TotalRawCapacity = ParseNullableLong(element.Element("TotalRawCapacity")),
                Type = ParseTapeType(element.Element("Type")),
                VerifyPending = ParseNullablePriority(element.Element("VerifyPending")),
                WriteProtected = ParseBool(element.Element("WriteProtected"))
            };
        }

        public static NamedDetailedTape ParseNullableNamedDetailedTape(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseNamedDetailedTape(element);
        }
        public static TapeFailure ParseTapeFailure(XElement element)
        {
            return new TapeFailure
            {
                Cause = ParseNullableString(element.Element("Cause")),
                Tape = ParseTape(element.Element("Tape"))
            };
        }

        public static TapeFailure ParseNullableTapeFailure(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapeFailure(element);
        }
        public static TapeFailureList ParseTapeFailureList(XElement element)
        {
            return new TapeFailureList
            {
                Failures = element.Elements("failure").Select(ParseTapeFailure).ToList()
            };
        }

        public static TapeFailureList ParseNullableTapeFailureList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapeFailureList(element);
        }
        public static Duration ParseDuration(XElement element)
        {
            return new Duration
            {
                ElapsedMillis = ParseLong(element.Element("ElapsedMillis")),
                ElapsedMinutes = ParseInt(element.Element("ElapsedMinutes")),
                ElapsedNanos = ParseLong(element.Element("ElapsedNanos")),
                ElapsedSeconds = ParseInt(element.Element("ElapsedSeconds"))
            };
        }

        public static Duration ParseNullableDuration(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDuration(element);
        }
        public static BucketAclList ParseBucketAclList(XElement element)
        {
            return new BucketAclList
            {
                BucketAcls = element.Elements("BucketAcl").Select(ParseBucketAcl).ToList()
            };
        }

        public static BucketAclList ParseNullableBucketAclList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseBucketAclList(element);
        }
        public static DataPolicyAclList ParseDataPolicyAclList(XElement element)
        {
            return new DataPolicyAclList
            {
                DataPolicyAcls = element.Elements("DataPolicyAcl").Select(ParseDataPolicyAcl).ToList()
            };
        }

        public static DataPolicyAclList ParseNullableDataPolicyAclList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDataPolicyAclList(element);
        }
        public static BucketList ParseBucketList(XElement element)
        {
            return new BucketList
            {
                Buckets = element.Elements("Bucket").Select(ParseBucket).ToList()
            };
        }

        public static BucketList ParseNullableBucketList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseBucketList(element);
        }
        public static CacheFilesystemList ParseCacheFilesystemList(XElement element)
        {
            return new CacheFilesystemList
            {
                CacheFilesystems = element.Elements("CacheFilesystem").Select(ParseCacheFilesystem).ToList()
            };
        }

        public static CacheFilesystemList ParseNullableCacheFilesystemList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseCacheFilesystemList(element);
        }
        public static DataPersistenceRuleList ParseDataPersistenceRuleList(XElement element)
        {
            return new DataPersistenceRuleList
            {
                DataPersistenceRules = element.Elements("DataPersistenceRule").Select(ParseDataPersistenceRule).ToList()
            };
        }

        public static DataPersistenceRuleList ParseNullableDataPersistenceRuleList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDataPersistenceRuleList(element);
        }
        public static DataPolicyList ParseDataPolicyList(XElement element)
        {
            return new DataPolicyList
            {
                DataPolicies = element.Elements("DataPolicy").Select(ParseDataPolicy).ToList()
            };
        }

        public static DataPolicyList ParseNullableDataPolicyList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDataPolicyList(element);
        }
        public static GroupMemberList ParseGroupMemberList(XElement element)
        {
            return new GroupMemberList
            {
                GroupMembers = element.Elements("GroupMember").Select(ParseGroupMember).ToList()
            };
        }

        public static GroupMemberList ParseNullableGroupMemberList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseGroupMemberList(element);
        }
        public static GroupList ParseGroupList(XElement element)
        {
            return new GroupList
            {
                Groups = element.Elements("Group").Select(ParseGroup).ToList()
            };
        }

        public static GroupList ParseNullableGroupList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseGroupList(element);
        }
        public static ActiveJobList ParseActiveJobList(XElement element)
        {
            return new ActiveJobList
            {
                ActiveJobs = element.Elements("Job").Select(ParseActiveJob).ToList()
            };
        }

        public static ActiveJobList ParseNullableActiveJobList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseActiveJobList(element);
        }
        public static CanceledJobList ParseCanceledJobList(XElement element)
        {
            return new CanceledJobList
            {
                CanceledJobs = element.Elements("CanceledJob").Select(ParseCanceledJob).ToList()
            };
        }

        public static CanceledJobList ParseNullableCanceledJobList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseCanceledJobList(element);
        }
        public static CompletedJobList ParseCompletedJobList(XElement element)
        {
            return new CompletedJobList
            {
                CompletedJobs = element.Elements("CompletedJob").Select(ParseCompletedJob).ToList()
            };
        }

        public static CompletedJobList ParseNullableCompletedJobList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseCompletedJobList(element);
        }
        public static NodeList ParseNodeList(XElement element)
        {
            return new NodeList
            {
                Nodes = element.Elements("Node").Select(ParseNode).ToList()
            };
        }

        public static NodeList ParseNullableNodeList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseNodeList(element);
        }
        public static JobCompletedNotificationRegistrationList ParseJobCompletedNotificationRegistrationList(XElement element)
        {
            return new JobCompletedNotificationRegistrationList
            {
                JobCompletedNotificationRegistrations = element.Elements("JobCompletedNotificationRegistration").Select(ParseJobCompletedNotificationRegistration).ToList()
            };
        }

        public static JobCompletedNotificationRegistrationList ParseNullableJobCompletedNotificationRegistrationList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseJobCompletedNotificationRegistrationList(element);
        }
        public static JobCreatedNotificationRegistrationList ParseJobCreatedNotificationRegistrationList(XElement element)
        {
            return new JobCreatedNotificationRegistrationList
            {
                JobCreatedNotificationRegistrations = element.Elements("JobCreatedNotificationRegistration").Select(ParseJobCreatedNotificationRegistration).ToList()
            };
        }

        public static JobCreatedNotificationRegistrationList ParseNullableJobCreatedNotificationRegistrationList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseJobCreatedNotificationRegistrationList(element);
        }
        public static S3ObjectCachedNotificationRegistrationList ParseS3ObjectCachedNotificationRegistrationList(XElement element)
        {
            return new S3ObjectCachedNotificationRegistrationList
            {
                S3ObjectCachedNotificationRegistrations = element.Elements("S3ObjectCachedNotificationRegistration").Select(ParseS3ObjectCachedNotificationRegistration).ToList()
            };
        }

        public static S3ObjectCachedNotificationRegistrationList ParseNullableS3ObjectCachedNotificationRegistrationList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseS3ObjectCachedNotificationRegistrationList(element);
        }
        public static S3ObjectLostNotificationRegistrationList ParseS3ObjectLostNotificationRegistrationList(XElement element)
        {
            return new S3ObjectLostNotificationRegistrationList
            {
                S3ObjectLostNotificationRegistrations = element.Elements("S3ObjectLostNotificationRegistration").Select(ParseS3ObjectLostNotificationRegistration).ToList()
            };
        }

        public static S3ObjectLostNotificationRegistrationList ParseNullableS3ObjectLostNotificationRegistrationList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseS3ObjectLostNotificationRegistrationList(element);
        }
        public static S3ObjectPersistedNotificationRegistrationList ParseS3ObjectPersistedNotificationRegistrationList(XElement element)
        {
            return new S3ObjectPersistedNotificationRegistrationList
            {
                S3ObjectPersistedNotificationRegistrations = element.Elements("S3ObjectPersistedNotificationRegistration").Select(ParseS3ObjectPersistedNotificationRegistration).ToList()
            };
        }

        public static S3ObjectPersistedNotificationRegistrationList ParseNullableS3ObjectPersistedNotificationRegistrationList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseS3ObjectPersistedNotificationRegistrationList(element);
        }
        public static PoolFailureNotificationRegistrationList ParsePoolFailureNotificationRegistrationList(XElement element)
        {
            return new PoolFailureNotificationRegistrationList
            {
                PoolFailureNotificationRegistrations = element.Elements("PoolFailureNotificationRegistration").Select(ParsePoolFailureNotificationRegistration).ToList()
            };
        }

        public static PoolFailureNotificationRegistrationList ParseNullablePoolFailureNotificationRegistrationList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParsePoolFailureNotificationRegistrationList(element);
        }
        public static StorageDomainFailureNotificationRegistrationList ParseStorageDomainFailureNotificationRegistrationList(XElement element)
        {
            return new StorageDomainFailureNotificationRegistrationList
            {
                StorageDomainFailureNotificationRegistrations = element.Elements("StorageDomainFailureNotificationRegistration").Select(ParseStorageDomainFailureNotificationRegistration).ToList()
            };
        }

        public static StorageDomainFailureNotificationRegistrationList ParseNullableStorageDomainFailureNotificationRegistrationList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseStorageDomainFailureNotificationRegistrationList(element);
        }
        public static SystemFailureNotificationRegistrationList ParseSystemFailureNotificationRegistrationList(XElement element)
        {
            return new SystemFailureNotificationRegistrationList
            {
                SystemFailureNotificationRegistrations = element.Elements("SystemFailureNotificationRegistration").Select(ParseSystemFailureNotificationRegistration).ToList()
            };
        }

        public static SystemFailureNotificationRegistrationList ParseNullableSystemFailureNotificationRegistrationList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseSystemFailureNotificationRegistrationList(element);
        }
        public static TapeFailureNotificationRegistrationList ParseTapeFailureNotificationRegistrationList(XElement element)
        {
            return new TapeFailureNotificationRegistrationList
            {
                TapeFailureNotificationRegistrations = element.Elements("TapeFailureNotificationRegistration").Select(ParseTapeFailureNotificationRegistration).ToList()
            };
        }

        public static TapeFailureNotificationRegistrationList ParseNullableTapeFailureNotificationRegistrationList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapeFailureNotificationRegistrationList(element);
        }
        public static TapePartitionFailureNotificationRegistrationList ParseTapePartitionFailureNotificationRegistrationList(XElement element)
        {
            return new TapePartitionFailureNotificationRegistrationList
            {
                TapePartitionFailureNotificationRegistrations = element.Elements("TapePartitionFailureNotificationRegistration").Select(ParseTapePartitionFailureNotificationRegistration).ToList()
            };
        }

        public static TapePartitionFailureNotificationRegistrationList ParseNullableTapePartitionFailureNotificationRegistrationList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapePartitionFailureNotificationRegistrationList(element);
        }
        public static S3ObjectList ParseS3ObjectList(XElement element)
        {
            return new S3ObjectList
            {
                S3Objects = element.Elements("S3Object").Select(ParseS3Object).ToList()
            };
        }

        public static S3ObjectList ParseNullableS3ObjectList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseS3ObjectList(element);
        }
        public static DetailedS3ObjectList ParseDetailedS3ObjectList(XElement element)
        {
            return new DetailedS3ObjectList
            {
                DetailedS3Objects = element.Elements("DetailedS3Object").Select(ParseDetailedS3Object).ToList()
            };
        }

        public static DetailedS3ObjectList ParseNullableDetailedS3ObjectList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDetailedS3ObjectList(element);
        }
        public static PoolFailureList ParsePoolFailureList(XElement element)
        {
            return new PoolFailureList
            {
                PoolFailures = element.Elements("PoolFailure").Select(ParsePoolFailure).ToList()
            };
        }

        public static PoolFailureList ParseNullablePoolFailureList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParsePoolFailureList(element);
        }
        public static PoolPartitionList ParsePoolPartitionList(XElement element)
        {
            return new PoolPartitionList
            {
                PoolPartitions = element.Elements("PoolPartition").Select(ParsePoolPartition).ToList()
            };
        }

        public static PoolPartitionList ParseNullablePoolPartitionList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParsePoolPartitionList(element);
        }
        public static PoolList ParsePoolList(XElement element)
        {
            return new PoolList
            {
                Pools = element.Elements("Pool").Select(ParsePool).ToList()
            };
        }

        public static PoolList ParseNullablePoolList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParsePoolList(element);
        }
        public static StorageDomainFailureList ParseStorageDomainFailureList(XElement element)
        {
            return new StorageDomainFailureList
            {
                StorageDomainFailures = element.Elements("StorageDomainFailure").Select(ParseStorageDomainFailure).ToList()
            };
        }

        public static StorageDomainFailureList ParseNullableStorageDomainFailureList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseStorageDomainFailureList(element);
        }
        public static StorageDomainMemberList ParseStorageDomainMemberList(XElement element)
        {
            return new StorageDomainMemberList
            {
                StorageDomainMembers = element.Elements("StorageDomainMember").Select(ParseStorageDomainMember).ToList()
            };
        }

        public static StorageDomainMemberList ParseNullableStorageDomainMemberList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseStorageDomainMemberList(element);
        }
        public static StorageDomainList ParseStorageDomainList(XElement element)
        {
            return new StorageDomainList
            {
                StorageDomains = element.Elements("StorageDomain").Select(ParseStorageDomain).ToList()
            };
        }

        public static StorageDomainList ParseNullableStorageDomainList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseStorageDomainList(element);
        }
        public static SystemFailureList ParseSystemFailureList(XElement element)
        {
            return new SystemFailureList
            {
                SystemFailures = element.Elements("SystemFailure").Select(ParseSystemFailure).ToList()
            };
        }

        public static SystemFailureList ParseNullableSystemFailureList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseSystemFailureList(element);
        }
        public static TapeDensityDirectiveList ParseTapeDensityDirectiveList(XElement element)
        {
            return new TapeDensityDirectiveList
            {
                TapeDensityDirectives = element.Elements("TapeDensityDirective").Select(ParseTapeDensityDirective).ToList()
            };
        }

        public static TapeDensityDirectiveList ParseNullableTapeDensityDirectiveList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapeDensityDirectiveList(element);
        }
        public static TapeDriveList ParseTapeDriveList(XElement element)
        {
            return new TapeDriveList
            {
                TapeDrives = element.Elements("TapeDrive").Select(ParseTapeDrive).ToList()
            };
        }

        public static TapeDriveList ParseNullableTapeDriveList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapeDriveList(element);
        }
        public static DetailedTapeFailureList ParseDetailedTapeFailureList(XElement element)
        {
            return new DetailedTapeFailureList
            {
                DetailedTapeFailures = element.Elements("TapeFailure").Select(ParseDetailedTapeFailure).ToList()
            };
        }

        public static DetailedTapeFailureList ParseNullableDetailedTapeFailureList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDetailedTapeFailureList(element);
        }
        public static TapeLibraryList ParseTapeLibraryList(XElement element)
        {
            return new TapeLibraryList
            {
                TapeLibraries = element.Elements("TapeLibrary").Select(ParseTapeLibrary).ToList()
            };
        }

        public static TapeLibraryList ParseNullableTapeLibraryList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapeLibraryList(element);
        }
        public static TapePartitionFailureList ParseTapePartitionFailureList(XElement element)
        {
            return new TapePartitionFailureList
            {
                TapePartitionFailures = element.Elements("TapePartitionFailure").Select(ParseTapePartitionFailure).ToList()
            };
        }

        public static TapePartitionFailureList ParseNullableTapePartitionFailureList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapePartitionFailureList(element);
        }
        public static TapePartitionList ParseTapePartitionList(XElement element)
        {
            return new TapePartitionList
            {
                TapePartitions = element.Elements("TapePartition").Select(ParseTapePartition).ToList()
            };
        }

        public static TapePartitionList ParseNullableTapePartitionList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapePartitionList(element);
        }
        public static NamedDetailedTapePartitionList ParseNamedDetailedTapePartitionList(XElement element)
        {
            return new NamedDetailedTapePartitionList
            {
                NamedDetailedTapePartitions = element.Elements("NamedDetailedTapePartition").Select(ParseNamedDetailedTapePartition).ToList()
            };
        }

        public static NamedDetailedTapePartitionList ParseNullableNamedDetailedTapePartitionList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseNamedDetailedTapePartitionList(element);
        }
        public static TapeList ParseTapeList(XElement element)
        {
            return new TapeList
            {
                Tapes = element.Elements("Tape").Select(ParseTape).ToList()
            };
        }

        public static TapeList ParseNullableTapeList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapeList(element);
        }
        public static NamedDetailedTapeList ParseNamedDetailedTapeList(XElement element)
        {
            return new NamedDetailedTapeList
            {
                NamedDetailedTapes = element.Elements("Tape").Select(ParseNamedDetailedTape).ToList()
            };
        }

        public static NamedDetailedTapeList ParseNullableNamedDetailedTapeList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseNamedDetailedTapeList(element);
        }
        public static SpectraUserList ParseSpectraUserList(XElement element)
        {
            return new SpectraUserList
            {
                SpectraUsers = element.Elements("User").Select(ParseSpectraUser).ToList()
            };
        }

        public static SpectraUserList ParseNullableSpectraUserList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseSpectraUserList(element);
        }

        public static AutoInspectMode? ParseNullableAutoInspectMode(string autoInspectModeOrNull)
        {
            return string.IsNullOrWhiteSpace(autoInspectModeOrNull)
                ? (AutoInspectMode?) null
                : ParseAutoInspectMode(autoInspectModeOrNull);
        }

        public static AutoInspectMode ParseAutoInspectMode(string autoInspectMode)
        {
            return ParseEnumType<AutoInspectMode>(autoInspectMode);
        }

        public static AutoInspectMode? ParseNullableAutoInspectMode(XElement element)
        {
            return ParseNullableAutoInspectMode(element.Value);
        }

        public static AutoInspectMode ParseAutoInspectMode(XElement element)
        {
            return ParseAutoInspectMode(element.Value);
        }
        public static Priority? ParseNullablePriority(string priorityOrNull)
        {
            return string.IsNullOrWhiteSpace(priorityOrNull)
                ? (Priority?) null
                : ParsePriority(priorityOrNull);
        }

        public static Priority ParsePriority(string priority)
        {
            return ParseEnumType<Priority>(priority);
        }

        public static Priority? ParseNullablePriority(XElement element)
        {
            return ParseNullablePriority(element.Value);
        }

        public static Priority ParsePriority(XElement element)
        {
            return ParsePriority(element.Value);
        }
        public static BucketAclPermission? ParseNullableBucketAclPermission(string bucketAclPermissionOrNull)
        {
            return string.IsNullOrWhiteSpace(bucketAclPermissionOrNull)
                ? (BucketAclPermission?) null
                : ParseBucketAclPermission(bucketAclPermissionOrNull);
        }

        public static BucketAclPermission ParseBucketAclPermission(string bucketAclPermission)
        {
            return ParseEnumType<BucketAclPermission>(bucketAclPermission);
        }

        public static BucketAclPermission? ParseNullableBucketAclPermission(XElement element)
        {
            return ParseNullableBucketAclPermission(element.Value);
        }

        public static BucketAclPermission ParseBucketAclPermission(XElement element)
        {
            return ParseBucketAclPermission(element.Value);
        }
        public static DataIsolationLevel? ParseNullableDataIsolationLevel(string dataIsolationLevelOrNull)
        {
            return string.IsNullOrWhiteSpace(dataIsolationLevelOrNull)
                ? (DataIsolationLevel?) null
                : ParseDataIsolationLevel(dataIsolationLevelOrNull);
        }

        public static DataIsolationLevel ParseDataIsolationLevel(string dataIsolationLevel)
        {
            return ParseEnumType<DataIsolationLevel>(dataIsolationLevel);
        }

        public static DataIsolationLevel? ParseNullableDataIsolationLevel(XElement element)
        {
            return ParseNullableDataIsolationLevel(element.Value);
        }

        public static DataIsolationLevel ParseDataIsolationLevel(XElement element)
        {
            return ParseDataIsolationLevel(element.Value);
        }
        public static DataPersistenceRuleState? ParseNullableDataPersistenceRuleState(string dataPersistenceRuleStateOrNull)
        {
            return string.IsNullOrWhiteSpace(dataPersistenceRuleStateOrNull)
                ? (DataPersistenceRuleState?) null
                : ParseDataPersistenceRuleState(dataPersistenceRuleStateOrNull);
        }

        public static DataPersistenceRuleState ParseDataPersistenceRuleState(string dataPersistenceRuleState)
        {
            return ParseEnumType<DataPersistenceRuleState>(dataPersistenceRuleState);
        }

        public static DataPersistenceRuleState? ParseNullableDataPersistenceRuleState(XElement element)
        {
            return ParseNullableDataPersistenceRuleState(element.Value);
        }

        public static DataPersistenceRuleState ParseDataPersistenceRuleState(XElement element)
        {
            return ParseDataPersistenceRuleState(element.Value);
        }
        public static DataPersistenceRuleType? ParseNullableDataPersistenceRuleType(string dataPersistenceRuleTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(dataPersistenceRuleTypeOrNull)
                ? (DataPersistenceRuleType?) null
                : ParseDataPersistenceRuleType(dataPersistenceRuleTypeOrNull);
        }

        public static DataPersistenceRuleType ParseDataPersistenceRuleType(string dataPersistenceRuleType)
        {
            return ParseEnumType<DataPersistenceRuleType>(dataPersistenceRuleType);
        }

        public static DataPersistenceRuleType? ParseNullableDataPersistenceRuleType(XElement element)
        {
            return ParseNullableDataPersistenceRuleType(element.Value);
        }

        public static DataPersistenceRuleType ParseDataPersistenceRuleType(XElement element)
        {
            return ParseDataPersistenceRuleType(element.Value);
        }
        public static JobChunkClientProcessingOrderGuarantee? ParseNullableJobChunkClientProcessingOrderGuarantee(string jobChunkClientProcessingOrderGuaranteeOrNull)
        {
            return string.IsNullOrWhiteSpace(jobChunkClientProcessingOrderGuaranteeOrNull)
                ? (JobChunkClientProcessingOrderGuarantee?) null
                : ParseJobChunkClientProcessingOrderGuarantee(jobChunkClientProcessingOrderGuaranteeOrNull);
        }

        public static JobChunkClientProcessingOrderGuarantee ParseJobChunkClientProcessingOrderGuarantee(string jobChunkClientProcessingOrderGuarantee)
        {
            return ParseEnumType<JobChunkClientProcessingOrderGuarantee>(jobChunkClientProcessingOrderGuarantee);
        }

        public static JobChunkClientProcessingOrderGuarantee? ParseNullableJobChunkClientProcessingOrderGuarantee(XElement element)
        {
            return ParseNullableJobChunkClientProcessingOrderGuarantee(element.Value);
        }

        public static JobChunkClientProcessingOrderGuarantee ParseJobChunkClientProcessingOrderGuarantee(XElement element)
        {
            return ParseJobChunkClientProcessingOrderGuarantee(element.Value);
        }
        public static JobRequestType? ParseNullableJobRequestType(string jobRequestTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(jobRequestTypeOrNull)
                ? (JobRequestType?) null
                : ParseJobRequestType(jobRequestTypeOrNull);
        }

        public static JobRequestType ParseJobRequestType(string jobRequestType)
        {
            return ParseEnumType<JobRequestType>(jobRequestType);
        }

        public static JobRequestType? ParseNullableJobRequestType(XElement element)
        {
            return ParseNullableJobRequestType(element.Value);
        }

        public static JobRequestType ParseJobRequestType(XElement element)
        {
            return ParseJobRequestType(element.Value);
        }
        public static LtfsFileNamingMode? ParseNullableLtfsFileNamingMode(string ltfsFileNamingModeOrNull)
        {
            return string.IsNullOrWhiteSpace(ltfsFileNamingModeOrNull)
                ? (LtfsFileNamingMode?) null
                : ParseLtfsFileNamingMode(ltfsFileNamingModeOrNull);
        }

        public static LtfsFileNamingMode ParseLtfsFileNamingMode(string ltfsFileNamingMode)
        {
            return ParseEnumType<LtfsFileNamingMode>(ltfsFileNamingMode);
        }

        public static LtfsFileNamingMode? ParseNullableLtfsFileNamingMode(XElement element)
        {
            return ParseNullableLtfsFileNamingMode(element.Value);
        }

        public static LtfsFileNamingMode ParseLtfsFileNamingMode(XElement element)
        {
            return ParseLtfsFileNamingMode(element.Value);
        }
        public static S3ObjectType? ParseNullableS3ObjectType(string s3ObjectTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(s3ObjectTypeOrNull)
                ? (S3ObjectType?) null
                : ParseS3ObjectType(s3ObjectTypeOrNull);
        }

        public static S3ObjectType ParseS3ObjectType(string s3ObjectType)
        {
            return ParseEnumType<S3ObjectType>(s3ObjectType);
        }

        public static S3ObjectType? ParseNullableS3ObjectType(XElement element)
        {
            return ParseNullableS3ObjectType(element.Value);
        }

        public static S3ObjectType ParseS3ObjectType(XElement element)
        {
            return ParseS3ObjectType(element.Value);
        }
        public static StorageDomainFailureType? ParseNullableStorageDomainFailureType(string storageDomainFailureTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(storageDomainFailureTypeOrNull)
                ? (StorageDomainFailureType?) null
                : ParseStorageDomainFailureType(storageDomainFailureTypeOrNull);
        }

        public static StorageDomainFailureType ParseStorageDomainFailureType(string storageDomainFailureType)
        {
            return ParseEnumType<StorageDomainFailureType>(storageDomainFailureType);
        }

        public static StorageDomainFailureType? ParseNullableStorageDomainFailureType(XElement element)
        {
            return ParseNullableStorageDomainFailureType(element.Value);
        }

        public static StorageDomainFailureType ParseStorageDomainFailureType(XElement element)
        {
            return ParseStorageDomainFailureType(element.Value);
        }
        public static StorageDomainMemberState? ParseNullableStorageDomainMemberState(string storageDomainMemberStateOrNull)
        {
            return string.IsNullOrWhiteSpace(storageDomainMemberStateOrNull)
                ? (StorageDomainMemberState?) null
                : ParseStorageDomainMemberState(storageDomainMemberStateOrNull);
        }

        public static StorageDomainMemberState ParseStorageDomainMemberState(string storageDomainMemberState)
        {
            return ParseEnumType<StorageDomainMemberState>(storageDomainMemberState);
        }

        public static StorageDomainMemberState? ParseNullableStorageDomainMemberState(XElement element)
        {
            return ParseNullableStorageDomainMemberState(element.Value);
        }

        public static StorageDomainMemberState ParseStorageDomainMemberState(XElement element)
        {
            return ParseStorageDomainMemberState(element.Value);
        }
        public static SystemFailureType? ParseNullableSystemFailureType(string systemFailureTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(systemFailureTypeOrNull)
                ? (SystemFailureType?) null
                : ParseSystemFailureType(systemFailureTypeOrNull);
        }

        public static SystemFailureType ParseSystemFailureType(string systemFailureType)
        {
            return ParseEnumType<SystemFailureType>(systemFailureType);
        }

        public static SystemFailureType? ParseNullableSystemFailureType(XElement element)
        {
            return ParseNullableSystemFailureType(element.Value);
        }

        public static SystemFailureType ParseSystemFailureType(XElement element)
        {
            return ParseSystemFailureType(element.Value);
        }
        public static UnavailableMediaUsagePolicy? ParseNullableUnavailableMediaUsagePolicy(string unavailableMediaUsagePolicyOrNull)
        {
            return string.IsNullOrWhiteSpace(unavailableMediaUsagePolicyOrNull)
                ? (UnavailableMediaUsagePolicy?) null
                : ParseUnavailableMediaUsagePolicy(unavailableMediaUsagePolicyOrNull);
        }

        public static UnavailableMediaUsagePolicy ParseUnavailableMediaUsagePolicy(string unavailableMediaUsagePolicy)
        {
            return ParseEnumType<UnavailableMediaUsagePolicy>(unavailableMediaUsagePolicy);
        }

        public static UnavailableMediaUsagePolicy? ParseNullableUnavailableMediaUsagePolicy(XElement element)
        {
            return ParseNullableUnavailableMediaUsagePolicy(element.Value);
        }

        public static UnavailableMediaUsagePolicy ParseUnavailableMediaUsagePolicy(XElement element)
        {
            return ParseUnavailableMediaUsagePolicy(element.Value);
        }
        public static VersioningLevel? ParseNullableVersioningLevel(string versioningLevelOrNull)
        {
            return string.IsNullOrWhiteSpace(versioningLevelOrNull)
                ? (VersioningLevel?) null
                : ParseVersioningLevel(versioningLevelOrNull);
        }

        public static VersioningLevel ParseVersioningLevel(string versioningLevel)
        {
            return ParseEnumType<VersioningLevel>(versioningLevel);
        }

        public static VersioningLevel? ParseNullableVersioningLevel(XElement element)
        {
            return ParseNullableVersioningLevel(element.Value);
        }

        public static VersioningLevel ParseVersioningLevel(XElement element)
        {
            return ParseVersioningLevel(element.Value);
        }
        public static WriteOptimization? ParseNullableWriteOptimization(string writeOptimizationOrNull)
        {
            return string.IsNullOrWhiteSpace(writeOptimizationOrNull)
                ? (WriteOptimization?) null
                : ParseWriteOptimization(writeOptimizationOrNull);
        }

        public static WriteOptimization ParseWriteOptimization(string writeOptimization)
        {
            return ParseEnumType<WriteOptimization>(writeOptimization);
        }

        public static WriteOptimization? ParseNullableWriteOptimization(XElement element)
        {
            return ParseNullableWriteOptimization(element.Value);
        }

        public static WriteOptimization ParseWriteOptimization(XElement element)
        {
            return ParseWriteOptimization(element.Value);
        }
        public static WritePreferenceLevel? ParseNullableWritePreferenceLevel(string writePreferenceLevelOrNull)
        {
            return string.IsNullOrWhiteSpace(writePreferenceLevelOrNull)
                ? (WritePreferenceLevel?) null
                : ParseWritePreferenceLevel(writePreferenceLevelOrNull);
        }

        public static WritePreferenceLevel ParseWritePreferenceLevel(string writePreferenceLevel)
        {
            return ParseEnumType<WritePreferenceLevel>(writePreferenceLevel);
        }

        public static WritePreferenceLevel? ParseNullableWritePreferenceLevel(XElement element)
        {
            return ParseNullableWritePreferenceLevel(element.Value);
        }

        public static WritePreferenceLevel ParseWritePreferenceLevel(XElement element)
        {
            return ParseWritePreferenceLevel(element.Value);
        }
        public static PoolFailureType? ParseNullablePoolFailureType(string poolFailureTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(poolFailureTypeOrNull)
                ? (PoolFailureType?) null
                : ParsePoolFailureType(poolFailureTypeOrNull);
        }

        public static PoolFailureType ParsePoolFailureType(string poolFailureType)
        {
            return ParseEnumType<PoolFailureType>(poolFailureType);
        }

        public static PoolFailureType? ParseNullablePoolFailureType(XElement element)
        {
            return ParseNullablePoolFailureType(element.Value);
        }

        public static PoolFailureType ParsePoolFailureType(XElement element)
        {
            return ParsePoolFailureType(element.Value);
        }
        public static PoolHealth? ParseNullablePoolHealth(string poolHealthOrNull)
        {
            return string.IsNullOrWhiteSpace(poolHealthOrNull)
                ? (PoolHealth?) null
                : ParsePoolHealth(poolHealthOrNull);
        }

        public static PoolHealth ParsePoolHealth(string poolHealth)
        {
            return ParseEnumType<PoolHealth>(poolHealth);
        }

        public static PoolHealth? ParseNullablePoolHealth(XElement element)
        {
            return ParseNullablePoolHealth(element.Value);
        }

        public static PoolHealth ParsePoolHealth(XElement element)
        {
            return ParsePoolHealth(element.Value);
        }
        public static PoolState? ParseNullablePoolState(string poolStateOrNull)
        {
            return string.IsNullOrWhiteSpace(poolStateOrNull)
                ? (PoolState?) null
                : ParsePoolState(poolStateOrNull);
        }

        public static PoolState ParsePoolState(string poolState)
        {
            return ParseEnumType<PoolState>(poolState);
        }

        public static PoolState? ParseNullablePoolState(XElement element)
        {
            return ParseNullablePoolState(element.Value);
        }

        public static PoolState ParsePoolState(XElement element)
        {
            return ParsePoolState(element.Value);
        }
        public static PoolType? ParseNullablePoolType(string poolTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(poolTypeOrNull)
                ? (PoolType?) null
                : ParsePoolType(poolTypeOrNull);
        }

        public static PoolType ParsePoolType(string poolType)
        {
            return ParseEnumType<PoolType>(poolType);
        }

        public static PoolType? ParseNullablePoolType(XElement element)
        {
            return ParseNullablePoolType(element.Value);
        }

        public static PoolType ParsePoolType(XElement element)
        {
            return ParsePoolType(element.Value);
        }
        public static ImportConflictResolutionMode? ParseNullableImportConflictResolutionMode(string importConflictResolutionModeOrNull)
        {
            return string.IsNullOrWhiteSpace(importConflictResolutionModeOrNull)
                ? (ImportConflictResolutionMode?) null
                : ParseImportConflictResolutionMode(importConflictResolutionModeOrNull);
        }

        public static ImportConflictResolutionMode ParseImportConflictResolutionMode(string importConflictResolutionMode)
        {
            return ParseEnumType<ImportConflictResolutionMode>(importConflictResolutionMode);
        }

        public static ImportConflictResolutionMode? ParseNullableImportConflictResolutionMode(XElement element)
        {
            return ParseNullableImportConflictResolutionMode(element.Value);
        }

        public static ImportConflictResolutionMode ParseImportConflictResolutionMode(XElement element)
        {
            return ParseImportConflictResolutionMode(element.Value);
        }
        public static Quiesced? ParseNullableQuiesced(string quiescedOrNull)
        {
            return string.IsNullOrWhiteSpace(quiescedOrNull)
                ? (Quiesced?) null
                : ParseQuiesced(quiescedOrNull);
        }

        public static Quiesced ParseQuiesced(string quiesced)
        {
            return ParseEnumType<Quiesced>(quiesced);
        }

        public static Quiesced? ParseNullableQuiesced(XElement element)
        {
            return ParseNullableQuiesced(element.Value);
        }

        public static Quiesced ParseQuiesced(XElement element)
        {
            return ParseQuiesced(element.Value);
        }
        public static ReplicationConflictResolutionMode? ParseNullableReplicationConflictResolutionMode(string replicationConflictResolutionModeOrNull)
        {
            return string.IsNullOrWhiteSpace(replicationConflictResolutionModeOrNull)
                ? (ReplicationConflictResolutionMode?) null
                : ParseReplicationConflictResolutionMode(replicationConflictResolutionModeOrNull);
        }

        public static ReplicationConflictResolutionMode ParseReplicationConflictResolutionMode(string replicationConflictResolutionMode)
        {
            return ParseEnumType<ReplicationConflictResolutionMode>(replicationConflictResolutionMode);
        }

        public static ReplicationConflictResolutionMode? ParseNullableReplicationConflictResolutionMode(XElement element)
        {
            return ParseNullableReplicationConflictResolutionMode(element.Value);
        }

        public static ReplicationConflictResolutionMode ParseReplicationConflictResolutionMode(XElement element)
        {
            return ParseReplicationConflictResolutionMode(element.Value);
        }
        public static ImportExportConfiguration? ParseNullableImportExportConfiguration(string importExportConfigurationOrNull)
        {
            return string.IsNullOrWhiteSpace(importExportConfigurationOrNull)
                ? (ImportExportConfiguration?) null
                : ParseImportExportConfiguration(importExportConfigurationOrNull);
        }

        public static ImportExportConfiguration ParseImportExportConfiguration(string importExportConfiguration)
        {
            return ParseEnumType<ImportExportConfiguration>(importExportConfiguration);
        }

        public static ImportExportConfiguration? ParseNullableImportExportConfiguration(XElement element)
        {
            return ParseNullableImportExportConfiguration(element.Value);
        }

        public static ImportExportConfiguration ParseImportExportConfiguration(XElement element)
        {
            return ParseImportExportConfiguration(element.Value);
        }
        public static TapeDriveState? ParseNullableTapeDriveState(string tapeDriveStateOrNull)
        {
            return string.IsNullOrWhiteSpace(tapeDriveStateOrNull)
                ? (TapeDriveState?) null
                : ParseTapeDriveState(tapeDriveStateOrNull);
        }

        public static TapeDriveState ParseTapeDriveState(string tapeDriveState)
        {
            return ParseEnumType<TapeDriveState>(tapeDriveState);
        }

        public static TapeDriveState? ParseNullableTapeDriveState(XElement element)
        {
            return ParseNullableTapeDriveState(element.Value);
        }

        public static TapeDriveState ParseTapeDriveState(XElement element)
        {
            return ParseTapeDriveState(element.Value);
        }
        public static TapeDriveType? ParseNullableTapeDriveType(string tapeDriveTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(tapeDriveTypeOrNull)
                ? (TapeDriveType?) null
                : ParseTapeDriveType(tapeDriveTypeOrNull);
        }

        public static TapeDriveType ParseTapeDriveType(string tapeDriveType)
        {
            return ParseEnumType<TapeDriveType>(tapeDriveType);
        }

        public static TapeDriveType? ParseNullableTapeDriveType(XElement element)
        {
            return ParseNullableTapeDriveType(element.Value);
        }

        public static TapeDriveType ParseTapeDriveType(XElement element)
        {
            return ParseTapeDriveType(element.Value);
        }
        public static TapeFailureType? ParseNullableTapeFailureType(string tapeFailureTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(tapeFailureTypeOrNull)
                ? (TapeFailureType?) null
                : ParseTapeFailureType(tapeFailureTypeOrNull);
        }

        public static TapeFailureType ParseTapeFailureType(string tapeFailureType)
        {
            return ParseEnumType<TapeFailureType>(tapeFailureType);
        }

        public static TapeFailureType? ParseNullableTapeFailureType(XElement element)
        {
            return ParseNullableTapeFailureType(element.Value);
        }

        public static TapeFailureType ParseTapeFailureType(XElement element)
        {
            return ParseTapeFailureType(element.Value);
        }
        public static TapePartitionFailureType? ParseNullableTapePartitionFailureType(string tapePartitionFailureTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(tapePartitionFailureTypeOrNull)
                ? (TapePartitionFailureType?) null
                : ParseTapePartitionFailureType(tapePartitionFailureTypeOrNull);
        }

        public static TapePartitionFailureType ParseTapePartitionFailureType(string tapePartitionFailureType)
        {
            return ParseEnumType<TapePartitionFailureType>(tapePartitionFailureType);
        }

        public static TapePartitionFailureType? ParseNullableTapePartitionFailureType(XElement element)
        {
            return ParseNullableTapePartitionFailureType(element.Value);
        }

        public static TapePartitionFailureType ParseTapePartitionFailureType(XElement element)
        {
            return ParseTapePartitionFailureType(element.Value);
        }
        public static TapePartitionState? ParseNullableTapePartitionState(string tapePartitionStateOrNull)
        {
            return string.IsNullOrWhiteSpace(tapePartitionStateOrNull)
                ? (TapePartitionState?) null
                : ParseTapePartitionState(tapePartitionStateOrNull);
        }

        public static TapePartitionState ParseTapePartitionState(string tapePartitionState)
        {
            return ParseEnumType<TapePartitionState>(tapePartitionState);
        }

        public static TapePartitionState? ParseNullableTapePartitionState(XElement element)
        {
            return ParseNullableTapePartitionState(element.Value);
        }

        public static TapePartitionState ParseTapePartitionState(XElement element)
        {
            return ParseTapePartitionState(element.Value);
        }
        public static TapeState? ParseNullableTapeState(string tapeStateOrNull)
        {
            return string.IsNullOrWhiteSpace(tapeStateOrNull)
                ? (TapeState?) null
                : ParseTapeState(tapeStateOrNull);
        }

        public static TapeState ParseTapeState(string tapeState)
        {
            return ParseEnumType<TapeState>(tapeState);
        }

        public static TapeState? ParseNullableTapeState(XElement element)
        {
            return ParseNullableTapeState(element.Value);
        }

        public static TapeState ParseTapeState(XElement element)
        {
            return ParseTapeState(element.Value);
        }
        public static TapeType? ParseNullableTapeType(string tapeTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(tapeTypeOrNull)
                ? (TapeType?) null
                : ParseTapeType(tapeTypeOrNull);
        }

        public static TapeType ParseTapeType(string tapeType)
        {
            return ParseEnumType<TapeType>(tapeType);
        }

        public static TapeType? ParseNullableTapeType(XElement element)
        {
            return ParseNullableTapeType(element.Value);
        }

        public static TapeType ParseTapeType(XElement element)
        {
            return ParseTapeType(element.Value);
        }
        public static BlobStoreTaskState? ParseNullableBlobStoreTaskState(string blobStoreTaskStateOrNull)
        {
            return string.IsNullOrWhiteSpace(blobStoreTaskStateOrNull)
                ? (BlobStoreTaskState?) null
                : ParseBlobStoreTaskState(blobStoreTaskStateOrNull);
        }

        public static BlobStoreTaskState ParseBlobStoreTaskState(string blobStoreTaskState)
        {
            return ParseEnumType<BlobStoreTaskState>(blobStoreTaskState);
        }

        public static BlobStoreTaskState? ParseNullableBlobStoreTaskState(XElement element)
        {
            return ParseNullableBlobStoreTaskState(element.Value);
        }

        public static BlobStoreTaskState ParseBlobStoreTaskState(XElement element)
        {
            return ParseBlobStoreTaskState(element.Value);
        }
        public static CacheEntryState? ParseNullableCacheEntryState(string cacheEntryStateOrNull)
        {
            return string.IsNullOrWhiteSpace(cacheEntryStateOrNull)
                ? (CacheEntryState?) null
                : ParseCacheEntryState(cacheEntryStateOrNull);
        }

        public static CacheEntryState ParseCacheEntryState(string cacheEntryState)
        {
            return ParseEnumType<CacheEntryState>(cacheEntryState);
        }

        public static CacheEntryState? ParseNullableCacheEntryState(XElement element)
        {
            return ParseNullableCacheEntryState(element.Value);
        }

        public static CacheEntryState ParseCacheEntryState(XElement element)
        {
            return ParseCacheEntryState(element.Value);
        }
        public static JobStatus? ParseNullableJobStatus(string jobStatusOrNull)
        {
            return string.IsNullOrWhiteSpace(jobStatusOrNull)
                ? (JobStatus?) null
                : ParseJobStatus(jobStatusOrNull);
        }

        public static JobStatus ParseJobStatus(string jobStatus)
        {
            return ParseEnumType<JobStatus>(jobStatus);
        }

        public static JobStatus? ParseNullableJobStatus(XElement element)
        {
            return ParseNullableJobStatus(element.Value);
        }

        public static JobStatus ParseJobStatus(XElement element)
        {
            return ParseJobStatus(element.Value);
        }
        public static RestOperationType? ParseNullableRestOperationType(string restOperationTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(restOperationTypeOrNull)
                ? (RestOperationType?) null
                : ParseRestOperationType(restOperationTypeOrNull);
        }

        public static RestOperationType ParseRestOperationType(string restOperationType)
        {
            return ParseEnumType<RestOperationType>(restOperationType);
        }

        public static RestOperationType? ParseNullableRestOperationType(XElement element)
        {
            return ParseNullableRestOperationType(element.Value);
        }

        public static RestOperationType ParseRestOperationType(XElement element)
        {
            return ParseRestOperationType(element.Value);
        }
        public static DatabasePhysicalSpaceState? ParseNullableDatabasePhysicalSpaceState(string databasePhysicalSpaceStateOrNull)
        {
            return string.IsNullOrWhiteSpace(databasePhysicalSpaceStateOrNull)
                ? (DatabasePhysicalSpaceState?) null
                : ParseDatabasePhysicalSpaceState(databasePhysicalSpaceStateOrNull);
        }

        public static DatabasePhysicalSpaceState ParseDatabasePhysicalSpaceState(string databasePhysicalSpaceState)
        {
            return ParseEnumType<DatabasePhysicalSpaceState>(databasePhysicalSpaceState);
        }

        public static DatabasePhysicalSpaceState? ParseNullableDatabasePhysicalSpaceState(XElement element)
        {
            return ParseNullableDatabasePhysicalSpaceState(element.Value);
        }

        public static DatabasePhysicalSpaceState ParseDatabasePhysicalSpaceState(XElement element)
        {
            return ParseDatabasePhysicalSpaceState(element.Value);
        }
        public static HttpResponseFormatType? ParseNullableHttpResponseFormatType(string httpResponseFormatTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(httpResponseFormatTypeOrNull)
                ? (HttpResponseFormatType?) null
                : ParseHttpResponseFormatType(httpResponseFormatTypeOrNull);
        }

        public static HttpResponseFormatType ParseHttpResponseFormatType(string httpResponseFormatType)
        {
            return ParseEnumType<HttpResponseFormatType>(httpResponseFormatType);
        }

        public static HttpResponseFormatType? ParseNullableHttpResponseFormatType(XElement element)
        {
            return ParseNullableHttpResponseFormatType(element.Value);
        }

        public static HttpResponseFormatType ParseHttpResponseFormatType(XElement element)
        {
            return ParseHttpResponseFormatType(element.Value);
        }
        public static RequestType? ParseNullableRequestType(string requestTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(requestTypeOrNull)
                ? (RequestType?) null
                : ParseRequestType(requestTypeOrNull);
        }

        public static RequestType ParseRequestType(string requestType)
        {
            return ParseEnumType<RequestType>(requestType);
        }

        public static RequestType? ParseNullableRequestType(XElement element)
        {
            return ParseNullableRequestType(element.Value);
        }

        public static RequestType ParseRequestType(XElement element)
        {
            return ParseRequestType(element.Value);
        }
        public static NamingConventionType? ParseNullableNamingConventionType(string namingConventionTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(namingConventionTypeOrNull)
                ? (NamingConventionType?) null
                : ParseNamingConventionType(namingConventionTypeOrNull);
        }

        public static NamingConventionType ParseNamingConventionType(string namingConventionType)
        {
            return ParseEnumType<NamingConventionType>(namingConventionType);
        }

        public static NamingConventionType? ParseNullableNamingConventionType(XElement element)
        {
            return ParseNullableNamingConventionType(element.Value);
        }

        public static NamingConventionType ParseNamingConventionType(XElement element)
        {
            return ParseNamingConventionType(element.Value);
        }

        //ChecksumType parsers

        public static ChecksumType.Type? ParseNullableChecksumType(string checksumTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(checksumTypeOrNull)
                ? (ChecksumType.Type?) null
                : ParseChecksumType(checksumTypeOrNull);
        }

        public static ChecksumType.Type ParseChecksumType(string checksumType)
        {
            return
                ParseEnumType<ChecksumType.Type>(checksumType);
        }

        public static ChecksumType.Type? ParseNullableChecksumType(XElement element)
        {
            return ParseNullableChecksumType(element.Value);
        }

        public static ChecksumType.Type ParseChecksumType(XElement element)
        {
            return ParseChecksumType(element.Value);
        }

        //Guid parsers

        public static Guid? ParseNullableGuid(XElement element)
        {
            return ParseNullableGuid(element.Value);
        }

        public static Guid? ParseNullableGuid(string guidStringOrNull)
        {
            return string.IsNullOrWhiteSpace(guidStringOrNull)
                ? (Guid?)null
                : ParseGuid(guidStringOrNull);
        }

        public static Guid ParseGuid(XElement element)
        {
            return ParseGuid(element.Value);
        }

        public static Guid ParseGuid(string guidString)
        {
            return Guid.Parse(guidString);
        }

        //DateTime parsers

        public static DateTime? ParseNullableDateTime(XElement element)
        {
            return ParseNullableDateTime(element.Value);
        }

        public static DateTime? ParseNullableDateTime(string dateTimeStringOrNull)
        {
            return string.IsNullOrWhiteSpace(dateTimeStringOrNull)
                ? (DateTime?)null
                : ParseDateTime(dateTimeStringOrNull);
        }

        public static DateTime ParseDateTime(XElement element)
        {
            return ParseDateTime(element.Value);
        }

        public static DateTime ParseDateTime(string dateTimeString)
        {
            return DateTime.Parse(dateTimeString);
        }

        //Boolean parsers

        public static bool? ParseNullableBool(XElement element)
        {
            return ParseNullableBool(element.Value);
        }

        public static bool? ParseNullableBool(string boolStringOrNull)
        {
            return string.IsNullOrWhiteSpace(boolStringOrNull)
                ? (bool?)null
                : ParseBool(boolStringOrNull);
        }

        public static bool ParseBool(XElement element)
        {
            return ParseBool(element.Value);
        }

        public static bool ParseBool(string boolString)
        {
            return bool.Parse(boolString);
        }

        //String parsers

        public static string ParseNullableString(XElement element)
        {
            return ParseNullableString(element.Value);
        }

        public static string ParseNullableString(string stringOrNull)
        {
            return string.IsNullOrWhiteSpace(stringOrNull)
                ? (string)null
                : ParseString(stringOrNull);
        }

        public static string ParseString(XElement element)
        {
            return ParseString(element.Value);
        }

        public static string ParseString(string str)
        {
            return str;
        }

        //Integer parsers

        public static int? ParseNullableInt(XElement element)
        {
            return ParseNullableInt(element.Value);
        }

        public static int? ParseNullableInt(string intStringOrNull)
        {
            return string.IsNullOrWhiteSpace(intStringOrNull)
                ? (int?)null
                : ParseInt(intStringOrNull);
        }

        public static int ParseInt(XElement element)
        {
            return ParseInt(element.Value);
        }

        public static int ParseInt(string intString)
        {
            return int.Parse(intString);
        }

        //Long parsers

        public static long? ParseNullableLong(XElement element)
        {
            return ParseNullableLong(element.Value);
        }

        public static long? ParseNullableLong(string longStringOrNull)
        {
            return string.IsNullOrWhiteSpace(longStringOrNull)
                ? (long?)null
                : ParseLong(longStringOrNull);
        }

        public static long ParseLong(XElement element)
        {
            return ParseLong(element.Value);
        }

        public static long ParseLong(string longString)
        {
            return long.Parse(longString);
        }

        //Double parsers

        public static double? ParseNullableDouble(XElement element)
        {
            return ParseNullableDouble(element.Value);
        }

        public static double? ParseNullableDouble(string doubleStringOrNull)
        {
            return string.IsNullOrWhiteSpace(doubleStringOrNull)
                ? (double?)null
                : ParseDouble(doubleStringOrNull);
        }

        public static double ParseDouble(XElement element)
        {
            return ParseDouble(element.Value);
        }

        public static double ParseDouble(string doubleString)
        {
            return double.Parse(doubleString);
        }

        //Enum parser

        public static T ParseEnumType<T>(string enumString)
            where T : struct
        {
            T result;
            if (!Enum.TryParse(ConvertToPascalCase(enumString), out result))
            {
                throw new ArgumentException(string.Format(Resources.InvalidValueForTypeException, typeof(T).Name));
            }
            return result;
        }

        public static string ConvertToPascalCase(string uppercaseUnderscore)
        {
            var sb = new StringBuilder();
            foreach (var word in uppercaseUnderscore.Split('_'))
            {
                sb.Append(word[0]);
                sb.Append(word.Substring(1).ToLowerInvariant());
            }
            return sb.ToString();
        }
    }
}
