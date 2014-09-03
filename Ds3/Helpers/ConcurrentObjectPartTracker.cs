using System;

namespace Ds3.Helpers
{
    internal class ConcurrentObjectPartTracker : IObjectPartTracker
    {
        private readonly IObjectPartTracker _innerTracker;
        private readonly object _lock = new object();

        public ConcurrentObjectPartTracker(IObjectPartTracker innerTracker)
        {
            this._innerTracker = innerTracker;
        }

        public event Action<long> DataTransferred
        {
            add
            {
                lock (this._lock)
                {
                    this.DataTransferred += value;
                }
            }
            remove
            {
                lock (this._lock)
                {
                    this.DataTransferred -= value;
                }
            }
        }

        public event Action Completed
        {
            add
            {
                lock (this._lock)
                {
                    this._innerTracker.Completed += value;
                }
            }
            remove
            {
                lock (this._lock)
                {
                    this._innerTracker.Completed -= value;
                }
            }
        }

        public void CompletePart(ObjectPart partToRemove)
        {
            lock (this._lock)
            {
                this._innerTracker.CompletePart(partToRemove);
            }
        }
    }
}
