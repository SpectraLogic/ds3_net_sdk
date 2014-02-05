using System;
using System.IO;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;

namespace Ds3
{
    public class Ds3Client
    {
        private Credentials Creds;
        private Uri Endpoint;        

        public Ds3Client(string endpoint, Credentials creds) {
            this.Creds = creds;
            this.Endpoint = new Uri(endpoint);            
        }
        
        private string Signature(string key, string payload)
        {
            Console.WriteLine(payload);
            HMACSHA1 hmac = new HMACSHA1(System.Text.Encoding.UTF8.GetBytes(key));
            byte[] hashResult = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(payload));            
            return System.Convert.ToBase64String(hashResult).Trim();
        }

        private string AuthField(string verb, string date, string resourcePath, string _md5 = "", string _contentType = "", string _amzHeaders = "")
        {
            string signature = Signature(Creds.Key, BuildPayload(verb, date, resourcePath, _md5, _contentType, _amzHeaders));
            Console.WriteLine(signature);
            return "AWS " + Creds.AccessId + ":" + signature;
        }

        private string BuildPayload(string verb, string date, string resourcePath, string md5="", string contentType="", string amzHeaders="") {
            StringBuilder builder = new StringBuilder();
            builder.Append(verb).Append("\n");
            builder.Append(md5).Append("\n");
            builder.Append(contentType).Append("\n");
            builder.Append(date).Append("\n");
            builder.Append(amzHeaders).Append(resourcePath);
            return builder.ToString();
        }

        private string FormatedDateString()
        {
            //return "Thu, 30 Jan 2014 11:24:48 -07:00";
            return DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss K");
        }

        public string GetService()
        {
            //string formatedDate = FormatedDateString();
            //Console.WriteLine(formatedDate);
            DateTime date = DateTime.UtcNow;

            UriBuilder uriBuilder = new UriBuilder(this.Endpoint);
            uriBuilder.Path = "/";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriBuilder.ToString());
            request.Method = "GET";
            request.Date = date;
            request.Host = this.Endpoint.Host;
            
            
            //request.Headers.Add("Date", formatedDate);
            request.Headers.Add("Authorization", AuthField("GET", date.ToString("r"), "/"));

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();


            if (response.StatusCode.Equals(200))
            {
                throw new Exception("Bad response code: " + response.StatusCode.ToString());
            }

            Stream content = response.GetResponseStream();
            byte[] contentBuff = new byte[response.ContentLength];
            content.Read(contentBuff, 0, (int)response.ContentLength);
            response.Close();

            return System.Text.Encoding.UTF8.GetString(contentBuff);
            /*
            using (HttpClient client = new HttpClient())
            {
                                
            

                Console.WriteLine(uriBuilder.Uri.ToString());

                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Get, uriBuilder.Uri);
            
                
                Console.WriteLine("Getting request");
                var response = client.SendAsync(request);                
                if (response.Result.IsSuccessStatusCode)
                {
                    Console.WriteLine("Got response back");
                    var responseContent = response.Result.Content;

                    return responseContent.ReadAsStringAsync().Result;
                }
                else
                {
                    throw new Exception("Bad response code: " + response.Result.StatusCode);
                }
            }
             * 
             * */
            
        }

        public static string FormatXml(String Xml)
        {
            try
            {
                XDocument doc = XDocument.Parse(Xml);                
                return doc.ToString();
                
            }
            catch (Exception)
            {
                return Xml;
            }
        }

    }
}
