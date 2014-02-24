using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

using System.Net;
using System.Net.Http;

using Ds3.Models;

namespace Ds3.Runtime
{
    class Network
    {
        public static async Task<T> Invoke<T, K>(K request, Uri endpoint, Credentials creds) where T: Ds3Response where K : Ds3Request
        {
            DateTime date = DateTime.UtcNow;
            UriBuilder uriBuilder = new UriBuilder(endpoint);            
            uriBuilder.Path = request.Path;

            if (request.QueryParams.Count > 0)
            {
                uriBuilder.Query = buildQueryParams(request.QueryParams);
            }

            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(uriBuilder.ToString());
            httpRequest.Method = request.Verb.ToString();
            httpRequest.Date = date;
            httpRequest.Host = endpoint.Host;
            httpRequest.Headers.Add("Authorization", AuthField(creds, request.Verb, date.ToString("r"), request.Path));

            if (request.Verb == HttpVerb.PUT || request.Verb == HttpVerb.POST)
            {
                using (Stream content = request.getContentStream()) {
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
            
            try
            {
                HttpWebResponse httpResponse = (HttpWebResponse)await httpRequest.GetResponseAsync();                
                return CreateResponseInstance<T>(httpResponse);
            }
            catch (WebException e)
            {
                //We want this response to go to the handlers so that they can perform any special actions that they need to.
                return CreateResponseInstance<T>((HttpWebResponse)e.Response);
            }            
        }

        private static T CreateResponseInstance<T>(HttpWebResponse content)
        {
            Type type = typeof(T);            
            return (T)Activator.CreateInstance(type, content);
        }

        private static string FormatedDateString()
        {
            return DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss K");
        }

        private static string AuthField(Credentials creds, HttpVerb verb, string date, string resourcePath, string _md5 = "", string _contentType = "", string _amzHeaders = "")
        {
            string signature = S3Signer.Signature(creds.Key, BuildPayload(verb, date, resourcePath, _md5, _contentType, _amzHeaders));
            return "AWS " + creds.AccessId + ":" + signature;
        }

        private static string BuildPayload(HttpVerb verb, string date, string resourcePath, string md5 = "", string contentType = "", string amzHeaders = "")
        {
            StringBuilder builder = new StringBuilder();

            builder.Append(verb).Append("\n");
            builder.Append(md5).Append("\n");
            builder.Append(contentType).Append("\n");
            builder.Append(date).Append("\n");
            builder.Append(amzHeaders).Append(resourcePath);
            return builder.ToString();
        }

        private static string buildQueryParams(Dictionary<string, string> queryParams)
        {
            List<string> queryList = queryParams.Select(kvp => kvp.Key + "=" + kvp.Value).ToList();
            return String.Join("&", queryList);            
        }
    }
}
