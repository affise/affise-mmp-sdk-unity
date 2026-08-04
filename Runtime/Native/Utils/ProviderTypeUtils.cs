#nullable enable
using System.Collections.Generic;
using AffiseAttributionLib.AffiseParameters;
using AffiseAttributionLib.SimpleJSON;

namespace AffiseAttributionLib.Native.Utils
{
    internal static class ProviderTypeUtils
    {
        public static Dictionary<ProviderType, object?> ToProviderMap(this JSONObject? json)
        {
            var result = new Dictionary<ProviderType, object?>();
            if (json is null) return result;
            if (json.IsNull) return result;

            foreach (var jsonKey in json.Keys)
            {
                var type = ProviderTypeExt.From(jsonKey);
                if (type is null) continue;
                var providerType = (ProviderType)type;
                result[providerType] = json[jsonKey]?.ToType();
            }

            return result;
        }

        private static object? ToType(this JSONNode json)
        {
            if (json.IsNull) return null;
            if (json.IsString)
            {
                return json.Value;
            }

            if (json.IsNumber)
            {
                return json.AsLong;
            }

            if (json.IsBoolean)
            {
                return json.AsBool;
            }

            return null;
        }

        private static object? ToProviderType(this JSONNode json, ProviderType type)
        {
            if (json.IsNull) return null;

            switch (type)
            {
                case ProviderType.AFFISE_APP_TOKEN:
                case ProviderType.AFFISE_ALT_DEVICE_ID:
                case ProviderType.AFFISE_APP_ID:
                case ProviderType.AFFISE_DEVICE_ID:
                case ProviderType.AFFISE_PKG_APP_NAME:
                case ProviderType.AFFISE_PART_PARAM_NAME:
                case ProviderType.AFFISE_PART_PARAM_NAME_TOKEN:
                case ProviderType.AFFISE_SDK_SECRET_ID:
                case ProviderType.AFFISE_SDK_VERSION:
                case ProviderType.ANDROID_ID:
                case ProviderType.API_LEVEL_OS:
                case ProviderType.APP_VERSION:
                case ProviderType.APP_VERSION_RAW:
                case ProviderType.CONNECTION_TYPE:
                case ProviderType.CPU_TYPE:
                case ProviderType.AFFISE_DEEPLINK:
                case ProviderType.DEVICE_MANUFACTURER:
                case ProviderType.DEVICE_NAME:
                case ProviderType.DEVICE_TYPE:
                case ProviderType.GAID_ADID_MD5:
                case ProviderType.GAID_ADID:
                case ProviderType.HARDWARE_NAME:
                case ProviderType.REFERRER:
                case ProviderType.ISP:
                case ProviderType.AFFISE_SDK_POS:
                case ProviderType.LANGUAGE:
                case ProviderType.NETWORK_TYPE:
                case ProviderType.OS_NAME:
                case ProviderType.OS_VERSION:
                case ProviderType.PLATFORM:
                case ProviderType.PUSHTOKEN:
                case ProviderType.PUSHTOKEN_SERVICE:
                case ProviderType.RANDOM_USER_ID:
                case ProviderType.REFERRER_INSTALL_VERSION:
                case ProviderType.REFTOKEN:
                case ProviderType.SDK_PLATFORM:
                case ProviderType.STORE:
                case ProviderType.TIMEZONE_DEV:
                case ProviderType.USER_AGENT:
                case ProviderType.UUID:
                    if (json.IsString)
                    {
                        return json.Value;
                    }

                    break;
                case ProviderType.AFFISE_APP_OPENED:
                case ProviderType.AFFISE_SESSION_COUNT:
                case ProviderType.CREATED_TIME_HOUR:
                case ProviderType.CREATED_TIME_MILLI:
                case ProviderType.CREATED_TIME:
                case ProviderType.FIRST_OPEN_HOUR:
                case ProviderType.FIRST_OPEN_TIME:
                case ProviderType.INSTALL_BEGIN_TIME:
                case ProviderType.INSTALLED_HOUR:
                case ProviderType.INSTALLED_TIME:
                case ProviderType.INSTALL_FINISH_TIME:
                case ProviderType.LAST_SESSION_TIME:
                case ProviderType.LIFETIME_SESSION_COUNT:
                case ProviderType.REFERRER_CLICK_TIME:
                case ProviderType.REFERRER_CLICK_TIME_SERVER:
                case ProviderType.TIME_SESSION:
                    if (json.IsNumber)
                    {
                        return json.AsLong;
                    }

                    break;
                case ProviderType.DEEPLINK_CLICK:
                case ProviderType.INSTALL_FIRST_EVENT:
                case ProviderType.REFERRER_GOOGLE_PLAY_INSTANT:
                    if (json.IsBoolean)
                    {
                        return json.AsBool;
                    }

                    break;
                default:
                    return null;
            }

            return null;
        }
    }
}