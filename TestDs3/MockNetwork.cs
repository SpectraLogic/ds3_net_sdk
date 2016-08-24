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

using Ds3;
using Ds3.Calls;
using Ds3.Runtime;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;

namespace TestDs3
{
    internal interface IMockNetworkWithExpectation
    {
        IMockNetworkWithReturn Returning(HttpStatusCode statusCode, string responseContent, IDictionary<string, string> responseHeaders);
    }

    interface IMockNetworkWithReturn
    {
        IDs3Client AsClient { get; }
    }

    internal class MockNetwork : INetwork, IMockNetworkWithExpectation, IMockNetworkWithReturn
    {
        private HttpVerb _verb;
        private string _path;
        private IDictionary<string, string> _queryParams;
        private string _requestContent;
        private HttpStatusCode _statusCode;
        private string _responseContent;
        private IDictionary<string, string> _responseHeaders;
        private IDictionary<string, string> _requestHeaders;

        public static IMockNetworkWithExpectation Expecting(
            HttpVerb verb,
            string path,
            IDictionary<string, string> queryParams,
            string requestContent)
        {
            var mock = new MockNetwork
            {
                _verb = verb,
                _path = path,
                _queryParams = queryParams,
                _requestContent = requestContent
            };
            return mock;
        }

        public static IMockNetworkWithExpectation Expecting(
            HttpVerb verb,
            string path,
            IDictionary<string, string> queryParams,
            IDictionary<string, string> requestHeaders,
            string requestContent)
        {
            var mock = new MockNetwork
            {
                _verb = verb,
                _path = path,
                _queryParams = queryParams,
                _requestHeaders = requestHeaders,
                _requestContent = requestContent
            };
            return mock;
        }

        IMockNetworkWithReturn IMockNetworkWithExpectation.Returning(HttpStatusCode statusCode, string responseContent, IDictionary<string, string> responseHeaders)
        {
            _statusCode = statusCode;
            _responseContent = responseContent;
            _responseHeaders = responseHeaders.ToDictionary(kvp => kvp.Key.ToLowerInvariant(), kvp => kvp.Value);
            return this;
        }

        IDs3Client IMockNetworkWithReturn.AsClient { get { return new Ds3Client(this); } }

        public IWebResponse Invoke(Ds3Request request)
        {
            Assert.AreEqual(_verb, request.Verb);
            Assert.AreEqual(_path, request.Path);
            CollectionAssert.AreEquivalent(_queryParams, request.QueryParams);
            if (_requestHeaders != null)
            {
                CollectionAssert.AreEquivalent(_requestHeaders, request.Headers);
            }
            Assert.AreEqual(_requestContent, HelpersForTest.StringFromStream(request.GetContentStream()));
            return new MockWebResponse(_responseContent, _statusCode, _responseHeaders);
        }

        public int CopyBufferSize
        {
            get { return Network.DefaultCopyBufferSize; }
        }
    }

    internal class MockWebResponse : IWebResponse
    {
        private readonly string _responseString;
        private readonly HttpStatusCode _statusCode;
        private readonly IDictionary<string, string> _responseHeaders;

        public MockWebResponse(string responseString, HttpStatusCode statusCode, IDictionary<string, string> responseHeaders)
        {
            _responseString = responseString;
            _statusCode = statusCode;
            _responseHeaders = responseHeaders;
        }

        public Stream GetResponseStream()
        {
            return HelpersForTest.StringToStream(_responseString);
        }

        public HttpStatusCode StatusCode
        {
            get { return _statusCode; }
        }

        public IDictionary<string, string> Headers
        {
            get { return _responseHeaders; }
        }

        public void Dispose()
        {
        }
    }

    internal class MockWebRequest : IWebRequest
    {
        private readonly MockWebResponse _mockWebResponse;

        public MockWebRequest(MockWebResponse mockWebResponse)
        {
            _mockWebResponse = mockWebResponse;
        }
        public IWebResponse GetResponse()
        {
            return _mockWebResponse;
        }
    }
}
