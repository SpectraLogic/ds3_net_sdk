using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ds3.Models
{
    public class DS3GetObjectsInfo : Ds3Object
    {
        public string Name { get; private set; }
        public string Id { get; private set; }
        public string BucketId { get; private set; }
        public DateTime CreationDate { get; private set; }
        public string Type { get; private set; }
        public Int64 Version { get; private set; }


        public DS3GetObjectsInfo(
            string name,
            string id,
            string bucketId,
            DateTime creationDate,
            string type,
            Int64 version) : base(name, null)
        {
            this.Name = name;
            this.CreationDate = creationDate;
            this.BucketId = bucketId;
            this.Id = id;
            this.Type = type;
            this.Version = version;
        }
    }
}
