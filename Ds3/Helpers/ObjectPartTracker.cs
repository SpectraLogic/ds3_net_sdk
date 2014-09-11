/*
 * ******************************************************************************
 *   Copyright 2014 Spectra Logic Corporation. All Rights Reserved.
 *   Licensed under the Apache License, Version 2.0 (the "License"). You may not use
 *   this file except in compliance with the License. A copy of the License is located at
 *
 *   http://www.apache.org/licenses/LICENSE-2.0
 *
 *   or in the "license" file accompanying this file.
 *   This file is distributed on an "AS IS" BASIS, WITHOUT WARRANTIES OR
 *   CONDITIONS OF ANY KIND, either express or implied. See the License for the
 *   specific language governing permissions and limitations under the License.
 * ****************************************************************************
 */

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


        public bool ContainsPart(ObjectPart part)
        {
            return this._parts.Any(p => p.CompareTo(part) == 0);
        }
    }
}
