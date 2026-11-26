#nullable enable
using AffiseAttributionLib.AffiseParameters;
using AffiseAttributionLib.AffiseParameters.Base;
using AffiseAttributionLib.Module.Hardware.UseCase;

namespace AffiseAttributionLib.Module.Hardware.Providers
{
    internal sealed class DensityProvider : FloatPropertyProvider
    {
        private readonly ScreenUseCase _screen;

        public DensityProvider(ScreenUseCase screen) => _screen = screen;

        public override float Order => 42.3f;
        public override ProviderType? Key => ProviderType.DENSITY;

        public override float? Provide() => _screen.Density;
    }
}
