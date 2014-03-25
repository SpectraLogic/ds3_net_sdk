using System;

using Ds3.Runtime;

namespace Ds3
{
    public class Ds3Builder
    {
        private Credentials Creds;
        private Uri Endpoint;
        private Uri Proxy = null;
        private int RedirectRetryCount = 5;
                
        public Ds3Builder(string endpoint, Credentials creds) {
            this.Creds = creds;
            this.Endpoint = new Uri(endpoint);
        }

        public Ds3Builder(Uri endpoint, Credentials creds)
        {
            this.Creds = creds;
            this.Endpoint = endpoint;
        }

        public Ds3Builder WithProxy(Uri proxy)
        {
            this.Proxy = proxy;
            return this;
        }

        public Ds3Builder WithRedirectRetries(int count)
        {
            this.RedirectRetryCount = count;
            return this;
        }

        public Ds3Client Build()
        {
            Network netLayer = new Network(Endpoint, Creds, RedirectRetryCount);
            if (Proxy != null)
            {
                netLayer.Proxy =Proxy;
            }
            return new Ds3Client(netLayer);
        }
    }
}
