using AffiseAttributionLib.Extensions;
using AffiseAttributionLib.Utils;
using AffiseAttributionLib.SimpleJSON;

namespace AffiseAttributionLib.Events.Property
{
    public class AffisePropertyBuilder
    {
        private const string PREFIX = "affise_event";
        
        private string _name;
        private readonly JSONObject _data = new();
        
        public AffisePropertyBuilder Init(string name)
        {
            _name = name.ToSnakeCase();
            return this;
        }

        public AffisePropertyBuilder Add(AffiseProperty key, object value)
        {
            return Add(key.ToValue(), value);
        }

        public AffisePropertyBuilder Add(string key, object value)
        {
            if (string.IsNullOrEmpty(key)) return this;
            return AddRaw(EventProperty(key), value);
        }

        public AffisePropertyBuilder AddRaw(string key, object value)
        {
            if (string.IsNullOrEmpty(key)) return this;
            _data.AddAny(key.ToLower(), value);
            return this;
        }

        public JSONObject Build() => _data;

        private string EventName() => $"{PREFIX}_{_name}";
        
        private string EventProperty(string key)
        {
            return $"{EventName()}_{key}";
        }
    }
}