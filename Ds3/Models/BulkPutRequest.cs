using System.IO;
using System.Collections.Generic;
using System.Xml.Serialization;

using Ds3.AwsModels;

namespace Ds3.Models
{
    class BulkPutRequest : Ds3Request
    {

        public override HttpVerb Verb
        {
            get
            {
                return HttpVerb.PUT;
            }
        }

        public override string Path
        {
            get
            {
                return "/" + BucketName;
            }
        }

        private Stream _content;

        public override Stream Content
        {
            get
            {
                return generateStream();
            }
        }

        private string _bucketName;

        public string BucketName
        {
            get { return _bucketName; }
        }

        private List<Ds3Object> _objects;

        public List<Ds3Object> Objets
        {
            get { return _objects; }
        }

        public BulkPutRequest(string bucketName, List<Ds3Object> objects)
        {
            this._bucketName = bucketName;
            this._objects = objects;

            QueryParams.Add("operation", "start_bulk_put");
        }

        private objects convertToAwsModel()
        {
            objects objs = new objects();
            objectsObject[] objList = new objectsObject[_objects.Count];
            
            objs.Items = objList;

            foreach(Ds3Object obj in _objects) 
            {
                objectsObject newObj = new objectsObject();
                newObj.name = obj.Name;
                newObj.size = obj.Size.ToString();
            }

            return objs;
        }

        private Stream generateStream()
        {
            MemoryStream stream = new MemoryStream();
            XmlSerializer serializer = new XmlSerializer(typeof(objects));

            serializer.Serialize(stream, convertToAwsModel());
            return stream;
        }
    }
}
