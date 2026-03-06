#nullable enable
using AffiseAttributionLib.SKAd;

namespace AffiseAttributionLib.Module.Attribution
{
    public interface IAffiseIOSApi
    {
        /**
         * StoreKit Ad Network register app
         */
        public void RegisterAppForAdNetworkAttribution(ErrorCallback completionHandler);

        /**
         * StoreKit Ad Network updatePostbackConversionValue
         */
        public void UpdatePostbackConversionValue(
            int fineValue,
            CoarseValue coarseValue,
            ErrorCallback completionHandler
        );
    }
}