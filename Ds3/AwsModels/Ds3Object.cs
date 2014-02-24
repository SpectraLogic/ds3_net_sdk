using System;
namespace Ds3.AwsModels
{
    public class Ds3Object
    {
        private string _name;
        private string _etag;
        private DateTime _lastModified;
        private Owner _owner;
        private long? _size;
        private string _storageClass;

        public Ds3Object(string name) 
            : this(name, null)
        {
        }

        public Ds3Object(string name, long? size)
            : this(name, size, null)
        {
        }

        public Ds3Object(string name, long? size, Owner owner)
            : this(name, size, owner, "", "", Convert.ToDateTime(null))
        {
        }

        public Ds3Object(string name, long? size, Owner owner, string etag)
            : this(name, size, owner, etag, "", Convert.ToDateTime(null))
        {
        }

        public Ds3Object(string name, long? size, Owner owner, string etag, string storageClass, DateTime lastModified)
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

        public long? Size
        {
            get { return _size; }
        }

        public string StorageClass
        {
            get { return _storageClass; }
        }
    }
}
