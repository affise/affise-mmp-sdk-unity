using AffiseAttributionLib.Events;
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
using AffiseAttributionLib.Native;
#endif

namespace AffiseAttributionLib
{
    internal static class AffiseInternal
    {
        /**
         * Store and send [affiseEvent]
         */
        internal static void SendEvent(AffiseEvent affiseEvent)
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            Affise._native?.SendEvent(affiseEvent);
#else
            Affise._api?.StoreEventUseCase.StoreEvent(affiseEvent);
#endif
        }

        /**
         * Send [affiseEvent] now
         */
        internal static void SendEventNow(AffiseEvent affiseEvent, OnSendSuccessCallback success, OnSendFailedCallback failed)
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            Affise._native?.SendEventNow(affiseEvent, success, failed);
#else
            Affise._api?.ImmediateSendToServerUseCase.SendNow(affiseEvent, success, failed);
#endif
        }
    }
}