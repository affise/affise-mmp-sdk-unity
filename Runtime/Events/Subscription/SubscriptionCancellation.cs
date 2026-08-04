using AffiseAttributionLib.SimpleJSON;

namespace AffiseAttributionLib.Events.Subscription
{
    public class FailedTrialEvent : BaseSubscriptionEvent
    {
        public FailedTrialEvent(JSONObject data) : base(data)
        {
        }

        public FailedTrialEvent(JSONObject data, string userData) : base(data, userData)
        {
        }

        public override string Type() => SubscriptionEventName.AFFISE_SUBSCRIPTION_CANCELLATION.EventName();

        public override string SubType() => SubscriptionSubType.AFFISE_SUB_FAILED_TRIAL.TypeName();
    }

    public class FailedOfferiseEvent : BaseSubscriptionEvent
    {
        public FailedOfferiseEvent(JSONObject data) : base(data)
        {
        }

        public FailedOfferiseEvent(JSONObject data, string userData) : base(data, userData)
        {
        }

        public override string Type() => SubscriptionEventName.AFFISE_SUBSCRIPTION_CANCELLATION.EventName();

        public override string SubType() => SubscriptionSubType.AFFISE_SUB_FAILED_OFFERISE.TypeName();
    }

    public class FailedSubscriptionEvent : BaseSubscriptionEvent
    {
        public FailedSubscriptionEvent(JSONObject data) : base(data)
        {
        }

        public FailedSubscriptionEvent(JSONObject data, string userData) : base(data, userData)
        {
        }

        public override string Type() => SubscriptionEventName.AFFISE_SUBSCRIPTION_CANCELLATION.EventName();

        public override string SubType() => SubscriptionSubType.AFFISE_SUB_FAILED_SUBSCRIPTION.TypeName();
    }

    public class FailedTrialFromRetryEvent : BaseSubscriptionEvent
    {
        public FailedTrialFromRetryEvent(JSONObject data) : base(data)
        {
        }

        public FailedTrialFromRetryEvent(JSONObject data, string userData) : base(data, userData)
        {
        }

        public override string Type() => SubscriptionEventName.AFFISE_SUBSCRIPTION_CANCELLATION.EventName();

        public override string SubType() => SubscriptionSubType.AFFISE_SUB_FAILED_TRIAL_FROM_RETRY.TypeName();
    }

    public class FailedOfferFromRetryEvent : BaseSubscriptionEvent
    {
        public FailedOfferFromRetryEvent(JSONObject data) : base(data)
        {
        }

        public FailedOfferFromRetryEvent(JSONObject data, string userData) : base(data, userData)
        {
        }

        public override string Type() => SubscriptionEventName.AFFISE_SUBSCRIPTION_CANCELLATION.EventName();

        public override string SubType() => SubscriptionSubType.AFFISE_SUB_FAILED_OFFER_FROM_RETRY.TypeName();
    }

    public class FailedSubscriptionFromRetryEvent : BaseSubscriptionEvent
    {
        public FailedSubscriptionFromRetryEvent(JSONObject data) : base(data)
        {
        }

        public FailedSubscriptionFromRetryEvent(JSONObject data, string userData) : base(data, userData)
        {
        }

        public override string Type() => SubscriptionEventName.AFFISE_SUBSCRIPTION_CANCELLATION.EventName();

        public override string SubType() => SubscriptionSubType.AFFISE_SUB_FAILED_SUBSCRIPTION_FROM_RETRY.TypeName();
    }
}