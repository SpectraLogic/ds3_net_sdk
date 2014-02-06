using Ds3;
using Ds3.Models;
using Ds3.AwsModels;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ds3ClassRunner
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Ds3Client client = new Ds3Client("http://10.1.19.180:8080", new Credentials("cnlhbg==", "VJ4sBGMu"));

                GetServiceResponse response = client.GetService(new GetServiceRequest());
                Console.WriteLine(response.Owner.ToString());
                foreach(Bucket bucket in response.Buckets) {
                    Console.WriteLine(bucket.Name + ":" + bucket.CreationDate);
                }

                GetBucketResponse bucketResponse = client.GetBucket(new GetBucketRequest("test"));
                Console.WriteLine(bucketResponse.BucketName);

            }
            catch(Exception e) {
                System.Console.WriteLine("Failed with exception:" + e.ToString());
            }
            Console.ReadKey();
        }
    }
}
