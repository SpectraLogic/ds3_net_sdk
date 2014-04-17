using System;
using System.Net;

namespace Ds3.Runtime
{
    public class Ds3RequestException : Exception
    {
        internal Ds3RequestException(string message)
            : base(message)
        {
        }

        internal Ds3RequestException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
