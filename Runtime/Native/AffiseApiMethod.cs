#nullable enable
using System;

namespace AffiseAttributionLib.Native
{
    internal enum AffiseApiMethod
    {
        INIT,
        IS_INITIALIZED,
        SEND_EVENT,
        SEND_EVENT_NOW,
        ADD_PUSH_TOKEN,
        REGISTER_WEB_VIEW,
        UNREGISTER_WEB_VIEW,
        SET_SECRET_KEY,
        SET_AUTO_CATCHING_TYPES,
        SET_OFFLINE_MODE_ENABLED,
        IS_OFFLINE_MODE_ENABLED,
        SET_BACKGROUND_TRACKING_ENABLED,
        IS_BACKGROUND_TRACKING_ENABLED,
        SET_TRACKING_ENABLED,
        IS_TRACKING_ENABLED,
        FORGET,
        SET_ENABLED_METRICS,
        CRASH_APPLICATION,
        GET_RANDOM_USER_ID,
        GET_RANDOM_DEVICE_ID,
        GET_PROVIDERS,
        IS_FIRST_RUN,

        // callbacks
        ON_INIT_SUCCESS_HANDLER,
        ON_INIT_ERROR_HANDLER,
        GET_REFERRER_URL_CALLBACK,
        GET_REFERRER_URL_VALUE_CALLBACK,
        GET_DEFERRED_DEEPLINK_CALLBACK,
        GET_DEFERRED_DEEPLINK_VALUE_CALLBACK,
        REGISTER_DEEPLINK_CALLBACK,
        SKAD_REGISTER_ERROR_CALLBACK,
        SKAD_POSTBACK_ERROR_CALLBACK,
        
        // debug
        DEBUG_VALIDATE_CALLBACK,
        DEBUG_NETWORK_CALLBACK,
        DEBUG_VERSION_NATIVE,
        ////////////////////////////////////////
        // modules
        ////////////////////////////////////////
        MODULE_START,
        GET_MODULES_INSTALLED,
        GET_STATUS_CALLBACK,
        // AppsFlyer Module
        MODULE_APPSFLYER_LOG_EVENT,
        // Link Module
        MODULE_LINK_LINK_RESOLVE_CALLBACK,
        // Module Subscription
        MODULE_SUBS_FETCH_PRODUCTS_CALLBACK,
        MODULE_SUBS_PURCHASE_CALLBACK,
        // TikTok Module
        MODULE_TIKTOK_EVENT,
        ////////////////////////////////////////
        // modules
        ////////////////////////////////////////
    }
    
    internal static class AffiseApiMethodExt
    {
        public static AffiseApiMethod? ToAffiseApiMethod(this string? name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;
            foreach (var value in Enum.GetValues(typeof(AffiseApiMethod)))
            {
                if (value is not AffiseApiMethod method) continue;
                if (name == method.ToMethod()) return method;
            }
            return null;
        }

        public static string? ToMethod(this AffiseApiMethod method)
        {
            return method switch
            {
                AffiseApiMethod.INIT => "init",
                AffiseApiMethod.IS_INITIALIZED => "is_initialized",
                AffiseApiMethod.SEND_EVENT => "send_event",
                AffiseApiMethod.SEND_EVENT_NOW => "send_event_now",
                AffiseApiMethod.ADD_PUSH_TOKEN => "add_push_token",
                AffiseApiMethod.REGISTER_WEB_VIEW => "register_web_view",
                AffiseApiMethod.UNREGISTER_WEB_VIEW => "unregister_web_view",
                AffiseApiMethod.SET_SECRET_KEY => "set_secret_id",
                AffiseApiMethod.SET_AUTO_CATCHING_TYPES => "set_auto_catching_types",
                AffiseApiMethod.SET_OFFLINE_MODE_ENABLED => "set_offline_mode_enabled",
                AffiseApiMethod.IS_OFFLINE_MODE_ENABLED => "is_offline_mode_enabled",
                AffiseApiMethod.SET_BACKGROUND_TRACKING_ENABLED => "set_background_tracking_enabled",
                AffiseApiMethod.IS_BACKGROUND_TRACKING_ENABLED => "is_background_tracking_enabled",
                AffiseApiMethod.SET_TRACKING_ENABLED => "set_tracking_enabled",
                AffiseApiMethod.IS_TRACKING_ENABLED => "is_tracking_enabled",
                AffiseApiMethod.FORGET => "forget",
                AffiseApiMethod.SET_ENABLED_METRICS => "set_enabled_metrics",
                AffiseApiMethod.CRASH_APPLICATION => "crash_application",
                AffiseApiMethod.GET_RANDOM_USER_ID => "get_random_user_id",
                AffiseApiMethod.GET_RANDOM_DEVICE_ID => "get_random_device_id",
                AffiseApiMethod.GET_PROVIDERS => "get_providers",
                AffiseApiMethod.IS_FIRST_RUN => "is_first_run",
                // callbacks
                AffiseApiMethod.ON_INIT_SUCCESS_HANDLER => "on_init_success_handler",
                AffiseApiMethod.ON_INIT_ERROR_HANDLER => "on_init_error_handler",
                AffiseApiMethod.GET_REFERRER_URL_CALLBACK => "get_referrer_url_callback",
                AffiseApiMethod.GET_REFERRER_URL_VALUE_CALLBACK => "get_referrer_url_value_callback",
                AffiseApiMethod.GET_DEFERRED_DEEPLINK_CALLBACK => "get_deferred_deeplink_callback",
                AffiseApiMethod.GET_DEFERRED_DEEPLINK_VALUE_CALLBACK => "get_deferred_deeplink_value_callback",
                AffiseApiMethod.REGISTER_DEEPLINK_CALLBACK => "register_deeplink_callback",
                AffiseApiMethod.SKAD_REGISTER_ERROR_CALLBACK => "skad_register_error_callback",
                AffiseApiMethod.SKAD_POSTBACK_ERROR_CALLBACK => "skad_postback_error_callback",
                // debug
                AffiseApiMethod.DEBUG_VALIDATE_CALLBACK => "debug_validate_callback",
                AffiseApiMethod.DEBUG_NETWORK_CALLBACK => "debug_network_callback",
                AffiseApiMethod.DEBUG_VERSION_NATIVE => "debug_version_native",

                ////////////////////////////////////////
                // modules
                ////////////////////////////////////////
                AffiseApiMethod.MODULE_START => "module_start",
                AffiseApiMethod.GET_MODULES_INSTALLED => "get_modules_installed",
                AffiseApiMethod.GET_STATUS_CALLBACK => "get_status_callback",
                // AppsFlyer Module
                AffiseApiMethod.MODULE_APPSFLYER_LOG_EVENT => "module_appsflyer_log_event",
                // Link Module
                AffiseApiMethod.MODULE_LINK_LINK_RESOLVE_CALLBACK => "module_link_link_resolve_callback",
                // Module Subscription
                AffiseApiMethod.MODULE_SUBS_FETCH_PRODUCTS_CALLBACK => "module_subs_fetch_products_callback",
                AffiseApiMethod.MODULE_SUBS_PURCHASE_CALLBACK => "module_subs_purchase_callback",
                // TikTok Module
                AffiseApiMethod.MODULE_TIKTOK_EVENT => "module_tiktok_event",
                ////////////////////////////////////////
                // modules
                ////////////////////////////////////////
                _ => null
            };
        }
    }
}