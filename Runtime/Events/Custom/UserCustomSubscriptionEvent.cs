#nullable enable
using System.Collections.Generic;
using AffiseAttributionLib.Events.Subscription;
using AffiseAttributionLib.SimpleJSON;

namespace AffiseAttributionLib.Events.Custom
{
    public class UserCustomSubscriptionEvent : BaseSubscriptionEvent
    {
        private readonly string _type;
        private readonly string _subtype;
        private readonly string? _category;
        
        public UserCustomSubscriptionEvent(
            string type, 
            string subtype, 
            JSONObject data,
            string? category
        ) : base(data)
        {
            _type = type;
            _subtype = subtype;
            _category = category;
        }
        
        public UserCustomSubscriptionEvent(
            string type,
            string subtype, 
            JSONObject data,
            string? userData,
            string? category
        ) : base(data, userData)
        {
            _type = type;
            _subtype = subtype;
            _category = category;
        }

        public override string Type() => _type;

        public override string SubType() => _subtype;

        public override string GetCategory()
        {
            return _category ?? base.GetCategory();
        }

        public AffiseEvent InternalAddRawParameters(Dictionary<string, object> parameters)
        {
            AddRawParameters(parameters);
            return this;
        }
    }
}