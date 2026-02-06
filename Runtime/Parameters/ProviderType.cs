#nullable enable
using System;

namespace AffiseAttributionLib.AffiseParameters
{
    public enum ProviderType
    {
        AFFISE_APP_ID,
        AFFISE_PKG_APP_NAME,
        AFF_APP_NAME_DASHBOARD,
        APP_VERSION,
        APP_VERSION_RAW,
        STORE,
        TRACKER_TOKEN,
        TRACKER_NAME,
        FIRST_TRACKER_TOKEN,
        FIRST_TRACKER_NAME,
        LAST_TRACKER_TOKEN,
        LAST_TRACKER_NAME,
        OUTDATED_TRACKER_TOKEN,
        INSTALLED_TIME,
        FIRST_OPEN_TIME,
        INSTALLED_HOUR,
        FIRST_OPEN_HOUR,
        INSTALL_FIRST_EVENT,
        INSTALL_BEGIN_TIME,
        INSTALL_FINISH_TIME,
        REFERRER_INSTALL_VERSION,
        REFERRAL_TIME,
        REFERRER_CLICK_TIME,
        REFERRER_CLICK_TIME_SERVER,
        REFERRER_GOOGLE_PLAY_INSTANT,
        CREATED_TIME,
        CREATED_TIME_MILLI,
        CREATED_TIME_HOUR,
        UNINSTALL_TIME,
        REINSTALL_TIME,
        LAST_SESSION_TIME,
        CPU_TYPE,
        HARDWARE_NAME,
        DEVICE_MANUFACTURER,
        DEEPLINK_CLICK,
        DEVICE_ATLAS_ID,
        AFFISE_DEVICE_ID,
        AFFISE_ALT_DEVICE_ID,
        ANDROID_ID,
        ANDROID_ID_MD5,
        REFTOKEN,
        REFTOKENS,
        REFERRER,
        REFERRER_UPDATED,
        USER_AGENT,
        MCCODE,
        MNCODE,
        REGION,
        COUNTRY,
        LANGUAGE,
        DEVICE_NAME,
        DEVICE_TYPE,
        OS_NAME,
        PLATFORM,
        SDK_PLATFORM,
        API_LEVEL_OS,
        AFFISE_SDK_VERSION,
        OS_VERSION,
        RANDOM_USER_ID,
        AFFISE_SDK_POS,
        TIMEZONE_DEV,
        AFFISE_EVENT_NAME,
        AFFISE_EVENT_TOKEN,
        LAST_TIME_SESSION,
        TIME_SESSION,
        AFFISE_SESSION_COUNT,
        LIFETIME_SESSION_COUNT,
        AFFISE_DEEPLINK,
        AFFISE_PART_PARAM_NAME,
        AFFISE_PART_PARAM_NAME_TOKEN,
        AFFISE_APP_TOKEN,
        LABEL,
        AFFISE_SDK_SECRET_ID,
        UUID,
        AFFISE_APP_OPENED,
        PUSHTOKEN,
        PUSHTOKEN_SERVICE,
        AFFISE_EVENTS_COUNT,
        AFFISE_SDK_EVENTS_COUNT,
        AFFISE_METRICS_EVENTS_COUNT,
        AFFISE_INTERNAL_EVENTS_COUNT,
        IS_ROOTED,
        IS_EMULATOR,

        // remarketing
        OS_AND_VERSION,
        DEVICE,
        BUILD,
        
        // advertising
        GAID_ADID,
        GAID_ADID_MD5,
        OAID,
        OAID_MD5,
        ADID,
        ALTSTR_ADID,
        FIREOS_ADID,
        COLOROS_ADID,

        // meta
        META,

        // network
        MAC_SHA1,
        MAC_MD5,
        CONNECTION_TYPE,
        PROXY_IP_ADDRESS,

        // phone
        NETWORK_TYPE,
        ISP,
    }

