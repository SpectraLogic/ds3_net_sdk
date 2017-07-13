/*
 * ******************************************************************************
 *   Copyright 2014-2017 Spectra Logic Corporation. All Rights Reserved.
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
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using Ds3;
using Ds3.Calls;
using Ds3.Helpers;
using Ds3.Helpers.Strategies;
using Ds3.Models;

namespace IntegrationTestDS3
{
    public static class Ds3TestUtils
    {
        private static readonly List<Ds3Object> Objects = new List<Ds3Object>
        {
            new Ds3Object("beowulf.txt", 294059),
            new Ds3Object("sherlock_holmes.txt", 581881),
            new Ds3Object("tale_of_two_cities.txt", 776649),
            new Ds3Object("ulysses.txt", 1540095)
        };

        public static IDs3Client CreateClient(int? copyBufferSize = null)
        {
            const int defaultCopyBufferSize = 1*1024*1024;

            return Ds3Builder.FromEnv().
                WithCopyBufferSize(copyBufferSize ?? defaultCopyBufferSize).
                Build();
        }

        public static void LoadTestData(IDs3Client client, string bucketName,
            IHelperStrategy<string> helperStrategy = null)
        {
            PutFiles(client, bucketName, Objects, ReadResource, helperStrategy);
        }

        /// <summary>
        /// This will get the object and return the name of the temporary file it was written to.
        /// It is up to the caller to delete the temporary file
        /// </summary>
        public static string GetSingleObject(IDs3Client client, string bucketName, string objectName, int retries = 5,
            IHelperStrategy<string> helperStrategy = null)
        {
            var tempFilename = Path.GetTempFileName();

            using (Stream fileStream = new FileStream(tempFilename, FileMode.Truncate, FileAccess.Write))
            {
                IDs3ClientHelpers helper = new Ds3ClientHelpers(client, objectTransferAttempts: retries);

                if (helperStrategy == null)
                {
                    helperStrategy = new ReadRandomAccessHelperStrategy<string>();
                }

                var job = helper.StartReadJob(bucketName, new List<Ds3Object> {new Ds3Object(objectName, null)},
                    helperStrategy: helperStrategy);

                job.Transfer(key => fileStream);

                return tempFilename;
            }
        }

        internal static string GetSingleObjectWithRange(IDs3Client client, string bucketName, string objectName,
            Range range, IHelperStrategy<Ds3PartialObject> helperStrategy = null)
        {
            var tempFilename = Path.GetTempFileName();

            using (Stream fileStream = new FileStream(tempFilename, FileMode.Truncate, FileAccess.Write))
            {
                IDs3ClientHelpers helper = new Ds3ClientHelpers(client);

                if (helperStrategy == null)
                {
                    helperStrategy = new ReadRandomAccessHelperStrategy<Ds3PartialObject>();
                }

                var job = helper.StartPartialReadJob(bucketName, new List<string>(),
                    new List<Ds3PartialObject> {new Ds3PartialObject(range, objectName)}, helperStrategy: helperStrategy);

                job.Transfer(key => fileStream);

                return tempFilename;
            }
        }

        public static void PutFiles(IDs3Client client, string bucketName, IEnumerable<Ds3Object> files,
            Func<string, Stream> createStreamForTransferItem, IHelperStrategy<string> helperStrategy = null)
        {
            IDs3ClientHelpers helper = new Ds3ClientHelpers(client);

            helper.EnsureBucketExists(bucketName);

            var job = helper.StartWriteJob(bucketName, files, helperStrategy: helperStrategy);

            job.Transfer(createStreamForTransferItem);
        }

        public static void DeleteBucket(IDs3Client client, string bucketName)
        {
            if (client.HeadBucket(new HeadBucketRequest(bucketName)).Status == HeadBucketResponse.StatusType.DoesntExist)
            {
                return;
            }

            IDs3ClientHelpers helpers = new Ds3ClientHelpers(client);

            var objs = helpers.ListObjects(bucketName);

            client.DeleteObjects(new DeleteObjectsRequest(bucketName, objs));
            client.DeleteBucket(new DeleteBucketRequest(bucketName));
        }

        public static string ComputeSha1(string fileName)
        {
            using (var sha1Managed = new SHA1Managed())
            using (Stream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (Stream sha1Stream = new CryptoStream(fileStream, sha1Managed, CryptoStreamMode.Read))
            {
                while (sha1Stream.ReadByte() != -1)
                {
                }

                return Convert.ToBase64String(sha1Managed.Hash);
            }
        }

        private static Stream ReadResource(string resourceName)
        {
            return
                Assembly.GetExecutingAssembly().GetManifestResourceStream("IntegrationTestDS3.TestData." + resourceName);
        }

        public static void UsingAllWriteStrategies(Action<IHelperStrategy<string>> action)
        {
            var writeStrategyList = new List<IHelperStrategy<string>>
            {
                null, //using the default strategy
                new WriteRandomAccessHelperStrategy(),
                new WriteAggregateJobsHelperStrategy(Objects),
                new WriteNoAllocateHelperStrategy(),
                new WriteStreamHelperStrategy()
            };

            writeStrategyList.ForEach(action);
        }

        public static void UsingAllStringReadStrategies(Action<IHelperStrategy<string>> action)
        {
            var writeStrategyList = new List<IHelperStrategy<string>>
            {
                null, //using the default strategy
                new ReadRandomAccessHelperStrategy<string>(),
                new ReadStreamHelperStrategy()
            };

            writeStrategyList.ForEach(action);
        }

        public static void UsingAllDs3PartialObjectReadStrategies(Action<IHelperStrategy<Ds3PartialObject>> action)
        {
            var writeStrategyList = new List<IHelperStrategy<Ds3PartialObject>>
            {
                null, //using the default strategy
                new ReadRandomAccessHelperStrategy<Ds3PartialObject>()
            };

            writeStrategyList.ForEach(action);
        }

        public static string ComputeChecksum(FileStream file, ChecksumType.Type type)
        {
            switch (type)
            {
                case ChecksumType.Type.MD5:
                    return Convert.ToBase64String(MD5.Create().ComputeHash(file));
                case ChecksumType.Type.SHA_256:
                    return
                        Convert.ToBase64String(SHA256.Create().ComputeHash(file));
                case ChecksumType.Type.SHA_512:
                    return
                        Convert.ToBase64String(SHA512.Create().ComputeHash(file));
                case ChecksumType.Type.CRC_32:
                    return
                        Convert.ToBase64String(Crc32.Create().ComputeHash(file));
                case ChecksumType.Type.CRC_32C:
                    return
                        Convert.ToBase64String(Crc32C.Create().ComputeHash(file));
                default:
                    return "";
            }
        }
    }
}