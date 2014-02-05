using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ds3.Models
{
    public abstract class Ds3Request
    {
        public abstract HttpVerb Verb{
            get;
        }
        public abstract string Path{
            get;   
        }


    }

    public enum HttpVerb {GET, PUT, POST, DELETE, HEAD, PATCH};
}
