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
            
            Ds3Client client = new Ds3Client("http://10.1.18.8:8080", new Credentials("cnlhbg==", "T8NmDqUh"));

            /*
            GetServiceResponse response = client.GetService(new GetServiceRequest());
            Console.WriteLine(response.Owner.ToString());
            foreach(Bucket bucket in response.Buckets) {
                Console.WriteLine(bucket.Name + ":" + bucket.CreationDate);
            }
            */

            GetBucketResponse bucketResponse = client.GetBucket(new GetBucketRequest("books9"));
            Console.WriteLine(bucketResponse.Name);
            List<Ds3Object> objects = bucketResponse.Objects;
            objects.Reverse();
            foreach (Ds3Object objectName in objects)
            {                    
                DeleteObjectResponse deleteObjectResponse = client.DeleteObject(new DeleteObjectRequest("books9", objectName.Name));
            }

            DeleteBucketResponse deleteBucketResponse = client.DeleteBucket(new DeleteBucketRequest("books9"));

            
            Console.ReadKey();
        }
    }
}
