using System;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using System.Collections.Generic;

using Ds3.AwsModels;
using Ds3.Runtime;

namespace Ds3.Models
{
    public class BulkResponse : Ds3Response
    {
        private List<List<Ds3Object>> _objectLists;

        /// <summary>
        /// The ordered lists of objects to put or get.
        /// Note that the inner lists may be processed concurrently.
        /// </summary>
        public List<List<Ds3Object>> ObjectLists
        {
            get { return _objectLists; }
        }

        internal BulkResponse(IWebResponse response)
            : base(response)
        {
            HandleStatusCode(HttpStatusCode.OK);
            _objectLists = new List<List<Ds3Object>>();
            ProcessRequest();
        }

        private void ProcessRequest()
        {
            using (Stream content = response.GetResponseStream())
            {
                XmlSerializer serializer = new XmlSerializer(typeof(masterobjectlist));
                masterobjectlist results = (masterobjectlist)serializer.Deserialize(content);

                foreach (masterobjectlistObjects objsList in results.Items)
                {
                    List<Ds3Object> objList = new List<Ds3Object>();
                    _objectLists.Add(objList);

                    foreach (masterobjectlistObjectsObject obj in objsList.@object)
                    {
                        Ds3Object ds3Obj = new Ds3Object(obj.name, Convert.ToInt64(obj.size));
                        objList.Add(ds3Obj);
                    }
                }
            }
        }
    }
}
