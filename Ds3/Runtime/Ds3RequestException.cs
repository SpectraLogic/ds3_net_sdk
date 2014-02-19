using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;

namespace Ds3.Runtime
{
    public class Ds3RequestException : Exception
    {
        public Ds3RequestException(HttpStatusCode expectedStatusCode, HttpStatusCode receivedStatusCode) 
            : base(statusCodeMessage(expectedStatusCode, receivedStatusCode))
        {
        }

        private static string statusCodeMessage(HttpStatusCode expectedStatusCode, HttpStatusCode receivedStatusCode)
        {
            return "Received a status code of " + receivedStatusCode.ToString() + " when " + expectedStatusCode.ToString() + " was expected";
        }
    }
}
