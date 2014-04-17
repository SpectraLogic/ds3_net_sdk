using System.Net;
using System.IO;
using System.Collections.Generic;

namespace Ds3.Calls
{
    public abstract class Ds3Request
    {
        public class Range
        {
            public long Start { get; private set; }
            public long End { get; private set; }

            public Range(long start, long end)
            {
                this.Start = start;
                this.End = end;
            }
        }

        internal abstract HttpVerb Verb
        {
            get;
        }
        
        internal abstract string Path
        {
            get;   
        }

        internal virtual Stream GetContentStream() {
            return Stream.Null;
        }

        internal virtual Range GetByteRange()
        {
            return null;
        }

        private Dictionary<string, string> _queryParams = new Dictionary<string, string>();
        internal virtual Dictionary<string,string> QueryParams
        {
            get { return _queryParams; }
        }

        private Dictionary<string, string> _headers = new Dictionary<string, string>();
        internal virtual Dictionary<string, string> Headers
        {
            get { return _headers; }
        }
    }

    internal enum HttpVerb {GET, PUT, POST, DELETE, HEAD, PATCH};
}
