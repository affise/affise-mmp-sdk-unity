using System.Collections.Generic;
using AffiseAttributionLib.Converter;
using AffiseAttributionLib.Events;
using AffiseAttributionLib.Storages;

namespace AffiseAttributionLib.Internal
{
    internal class InternalEventsRepositoryImpl : IInternalEventsRepository
    {
        private readonly IConverter<string, string> _converterToBase64;
        private readonly IConverter<InternalEvent, SerializedEvent> _internalEventToSerializedEventConverter;
        private readonly IEventsStorage _eventsStorage;

        public InternalEventsRepositoryImpl(
            IConverter<string, string> converterToBase64,
            IConverter<InternalEvent, SerializedEvent> internalEventToSerializedEventConverter,
            IEventsStorage eventsStorage
        )
        {
            _converterToBase64 = converterToBase64;
            _internalEventToSerializedEventConverter = internalEventToSerializedEventConverter;
            _eventsStorage = eventsStorage;
        }

        public bool HasEvents(string url)
        {
            return _eventsStorage.HasEvent(
                _converterToBase64.Convert(url)
            );
        }

        public void StoreEvent(InternalEvent internalEvent, IEnumerable<string> urls)
        {
            foreach (var url in urls)
            {
                _eventsStorage.SaveEvent(
                    _converterToBase64.Convert(url),
                    _internalEventToSerializedEventConverter.Convert(internalEvent)
                );
            }
        }

        public List<SerializedEvent> GetEvents(string url)
        {
            return _eventsStorage.GetEvents(
                _converterToBase64.Convert(url)
            );
        }

        public void DeleteEvent(IEnumerable<string> ids, string url)
        {
            _eventsStorage.DeleteEvent(
                _converterToBase64.Convert(url),
                ids
            );
        }

        public void Clear()
        {
            _eventsStorage.Clear();
        }
    }
}
