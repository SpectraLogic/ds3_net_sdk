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
            var redirectCount = 0;

            using (var content = request.GetContentStream())
            {
                do
                {
                    if (sdkNetworkSwitch.TraceInfo) { Trace.WriteLine(string.Format(Resources.RequestLogging, request.GetType().ToString())); }
                    if (sdkNetworkSwitch.TraceVerbose) { Trace.WriteLine(request.getDescription(BuildQueryParams(request.QueryParams))); }

                    var httpRequest = CreateRequest(request, content);
                    try
                    {
                        var send = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;
                        var response = new WebResponse((HttpWebResponse)httpRequest.GetResponse());
                        var millis = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - send;
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
                } while (redirectCount < _redirectRetryCount);
            }

            throw new Ds3RedirectLimitException(Resources.TooManyRedirectsException);
        }

        private HttpWebRequest CreateRequest(Ds3Request request, Stream content)
        {
            if (request.Verb == HttpVerb.PUT || request.Verb == HttpVerb.POST)
            {
                if (content != Stream.Null && !content.CanRead)
                {
                    throw new Ds3RequestException(Resources.InvalidStreamException);
                }
            }

            var date = DateTime.UtcNow;
            var uriBuilder = new UriBuilder(_endpoint);
            uriBuilder.Path = HttpHelper.PercentEncodePath(request.Path);

            if (request.QueryParams.Count > 0)
            {
                uriBuilder.Query = BuildQueryParams(request.QueryParams);
            }

            var httpRequest = (HttpWebRequest)WebRequest.Create(uriBuilder.ToString());
            httpRequest.ServicePoint.ConnectionLimit = _connectionLimit;
            httpRequest.Method = request.Verb.ToString();
            if (Proxy != null)
            {
                var webProxy = new WebProxy();
                webProxy.Address = Proxy;
                httpRequest.Proxy = webProxy;
            }
            httpRequest.Date = date;
            httpRequest.Host = CreateHostString(_endpoint);
            httpRequest.AllowAutoRedirect = false;
            httpRequest.AllowWriteStreamBuffering = false;
            // hangs on Mono's NET framework for DELETEs (NETSDK-58)
            if (request.Verb == HttpVerb.DELETE)
            {
                httpRequest.AllowWriteStreamBuffering = true;
            }
            httpRequest.ReadWriteTimeout = this._readWriteTimeout;
            httpRequest.Timeout = this._requestTimeout;

            var chucksumValue = ComputeChecksum(request.ChecksumValue, content, request.CType);
            if (!string.IsNullOrEmpty(chucksumValue))
            {
                switch (request.CType)
                {
                    case ChecksumType.Type.MD5:
                        if (sdkNetworkSwitch.TraceVerbose) Trace.WriteLine(string.Format("MD5 checksum is {0}", chucksumValue));
                        httpRequest.Headers.Add(HttpHeaders.ContentMd5, chucksumValue);
                        break;
                    case ChecksumType.Type.SHA_256:
                        if (sdkNetworkSwitch.TraceVerbose) Trace.WriteLine(string.Format("SHA-256 checksum is {0}", chucksumValue));
                        httpRequest.Headers.Add(HttpHeaders.ContentSha256, chucksumValue);
                        break;
                    case ChecksumType.Type.SHA_512:
                        if (sdkNetworkSwitch.TraceVerbose) Trace.WriteLine(string.Format("SHA-512 checksum is {0}", chucksumValue));
                        httpRequest.Headers.Add(HttpHeaders.ContentSha512, chucksumValue);
                        break;
                    case ChecksumType.Type.CRC_32:
                        if (sdkNetworkSwitch.TraceVerbose) Trace.WriteLine(string.Format("Crc32 checksum is {0}", chucksumValue));
                        httpRequest.Headers.Add(HttpHeaders.ContentCRC32, chucksumValue);
                        break;
                    case ChecksumType.Type.CRC_32C:
                        if (sdkNetworkSwitch.TraceVerbose) Trace.WriteLine(string.Format("Crc32C checksum is {0}", chucksumValue));
                        httpRequest.Headers.Add(HttpHeaders.ContentCRC32C, chucksumValue);
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
                httpRequest.ContentLength = request.GetContentLength();
                if (content != Stream.Null)
                {
                    var ds3ContentLengthNotMatchCatched = false;
                    Ds3ContentLengthNotMatch ds3ContentLengthNotMatch = null;
                    try
                    {
                        using (var requestStream = httpRequest.GetRequestStream())
                        {
                            if (content.CanSeek && content.Position != 0)
                            {
                                content.Seek(0, SeekOrigin.Begin);
                            }
                            using (var webStream = new WebStream(content, request.GetContentLength()))
                            {
                                try
                                {
                                    webStream.CopyTo(requestStream, this.CopyBufferSize);
                                }
                                catch (Ds3ContentLengthNotMatch ex)
                                {
                                    ds3ContentLengthNotMatchCatched = true;
                                    ds3ContentLengthNotMatch = ex;
                                    throw ds3ContentLengthNotMatch;
                                }
                                catch (Exception ex)
                                {
                                    const string windowsMessage = "Bytes to be written to the stream exceed the Content-Length bytes size specified.";
                                    const string monoMessage = "The number of bytes to be written is greater than the specified ContentLength.";
                                    if (ex.Message.Equals(windowsMessage) || ex.Message.Equals(monoMessage))
                                    {
                                        ds3ContentLengthNotMatchCatched = true;
                                        ds3ContentLengthNotMatch = new Ds3ContentLengthNotMatch(ex.Message, ex);
                                        throw ds3ContentLengthNotMatch;
                                    }
                                    throw;
                                }
                                requestStream.Flush();
                            }
                        }
                    }
                    //if only Ds3ContentLengthNotMatch was thrown than just re-throw it
                    catch (Ds3ContentLengthNotMatch)
                    {
                        throw;
                    }
                    //if an Exception was thrown from closing requestStream in finally block and also Ds3ContentLengthNotMatch than we will aggregate them,
                    //and if not than just re-throw
                    catch (Exception ex)
                    {
                        if (!ds3ContentLengthNotMatchCatched) throw;

                        var innerExceptions = ds3ContentLengthNotMatch.InnerExceptions.Select(e=>e).ToList();
                        innerExceptions.Add(ex);
                        throw new Ds3ContentLengthNotMatch(ds3ContentLengthNotMatch.Message, innerExceptions);
                    }
                }
            }
            return httpRequest;
        }

        private static string ComputeChecksum(ChecksumType checksum, Stream content, ChecksumType.Type type = ChecksumType.Type.MD5)
        {
            return checksum.Match(
                () => "",
                () =>
                {
                    switch (type)
                    {
                        case ChecksumType.Type.MD5:
                            return Convert.ToBase64String(System.Security.Cryptography.MD5.Create().ComputeHash(content));
                        case ChecksumType.Type.SHA_256:
                            return
                                Convert.ToBase64String(System.Security.Cryptography.SHA256.Create().ComputeHash(content));
                        case ChecksumType.Type.SHA_512:
                            return
                                Convert.ToBase64String(System.Security.Cryptography.SHA512.Create().ComputeHash(content));
                        case ChecksumType.Type.CRC_32:
                            return
                                Convert.ToBase64String(Ds3.Models.Crc32.Create().ComputeHash(content));
                        case ChecksumType.Type.CRC_32C:
                            return
                                Convert.ToBase64String(Ds3.Models.Crc32C.Create().ComputeHash(content));
                        default:
                            return "";
                    }
                },
                hash => Convert.ToBase64String(hash)
                );
        }

        private string CreateHostString(Uri endpoint)
        {
            if (endpoint.Port > 0)
            {
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
                select kvp.Value != null && kvp.Value.Length > 0
                    ? encodedKey + "=" + HttpHelper.PercentEncodePath(kvp.Value, _noChars)
                    : encodedKey
            );
        }
    }
}