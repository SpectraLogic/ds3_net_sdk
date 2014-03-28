using System.Net;
using Ds3.Models;

namespace Ds3.Runtime
{
    class Ds3BadStatusCodeException : Ds3RequestException
    {
        private HttpStatusCode _statusCode;
        private Ds3Error _error;

        public HttpStatusCode StatusCode
        {
            get { return _statusCode; }
        }

        public Ds3Error Error
        {
            get { return _error; }
        }

        public Ds3BadStatusCodeException(HttpStatusCode expectedStatusCode, HttpStatusCode receivedStatusCode, Ds3Error error)
            : base(StatusCodeMessage(expectedStatusCode, receivedStatusCode, error))
        {
            this._statusCode = receivedStatusCode;
            this._error = error;
        }

        private static string StatusCodeMessage(HttpStatusCode expectedStatusCode, HttpStatusCode receivedStatusCode, Ds3Error error)
        {
            return string.Format(Resources.BadStatusCodeException, receivedStatusCode, expectedStatusCode, error.Message);
        }
    }
}
