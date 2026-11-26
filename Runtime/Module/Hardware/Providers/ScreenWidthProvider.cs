#nullable enable
using AffiseAttributionLib.AffiseParameters;
using AffiseAttributionLib.AffiseParameters.Base;
using AffiseAttributionLib.Module.Hardware.UseCase;

namespace AffiseAttributionLib.Module.Hardware.Providers
{
    internal sealed class ScreenWidthProvider : LongPropertyProvider
    {
        private readonly ScreenUseCase _screen;

        public ScreenWidthProvider(ScreenUseCase screen) => _screen = screen;

        public override float Order => 42.1f;
        public override ProviderType? Key => ProviderType.SCREEN_WIDTH;

        public override long? Provide() => _screen.Width;
    }
}
