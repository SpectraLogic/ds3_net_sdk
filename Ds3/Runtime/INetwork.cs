using System.Net;

using Ds3.Models;

namespace Ds3.Runtime
{
    internal interface INetwork
    {
        IWebResponse Invoke(Ds3Request request);
    }
}