    public static class ProviderTypeExt
    {
        public static string Provider(this ProviderType param)
        {
            return param switch
            {
                ProviderType.AFFISE_APP_ID => "affise_app_id",
                ProviderType.AFFISE_PKG_APP_NAME => "affise_pkg_app_name",
                ProviderType.AFF_APP_NAME_DASHBOARD => "affise_app_name_dashboard",
                ProviderType.APP_VERSION => "app_version",
                ProviderType.APP_VERSION_RAW => "app_version_raw",
                ProviderType.STORE => "store",
                ProviderType.TRACKER_TOKEN => "tracker_token",
                ProviderType.TRACKER_NAME => "tracker_name",
                ProviderType.FIRST_TRACKER_TOKEN => "first_tracker_token",
                ProviderType.FIRST_TRACKER_NAME => "first_tracker_name",
                ProviderType.LAST_TRACKER_TOKEN => "last_tracker_token",
                ProviderType.LAST_TRACKER_NAME => "last_tracker_name",
                ProviderType.OUTDATED_TRACKER_TOKEN => "outdated_tracker_token",
                ProviderType.INSTALLED_TIME => "installed_time",
                ProviderType.FIRST_OPEN_TIME => "first_open_time",
                ProviderType.INSTALLED_HOUR => "installed_hour",
                ProviderType.FIRST_OPEN_HOUR => "first_open_hour",
                ProviderType.INSTALL_FIRST_EVENT => "install_first_event",
                ProviderType.INSTALL_BEGIN_TIME => "install_begin_time",
                ProviderType.INSTALL_FINISH_TIME => "install_finish_time",
                ProviderType.REFERRER_INSTALL_VERSION => "referrer_install_version",
                ProviderType.REFERRAL_TIME => "referral_time",
                ProviderType.REFERRER_CLICK_TIME => "referrer_click_time",
                ProviderType.REFERRER_CLICK_TIME_SERVER => "referrer_click_time_server",
                ProviderType.REFERRER_GOOGLE_PLAY_INSTANT => "referrer_google_play_instant",
                ProviderType.CREATED_TIME => "created_time",
                ProviderType.CREATED_TIME_MILLI => "created_time_milli",
                ProviderType.CREATED_TIME_HOUR => "created_time_hour",
                ProviderType.UNINSTALL_TIME => "uninstall_time",
                ProviderType.REINSTALL_TIME => "reinstall_time",
                ProviderType.LAST_SESSION_TIME => "last_session_time",
                ProviderType.CPU_TYPE => "cpu_type",
                ProviderType.HARDWARE_NAME => "hardware_name",
                ProviderType.DEVICE_MANUFACTURER => "device_manufacturer",
                ProviderType.DEEPLINK_CLICK => "deeplink_click",
                ProviderType.DEVICE_ATLAS_ID => "device_atlas_id",
                ProviderType.AFFISE_DEVICE_ID => "affise_device_id",
                ProviderType.AFFISE_ALT_DEVICE_ID => "affise_alt_device_id",
                ProviderType.ANDROID_ID => "android_id",
                ProviderType.ANDROID_ID_MD5 => "android_id_md5",
                ProviderType.REFTOKEN => "reftoken",
                ProviderType.REFTOKENS => "reftokens",
                ProviderType.REFERRER => "referrer",
                ProviderType.REFERRER_UPDATED => "referrer_updated",
                ProviderType.USER_AGENT => "user_agent",
                ProviderType.MCCODE => "mccode",
                ProviderType.MNCODE => "mncode",
                ProviderType.REGION => "region",
                ProviderType.COUNTRY => "country",
                ProviderType.LANGUAGE => "language",
                ProviderType.DEVICE_NAME => "device_name",
                ProviderType.DEVICE_TYPE => "device_type",
                ProviderType.OS_NAME => "os_name",
                ProviderType.PLATFORM => "platform",
                ProviderType.SDK_PLATFORM => "sdk_platform",
                ProviderType.API_LEVEL_OS => "api_level_os",
                ProviderType.AFFISE_SDK_VERSION => "affise_sdk_version",
                ProviderType.OS_VERSION => "os_version",
                ProviderType.RANDOM_USER_ID => "random_user_id",
                ProviderType.AFFISE_SDK_POS => "affise_sdk_pos",
                ProviderType.TIMEZONE_DEV => "timezone_dev",
                ProviderType.AFFISE_EVENT_NAME => "affise_event_name",
                ProviderType.AFFISE_EVENT_TOKEN => "affise_event_token",
                ProviderType.LAST_TIME_SESSION => "last_time_session",
                ProviderType.TIME_SESSION => "time_session",
                ProviderType.AFFISE_SESSION_COUNT => "affise_session_count",
                ProviderType.LIFETIME_SESSION_COUNT => "lifetime_session_count",
                ProviderType.AFFISE_DEEPLINK => "affise_deeplink",
                ProviderType.AFFISE_PART_PARAM_NAME => "affise_part_param_name",
                ProviderType.AFFISE_PART_PARAM_NAME_TOKEN => "affise_part_param_name_token",
                ProviderType.AFFISE_APP_TOKEN => "affise_app_token",
                ProviderType.LABEL => "label",
                ProviderType.AFFISE_SDK_SECRET_ID => "affise_sdk_secret_id",
                ProviderType.UUID => "uuid",
                ProviderType.AFFISE_APP_OPENED => "affise_app_opened",
                ProviderType.PUSHTOKEN => "pushtoken",
                ProviderType.PUSHTOKEN_SERVICE => "pushtoken_service",
                ProviderType.AFFISE_EVENTS_COUNT => "affise_events_count",
                ProviderType.AFFISE_SDK_EVENTS_COUNT => "affise_sdk_events_count",
                ProviderType.AFFISE_METRICS_EVENTS_COUNT => "affise_metrics_events_count",
                ProviderType.AFFISE_INTERNAL_EVENTS_COUNT => "affise_internal_events_count",
                ProviderType.IS_ROOTED => "is_rooted",
                ProviderType.IS_EMULATOR => "is_emulator",

                // remarketing
                ProviderType.OS_AND_VERSION => "os_and_version",
                ProviderType.DEVICE => "device",
                ProviderType.BUILD => "build",
                
                // advertising
                ProviderType.GAID_ADID => "gaid_adid",
                ProviderType.GAID_ADID_MD5 => "gaid_adid_md5",
                ProviderType.OAID => "oaid",
                ProviderType.OAID_MD5 => "oaid_md5",
                ProviderType.ADID => "adid",
                ProviderType.ALTSTR_ADID => "altstr_adid",
                ProviderType.FIREOS_ADID => "fireos_adid",
                ProviderType.COLOROS_ADID => "coloros_adid",

                // meta
                ProviderType.META => "meta",

                // network
                ProviderType.MAC_SHA1 => "mac_sha1",
                ProviderType.MAC_MD5 => "mac_md5",
                ProviderType.CONNECTION_TYPE => "connection_type",
                ProviderType.PROXY_IP_ADDRESS => "proxy_ip_address",

                // phone
                ProviderType.NETWORK_TYPE => "network_type",
                ProviderType.ISP => "isp",
                
                _ => ""
            };
        }


        public static ProviderType? From(string? value)
        {
            if (value is null) return null;
            foreach (var type in Enum.GetValues(typeof(ProviderType)))
            {
                if (type is not ProviderType providerType) continue;
                if (providerType.Provider() == value) return providerType;
            }
            return null;
        }
    }
}