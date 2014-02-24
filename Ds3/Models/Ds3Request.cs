using System.Net;
using System.IO;
using System.Collections.Generic;

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

        public virtual Stream getContentStream() {
            return Stream.Null;
        }            

        private Dictionary<string, string> _queryParams = new Dictionary<string, string>();
        public virtual Dictionary<string,string> QueryParams
        {
            get { return _queryParams; }
        }
    }

    public enum HttpVerb {GET, PUT, POST, DELETE, HEAD, PATCH};
}
