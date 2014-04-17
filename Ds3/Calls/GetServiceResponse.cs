using System.IO;
using System.Net;
using System.Linq;
using System.Xml.Linq;
using System.Collections.Generic;

using Ds3.Runtime;
using Ds3.Models;

namespace Ds3.Calls
{
    public class GetServiceResponse : Ds3Response
    {
        public Owner Owner { get; private set; }
        public List<Bucket> Buckets { get; private set; }

        internal GetServiceResponse(IWebResponse responseStream)
            : base(responseStream)
        {
            HandleStatusCode(HttpStatusCode.OK);
            ProcessReponse();
        }

        private void ProcessReponse()
        {
            using (Stream content = response.GetResponseStream())
            {
                var root = XmlExtensions.ReadDocument(content).ElementOrThrow("ListAllMyBucketsResult");

                // Parse owner.
                var owner = root.ElementOrThrow("Owner");
                this.Owner = new Owner(owner.ElementOrThrow("ID").Value, owner.ElementOrThrow("DisplayName").Value);

                // Parse buckets.
                this.Buckets = (
                    from bucket in root.ElementOrThrow("Buckets").Elements("Bucket")
                    select new Bucket(bucket.ElementOrThrow("Name").Value, bucket.ElementOrThrow("CreationDate").Value)
                ).ToList();
            }
        }
    }
}
