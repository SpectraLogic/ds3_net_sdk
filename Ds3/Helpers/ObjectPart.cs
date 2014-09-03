using System;

namespace Ds3.Helpers
{
    public class ObjectPart : IComparable<ObjectPart>
    {
        public long Offset { get; private set; }
        public long Length { get; private set; }
        public long End
        {
            get { return this.Offset + this.Length - 1; }
        }

        public ObjectPart(long offset, long length)
        {
            this.Offset = offset;
            this.Length = length;
        }

        public int CompareTo(ObjectPart other)
        {
            return Math.Sign(this.Length - other.Length)
                + 2 * Math.Sign(this.Offset - other.Offset);
        }
    }
}
