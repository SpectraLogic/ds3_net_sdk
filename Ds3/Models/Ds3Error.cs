using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ds3.Models
{
    public class Ds3Error
    {
        public string Code { get; private set; }
        public string Message { get; private set; }
        public string Resource { get; private set; }
        public string RequestId { get; private set; }

        internal Ds3Error(string code, string message, string resource, string requestId)
        {
            this.Code = code;
            this.Message = message;
            this.Resource = resource;
            this.RequestId = requestId;
        }
    }
}
