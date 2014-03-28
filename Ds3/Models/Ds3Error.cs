using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ds3.Models
{
    public class Ds3Error
    {
        public Ds3Error(string code, string message, string resource, string requestId)
        {
            this._code = code;
            this._message = message;
            this._resource = resource;
            this._requestId = requestId;
        }

        private readonly string _code;
        private readonly string _message;
        private readonly string _resource;
        private readonly string _requestId;

        public string Code
        {
            get { return _code; }
        }

        public string Message
        {
            get { return _message; }
        }

        public string Resource
        {
            get { return _resource; }
        }

        public string RequestId
        {
            get { return _requestId; }
        }
    }
}
