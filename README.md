# DS3 .Net SDK

[![Build Status](https://travis-ci.org/SpectraLogic/ds3_net_sdk.svg?branch=v1.2.3)](https://travis-ci.org/SpectraLogic/ds3_net_sdk)
[![Apache V2 License](http://img.shields.io/badge/license-Apache%20V2-blue.svg)](https://github.com/SpectraLogic/ds3_net_sdk/blob/master/LICENSE.md)

An [SDK](http://en.wikipedia.org/wiki/Software_development_kit) conforming to
the DS3 specification.

## Contact Us

Join us at our [Google Groups](https://groups.google.com/d/forum/spectralogicds3-sdks) forum to ask questions, or see frequently asked questions.

## Setting up NuGet

The SDK is distributed as a [NuGet](http://www.nuget.org) package for .Net 4.5.2
and above. From the NuGet website:

> *What is NuGet?*

> NuGet is the package manager for the Microsoft development platform including
> .NET. The NuGet client tools provide the ability to produce and consume
> packages. The NuGet Gallery is the central package repository used by all
> package authors and consumers.

While the DS3 SDK is not yet in the [NuGet
Gallery](http://www.nuget.org/packages), you can easily create a package feed
on your computer using the latest release:

1. Download the .nupkg file from the [Releases](../../releases) page to a new
   directory of your choice.
2. Follow the NuGet instructions on [Creating Local Feeds](http://docs.nuget.org/docs/creating-packages/hosting-your-own-nuget-feeds#Creating_Local_Feeds)
   using the directory that you've created.

Or you can use Spectra Logic Bintray NuGet repository:

To configure the NuGet Visual Studio Extension to use Bintray, you need to add Bintray as another Package.

1. Under the "Tools > Options" menu
2. Select "NuGet Package Manager > Package Sources" and add a new Package Source:

   Name: SpectraLogic .NET SDK (or any other resource name)
   
   Paste the url below into the Source field:
   
   `https://api.bintray.com/nuget/spectralogic/ds3_net`
3. Make sure you have enabled the new source by using the checkbox in the available sources list.

This makes the DS3 SDK available for installation into a Visual Studio .NET
Project.

## Using the Examples Project

The [Releases](../../releases) page has an examples project that references the
DS3 SDK package.

1. Unzip `DS3Examples.zip` and open the `DS3Examples.sln` file in Visual
   Studio.
2. Edit the `App.config` file to use the endpoint, access key, and secret key
   for your DS3 applicance.
3. Right-click the DS3Examples project in the Solution Explorer and click
   "Properties".
4. Choose which of the four examples you'd like to run from the "Startup
   Object" dropdown box.
5. Press the F5 key to run the program within Visual Studio.

Each of the example programs in the project contains a description at the top
explaining what the program does.

## Installing the DS3 SDK Into Your Own Project

You can also install the SDK into your own project.

1. Open your existing .NET project or create a new one.
2. Right-click the project and click "Manage NuGet Packages..."
3. Click "Online" on the left panel.
4. In the search box on the upper right, type "DS3".
5. Click the "Install" button next to the "DS3 .NET SDK" package and close the
   package manager dialog.

Your project should now reference the SDK and be able to use its API.

## About the API

The SDK consists of two levels of abstraction:

1. A high level interface
   ([`Ds3.Helpers.IDs3ClientHelpers`](http://spectralogic.github.io/ds3_net_sdk/3.0.0/interface_ds3_1_1_helpers_1_1_i_ds3_client_helpers.html))
   that abstracts several very common application requirements.
2. The core client interface
   ([`Ds3.IDs3Client`](http://spectralogic.github.io/ds3_net_sdk/3.0.0/interface_ds3_1_1_i_ds3_client.html))
   whose method calls each result in exactly one HTTP request.


Some aspects of the low-level Amazon S3 and Spectra Logic DS3 requests require
a fair amount of non-obvious boilerplate logic that's the same in every
application. Thus we _strongly_ recommend that all SDK users use the higher
level interface wherever possible.

As an example, the standard Amazon S3 request to list objects in a bucket only
returns 1,000 results at a time and must be called repeatedly with paging
parameters to get a complete list. Since the code to do this will likely be the
same regardless of the application, we've created the
[`ListObjects`](http://spectralogic.github.io/ds3_net_sdk/3.0.0/interface_ds3_1_1_helpers_1_1_i_ds3_client_helpers.html#aa5255c4e1bc7b4fe515dea0e6d519147)
method to handle this paging for you.

## Instantiating the API

The example below shows how to configure and instantiate `Ds3.IDs3Client` and
`Ds3.Helpers.IDs3ClientHelpers`.

```csharp
using Ds3;
using Ds3.Helpers;
using System.Configuration;

namespace YourApplication
{
    class YourClass
    {
        public void YourMethod()
        {
            // Configure and build the core client.
            IDs3Client client = new Ds3Builder(
                "http://ds3-endpoint",
                new Credentials("access key", "secrete key")
            ).Build();

            // Set up the high-level abstractions.
            IDs3ClientHelpers helpers = new Ds3ClientHelpers(client);

            // Use functionality from 'helpers' and 'client', preferring 'helpers'.
            // ...
        }
    }
}
```

## SDK Development Resources

[Running Unit Tests](../../wiki/Running-Unit-Tests)

[Building from Source](../../wiki/Building-from-Source)

[Building the DS3 SDK NuGet Package](../../wiki/Building-the-DS3-SDK-NuGet-package)

API documentation resides in the gh-pages branch. See the README.md there for information on how to regenerate the API documentation.

