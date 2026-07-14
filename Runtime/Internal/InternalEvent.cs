#nullable enable
using System.Collections.Generic;
using AffiseAttributionLib.Extensions;
using SimpleJSON;

namespace AffiseAttributionLib.Internal
{
    internal abstract class InternalEvent
    {
        private readonly Dictionary<string, object?> _parameters = new();

        public abstract InternalEventName GetName();

        public virtual long GetTimestamp() => Utils.Timestamp.New();

        public JSONNode Serialize()
        {
            var json = new JSONObject();
            foreach (var (key, value) in _parameters)
            {
                json.AddAny(key, value);
            }

            return json;
        }

        public InternalEvent AddPropertyRaw(string property, object? value)
        {
            _parameters[property] = value;
            return this;
        }

        public void Send()
        {
            AffiseInternal.SendInternalEvent(this);
        }
    }
}
