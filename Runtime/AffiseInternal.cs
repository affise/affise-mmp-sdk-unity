using System;
using AffiseAttributionLib.Events;
using AffiseAttributionLib.Exceptions;
using AffiseAttributionLib.Init;
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
using AffiseAttributionLib.Native;
#endif

namespace AffiseAttributionLib
{
    internal static class AffiseInternal
    {
        internal static void Start(AffiseInitProperties initProperties)
        {
            if (Affise.IsInit)
            {
                initProperties.OnInitErrorHandler?.Invoke(AffiseError.MESSAGE_ALREADY_INITIALIZED);
                return;
            }
            
            try
            {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
                Affise._native = new AffiseNative(initProperties);
                // OnInitSuccessHandler is called from native
#else
                Affise._api = new AffiseComponent(initProperties);
                initProperties.OnInitSuccessHandler?.Invoke();
#endif
            }
            catch (Exception e)
            {
                initProperties.OnInitErrorHandler?.Invoke(e.StackTrace);
            }
        }
        
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