using System;
using System.Collections.Generic;
using System.Linq;

namespace Ds3.Helpers
{
    internal class ObjectPartTracker : IObjectPartTracker
    {
        private readonly ISet<ObjectPart> _parts;

        public event Action<long> DataTransferred;
        public event Action Completed;

        public ObjectPartTracker(IEnumerable<ObjectPart> parts)
        {
            ValidateParts(parts);
            this._parts = new SortedSet<ObjectPart>(parts);
        }

        public void CompletePart(ObjectPart partToRemove)
        {
            var existingPart = this._parts.LastOrDefault(part => part.Offset <= partToRemove.Offset);
            if (existingPart == null)
            {
                throw new InvalidOperationException(Resources.ObjectPartOutOfRangeException);
            }
            if (partToRemove.End > existingPart.End)
            {
                throw new InvalidOperationException(Resources.ObjectPartOutOfRangeException);
            }
            this._parts.Remove(existingPart);
            if (partToRemove.Offset > existingPart.Offset)
            {
                this._parts.Add(new ObjectPart(existingPart.Offset, partToRemove.Offset - existingPart.Offset));
            }
            if (partToRemove.End < existingPart.End)
            {
                this._parts.Add(new ObjectPart(partToRemove.End + 1, existingPart.End - partToRemove.End));
            }
            OnDataTransferred(partToRemove.Length);
            if (this._parts.Count == 0)
            {
                OnCompleted();
            }
        }

        private void OnDataTransferred(long p)
        {
            if (this.DataTransferred != null)
            {
                this.DataTransferred(p);
            }
        }

        private void OnCompleted()
        {
            if (this.Completed != null)
            {
                this.Completed();
            }
        }

        private static void ValidateParts(IEnumerable<ObjectPart> parts)
        {
            var ranges = parts
                .OrderBy(part => part.Offset)
                .Select(part => new { Start = part.Offset, End = part.Offset + part.Length - 1 });
            var lastEnd = -1L;
            foreach (var range in ranges)
            {
                if (range.Start <= lastEnd)
                {
                    throw new ArgumentOutOfRangeException();
                }
                lastEnd = range.End;
            }
        }
    }
}
