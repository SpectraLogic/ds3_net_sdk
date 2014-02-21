using System;
using System.IO;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net;
using System.Xml.Serialization;

using Ds3.AwsModels;

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

        public GetBucketResponse(HttpWebResponse responseStream)
            : base(responseStream)
        {
            handleStatusCode(HttpStatusCode.OK);
            _objects = new List<Ds3Object>();            
            processResponse();
        }

        private void processResponse()
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
                    foreach (ListBucketResultContents contents in results.Contents)
                    {
                        Owner owner = new Owner(contents.Owner[0].ID, contents.Owner[0].DisplayName);
                        Ds3Object ds3Object = new Ds3Object(contents.Key, int.Parse(contents.Size), owner);
                        _objects.Add(ds3Object);
                    }
                }
            }
        }
    }

    public class Ds3Object
    {
        private string _name;
        private string _etag;
        private DateTime _lastModified;
        private Owner _owner;
        private long _size;
        private string _storageClass;


        public Ds3Object(string name, int size)
            : this(name, size, null)
        {

        }

        public Ds3Object(string name, int size, Owner owner) 
            : this (name,size, owner, "", "", Convert.ToDateTime(null))
        {            
        }

        public Ds3Object(string name, int size, Owner owner, string etag)
            : this(name, size, owner, etag, "", Convert.ToDateTime(null))
        {
        }

        public Ds3Object(string name, int size, Owner owner, string etag, string storageClass, DateTime lastModified)
        {
            this._name = name;
            this._size = size;
            this._owner = owner;
            this._etag = etag;
            this._storageClass = storageClass;
            this._lastModified = lastModified;
        }

        public string Name
        {
            get { return _name; }
        }

        public string Etag
        {
            get { return _etag; }
        }

        public DateTime LastModified
        {
            get { return _lastModified; }
        }

        public Owner Owner
        {
            get { return _owner; }
        }

        public long Size
        {
            get { return _size; }
        }

        public string StorageClass
        {
            get { return _storageClass; }
        }
    }
}
