#nullable enable
using AffiseAttributionLib.AffiseParameters;
using AffiseAttributionLib.AffiseParameters.Base;
using AffiseAttributionLib.Module.Network.UseCase;

namespace AffiseAttributionLib.Module.Network.Providers
{
    internal sealed class ProxyIpAddressProvider : StringPropertyProvider
    {
        private readonly ProxyInfoUseCase _proxyInfo;

        public ProxyIpAddressProvider(ProxyInfoUseCase proxyInfo) => _proxyInfo = proxyInfo;

        public override float Order => 24.1f;
        public override ProviderType? Key => ProviderType.PROXY_IP_ADDRESS;

        public override string? Provide() => _proxyInfo.GetProxyHost();
    }
}
