using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ds3.Models
{
    public class Bucket
    {
        public string Name { get; private set; }
        public string CreationDate { get; private set; }

        internal Bucket(string name, string creationDate)
        {
            this.Name = name;
            this.CreationDate = creationDate;
        }
    }
}
