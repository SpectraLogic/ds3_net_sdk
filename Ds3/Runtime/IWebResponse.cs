using System;
using System.IO;
using System.Net;

namespace Ds3.Runtime
{
    internal interface IWebResponse : IDisposable
    {
        Stream GetResponseStream();
        HttpStatusCode StatusCode { get; }
    }
}
