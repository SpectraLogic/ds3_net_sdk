using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

using System.Net;
//using System.Net.Http;

using Ds3.Models;

namespace Ds3.Runtime
{
    internal class Network
    {

        private Uri Endpoint;
        private Credentials Creds;
        private int MaxRedirects = 0;

        internal Uri Proxy = null;

        public Network(Uri endpoint, Credentials creds, int maxRedirects)
        {
            this.Endpoint = endpoint;
            this.Creds = creds;
            this.MaxRedirects = maxRedirects;
        }

        public HttpWebResponse Invoke<K>(K request) where K : Ds3Request
        {
            bool redirect = false;
            int redirectCount = 0;            

            do
            {
                HttpWebRequest httpRequest = createRequest(request);
                try
                {
                    HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                    if (Is307(httpResponse))
                    {
                        redirect = true;
                        redirectCount++;
                        Trace.Write("Encountered 307 number: " + redirectCount, "Ds3Network");
                        continue;
                    }
                    return httpResponse;
                }
                catch (WebException e)
                {
                    if (e.Response == null)
                    {
                        throw e;
                    }
                    return (HttpWebResponse)e.Response;
                }
            } while (redirect && redirectCount < MaxRedirects);

            throw new Ds3RedirectLimitException("Too many redirects.");      
        }

        private HttpWebRequest createRequest<K>(K request) where K : Ds3Request
        {
            DateTime date = DateTime.UtcNow;
            UriBuilder uriBuilder = new UriBuilder(Endpoint);
            uriBuilder.Path = request.Path;

            if (request.QueryParams.Count > 0)
            {
                uriBuilder.Query = BuildQueryParams(request.QueryParams);
            }

            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(uriBuilder.ToString());
            httpRequest.Method = request.Verb.ToString();
            if (Proxy != null)
            {
                WebProxy webProxy = new WebProxy();
                webProxy.Address = Proxy;
                httpRequest.Proxy = webProxy;
            }
            httpRequest.Date = date;
            httpRequest.Host = CreateHostString(Endpoint);
            httpRequest.AllowAutoRedirect = false;
            httpRequest.Headers.Add("Authorization", S3Signer.AuthField(Creds, request.Verb.ToString(), date.ToString("r"), request.Path));

            if (request.Verb == HttpVerb.PUT || request.Verb == HttpVerb.POST)
            {
                using (Stream content = request.getContentStream())
                {
                    httpRequest.ContentLength = content.Length;
                    using (Stream requestStream = httpRequest.GetRequestStream())
                    {
                        if (content != Stream.Null)
                        {
                            content.CopyTo(requestStream);
                            requestStream.Flush();
                        }
                    }
                }
            }
            return httpRequest;
        }

        private string CreateHostString(Uri endpoint)
        {
            if(endpoint.Port > 0) {
                return endpoint.Host + ":" + endpoint.Port;
            }
            return endpoint.Host;
        }

        private bool Is307(HttpWebResponse httpResponse)
        {
            return httpResponse.StatusCode.Equals(HttpStatusCode.TemporaryRedirect);
        }

        private string FormatedDateString()
        {
            return DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss K");
        }

        private string BuildQueryParams(Dictionary<string, string> queryParams)
        {
            List<string> queryList = queryParams.Select(kvp => kvp.Key + "=" + kvp.Value).ToList();
            return String.Join("&", queryList);            
        }

    }
}
