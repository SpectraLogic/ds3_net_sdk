using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net;

using Ds3.Runtime;
namespace Ds3.Models
{
    public class DeleteObjectResponse : Ds3Response
    {
        public DeleteObjectResponse(HttpWebResponse response)
            : base(response)
        {
            processResponse();
        }

        private void processResponse()
        {
            HttpStatusCode statusCode = response.StatusCode;
            if (!statusCode.Equals(HttpStatusCode.NoContent))
            {
                throw new Ds3RequestException(HttpStatusCode.NoContent, statusCode);
            }
        }
    }
}
