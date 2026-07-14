# DS3 .Net SDK

[![Apache V2 License](http://img.shields.io/badge/license-Apache%20V2-blue.svg)](https://github.com/SpectraLogic/ds3_net_sdk/blob/master/LICENSE.md)

An [SDK](http://en.wikipedia.org/wiki/Software_development_kit) conforming to
the DS3 specification.

## Contact Us

Join us at our [Google Groups](https://groups.google.com/d/forum/spectralogicds3-sdks) forum to ask questions, or see frequently asked questions.

## Setting up NuGet

The SDK is distributed as a [NuGet](http://www.nuget.org) package. The library multi-targets **.NET Standard 2.0** and **.NET Framework 4.5.1**, so it can be consumed from .NET Framework 4.5.1+ and any .NET 5+/8+/10+ project.

### Installation

The easiest way to install the SDK is via the `dotnet` CLI:

```bash
dotnet add package Ds3
```

Or via the NuGet Package Manager in your IDE by searching for `Ds3`.

---

## Legacy Support

> **Note:** Instructions for versions **v5.4.0 and below** (legacy .NET Framework 4.5.1 only) have been moved to the [Legacy Documentation](../../wiki/Legacy-Home).

---

## Using the Examples Project

The [Releases](../../releases) page has an examples project that references the
DS3 SDK package.

1. Unzip `DS3Examples.zip` and open the `DS3Examples.sln` file in Rider, Visual Studio, or use the `dotnet` CLI.
2. Edit the `App.config` file to use your DS3 appliance credentials.
3. Run the examples using your IDE's run configuration or `dotnet run --project Ds3Examples`.

## Installing the DS3 SDK Into Your Own Project

You can install the SDK into your own project using the `dotnet` CLI:

```bash
dotnet add package Ds3
```

Or via the NuGet Package Manager in your IDE.

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

## Creating a New Release

Update the version of the SDK before creating a new release. The format is `<major>.<minor>.<patch>`, where the 
`<major>.<minor>` numbers must match the version of BP. The `<patch>` is an incrementing number that increments with 
each SDK release for a given major/minor release.

The version is located in:
* `VersionInfo.cs`: Used for assembly versioning.
* `Ds3/Ds3.csproj`: Used for NuGet package versioning (`<Version>` tag).

Build the NuGet package with:

```bash
make pack
# or
dotnet pack Ds3/Ds3.csproj -c Release -o nupkgs
```

