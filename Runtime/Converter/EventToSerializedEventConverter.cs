using AffiseAttributionLib.Events;
using AffiseAttributionLib.AffiseParameters;
using AffiseAttributionLib.Extensions;
using AffiseAttributionLib.Usecase;
using AffiseAttributionLib.Utils;
using AffiseAttributionLib.SimpleJSON;

namespace AffiseAttributionLib.Converter
{
    public class EventToSerializedEventConverter : IConverter<AffiseEvent, SerializedEvent>
    {
        private readonly IIndexUseCase _indexUseCase;

        public EventToSerializedEventConverter(IIndexUseCase indexUseCase)
        {
            _indexUseCase = indexUseCase;
        }

        public SerializedEvent Convert(AffiseEvent from)
        {
            var id = Uuid.Generate();
            var json = new JSONObject
            {
                [Parameters.AFFISE_EVENT_ID] = id,
                [Parameters.AFFISE_EVENT_NAME] = from.GetName(),
                [Parameters.AFFISE_EVENT_CATEGORY] = from.GetCategory(),
                [Parameters.AFFISE_EVENT_TIMESTAMP] = Timestamp.New(),
                //Add id index
                [Parameters.AFFISE_EVENT_ID_INDEX] = _indexUseCase.GetAffiseEventIdIndex(),
                [Parameters.AFFISE_EVENT_FIRST_FOR_USER] = from.IsFirstForUser(),
                [Parameters.AFFISE_EVENT_USER_DATA] = from.GetUserData(),
                [Parameters.AFFISE_EVENT_DATA] = from.Serialize(),
                [Parameters.AFFISE_PARAMETERS] = from.GetPredefinedParameters().ToJsonObject(),
            };

            return new SerializedEvent(id, json);
        }
    }
}