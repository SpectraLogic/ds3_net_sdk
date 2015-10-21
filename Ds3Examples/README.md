
# DS3 .Net SDK Examples

A light example client exploring the DS3 .Net SDK for DS3 transfers.

## Contact Us

Join us at our [Google Groups](https://groups.google.com/d/forum/spectralogicds3-sdks) forum to ask questions, or see frequently asked questions.

## Quick Start

The Releases page has an examples project that references the DS3 SDK package.
1.Install the Black Pearl Simulator (https://developer.spectralogic.com/downloads/) or connect to a DS3 appliance.
2.Set the environment variables for [DS3_ENDPOINT, DS3_SECRET_KEY, DS3_ACCESS_KEY](https://developer.spectralogic.com/sim-install/) and optionally http_proxy. 
3.Download and build the ds3_net_dsk (https://github.com/SpectraLogic/ds3_net_sdk)
4.Set Ds3Examples as start up project in Visual Studio and ensure that Ds3Examples.Ds3ExampleClient is the startup object.
5.Start debugging (F5) and Ds3Examples.Ds3ExampleClient.main() should execute. This will copy a few files to an example bucket on the device. list them objects, restore to a directory typically under Ds3Examples/bin/Debug, then delete the objects and the new bucket.
6.Optionally, set the variables sourcedir and destdir in Ds3Examples.Ds3ExampleClient.main() to recursively copy files from your local filesystem (sourcedir) and restore them (destdir).

## Installing the DS3 SDK Into Your Own Project

You can also install the SDK into your own project.
1.Open your existing .NET project or create a new one.
2.Right-click the project and click "Manage NuGet Packages..."
3.Click "Online" on the left panel.
4.In the search box on the upper right, type "DS3".
5.Click the "Install" button next to the "DS3 .NET SDK" package and close the package manager dialog.

Your project should now reference the SDK and be able to use its API.

## About the API

The SDK consists of two levels of abstraction:

1. A high level interface
   ([`Ds3.Helpers.IDs3ClientHelpers`](http://spectralogic.github.io/ds3_net_sdk/api/interface_ds3_1_1_helpers_1_1_i_ds3_client_helpers.html))
   that abstracts several very common application requirements.
2. The core client interface
   ([`Ds3.IDs3Client`](http://spectralogic.github.io/ds3_net_sdk/api/interface_ds3_1_1_i_ds3_client.html))
   whose method calls each result in exactly one HTTP request.


Some aspects of the low-level Amazon S3 and Spectra Logic DS3 requests require a fair amount of non-obvious boilerplate logic that's the same in every application. Thus we strongly recommend that all SDK users use the higher level interface wherever possible.

As an example, the standard Amazon S3 request to list objects in a bucket only
returns 1,000 results at a time and must be called repeatedly with paging
parameters to get a complete list. Since the code to do this will likely be the
same regardless of the application, we've created the
[`ListObjects`](http://spectralogic.github.io/ds3_net_sdk/api/interface_ds3_1_1_helpers_1_1_i_ds3_client_helpers.html#aa5255c4e1bc7b4fe515dea0e6d519147)
method to handle this paging for you.

## SDK Development Resources

[Running Unit Tests](../../wiki/Running-Unit-Tests)

[Building from Source](../../wiki/Building-from-Source)

[Building the DS3 SDK NuGet Package](../../wiki/Building-the-DS3-SDK-NuGet-package)

API documentation resides in the gh-pages branch. See the README.md there for information on how to regenerate the API documentation.

