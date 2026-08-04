using AffiseAttributionLib.SimpleJSON;

namespace AffiseAttributionLib.Events.Subscription
{
    public class ConvertedTrialFromRetryEvent : BaseSubscriptionEvent
    {
        public ConvertedTrialFromRetryEvent(JSONObject data) : base(data)
        {
        }
        
        public ConvertedTrialFromRetryEvent(JSONObject data, string userData) : base(data, userData)
        {
        }

        public override string Type() => SubscriptionEventName.AFFISE_SUBSCRIPTION_RENEWAL_FROM_BILLING_RETRY.EventName();

        public override string SubType() => SubscriptionSubType.AFFISE_SUB_CONVERTED_TRIAL_FROM_RETRY.TypeName();
    }
    
    public class ConvertedOfferFromRetryEvent : BaseSubscriptionEvent
    {
        public ConvertedOfferFromRetryEvent(JSONObject data) : base(data)
        {
        }
        
        public ConvertedOfferFromRetryEvent(JSONObject data, string userData) : base(data, userData)
        {
        }

        public override string Type() => SubscriptionEventName.AFFISE_SUBSCRIPTION_RENEWAL_FROM_BILLING_RETRY.EventName();

        public override string SubType() => SubscriptionSubType.AFFISE_SUB_CONVERTED_OFFER_FROM_RETRY.TypeName();
    }
    
    public class RenewedSubscriptionFromRetryEvent : BaseSubscriptionEvent
    {
        public RenewedSubscriptionFromRetryEvent(JSONObject data) : base(data)
        {
        }
        
        public RenewedSubscriptionFromRetryEvent(JSONObject data, string userData) : base(data, userData)
        {
        }

        public override string Type() => SubscriptionEventName.AFFISE_SUBSCRIPTION_RENEWAL_FROM_BILLING_RETRY.EventName();

        public override string SubType() => SubscriptionSubType.AFFISE_SUB_RENEWED_SUBSCRIPTION_FROM_RETRY.TypeName();
    }
}