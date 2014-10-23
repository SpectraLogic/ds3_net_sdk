# DS3 .Net SDK

An [SDK](http://en.wikipedia.org/wiki/Software_development_kit) and Windows PowerShell [CLI](http://en.wikipedia.org/wiki/Command-line_interface) conforming to the DS3 specification.

See the [PowerShell DS3 Module Documentation](../../wiki/PowerShell-DS3-Module) for more information on using DS3 from the command line on Windows.

## Getting Started with the SDK

The SDK is distributed as a DLL.  The DLL can be added as a Reference into Visual Studio and supports .Net 4.0 and above. You can download the latest ds3_net_sdk.dll from the [Releases](../../releases) page.

The [`Ds3.IDs3Client`](http://spectralogic.github.io/ds3_net_sdk/interface_ds3_1_1_i_ds3_client.html) interface provides the core functionality of the SDK. You can create an instance of it using the [`Ds3.Ds3Builder`](http://spectralogic.github.io/ds3_net_sdk/class_ds3_1_1_ds3_builder.html) class. See the examples below for a starting point. More detailed documentation can be found in the [SDK API Reference](http://spectralogic.github.io/ds3_net_sdk/annotated.html).

## Examples

This example lists all of the buckets available to the specified user on a remote DS3 appliance.

```csharp

using System;

using Ds3;
using Ds3.Calls;
using Ds3.Models;

namespace Ds3Example
{
    class ListBucketsExample
    {
        static void Main(string[] args)
        {
            IDs3Client client = new Ds3Builder("http://ds3hostname:8080",
              new Credentials("accessKey", "secretKey")).Build();

            GetServiceResponse response = client.GetService(new GetServiceRequest());

            foreach (Bucket bucket in response.Buckets)
            {
                Console.WriteLine(bucket.Name);
            }
        }
    }
}

```

This example performs a bulk put of three files to a remote DS3 appliance and retrieves the list of objects to verify that the operations succeeded.

```csharp
using System;
using System.IO;
using System.Collections.Generic;

using Ds3;
using Ds3.Calls;
using Ds3.Models;

namespace Ds3Example
{
    class BulkPutExample
    {
        static void Main(string[] args)
        {
            IDs3Client client = new Ds3Builder("http://192.168.6.138:8080",
              new Credentials("cnlhbg==", "4iDEhFRV")).Build();

            string bucketName = "bulkBucket";
            client.PutBucket(new PutBucketRequest(bucketName));

            // Generate the list of files and their sizes.
            string[] fileList = new string[3] { "beowulf.txt", "frankenstein.txt", "ulysses.txt" };
            List<Ds3Object> objects = new List<Ds3Object>();

            foreach (string file in fileList)
            {
                FileInfo info = new FileInfo(file);
                objects.Add(new Ds3Object(file, info.Length));
            }

            // Create the bulk request.  The DS3 Appliance must first be primed
            // with the Bulk command before individual files are Put.
            var response = client.BulkPut(new BulkPutRequest(bucketName, objects));
            foreach (var objList in response.ObjectLists)
            {
                foreach (var obj in objList)
                {
                    client.PutObject(new PutObjectRequest(bucketName, obj.Name,
                          new FileStream(obj.Name, FileMode.Open)));
                }
            }

            // Verify all objects were put to the DS3 appliance by listing all the
            // objects in the bucket    
            GetBucketResponse bucketResponse = client.GetBucket(
              new GetBucketRequest(bucketName));
            foreach (Ds3Object obj in bucketResponse.Objects)
            {
                Console.WriteLine(obj.Name);
            }
        }
    }
}

```

## SDK Development Resources

[Running Unit Tests](../../wiki/Running-Unit-Tests)

[Building from Source](../../wiki/Building-from-Source)

[Building the DS3 SDK NuGet Package](../../wiki/Building-the-DS3-SDK-NuGet-package)


API documentation resides in the gh-pages branch. See the README.md there for information on how to regenerate the documentation.

