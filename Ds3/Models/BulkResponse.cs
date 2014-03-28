using System;
using System.IO;
using System.Net;
using System.Xml.Serialization;
using System.Collections.Generic;

using Ds3.AwsModels;

namespace Ds3.Models
{
    public class BulkResponse : Ds3Response
    {
        private List<List<Ds3Object>> _objectLists;

        public List<List<Ds3Object>> ObjectLists
        {
            get { return _objectLists; }
        }


        public BulkResponse(HttpWebResponse response) : base(response)
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
                        Ds3Object ds3Obj = new Ds3Object(obj.name);
                        objList.Add(ds3Obj);
                    }
                }
            }
        }
    }
}
