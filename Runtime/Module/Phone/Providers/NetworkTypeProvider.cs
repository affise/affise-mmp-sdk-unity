using AffiseAttributionLib.AffiseParameters.Base;
using AffiseAttributionLib.AffiseParameters;

namespace AffiseAttributionLib.Module.Phone.Providers
{
    /**
     * Provider for parameter [ProviderType.NETWORK_TYPE]
     */
    internal class NetworkTypeProvider : StringPropertyProvider
    {
        public override float Order => 23.1f;
        public override ProviderType? Key => ProviderType.NETWORK_TYPE;
        public override string Provide() => null;
    }
}
