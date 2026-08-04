using System;
using AffiseAttributionLib.Logs;
using AffiseAttributionLib.AffiseParameters.Logs;
using AffiseAttributionLib.Utils;
using AffiseAttributionLib.SimpleJSON;

namespace AffiseAttributionLib.Converter
{
    internal class LogToSerializedLogConverter : IConverter<AffiseLog, SerializedLog>
    {
        public SerializedLog Convert(AffiseLog from)
        {
            //Generate id
            var id = Guid.NewGuid().ToString();

            //Type of log
            var type = from.Name.Type();

            //Create JSONObject for parameters
            var parameters = new JSONObject();
            //Generate parameters
            if (from.GetType() == typeof(AffiseLog.NetworkLog))
            {
                var jsonData = (from as AffiseLog.NetworkLog)?.JsonObject;
                parameters[type] = jsonData;
            }
            else
            {
                parameters[type] = from.Value;
            }

            //Generate data
            var json = new JSONObject()
            {
                //Add id
                ["affise_sdkevent_id"] = id,

                //Add name
                ["affise_sdkevent_name"] = "affise_event_sdklog",

                //Add timestam
                ["affise_sdkevent_timestamp"] = Timestamp.New(),

                //Add parameters
                ["affise_sdkevent_parameters"] = parameters,
            };

            //Create serialized object
            return new SerializedLog(id, type, json);
        }
    }
}