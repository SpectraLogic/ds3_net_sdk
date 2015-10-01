/*
 * ******************************************************************************
 *   Copyright 2014 Spectra Logic Corporation. All Rights Reserved.
 *   Licensed under the Apache License, Version 2.0 (the "License"). You may not use
 *   this file except in compliance with the License. A copy of the License is located at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 *   or in the "license" file accompanying this file.
 *   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
 *   CONDITIONS OF ANY KIND, either express or implied. See the License for the
 *   specific language governing permissions and limitations under the License.
 * ****************************************************************************
 */

using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

using Ds3.Models;
using Ds3.Calls;

namespace Ds3.Runtime
{
    internal class Network : INetwork
    {
        private static TraceSwitch sdkNetworkSwitch = new TraceSwitch("sdkNetworkSwitch", "set in config file");

        internal const int DefaultCopyBufferSize = 1 * 1024 * 1024;

        private readonly Uri _endpoint;
        private readonly Credentials _creds;
        private readonly int _maxRedirects = 0;
        private readonly int _redirectRetryCount;
        private readonly int _readWriteTimeout;
        private readonly int _requestTimeout;
        private readonly int _connectionLimit;

        internal Uri Proxy = null;
        private readonly char[] _noChars = new char[0];

        public Network(
            Uri endpoint,
            Credentials creds,
            int redirectRetryCount,
            int copyBufferSize,
            int readWriteTimeout,
            int requestTimeout,
            int connectionLimit)
        {
            this._endpoint = endpoint;
            this._creds = creds;
            this._redirectRetryCount = redirectRetryCount;
            this.CopyBufferSize = copyBufferSize;
            this._readWriteTimeout = readWriteTimeout;
            this._requestTimeout = requestTimeout;
            this._connectionLimit = connectionLimit;
        }

        public int CopyBufferSize { get; private set; }

        public IWebResponse Invoke(Ds3Request request)
        {
            int redirectCount = 0;

            using (var content = request.GetContentStream())
            {
                do
                {
                    if (sdkNetworkSwitch.TraceInfo) { Trace.WriteLine(string.Format(Resources.Request_Logging, request.GetType().ToString())); }
                    if (sdkNetworkSwitch.TraceVerbose) { Trace.WriteLine(request.getDescription(BuildQueryParams(request.QueryParams))); }

                    HttpWebRequest httpRequest = CreateRequest(request, content);
                    try
                    {
                        long send = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                        var response = new WebResponse((HttpWebResponse)httpRequest.GetResponse());
                        long millis = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - send;
                        if (Is307(response))
                        {
                            redirectCount++;
                            if (sdkNetworkSwitch.TraceWarning) { Trace.Write(string.Format(Resources.Encountered307NTimes, redirectCount), "Ds3Network"); }
                        }
                        else
                        {
                            if (sdkNetworkSwitch.TraceInfo) { Trace.WriteLine(string.Format(Resources.ResponseLogging, response.StatusCode.ToString(), millis)); }
                            return response;
                        }
                    }
                    catch (WebException e)
                    {
                        if (e.Response == null)
                        {
                            throw e;
                        }
                        return new WebResponse((HttpWebResponse)e.Response);
                    }
                } while (redirectCount < _maxRedirects);
            }

            throw new Ds3RedirectLimitException(Resources.TooManyRedirectsException);
        }

