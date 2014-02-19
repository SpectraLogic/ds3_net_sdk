using System.Net;

namespace Ds3.Models
{
    public class GetServiceRequest : Ds3Request
    {
        public override HttpVerb Verb
        {
            get
            {
                return HttpVerb.GET;
            }
        }

        public override string Path
        {
            get
            {
                return "/";
            }
        }

        public override HttpStatusCode StatusCode
        {
            get { return HttpStatusCode.OK; }
        }
    }
}
