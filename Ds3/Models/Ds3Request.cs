using System.Net;

namespace Ds3.Models
{
    public abstract class Ds3Request
    {
        public abstract HttpVerb Verb
        {
            get;
        }
        
        public abstract string Path
        {
            get;   
        }

        public abstract HttpStatusCode StatusCode
        {
            get;
        }

    }

    public enum HttpVerb {GET, PUT, POST, DELETE, HEAD, PATCH};
}
