using System;
using System.Collections.Generic;

namespace Ds3.Helpers
{
    internal class JobPartTracker
    {
        private readonly IDictionary<string, IObjectPartTracker> _trackers;

        public event Action<long> DataTransferred;
        public event Action<string> ObjectCompleted;

        public JobPartTracker(IDictionary<string, IObjectPartTracker> trackers)
        {
            this._trackers = trackers;
            foreach (var kvp in trackers)
            {
                kvp.Value.DataTransferred += OnDataTransferred;
                kvp.Value.Completed += () => OnObjectCompleted(kvp.Key);
            }
        }

        public void CompletePart(string name, ObjectPart part)
        {
            this._trackers[name].CompletePart(part);
        }

        public bool ContainsPart(string name, ObjectPart part)
        {
            return this._trackers[name].ContainsPart(part);
        }

        private void OnDataTransferred(long size)
        {
            if (this.DataTransferred != null)
            {
                this.DataTransferred(size);
            }
        }

        private void OnObjectCompleted(string objectName)
        {
            if (this.ObjectCompleted != null)
            {
                this.ObjectCompleted(objectName);
            }
        }
    }
}
