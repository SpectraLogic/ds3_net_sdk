using System.Net;

using Ds3.Models;
using Ds3.Calls;

namespace Ds3.Runtime
{
    internal interface INetwork
    {
        IWebResponse Invoke(Ds3Request request);
    }
}
