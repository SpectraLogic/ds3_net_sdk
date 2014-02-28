using Ds3;
using Ds3.Models;
using Ds3.AwsModels;

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Xml.Serialization;


namespace ds3ClassRunner
{
    class Program
    {
        static void Main(string[] args)
        {   
            
            string bucketName = "bulkTest1";

            Ds3Client client = new Ds3Client("http://192.168.6.156:8080", new Credentials("cnlhbg==", "4iDEhFRV"));

            //PutBucketResponse bucketRequest = client.PutBucket(new PutBucketRequest(bucketName));
            //Console.WriteLine("Created bucket: " + bucketName);

            List<Ds3Object> objects = new List<Ds3Object>();
            objects.Add(new Ds3Object("beowulf.txt", 301063));
            objects.Add(new Ds3Object("frankenstein.txt", 448689));
            objects.Add(new Ds3Object("ulysses.txt", 2547454));

            BulkGetResponse response = client.BulkGet(new BulkGetRequest(bucketName, objects));
            //BulkPutResponse response = client.BulkPut(new BulkPutRequest(bucketName, objects));

            Console.WriteLine("Bulk Get Prime came back.");

            foreach (List<Ds3Object> objList in response.ObjectLists)
            {
                foreach (Ds3Object obj in objList)
                {
                    //PutObjectResponse objResponse = client.PutObject(new PutObjectRequest(bucketName, obj.Name, new FileStream(obj.Name, FileMode.Open)));

                    
                    GetObjectResponse objResponse = client.GetObject(new GetObjectRequest(bucketName, obj.Name));
                    using (FileStream outStream = new FileStream(obj.Name + ".copy", FileMode.Create))
                    using (Stream inStream = objResponse.Contents)
                    {
                        inStream.CopyTo(outStream);
                    }
                     
                }
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
