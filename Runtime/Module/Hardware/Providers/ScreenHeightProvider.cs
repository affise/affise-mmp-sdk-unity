#nullable enable
using AffiseAttributionLib.AffiseParameters;
using AffiseAttributionLib.AffiseParameters.Base;
using AffiseAttributionLib.Module.Hardware.UseCase;

namespace AffiseAttributionLib.Module.Hardware.Providers
{
    internal sealed class ScreenHeightProvider : LongPropertyProvider
    {
        private readonly ScreenUseCase _screen;

        public ScreenHeightProvider(ScreenUseCase screen) => _screen = screen;

        public override float Order => 42.2f;
        public override ProviderType? Key => ProviderType.SCREEN_HEIGHT;

        public override long? Provide() => _screen.Height;
    }
}
