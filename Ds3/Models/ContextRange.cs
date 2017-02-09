/*
 * ******************************************************************************
 *   Copyright 2014-2017 Spectra Logic Corporation. All Rights Reserved.
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

using Ds3.Models;
using System;

namespace Ds3.Models
{
    internal static class ContextRange
    {
        public static ContextRange<T> Create<T>(Range range, T context)
            where T : IComparable<T>
        {
            return new ContextRange<T>(range, context);
        }
    }

    public class ContextRange<T> : IComparable, IComparable<ContextRange<T>>
        where T : IComparable<T>
    {
        public Range Range { get; private set; }
        public T Context { get; private set; }

        internal ContextRange(Range range, T context)
        {
            this.Range = range;
            this.Context = context;
        }

        public override bool Equals(object obj)
        {
            var contextRange = obj as ContextRange<T>;
            return
                contextRange != null
                && this.Context.Equals(contextRange.Context)
                && this.Range.Equals(contextRange.Range);
        }

        public override int GetHashCode()
        {
            // Algorithm based on http://stackoverflow.com/a/263416/472522
            unchecked
            {
                int hash = 486187739;
                hash = hash * 16777619 + this.Context.GetHashCode();
                hash = hash * 16777619 + this.Range.GetHashCode();
                return hash;
            }
        }

        public int CompareTo(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            var contextRange = obj as ContextRange<T>;
            if (contextRange == null)
            {
                throw new ArgumentException(Resources.ExpectedObjectOfSameTypeException, "obj");
            }
            return this.CompareTo(contextRange);
        }

        public int CompareTo(ContextRange<T> other)
        {
            if (other == null)
            {
                throw new ArgumentNullException("other");
            }
            return Math.Sign(
                2 * Math.Sign(this.Context.CompareTo(other.Context))
                + Math.Sign(this.Range.CompareTo(other.Range))
            );
        }

        public override string ToString()
        {
            return string.Format("ContextRange{{context='{0}',range={1}}}", this.Context, this.Range);
        }
    }
}
