using AffiseAttributionLib.SimpleJSON;

namespace AffiseAttributionLib.Events.Subscription
{
    public class InitialSubscriptionEvent : BaseSubscriptionEvent
    {
        public InitialSubscriptionEvent(JSONObject data) : base(data)
        {
        }

        public InitialSubscriptionEvent(JSONObject data, string userData) : base(data, userData)
        {
        }

        public override string Type() => SubscriptionEventName.AFFISE_SUBSCRIPTION_ACTIVATION.EventName();

        public override string SubType() => SubscriptionSubType.AFFISE_SUB_INITIAL_SUBSCRIPTION.TypeName();
    }

    public class InitialTrialEvent : BaseSubscriptionEvent
    {
        public InitialTrialEvent(JSONObject data) : base(data)
        {
        }

        public InitialTrialEvent(JSONObject data, string userData) : base(data, userData)
        {
        }

        public override string Type() => SubscriptionEventName.AFFISE_SUBSCRIPTION_ACTIVATION.EventName();

        public override string SubType() => SubscriptionSubType.AFFISE_SUB_INITIAL_TRIAL.TypeName();
    }

    public class InitialOfferEvent : BaseSubscriptionEvent
    {
        public InitialOfferEvent(JSONObject data) : base(data)
        {
        }

        public InitialOfferEvent(JSONObject data, string userData) : base(data, userData)
        {
        }

        public override string Type() => SubscriptionEventName.AFFISE_SUBSCRIPTION_ACTIVATION.EventName();

        public override string SubType() => SubscriptionSubType.AFFISE_SUB_INITIAL_OFFER.TypeName();
    }
}