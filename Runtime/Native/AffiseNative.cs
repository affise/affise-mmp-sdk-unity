#nullable enable
using System.Collections.Generic;
using AffiseAttributionLib.AffiseParameters;
using AffiseAttributionLib.Converter;
using AffiseAttributionLib.Debugger.Network;
using AffiseAttributionLib.Debugger.Validate;
using AffiseAttributionLib.Deeplink;
using AffiseAttributionLib.Events;
using AffiseAttributionLib.Extensions;
using AffiseAttributionLib.Init;
using AffiseAttributionLib.Module.Link;
using AffiseAttributionLib.Module.Subscription;
using AffiseAttributionLib.Modules;
using AffiseAttributionLib.Native.Data;
using AffiseAttributionLib.Native.Utils;
using AffiseAttributionLib.Referrer;
using AffiseAttributionLib.Settings;
using AffiseAttributionLib.SKAd;
using AffiseAttributionLib.Usecase;
using SimpleJSON;

namespace AffiseAttributionLib.Native
{
    internal class AffiseNative : NativeBase, IAffiseNative
    {
        private readonly EventToSerializedEventConverter _converter = new(new IndexUseCaseImpl());

        public AffiseNative(AffiseInitProperties initProperties)
        {
            if (initProperties.OnInitSuccessHandler is not null)
            {
                NativeCallbackOnly(
                    AffiseApiMethod.ON_INIT_SUCCESS_HANDLER,
                    callback: initProperties.OnInitSuccessHandler
                );
            }

            if (initProperties.OnInitErrorHandler is not null)
            {
                NativeCallbackOnly(
                    AffiseApiMethod.ON_INIT_ERROR_HANDLER,
                    callback: initProperties.OnInitErrorHandler
                );
            }

            Init(initProperties);
        }

        public void Init(AffiseInitProperties initProperties)
        {
            Native(AffiseApiMethod.INIT, initProperties.ToJson);
        }

        public bool IsInitialized()
        {
            return Native<bool>(AffiseApiMethod.IS_INITIALIZED);
        }

        public void SendEvent(AffiseEvent affiseEvent)
        {
            var data = _converter.Convert(affiseEvent).Data;
            Native(AffiseApiMethod.SEND_EVENT, data);
        }

        public void SendEventNow(AffiseEvent affiseEvent, OnSendSuccessCallback success, OnSendFailedCallback failed)
        {
            var data = _converter.Convert(affiseEvent).Data;
            NativeCallbackGroup(
                AffiseApiMethod.SEND_EVENT_NOW,
                new Dictionary<string, CallbackData>
                {
                    { DataName.SUCCESS, new CallbackData(success) },
                    { DataName.FAILED, new CallbackData(failed)  }
                },
                data
            );
        }

        public void AddPushToken(string pushToken, PushTokenService service)
        {
            Native(AffiseApiMethod.ADD_PUSH_TOKEN, new Dictionary<string, string?>
            {
                { DataName.PUSH_TOKEN, pushToken },
                { DataName.PUSH_TOKEN_SERVICE, service.Service() }
            });
        }

        public void RegisterDeeplinkCallback(DeeplinkCallback callback)
        {
            NativeCallback(AffiseApiMethod.REGISTER_DEEPLINK_CALLBACK, callback: callback);
            InitialLink();
        }

        public void SetSecretKey(string secretKey)
        {
            Native(AffiseApiMethod.SET_SECRET_KEY, secretKey);
        }

        public void SetAutoCatchingTypes(List<AutoCatchingType> types)
        {
            Native(AffiseApiMethod.SET_AUTO_CATCHING_TYPES, types.ToJsonArray());
        }

        public void SetOfflineModeEnabled(bool enabled)
        {
            Native(AffiseApiMethod.SET_OFFLINE_MODE_ENABLED, enabled);
        }

        public bool IsOfflineModeEnabled()
        {
            return Native<bool>(AffiseApiMethod.IS_OFFLINE_MODE_ENABLED);
        }

        public void SetBackgroundTrackingEnabled(bool enabled)
        {
            Native(AffiseApiMethod.SET_BACKGROUND_TRACKING_ENABLED, enabled);
        }

        public bool IsBackgroundTrackingEnabled()
        {
            return Native<bool>(AffiseApiMethod.IS_BACKGROUND_TRACKING_ENABLED);
        }

        public void SetTrackingEnabled(bool enabled)
        {
            Native(AffiseApiMethod.SET_TRACKING_ENABLED, enabled);
        }

        public bool IsTrackingEnabled()
        {
            return Native<bool>(AffiseApiMethod.IS_TRACKING_ENABLED);
        }

        public void Forget(string userData)
        {
            Native(AffiseApiMethod.FORGET, userData);
        }

        public void SetEnabledMetrics(bool enabled)
        {
            Native(AffiseApiMethod.SET_ENABLED_METRICS, enabled);
        }

        public void CrashApplication()
        {
            Native(AffiseApiMethod.CRASH_APPLICATION);
        }

