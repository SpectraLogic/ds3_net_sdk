using System.Net;
using System.IO;

using Ds3.Runtime;

namespace Ds3.Calls
{
    public class GetObjectResponse : Ds3Response
    {
        private Stream _contents;

        /// <summary>
        /// The contents of the object. Disposing of GetObjectResponse will also dispose of this stream.
        /// </summary>
        public Stream Contents
        {
            get { return _contents; }
        }

        internal GetObjectResponse(IWebResponse responseStream) 
            : base(responseStream)
        {
            HandleStatusCode(HttpStatusCode.OK);
            ProcessResponse();   
        }

        private void ProcessResponse()
        {
            _contents = response.GetResponseStream();
        }
    }
}
