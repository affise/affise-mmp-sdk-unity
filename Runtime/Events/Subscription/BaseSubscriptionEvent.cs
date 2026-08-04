#nullable enable
using AffiseAttributionLib.Events.Property;
using AffiseAttributionLib.SimpleJSON;

namespace AffiseAttributionLib.Events.Subscription
{
    public abstract class BaseSubscriptionEvent : NativeEvent
    {
        private readonly JSONObject _data;
        
        protected BaseSubscriptionEvent(JSONObject data)
        {
            _data = data;
        }
        
        protected BaseSubscriptionEvent(JSONObject data, string? userData) 
            : base(userData)
        {
            _data = data;
        }

        protected override AffisePropertyBuilder SerializeBuilder()
        {
            var result = base.SerializeBuilder();
            result.AddRaw(SubscriptionParameters.AFFISE_SUBSCRIPTION_EVENT_TYPE_KEY, SubType());
            foreach (var item in _data)
            {
                result.AddRaw(item.Key, item.Value);
            }
            return result;
        }

        public override string GetName() => Type();

        public abstract string Type();

        public abstract string SubType();
    }
}