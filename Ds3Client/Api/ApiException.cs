using System;

namespace Ds3Client.Api
{
    public class ApiException : Exception
    {
        public ApiException(string message, params object[] parameters)
            : base(string.Format(message, parameters))
        {
        }
    }
}
