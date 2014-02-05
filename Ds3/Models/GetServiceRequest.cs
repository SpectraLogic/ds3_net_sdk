using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
