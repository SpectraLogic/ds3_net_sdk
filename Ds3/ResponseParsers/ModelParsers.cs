/*
 * ******************************************************************************
 *   Copyright 2014-2015 Spectra Logic Corporation. All Rights Reserved.
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

namespace Ds3.ResponseParsers
{
    internal class ModelParsers
    {

        internal static PhysicalPlacement ParsePhysicalPlacement(XElement element)
        {
            return new PhysicalPlacement
            {
                pools = element.Element("Pools").Elements("Pool").Select(ParsePool).ToList(),
                tapes = element.Element("Tapes").Elements("Tape").Select(ParseTape).ToList()
            };
        }

        public static PhysicalPlacement ParseNullablePhysicalPlacement(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParsePhysicalPlacement(content);
        }
        internal static Blob ParseBlob(XElement element)
        {
            return new Blob
            {
                byteOffset = ParseLong(element.Element("ByteOffset")),
                checksum = ParseNullableString(element.Element("Checksum")),
                checksumType = ParseNullableChecksumType.Type(element.Element("ChecksumType")),
                id = ParseGuid(element.Element("Id")),
                length = ParseLong(element.Element("Length")),
                objectId = ParseGuid(element.Element("ObjectId"))
            };
        }

        public static Blob ParseNullableBlob(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseBlob(content);
        }
        internal static Bucket ParseBucket(XElement element)
        {
            return new Bucket
            {
                creationDate = ParseDateTime(element.Element("CreationDate")),
                dataPolicyId = ParseGuid(element.Element("DataPolicyId")),
                id = ParseGuid(element.Element("Id")),
                lastPreferredChunkSizeInBytes = ParseNullableLong(element.Element("LastPreferredChunkSizeInBytes")),
                logicalUsedCapacity = ParseNullableLong(element.Element("LogicalUsedCapacity")),
                name = ParseNullableString(element.Element("Name")),
                userId = ParseGuid(element.Element("UserId"))
            };
        }

        public static Bucket ParseNullableBucket(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseBucket(content);
        }
        internal static BucketAcl ParseBucketAcl(XElement element)
        {
            return new BucketAcl
            {
                bucketId = ParseNullableGuid(element.Element("BucketId")),
                groupId = ParseNullableGuid(element.Element("GroupId")),
                id = ParseGuid(element.Element("Id")),
                permission = ParseBucketAclPermission(element.Element("Permission")),
                userId = ParseNullableGuid(element.Element("UserId"))
            };
        }

        public static BucketAcl ParseNullableBucketAcl(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseBucketAcl(content);
        }
        internal static CanceledJob ParseCanceledJob(XElement element)
        {
            return new CanceledJob
            {
                bucketId = ParseGuid(element.Element("BucketId")),
                cachedSizeInBytes = ParseLong(element.Element("CachedSizeInBytes")),
                chunkClientProcessingOrderGuarantee = ParseJobChunkClientProcessingOrderGuarantee(element.Element("ChunkClientProcessingOrderGuarantee")),
                completedSizeInBytes = ParseLong(element.Element("CompletedSizeInBytes")),
                createdAt = ParseDateTime(element.Element("CreatedAt")),
                dateCanceled = ParseDateTime(element.Element("DateCanceled")),
                errorMessage = ParseNullableString(element.Element("ErrorMessage")),
                id = ParseGuid(element.Element("Id")),
                naked = ParseBool(element.Element("Naked")),
                name = ParseNullableString(element.Element("Name")),
                originalSizeInBytes = ParseLong(element.Element("OriginalSizeInBytes")),
                priority = ParsePriority(element.Element("Priority")),
                rechunked = ParseNullableDateTime(element.Element("Rechunked")),
                requestType = ParseJobRequestType(element.Element("RequestType")),
                truncated = ParseBool(element.Element("Truncated")),
                userId = ParseGuid(element.Element("UserId"))
            };
        }

        public static CanceledJob ParseNullableCanceledJob(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseCanceledJob(content);
        }
        internal static CapacitySummaryContainer ParseCapacitySummaryContainer(XElement element)
        {
            return new CapacitySummaryContainer
            {
                pool = ParseStorageDomainCapacitySummary(element.Element("Pool")),
                tape = ParseStorageDomainCapacitySummary(element.Element("Tape"))
            };
        }

        public static CapacitySummaryContainer ParseNullableCapacitySummaryContainer(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseCapacitySummaryContainer(content);
        }
        internal static CompletedJob ParseCompletedJob(XElement element)
        {
            return new CompletedJob
            {
                bucketId = ParseGuid(element.Element("BucketId")),
                cachedSizeInBytes = ParseLong(element.Element("CachedSizeInBytes")),
                chunkClientProcessingOrderGuarantee = ParseJobChunkClientProcessingOrderGuarantee(element.Element("ChunkClientProcessingOrderGuarantee")),
                completedSizeInBytes = ParseLong(element.Element("CompletedSizeInBytes")),
                createdAt = ParseDateTime(element.Element("CreatedAt")),
                dateCompleted = ParseDateTime(element.Element("DateCompleted")),
                errorMessage = ParseNullableString(element.Element("ErrorMessage")),
                id = ParseGuid(element.Element("Id")),
                naked = ParseBool(element.Element("Naked")),
                name = ParseNullableString(element.Element("Name")),
                originalSizeInBytes = ParseLong(element.Element("OriginalSizeInBytes")),
                priority = ParsePriority(element.Element("Priority")),
                rechunked = ParseNullableDateTime(element.Element("Rechunked")),
                requestType = ParseJobRequestType(element.Element("RequestType")),
                truncated = ParseBool(element.Element("Truncated")),
                userId = ParseGuid(element.Element("UserId"))
            };
        }

        public static CompletedJob ParseNullableCompletedJob(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseCompletedJob(content);
        }
        internal static DataPathBackend ParseDataPathBackend(XElement element)
        {
            return new DataPathBackend
            {
                activated = ParseBool(element.Element("Activated")),
                autoActivateTimeoutInMins = ParseNullableInt(element.Element("AutoActivateTimeoutInMins")),
                autoInspect = ParseAutoInspectMode(element.Element("AutoInspect")),
                defaultImportConflictResolutionMode = ParseImportConflictResolutionMode(element.Element("DefaultImportConflictResolutionMode")),
                id = ParseGuid(element.Element("Id")),
                lastHeartbeat = ParseDateTime(element.Element("LastHeartbeat")),
                unavailableMediaPolicy = ParseUnavailableMediaUsagePolicy(element.Element("UnavailableMediaPolicy")),
                unavailablePoolMaxJobRetryInMins = ParseInt(element.Element("UnavailablePoolMaxJobRetryInMins")),
                unavailableTapePartitionMaxJobRetryInMins = ParseInt(element.Element("UnavailableTapePartitionMaxJobRetryInMins"))
            };
        }

        public static DataPathBackend ParseNullableDataPathBackend(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDataPathBackend(content);
        }
        internal static DataPersistenceRule ParseDataPersistenceRule(XElement element)
        {
            return new DataPersistenceRule
            {
                dataPolicyId = ParseGuid(element.Element("DataPolicyId")),
                id = ParseGuid(element.Element("Id")),
                isolationLevel = ParseDataIsolationLevel(element.Element("IsolationLevel")),
                minimumDaysToRetain = ParseNullableInt(element.Element("MinimumDaysToRetain")),
                state = ParseDataPersistenceRuleState(element.Element("State")),
                storageDomainId = ParseGuid(element.Element("StorageDomainId")),
                type = ParseDataPersistenceRuleType(element.Element("Type"))
            };
        }

        public static DataPersistenceRule ParseNullableDataPersistenceRule(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDataPersistenceRule(content);
        }
        internal static DataPolicy ParseDataPolicy(XElement element)
        {
            return new DataPolicy
            {
                blobbingEnabled = ParseBool(element.Element("BlobbingEnabled")),
                checksumType = ParseChecksumType.Type(element.Element("ChecksumType")),
                creationDate = ParseDateTime(element.Element("CreationDate")),
                defaultBlobSize = ParseNullableLong(element.Element("DefaultBlobSize")),
                defaultGetJobPriority = ParsePriority(element.Element("DefaultGetJobPriority")),
                defaultPutJobPriority = ParsePriority(element.Element("DefaultPutJobPriority")),
                defaultVerifyJobPriority = ParsePriority(element.Element("DefaultVerifyJobPriority")),
                endToEndCrcRequired = ParseBool(element.Element("EndToEndCrcRequired")),
                id = ParseGuid(element.Element("Id")),
                ltfsObjectNamingAllowed = ParseBool(element.Element("LtfsObjectNamingAllowed")),
                name = ParseNullableString(element.Element("Name")),
                rebuildPriority = ParsePriority(element.Element("RebuildPriority")),
                versioning = ParseVersioningLevel(element.Element("Versioning"))
            };
        }

        public static DataPolicy ParseNullableDataPolicy(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDataPolicy(content);
        }
        internal static DataPolicyAcl ParseDataPolicyAcl(XElement element)
        {
            return new DataPolicyAcl
            {
                dataPolicyId = ParseNullableGuid(element.Element("DataPolicyId")),
                groupId = ParseNullableGuid(element.Element("GroupId")),
                id = ParseGuid(element.Element("Id")),
                userId = ParseNullableGuid(element.Element("UserId"))
            };
        }

        public static DataPolicyAcl ParseNullableDataPolicyAcl(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDataPolicyAcl(content);
        }
        internal static Group ParseGroup(XElement element)
        {
            return new Group
            {
                builtIn = ParseBool(element.Element("BuiltIn")),
                id = ParseGuid(element.Element("Id")),
                name = ParseNullableString(element.Element("Name"))
            };
        }

        public static Group ParseNullableGroup(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseGroup(content);
        }
        internal static GroupMember ParseGroupMember(XElement element)
        {
            return new GroupMember
            {
                groupId = ParseGuid(element.Element("GroupId")),
                id = ParseGuid(element.Element("Id")),
                memberGroupId = ParseNullableGuid(element.Element("MemberGroupId")),
                memberUserId = ParseNullableGuid(element.Element("MemberUserId"))
            };
        }

        public static GroupMember ParseNullableGroupMember(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseGroupMember(content);
        }
        internal static ActiveJob ParseActiveJob(XElement element)
        {
            return new ActiveJob
            {
                aggregating = ParseBool(element.Element("Aggregating")),
                bucketId = ParseGuid(element.Element("BucketId")),
                cachedSizeInBytes = ParseLong(element.Element("CachedSizeInBytes")),
                chunkClientProcessingOrderGuarantee = ParseJobChunkClientProcessingOrderGuarantee(element.Element("ChunkClientProcessingOrderGuarantee")),
                completedSizeInBytes = ParseLong(element.Element("CompletedSizeInBytes")),
                createdAt = ParseDateTime(element.Element("CreatedAt")),
                errorMessage = ParseNullableString(element.Element("ErrorMessage")),
                id = ParseGuid(element.Element("Id")),
                naked = ParseBool(element.Element("Naked")),
                name = ParseNullableString(element.Element("Name")),
                originalSizeInBytes = ParseLong(element.Element("OriginalSizeInBytes")),
                priority = ParsePriority(element.Element("Priority")),
                rechunked = ParseNullableDateTime(element.Element("Rechunked")),
                requestType = ParseJobRequestType(element.Element("RequestType")),
                truncated = ParseBool(element.Element("Truncated")),
                userId = ParseGuid(element.Element("UserId"))
            };
        }

        public static ActiveJob ParseNullableActiveJob(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseActiveJob(content);
        }
        internal static Node ParseNode(XElement element)
        {
            return new Node
            {
                dataPathHttpPort = ParseNullableInt(element.Element("DataPathHttpPort")),
                dataPathHttpsPort = ParseNullableInt(element.Element("DataPathHttpsPort")),
                dataPathIpAddress = ParseNullableString(element.Element("DataPathIpAddress")),
                dnsName = ParseNullableString(element.Element("DnsName")),
                id = ParseGuid(element.Element("Id")),
                lastHeartbeat = ParseDateTime(element.Element("LastHeartbeat")),
                name = ParseNullableString(element.Element("Name")),
                serialNumber = ParseNullableString(element.Element("SerialNumber"))
            };
        }

        public static Node ParseNullableNode(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseNode(content);
        }
        internal static S3Object ParseS3Object(XElement element)
        {
            return new S3Object
            {
                bucketId = ParseGuid(element.Element("BucketId")),
                creationDate = ParseNullableDateTime(element.Element("CreationDate")),
                id = ParseGuid(element.Element("Id")),
                latest = ParseBool(element.Element("Latest")),
                name = ParseNullableString(element.Element("Name")),
                type = ParseS3ObjectType(element.Element("Type")),
                version = ParseLong(element.Element("Version"))
            };
        }

        public static S3Object ParseNullableS3Object(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseS3Object(content);
        }
        internal static StorageDomain ParseStorageDomain(XElement element)
        {
            return new StorageDomain
            {
                autoEjectUponCron = ParseNullableString(element.Element("AutoEjectUponCron")),
                autoEjectUponJobCancellation = ParseBool(element.Element("AutoEjectUponJobCancellation")),
                autoEjectUponJobCompletion = ParseBool(element.Element("AutoEjectUponJobCompletion")),
                autoEjectUponMediaFull = ParseBool(element.Element("AutoEjectUponMediaFull")),
                id = ParseGuid(element.Element("Id")),
                ltfsFileNaming = ParseLtfsFileNamingMode(element.Element("LtfsFileNaming")),
                maxTapeFragmentationPercent = ParseInt(element.Element("MaxTapeFragmentationPercent")),
                maximumAutoVerificationFrequencyInDays = ParseInt(element.Element("MaximumAutoVerificationFrequencyInDays")),
                mediaEjectionAllowed = ParseBool(element.Element("MediaEjectionAllowed")),
                name = ParseNullableString(element.Element("Name")),
                verifyPriorToAutoEject = ParseNullablePriority(element.Element("VerifyPriorToAutoEject")),
                writeOptimization = ParseWriteOptimization(element.Element("WriteOptimization"))
            };
        }

        public static StorageDomain ParseNullableStorageDomain(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseStorageDomain(content);
        }
        internal static StorageDomainCapacitySummary ParseStorageDomainCapacitySummary(XElement element)
        {
            return new StorageDomainCapacitySummary
            {
                physicalAllocated = ParseLong(element.Element("PhysicalAllocated")),
                physicalFree = ParseLong(element.Element("PhysicalFree")),
                physicalUsed = ParseLong(element.Element("PhysicalUsed"))
            };
        }

        public static StorageDomainCapacitySummary ParseNullableStorageDomainCapacitySummary(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseStorageDomainCapacitySummary(content);
        }
        internal static StorageDomainFailure ParseStorageDomainFailure(XElement element)
        {
            return new StorageDomainFailure
            {
                date = ParseDateTime(element.Element("Date")),
                errorMessage = ParseNullableString(element.Element("ErrorMessage")),
                id = ParseGuid(element.Element("Id")),
                storageDomainId = ParseGuid(element.Element("StorageDomainId")),
                type = ParseStorageDomainFailureType(element.Element("Type"))
            };
        }

        public static StorageDomainFailure ParseNullableStorageDomainFailure(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseStorageDomainFailure(content);
        }
        internal static StorageDomainMember ParseStorageDomainMember(XElement element)
        {
            return new StorageDomainMember
            {
                id = ParseGuid(element.Element("Id")),
                poolPartitionId = ParseNullableGuid(element.Element("PoolPartitionId")),
                state = ParseStorageDomainMemberState(element.Element("State")),
                storageDomainId = ParseGuid(element.Element("StorageDomainId")),
                tapePartitionId = ParseNullableGuid(element.Element("TapePartitionId")),
                tapeType = ParseNullableTapeType(element.Element("TapeType")),
                writePreference = ParseWritePreferenceLevel(element.Element("WritePreference"))
            };
        }

        public static StorageDomainMember ParseNullableStorageDomainMember(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseStorageDomainMember(content);
        }
        internal static SystemFailure ParseSystemFailure(XElement element)
        {
            return new SystemFailure
            {
                date = ParseDateTime(element.Element("Date")),
                errorMessage = ParseNullableString(element.Element("ErrorMessage")),
                id = ParseGuid(element.Element("Id")),
                type = ParseSystemFailureType(element.Element("Type"))
            };
        }

        public static SystemFailure ParseNullableSystemFailure(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseSystemFailure(content);
        }
        internal static SpectraUser ParseSpectraUser(XElement element)
        {
            return new SpectraUser
            {
                authId = ParseNullableString(element.Element("AuthId")),
                defaultDataPolicyId = ParseNullableGuid(element.Element("DefaultDataPolicyId")),
                id = ParseGuid(element.Element("Id")),
                name = ParseNullableString(element.Element("Name")),
                secretKey = ParseNullableString(element.Element("SecretKey"))
            };
        }

        public static SpectraUser ParseNullableSpectraUser(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseSpectraUser(content);
        }
        internal static GenericDaoNotificationRegistration ParseGenericDaoNotificationRegistration(XElement element)
        {
            return new GenericDaoNotificationRegistration
            {
                creationDate = ParseDateTime(element.Element("CreationDate")),
                daoType = ParseNullableString(element.Element("DaoType")),
                format = ParseHttpResponseFormatType(element.Element("Format")),
                id = ParseGuid(element.Element("Id")),
                lastFailure = ParseNullableString(element.Element("LastFailure")),
                lastHttpResponseCode = ParseNullableInt(element.Element("LastHttpResponseCode")),
                lastNotification = ParseNullableDateTime(element.Element("LastNotification")),
                namingConvention = ParseNamingConventionType(element.Element("NamingConvention")),
                notificationEndPoint = ParseNullableString(element.Element("NotificationEndPoint")),
                notificationHttpMethod = ParseRequestType(element.Element("NotificationHttpMethod")),
                numberOfFailuresSinceLastSuccess = ParseInt(element.Element("NumberOfFailuresSinceLastSuccess")),
                userId = ParseNullableGuid(element.Element("UserId"))
            };
        }

        public static GenericDaoNotificationRegistration ParseNullableGenericDaoNotificationRegistration(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseGenericDaoNotificationRegistration(content);
        }
        internal static JobCompletedNotificationRegistration ParseJobCompletedNotificationRegistration(XElement element)
        {
            return new JobCompletedNotificationRegistration
            {
                creationDate = ParseDateTime(element.Element("CreationDate")),
                format = ParseHttpResponseFormatType(element.Element("Format")),
                id = ParseGuid(element.Element("Id")),
                jobId = ParseNullableGuid(element.Element("JobId")),
                lastFailure = ParseNullableString(element.Element("LastFailure")),
                lastHttpResponseCode = ParseNullableInt(element.Element("LastHttpResponseCode")),
                lastNotification = ParseNullableDateTime(element.Element("LastNotification")),
                namingConvention = ParseNamingConventionType(element.Element("NamingConvention")),
                notificationEndPoint = ParseNullableString(element.Element("NotificationEndPoint")),
                notificationHttpMethod = ParseRequestType(element.Element("NotificationHttpMethod")),
                numberOfFailuresSinceLastSuccess = ParseInt(element.Element("NumberOfFailuresSinceLastSuccess")),
                userId = ParseNullableGuid(element.Element("UserId"))
            };
        }

        public static JobCompletedNotificationRegistration ParseNullableJobCompletedNotificationRegistration(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseJobCompletedNotificationRegistration(content);
        }
        internal static JobCreatedNotificationRegistration ParseJobCreatedNotificationRegistration(XElement element)
        {
            return new JobCreatedNotificationRegistration
            {
                creationDate = ParseDateTime(element.Element("CreationDate")),
                format = ParseHttpResponseFormatType(element.Element("Format")),
                id = ParseGuid(element.Element("Id")),
                lastFailure = ParseNullableString(element.Element("LastFailure")),
                lastHttpResponseCode = ParseNullableInt(element.Element("LastHttpResponseCode")),
                lastNotification = ParseNullableDateTime(element.Element("LastNotification")),
                namingConvention = ParseNamingConventionType(element.Element("NamingConvention")),
                notificationEndPoint = ParseNullableString(element.Element("NotificationEndPoint")),
                notificationHttpMethod = ParseRequestType(element.Element("NotificationHttpMethod")),
                numberOfFailuresSinceLastSuccess = ParseInt(element.Element("NumberOfFailuresSinceLastSuccess")),
                userId = ParseNullableGuid(element.Element("UserId"))
            };
        }

        public static JobCreatedNotificationRegistration ParseNullableJobCreatedNotificationRegistration(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseJobCreatedNotificationRegistration(content);
        }
        internal static PoolFailureNotificationRegistration ParsePoolFailureNotificationRegistration(XElement element)
        {
            return new PoolFailureNotificationRegistration
            {
                creationDate = ParseDateTime(element.Element("CreationDate")),
                format = ParseHttpResponseFormatType(element.Element("Format")),
                id = ParseGuid(element.Element("Id")),
                lastFailure = ParseNullableString(element.Element("LastFailure")),
                lastHttpResponseCode = ParseNullableInt(element.Element("LastHttpResponseCode")),
                lastNotification = ParseNullableDateTime(element.Element("LastNotification")),
                namingConvention = ParseNamingConventionType(element.Element("NamingConvention")),
                notificationEndPoint = ParseNullableString(element.Element("NotificationEndPoint")),
                notificationHttpMethod = ParseRequestType(element.Element("NotificationHttpMethod")),
                numberOfFailuresSinceLastSuccess = ParseInt(element.Element("NumberOfFailuresSinceLastSuccess")),
                userId = ParseNullableGuid(element.Element("UserId"))
            };
        }

        public static PoolFailureNotificationRegistration ParseNullablePoolFailureNotificationRegistration(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParsePoolFailureNotificationRegistration(content);
        }
        internal static S3ObjectCachedNotificationRegistration ParseS3ObjectCachedNotificationRegistration(XElement element)
        {
            return new S3ObjectCachedNotificationRegistration
            {
                creationDate = ParseDateTime(element.Element("CreationDate")),
                format = ParseHttpResponseFormatType(element.Element("Format")),
                id = ParseGuid(element.Element("Id")),
                jobId = ParseNullableGuid(element.Element("JobId")),
                lastFailure = ParseNullableString(element.Element("LastFailure")),
                lastHttpResponseCode = ParseNullableInt(element.Element("LastHttpResponseCode")),
                lastNotification = ParseNullableDateTime(element.Element("LastNotification")),
                namingConvention = ParseNamingConventionType(element.Element("NamingConvention")),
                notificationEndPoint = ParseNullableString(element.Element("NotificationEndPoint")),
                notificationHttpMethod = ParseRequestType(element.Element("NotificationHttpMethod")),
                numberOfFailuresSinceLastSuccess = ParseInt(element.Element("NumberOfFailuresSinceLastSuccess")),
                userId = ParseNullableGuid(element.Element("UserId"))
            };
        }

        public static S3ObjectCachedNotificationRegistration ParseNullableS3ObjectCachedNotificationRegistration(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseS3ObjectCachedNotificationRegistration(content);
        }
        internal static S3ObjectLostNotificationRegistration ParseS3ObjectLostNotificationRegistration(XElement element)
        {
            return new S3ObjectLostNotificationRegistration
            {
                creationDate = ParseDateTime(element.Element("CreationDate")),
                format = ParseHttpResponseFormatType(element.Element("Format")),
                id = ParseGuid(element.Element("Id")),
                lastFailure = ParseNullableString(element.Element("LastFailure")),
                lastHttpResponseCode = ParseNullableInt(element.Element("LastHttpResponseCode")),
                lastNotification = ParseNullableDateTime(element.Element("LastNotification")),
                namingConvention = ParseNamingConventionType(element.Element("NamingConvention")),
                notificationEndPoint = ParseNullableString(element.Element("NotificationEndPoint")),
                notificationHttpMethod = ParseRequestType(element.Element("NotificationHttpMethod")),
                numberOfFailuresSinceLastSuccess = ParseInt(element.Element("NumberOfFailuresSinceLastSuccess")),
                userId = ParseNullableGuid(element.Element("UserId"))
            };
        }

        public static S3ObjectLostNotificationRegistration ParseNullableS3ObjectLostNotificationRegistration(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseS3ObjectLostNotificationRegistration(content);
        }
        internal static S3ObjectPersistedNotificationRegistration ParseS3ObjectPersistedNotificationRegistration(XElement element)
        {
            return new S3ObjectPersistedNotificationRegistration
            {
                creationDate = ParseDateTime(element.Element("CreationDate")),
                format = ParseHttpResponseFormatType(element.Element("Format")),
                id = ParseGuid(element.Element("Id")),
                jobId = ParseNullableGuid(element.Element("JobId")),
                lastFailure = ParseNullableString(element.Element("LastFailure")),
                lastHttpResponseCode = ParseNullableInt(element.Element("LastHttpResponseCode")),
                lastNotification = ParseNullableDateTime(element.Element("LastNotification")),
                namingConvention = ParseNamingConventionType(element.Element("NamingConvention")),
                notificationEndPoint = ParseNullableString(element.Element("NotificationEndPoint")),
                notificationHttpMethod = ParseRequestType(element.Element("NotificationHttpMethod")),
                numberOfFailuresSinceLastSuccess = ParseInt(element.Element("NumberOfFailuresSinceLastSuccess")),
                userId = ParseNullableGuid(element.Element("UserId"))
            };
        }

        public static S3ObjectPersistedNotificationRegistration ParseNullableS3ObjectPersistedNotificationRegistration(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseS3ObjectPersistedNotificationRegistration(content);
        }
        internal static StorageDomainFailureNotificationRegistration ParseStorageDomainFailureNotificationRegistration(XElement element)
        {
            return new StorageDomainFailureNotificationRegistration
            {
                creationDate = ParseDateTime(element.Element("CreationDate")),
                format = ParseHttpResponseFormatType(element.Element("Format")),
                id = ParseGuid(element.Element("Id")),
                lastFailure = ParseNullableString(element.Element("LastFailure")),
                lastHttpResponseCode = ParseNullableInt(element.Element("LastHttpResponseCode")),
                lastNotification = ParseNullableDateTime(element.Element("LastNotification")),
                namingConvention = ParseNamingConventionType(element.Element("NamingConvention")),
                notificationEndPoint = ParseNullableString(element.Element("NotificationEndPoint")),
                notificationHttpMethod = ParseRequestType(element.Element("NotificationHttpMethod")),
                numberOfFailuresSinceLastSuccess = ParseInt(element.Element("NumberOfFailuresSinceLastSuccess")),
                userId = ParseNullableGuid(element.Element("UserId"))
            };
        }

        public static StorageDomainFailureNotificationRegistration ParseNullableStorageDomainFailureNotificationRegistration(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseStorageDomainFailureNotificationRegistration(content);
        }
        internal static SystemFailureNotificationRegistration ParseSystemFailureNotificationRegistration(XElement element)
        {
            return new SystemFailureNotificationRegistration
            {
                creationDate = ParseDateTime(element.Element("CreationDate")),
                format = ParseHttpResponseFormatType(element.Element("Format")),
                id = ParseGuid(element.Element("Id")),
                lastFailure = ParseNullableString(element.Element("LastFailure")),
                lastHttpResponseCode = ParseNullableInt(element.Element("LastHttpResponseCode")),
                lastNotification = ParseNullableDateTime(element.Element("LastNotification")),
                namingConvention = ParseNamingConventionType(element.Element("NamingConvention")),
                notificationEndPoint = ParseNullableString(element.Element("NotificationEndPoint")),
                notificationHttpMethod = ParseRequestType(element.Element("NotificationHttpMethod")),
                numberOfFailuresSinceLastSuccess = ParseInt(element.Element("NumberOfFailuresSinceLastSuccess")),
                userId = ParseNullableGuid(element.Element("UserId"))
            };
        }

        public static SystemFailureNotificationRegistration ParseNullableSystemFailureNotificationRegistration(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseSystemFailureNotificationRegistration(content);
        }
        internal static TapeFailureNotificationRegistration ParseTapeFailureNotificationRegistration(XElement element)
        {
            return new TapeFailureNotificationRegistration
            {
                creationDate = ParseDateTime(element.Element("CreationDate")),
                format = ParseHttpResponseFormatType(element.Element("Format")),
                id = ParseGuid(element.Element("Id")),
                lastFailure = ParseNullableString(element.Element("LastFailure")),
                lastHttpResponseCode = ParseNullableInt(element.Element("LastHttpResponseCode")),
                lastNotification = ParseNullableDateTime(element.Element("LastNotification")),
                namingConvention = ParseNamingConventionType(element.Element("NamingConvention")),
                notificationEndPoint = ParseNullableString(element.Element("NotificationEndPoint")),
                notificationHttpMethod = ParseRequestType(element.Element("NotificationHttpMethod")),
                numberOfFailuresSinceLastSuccess = ParseInt(element.Element("NumberOfFailuresSinceLastSuccess")),
                userId = ParseNullableGuid(element.Element("UserId"))
            };
        }

        public static TapeFailureNotificationRegistration ParseNullableTapeFailureNotificationRegistration(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapeFailureNotificationRegistration(content);
        }
        internal static TapePartitionFailureNotificationRegistration ParseTapePartitionFailureNotificationRegistration(XElement element)
        {
            return new TapePartitionFailureNotificationRegistration
            {
                creationDate = ParseDateTime(element.Element("CreationDate")),
                format = ParseHttpResponseFormatType(element.Element("Format")),
                id = ParseGuid(element.Element("Id")),
                lastFailure = ParseNullableString(element.Element("LastFailure")),
                lastHttpResponseCode = ParseNullableInt(element.Element("LastHttpResponseCode")),
                lastNotification = ParseNullableDateTime(element.Element("LastNotification")),
                namingConvention = ParseNamingConventionType(element.Element("NamingConvention")),
                notificationEndPoint = ParseNullableString(element.Element("NotificationEndPoint")),
                notificationHttpMethod = ParseRequestType(element.Element("NotificationHttpMethod")),
                numberOfFailuresSinceLastSuccess = ParseInt(element.Element("NumberOfFailuresSinceLastSuccess")),
                userId = ParseNullableGuid(element.Element("UserId"))
            };
        }

        public static TapePartitionFailureNotificationRegistration ParseNullableTapePartitionFailureNotificationRegistration(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapePartitionFailureNotificationRegistration(content);
        }
        internal static CacheFilesystem ParseCacheFilesystem(XElement element)
        {
            return new CacheFilesystem
            {
                autoReclaimInitiateThreshold = ParseDouble(element.Element("AutoReclaimInitiateThreshold")),
                autoReclaimTerminateThreshold = ParseDouble(element.Element("AutoReclaimTerminateThreshold")),
                burstThreshold = ParseDouble(element.Element("BurstThreshold")),
                id = ParseGuid(element.Element("Id")),
                maxCapacityInBytes = ParseNullableLong(element.Element("MaxCapacityInBytes")),
                maxPercentUtilizationOfFilesystem = ParseNullableDouble(element.Element("MaxPercentUtilizationOfFilesystem")),
                nodeId = ParseGuid(element.Element("NodeId")),
                path = ParseNullableString(element.Element("Path"))
            };
        }

        public static CacheFilesystem ParseNullableCacheFilesystem(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseCacheFilesystem(content);
        }
        internal static Pool ParsePool(XElement element)
        {
            return new Pool
            {
                assignedToStorageDomain = ParseBool(element.Element("AssignedToStorageDomain")),
                availableCapacity = ParseLong(element.Element("AvailableCapacity")),
                bucketId = ParseNullableGuid(element.Element("BucketId")),
                guid = ParseNullableString(element.Element("Guid")),
                health = ParsePoolHealth(element.Element("Health")),
                id = ParseGuid(element.Element("Id")),
                lastAccessed = ParseNullableDateTime(element.Element("LastAccessed")),
                lastModified = ParseNullableDateTime(element.Element("LastModified")),
                lastVerified = ParseNullableDateTime(element.Element("LastVerified")),
                mountpoint = ParseNullableString(element.Element("Mountpoint")),
                name = ParseNullableString(element.Element("Name")),
                partitionId = ParseNullableGuid(element.Element("PartitionId")),
                poweredOn = ParseBool(element.Element("PoweredOn")),
                quiesced = ParseQuiesced(element.Element("Quiesced")),
                reservedCapacity = ParseLong(element.Element("ReservedCapacity")),
                state = ParsePoolState(element.Element("State")),
                storageDomainId = ParseNullableGuid(element.Element("StorageDomainId")),
                totalCapacity = ParseLong(element.Element("TotalCapacity")),
                type = ParsePoolType(element.Element("Type")),
                usedCapacity = ParseLong(element.Element("UsedCapacity"))
            };
        }

        public static Pool ParseNullablePool(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParsePool(content);
        }
        internal static PoolFailure ParsePoolFailure(XElement element)
        {
            return new PoolFailure
            {
                date = ParseDateTime(element.Element("Date")),
                errorMessage = ParseNullableString(element.Element("ErrorMessage")),
                id = ParseGuid(element.Element("Id")),
                poolId = ParseGuid(element.Element("PoolId")),
                type = ParsePoolFailureType(element.Element("Type"))
            };
        }

        public static PoolFailure ParseNullablePoolFailure(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParsePoolFailure(content);
        }
        internal static PoolPartition ParsePoolPartition(XElement element)
        {
            return new PoolPartition
            {
                id = ParseGuid(element.Element("Id")),
                name = ParseNullableString(element.Element("Name")),
                type = ParsePoolType(element.Element("Type"))
            };
        }

        public static PoolPartition ParseNullablePoolPartition(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParsePoolPartition(content);
        }
        internal static Tape ParseTape(XElement element)
        {
            return new Tape
            {
                assignedToStorageDomain = ParseBool(element.Element("AssignedToStorageDomain")),
                availableRawCapacity = ParseNullableLong(element.Element("AvailableRawCapacity")),
                barCode = ParseNullableString(element.Element("BarCode")),
                bucketId = ParseNullableGuid(element.Element("BucketId")),
                descriptionForIdentification = ParseNullableString(element.Element("DescriptionForIdentification")),
                ejectDate = ParseNullableDateTime(element.Element("EjectDate")),
                ejectLabel = ParseNullableString(element.Element("EjectLabel")),
                ejectLocation = ParseNullableString(element.Element("EjectLocation")),
                ejectPending = ParseNullableDateTime(element.Element("EjectPending")),
                fullOfData = ParseBool(element.Element("FullOfData")),
                id = ParseGuid(element.Element("Id")),
                lastAccessed = ParseNullableDateTime(element.Element("LastAccessed")),
                lastCheckpoint = ParseNullableString(element.Element("LastCheckpoint")),
                lastModified = ParseNullableDateTime(element.Element("LastModified")),
                lastVerified = ParseNullableDateTime(element.Element("LastVerified")),
                partitionId = ParseNullableGuid(element.Element("PartitionId")),
                previousState = ParseNullableTapeState(element.Element("PreviousState")),
                serialNumber = ParseNullableString(element.Element("SerialNumber")),
                state = ParseTapeState(element.Element("State")),
                storageDomainId = ParseNullableGuid(element.Element("StorageDomainId")),
                takeOwnershipPending = ParseBool(element.Element("TakeOwnershipPending")),
                totalRawCapacity = ParseNullableLong(element.Element("TotalRawCapacity")),
                type = ParseTapeType(element.Element("Type")),
                verifyPending = ParseNullablePriority(element.Element("VerifyPending")),
                writeProtected = ParseBool(element.Element("WriteProtected"))
            };
        }

        public static Tape ParseNullableTape(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTape(content);
        }
        internal static TapeDensityDirective ParseTapeDensityDirective(XElement element)
        {
            return new TapeDensityDirective
            {
                density = ParseTapeDriveType(element.Element("Density")),
                id = ParseGuid(element.Element("Id")),
                partitionId = ParseGuid(element.Element("PartitionId")),
                tapeType = ParseTapeType(element.Element("TapeType"))
            };
        }

        public static TapeDensityDirective ParseNullableTapeDensityDirective(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapeDensityDirective(content);
        }
        internal static TapeDrive ParseTapeDrive(XElement element)
        {
            return new TapeDrive
            {
                errorMessage = ParseNullableString(element.Element("ErrorMessage")),
                forceTapeRemoval = ParseBool(element.Element("ForceTapeRemoval")),
                id = ParseGuid(element.Element("Id")),
                lastCleaned = ParseNullableDateTime(element.Element("LastCleaned")),
                partitionId = ParseGuid(element.Element("PartitionId")),
                serialNumber = ParseNullableString(element.Element("SerialNumber")),
                state = ParseTapeDriveState(element.Element("State")),
                tapeId = ParseNullableGuid(element.Element("TapeId")),
                type = ParseTapeDriveType(element.Element("Type"))
            };
        }

        public static TapeDrive ParseNullableTapeDrive(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapeDrive(content);
        }
        internal static DetailedTapeFailure ParseDetailedTapeFailure(XElement element)
        {
            return new DetailedTapeFailure
            {
                date = ParseDateTime(element.Element("Date")),
                errorMessage = ParseNullableString(element.Element("ErrorMessage")),
                id = ParseGuid(element.Element("Id")),
                tapeDriveId = ParseGuid(element.Element("TapeDriveId")),
                tapeId = ParseGuid(element.Element("TapeId")),
                type = ParseTapeFailureType(element.Element("Type"))
            };
        }

        public static DetailedTapeFailure ParseNullableDetailedTapeFailure(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDetailedTapeFailure(content);
        }
        internal static TapeLibrary ParseTapeLibrary(XElement element)
        {
            return new TapeLibrary
            {
                id = ParseGuid(element.Element("Id")),
                managementUrl = ParseNullableString(element.Element("ManagementUrl")),
                name = ParseNullableString(element.Element("Name")),
                serialNumber = ParseNullableString(element.Element("SerialNumber"))
            };
        }

        public static TapeLibrary ParseNullableTapeLibrary(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapeLibrary(content);
        }
        internal static TapePartition ParseTapePartition(XElement element)
        {
            return new TapePartition
            {
                driveType = ParseNullableTapeDriveType(element.Element("DriveType")),
                errorMessage = ParseNullableString(element.Element("ErrorMessage")),
                id = ParseGuid(element.Element("Id")),
                importExportConfiguration = ParseImportExportConfiguration(element.Element("ImportExportConfiguration")),
                libraryId = ParseGuid(element.Element("LibraryId")),
                name = ParseNullableString(element.Element("Name")),
                quiesced = ParseQuiesced(element.Element("Quiesced")),
                serialNumber = ParseNullableString(element.Element("SerialNumber")),
                state = ParseTapePartitionState(element.Element("State"))
            };
        }

        public static TapePartition ParseNullableTapePartition(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapePartition(content);
        }
        internal static TapePartitionFailure ParseTapePartitionFailure(XElement element)
        {
            return new TapePartitionFailure
            {
                date = ParseDateTime(element.Element("Date")),
                errorMessage = ParseNullableString(element.Element("ErrorMessage")),
                id = ParseGuid(element.Element("Id")),
                partitionId = ParseGuid(element.Element("PartitionId")),
                type = ParseTapePartitionFailureType(element.Element("Type"))
            };
        }

        public static TapePartitionFailure ParseNullableTapePartitionFailure(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapePartitionFailure(content);
        }
        internal static BulkObject ParseBulkObject(XElement element)
        {
            return new BulkObject
            {
                id = ParseGuid(element.Element("Id")),
                inCache = ParseNullableBool(element.AttributeText("InCache")),
                latest = ParseBool(element.AttributeText("Latest")),
                length = ParseLong(element.AttributeText("Length")),
                name = ParseNullableString(element.AttributeText("Name")),
                offset = ParseLong(element.AttributeText("Offset")),
                physicalPlacement = ParsePhysicalPlacement(element.Element("PhysicalPlacement")),
                version = ParseLong(element.AttributeText("Version"))
            };
        }

        public static BulkObject ParseNullableBulkObject(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseBulkObject(content);
        }
        internal static BulkObjectList ParseBulkObjectList(XElement element)
        {
            return new BulkObjectList
            {
                objects = element.Elements("object").Select(ParseBulkObject).ToList()
            };
        }

        public static BulkObjectList ParseNullableBulkObjectList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseBulkObjectList(content);
        }
        internal static BuildInformation ParseBuildInformation(XElement element)
        {
            return new BuildInformation
            {
                branch = ParseNullableString(element.Element("Branch")),
                revision = ParseNullableString(element.Element("Revision")),
                version = ParseNullableString(element.Element("Version"))
            };
        }

        public static BuildInformation ParseNullableBuildInformation(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseBuildInformation(content);
        }
        internal static GenericDaoNotificationPayload ParseGenericDaoNotificationPayload(XElement element)
        {
            return new GenericDaoNotificationPayload
            {
                daoType = ParseNullableString(element.Element("DaoType")),
                ids = element.Elements("Ids").Select(ParseGuid).ToList(),
                notificationGenerationDate = ParseDateTime(element.Element("NotificationGenerationDate")),
                sqlOperation = ParseSqlOperation(element.Element("SqlOperation"))
            };
        }

        public static GenericDaoNotificationPayload ParseNullableGenericDaoNotificationPayload(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseGenericDaoNotificationPayload(content);
        }
        internal static JobCompletedNotificationPayload ParseJobCompletedNotificationPayload(XElement element)
        {
            return new JobCompletedNotificationPayload
            {
                cancelOccurred = ParseBool(element.Element("CancelOccurred")),
                jobId = ParseGuid(element.Element("JobId")),
                notificationGenerationDate = ParseDateTime(element.Element("NotificationGenerationDate")),
                objectsNotPersisted = element.Element("ObjectsNotPersisted").Elements("Object").Select(ParseBulkObject).ToList()
            };
        }

        public static JobCompletedNotificationPayload ParseNullableJobCompletedNotificationPayload(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseJobCompletedNotificationPayload(content);
        }
        internal static JobCreatedNotificationPayload ParseJobCreatedNotificationPayload(XElement element)
        {
            return new JobCreatedNotificationPayload
            {
                jobId = ParseGuid(element.Element("JobId")),
                notificationGenerationDate = ParseDateTime(element.Element("NotificationGenerationDate"))
            };
        }

        public static JobCreatedNotificationPayload ParseNullableJobCreatedNotificationPayload(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseJobCreatedNotificationPayload(content);
        }
        internal static PoolFailureNotificationPayload ParsePoolFailureNotificationPayload(XElement element)
        {
            return new PoolFailureNotificationPayload
            {
                date = ParseDateTime(element.Element("Date")),
                errorMessage = ParseNullableString(element.Element("ErrorMessage")),
                notificationGenerationDate = ParseDateTime(element.Element("NotificationGenerationDate")),
                poolId = ParseGuid(element.Element("PoolId")),
                type = ParsePoolFailureType(element.Element("Type"))
            };
        }

        public static PoolFailureNotificationPayload ParseNullablePoolFailureNotificationPayload(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParsePoolFailureNotificationPayload(content);
        }
        internal static S3ObjectsCachedNotificationPayload ParseS3ObjectsCachedNotificationPayload(XElement element)
        {
            return new S3ObjectsCachedNotificationPayload
            {
                jobId = ParseGuid(element.Element("JobId")),
                notificationGenerationDate = ParseDateTime(element.Element("NotificationGenerationDate")),
                objects = element.Element("Objects").Elements("Object").Select(ParseBulkObject).ToList()
            };
        }

        public static S3ObjectsCachedNotificationPayload ParseNullableS3ObjectsCachedNotificationPayload(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseS3ObjectsCachedNotificationPayload(content);
        }
        internal static S3ObjectsLostNotificationPayload ParseS3ObjectsLostNotificationPayload(XElement element)
        {
            return new S3ObjectsLostNotificationPayload
            {
                notificationGenerationDate = ParseDateTime(element.Element("NotificationGenerationDate")),
                objects = element.Element("Objects").Elements("Object").Select(ParseBulkObject).ToList()
            };
        }

        public static S3ObjectsLostNotificationPayload ParseNullableS3ObjectsLostNotificationPayload(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseS3ObjectsLostNotificationPayload(content);
        }
        internal static S3ObjectsPersistedNotificationPayload ParseS3ObjectsPersistedNotificationPayload(XElement element)
        {
            return new S3ObjectsPersistedNotificationPayload
            {
                jobId = ParseGuid(element.Element("JobId")),
                notificationGenerationDate = ParseDateTime(element.Element("NotificationGenerationDate")),
                objects = element.Element("Objects").Elements("Object").Select(ParseBulkObject).ToList()
            };
        }

        public static S3ObjectsPersistedNotificationPayload ParseNullableS3ObjectsPersistedNotificationPayload(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseS3ObjectsPersistedNotificationPayload(content);
        }
        internal static StorageDomainFailureNotificationPayload ParseStorageDomainFailureNotificationPayload(XElement element)
        {
            return new StorageDomainFailureNotificationPayload
            {
                date = ParseDateTime(element.Element("Date")),
                errorMessage = ParseNullableString(element.Element("ErrorMessage")),
                notificationGenerationDate = ParseDateTime(element.Element("NotificationGenerationDate")),
                storageDomainId = ParseGuid(element.Element("StorageDomainId")),
                type = ParseStorageDomainFailureType(element.Element("Type"))
            };
        }

        public static StorageDomainFailureNotificationPayload ParseNullableStorageDomainFailureNotificationPayload(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseStorageDomainFailureNotificationPayload(content);
        }
        internal static SystemFailureNotificationPayload ParseSystemFailureNotificationPayload(XElement element)
        {
            return new SystemFailureNotificationPayload
            {
                date = ParseDateTime(element.Element("Date")),
                errorMessage = ParseNullableString(element.Element("ErrorMessage")),
                notificationGenerationDate = ParseDateTime(element.Element("NotificationGenerationDate")),
                type = ParseSystemFailureType(element.Element("Type"))
            };
        }

        public static SystemFailureNotificationPayload ParseNullableSystemFailureNotificationPayload(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseSystemFailureNotificationPayload(content);
        }
        internal static TapeFailureNotificationPayload ParseTapeFailureNotificationPayload(XElement element)
        {
            return new TapeFailureNotificationPayload
            {
                date = ParseDateTime(element.Element("Date")),
                errorMessage = ParseNullableString(element.Element("ErrorMessage")),
                notificationGenerationDate = ParseDateTime(element.Element("NotificationGenerationDate")),
                tapeDriveId = ParseGuid(element.Element("TapeDriveId")),
                tapeId = ParseGuid(element.Element("TapeId")),
                type = ParseTapeFailureType(element.Element("Type"))
            };
        }

        public static TapeFailureNotificationPayload ParseNullableTapeFailureNotificationPayload(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapeFailureNotificationPayload(content);
        }
        internal static TapePartitionFailureNotificationPayload ParseTapePartitionFailureNotificationPayload(XElement element)
        {
            return new TapePartitionFailureNotificationPayload
            {
                date = ParseDateTime(element.Element("Date")),
                errorMessage = ParseNullableString(element.Element("ErrorMessage")),
                notificationGenerationDate = ParseDateTime(element.Element("NotificationGenerationDate")),
                partitionId = ParseGuid(element.Element("PartitionId")),
                type = ParseTapePartitionFailureType(element.Element("Type"))
            };
        }

        public static TapePartitionFailureNotificationPayload ParseNullableTapePartitionFailureNotificationPayload(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapePartitionFailureNotificationPayload(content);
        }
        internal static BlobStoreTaskInformation ParseBlobStoreTaskInformation(XElement element)
        {
            return new BlobStoreTaskInformation
            {
                dateScheduled = ParseDateTime(element.Element("DateScheduled")),
                dateStarted = ParseNullableDateTime(element.Element("DateStarted")),
                description = ParseNullableString(element.Element("Description")),
                driveId = ParseNullableGuid(element.Element("DriveId")),
                durationInProgress = ParseNullableDuration(element.Element("DurationInProgress")),
                durationScheduled = ParseNullableDuration(element.Element("DurationScheduled")),
                id = ParseLong(element.Element("Id")),
                name = ParseNullableString(element.Element("Name")),
                poolId = ParseNullableGuid(element.Element("PoolId")),
                priority = ParsePriority(element.Element("Priority")),
                state = ParseBlobStoreTaskState(element.Element("State")),
                tapeId = ParseNullableGuid(element.Element("TapeId"))
            };
        }

        public static BlobStoreTaskInformation ParseNullableBlobStoreTaskInformation(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseBlobStoreTaskInformation(content);
        }
        internal static BlobStoreTasksInformation ParseBlobStoreTasksInformation(XElement element)
        {
            return new BlobStoreTasksInformation
            {
                tasks = element.Elements("Tasks").Select(ParseBlobStoreTaskInformation).ToList()
            };
        }

        public static BlobStoreTasksInformation ParseNullableBlobStoreTasksInformation(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseBlobStoreTasksInformation(content);
        }
        internal static CacheEntryInformation ParseCacheEntryInformation(XElement element)
        {
            return new CacheEntryInformation
            {
                blob = ParseNullableBlob(element.Element("Blob")),
                state = ParseCacheEntryState(element.Element("State"))
            };
        }

        public static CacheEntryInformation ParseNullableCacheEntryInformation(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseCacheEntryInformation(content);
        }
        internal static CacheFilesystemInformation ParseCacheFilesystemInformation(XElement element)
        {
            return new CacheFilesystemInformation
            {
                availableCapacityInBytes = ParseLong(element.Element("AvailableCapacityInBytes")),
                cacheFilesystem = ParseCacheFilesystem(element.Element("CacheFilesystem")),
                entries = element.Elements("Entries").Select(ParseCacheEntryInformation).ToList(),
                summary = ParseNullableString(element.Element("Summary")),
                totalCapacityInBytes = ParseLong(element.Element("TotalCapacityInBytes")),
                unavailableCapacityInBytes = ParseLong(element.Element("UnavailableCapacityInBytes")),
                usedCapacityInBytes = ParseLong(element.Element("UsedCapacityInBytes"))
            };
        }

        public static CacheFilesystemInformation ParseNullableCacheFilesystemInformation(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseCacheFilesystemInformation(content);
        }
        internal static CacheInformation ParseCacheInformation(XElement element)
        {
            return new CacheInformation
            {
                filesystems = element.Elements("Filesystems").Select(ParseCacheFilesystemInformation).ToList()
            };
        }

        public static CacheInformation ParseNullableCacheInformation(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseCacheInformation(content);
        }
        internal static Ds3Bucket ParseDs3Bucket(XElement element)
        {
            return new Ds3Bucket
            {
                creationDate = ParseDateTime(element.Element("CreationDate")),
                name = ParseNullableString(element.Element("Name"))
            };
        }

        public static Ds3Bucket ParseNullableDs3Bucket(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDs3Bucket(content);
        }
        internal static ListBucketResult ParseListBucketResult(XElement element)
        {
            return new ListBucketResult
            {
                commonPrefixes = element.Element("CommonPrefixes").Elements("Prefix").Select(ParseString).ToList(),
                creationDate = ParseDateTime(element.Element("CreationDate")),
                delimiter = ParseNullableString(element.Element("Delimiter")),
                marker = ParseNullableString(element.Element("Marker")),
                maxKeys = ParseInt(element.Element("MaxKeys")),
                name = ParseNullableString(element.Element("Name")),
                nextMarker = ParseNullableString(element.Element("NextMarker")),
                objects = element.Elements("Contents").Select(ParseContents).ToList(),
                prefix = ParseNullableString(element.Element("Prefix")),
                truncated = ParseBool(element.Element("IsTruncated"))
            };
        }

        public static ListBucketResult ParseNullableListBucketResult(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseListBucketResult(content);
        }
        internal static ListAllMyBucketsResult ParseListAllMyBucketsResult(XElement element)
        {
            return new ListAllMyBucketsResult
            {
                buckets = element.Element("Buckets").Elements("Bucket").Select(ParseDs3Bucket).ToList(),
                owner = ParseUser(element.Element("Owner"))
            };
        }

        public static ListAllMyBucketsResult ParseNullableListAllMyBucketsResult(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseListAllMyBucketsResult(content);
        }
        internal static CompleteMultipartUploadResult ParseCompleteMultipartUploadResult(XElement element)
        {
            return new CompleteMultipartUploadResult
            {
                bucket = ParseNullableString(element.Element("Bucket")),
                eTag = ParseNullableString(element.Element("ETag")),
                key = ParseNullableString(element.Element("Key")),
                location = ParseNullableString(element.Element("Location"))
            };
        }

        public static CompleteMultipartUploadResult ParseNullableCompleteMultipartUploadResult(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseCompleteMultipartUploadResult(content);
        }
        internal static DeleteObjectError ParseDeleteObjectError(XElement element)
        {
            return new DeleteObjectError
            {
                code = ParseNullableString(element.Element("Code")),
                key = ParseNullableString(element.Element("Key")),
                message = ParseNullableString(element.Element("Message"))
            };
        }

        public static DeleteObjectError ParseNullableDeleteObjectError(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDeleteObjectError(content);
        }
        internal static DeleteResult ParseDeleteResult(XElement element)
        {
            return new DeleteResult
            {
                deletedObjects = element.Elements("Deleted").Select(ParseS3ObjectToDelete).ToList(),
                errors = element.Elements("Error").Select(ParseDeleteObjectError).ToList()
            };
        }

        public static DeleteResult ParseNullableDeleteResult(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDeleteResult(content);
        }
        internal static DetailedTape ParseDetailedTape(XElement element)
        {
            return new DetailedTape
            {
                assignedToStorageDomain = ParseBool(element.Element("AssignedToStorageDomain")),
                availableRawCapacity = ParseNullableLong(element.Element("AvailableRawCapacity")),
                barCode = ParseNullableString(element.Element("BarCode")),
                bucketId = ParseNullableGuid(element.Element("BucketId")),
                descriptionForIdentification = ParseNullableString(element.Element("DescriptionForIdentification")),
                ejectDate = ParseNullableDateTime(element.Element("EjectDate")),
                ejectLabel = ParseNullableString(element.Element("EjectLabel")),
                ejectLocation = ParseNullableString(element.Element("EjectLocation")),
                ejectPending = ParseNullableDateTime(element.Element("EjectPending")),
                fullOfData = ParseBool(element.Element("FullOfData")),
                id = ParseGuid(element.Element("Id")),
                lastAccessed = ParseNullableDateTime(element.Element("LastAccessed")),
                lastCheckpoint = ParseNullableString(element.Element("LastCheckpoint")),
                lastModified = ParseNullableDateTime(element.Element("LastModified")),
                lastVerified = ParseNullableDateTime(element.Element("LastVerified")),
                mostRecentFailure = ParseDetailedTapeFailure(element.Element("MostRecentFailure")),
                partitionId = ParseNullableGuid(element.Element("PartitionId")),
                previousState = ParseNullableTapeState(element.Element("PreviousState")),
                serialNumber = ParseNullableString(element.Element("SerialNumber")),
                state = ParseTapeState(element.Element("State")),
                storageDomainId = ParseNullableGuid(element.Element("StorageDomainId")),
                takeOwnershipPending = ParseBool(element.Element("TakeOwnershipPending")),
                totalRawCapacity = ParseNullableLong(element.Element("TotalRawCapacity")),
                type = ParseTapeType(element.Element("Type")),
                verifyPending = ParseNullablePriority(element.Element("VerifyPending")),
                writeProtected = ParseBool(element.Element("WriteProtected"))
            };
        }

        public static DetailedTape ParseNullableDetailedTape(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDetailedTape(content);
        }
        internal static DetailedTapePartition ParseDetailedTapePartition(XElement element)
        {
            return new DetailedTapePartition
            {
                driveType = ParseNullableTapeDriveType(element.Element("DriveType")),
                driveTypes = element.Elements("DriveTypes").Select(ParseTapeDriveType).ToList(),
                errorMessage = ParseNullableString(element.Element("ErrorMessage")),
                id = ParseGuid(element.Element("Id")),
                importExportConfiguration = ParseImportExportConfiguration(element.Element("ImportExportConfiguration")),
                libraryId = ParseGuid(element.Element("LibraryId")),
                name = ParseNullableString(element.Element("Name")),
                quiesced = ParseQuiesced(element.Element("Quiesced")),
                serialNumber = ParseNullableString(element.Element("SerialNumber")),
                state = ParseTapePartitionState(element.Element("State")),
                tapeTypes = element.Elements("TapeTypes").Select(ParseTapeType).ToList()
            };
        }

        public static DetailedTapePartition ParseNullableDetailedTapePartition(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDetailedTapePartition(content);
        }
        internal static Error ParseError(XElement element)
        {
            return new Error
            {
                code = ParseNullableString(element.Element("Code")),
                httpErrorCode = ParseInt(element.Element("HttpErrorCode")),
                message = ParseNullableString(element.Element("Message")),
                resource = ParseNullableString(element.Element("Resource")),
                resourceId = ParseLong(element.Element("ResourceId"))
            };
        }

        public static Error ParseNullableError(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseError(content);
        }
        internal static InitiateMultipartUploadResult ParseInitiateMultipartUploadResult(XElement element)
        {
            return new InitiateMultipartUploadResult
            {
                bucket = ParseNullableString(element.Element("Bucket")),
                key = ParseNullableString(element.Element("Key")),
                uploadId = ParseNullableString(element.Element("UploadId"))
            };
        }

        public static InitiateMultipartUploadResult ParseNullableInitiateMultipartUploadResult(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseInitiateMultipartUploadResult(content);
        }
        internal static Job ParseJob(XElement element)
        {
            return new Job
            {
                aggregating = ParseBool(element.AttributeText("Aggregating")),
                bucketName = ParseNullableString(element.AttributeText("BucketName")),
                cachedSizeInBytes = ParseLong(element.AttributeText("CachedSizeInBytes")),
                chunkClientProcessingOrderGuarantee = ParseJobChunkClientProcessingOrderGuarantee(element.AttributeText("ChunkClientProcessingOrderGuarantee")),
                completedSizeInBytes = ParseLong(element.AttributeText("CompletedSizeInBytes")),
                jobId = ParseGuid(element.AttributeText("JobId")),
                naked = ParseBool(element.AttributeText("Naked")),
                name = ParseNullableString(element.AttributeText("Name")),
                nodes = element.Element("Nodes").Elements("Node").Select(ParseDs3Node).ToList(),
                originalSizeInBytes = ParseLong(element.AttributeText("OriginalSizeInBytes")),
                priority = ParsePriority(element.AttributeText("Priority")),
                requestType = ParseJobRequestType(element.AttributeText("RequestType")),
                startDate = ParseDateTime(element.AttributeText("StartDate")),
                status = ParseJobStatus(element.AttributeText("Status")),
                userId = ParseGuid(element.AttributeText("UserId")),
                userName = ParseNullableString(element.AttributeText("UserName")),
                writeOptimization = ParseWriteOptimization(element.AttributeText("WriteOptimization"))
            };
        }

        public static Job ParseNullableJob(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseJob(content);
        }
        internal static Objects ParseObjects(XElement element)
        {
            return new Objects
            {
                chunkId = ParseGuid(element.AttributeText("ChunkId")),
                chunkNumber = ParseInt(element.AttributeText("ChunkNumber")),
                nodeId = ParseNullableGuid(element.AttributeText("NodeId")),
                objects = element.Elements("object").Select(ParseBulkObject).ToList()
            };
        }

        public static Objects ParseNullableObjects(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseObjects(content);
        }
        internal static MasterObjectList ParseMasterObjectList(XElement element)
        {
            return new MasterObjectList
            {
                aggregating = ParseBool(element.AttributeText("Aggregating")),
                bucketName = ParseNullableString(element.AttributeText("BucketName")),
                cachedSizeInBytes = ParseLong(element.AttributeText("CachedSizeInBytes")),
                chunkClientProcessingOrderGuarantee = ParseJobChunkClientProcessingOrderGuarantee(element.AttributeText("ChunkClientProcessingOrderGuarantee")),
                completedSizeInBytes = ParseLong(element.AttributeText("CompletedSizeInBytes")),
                jobId = ParseGuid(element.AttributeText("JobId")),
                naked = ParseBool(element.AttributeText("Naked")),
                name = ParseNullableString(element.AttributeText("Name")),
                nodes = element.Element("Nodes").Elements("Node").Select(ParseDs3Node).ToList(),
                objects = element.Elements("Objects").Select(ParseObjects).ToList(),
                originalSizeInBytes = ParseLong(element.AttributeText("OriginalSizeInBytes")),
                priority = ParsePriority(element.AttributeText("Priority")),
                requestType = ParseJobRequestType(element.AttributeText("RequestType")),
                startDate = ParseDateTime(element.AttributeText("StartDate")),
                status = ParseJobStatus(element.AttributeText("Status")),
                userId = ParseGuid(element.AttributeText("UserId")),
                userName = ParseNullableString(element.AttributeText("UserName")),
                writeOptimization = ParseWriteOptimization(element.AttributeText("WriteOptimization"))
            };
        }

        public static MasterObjectList ParseNullableMasterObjectList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseMasterObjectList(content);
        }
        internal static JobList ParseJobList(XElement element)
        {
            return new JobList
            {
                jobs = element.Element("Jobs").Elements("Job").Select(ParseJob).ToList()
            };
        }

        public static JobList ParseNullableJobList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseJobList(content);
        }
        internal static ListPartsResult ParseListPartsResult(XElement element)
        {
            return new ListPartsResult
            {
                bucket = ParseNullableString(element.Element("Bucket")),
                key = ParseNullableString(element.Element("Key")),
                maxParts = ParseInt(element.Element("MaxParts")),
                nextPartNumberMarker = ParseInt(element.Element("NextPartNumberMarker")),
                owner = ParseUser(element.Element("Owner")),
                partNumberMarker = ParseNullableInt(element.Element("PartNumberMarker")),
                parts = element.Elements("Part").Select(ParseMultiPartUploadPart).ToList(),
                truncated = ParseBool(element.Element("IsTruncated")),
                uploadId = ParseGuid(element.Element("UploadId"))
            };
        }

        public static ListPartsResult ParseNullableListPartsResult(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseListPartsResult(content);
        }
        internal static ListMultiPartUploadsResult ParseListMultiPartUploadsResult(XElement element)
        {
            return new ListMultiPartUploadsResult
            {
                bucket = ParseNullableString(element.Element("Bucket")),
                commonPrefixes = element.Element("CommonPrefixes").Elements("Prefix").Select(ParseString).ToList(),
                delimiter = ParseNullableString(element.Element("Delimiter")),
                keyMarker = ParseNullableString(element.Element("KeyMarker")),
                maxUploads = ParseInt(element.Element("MaxUploads")),
                nextKeyMarker = ParseNullableString(element.Element("NextKeyMarker")),
                nextUploadIdMarker = ParseNullableString(element.Element("NextUploadIdMarker")),
                prefix = ParseNullableString(element.Element("Prefix")),
                truncated = ParseBool(element.Element("IsTruncated")),
                uploadIdMarker = ParseNullableString(element.Element("UploadIdMarker")),
                uploads = element.Elements("Upload").Select(ParseMultiPartUpload).ToList()
            };
        }

        public static ListMultiPartUploadsResult ParseNullableListMultiPartUploadsResult(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseListMultiPartUploadsResult(content);
        }
        internal static MultiPartUpload ParseMultiPartUpload(XElement element)
        {
            return new MultiPartUpload
            {
                initiated = ParseDateTime(element.Element("Initiated")),
                key = ParseNullableString(element.Element("Key")),
                owner = ParseUser(element.Element("Owner")),
                uploadId = ParseGuid(element.Element("UploadId"))
            };
        }

        public static MultiPartUpload ParseNullableMultiPartUpload(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseMultiPartUpload(content);
        }
        internal static MultiPartUploadPart ParseMultiPartUploadPart(XElement element)
        {
            return new MultiPartUploadPart
            {
                eTag = ParseNullableString(element.Element("ETag")),
                lastModified = ParseDateTime(element.Element("LastModified")),
                partNumber = ParseInt(element.Element("PartNumber"))
            };
        }

        public static MultiPartUploadPart ParseNullableMultiPartUploadPart(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseMultiPartUploadPart(content);
        }
        internal static Ds3Node ParseDs3Node(XElement element)
        {
            return new Ds3Node
            {
                endPoint = ParseNullableString(element.AttributeText("EndPoint")),
                httpPort = ParseNullableInt(element.AttributeText("HttpPort")),
                httpsPort = ParseNullableInt(element.AttributeText("HttpsPort")),
                id = ParseGuid(element.AttributeText("Id"))
            };
        }

        public static Ds3Node ParseNullableDs3Node(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDs3Node(content);
        }
        internal static Contents ParseContents(XElement element)
        {
            return new Contents
            {
                eTag = ParseNullableString(element.Element("ETag")),
                key = ParseNullableString(element.Element("Key")),
                lastModified = ParseDateTime(element.Element("LastModified")),
                owner = ParseUser(element.Element("Owner")),
                size = ParseLong(element.Element("Size")),
                storageClass = ParseObject(element.Element("StorageClass"))
            };
        }

        public static Contents ParseNullableContents(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseContents(content);
        }
        internal static S3ObjectToDelete ParseS3ObjectToDelete(XElement element)
        {
            return new S3ObjectToDelete
            {
                key = ParseNullableString(element.Element("Key"))
            };
        }

        public static S3ObjectToDelete ParseNullableS3ObjectToDelete(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseS3ObjectToDelete(content);
        }
        internal static User ParseUser(XElement element)
        {
            return new User
            {
                displayName = ParseNullableString(element.Element("DisplayName")),
                id = ParseGuid(element.Element("iD"))
            };
        }

        public static User ParseNullableUser(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseUser(content);
        }
        internal static DetailedS3Object ParseDetailedS3Object(XElement element)
        {
            return new DetailedS3Object
            {
                blobs = ParseBulkObjectList(element.Element("Blobs")),
                blobsBeingPersisted = ParseNullableInt(element.Element("BlobsBeingPersisted")),
                blobsDegraded = ParseNullableInt(element.Element("BlobsDegraded")),
                blobsInCache = ParseNullableInt(element.Element("BlobsInCache")),
                blobsTotal = ParseNullableInt(element.Element("BlobsTotal")),
                bucketId = ParseGuid(element.Element("BucketId")),
                creationDate = ParseNullableDateTime(element.Element("CreationDate")),
                eTag = ParseNullableString(element.Element("ETag")),
                id = ParseGuid(element.Element("Id")),
                latest = ParseBool(element.Element("Latest")),
                name = ParseNullableString(element.Element("Name")),
                owner = ParseNullableString(element.Element("Owner")),
                size = ParseLong(element.Element("Size")),
                type = ParseS3ObjectType(element.Element("Type")),
                version = ParseLong(element.Element("Version"))
            };
        }

        public static DetailedS3Object ParseNullableDetailedS3Object(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDetailedS3Object(content);
        }
        internal static SystemInformation ParseSystemInformation(XElement element)
        {
            return new SystemInformation
            {
                apiVersion = ParseNullableString(element.Element("ApiVersion")),
                backendActivated = ParseBool(element.Element("BackendActivated")),
                buildInformation = ParseBuildInformation(element.Element("BuildInformation")),
                serialNumber = ParseNullableString(element.Element("SerialNumber"))
            };
        }

        public static SystemInformation ParseNullableSystemInformation(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseSystemInformation(content);
        }
        internal static HealthVerificationResult ParseHealthVerificationResult(XElement element)
        {
            return new HealthVerificationResult
            {
                databaseFilesystemFreeSpace = ParseDatabasePhysicalSpaceState(element.Element("DatabaseFilesystemFreeSpace")),
                msRequiredToVerifyDataPlannerHealth = ParseLong(element.Element("MsRequiredToVerifyDataPlannerHealth"))
            };
        }

        public static HealthVerificationResult ParseNullableHealthVerificationResult(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseHealthVerificationResult(content);
        }
        internal static NamedDetailedTapePartition ParseNamedDetailedTapePartition(XElement element)
        {
            return new NamedDetailedTapePartition
            {
                driveType = ParseNullableTapeDriveType(element.Element("DriveType")),
                driveTypes = element.Elements("DriveTypes").Select(ParseTapeDriveType).ToList(),
                errorMessage = ParseNullableString(element.Element("ErrorMessage")),
                id = ParseGuid(element.Element("Id")),
                importExportConfiguration = ParseImportExportConfiguration(element.Element("ImportExportConfiguration")),
                libraryId = ParseGuid(element.Element("LibraryId")),
                name = ParseNullableString(element.Element("Name")),
                quiesced = ParseQuiesced(element.Element("Quiesced")),
                serialNumber = ParseNullableString(element.Element("SerialNumber")),
                state = ParseTapePartitionState(element.Element("State")),
                tapeTypes = element.Elements("TapeTypes").Select(ParseTapeType).ToList()
            };
        }

        public static NamedDetailedTapePartition ParseNullableNamedDetailedTapePartition(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseNamedDetailedTapePartition(content);
        }
        internal static NamedDetailedTape ParseNamedDetailedTape(XElement element)
        {
            return new NamedDetailedTape
            {
                assignedToStorageDomain = ParseBool(element.Element("AssignedToStorageDomain")),
                availableRawCapacity = ParseNullableLong(element.Element("AvailableRawCapacity")),
                barCode = ParseNullableString(element.Element("BarCode")),
                bucketId = ParseNullableGuid(element.Element("BucketId")),
                descriptionForIdentification = ParseNullableString(element.Element("DescriptionForIdentification")),
                ejectDate = ParseNullableDateTime(element.Element("EjectDate")),
                ejectLabel = ParseNullableString(element.Element("EjectLabel")),
                ejectLocation = ParseNullableString(element.Element("EjectLocation")),
                ejectPending = ParseNullableDateTime(element.Element("EjectPending")),
                fullOfData = ParseBool(element.Element("FullOfData")),
                id = ParseGuid(element.Element("Id")),
                lastAccessed = ParseNullableDateTime(element.Element("LastAccessed")),
                lastCheckpoint = ParseNullableString(element.Element("LastCheckpoint")),
                lastModified = ParseNullableDateTime(element.Element("LastModified")),
                lastVerified = ParseNullableDateTime(element.Element("LastVerified")),
                mostRecentFailure = ParseDetailedTapeFailure(element.Element("MostRecentFailure")),
                partitionId = ParseNullableGuid(element.Element("PartitionId")),
                previousState = ParseNullableTapeState(element.Element("PreviousState")),
                serialNumber = ParseNullableString(element.Element("SerialNumber")),
                state = ParseTapeState(element.Element("State")),
                storageDomainId = ParseNullableGuid(element.Element("StorageDomainId")),
                takeOwnershipPending = ParseBool(element.Element("TakeOwnershipPending")),
                totalRawCapacity = ParseNullableLong(element.Element("TotalRawCapacity")),
                type = ParseTapeType(element.Element("Type")),
                verifyPending = ParseNullablePriority(element.Element("VerifyPending")),
                writeProtected = ParseBool(element.Element("WriteProtected"))
            };
        }

        public static NamedDetailedTape ParseNullableNamedDetailedTape(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseNamedDetailedTape(content);
        }
        internal static TapeFailure ParseTapeFailure(XElement element)
        {
            return new TapeFailure
            {
                cause = ParseNullableString(element.Element("Cause")),
                tape = ParseTape(element.Element("Tape"))
            };
        }

        public static TapeFailure ParseNullableTapeFailure(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapeFailure(content);
        }
        internal static TapeFailureList ParseTapeFailureList(XElement element)
        {
            return new TapeFailureList
            {
                failures = element.Elements("failure").Select(ParseTapeFailure).ToList()
            };
        }

        public static TapeFailureList ParseNullableTapeFailureList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapeFailureList(content);
        }
        internal static CreateHeapDumpParams ParseCreateHeapDumpParams(XElement element)
        {
            return new CreateHeapDumpParams
            {
                application = ParseApplication(element.Element("Application")),
                path = ParseNullableString(element.Element("Path"))
            };
        }

        public static CreateHeapDumpParams ParseNullableCreateHeapDumpParams(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseCreateHeapDumpParams(content);
        }
        internal static DatabaseContents ParseDatabaseContents(XElement element)
        {
            return new DatabaseContents
            {
                types = element.Elements("Types").Select(ParseType).ToList()
            };
        }

        public static DatabaseContents ParseNullableDatabaseContents(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDatabaseContents(content);
        }
        internal static Type ParseType(XElement element)
        {
            return new Type
            {
                beansRetrieverName = ParseNullableString(element.Element("BeansRetrieverName")),
                domainName = ParseNullableString(element.Element("DomainName")),
                numberOfType = ParseNullableInt(element.Element("NumberOfType"))
            };
        }

        public static Type ParseNullableType(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseType(content);
        }
        internal static Duration ParseDuration(XElement element)
        {
            return new Duration
            {
                elapsedMillis = ParseLong(element.Element("ElapsedMillis")),
                elapsedMinutes = ParseInt(element.Element("ElapsedMinutes")),
                elapsedNanos = ParseLong(element.Element("ElapsedNanos")),
                elapsedSeconds = ParseInt(element.Element("ElapsedSeconds"))
            };
        }

        public static Duration ParseNullableDuration(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDuration(content);
        }
        internal static BucketAclList ParseBucketAclList(XElement element)
        {
            return new BucketAclList
            {
                bucketAcls = element.Elements("BucketAcl").Select(ParseBucketAcl).ToList()
            };
        }

        public static BucketAclList ParseNullableBucketAclList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseBucketAclList(content);
        }
        internal static DataPolicyAclList ParseDataPolicyAclList(XElement element)
        {
            return new DataPolicyAclList
            {
                dataPolicyAcls = element.Elements("DataPolicyAcl").Select(ParseDataPolicyAcl).ToList()
            };
        }

        public static DataPolicyAclList ParseNullableDataPolicyAclList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDataPolicyAclList(content);
        }
        internal static BucketList ParseBucketList(XElement element)
        {
            return new BucketList
            {
                buckets = element.Elements("Bucket").Select(ParseBucket).ToList()
            };
        }

        public static BucketList ParseNullableBucketList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseBucketList(content);
        }
        internal static CacheFilesystemList ParseCacheFilesystemList(XElement element)
        {
            return new CacheFilesystemList
            {
                cacheFilesystems = element.Elements("CacheFilesystem").Select(ParseCacheFilesystem).ToList()
            };
        }

        public static CacheFilesystemList ParseNullableCacheFilesystemList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseCacheFilesystemList(content);
        }
        internal static DataPersistenceRuleList ParseDataPersistenceRuleList(XElement element)
        {
            return new DataPersistenceRuleList
            {
                dataPersistenceRules = element.Elements("DataPersistenceRule").Select(ParseDataPersistenceRule).ToList()
            };
        }

        public static DataPersistenceRuleList ParseNullableDataPersistenceRuleList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDataPersistenceRuleList(content);
        }
        internal static DataPolicyList ParseDataPolicyList(XElement element)
        {
            return new DataPolicyList
            {
                dataPolicies = element.Elements("DataPolicy").Select(ParseDataPolicy).ToList()
            };
        }

        public static DataPolicyList ParseNullableDataPolicyList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDataPolicyList(content);
        }
        internal static GroupMemberList ParseGroupMemberList(XElement element)
        {
            return new GroupMemberList
            {
                groupMembers = element.Elements("GroupMember").Select(ParseGroupMember).ToList()
            };
        }

        public static GroupMemberList ParseNullableGroupMemberList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseGroupMemberList(content);
        }
        internal static GroupList ParseGroupList(XElement element)
        {
            return new GroupList
            {
                groups = element.Elements("Group").Select(ParseGroup).ToList()
            };
        }

        public static GroupList ParseNullableGroupList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseGroupList(content);
        }
        internal static ActiveJobList ParseActiveJobList(XElement element)
        {
            return new ActiveJobList
            {
                activeJobs = element.Elements("Job").Select(ParseActiveJob).ToList()
            };
        }

        public static ActiveJobList ParseNullableActiveJobList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseActiveJobList(content);
        }
        internal static CanceledJobList ParseCanceledJobList(XElement element)
        {
            return new CanceledJobList
            {
                canceledJobs = element.Elements("CanceledJob").Select(ParseCanceledJob).ToList()
            };
        }

        public static CanceledJobList ParseNullableCanceledJobList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseCanceledJobList(content);
        }
        internal static CompletedJobList ParseCompletedJobList(XElement element)
        {
            return new CompletedJobList
            {
                completedJobs = element.Elements("CompletedJob").Select(ParseCompletedJob).ToList()
            };
        }

        public static CompletedJobList ParseNullableCompletedJobList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseCompletedJobList(content);
        }
        internal static NodeList ParseNodeList(XElement element)
        {
            return new NodeList
            {
                nodes = element.Elements("Node").Select(ParseNode).ToList()
            };
        }

        public static NodeList ParseNullableNodeList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseNodeList(content);
        }
        internal static JobCompletedNotificationRegistrationList ParseJobCompletedNotificationRegistrationList(XElement element)
        {
            return new JobCompletedNotificationRegistrationList
            {
                jobCompletedNotificationRegistrations = element.Elements("JobCompletedNotificationRegistration").Select(ParseJobCompletedNotificationRegistration).ToList()
            };
        }

        public static JobCompletedNotificationRegistrationList ParseNullableJobCompletedNotificationRegistrationList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseJobCompletedNotificationRegistrationList(content);
        }
        internal static JobCreatedNotificationRegistrationList ParseJobCreatedNotificationRegistrationList(XElement element)
        {
            return new JobCreatedNotificationRegistrationList
            {
                jobCreatedNotificationRegistrations = element.Elements("JobCreatedNotificationRegistration").Select(ParseJobCreatedNotificationRegistration).ToList()
            };
        }

        public static JobCreatedNotificationRegistrationList ParseNullableJobCreatedNotificationRegistrationList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseJobCreatedNotificationRegistrationList(content);
        }
        internal static S3ObjectCachedNotificationRegistrationList ParseS3ObjectCachedNotificationRegistrationList(XElement element)
        {
            return new S3ObjectCachedNotificationRegistrationList
            {
                s3ObjectCachedNotificationRegistrations = element.Elements("S3ObjectCachedNotificationRegistration").Select(ParseS3ObjectCachedNotificationRegistration).ToList()
            };
        }

        public static S3ObjectCachedNotificationRegistrationList ParseNullableS3ObjectCachedNotificationRegistrationList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseS3ObjectCachedNotificationRegistrationList(content);
        }
        internal static S3ObjectLostNotificationRegistrationList ParseS3ObjectLostNotificationRegistrationList(XElement element)
        {
            return new S3ObjectLostNotificationRegistrationList
            {
                s3ObjectLostNotificationRegistrations = element.Elements("S3ObjectLostNotificationRegistration").Select(ParseS3ObjectLostNotificationRegistration).ToList()
            };
        }

        public static S3ObjectLostNotificationRegistrationList ParseNullableS3ObjectLostNotificationRegistrationList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseS3ObjectLostNotificationRegistrationList(content);
        }
        internal static S3ObjectPersistedNotificationRegistrationList ParseS3ObjectPersistedNotificationRegistrationList(XElement element)
        {
            return new S3ObjectPersistedNotificationRegistrationList
            {
                s3ObjectPersistedNotificationRegistrations = element.Elements("S3ObjectPersistedNotificationRegistration").Select(ParseS3ObjectPersistedNotificationRegistration).ToList()
            };
        }

        public static S3ObjectPersistedNotificationRegistrationList ParseNullableS3ObjectPersistedNotificationRegistrationList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseS3ObjectPersistedNotificationRegistrationList(content);
        }
        internal static PoolFailureNotificationRegistrationList ParsePoolFailureNotificationRegistrationList(XElement element)
        {
            return new PoolFailureNotificationRegistrationList
            {
                poolFailureNotificationRegistrations = element.Elements("PoolFailureNotificationRegistration").Select(ParsePoolFailureNotificationRegistration).ToList()
            };
        }

        public static PoolFailureNotificationRegistrationList ParseNullablePoolFailureNotificationRegistrationList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParsePoolFailureNotificationRegistrationList(content);
        }
        internal static StorageDomainFailureNotificationRegistrationList ParseStorageDomainFailureNotificationRegistrationList(XElement element)
        {
            return new StorageDomainFailureNotificationRegistrationList
            {
                storageDomainFailureNotificationRegistrations = element.Elements("StorageDomainFailureNotificationRegistration").Select(ParseStorageDomainFailureNotificationRegistration).ToList()
            };
        }

        public static StorageDomainFailureNotificationRegistrationList ParseNullableStorageDomainFailureNotificationRegistrationList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseStorageDomainFailureNotificationRegistrationList(content);
        }
        internal static SystemFailureNotificationRegistrationList ParseSystemFailureNotificationRegistrationList(XElement element)
        {
            return new SystemFailureNotificationRegistrationList
            {
                systemFailureNotificationRegistrations = element.Elements("SystemFailureNotificationRegistration").Select(ParseSystemFailureNotificationRegistration).ToList()
            };
        }

        public static SystemFailureNotificationRegistrationList ParseNullableSystemFailureNotificationRegistrationList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseSystemFailureNotificationRegistrationList(content);
        }
        internal static TapeFailureNotificationRegistrationList ParseTapeFailureNotificationRegistrationList(XElement element)
        {
            return new TapeFailureNotificationRegistrationList
            {
                tapeFailureNotificationRegistrations = element.Elements("TapeFailureNotificationRegistration").Select(ParseTapeFailureNotificationRegistration).ToList()
            };
        }

        public static TapeFailureNotificationRegistrationList ParseNullableTapeFailureNotificationRegistrationList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapeFailureNotificationRegistrationList(content);
        }
        internal static TapePartitionFailureNotificationRegistrationList ParseTapePartitionFailureNotificationRegistrationList(XElement element)
        {
            return new TapePartitionFailureNotificationRegistrationList
            {
                tapePartitionFailureNotificationRegistrations = element.Elements("TapePartitionFailureNotificationRegistration").Select(ParseTapePartitionFailureNotificationRegistration).ToList()
            };
        }

        public static TapePartitionFailureNotificationRegistrationList ParseNullableTapePartitionFailureNotificationRegistrationList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapePartitionFailureNotificationRegistrationList(content);
        }
        internal static S3ObjectList ParseS3ObjectList(XElement element)
        {
            return new S3ObjectList
            {
                s3Objects = element.Elements("S3Object").Select(ParseS3Object).ToList()
            };
        }

        public static S3ObjectList ParseNullableS3ObjectList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseS3ObjectList(content);
        }
        internal static DetailedS3ObjectList ParseDetailedS3ObjectList(XElement element)
        {
            return new DetailedS3ObjectList
            {
                detailedS3Objects = element.Elements("DetailedS3Object").Select(ParseDetailedS3Object).ToList()
            };
        }

        public static DetailedS3ObjectList ParseNullableDetailedS3ObjectList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDetailedS3ObjectList(content);
        }
        internal static PoolFailureList ParsePoolFailureList(XElement element)
        {
            return new PoolFailureList
            {
                poolFailures = element.Elements("PoolFailure").Select(ParsePoolFailure).ToList()
            };
        }

        public static PoolFailureList ParseNullablePoolFailureList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParsePoolFailureList(content);
        }
        internal static PoolPartitionList ParsePoolPartitionList(XElement element)
        {
            return new PoolPartitionList
            {
                poolPartitions = element.Elements("PoolPartition").Select(ParsePoolPartition).ToList()
            };
        }

        public static PoolPartitionList ParseNullablePoolPartitionList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParsePoolPartitionList(content);
        }
        internal static PoolList ParsePoolList(XElement element)
        {
            return new PoolList
            {
                pools = element.Elements("Pool").Select(ParsePool).ToList()
            };
        }

        public static PoolList ParseNullablePoolList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParsePoolList(content);
        }
        internal static StorageDomainFailureList ParseStorageDomainFailureList(XElement element)
        {
            return new StorageDomainFailureList
            {
                storageDomainFailures = element.Elements("StorageDomainFailure").Select(ParseStorageDomainFailure).ToList()
            };
        }

        public static StorageDomainFailureList ParseNullableStorageDomainFailureList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseStorageDomainFailureList(content);
        }
        internal static StorageDomainMemberList ParseStorageDomainMemberList(XElement element)
        {
            return new StorageDomainMemberList
            {
                storageDomainMembers = element.Elements("StorageDomainMember").Select(ParseStorageDomainMember).ToList()
            };
        }

        public static StorageDomainMemberList ParseNullableStorageDomainMemberList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseStorageDomainMemberList(content);
        }
        internal static StorageDomainList ParseStorageDomainList(XElement element)
        {
            return new StorageDomainList
            {
                storageDomains = element.Elements("StorageDomain").Select(ParseStorageDomain).ToList()
            };
        }

        public static StorageDomainList ParseNullableStorageDomainList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseStorageDomainList(content);
        }
        internal static SystemFailureList ParseSystemFailureList(XElement element)
        {
            return new SystemFailureList
            {
                systemFailures = element.Elements("SystemFailure").Select(ParseSystemFailure).ToList()
            };
        }

        public static SystemFailureList ParseNullableSystemFailureList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseSystemFailureList(content);
        }
        internal static TapeDensityDirectiveList ParseTapeDensityDirectiveList(XElement element)
        {
            return new TapeDensityDirectiveList
            {
                tapeDensityDirectives = element.Elements("TapeDensityDirective").Select(ParseTapeDensityDirective).ToList()
            };
        }

        public static TapeDensityDirectiveList ParseNullableTapeDensityDirectiveList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapeDensityDirectiveList(content);
        }
        internal static TapeDriveList ParseTapeDriveList(XElement element)
        {
            return new TapeDriveList
            {
                tapeDrives = element.Elements("TapeDrive").Select(ParseTapeDrive).ToList()
            };
        }

        public static TapeDriveList ParseNullableTapeDriveList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapeDriveList(content);
        }
        internal static DetailedTapeFailureList ParseDetailedTapeFailureList(XElement element)
        {
            return new DetailedTapeFailureList
            {
                detailedTapeFailures = element.Elements("TapeFailure").Select(ParseDetailedTapeFailure).ToList()
            };
        }

        public static DetailedTapeFailureList ParseNullableDetailedTapeFailureList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseDetailedTapeFailureList(content);
        }
        internal static TapeLibraryList ParseTapeLibraryList(XElement element)
        {
            return new TapeLibraryList
            {
                tapeLibraries = element.Elements("TapeLibrary").Select(ParseTapeLibrary).ToList()
            };
        }

        public static TapeLibraryList ParseNullableTapeLibraryList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapeLibraryList(content);
        }
        internal static TapePartitionFailureList ParseTapePartitionFailureList(XElement element)
        {
            return new TapePartitionFailureList
            {
                tapePartitionFailures = element.Elements("TapePartitionFailure").Select(ParseTapePartitionFailure).ToList()
            };
        }

        public static TapePartitionFailureList ParseNullableTapePartitionFailureList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapePartitionFailureList(content);
        }
        internal static TapePartitionList ParseTapePartitionList(XElement element)
        {
            return new TapePartitionList
            {
                tapePartitions = element.Elements("TapePartition").Select(ParseTapePartition).ToList()
            };
        }

        public static TapePartitionList ParseNullableTapePartitionList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapePartitionList(content);
        }
        internal static NamedDetailedTapePartitionList ParseNamedDetailedTapePartitionList(XElement element)
        {
            return new NamedDetailedTapePartitionList
            {
                namedDetailedTapePartitions = element.Elements("NamedDetailedTapePartition").Select(ParseNamedDetailedTapePartition).ToList()
            };
        }

        public static NamedDetailedTapePartitionList ParseNullableNamedDetailedTapePartitionList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseNamedDetailedTapePartitionList(content);
        }
        internal static TapeList ParseTapeList(XElement element)
        {
            return new TapeList
            {
                tapes = element.Elements("Tape").Select(ParseTape).ToList()
            };
        }

        public static TapeList ParseNullableTapeList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseTapeList(content);
        }
        internal static NamedDetailedTapeList ParseNamedDetailedTapeList(XElement element)
        {
            return new NamedDetailedTapeList
            {
                namedDetailedTapes = element.Elements("Tape").Select(ParseNamedDetailedTape).ToList()
            };
        }

        public static NamedDetailedTapeList ParseNullableNamedDetailedTapeList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseNamedDetailedTapeList(content);
        }
        internal static SpectraUserList ParseSpectraUserList(XElement element)
        {
            return new SpectraUserList
            {
                spectraUsers = element.Elements("User").Select(ParseSpectraUser).ToList()
            };
        }

        public static SpectraUserList ParseNullableSpectraUserList(XElement element)
        {
            return element == null || element.IsEmpty
                ? null
                : ParseSpectraUserList(content);
        }

        private static AutoInspectMode? ParseNullableAutoInspectMode(string autoInspectModeOrNull)
        {
            return string.IsNullOrWhiteSpace(autoInspectModeOrNull)
                ? (AutoInspectMode?) null
                : ParseAutoInspectMode(autoInspectModeOrNull);
        }

        private static AutoInspectMode ParseAutoInspectMode(string autoInspectMode)
        {
            return ParseEnumType<AutoInspectMode>(autoInspectMode);
        }

        private static AutoInspectMode? ParseNullableAutoInspectMode(XElement element)
        {
            return ParseNullableAutoInspectMode(element.Value);
        }

        private static AutoInspectMode ParseAutoInspectMode(XElement element)
        {
            return ParseAutoInspectMode(element.Value);
        }
        private static Priority? ParseNullablePriority(string priorityOrNull)
        {
            return string.IsNullOrWhiteSpace(priorityOrNull)
                ? (Priority?) null
                : ParsePriority(priorityOrNull);
        }

        private static Priority ParsePriority(string priority)
        {
            return ParseEnumType<Priority>(priority);
        }

        private static Priority? ParseNullablePriority(XElement element)
        {
            return ParseNullablePriority(element.Value);
        }

        private static Priority ParsePriority(XElement element)
        {
            return ParsePriority(element.Value);
        }
        private static BucketAclPermission? ParseNullableBucketAclPermission(string bucketAclPermissionOrNull)
        {
            return string.IsNullOrWhiteSpace(bucketAclPermissionOrNull)
                ? (BucketAclPermission?) null
                : ParseBucketAclPermission(bucketAclPermissionOrNull);
        }

        private static BucketAclPermission ParseBucketAclPermission(string bucketAclPermission)
        {
            return ParseEnumType<BucketAclPermission>(bucketAclPermission);
        }

        private static BucketAclPermission? ParseNullableBucketAclPermission(XElement element)
        {
            return ParseNullableBucketAclPermission(element.Value);
        }

        private static BucketAclPermission ParseBucketAclPermission(XElement element)
        {
            return ParseBucketAclPermission(element.Value);
        }
        private static DataIsolationLevel? ParseNullableDataIsolationLevel(string dataIsolationLevelOrNull)
        {
            return string.IsNullOrWhiteSpace(dataIsolationLevelOrNull)
                ? (DataIsolationLevel?) null
                : ParseDataIsolationLevel(dataIsolationLevelOrNull);
        }

        private static DataIsolationLevel ParseDataIsolationLevel(string dataIsolationLevel)
        {
            return ParseEnumType<DataIsolationLevel>(dataIsolationLevel);
        }

        private static DataIsolationLevel? ParseNullableDataIsolationLevel(XElement element)
        {
            return ParseNullableDataIsolationLevel(element.Value);
        }

        private static DataIsolationLevel ParseDataIsolationLevel(XElement element)
        {
            return ParseDataIsolationLevel(element.Value);
        }
        private static DataPersistenceRuleState? ParseNullableDataPersistenceRuleState(string dataPersistenceRuleStateOrNull)
        {
            return string.IsNullOrWhiteSpace(dataPersistenceRuleStateOrNull)
                ? (DataPersistenceRuleState?) null
                : ParseDataPersistenceRuleState(dataPersistenceRuleStateOrNull);
        }

        private static DataPersistenceRuleState ParseDataPersistenceRuleState(string dataPersistenceRuleState)
        {
            return ParseEnumType<DataPersistenceRuleState>(dataPersistenceRuleState);
        }

        private static DataPersistenceRuleState? ParseNullableDataPersistenceRuleState(XElement element)
        {
            return ParseNullableDataPersistenceRuleState(element.Value);
        }

        private static DataPersistenceRuleState ParseDataPersistenceRuleState(XElement element)
        {
            return ParseDataPersistenceRuleState(element.Value);
        }
        private static DataPersistenceRuleType? ParseNullableDataPersistenceRuleType(string dataPersistenceRuleTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(dataPersistenceRuleTypeOrNull)
                ? (DataPersistenceRuleType?) null
                : ParseDataPersistenceRuleType(dataPersistenceRuleTypeOrNull);
        }

        private static DataPersistenceRuleType ParseDataPersistenceRuleType(string dataPersistenceRuleType)
        {
            return ParseEnumType<DataPersistenceRuleType>(dataPersistenceRuleType);
        }

        private static DataPersistenceRuleType? ParseNullableDataPersistenceRuleType(XElement element)
        {
            return ParseNullableDataPersistenceRuleType(element.Value);
        }

        private static DataPersistenceRuleType ParseDataPersistenceRuleType(XElement element)
        {
            return ParseDataPersistenceRuleType(element.Value);
        }
        private static JobChunkClientProcessingOrderGuarantee? ParseNullableJobChunkClientProcessingOrderGuarantee(string jobChunkClientProcessingOrderGuaranteeOrNull)
        {
            return string.IsNullOrWhiteSpace(jobChunkClientProcessingOrderGuaranteeOrNull)
                ? (JobChunkClientProcessingOrderGuarantee?) null
                : ParseJobChunkClientProcessingOrderGuarantee(jobChunkClientProcessingOrderGuaranteeOrNull);
        }

        private static JobChunkClientProcessingOrderGuarantee ParseJobChunkClientProcessingOrderGuarantee(string jobChunkClientProcessingOrderGuarantee)
        {
            return ParseEnumType<JobChunkClientProcessingOrderGuarantee>(jobChunkClientProcessingOrderGuarantee);
        }

        private static JobChunkClientProcessingOrderGuarantee? ParseNullableJobChunkClientProcessingOrderGuarantee(XElement element)
        {
            return ParseNullableJobChunkClientProcessingOrderGuarantee(element.Value);
        }

        private static JobChunkClientProcessingOrderGuarantee ParseJobChunkClientProcessingOrderGuarantee(XElement element)
        {
            return ParseJobChunkClientProcessingOrderGuarantee(element.Value);
        }
        private static JobRequestType? ParseNullableJobRequestType(string jobRequestTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(jobRequestTypeOrNull)
                ? (JobRequestType?) null
                : ParseJobRequestType(jobRequestTypeOrNull);
        }

        private static JobRequestType ParseJobRequestType(string jobRequestType)
        {
            return ParseEnumType<JobRequestType>(jobRequestType);
        }

        private static JobRequestType? ParseNullableJobRequestType(XElement element)
        {
            return ParseNullableJobRequestType(element.Value);
        }

        private static JobRequestType ParseJobRequestType(XElement element)
        {
            return ParseJobRequestType(element.Value);
        }
        private static LtfsFileNamingMode? ParseNullableLtfsFileNamingMode(string ltfsFileNamingModeOrNull)
        {
            return string.IsNullOrWhiteSpace(ltfsFileNamingModeOrNull)
                ? (LtfsFileNamingMode?) null
                : ParseLtfsFileNamingMode(ltfsFileNamingModeOrNull);
        }

        private static LtfsFileNamingMode ParseLtfsFileNamingMode(string ltfsFileNamingMode)
        {
            return ParseEnumType<LtfsFileNamingMode>(ltfsFileNamingMode);
        }

        private static LtfsFileNamingMode? ParseNullableLtfsFileNamingMode(XElement element)
        {
            return ParseNullableLtfsFileNamingMode(element.Value);
        }

        private static LtfsFileNamingMode ParseLtfsFileNamingMode(XElement element)
        {
            return ParseLtfsFileNamingMode(element.Value);
        }
        private static S3ObjectType? ParseNullableS3ObjectType(string s3ObjectTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(s3ObjectTypeOrNull)
                ? (S3ObjectType?) null
                : ParseS3ObjectType(s3ObjectTypeOrNull);
        }

        private static S3ObjectType ParseS3ObjectType(string s3ObjectType)
        {
            return ParseEnumType<S3ObjectType>(s3ObjectType);
        }

        private static S3ObjectType? ParseNullableS3ObjectType(XElement element)
        {
            return ParseNullableS3ObjectType(element.Value);
        }

        private static S3ObjectType ParseS3ObjectType(XElement element)
        {
            return ParseS3ObjectType(element.Value);
        }
        private static StorageDomainFailureType? ParseNullableStorageDomainFailureType(string storageDomainFailureTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(storageDomainFailureTypeOrNull)
                ? (StorageDomainFailureType?) null
                : ParseStorageDomainFailureType(storageDomainFailureTypeOrNull);
        }

        private static StorageDomainFailureType ParseStorageDomainFailureType(string storageDomainFailureType)
        {
            return ParseEnumType<StorageDomainFailureType>(storageDomainFailureType);
        }

        private static StorageDomainFailureType? ParseNullableStorageDomainFailureType(XElement element)
        {
            return ParseNullableStorageDomainFailureType(element.Value);
        }

        private static StorageDomainFailureType ParseStorageDomainFailureType(XElement element)
        {
            return ParseStorageDomainFailureType(element.Value);
        }
        private static StorageDomainMemberState? ParseNullableStorageDomainMemberState(string storageDomainMemberStateOrNull)
        {
            return string.IsNullOrWhiteSpace(storageDomainMemberStateOrNull)
                ? (StorageDomainMemberState?) null
                : ParseStorageDomainMemberState(storageDomainMemberStateOrNull);
        }

        private static StorageDomainMemberState ParseStorageDomainMemberState(string storageDomainMemberState)
        {
            return ParseEnumType<StorageDomainMemberState>(storageDomainMemberState);
        }

        private static StorageDomainMemberState? ParseNullableStorageDomainMemberState(XElement element)
        {
            return ParseNullableStorageDomainMemberState(element.Value);
        }

        private static StorageDomainMemberState ParseStorageDomainMemberState(XElement element)
        {
            return ParseStorageDomainMemberState(element.Value);
        }
        private static SystemFailureType? ParseNullableSystemFailureType(string systemFailureTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(systemFailureTypeOrNull)
                ? (SystemFailureType?) null
                : ParseSystemFailureType(systemFailureTypeOrNull);
        }

        private static SystemFailureType ParseSystemFailureType(string systemFailureType)
        {
            return ParseEnumType<SystemFailureType>(systemFailureType);
        }

        private static SystemFailureType? ParseNullableSystemFailureType(XElement element)
        {
            return ParseNullableSystemFailureType(element.Value);
        }

        private static SystemFailureType ParseSystemFailureType(XElement element)
        {
            return ParseSystemFailureType(element.Value);
        }
        private static UnavailableMediaUsagePolicy? ParseNullableUnavailableMediaUsagePolicy(string unavailableMediaUsagePolicyOrNull)
        {
            return string.IsNullOrWhiteSpace(unavailableMediaUsagePolicyOrNull)
                ? (UnavailableMediaUsagePolicy?) null
                : ParseUnavailableMediaUsagePolicy(unavailableMediaUsagePolicyOrNull);
        }

        private static UnavailableMediaUsagePolicy ParseUnavailableMediaUsagePolicy(string unavailableMediaUsagePolicy)
        {
            return ParseEnumType<UnavailableMediaUsagePolicy>(unavailableMediaUsagePolicy);
        }

        private static UnavailableMediaUsagePolicy? ParseNullableUnavailableMediaUsagePolicy(XElement element)
        {
            return ParseNullableUnavailableMediaUsagePolicy(element.Value);
        }

        private static UnavailableMediaUsagePolicy ParseUnavailableMediaUsagePolicy(XElement element)
        {
            return ParseUnavailableMediaUsagePolicy(element.Value);
        }
        private static VersioningLevel? ParseNullableVersioningLevel(string versioningLevelOrNull)
        {
            return string.IsNullOrWhiteSpace(versioningLevelOrNull)
                ? (VersioningLevel?) null
                : ParseVersioningLevel(versioningLevelOrNull);
        }

        private static VersioningLevel ParseVersioningLevel(string versioningLevel)
        {
            return ParseEnumType<VersioningLevel>(versioningLevel);
        }

        private static VersioningLevel? ParseNullableVersioningLevel(XElement element)
        {
            return ParseNullableVersioningLevel(element.Value);
        }

        private static VersioningLevel ParseVersioningLevel(XElement element)
        {
            return ParseVersioningLevel(element.Value);
        }
        private static WriteOptimization? ParseNullableWriteOptimization(string writeOptimizationOrNull)
        {
            return string.IsNullOrWhiteSpace(writeOptimizationOrNull)
                ? (WriteOptimization?) null
                : ParseWriteOptimization(writeOptimizationOrNull);
        }

        private static WriteOptimization ParseWriteOptimization(string writeOptimization)
        {
            return ParseEnumType<WriteOptimization>(writeOptimization);
        }

        private static WriteOptimization? ParseNullableWriteOptimization(XElement element)
        {
            return ParseNullableWriteOptimization(element.Value);
        }

        private static WriteOptimization ParseWriteOptimization(XElement element)
        {
            return ParseWriteOptimization(element.Value);
        }
        private static WritePreferenceLevel? ParseNullableWritePreferenceLevel(string writePreferenceLevelOrNull)
        {
            return string.IsNullOrWhiteSpace(writePreferenceLevelOrNull)
                ? (WritePreferenceLevel?) null
                : ParseWritePreferenceLevel(writePreferenceLevelOrNull);
        }

        private static WritePreferenceLevel ParseWritePreferenceLevel(string writePreferenceLevel)
        {
            return ParseEnumType<WritePreferenceLevel>(writePreferenceLevel);
        }

        private static WritePreferenceLevel? ParseNullableWritePreferenceLevel(XElement element)
        {
            return ParseNullableWritePreferenceLevel(element.Value);
        }

        private static WritePreferenceLevel ParseWritePreferenceLevel(XElement element)
        {
            return ParseWritePreferenceLevel(element.Value);
        }
        private static PoolFailureType? ParseNullablePoolFailureType(string poolFailureTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(poolFailureTypeOrNull)
                ? (PoolFailureType?) null
                : ParsePoolFailureType(poolFailureTypeOrNull);
        }

        private static PoolFailureType ParsePoolFailureType(string poolFailureType)
        {
            return ParseEnumType<PoolFailureType>(poolFailureType);
        }

        private static PoolFailureType? ParseNullablePoolFailureType(XElement element)
        {
            return ParseNullablePoolFailureType(element.Value);
        }

        private static PoolFailureType ParsePoolFailureType(XElement element)
        {
            return ParsePoolFailureType(element.Value);
        }
        private static PoolHealth? ParseNullablePoolHealth(string poolHealthOrNull)
        {
            return string.IsNullOrWhiteSpace(poolHealthOrNull)
                ? (PoolHealth?) null
                : ParsePoolHealth(poolHealthOrNull);
        }

        private static PoolHealth ParsePoolHealth(string poolHealth)
        {
            return ParseEnumType<PoolHealth>(poolHealth);
        }

        private static PoolHealth? ParseNullablePoolHealth(XElement element)
        {
            return ParseNullablePoolHealth(element.Value);
        }

        private static PoolHealth ParsePoolHealth(XElement element)
        {
            return ParsePoolHealth(element.Value);
        }
        private static PoolState? ParseNullablePoolState(string poolStateOrNull)
        {
            return string.IsNullOrWhiteSpace(poolStateOrNull)
                ? (PoolState?) null
                : ParsePoolState(poolStateOrNull);
        }

        private static PoolState ParsePoolState(string poolState)
        {
            return ParseEnumType<PoolState>(poolState);
        }

        private static PoolState? ParseNullablePoolState(XElement element)
        {
            return ParseNullablePoolState(element.Value);
        }

        private static PoolState ParsePoolState(XElement element)
        {
            return ParsePoolState(element.Value);
        }
        private static PoolType? ParseNullablePoolType(string poolTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(poolTypeOrNull)
                ? (PoolType?) null
                : ParsePoolType(poolTypeOrNull);
        }

        private static PoolType ParsePoolType(string poolType)
        {
            return ParseEnumType<PoolType>(poolType);
        }

        private static PoolType? ParseNullablePoolType(XElement element)
        {
            return ParseNullablePoolType(element.Value);
        }

        private static PoolType ParsePoolType(XElement element)
        {
            return ParsePoolType(element.Value);
        }
        private static ImportConflictResolutionMode? ParseNullableImportConflictResolutionMode(string importConflictResolutionModeOrNull)
        {
            return string.IsNullOrWhiteSpace(importConflictResolutionModeOrNull)
                ? (ImportConflictResolutionMode?) null
                : ParseImportConflictResolutionMode(importConflictResolutionModeOrNull);
        }

        private static ImportConflictResolutionMode ParseImportConflictResolutionMode(string importConflictResolutionMode)
        {
            return ParseEnumType<ImportConflictResolutionMode>(importConflictResolutionMode);
        }

        private static ImportConflictResolutionMode? ParseNullableImportConflictResolutionMode(XElement element)
        {
            return ParseNullableImportConflictResolutionMode(element.Value);
        }

        private static ImportConflictResolutionMode ParseImportConflictResolutionMode(XElement element)
        {
            return ParseImportConflictResolutionMode(element.Value);
        }
        private static Quiesced? ParseNullableQuiesced(string quiescedOrNull)
        {
            return string.IsNullOrWhiteSpace(quiescedOrNull)
                ? (Quiesced?) null
                : ParseQuiesced(quiescedOrNull);
        }

        private static Quiesced ParseQuiesced(string quiesced)
        {
            return ParseEnumType<Quiesced>(quiesced);
        }

        private static Quiesced? ParseNullableQuiesced(XElement element)
        {
            return ParseNullableQuiesced(element.Value);
        }

        private static Quiesced ParseQuiesced(XElement element)
        {
            return ParseQuiesced(element.Value);
        }
        private static ReplicationConflictResolutionMode? ParseNullableReplicationConflictResolutionMode(string replicationConflictResolutionModeOrNull)
        {
            return string.IsNullOrWhiteSpace(replicationConflictResolutionModeOrNull)
                ? (ReplicationConflictResolutionMode?) null
                : ParseReplicationConflictResolutionMode(replicationConflictResolutionModeOrNull);
        }

        private static ReplicationConflictResolutionMode ParseReplicationConflictResolutionMode(string replicationConflictResolutionMode)
        {
            return ParseEnumType<ReplicationConflictResolutionMode>(replicationConflictResolutionMode);
        }

        private static ReplicationConflictResolutionMode? ParseNullableReplicationConflictResolutionMode(XElement element)
        {
            return ParseNullableReplicationConflictResolutionMode(element.Value);
        }

        private static ReplicationConflictResolutionMode ParseReplicationConflictResolutionMode(XElement element)
        {
            return ParseReplicationConflictResolutionMode(element.Value);
        }
        private static ImportExportConfiguration? ParseNullableImportExportConfiguration(string importExportConfigurationOrNull)
        {
            return string.IsNullOrWhiteSpace(importExportConfigurationOrNull)
                ? (ImportExportConfiguration?) null
                : ParseImportExportConfiguration(importExportConfigurationOrNull);
        }

        private static ImportExportConfiguration ParseImportExportConfiguration(string importExportConfiguration)
        {
            return ParseEnumType<ImportExportConfiguration>(importExportConfiguration);
        }

        private static ImportExportConfiguration? ParseNullableImportExportConfiguration(XElement element)
        {
            return ParseNullableImportExportConfiguration(element.Value);
        }

        private static ImportExportConfiguration ParseImportExportConfiguration(XElement element)
        {
            return ParseImportExportConfiguration(element.Value);
        }
        private static TapeDriveState? ParseNullableTapeDriveState(string tapeDriveStateOrNull)
        {
            return string.IsNullOrWhiteSpace(tapeDriveStateOrNull)
                ? (TapeDriveState?) null
                : ParseTapeDriveState(tapeDriveStateOrNull);
        }

        private static TapeDriveState ParseTapeDriveState(string tapeDriveState)
        {
            return ParseEnumType<TapeDriveState>(tapeDriveState);
        }

        private static TapeDriveState? ParseNullableTapeDriveState(XElement element)
        {
            return ParseNullableTapeDriveState(element.Value);
        }

        private static TapeDriveState ParseTapeDriveState(XElement element)
        {
            return ParseTapeDriveState(element.Value);
        }
        private static TapeDriveType? ParseNullableTapeDriveType(string tapeDriveTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(tapeDriveTypeOrNull)
                ? (TapeDriveType?) null
                : ParseTapeDriveType(tapeDriveTypeOrNull);
        }

        private static TapeDriveType ParseTapeDriveType(string tapeDriveType)
        {
            return ParseEnumType<TapeDriveType>(tapeDriveType);
        }

        private static TapeDriveType? ParseNullableTapeDriveType(XElement element)
        {
            return ParseNullableTapeDriveType(element.Value);
        }

        private static TapeDriveType ParseTapeDriveType(XElement element)
        {
            return ParseTapeDriveType(element.Value);
        }
        private static TapeFailureType? ParseNullableTapeFailureType(string tapeFailureTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(tapeFailureTypeOrNull)
                ? (TapeFailureType?) null
                : ParseTapeFailureType(tapeFailureTypeOrNull);
        }

        private static TapeFailureType ParseTapeFailureType(string tapeFailureType)
        {
            return ParseEnumType<TapeFailureType>(tapeFailureType);
        }

        private static TapeFailureType? ParseNullableTapeFailureType(XElement element)
        {
            return ParseNullableTapeFailureType(element.Value);
        }

        private static TapeFailureType ParseTapeFailureType(XElement element)
        {
            return ParseTapeFailureType(element.Value);
        }
        private static TapePartitionFailureType? ParseNullableTapePartitionFailureType(string tapePartitionFailureTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(tapePartitionFailureTypeOrNull)
                ? (TapePartitionFailureType?) null
                : ParseTapePartitionFailureType(tapePartitionFailureTypeOrNull);
        }

        private static TapePartitionFailureType ParseTapePartitionFailureType(string tapePartitionFailureType)
        {
            return ParseEnumType<TapePartitionFailureType>(tapePartitionFailureType);
        }

        private static TapePartitionFailureType? ParseNullableTapePartitionFailureType(XElement element)
        {
            return ParseNullableTapePartitionFailureType(element.Value);
        }

        private static TapePartitionFailureType ParseTapePartitionFailureType(XElement element)
        {
            return ParseTapePartitionFailureType(element.Value);
        }
        private static TapePartitionState? ParseNullableTapePartitionState(string tapePartitionStateOrNull)
        {
            return string.IsNullOrWhiteSpace(tapePartitionStateOrNull)
                ? (TapePartitionState?) null
                : ParseTapePartitionState(tapePartitionStateOrNull);
        }

        private static TapePartitionState ParseTapePartitionState(string tapePartitionState)
        {
            return ParseEnumType<TapePartitionState>(tapePartitionState);
        }

        private static TapePartitionState? ParseNullableTapePartitionState(XElement element)
        {
            return ParseNullableTapePartitionState(element.Value);
        }

        private static TapePartitionState ParseTapePartitionState(XElement element)
        {
            return ParseTapePartitionState(element.Value);
        }
        private static TapeState? ParseNullableTapeState(string tapeStateOrNull)
        {
            return string.IsNullOrWhiteSpace(tapeStateOrNull)
                ? (TapeState?) null
                : ParseTapeState(tapeStateOrNull);
        }

        private static TapeState ParseTapeState(string tapeState)
        {
            return ParseEnumType<TapeState>(tapeState);
        }

        private static TapeState? ParseNullableTapeState(XElement element)
        {
            return ParseNullableTapeState(element.Value);
        }

        private static TapeState ParseTapeState(XElement element)
        {
            return ParseTapeState(element.Value);
        }
        private static TapeType? ParseNullableTapeType(string tapeTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(tapeTypeOrNull)
                ? (TapeType?) null
                : ParseTapeType(tapeTypeOrNull);
        }

        private static TapeType ParseTapeType(string tapeType)
        {
            return ParseEnumType<TapeType>(tapeType);
        }

        private static TapeType? ParseNullableTapeType(XElement element)
        {
            return ParseNullableTapeType(element.Value);
        }

        private static TapeType ParseTapeType(XElement element)
        {
            return ParseTapeType(element.Value);
        }
        private static BlobStoreTaskState? ParseNullableBlobStoreTaskState(string blobStoreTaskStateOrNull)
        {
            return string.IsNullOrWhiteSpace(blobStoreTaskStateOrNull)
                ? (BlobStoreTaskState?) null
                : ParseBlobStoreTaskState(blobStoreTaskStateOrNull);
        }

        private static BlobStoreTaskState ParseBlobStoreTaskState(string blobStoreTaskState)
        {
            return ParseEnumType<BlobStoreTaskState>(blobStoreTaskState);
        }

        private static BlobStoreTaskState? ParseNullableBlobStoreTaskState(XElement element)
        {
            return ParseNullableBlobStoreTaskState(element.Value);
        }

        private static BlobStoreTaskState ParseBlobStoreTaskState(XElement element)
        {
            return ParseBlobStoreTaskState(element.Value);
        }
        private static CacheEntryState? ParseNullableCacheEntryState(string cacheEntryStateOrNull)
        {
            return string.IsNullOrWhiteSpace(cacheEntryStateOrNull)
                ? (CacheEntryState?) null
                : ParseCacheEntryState(cacheEntryStateOrNull);
        }

        private static CacheEntryState ParseCacheEntryState(string cacheEntryState)
        {
            return ParseEnumType<CacheEntryState>(cacheEntryState);
        }

        private static CacheEntryState? ParseNullableCacheEntryState(XElement element)
        {
            return ParseNullableCacheEntryState(element.Value);
        }

        private static CacheEntryState ParseCacheEntryState(XElement element)
        {
            return ParseCacheEntryState(element.Value);
        }
        private static JobStatus? ParseNullableJobStatus(string jobStatusOrNull)
        {
            return string.IsNullOrWhiteSpace(jobStatusOrNull)
                ? (JobStatus?) null
                : ParseJobStatus(jobStatusOrNull);
        }

        private static JobStatus ParseJobStatus(string jobStatus)
        {
            return ParseEnumType<JobStatus>(jobStatus);
        }

        private static JobStatus? ParseNullableJobStatus(XElement element)
        {
            return ParseNullableJobStatus(element.Value);
        }

        private static JobStatus ParseJobStatus(XElement element)
        {
            return ParseJobStatus(element.Value);
        }
        private static Application? ParseNullableApplication(string applicationOrNull)
        {
            return string.IsNullOrWhiteSpace(applicationOrNull)
                ? (Application?) null
                : ParseApplication(applicationOrNull);
        }

        private static Application ParseApplication(string application)
        {
            return ParseEnumType<Application>(application);
        }

        private static Application? ParseNullableApplication(XElement element)
        {
            return ParseNullableApplication(element.Value);
        }

        private static Application ParseApplication(XElement element)
        {
            return ParseApplication(element.Value);
        }
        private static RestActionType? ParseNullableRestActionType(string restActionTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(restActionTypeOrNull)
                ? (RestActionType?) null
                : ParseRestActionType(restActionTypeOrNull);
        }

        private static RestActionType ParseRestActionType(string restActionType)
        {
            return ParseEnumType<RestActionType>(restActionType);
        }

        private static RestActionType? ParseNullableRestActionType(XElement element)
        {
            return ParseNullableRestActionType(element.Value);
        }

        private static RestActionType ParseRestActionType(XElement element)
        {
            return ParseRestActionType(element.Value);
        }
        private static RestDomainType? ParseNullableRestDomainType(string restDomainTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(restDomainTypeOrNull)
                ? (RestDomainType?) null
                : ParseRestDomainType(restDomainTypeOrNull);
        }

        private static RestDomainType ParseRestDomainType(string restDomainType)
        {
            return ParseEnumType<RestDomainType>(restDomainType);
        }

        private static RestDomainType? ParseNullableRestDomainType(XElement element)
        {
            return ParseNullableRestDomainType(element.Value);
        }

        private static RestDomainType ParseRestDomainType(XElement element)
        {
            return ParseRestDomainType(element.Value);
        }
        private static RestOperationType? ParseNullableRestOperationType(string restOperationTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(restOperationTypeOrNull)
                ? (RestOperationType?) null
                : ParseRestOperationType(restOperationTypeOrNull);
        }

        private static RestOperationType ParseRestOperationType(string restOperationType)
        {
            return ParseEnumType<RestOperationType>(restOperationType);
        }

        private static RestOperationType? ParseNullableRestOperationType(XElement element)
        {
            return ParseNullableRestOperationType(element.Value);
        }

        private static RestOperationType ParseRestOperationType(XElement element)
        {
            return ParseRestOperationType(element.Value);
        }
        private static RestResourceType? ParseNullableRestResourceType(string restResourceTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(restResourceTypeOrNull)
                ? (RestResourceType?) null
                : ParseRestResourceType(restResourceTypeOrNull);
        }

        private static RestResourceType ParseRestResourceType(string restResourceType)
        {
            return ParseEnumType<RestResourceType>(restResourceType);
        }

        private static RestResourceType? ParseNullableRestResourceType(XElement element)
        {
            return ParseNullableRestResourceType(element.Value);
        }

        private static RestResourceType ParseRestResourceType(XElement element)
        {
            return ParseRestResourceType(element.Value);
        }
        private static SqlOperation? ParseNullableSqlOperation(string sqlOperationOrNull)
        {
            return string.IsNullOrWhiteSpace(sqlOperationOrNull)
                ? (SqlOperation?) null
                : ParseSqlOperation(sqlOperationOrNull);
        }

        private static SqlOperation ParseSqlOperation(string sqlOperation)
        {
            return ParseEnumType<SqlOperation>(sqlOperation);
        }

        private static SqlOperation? ParseNullableSqlOperation(XElement element)
        {
            return ParseNullableSqlOperation(element.Value);
        }

        private static SqlOperation ParseSqlOperation(XElement element)
        {
            return ParseSqlOperation(element.Value);
        }
        private static DatabasePhysicalSpaceState? ParseNullableDatabasePhysicalSpaceState(string databasePhysicalSpaceStateOrNull)
        {
            return string.IsNullOrWhiteSpace(databasePhysicalSpaceStateOrNull)
                ? (DatabasePhysicalSpaceState?) null
                : ParseDatabasePhysicalSpaceState(databasePhysicalSpaceStateOrNull);
        }

        private static DatabasePhysicalSpaceState ParseDatabasePhysicalSpaceState(string databasePhysicalSpaceState)
        {
            return ParseEnumType<DatabasePhysicalSpaceState>(databasePhysicalSpaceState);
        }

        private static DatabasePhysicalSpaceState? ParseNullableDatabasePhysicalSpaceState(XElement element)
        {
            return ParseNullableDatabasePhysicalSpaceState(element.Value);
        }

        private static DatabasePhysicalSpaceState ParseDatabasePhysicalSpaceState(XElement element)
        {
            return ParseDatabasePhysicalSpaceState(element.Value);
        }
        private static HttpResponseFormatType? ParseNullableHttpResponseFormatType(string httpResponseFormatTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(httpResponseFormatTypeOrNull)
                ? (HttpResponseFormatType?) null
                : ParseHttpResponseFormatType(httpResponseFormatTypeOrNull);
        }

        private static HttpResponseFormatType ParseHttpResponseFormatType(string httpResponseFormatType)
        {
            return ParseEnumType<HttpResponseFormatType>(httpResponseFormatType);
        }

        private static HttpResponseFormatType? ParseNullableHttpResponseFormatType(XElement element)
        {
            return ParseNullableHttpResponseFormatType(element.Value);
        }

        private static HttpResponseFormatType ParseHttpResponseFormatType(XElement element)
        {
            return ParseHttpResponseFormatType(element.Value);
        }
        private static RequestType? ParseNullableRequestType(string requestTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(requestTypeOrNull)
                ? (RequestType?) null
                : ParseRequestType(requestTypeOrNull);
        }

        private static RequestType ParseRequestType(string requestType)
        {
            return ParseEnumType<RequestType>(requestType);
        }

        private static RequestType? ParseNullableRequestType(XElement element)
        {
            return ParseNullableRequestType(element.Value);
        }

        private static RequestType ParseRequestType(XElement element)
        {
            return ParseRequestType(element.Value);
        }
        private static NamingConventionType? ParseNullableNamingConventionType(string namingConventionTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(namingConventionTypeOrNull)
                ? (NamingConventionType?) null
                : ParseNamingConventionType(namingConventionTypeOrNull);
        }

        private static NamingConventionType ParseNamingConventionType(string namingConventionType)
        {
            return ParseEnumType<NamingConventionType>(namingConventionType);
        }

        private static NamingConventionType? ParseNullableNamingConventionType(XElement element)
        {
            return ParseNullableNamingConventionType(element.Value);
        }

        private static NamingConventionType ParseNamingConventionType(XElement element)
        {
            return ParseNamingConventionType(element.Value);
        }
        private static ChecksumType? ParseNullableChecksumType(string checksumTypeOrNull)
        {
            return string.IsNullOrWhiteSpace(checksumTypeOrNull)
                ? (ChecksumType?) null
                : ParseChecksumType(checksumTypeOrNull);
        }

        private static ChecksumType ParseChecksumType(string checksumType)
        {
            return ParseEnumType<ChecksumType>(checksumType);
        }

        private static ChecksumType? ParseNullableChecksumType(XElement element)
        {
            return ParseNullableChecksumType(element.Value);
        }

        private static ChecksumType ParseChecksumType(XElement element)
        {
            return ParseChecksumType(element.Value);
        }

        //DateTime parsers

        private static DateTime? ParseNullableDateTime(XElement element)
        {
            return ParseNullableDateTime(element.Value);
        }

        private static DateTime? ParseNullableDateTime(string dateTimeStringOrNull)
        {
            return string.IsNullOrWhiteSpace(dateTimeStringOrNull)
                ? (DateTime?)null
                : ParseDateTime(dateTimeStringOrNull);
        }

        private static DateTime ParseDateTime(XElement element)
        {
            return ParseDateTime(element.Value);
        }

        private static DateTime ParseDateTime(string dateTimeString)
        {
            return DateTime.Parse(dateTimeString);
        }

        //Boolean parsers

        private static bool? ParseNullableBool(XElement element)
        {
            return ParseNullableBool(element.Value);
        }

        private static bool? ParseNullableBool(string boolStringOrNull)
        {
            return string.IsNullOrWhiteSpace(boolStringOrNull)
                ? (bool?)null
                : ParseBool(boolStringOrNull);
        }

        private static bool ParseBool(XElement element)
        {
            return ParseBool(element.Value);
        }

        private static bool ParseBool(string boolString)
        {
            return bool.Parse(boolString);
        }

        //String parsers

        private static string ParseNullableString(XElement element)
        {
            return ParseNullableString(element.Value);
        }

        private static string ParseNullableString(string stringOrNull)
        {
            return string.IsNullOrWhiteSpace(stringOrNull)
                ? (string)null
                : ParseString(stringOrNull);
        }

        private static string ParseString(XElement element)
        {
            return ParseString(element.Value);
        }

        private static string ParseString(string str)
        {
            return str;
        }

        //Integer parsers

        private static int? ParseNullableInt(XElement element)
        {
            return ParseNullableInt(element.Value);
        }

        private static int? ParseNullableInt(string intStringOrNull)
        {
            return string.IsNullOrWhiteSpace(intStringOrNull)
                ? (int?)null
                : ParseInt(intStringOrNull);
        }

        private static int ParseInt(XElement element)
        {
            return ParseInt(element.Value);
        }

        private static int ParseInt(string intString)
        {
            return int.Parse(intString);
        }

        //Long parsers

        private static long? ParseNullableLong(XElement element)
        {
            return ParseNullableLong(element.Value);
        }

        private static long? ParseNullableLong(string longStringOrNull)
        {
            return string.IsNullOrWhiteSpace(longStringOrNull)
                ? (long?)null
                : ParseLong(longStringOrNull);
        }

        private static long ParseLong(XElement element)
        {
            return ParseLong(element.Value);
        }

        private static long ParseLong(string longString)
        {
            return long.Parse(longString);
        }

        //Double parsers

        private static double? ParseNullableDouble(XElement element)
        {
            return ParseNullableDouble(element.Value);
        }

        private static double? ParseNullableDouble(string doubleStringOrNull)
        {
            return string.IsNullOrWhiteSpace(doubleStringOrNull)
                ? (double?)null
                : ParseDouble(doubleStringOrNull);
        }

        private static double ParseDouble(XElement element)
        {
            return ParseDouble(element.Value);
        }

        private static double ParseDouble(string doubleString)
        {
            return double.Parse(doubleString);
        }

        //Enum parser

        private static T ParseEnumType<T>(string enumString)
            where T : struct
        {
            T result;
            if (!Enum.TryParse(ConvertToPascalCase(enumString), out result))
            {
                throw new ArgumentException(string.Format(Resources.InvalidValueForTypeException, typeof(T).Name));
            }
            return result;
        }

        private static string ConvertToPascalCase(string uppercaseUnderscore)
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
