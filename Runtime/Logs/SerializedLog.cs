using AffiseAttributionLib.SimpleJSON;

namespace AffiseAttributionLib.Logs
{
    /**
     * Serialized log contains [id] identification, [type] and log [data]
     */
    public class SerializedLog
    {
        /**
         * Log id
         */
        public string Id { get; }

        /**
         * Log type
         */
        public string Type { get; }

        /**
         * Log data
         */
        public JSONObject Data { get; }
        
        public SerializedLog(string id, string type, JSONObject data)
        {
            Id = id;
            Type = type;
            Data = data;
        }
    }
}