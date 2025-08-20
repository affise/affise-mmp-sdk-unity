#nullable enable
using System;
using System.Collections.Generic;
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

        public IAffiseModuleLinkApi Link { get; }
        public IAffiseModuleAppsFlyerApi AppsFlyer { get; }
        public IAffiseModuleSubscriptionApi Subscription { get; }

        public IAffiseModuleTikTokApi TikTok { get; }

        public AffiseAttributionModule()
        {
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
         * Manual module start
         */
        public bool ModuleStart(AffiseModules module)
        {
#if (UNITY_ANDROID || UNITY_IOS) && !UNITY_EDITOR
            return _native?.ModuleStart(module) ?? true;
#else
            return _api?.ModuleManager.ManualStart(module) ?? true;
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

        /**
         * Module Link url Resolve
         */
        [Obsolete("use Affise.Module.Link." + nameof(Affise.Module.Link.Resolve) + " instead.")]
        public void LinkResolve(string uri, AffiseLinkCallback callback)
        {
            Link.Resolve(uri, callback);
        }

        /**
         * Module Subscription fetch products
         */
        [Obsolete("use Affise.Module.Subscription." + nameof(Affise.Module.Subscription.FetchProducts) + " instead.")]
        public void FetchProducts(List<string> ids, AffiseResultCallback<AffiseProductsResult> callback)
        {
            Subscription.FetchProducts(ids, callback);
        }

        /**
         * Module Subscription purchase
         */
        [Obsolete("use Affise.Module.Subscription." + nameof(Affise.Module.Subscription.Purchase) + " instead.")]
        public void Purchase(
            AffiseProduct product,
            AffiseProductType type,
            AffiseResultCallback<AffisePurchasedInfo> callback
        )
        {
            Subscription.Purchase(product, type, callback);
        }
    }
}