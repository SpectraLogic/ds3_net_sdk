using System;

namespace Ds3.Helpers
{
    internal interface IObjectPartTracker
    {
        event Action<long> DataTransferred;
        event Action Completed;

        void CompletePart(ObjectPart partToRemove);
        bool ContainsPart(ObjectPart part);
    }
}
