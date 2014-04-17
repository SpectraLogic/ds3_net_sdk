using Ds3;
using Ds3.Models;

using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;
using Ds3.Calls;


namespace ds3ClassRunner
{
    class Program
    {
        static void Main(string[] args)
        {
           
            //Ds3Client client = new Ds3Builder("http://192.168.56.104:8088", new Credentials("cnlhbg==", "4iDEhFRV")).withRedirectRetries(3).build();

            Ds3Client client = new Ds3Builder(new Uri("http://192.168.56.104:8088"), new Credentials("cnlhbg==", "4iDEhFRV")).WithProxy(new Uri("http://192.168.56.104:8080")).Build();
            /*
            GetServiceResponse response = client.GetService(new GetServiceRequest());

            foreach(Bucket bucket in response.Buckets) {
                Console.WriteLine(bucket.Name);
            } 
             */

            client.GetObject(new GetObjectRequest("bucket", "file.txt"));
            
            //string bucketName = "bulkBucket3";
            //PutBucketResponse bucketRequest = client.PutBucket(new PutBucketRequest(bucketName));             
 
            /*
            string[] fileList = new string[3] {"beowulf.txt", "frankenstein.txt", "ulysses.txt"};
            List<Ds3Object> objects = new List<Ds3Object>();

            foreach (string file in fileList)
            {
                FileInfo info = new FileInfo(file);
                objects.Add(new Ds3Object(file, info.Length));
            }                        

            /
            //BulkGetResponse response = client.BulkGet(new BulkGetRequest(bucketName, objects));
            BulkPutResponse response = client.BulkPut(new BulkPutRequest(bucketName, objects));            

            foreach (List<Ds3Object> objList in response.ObjectLists)
            {
                foreach (Ds3Object obj in objList)
                {
                    PutObjectResponse objResponse = client.PutObject(new PutObjectRequest(bucketName, obj.Name, new FileStream(obj.Name, FileMode.Open)));

                    /*
                    GetObjectResponse objResponse = client.GetObject(new GetObjectRequest(bucketName, obj.Name));
                    using (FileStream outStream = new FileStream(obj.Name + ".copy", FileMode.Create))
                    using (Stream inStream = objResponse.Contents)
                    {
                        inStream.CopyTo(outStream);
                    }
                                          
                }
            }

    
            // Verify all objects were put to the DS3 appliance

            GetBucketResponse bucketResponse = client.GetBucket(new GetBucketRequest(bucketName));

            foreach (Ds3Object obj in bucketResponse.Objects)
            {
                Console.WriteLine(obj.Name);
            }

            Console.WriteLine("Finished Writing all objects to DS3");
            /*
            PutBucketResponse response = client.PutBucket(new PutBucketRequest("testBucket1"));
            Console.WriteLine("Created Bucket");

            GetServiceResponse serviceResponse = client.GetService(new GetServiceRequest());

            foreach(Bucket bucket in serviceResponse.Buckets)
            {
                Console.WriteLine(bucket.Name);
            }

            */
            
            Console.ReadKey();
        }
    }
}
