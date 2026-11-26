#nullable enable
using System.Collections.Generic;
using AffiseAttributionLib.AffiseParameters.Base;
using AffiseAttributionLib.Module.Phone.Providers;
using AffiseAttributionLib.Modules;

namespace AffiseAttributionLib.Module.Phone
{
    public class PhoneModule : AffiseModule
    {
        private readonly List<Provider> _providers = new();

        public override void Start()
        {
            _providers.AddRange(new Provider[]
            {
                new IspNameProvider(),
                new NetworkTypeProvider()
            });
        }

        public override IEnumerable<Provider> Providers() => _providers;
    }
}
