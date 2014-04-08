using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Xml.Serialization;

using Ds3.AwsModels;
using Ds3.Runtime;

namespace Ds3.Models
{
    public class GetBucketResponse : Ds3Response
    {
        private DateTime _creationDate;

        private string _delimiter;

        private bool _isTruncated;

        private string _marker;

        private int _maxKeys;
        
        private string _name;

        private string _nextMarker;

        private string _prefix;
        
        public string Name
        {
            get { return _name; }
        }
        
        public string Prefix
        {
            get { return _prefix; }
        }
        
        public string Marker
        {
            get { return _marker; }
        }
        
        public int MaxKeys
        {
            get { return _maxKeys; }
        }
        
        public bool IsTruncated
        {
            get { return _isTruncated; }
        }

        public string Delimiter
        {
            get { return _delimiter; }
        }

        public string NextMarker
        {
            get { return _nextMarker; }
        }

        public DateTime CreationDate
        {
            get { return _creationDate; }
        }

        private List<Ds3Object> _objects;

        public List<Ds3Object> Objects
        {
            get { return _objects; }
        }

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
                XmlSerializer serializer = new XmlSerializer(typeof(ListBucketResult));
                ListBucketResult results = (ListBucketResult)serializer.Deserialize(content);

                _name = results.Name;
                _prefix = results.Prefix;
                _marker = results.Marker;
                _nextMarker = results.NextMarker;
                _delimiter = results.Delimiter;
                _maxKeys = int.Parse(results.MaxKeys);
                _isTruncated = bool.Parse(results.IsTruncated);
                _creationDate = Convert.ToDateTime(results.CreationDate);

                if (results.Contents != null)
                {
                    _objects = results.Contents.Select(ds3Obj => new Ds3Object(
                        ds3Obj.Key,
                        int.Parse(ds3Obj.Size),
                        new Owner(ds3Obj.Owner[0].ID, ds3Obj.Owner[0].DisplayName),
                        ds3Obj.ETag,
                        ds3Obj.StorageClass,
                        Convert.ToDateTime(ds3Obj.LastModified)
                    )).ToList();
                }
            }
        }
    }
}
