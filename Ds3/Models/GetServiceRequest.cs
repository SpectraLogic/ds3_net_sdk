using System.Net;

namespace Ds3.Models
{
    public class GetServiceRequest : Ds3Request
    {
        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.GET;
            }
        }

        internal override string Path
        {
            get
            {
                return "/";
            }
        }
    }
}
