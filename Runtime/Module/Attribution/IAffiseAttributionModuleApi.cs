#nullable enable
using System;
using System.Collections.Generic;
using AffiseAttributionLib.Module.Advertising;
using AffiseAttributionLib.Module.AppsFlyer;
using AffiseAttributionLib.Module.Link;
using AffiseAttributionLib.Module.Subscription;
using AffiseAttributionLib.Module.TikTok;
using AffiseAttributionLib.Modules;

namespace AffiseAttributionLib.Module.Attribution
{
    public interface IAffiseAttributionModuleApi
    {
        internal const string NotSupported = "[Affise] platform not supported";
     
        public IAffiseModuleAdvertisingApi Advertising { get; }
        public IAffiseModuleLinkApi Link { get; }
        public IAffiseModuleAppsFlyerApi AppsFlyer { get; }
        public IAffiseModuleSubscriptionApi Subscription { get; }
        public IAffiseModuleTikTokApi TikTok { get; }

        /**
         * Get module status
         */
        public void GetStatus(AffiseModules module, OnKeyValueCallback onComplete);

        /**
         * Get installed modules
         */
        public List<AffiseModules> GetModulesInstalled();

        /**
         * Module Link url Resolve
         */
        [Obsolete("use Affise.Module.Link." + nameof(Affise.Module.Link.Resolve) + " instead.")]
        public void LinkResolve(string uri, AffiseLinkCallback callback);

        /**
         * Module Subscription fetch products
         */
        [Obsolete("use Affise.Module.Subscription." + nameof(Affise.Module.Subscription.FetchProducts) + " instead.")]
        public void FetchProducts(List<string> ids, AffiseResultCallback<AffiseProductsResult> callback);

        /**
         * Module Subscription purchase
         */
        [Obsolete("use Affise.Module.Subscription." + nameof(Affise.Module.Subscription.Purchase) + " instead.")]
        public void Purchase(
            AffiseProduct product,
            AffiseProductType type,
            AffiseResultCallback<AffisePurchasedInfo> callback
        );
    }
}