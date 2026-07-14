using AffiseAttributionLib.AffiseParameters;

namespace AffiseAttributionLib.Internal.Predefined
{
    internal class SessionStartInternalEvent : InternalEvent
    {
        public SessionStartInternalEvent(long affiseSessionCount, long lifetimeSessionCount)
        {
            AddPropertyRaw(ProviderType.AFFISE_SESSION_COUNT.Provider(), affiseSessionCount);
            AddPropertyRaw(ProviderType.LIFETIME_SESSION_COUNT.Provider(), lifetimeSessionCount);
        }

        public override InternalEventName GetName() => InternalEventName.SESSION_START;
    }
}
