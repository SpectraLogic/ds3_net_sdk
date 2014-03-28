using System.Net;
using Ds3.Models;

namespace Ds3.Runtime
{
    public class Ds3BadStatusCodeException : Ds3RequestException
    {
        private HttpStatusCode _statusCode;
        private Ds3Error _error;
        private string _responseBody;

        public HttpStatusCode StatusCode
        {
            get { return _statusCode; }
        }

        public Ds3Error Error
        {
            get { return _error; }
        }

        public string ResponseBody
        {
            get { return _responseBody; }
        }

        internal Ds3BadStatusCodeException(HttpStatusCode expectedStatusCode, HttpStatusCode receivedStatusCode, Ds3Error error, string responseBody)
            : base(StatusCodeMessage(expectedStatusCode, receivedStatusCode, error))
        {
            this._statusCode = receivedStatusCode;
            this._error = error;
            this._responseBody = responseBody;
        }

        private static string StatusCodeMessage(HttpStatusCode expectedStatusCode, HttpStatusCode receivedStatusCode, Ds3Error error)
        {
            if (error == null)
            {
                return string.Format(Resources.BadStatusCodeInvalidErrorResponseException, receivedStatusCode, expectedStatusCode);
            }
            else
            {
                return string.Format(Resources.BadStatusCodeException, receivedStatusCode, expectedStatusCode, error.Message);
            }
        }
    }
}
