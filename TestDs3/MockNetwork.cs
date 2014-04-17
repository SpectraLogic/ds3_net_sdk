using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

using Ds3;
using Ds3.Models;
using Ds3.Runtime;
using Ds3.Calls;

namespace TestDs3
{
    interface IMockNetworkWithExpectation
    {
        IMockNetworkWithReturn Returning(HttpStatusCode statusCode, string responseContent);
    }

    interface IMockNetworkWithReturn
    {
        Ds3Client AsClient { get; }
    }

    class MockNetwork : INetwork, IMockNetworkWithExpectation, IMockNetworkWithReturn
    {
        private HttpVerb _verb;
        private string _path;
        private IDictionary<string, string> _queryParams;
        private string _requestContent;
        private HttpStatusCode _statusCode;
        private string _responseContent;

        public static IMockNetworkWithExpectation Expecting(
            HttpVerb verb,
            string path,
            IDictionary<string, string> queryParams,
            string requestContent)
        {
            var mock = new MockNetwork();
            mock._verb = verb;
            mock._path = path;
            mock._queryParams = queryParams;
            mock._requestContent = requestContent;
            return mock;
        }

        IMockNetworkWithReturn IMockNetworkWithExpectation.Returning(HttpStatusCode statusCode, string responseContent)
        {
            _statusCode = statusCode;
            _responseContent = responseContent;
            return this;
        }

        Ds3Client IMockNetworkWithReturn.AsClient { get { return new Ds3Client(this); } }

        public IWebResponse Invoke(Ds3Request request)
        {
            Assert.AreEqual(_verb, request.Verb);
            Assert.AreEqual(_path, request.Path);
            CollectionAssert.AreEquivalent(_queryParams, request.QueryParams);//TODO: make sure this works correctly.
            Assert.AreEqual(_requestContent, Helpers.ReadContentStream(request));
            return new MockWebResponse(_responseContent, _statusCode);
        }
    }

    class MockWebResponse : IWebResponse
    {
        private readonly string _responseString;
        private readonly HttpStatusCode _statusCode;

        public MockWebResponse(string responseString, HttpStatusCode statusCode)
	    {
            _responseString = responseString;
            _statusCode = statusCode;
	    }

        public Stream GetResponseStream()
        {
            return Helpers.StringToStream(_responseString);
        }

        public HttpStatusCode StatusCode
        {
	        get { return _statusCode; }
        }

        public void Dispose()
        {
        }
    }
}
