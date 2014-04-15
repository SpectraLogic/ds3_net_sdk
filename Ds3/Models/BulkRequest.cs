using System;
using System.IO;
using System.Xml.Serialization;
using System.Collections.Generic;

using Ds3.Models;
using Ds3.AwsModels;

namespace Ds3.Models
{
    public abstract class BulkRequest : Ds3Request
    {
        internal override HttpVerb Verb
        {
            get
            {
                return HttpVerb.PUT;
            }
        }

        internal override string Path
        {
            get
            {
                return "/_rest_/buckets/" + BucketName;
            }
        }

        public string BucketName { get; private set; }
        public List<Ds3Object> Objects { get; private set; }

        public BulkRequest(string bucketName, List<Ds3Object> objectList)
        {
            this.BucketName = bucketName;
            this.Objects = objectList;
        }

        internal override Stream GetContentStream()
        {
            return GenerateObjectStream(Objects);
        }

        private objects ConvertToAwsModel(List<Ds3Object> objects)
        {
            objects objs = new objects();            

            List<objectsObject> objsList = new List<objectsObject>();

            foreach (Ds3Object obj in objects)
            {
                objectsObject newObj = new objectsObject();

                newObj.name = obj.Name;
                newObj.size = obj.Size.ToString();                
                objsList.Add(newObj);                
            }
            objs.Items = objsList.ToArray();

            return objs;
        }

        protected Stream GenerateObjectStream(List<Ds3Object> objects)
        {           
            MemoryStream stream = new MemoryStream();            
            XmlSerializer serializer = new XmlSerializer(typeof(objects));
          
            serializer.Serialize(stream, ConvertToAwsModel(objects));
            StreamReader reader = new StreamReader(stream);
            
            stream.Seek(0, SeekOrigin.Begin);
            return stream;
        }
    }
}
