using System.Linq;
using System.Collections.Generic;

namespace Ds3.Models
{
    public class JobObject
    {
        public string Name { get; private set; }
        public IEnumerable<Blob> Blobs { get; private set; }

        internal JobObject(string name, IEnumerable<Blob> blobs)
        {
            this.Name = name;
            this.Blobs = blobs.ToList();
        }
    }
}