        public void GetReferrerUrl(OnReferrerCallback callback)
        {
            NativeCallbackOnce(AffiseApiMethod.GET_REFERRER_URL_CALLBACK, callback: callback);
        }

        public void GetReferrerUrlValue(ReferrerKey key, OnReferrerCallback callback)
        {
            NativeCallbackOnce(AffiseApiMethod.GET_REFERRER_URL_VALUE_CALLBACK, callback: callback, data: key.ToValue());
        }

        public void GetDeferredDeeplink(OnReferrerCallback callback)
        {
            NativeCallbackOnce(AffiseApiMethod.GET_DEFERRED_DEEPLINK_CALLBACK, callback: callback);
        }

        public void GetDeferredDeeplinkValue(ReferrerKey key, OnReferrerCallback callback)
        {
            NativeCallbackOnce(AffiseApiMethod.GET_DEFERRED_DEEPLINK_VALUE_CALLBACK, callback: callback, data: key.ToValue());
        }

        public bool IsFirstRun()
        {
            return Native<bool>(AffiseApiMethod.IS_FIRST_RUN);
        }

        public string? GetRandomUserId()
        {
            return Native<string>(AffiseApiMethod.GET_RANDOM_USER_ID);
        }

        public string? GetRandomDeviceId()
        {
            return Native<string>(AffiseApiMethod.GET_RANDOM_DEVICE_ID);
        }

        public Dictionary<ProviderType, object?> GetProviders()
        {
            return NativeMap(AffiseApiMethod.GET_PROVIDERS).ToProviderMap();
        }

        public void RegisterAppForAdNetworkAttribution(ErrorCallback completionHandler)
        {
            NativeCallbackOnce(AffiseApiMethod.SKAD_REGISTER_ERROR_CALLBACK, callback: completionHandler);
        }

        public void UpdatePostbackConversionValue(
            int fineValue,
            CoarseValue coarseValue,
            ErrorCallback completionHandler
        )
        {
            var data = new Dictionary<string, object>
            {
                { DataName.FINE_VALUE, fineValue },
                { DataName.COARSE_VALUE, coarseValue.Value }
            };
            NativeCallbackOnce(
                api: AffiseApiMethod.SKAD_POSTBACK_ERROR_CALLBACK,
                callback: completionHandler, 
                data: data
            );
        }

        ////////////////////////////////////////
        // debug
        ////////////////////////////////////////
        public void Validate(DebugOnValidateCallback callback)
        {
            NativeCallbackOnce(AffiseApiMethod.DEBUG_VALIDATE_CALLBACK, callback: callback);
        }

        public void Network(DebugOnNetworkCallback callback)
        {
            NativeCallback(AffiseApiMethod.DEBUG_NETWORK_CALLBACK, callback: callback);
        }

        public string? VersionNative()
        {
            return Native<string>(AffiseApiMethod.DEBUG_VERSION_NATIVE);;
        }

        ////////////////////////////////////////
        // modules
        ////////////////////////////////////////
        public void GetStatus(AffiseModules module, OnKeyValueCallback callback)
        {
            NativeCallbackOnce(
                api: AffiseApiMethod.GET_STATUS_CALLBACK, 
                callback: callback, 
                data: module.Module()
            );
        }

        public List<AffiseModules> GetModules()
        {
            var result = new List<AffiseModules>();
            var data = NativeList(AffiseApiMethod.GET_MODULES_INSTALLED);
            if (data == null) return result;
            foreach (var (_, value) in data)
            {
                var module = AffiseModulesExt.From(value?.Value);
                if (module is null) continue;
                result.Add((AffiseModules)module);
            }

            return result;
        }

        // Module Link
        public void LinkResolve(string uri, AffiseLinkCallback callback)
        {
            NativeCallbackOnce(
                api: AffiseApiMethod.MODULE_LINK_LINK_RESOLVE_CALLBACK,
                callback: callback,
                data: uri
            );
        }
        
        // Module Subscription
        public void FetchProducts(List<string> ids, AffiseResultCallback<AffiseProductsResult> callback)
        {
            NativeCallbackOnce(
                api: AffiseApiMethod.MODULE_SUBS_FETCH_PRODUCTS_CALLBACK,
                callback: callback,
                data: ids
            );
        }

        // Module Subscription
        public void Purchase(
            AffiseProduct product,
            AffiseProductType type,
            AffiseResultCallback<AffisePurchasedInfo> callback
        )
        {
            var data = new Dictionary<string, object?>
            {
                { DataName.PRODUCT, DataMapper.FromProduct(product) },
                { DataName.TYPE, type.ToValue() }
            };

            NativeCallbackOnce(
                api: AffiseApiMethod.MODULE_SUBS_PURCHASE_CALLBACK,
                callback: callback,
                data: data
            );
        }

