using AffiseAttributionLib.SimpleJSON;

namespace AffiseAttributionLib.Events.Subscription
{
    public class ReactivatedSubscriptionEvent : BaseSubscriptionEvent
    {
        public ReactivatedSubscriptionEvent(JSONObject data) : base(data)
        {
        }

        public ReactivatedSubscriptionEvent(JSONObject data, string userData) : base(data, userData)
        {
        }

        public override string Type() => SubscriptionEventName.AFFISE_SUBSCRIPTION_REACTIVATION.EventName();

        public override string SubType() => SubscriptionSubType.AFFISE_SUB_REACTIVATED_SUBSCRIPTION.TypeName();
    }
}