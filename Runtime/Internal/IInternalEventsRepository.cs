using System.Collections.Generic;
using AffiseAttributionLib.Events;

namespace AffiseAttributionLib.Internal
{
    internal interface IInternalEventsRepository
    {
        bool HasEvents(string url);

        void StoreEvent(InternalEvent internalEvent, IEnumerable<string> urls);

        List<SerializedEvent> GetEvents(string url);

        void DeleteEvent(IEnumerable<string> ids, string url);

        void Clear();
    }
}
