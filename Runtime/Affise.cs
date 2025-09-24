#nullable enable
using System;
using System.Collections.Generic;
using AffiseAttributionLib.AffiseParameters;
using AffiseAttributionLib.AffiseParameters.Providers;
using AffiseAttributionLib.Debugger;
using AffiseAttributionLib.Deeplink;
using AffiseAttributionLib.Events;
using AffiseAttributionLib.Init;
using AffiseAttributionLib.Module.Attribution;
using AffiseAttributionLib.Modules;
using AffiseAttributionLib.Referrer;
using AffiseAttributionLib.Settings;
using AffiseAttributionLib.Utils;
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
using AffiseAttributionLib.Native;
#endif

namespace AffiseAttributionLib
{
    public static class Affise
    {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        internal static IAffiseNative? _native { get; private set; }
#else
        internal static AffiseComponent? _api { get; private set; }
#endif

        internal static bool IsInit
        {
            get
            {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
                return _native is not null;
#else
                return _api is not null;
#endif
            }
        }

        public static AffiseSettings Settings(string affiseAppId, string secretKey)
        {
            return new AffiseSettings(affiseAppId, secretKey);
        }

        internal static void Start(AffiseInitProperties initProperties)
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            if (_native is not null) return;
            _native = new AffiseNative(initProperties);
#else
            if (_api is not null) return;
            try
            {
                _api = new AffiseComponent(initProperties);
                initProperties.OnInitSuccessHandler?.Invoke();
            }
            catch (Exception e)
            {
                initProperties.OnInitErrorHandler?.Invoke(e.StackTrace);
            }
#endif
        }

        [Obsolete("use Affise.Settings().SetOnInitSuccess() instead.")]
        public static bool IsInitialized()
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            return _native?.IsInitialized() ?? false;
#else
            return _api?.IsInitialized() ?? false;
#endif
        }

        /**
         * Store and send [affiseEvent]
         */
        public static void SendEvent(AffiseEvent affiseEvent)
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            _native?.SendEvent(affiseEvent);
#else
            _api?.StoreEventUseCase.StoreEvent(affiseEvent);
#endif
        }

        /**
         * Send [affiseEvent] now
         */
        public static void SendEventNow(AffiseEvent affiseEvent, OnSendSuccessCallback success, OnSendFailedCallback failed)
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            _native?.SendEventNow(affiseEvent, success, failed);
#else
            _api?.ImmediateSendToServerUseCase.SendNow(affiseEvent, success, failed);
#endif
        }
        
        /**
         * Add [pushToken]
         */
        public static void AddPushToken(string pushToken, PushTokenService service)
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            _native?.AddPushToken(pushToken, service);
#else
            _api?.PushTokenUseCase?.AddPushToken(pushToken, service);
#endif
        }

        /**
         * Register [callback] for deeplink
         */
        public static void RegisterDeeplinkCallback(DeeplinkCallback callback)
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            _native?.RegisterDeeplinkCallback(callback);
#else
            _api?.DeeplinkManager.SetDeeplinkCallback(callback);
#endif
        }

        /**
         * Set new SDK [secretKey]
         */
        public static void SetSecretKey(string secretKey)
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            _native?.SetSecretKey(secretKey);
#else
            _api?.InitPropertiesStorage?.UpdateSecretKey(secretKey);
#endif
        }

        /**
         * Set offline mode to [enabled] state
         */
        public static void SetOfflineModeEnabled(bool enabled)
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            _native?.SetOfflineModeEnabled(enabled);
#endif
        }

        /**
         * Return current offline mode state
         */
        public static bool? IsOfflineModeEnabled()
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            return _native?.IsOfflineModeEnabled();
#else
            return null;
#endif
        }

        /**
         * Set background tracking to [enabled] state
         */
        public static void SetBackgroundTrackingEnabled(bool enabled)
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            _native?.SetBackgroundTrackingEnabled(enabled);
#endif
        }

        /**
         * Return current background tracking state
         */
        public static bool? IsBackgroundTrackingEnabled()
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            return _native?.IsBackgroundTrackingEnabled();
#else
            return null;
#endif
        }

        /**
         * Set tracking to [enabled] state
         */
        public static void SetTrackingEnabled(bool enabled)
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            _native?.SetTrackingEnabled(enabled);
#endif
        }

        /**
         * Return current tracking state
         */
        public static bool? IsTrackingEnabled()
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            return _native?.IsTrackingEnabled();
#else
            return null;
