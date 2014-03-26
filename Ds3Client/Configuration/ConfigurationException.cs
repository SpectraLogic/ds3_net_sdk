using System;

namespace Ds3Client.Configuration
{
    public class ConfigurationException : Exception
    {
        public ConfigurationException(string message, params object[] parameters)
            : base(string.Format(message, parameters))
        {
        }
    }
}
