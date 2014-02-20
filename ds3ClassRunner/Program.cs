using Ds3;
using Ds3.Models;
using Ds3.AwsModels;

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;


namespace ds3ClassRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            
            Ds3Client client = new Ds3Client("http://10.1.18.8:8080", new Credentials("cnlhbg==", "T8NmDqUh"));

            PutBucketResponse response = client.PutBucket(new PutBucketRequest("testBucket1"));
            Console.WriteLine("Created Bucket");

            GetServiceResponse serviceResponse = client.GetService(new GetServiceRequest());

            foreach(Bucket bucket in serviceResponse.Buckets)
            {
                Console.WriteLine(bucket.Name);
            }
            /*
            using (FileStream inStream = new FileStream("out.txt", FileMode.Open))
            {
                PutObjectResponse response = client.PutObject(new PutObjectRequest("books8", "out.txt", inStream));
                Console.WriteLine("Finished writting file.");
            }

            /*
            GetObjectResponse response = client.GetObject(new GetObjectRequest("books8", "user/hduser/books/huckfinn.txt"));

            using (FileStream outStream = new FileStream("out.txt", FileMode.Create))
            using (Stream content = response.Contents)
            {
                content.CopyTo(outStream);
            }            
            
            GetBucketResponse response = client.GetBucket(new GetBucketRequest("books8"));

            foreach (Ds3Object obj in response.Objects)
            {
                Console.WriteLine(obj.Name);
            }

            /*
            GetServiceResponse response = client.GetService(new GetServiceRequest());
            Console.WriteLine(response.Owner.ToString());
            foreach(Bucket bucket in response.Buckets) {
                Console.WriteLine(bucket.Name + ":" + bucket.CreationDate);
            }
            

            GetBucketResponse bucketResponse = client.GetBucket(new GetBucketRequest("books9"));
            Console.WriteLine(bucketResponse.Name);
            List<Ds3Object> objects = bucketResponse.Objects;
            objects.Reverse();
            foreach (Ds3Object objectName in objects)
            {                    
                DeleteObjectResponse deleteObjectResponse = client.DeleteObject(new DeleteObjectRequest("books9", objectName.Name));
            }

            DeleteBucketResponse deleteBucketResponse = client.DeleteBucket(new DeleteBucketRequest("books9"));
            */
            
            Console.ReadKey();
        }
    }
}