#endif
        }

        /**
         * Get module status
         */
        [Obsolete("use Affise.Module." + nameof(Affise.Module.GetStatus) + " instead.")]
        public static void GetStatus(AffiseModules module, OnKeyValueCallback onComplete)
        {
            Module.GetStatus(module, onComplete);
        }
        
        /**
         * Get installed modules
         */
        [Obsolete("use Affise.Module." + nameof(Affise.Module.GetModulesInstalled) + " instead.")]
        public static List<AffiseModules> GetModulesInstalled()
        {
            return Module.GetModulesInstalled();
        }
        
        public static string? GetRandomUserId()
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            return _native?.GetRandomUserId();
#else
            return _api?.PostBackModelFactory.GetProvider<RandomUserIdProvider>()?.Provide();
#endif
        }

        public static string? GetRandomDeviceId()
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            return _native?.GetRandomDeviceId();
#else
            return _api?.PostBackModelFactory.GetProvider<AffiseDeviceIdProvider>()?.Provide();
#endif
        }

        public static Dictionary<ProviderType, object?> GetProviders()
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            return _native?.GetProviders() ?? new Dictionary<ProviderType, object?>();
#else
            return _api?.PostBackModelFactory.GetProvidersMap() ?? new Dictionary<ProviderType, object?>();
#endif
        }
        
        public static bool IsFirstRun()
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            return _native?.IsFirstRun() ?? true; 
#else
            return _api?.FirstAppOpenUseCase.IsFirstRun() ?? true;
#endif
        }
        
        /**
         * Get referrer
         */
        [Obsolete("use Affise." + nameof(Affise.GetReferrerUrl) + " instead.")]
        public static void GetReferrer(OnReferrerCallback callback)
        {
            GetReferrerUrl(callback);
        }

        /**
         * Get referrer value by key
         */
        [Obsolete("use Affise." + nameof(Affise.GetReferrerUrlValue) + " instead.")]
        public static void GetReferrerValue(ReferrerKey key, OnReferrerCallback callback)
        {
            GetReferrerUrlValue(key, callback);
        }

        /**
         * Get referrer url
         */
        public static void GetReferrerUrl(OnReferrerCallback callback)
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            _native?.GetReferrerUrl(callback);
#else
            callback.Invoke(NotSupported);
#endif
        }

        /**
         * Get referrer value by key
         */
        public static void GetReferrerUrlValue(ReferrerKey key, OnReferrerCallback callback)
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            _native?.GetReferrerUrlValue(key, callback);
#else
            callback.Invoke(NotSupported);
#endif
        }
        
        /**
         * Get referrer url
         */
        [Obsolete("use Affise." + nameof(Affise.GetDeferredDeeplink) + " instead.")]
        public static void GetReferrerOnServer(OnReferrerCallback callback)
        {
            GetDeferredDeeplink(callback);
        }

        /**
         * Get referrer value by key
         */
        [Obsolete("use Affise." + nameof(Affise.GetDeferredDeeplinkValue) + " instead.")]
        public static void GetReferrerOnServerValue(ReferrerKey key, OnReferrerCallback callback)
        {
            GetDeferredDeeplinkValue(key, callback);
        }

        /**
         * Get referrer url
         */
        public static void GetDeferredDeeplink(OnReferrerCallback callback)
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            _native?.GetDeferredDeeplink(callback);
#else
            _api?.RetrieveReferrerOnServerUseCase.GetReferrerOnServer(callback);
#endif
        }

        /**
         * Get referrer value by key
         */
        public static void GetDeferredDeeplinkValue(ReferrerKey key, OnReferrerCallback callback)
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            _native?.GetDeferredDeeplinkValue(key, callback);
#else
            _api?.RetrieveReferrerOnServerUseCase.GetReferrerOnServerValue(key, callback);
#endif
        }

        public static readonly IAffiseIOSApi IOS = new AffiseModuleIso();
        public static readonly IAffiseAndroidApi Android = new AffiseModuleAndroid();
        public static readonly IAffiseAttributionModuleApi Module = new AffiseAttributionModule();
        
        public static readonly IAffiseDebugApi Debug = new AffiseDebug();

        private const string NotSupported = "[Affise] platform not supported";
    }
}