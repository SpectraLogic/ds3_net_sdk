using Ds3;
using Ds3.Models;
using Ds3.AwsModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.IO;


namespace ds3ClassRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Ds3Client client = new Ds3Client("http://10.1.18.8:8080", new Credentials("cnlhbg==", "T8NmDqUh"));

                GetServiceResponse response = client.GetService(new GetServiceRequest());
                Console.WriteLine(response.Owner.ToString());
                foreach(Bucket bucket in response.Buckets) {
                    Console.WriteLine(bucket.Name + ":" + bucket.CreationDate);
                }

                GetBucketResponse bucketResponse = client.GetBucket(new GetBucketRequest("books3"));
                Console.WriteLine(bucketResponse.Name);
                foreach (Ds3Object objectName in bucketResponse.Objects)
                {
                    Console.WriteLine(objectName.Name + ": " + objectName.Size);
                }

                GetObjectResponse objectResponse = client.GetObject(new GetObjectRequest("books3", "user/hduser/books/beowulf.txt"));

                using (Stream objStream = objectResponse.Contents)
                using (FileStream writer = new FileStream("text.txt", FileMode.Create))
                {
                    objStream.CopyTo(writer);
                }

            }
            catch(Exception e) {
                System.Console.WriteLine("Failed with exception:" + e.ToString());
            }
            Console.ReadKey();
        }
    }
}
