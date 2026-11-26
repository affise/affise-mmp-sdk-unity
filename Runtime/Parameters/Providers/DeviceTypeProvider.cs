using AffiseAttributionLib.AffiseParameters.Base;
using AffiseAttributionLib.Module.Hardware.UseCase;

namespace AffiseAttributionLib.AffiseParameters.Providers
{
    /**
     * Provider for parameter [ProviderType.DEVICE_TYPE]
     */
    internal class DeviceTypeProvider : StringPropertyProvider
    {
        private readonly HardwareInfoUseCase _hardwareInfo;

        public DeviceTypeProvider(HardwareInfoUseCase hardwareInfo) => _hardwareInfo = hardwareInfo;

        public override float Order => 42.0f;
        public override ProviderType? Key => ProviderType.DEVICE_TYPE;

        public override string Provide() => _hardwareInfo.DeviceType;
    }
}
