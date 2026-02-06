#nullable enable
using AffiseAttributionLib.Debugger.Network;
using AffiseAttributionLib.Debugger.Validate;
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
using AffiseAttributionLib.Native;
#endif

namespace AffiseAttributionLib.Debugger
{
    internal class AffiseDebug : IAffiseDebugApi
    {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        private IAffiseNative? _native => Affise._native;
#else
        private AffiseComponent? _api => Affise._api;
#endif

        /**
         * Won't work on Production
         *
         * Validate credentials
         */
        public void Validate(DebugOnValidateCallback callback)
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            _native?.Validate(callback);
#else
            _api?.DebugValidateUseCase?.Validate(callback);
#endif
        }
            
        /**
         * Won't work on Production
         *
         * Show request/response data
         */
        public void Network(DebugOnNetworkCallback callback)
        { 
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            _native?.Network(callback);
#else
            _api?.DebugNetworkUseCase.OnRequest(callback);
#endif
        }

        /**
         * Debug get version of flutter library
         */
        public string Version()
        {
            return "1.6.52";
        }
            
        /**
         * Debug get version of native library Android/iOS
         */
        public string? VersionNative()
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            return _native?.VersionNative();
#else
            return null;
#endif
        }
    }
}