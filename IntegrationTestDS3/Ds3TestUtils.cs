using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Ds3;
using Ds3.Calls;
using Ds3.Helpers;
using Ds3.Models;
using System.Reflection;
using System.IO;
using System.Security.Cryptography;

namespace IntegrationTestDS3
{
    internal class Ds3TestUtils
    {

        public static List<Ds3Object> Objects = new List<Ds3Object>
        {
            new Ds3Object("beowulf.txt", 294059),
            new Ds3Object("sherlock_holmes.txt", 581881),
            new Ds3Object("tale_of_two_cities.txt", 776649),
            new Ds3Object("ulysses.txt", 1540095)
        };

        public static void LoadTestData(IDs3Client client, string bucketName)
        {
            IDs3ClientHelpers helper = new Ds3ClientHelpers(client);

            helper.EnsureBucketExists(bucketName);

            var job = helper.StartWriteJob(bucketName, Objects);

            job.Transfer(key => ReadResource(key));

        }

        /// <summary>
        /// This will get the object and return the name of the temporary file it was written to.
        /// It is up to the caller to delete the temporary file
        /// </summary>
        public static string GetSingleObject(IDs3Client client, string bucketName, string objectName)
        {
            string tempFilename = Path.GetTempFileName();

            using (Stream fileStream = new FileStream(tempFilename, FileMode.Truncate, FileAccess.Write))
            {

                IDs3ClientHelpers helper = new Ds3ClientHelpers(client);

                var job = helper.StartReadJob(bucketName, new List<Ds3Object>{ new Ds3Object(objectName, null)});
            
                job.Transfer(key => fileStream);

                return tempFilename;   
            }
        }

        public static void DeleteBucket(IDs3Client client, string bucketName)
        {
            if (client.HeadBucket(new HeadBucketRequest(bucketName)).Status == Ds3.Calls.HeadBucketResponse.StatusType.DoesntExist)
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
