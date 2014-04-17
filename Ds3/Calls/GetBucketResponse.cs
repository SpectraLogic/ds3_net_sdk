using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Collections.Generic;

using Ds3.Runtime;
using Ds3.Models;

namespace Ds3.Calls
{
    public class GetBucketResponse : Ds3Response
    {
        public string Name { get; private set; }
        public string Prefix { get; private set; }
        public string Marker { get; private set; }
        public int MaxKeys { get; private set; }
        public bool IsTruncated { get; private set; }
        public string Delimiter { get; private set; }
        public string NextMarker { get; private set; }
        public DateTime CreationDate { get; private set; }
        public List<Ds3Object> Objects { get; private set; }

        internal GetBucketResponse(IWebResponse responseStream)
            : base(responseStream)
        {
            HandleStatusCode(HttpStatusCode.OK);
            ProcessResponse();
        }

        private void ProcessResponse()
        {            
            using (Stream content = response.GetResponseStream())
            {
                var root = XmlExtensions.ReadDocument(content).ElementOrThrow("ListBucketResult");
                Name = root.TextOf("Name");
                Prefix = root.TextOf("Prefix");
                Marker = root.TextOf("Marker");
                NextMarker = root.TextOfOrNull("NextMarker");
                Delimiter = root.TextOfOrNull("Delimiter");
                MaxKeys = int.Parse(root.TextOf("MaxKeys"));
                IsTruncated = bool.Parse(root.TextOf("IsTruncated"));
                CreationDate = Convert.ToDateTime(root.TextOfOrNull("CreationDate"));
                Objects = (
                    from obj in root.Elements("Contents")
                    let owner = obj.ElementOrThrow("Owner")
                    select new Ds3Object(
                        obj.TextOf("Key"),
                        int.Parse(obj.TextOf("Size")),
                        new Owner(owner.TextOf("ID"), owner.TextOf("DisplayName")),
                        obj.TextOf("ETag"),
                        obj.TextOf("StorageClass"),
                        Convert.ToDateTime(obj.TextOf("LastModified"))
                    )
                ).ToList();
            }
        }
    }
}
