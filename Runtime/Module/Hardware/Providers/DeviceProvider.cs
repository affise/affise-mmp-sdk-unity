#nullable enable
using AffiseAttributionLib.AffiseParameters;
using AffiseAttributionLib.AffiseParameters.Base;
using AffiseAttributionLib.Module.Hardware.UseCase;

namespace AffiseAttributionLib.Module.Hardware.Providers
{
    internal sealed class DeviceProvider : StringPropertyProvider
    {
        private readonly RemarketingUseCase _remarketing;

        public DeviceProvider(RemarketingUseCase remarketing) => _remarketing = remarketing;

        public override float Order => 69.0f;
        public override ProviderType? Key => ProviderType.DEVICE;

        public override string? Provide() => _remarketing.Device;
    }
}
