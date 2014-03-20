using System;
using System.Net;

namespace Ds3.Runtime
{
    public class Ds3RequestException : Exception
    {
        public Ds3RequestException(string message)
            : base(message)
        {
        }

    }
}
