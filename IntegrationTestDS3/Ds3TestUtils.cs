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

using System;
using System.Collections.Generic;
using Ds3;
using Ds3.Calls;
using Ds3.Helpers;
using Ds3.Models;
using System.Reflection;
using System.IO;
using System.Security.Cryptography;

namespace IntegrationTestDS3
{
    internal static class Ds3TestUtils
    {

        public static IDs3Client CreateClient()
        {
            Environment.SetEnvironmentVariable("DS3_ENDPOINT", "http://sm25-2.eng.sldomain.com");
            Environment.SetEnvironmentVariable("DS3_ACCESS_KEY", "c3BlY3RyYQ==");
            Environment.SetEnvironmentVariable("DS3_SECRET_KEY", "uY9JtDZT");//uWvMXihc
            Environment.SetEnvironmentVariable("http_proxy", "");

            return Ds3Builder.FromEnv().Build();
        }

        public static List<Ds3Object> Objects = new List<Ds3Object>
        {
            new Ds3Object("beowulf.txt", 294059),
            new Ds3Object("sherlock_holmes.txt", 581881),
            new Ds3Object("tale_of_two_cities.txt", 776649),
            new Ds3Object("ulysses.txt", 1540095)
        };

        public static void LoadTestData(IDs3Client client, string bucketName)
        {
            PutFiles(client, bucketName, Objects, ReadResource);
        }

        /// <summary>
        /// This will get the object and return the name of the temporary file it was written to.
        /// It is up to the caller to delete the temporary file
        /// </summary>
        public static string GetSingleObject(IDs3Client client, string bucketName, string objectName, int retries = 5)
        {
            string tempFilename = Path.GetTempFileName();

            using (Stream fileStream = new FileStream(tempFilename, FileMode.Truncate, FileAccess.Write))
            {
                
                IDs3ClientHelpers helper = new Ds3ClientHelpers(client, getObjectRetries: retries);

                var job = helper.StartReadJob(bucketName, new List<Ds3Object>{ new Ds3Object(objectName, null)});
            
                job.Transfer(key => fileStream);

                return tempFilename;   
            }
        }

        internal static string GetSingleObjectWithRange(IDs3Client client, string bucketName, string objectName, Range range)
        {
            string tempFilename = Path.GetTempFileName();

            using (Stream fileStream = new FileStream(tempFilename, FileMode.Truncate, FileAccess.Write))
            {

                IDs3ClientHelpers helper = new Ds3ClientHelpers(client);

                var job = helper.StartPartialReadJob(bucketName, new List<string>(), new List<Ds3PartialObject> { new Ds3PartialObject(range, objectName) });
                
                job.Transfer(key => fileStream);

                return tempFilename;
            }
        }

        public static void PutFiles(IDs3Client client, string bucketName, IEnumerable<Ds3Object> files,
            Func<string, Stream> createStreamForTransferItem)
        {

            IDs3ClientHelpers helper = new Ds3ClientHelpers(client);

            helper.EnsureBucketExists(bucketName);

            var job = helper.StartWriteJob(bucketName, files);

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


            client.DeleteObjectList(new DeleteObjectListRequest(bucketName, objs));
            client.DeleteBucket(new DeleteBucketRequest(bucketName));
            
        }

        public static String ComputeSha1(string fileName)
        {
            using (var sha1Managed = new SHA1Managed())
            using (Stream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            using (Stream sha1Stream = new CryptoStream(fileStream, sha1Managed, CryptoStreamMode.Read))
            {
                while (sha1Stream.ReadByte() != -1);
            
                return Convert.ToBase64String(sha1Managed.Hash);
            }
        }

        public static Stream ReadResource(string resourceName)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream("IntegrationTestDS3.TestData." + resourceName);
        }


    }
}
