#nullable enable
using AffiseAttributionLib.SimpleJSON;

namespace AffiseAttributionLib.Modules
{
    public class AffiseKeyValue
    {
        public string Value { get; }

        public string Key { get; }

        internal AffiseKeyValue(JSONObject json) : this(key: json["key"], value: json["value"])
        {
        }

        public AffiseKeyValue(string key, string value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return $"AffiseKeyValue(key={Key}, value={Value})";
        }
    }
}
