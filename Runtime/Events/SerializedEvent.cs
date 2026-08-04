using AffiseAttributionLib.SimpleJSON;

namespace AffiseAttributionLib.Events
{
    public class SerializedEvent
    {
        public string Id { get; }
        public JSONNode Data { get; }
        
        public SerializedEvent(string id, JSONNode data)
        {
            Id = id;
            Data = data;
        }

    }
}