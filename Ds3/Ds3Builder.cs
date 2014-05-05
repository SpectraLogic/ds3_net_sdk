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

using Ds3.Runtime;

namespace Ds3
{
    public class Ds3Builder
    {
        private Credentials Creds;
        private Uri Endpoint;
        private Uri Proxy = null;
        private int RedirectRetryCount = 5;

        /// <summary>
        /// </summary>
        /// <param name="endpoint">The http or https location at which your DS3 server is listening.</param>
        /// <param name="creds">Credentials with which to specify identity and sign requests.</param>
        public Ds3Builder(string endpoint, Credentials creds) {
            this.Creds = creds;
            this.Endpoint = new Uri(endpoint);
        }

        /// <summary>
        /// </summary>
        /// <param name="endpoint">The http or https location at which your DS3 server is listening.</param>
        /// <param name="creds">Credentials with which to specify identity and sign requests.</param>
        public Ds3Builder(Uri endpoint, Credentials creds)
        {
            this.Creds = creds;
            this.Endpoint = endpoint;
        }

        /// <summary>
        /// Used to specify an HTTP proxy.
        /// </summary>
        /// <param name="proxy"></param>
        /// <returns></returns>
        public Ds3Builder WithProxy(Uri proxy)
        {
            this.Proxy = proxy;
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
            this.RedirectRetryCount = count;
            return this;
        }

        /// <summary>
        /// Creates the Ds3Client using the specified parameters.
        /// </summary>
        /// <returns></returns>
        public IDs3Client Build()
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
