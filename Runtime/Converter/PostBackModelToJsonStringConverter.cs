#nullable enable
using System.Collections.Generic;
using AffiseAttributionLib.AffiseParameters;
using AffiseAttributionLib.Extensions;
using AffiseAttributionLib.Network.Entity;
using AffiseAttributionLib.Usecase;
using SimpleJSON;

namespace AffiseAttributionLib.Converter
{
    public class PostBackModelToJsonStringConverter : IConverter<List<PostBackModel>, string>
    {
        private readonly IIndexUseCase _indexUseCase;

        public PostBackModelToJsonStringConverter(IIndexUseCase indexUseCase)
        {
            _indexUseCase = indexUseCase;
        }

        private const string EVENTS_KEY = "events";
        private const string SDK_EVENTS_KEY = "sdk_events";
        private const string INTERNAL_EVENTS_KEY = "internal_events";

        public string Convert(List<PostBackModel> from)
        {
            var jsonArray = new JSONArray();
            foreach (var model in from)
            {
                var jsonObject = MakeParameters(model);
                jsonArray.Add(jsonObject);
            }

            return jsonArray.ToString();
        }

        private JSONObject MakeParameters(PostBackModel obj)
        {
            var result = new JSONObject();

            //Events
            var eventsArray = new JSONArray();
            foreach (var evt in obj.Events)
            {
                eventsArray.Add(evt.Data);
            }

            //Logs
            var logsArray = new JSONArray();
            foreach (var log in obj.Logs)
            {
                logsArray.Add(log.Data);
            }

            //Internal events
            var internalEventsArray = new JSONArray();
            foreach (var internalEvent in obj.InternalEvents)
            {
                internalEventsArray.Add(internalEvent.Data);
            }

            foreach (var parameter in obj.Parameters)
            {
                result.AddAny(parameter.Key.Provider(), parameter.Value);
            }

            // PostBack index
            result.Add(Parameters.UUID_INDEX_KEY, _indexUseCase.GetUuidIndex());
            result[Parameters.AFFISE_EVENTS_COUNT] = eventsArray.Count;
            result[EVENTS_KEY] = eventsArray;
            result[Parameters.AFFISE_SDK_EVENTS_COUNT] = logsArray.Count;
            result[SDK_EVENTS_KEY] = logsArray;
            result[Parameters.AFFISE_INTERNAL_EVENTS_COUNT] = internalEventsArray.Count;
            result[INTERNAL_EVENTS_KEY] = internalEventsArray;

            return result;
        }
    }
}
