using System.Net;

namespace Ds3.Runtime
{
    class Ds3BadStatusCodeException : Ds3RequestException
    {

                private HttpStatusCode _statusCode;

        public HttpStatusCode StatusCode
        {
            get { return _statusCode; }
        }

        public Ds3BadStatusCodeException(HttpStatusCode expectedStatusCode, HttpStatusCode receivedStatusCode) 
            : base(statusCodeMessage(expectedStatusCode, receivedStatusCode))
        {
            this._statusCode = receivedStatusCode;
        }

        private static string statusCodeMessage(HttpStatusCode expectedStatusCode, HttpStatusCode receivedStatusCode)
        {
            return "Received a status code of " + receivedStatusCode.ToString() + " when " + expectedStatusCode.ToString() + " was expected";
        }
    }
}
