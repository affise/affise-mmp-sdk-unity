using AffiseAttributionLib.AffiseParameters.Base;

namespace AffiseAttributionLib.AffiseParameters.Providers
{
    /**
     * Provider for parameter [ProviderType.AFFISE_SDK_VERSION]
     */
    internal class AffSDKVersionProvider : StringPropertyProvider
    {
        public override float Order => 47.0f;
        public override ProviderType? Key => ProviderType.AFFISE_SDK_VERSION;
        public override string Provide() => "1.6.48";
    }
}