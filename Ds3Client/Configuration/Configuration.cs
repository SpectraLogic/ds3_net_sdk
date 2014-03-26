using System;
namespace Ds3Client.Configuration
{
    public class Configuration
    {
        public string Name { get; set; }
        public Uri Endpoint { get; set; }
        public Uri Proxy { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public bool IsSelected { get; set; }
    }
}
