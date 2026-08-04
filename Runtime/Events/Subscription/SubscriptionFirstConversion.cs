using AffiseAttributionLib.SimpleJSON;

namespace AffiseAttributionLib.Events.Subscription
{
    public class ConvertedTrialEvent : BaseSubscriptionEvent
    {
        public ConvertedTrialEvent(JSONObject data) : base(data)
        {
        }

        public ConvertedTrialEvent(JSONObject data, string userData) : base(data, userData)
        {
        }

        public override string Type() => SubscriptionEventName.AFFISE_SUBSCRIPTION_FIRST_CONVERSION.EventName();

        public override string SubType() => SubscriptionSubType.AFFISE_SUB_CONVERTED_TRIAL.TypeName();
    }
    
    public class ConvertedOfferEvent : BaseSubscriptionEvent
    {
        public ConvertedOfferEvent(JSONObject data) : base(data)
        {
        }

        public ConvertedOfferEvent(JSONObject data, string userData) : base(data, userData)
        {
        }

        public override string Type() => SubscriptionEventName.AFFISE_SUBSCRIPTION_FIRST_CONVERSION.EventName();

        public override string SubType() => SubscriptionSubType.AFFISE_SUB_CONVERTED_OFFER.TypeName();
    }
}