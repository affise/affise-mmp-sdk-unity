#nullable enable
using System.Collections.Generic;
using AffiseAttributionLib.Module.Advertising;
using AffiseAttributionLib.Module.AppsFlyer;
using AffiseAttributionLib.Module.Link;
using AffiseAttributionLib.Module.Subscription;
using AffiseAttributionLib.Module.TikTok;
using AffiseAttributionLib.Modules;
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
using AffiseAttributionLib.Native;
#endif

namespace AffiseAttributionLib.Module.Attribution
{
    internal class AffiseAttributionModule : IAffiseAttributionModuleApi
    {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
        private IAffiseNative? _native => Affise._native;
#else
        private AffiseComponent? _api => Affise._api;
#endif

        public IAffiseModuleAdvertisingApi Advertising { get; }
        public IAffiseModuleLinkApi Link { get; }
        public IAffiseModuleAppsFlyerApi AppsFlyer { get; }
        public IAffiseModuleSubscriptionApi Subscription { get; }

        public IAffiseModuleTikTokApi TikTok { get; }

        public AffiseAttributionModule()
        {
            Advertising = new AffiseAdvertising();
            Link = new AffiseLink();
            AppsFlyer = new AffiseAppsFlyer();
            Subscription = new AffiseSubscription();
            TikTok = new AffiseTikTok();
        }

        /**
         * Get module status
         */
        public void GetStatus(AffiseModules module, OnKeyValueCallback onComplete)
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            _native?.GetStatus(module, onComplete);
#else
            _api?.ModuleManager.Status(module, onComplete);
#endif
        }

        /**
         * Get installed modules
         */
        public List<AffiseModules> GetModulesInstalled()
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            return _native?.GetModules() ?? new List<AffiseModules>();
#else
            return _api?.ModuleManager.GetModules() ?? new List<AffiseModules>();
#endif
        }
    }
}