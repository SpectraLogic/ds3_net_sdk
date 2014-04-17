using System;

namespace Ds3.Models
{
    public class Ds3Object
    {
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
            this.Name = name;
            this.Size = size;
            this.Owner = owner;
            this.Etag = etag;
            this.StorageClass = storageClass;
            this.LastModified = lastModified;
        }

        public string Name { get; private set; }
        public string Etag { get; private set; }
        public DateTime LastModified { get; private set; }
        public Owner Owner { get; private set; }
        public long? Size { get; private set; }
        public string StorageClass { get; private set; }
    }
}
