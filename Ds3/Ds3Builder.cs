/*
 * ******************************************************************************
 *   Copyright 2014-2017 Spectra Logic Corporation. All Rights Reserved.
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
using Ds3.Runtime;

namespace Ds3
{
    public class Ds3Builder
    {
        private static readonly TraceSwitch Log = new TraceSwitch("Ds3", "set in config file");

        private readonly Credentials _creds;
        private readonly Uri _endpoint;
        private Uri _proxy;
        private int _redirectRetryCount = 5;
        private int _copyBufferSize = Network.DefaultCopyBufferSize;
        private int _readWriteTimeout = 12 * 60 * 60 * 1000; // 12H
        private int _requestTimeout = 12 * 60 * 60 * 1000; // 12H
        private int _connectionLimit = 12;

        /// <summary>
        /// Creates a Ds3Builder with the endpoint, credentials, and proxy all populated from
        /// environment variables.
        /// </summary>
        public static Ds3Builder FromEnv()
        {
            var endpoint = Environment.GetEnvironmentVariable("DS3_ENDPOINT");
            var accesskey = Environment.GetEnvironmentVariable("DS3_ACCESS_KEY");
            var secretkey = Environment.GetEnvironmentVariable("DS3_SECRET_KEY");
            var proxy = Environment.GetEnvironmentVariable("http_proxy");

            var credentials = new Credentials(accesskey, secretkey);
            var builder = new Ds3Builder(endpoint, credentials);
            if (!string.IsNullOrEmpty(proxy))
            {
                builder.WithProxy(new Uri(proxy));
            }

            return builder;
        }

        /// <summary>
        /// </summary>
        /// <param name="endpoint">The HTTP or HTTPS location at which your DS3 server is listening.</param>
        /// <param name="creds">Credentials with which to specify identity and sign requests.</param>
        public Ds3Builder(string endpoint, Credentials creds) {
            this._creds = creds;
            this._endpoint = new Uri(endpoint);
        }

        /// <summary>
        /// </summary>
        /// <param name="endpoint">The HTTP or HTTPS location at which your DS3 server is listening.</param>
        /// <param name="creds">Credentials with which to specify identity and sign requests.</param>
        public Ds3Builder(Uri endpoint, Credentials creds)
        {
            this._creds = creds;
            this._endpoint = endpoint;
        }

        /// <summary>
        /// Used to specify an HTTP proxy.
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        public Ds3Builder WithProxy(Uri proxy)
        {
            this._proxy = proxy;
            return this;
        }

        /// <summary>
        /// Sometimes the DS3 server isn't ready to service a request. In these
        /// situations it will periodically respond with a 307 redirect to keep
        /// the connection alive. The SDK makes this transparent if it happens up to RedirectRetries times.
        /// Use this to specify a limit on how many times this should happen before throwing an exception.
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public Ds3Builder WithRedirectRetries(int count)
        {
            this._redirectRetryCount = count;
            return this;
        }

        /// <summary>
        /// Determines how many bytes to copy at a time to and from input streams when
        /// performing object GET and PUT operations.
        /// </summary>
        /// <param name="copyBufferSize"></param>
        /// <returns></returns>
        public Ds3Builder WithCopyBufferSize(int copyBufferSize)
        {
            this._copyBufferSize = copyBufferSize;
            return this;
        }

        /// <summary>
        /// Specifies how long to wait in milliseconds for an HTTP request or response to transfer.
        /// </summary>
        /// <param name="readWriteTimeout"></param>
        /// <returns></returns>
        public Ds3Builder WithReadWriteTimeout(int readWriteTimeout)
        {
            this._readWriteTimeout = readWriteTimeout;
            return this;
        }

        /// <summary>
        /// Specifies how long to wait in milliseconds for the server to respond once the HTTP request has been fully sent.
        /// </summary>
        /// <param name="requestTimeout"></param>
        /// <returns></returns>
        public Ds3Builder WithRequestTimeout(int requestTimeout)
        {
            this._requestTimeout = requestTimeout;
            return this;
        }

        /// <summary>
        /// Specifies how many concurrent connections we can open to a single host.
        /// </summary>
        /// <param name="connectionLimit"></param>
        /// <returns></returns>
        public Ds3Builder WithConnectionLimit(int connectionLimit)
        {
            this._connectionLimit = connectionLimit;
            return this;
        }

        /// <summary>
        /// Creates the Ds3Client using the specified parameters.
        /// </summary>
        /// <returns></returns>
        public IDs3Client Build()
        {
            if (IsHttpsScheme() && RuntimeUtils.IsRunningOnMono())
            {
                throw new Exception(Resources.HttpsNotSupportedOnMono);
            }

            if (Log.TraceVerbose)
            {
                Trace.TraceInformation("Creating network layer with:\n" +
"\tendpoint = {0}\n" +
"\taccessId = {1}\n" +
"\tredirectRetryCount = {2}\n" +
"\tcopyBufferSize = {3}\n" +
"\treadWriteTimeout = {4}\n" +
"\trequestTimeout = {5}\n" +
"\tconnectionLimit = {6}\n" +
"\tproxy = {7}",
_endpoint, _creds.AccessId, _redirectRetryCount, _copyBufferSize, _readWriteTimeout, _requestTimeout, _connectionLimit, _proxy);
            }

            var netLayer = new Network(
                _endpoint,
                _creds,
                _redirectRetryCount,
                _copyBufferSize,
                _readWriteTimeout,
                _requestTimeout,
                _connectionLimit
            );
            if (_proxy != null)
            {
                netLayer.Proxy = _proxy;
            }
            return new Ds3Client(netLayer);
        }

        private bool IsHttpsScheme()
        {
            return _endpoint.Scheme.ToLower().Equals("https");
        }
    }
}