        private HttpWebRequest CreateRequest(Ds3Request request, Stream content)
        {
            if (request.Verb == HttpVerb.PUT || request.Verb == HttpVerb.POST)
            {
                if (content != Stream.Null && !content.CanRead)
                {
                    throw new Ds3.Runtime.Ds3RequestException(Resources.InvalidStreamException);
                }
            }

            DateTime date = DateTime.UtcNow;
            UriBuilder uriBuilder = new UriBuilder(_endpoint);
            uriBuilder.Path = HttpHelper.PercentEncodePath(request.Path);

            if (request.QueryParams.Count > 0)
            {
                uriBuilder.Query = BuildQueryParams(request.QueryParams);
            }

            HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(uriBuilder.ToString());
            httpRequest.ServicePoint.ConnectionLimit = _connectionLimit;
            httpRequest.Method = request.Verb.ToString();
            if (Proxy != null)
            {
                WebProxy webProxy = new WebProxy();
                webProxy.Address = Proxy;
                httpRequest.Proxy = webProxy;
            }
            httpRequest.Date = date;
            httpRequest.Host = CreateHostString(_endpoint);
            httpRequest.AllowAutoRedirect = false;
            httpRequest.AllowWriteStreamBuffering = false;
            httpRequest.ReadWriteTimeout = this._readWriteTimeout;
            httpRequest.Timeout = this._requestTimeout;

			var chucksumValue = ComputeChecksum(request.ChecksumObject, content, request.ChecksumType);
			if (!string.IsNullOrEmpty(chucksumValue))
			{
				switch (request.ChecksumType)
				{
					case Checksum.ChecksumType.Md5:
						if (sdkNetworkSwitch.TraceVerbose) Trace.WriteLine($"MD5 checksum is {chucksumValue}");
						httpRequest.Headers.Add(HttpHeaders.ContentMd5, chucksumValue);
						break;
					case Checksum.ChecksumType.Sha256:
						if (sdkNetworkSwitch.TraceVerbose) Trace.WriteLine($"SHA-256 checksum is {chucksumValue}");
						httpRequest.Headers.Add(HttpHeaders.ContentSha256, chucksumValue);
						break;
					case Checksum.ChecksumType.Sha512:
						if (sdkNetworkSwitch.TraceVerbose) Trace.WriteLine($"SHA-512 checksum is {chucksumValue}");
						httpRequest.Headers.Add(HttpHeaders.ContentSha512, chucksumValue);
						break;
				}
			}
	        httpRequest.Headers.Add(HttpHeaders.Authorization, S3Signer.AuthField(
                _creds,
                request.Verb.ToString(),
                date.ToString("r"),
                request.Path,
                request.QueryParams,
                chucksumValue,
                amzHeaders: request.Headers
            ));

            foreach (var byteRange in request.GetByteRanges())
            {
                httpRequest.AddRange(byteRange.Start, byteRange.End);
            }
            
            foreach (var header in request.Headers)
            {
                httpRequest.Headers.Add(header.Key, header.Value);
            }

            if (request.Verb == HttpVerb.PUT || request.Verb == HttpVerb.POST)
            {
                httpRequest.ContentLength = content.Length;
                if (content != Stream.Null)
                {
                    using (var requestStream = httpRequest.GetRequestStream())
                    {
                        if (content.Position != 0)
                        {
                            content.Seek(0, SeekOrigin.Begin);
                        }
                        content.CopyTo(requestStream, this.CopyBufferSize);
                        requestStream.Flush();
                    }
                }
            }
            return httpRequest;
        }

        private static string ComputeChecksum(Checksum checksum, Stream content, Checksum.ChecksumType checksumType = Checksum.ChecksumType.Md5)
        {
            return checksum.Match(
							 () => "",
							 () =>
							 {
								switch (checksumType)
								 {
									 case Checksum.ChecksumType.Md5:
										 return Convert.ToBase64String(System.Security.Cryptography.MD5.Create().ComputeHash(content));
									 case Checksum.ChecksumType.Sha256:
										 return Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(content));
									 case Checksum.ChecksumType.Sha512:
										 return Convert.ToBase64String(System.Security.Cryptography.SHA512.Create().ComputeHash(content));
									 default:
										 return "";
								 }
							 },
							 hash => Convert.ToBase64String(hash)
							 );
		}

        private string CreateHostString(Uri endpoint)
        {
            if(endpoint.Port > 0) {
                return endpoint.Host + ":" + endpoint.Port;
            }
            return endpoint.Host;
        }

        private bool Is307(IWebResponse httpResponse)
        {
            return httpResponse.StatusCode.Equals(HttpStatusCode.TemporaryRedirect);
        }

        private string FormatedDateString()
        {
            return DateTime.Now.ToString("ddd, dd MMM yyyy HH:mm:ss K");
        }

        private string BuildQueryParams(Dictionary<string, string> queryParams)
        {
            return String.Join(
                "&",
                from kvp in queryParams
                orderby kvp.Key
                let encodedKey = HttpHelper.PercentEncodePath(kvp.Key, _noChars)
                select kvp.Value.Length > 0
                    ? encodedKey + "=" + HttpHelper.PercentEncodePath(kvp.Value, _noChars)
                    : encodedKey
            );
        }
    }
}