        // AppsFlyer
        public void AppsFlyerLogEvent<T>(string eventName, Dictionary<string, T> eventValues) {
            var data = new Dictionary<string, object>
            {
                { DataName.EVENT_NAME, eventName },
                { DataName.EVENT_VALUES, eventValues.ToJsonObject() },
            };
            Native(AffiseApiMethod.MODULE_APPSFLYER_LOG_EVENT, data);
        }
        
                
        // TikTok
        public void TikTokSendEvent<T>(string? eventName, Dictionary<string, T>? properties, string? eventId)
        {
            var data = new Dictionary<string, object?>
            {
                { DataName.EVENT_NAME, eventName },
                { DataName.EVENT_VALUES, properties?.ToJsonObject() },
                { DataName.EVENT_ID, eventId },
            };
            Native(AffiseApiMethod.MODULE_TIKTOK_EVENT, data);
        }
        
        // Advertising
        public void AdvertisingStartModule()
        {
            Native(AffiseApiMethod.MODULE_ADVERTISING_START);
        }
        ////////////////////////////////////////
        // modules
        ////////////////////////////////////////

        protected override void HandleCallback(AffiseApiMethod api, object callback, JSONNode? json, string? tag)
        {
            switch (api)
            {
                case AffiseApiMethod.SEND_EVENT_NOW:
                    switch (tag)
                    {
                        case DataName.SUCCESS:
                            (callback as OnSendSuccessCallback)?.Invoke();
                            break;
                        case DataName.FAILED:
                            (callback as OnSendFailedCallback)?.Invoke(DebugUtils.ToResponse(json));
                            break;
                    }
                    break;
                case AffiseApiMethod.ON_INIT_SUCCESS_HANDLER:
                    (callback as OnInitSuccessHandler)?.Invoke();
                    break;
                case AffiseApiMethod.ON_INIT_ERROR_HANDLER:
                    (callback as OnInitErrorHandler)?.Invoke(DataMapper.ToNonNullString(json));
                    break;
                case AffiseApiMethod.REGISTER_DEEPLINK_CALLBACK:
                    (callback as DeeplinkCallback)?.Invoke(DataMapper.ToDeeplinkValue(json));
                    break;
                case AffiseApiMethod.GET_REFERRER_URL_CALLBACK:
                    (callback as OnReferrerCallback)?.Invoke(DataMapper.ToStringOrNull(json));
                    break;
                case AffiseApiMethod.GET_REFERRER_URL_VALUE_CALLBACK:
                    (callback as OnReferrerCallback)?.Invoke(DataMapper.ToStringOrNull(json));
                    break;
                case AffiseApiMethod.GET_DEFERRED_DEEPLINK_CALLBACK:
                    (callback as OnReferrerCallback)?.Invoke(DataMapper.ToStringOrNull(json));
                    break;
                case AffiseApiMethod.GET_DEFERRED_DEEPLINK_VALUE_CALLBACK:
                    (callback as OnReferrerCallback)?.Invoke(DataMapper.ToStringOrNull(json));
                    break;
                case AffiseApiMethod.SKAD_REGISTER_ERROR_CALLBACK:
                    (callback as ErrorCallback)?.Invoke(DataMapper.ToNonNullString(json));
                    break;
                case AffiseApiMethod.SKAD_POSTBACK_ERROR_CALLBACK:
                    (callback as ErrorCallback)?.Invoke(DataMapper.ToNonNullString(json));
                    break;
                case AffiseApiMethod.DEBUG_VALIDATE_CALLBACK:
                    (callback as DebugOnValidateCallback)?.Invoke(DebugUtils.GetValidationStatus(json));
                    break;
                case AffiseApiMethod.DEBUG_NETWORK_CALLBACK:
                    var (request, response) = DebugUtils.ParseRequestResponse(json);
                    (callback as DebugOnNetworkCallback)?.Invoke(request, response);
                    break;
                ////////////////////////////////////////
                // modules
                ////////////////////////////////////////
                case AffiseApiMethod.GET_STATUS_CALLBACK:
                    (callback as OnKeyValueCallback)?.Invoke(DataMapper.ToAffiseKeyValueList(json));
                    break;
                // Module Link
                case AffiseApiMethod.MODULE_LINK_LINK_RESOLVE_CALLBACK:
                    (callback as AffiseLinkCallback)?.Invoke(DataMapper.ToNonNullString(json));
                    break;
                // Module Subscription
                case AffiseApiMethod.MODULE_SUBS_FETCH_PRODUCTS_CALLBACK:
                    (callback as AffiseResultCallback<AffiseProductsResult>)
                        ?.Invoke(DataMapper.ToResultAffiseProductsResult(json));
                    break;
                // Module Subscription
                case AffiseApiMethod.MODULE_SUBS_PURCHASE_CALLBACK:
                    (callback as AffiseResultCallback<AffisePurchasedInfo>)
                        ?.Invoke(DataMapper.ToResultAffisePurchasedInfo(json));
                    break;
                ////////////////////////////////////////
                // modules
                ////////////////////////////////////////
            }
        }
    }
}