# DS3 .Net SDK

---

A SDK conforming to the SD3 specification.

## Installing

---

The SDK is distributed as a DLL.  The DLL can be added as a Reference into Visual Studio.

## SdK

---

The SDK provides an interface to communicate with a DS3 compliant appliance.  The primary class that is used to interact with DS3 is the `Ds3Client` class.  The `Ds3Client` class is located in the `Ds3` namespace and is used to communicate with DS3.  Here is an example using the Ds3Client class that lists all the buckets on a remote DS3 appliance.

```csharp

using System;

using Ds3;
using Ds3.Models;

namespace Ds3Example
{

  class ListBucketsExample
  {
    static void Main(string[] args)
    {
      Ds3Client client = new Ds3Client("http://ds3hostname:8080", new Credentials("accessKey", "secretKey"));

      GetServiceResponse response = client.GetService(new GetServiceRequest());

      foreach(Bucket bucket in response.Buckets)
      {
        Console.WriteLine(bucket.Name);
      }
    }
  }
}

```

The SDK allows you to fully communicate with a DS3 appliance.  Each command has a Synchronous version and an Asynchronous version.  The following is a list of commands that can be used to communicate with a DS3 appliance:

* `GetService`
    * Args: `GetServiceRequest`
    * Return: `GetServiceResponse`
        * Properties:
            * `Buckets`: `List<Bucket>` - List of Buckets the user has access to
            * `Owner`: `Owner` - Details on the downer
* `GetBucket`
    * Args: `GetBucketRequest`
        * ConstructorArgs:
            * `BucketName`: `string` - The name of the bucket to get information on
    * Return: `GetBucketResponse`
        * Properties:
            * `Name`: `string` - The name of the bucket
            * `Prefix`: `string` - The prefix used to generate the list of objects
            * `Marker`: `string` -
            * `MaxKeys`: `int` - The maximum number of objects returned in the list
            * `IsTruncated`: `bool` - `True` if the results were truncated
            * `NextMarker`: `string` - Used in pagination
            * `CreationDate`: `DateTime` - The `DateTime` that the bucket was created
            * `Objects`: `List<Ds3Object>` - The list of objects that are contained in the bucket
* `PutBucket`
    * Args: `PutBucketRequest`
        * ConstructorArgs:
            * `BucketName`: `string`  - The name of the new bucket to create
* `DeleteBucket`
    * Args: `DeleteBucketRequest`
        * ConstructorArgs:
            * `BucketName`: `string` - The name of the bucket to delete
    * Return: `DeleteBucketResponse`
        * If the Delete fails a `Ds3RequestException` will be thrown.  Otherwise the object will be returned.  Thise could mean that not all the objects in the bucket have been deleted
* `GetObject`
    * Args: `GetObjectRequest`
        * ConstructorArgs:
            * `BucketName`: `string` - The name of the bucket
            * `ObjectName`: `string` - The name of the object to retrieve
    * Return: `GetObjectResponse`
        * Properties:
            * `Contents`: `Stream` - The data stream containing the requested object
* `PutObject`
    * Args: `PutObjectRequest`
        * ConstructorArgs:
            * `BucketName`: `string` - The name of the bucket to store the object into
            * `ObjectName`: `string` - The name of the new object
            * `Content`: `Stream` - A `Stream` containing the object to put to DS3
    * Return: `PutObjectResponse`
        * If the Put fails a `Ds3RequestException` will be thrown.  Otherwise the object will be returned.
* `DeleteObject`
    * Args: `DeleteObjectRequest`
        * ConstructorArgs:
            * `BucketName`: `string` - The name of the bucket the object is stored in
            * `ObjectName`: `string` - The name of the object to delete
    * Return: `DeleteObjectRequest`
        * If the Delete fails a `Ds3RequestException` will be thrown.  Otherwise the object will be returned.
* `BulkGet`
    * Args: `BulkGetRequest`
        * ConstructorArgs:
            * `BucketName`: `string` - The name of the bucket to get objects from
            * `Objects`: `List<Ds3Object>` - The list of objects that will be used to prime DS3
    * Return: `BulkGetResponse`
        * Properties:
            * `ObjectLists`: `List<List<Ds3Object>>` - The list of lists of objects specifying the order that files should be retrieved in
* `BulkPut`
    * Args: `BulkPutRequest`
        * ConstructorArgs:
            * `BucketName`: `string` - The name of the bucket to get objects from
            * `Objects`: `List<Ds3Object>` - The list of objects that will be used to prime DS3
    * Return: `BulkGetResponse`
        * Properties:
            * `ObjectLists`: `List<List<Ds3Object>>` - The list of lists of objects specifying the order that files should be put in        

## Examples

---

The following are some examples of using the SDK.

### Bulk Put

```csharp

using System;

using Ds3;
using Ds3.Models;

namespace Ds3Example
{

  class BulkPutExample
  {
    static void Main(string[] args)
    {
      Ds3Client client = new Ds3Client("http://192.168.6.138:8080", new Credentials("cnlhbg==", "4iDEhFRV"));
    
      string bucketName = "bulkBucket";
      PutBucketResponse bucketRequest = client.PutBucket(new PutBucketRequest(bucketName));

      //Generate the list of files and their sizes.
      string[] fileList = new string[3] {"beowulf.txt", "frankenstein.txt", "ulysses.txt"};
      List<Ds3Object> objects = new List<Ds3Object>();
    
      foreach (string file in fileList)
      {
        FileInfo info = new FileInfo(file);
        objects.Add(new Ds3Object(file, info.Length));
      }                        
    
      // Create the bulk request.  The DS3 Appliance must first be primed with the Bulk command before
      // individual files are Put.
      BulkPutResponse response = client.BulkPut(new BulkPutRequest(bucketName, objects));            
      foreach (List<Ds3Object> objList in response.ObjectLists)
      {
        foreach (Ds3Object obj in objList)
        {
          PutObjectResponse objResponse = client.PutObject(new PutObjectRequest(bucketName, obj.Name, new FileStream(obj.Name, FileMode.Open)));                          
        }
      }
    
      // Verify all objects were put to the DS3 appliance by listing all the objects in the bucket    
      GetBucketResponse bucketResponse = client.GetBucket(new GetBucketRequest(bucketName));    
      foreach (Ds3Object obj in bucketResponse.Objects)
      {
        Console.WriteLine(obj.Name);
      }
    }
  }
}


```