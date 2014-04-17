using System;
using System.IO;
using System.Net;
using System.Linq;
using System.Collections.Generic;

using Ds3.Runtime;
using Ds3.Models;

namespace Ds3.Calls
{
    public class BulkResponse : Ds3Response
    {
        /// <summary>
        /// The ordered lists of objects to put or get.
        /// Note that the inner lists may be processed concurrently.
        /// </summary>
        public List<List<Ds3Object>> ObjectLists { get; private set; }

        internal BulkResponse(IWebResponse response)
            : base(response)
        {
            HandleStatusCode(HttpStatusCode.OK);
            ProcessRequest();
        }

        private void ProcessRequest()
        {
            using (Stream content = response.GetResponseStream())
            {
                ObjectLists = (
                    from objs in XmlExtensions
                        .ReadDocument(content)
                        .ElementOrThrow("masterobjectlist")
                        .Elements("objects")
                    select (
                        from obj in objs.Elements("object")
                        select new Ds3Object(
                            obj.AttributeOrThrow("name").Value,
                            Convert.ToInt64(obj.AttributeOrThrow("size").Value)
                        )
                    ).ToList()
                ).ToList();
            }
        }
    }
}
