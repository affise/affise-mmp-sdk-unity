#nullable enable
using AffiseAttributionLib.Converter;
using AffiseAttributionLib.Events;
using AffiseAttributionLib.Utils;
using SimpleJSON;

namespace AffiseAttributionLib.Internal
{
    internal class InternalEventToSerializedEventConverter : IConverter<InternalEvent, SerializedEvent>
    {
        public SerializedEvent Convert(InternalEvent from)
        {
            var id = Uuid.Generate();
            var json = new JSONObject
            {
                [InternalParameters.AFFISE_INTERNAL_EVENT_ID] = id,
                [InternalParameters.AFFISE_INTERNAL_EVENT_NAME] = from.GetName().ToEventName(),
                [InternalParameters.AFFISE_INTERNAL_EVENT_TIMESTAMP] = from.GetTimestamp(),
                [InternalParameters.AFFISE_INTERNAL_EVENT_DATA] = from.Serialize()
            };

            return new SerializedEvent(id, json);
        }
    }
}
