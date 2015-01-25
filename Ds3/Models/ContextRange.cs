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
