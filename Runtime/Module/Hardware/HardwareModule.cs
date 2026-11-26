#nullable enable
using System.Collections.Generic;
using AffiseAttributionLib.AffiseParameters.Base;
using AffiseAttributionLib.AffiseParameters.Providers;
using AffiseAttributionLib.Module.Hardware.Providers;
using AffiseAttributionLib.Module.Hardware.UseCase;
using AffiseAttributionLib.Modules;

namespace AffiseAttributionLib.Module.Hardware
{
    internal sealed class HardwareModule : AffiseModule
    {
        private readonly List<Provider> _providers = new();

        public override void Start()
        {
            var screen = new ScreenUseCase();
            var processInfo = new ProcessInfoUseCase();
            var hardwareInfo = new HardwareInfoUseCase();
            var remarketing = new RemarketingUseCase();

            _providers.AddRange(new Provider[]
            {
                new UserAgentProvider(),
                new CpuTypeProvider(processInfo),
                new CpuCoresProvider(processInfo),
                new DeviceProvider(remarketing),
                new BuildProvider(remarketing),
                new DeviceNameProvider(hardwareInfo),
                new ScreenWidthProvider(screen),
                new ScreenHeightProvider(screen),
                new DensityProvider(screen),
                new HardwareNameProvider(hardwareInfo),
                new DeviceTypeProvider(hardwareInfo),
                new OSVersionProvider(hardwareInfo),
                new OsAndVersionProvider(remarketing),
                new MCCProvider(),
                new MNCProvider(),
                new TimezoneDeviceProvider(),
                new LanguageProvider(remarketing),
                new CountryProvider(remarketing),
                new RegionProvider(remarketing)
            });
        }

        public override IEnumerable<Provider> Providers() => _providers;
    }
}
