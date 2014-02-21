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

            /*
            Ds3Client client = new Ds3Client("http://10.1.18.8:8080", new Credentials("cnlhbg==", "T8NmDqUh"));

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
