using AffiseAttributionLib.AffiseParameters.Base;
using AffiseAttributionLib.AffiseParameters;

namespace AffiseAttributionLib.Module.Phone.Providers
{
    /**
     * Provider for parameter [ProviderType.ISP]
     */
    internal class IspNameProvider : StringPropertyProvider
    {
        public override float Order => 37.1f;
        public override ProviderType? Key => ProviderType.ISP;
        public override string Provide() => null;
    }
}
