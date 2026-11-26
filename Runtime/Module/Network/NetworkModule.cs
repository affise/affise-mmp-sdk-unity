#nullable enable
using System.Collections.Generic;
using AffiseAttributionLib.AffiseParameters.Base;
using AffiseAttributionLib.Module.Network.Providers;
using AffiseAttributionLib.Module.Network.UseCase;
using AffiseAttributionLib.Modules;

namespace AffiseAttributionLib.Module.Network
{
    public class NetworkModule : AffiseModule
    {
        private readonly List<Provider> _providers = new();

        public override void Start()
        {
            _providers.AddRange(new Provider[]
            {
                new ConnectionTypeProvider(),
                new ProxyIpAddressProvider(new ProxyInfoUseCase())
            });
        }

        public override IEnumerable<Provider> Providers() => _providers;
    }
}
