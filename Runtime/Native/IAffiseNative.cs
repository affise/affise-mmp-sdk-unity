#nullable enable
using System.Collections.Generic;
using AffiseAttributionLib.AffiseParameters;
using AffiseAttributionLib.Debugger.Network;
using AffiseAttributionLib.Debugger.Validate;
using AffiseAttributionLib.Deeplink;
using AffiseAttributionLib.Events;
using AffiseAttributionLib.Init;
using AffiseAttributionLib.Module.Link;
using AffiseAttributionLib.Module.Subscription;
using AffiseAttributionLib.Modules;
using AffiseAttributionLib.Referrer;
using AffiseAttributionLib.Settings;
using AffiseAttributionLib.SKAd;

namespace AffiseAttributionLib.Native
{
    internal interface IAffiseNative
    {
        void Init(AffiseInitProperties initProperties);

        bool IsInitialized();

        void SendEvent(AffiseEvent affiseEvent);

        void SendEventNow(AffiseEvent affiseEvent, OnSendSuccessCallback success, OnSendFailedCallback failed);

        void AddPushToken(string pushToken, PushTokenService service);

        void RegisterDeeplinkCallback(DeeplinkCallback callback);

        void SetSecretKey(string secretKey);

        void SetAutoCatchingTypes(List<AutoCatchingType> types);

        void SetOfflineModeEnabled(bool enabled);

        bool IsOfflineModeEnabled();

        void SetBackgroundTrackingEnabled(bool enabled);

        bool IsBackgroundTrackingEnabled();

        void SetTrackingEnabled(bool enabled);

        bool IsTrackingEnabled();

        void Forget(string userData);

        void SetEnabledMetrics(bool enabled);

        void CrashApplication();

        void GetReferrerUrl(OnReferrerCallback callback);

        void GetReferrerUrlValue(ReferrerKey key, OnReferrerCallback callback);
        
        void GetDeferredDeeplink(OnReferrerCallback callback);

        void GetDeferredDeeplinkValue(ReferrerKey key, OnReferrerCallback callback);

        string? GetRandomUserId();

        string? GetRandomDeviceId();

        Dictionary<ProviderType, object?> GetProviders();

        bool IsFirstRun();

        void RegisterAppForAdNetworkAttribution(ErrorCallback completionHandler);

        void UpdatePostbackConversionValue(int fineValue, CoarseValue coarseValue, ErrorCallback completionHandler);
        
        ////////////////////////////////////////
        // debug
        ////////////////////////////////////////
        void Validate(DebugOnValidateCallback callback);

        void Network(DebugOnNetworkCallback callback);

        string? VersionNative();

        ////////////////////////////////////////
        // modules
        ////////////////////////////////////////
        void GetStatus(AffiseModules module, OnKeyValueCallback callback);

        List<AffiseModules> GetModules();
        
        // Module Link
        void LinkResolve(string uri, AffiseLinkCallback callback);

        // Module Subscription
        void FetchProducts(List<string> ids, AffiseResultCallback<AffiseProductsResult> callback);

        // Module Subscription
        void Purchase(
            AffiseProduct product, AffiseProductType type,
            AffiseResultCallback<AffisePurchasedInfo> callback
        );
        
        // AppsFlyer
        void AppsFlyerLogEvent<T>(string eventName, Dictionary<string, T> eventValues);
        
        // TikTok
        void TikTokSendEvent<T>(string? eventName, Dictionary<string, T>? properties, string? eventId);
        
        // Advertising
        void AdvertisingStartModule();
        ////////////////////////////////////////
        // modules
        ////////////////////////////////////////
    }
}