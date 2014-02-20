using System.Net;
using System.IO;

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

        public virtual Stream Content
        {
            get { return Stream.Null; }
        }
    }

    public enum HttpVerb {GET, PUT, POST, DELETE, HEAD, PATCH};
}
