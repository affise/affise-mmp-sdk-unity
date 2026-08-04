using AffiseAttributionLib.SimpleJSON;


namespace AffiseAttributionLib.Events.Subscription
{
    public class UnsubscriptionEvent : BaseSubscriptionEvent
    {
        public UnsubscriptionEvent(JSONObject data) : base(data)
        {
        }
        
        public UnsubscriptionEvent(JSONObject data, string userData) : base(data, userData)
        {
        }

        public override string Type() => SubscriptionEventName.AFFISE_UNSUBSCRIPTION.EventName();

        public override string SubType() => SubscriptionSubType.AFFISE_SUB_UNSUBSCRIPTION.TypeName();
    }
}