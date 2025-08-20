#nullable enable
using System;

namespace AffiseAttributionLib.Modules
{
    public enum AffiseModules
    {
        AdService,
        Advertising,
        AndroidId,
        AppsFlyer,
        Link,
        Meta,
        Network,
        Persistent,
        Phone,
        Status,
        Subscription,
        RuStore,
        Huawei,
        TikTok,
    }
    
    internal static class AffiseModulesExt
    {
        public static string Module(this AffiseModules key)
        {
            return key switch
            {
                AffiseModules.AdService => "AdService",
                AffiseModules.Advertising => "Advertising",
                AffiseModules.AndroidId => "AndroidId",
                AffiseModules.AppsFlyer => "AppsFlyer",
                AffiseModules.Link => "Link",
                AffiseModules.Meta => "Meta",
                AffiseModules.Network => "Network",
                AffiseModules.Persistent => "Persistent",
                AffiseModules.Phone => "Phone",
                AffiseModules.Status => "Status",
                AffiseModules.Subscription => "Subscription",
                AffiseModules.RuStore => "RuStore",
                AffiseModules.Huawei => "Huawei",
                AffiseModules.TikTok => "TikTok",
                _ => ""
            };
        }
        public static AffiseModules? From(string? value)
        {
            if (value is null) return null;
            foreach (var type in Enum.GetValues(typeof(AffiseModules)))
            {
                if (type is not AffiseModules modules) continue;
                if (modules.Module() == value) return modules;
            }
            return null;
        }
    }
}