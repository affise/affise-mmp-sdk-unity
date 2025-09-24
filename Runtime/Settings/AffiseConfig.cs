#nullable enable
using System;
using System.Collections.Generic;

namespace AffiseAttributionLib.Settings
{
    public enum AffiseConfig
    {
        FB_APP_ID,
    }
    
    internal static class AffiseConfigExt
    {
        public static string ToValue(this AffiseConfig key)
        {
            return key switch
            {
                AffiseConfig.FB_APP_ID => "fb_app_id",
                _ => ""
            };
        }
        public static AffiseConfig? From(string? value)
        {
            if (value is null) return null;
            foreach (var type in Enum.GetValues(typeof(AffiseConfig)))
            {
                if (type is not AffiseConfig modules) continue;
                if (modules.ToValue() == value) return modules;
            }
            return null;
        }

        internal static Dictionary<string, Object> ToMapString(this Dictionary<AffiseConfig, Object> from)
        {
            var result = new Dictionary<string, object>();
            foreach (var (key, value) in from)
            {
                result.Add(key.ToValue(), value);
            }
            return result;
        }
    }
}