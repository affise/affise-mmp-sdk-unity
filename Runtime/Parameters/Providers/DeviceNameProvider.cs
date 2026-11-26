using AffiseAttributionLib.AffiseParameters.Base;
using AffiseAttributionLib.Module.Hardware.UseCase;

namespace AffiseAttributionLib.AffiseParameters.Providers
{
    /**
     * Provider for parameter [ProviderType.DEVICE_NAME]
     */
    internal class DeviceNameProvider : StringPropertyProvider
    {
        private readonly HardwareInfoUseCase _hardwareInfo;

        public DeviceNameProvider(HardwareInfoUseCase hardwareInfo) => _hardwareInfo = hardwareInfo;

        public override float Order => 41.0f;
        public override ProviderType? Key => ProviderType.DEVICE_NAME;

        public override string Provide() => _hardwareInfo.DeviceName;
    }
}
